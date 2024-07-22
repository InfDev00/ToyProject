using System.Collections;
using System.Collections.Generic;
using Entities;
using UI;
using UnityEngine;
using Utils;

namespace Managers
{
    public class GamePlayManager : MonoBehaviour
    {
        public float gamePlayTime;

        [Header("Entities")]
        public PlayerController player;
        public GamePlayUI ui;

        public StageManager stageManager;
        public EnemyManager enemyManager;
        
        private bool isFollowingDragPath = false;
        private Coroutine followPathCoroutine;

        public void SetGamePlayTime(Stage stageInfo)
        {
            gamePlayTime = stageInfo.TotalStageTime;
        }


        private void Start()
        {
            ui.UpdateTimerDisplay(gamePlayTime);
            ui.jumpButton.onClick.AddListener(player.Jump);

            StageManager.OnStageChanged += SetGamePlayTime;
            StageManager.OnStageChanged?.Invoke(stageManager.currentStage);

            ui.drag.DragEndAction += OnDragEnd;
        }

        private void FixedUpdate()
        {
            if (!isFollowingDragPath)
            {
                player.EntityMove.MovePlayer(ui.joyStick.InputVector); // Player movement
            }

            gamePlayTime -= Time.fixedDeltaTime;
            ui.UpdateTimerDisplay(gamePlayTime);
            ui.UpdateStageDisplay(stageManager.currentStage, enemyManager.UpdateEnemyCount());

            stageManager.UpdateStage(stageManager.currentStage.type, gamePlayTime, enemyManager.UpdateEnemyCount());
        }

        private void OnDestroy()
        {
            StageManager.OnStageChanged -= SetGamePlayTime;
        }

        private void OnDragEnd(Queue<Vector3> hitPoints, List<Enemy> hitSet) // 추후 다른 위치로 변경
        {
            if (followPathCoroutine != null)
            {
                StopCoroutine(followPathCoroutine);
            }
            followPathCoroutine = StartCoroutine(FollowDragPath(hitPoints, hitSet));
        }

        private IEnumerator FollowDragPath(Queue<Vector3> hitPoints, List<Enemy> hitSet)
        {
            isFollowingDragPath = true;
            player.EntityMove.UpdateVelocity(200);
            player.BeInvincibility(true);
            while (hitPoints.Count > 0)
            {
                var targetPoint = hitPoints.Peek();
                while (Vector3.Distance(player.transform.position, targetPoint) > 1f)
                {
                    var direction = (targetPoint - player.transform.position).normalized;
                    player.EntityMove.Move(direction);
                    yield return null;
                }
                player.EntityMove.Stop();
                hitPoints.Dequeue();
            }

            isFollowingDragPath = false;
            player.BeInvincibility(false);
            foreach (var enemy in hitSet)
            {
                enemy.EntityMove.KnockBack(player.transform.position, 5000);
            }
            Util.SetTimeScale(1f);
            player.EntityMove.UpdateVelocity(10);
        }
    }
}