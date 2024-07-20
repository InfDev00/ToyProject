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

        private bool isFollowingDragPath = false;
        private Coroutine followPathCoroutine;

        private void Start()
        {
            ui.UpdateTimerDisplay(gamePlayTime);
            ui.jumpButton.onClick.AddListener(player.Jump);
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
        }

        private void OnDragEnd(Queue<Vector3> hitPoints, List<Enemy> hitSet)
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
                while (Vector3.Distance(player.transform.position, targetPoint) > 0.6f)
                {
                    var direction = (targetPoint - player.transform.position).normalized;
                    player.EntityMove.Move(direction);
                    yield return null;
                }
                hitPoints.Dequeue();
            }

            isFollowingDragPath = false;
            player.BeInvincibility(false);
            foreach (var obj in hitSet)
            {
                obj?.EntityMove.KnockBack(player.transform.position, 5000);
            }
            Util.SetTimeScale(1f);
            player.EntityMove.UpdateVelocity(10);
        }
    }
}