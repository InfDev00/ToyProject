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

        private void OnDragEnd(Queue<Vector3> hitPoints, HashSet<GameObject> hitSet)
        {
            if (followPathCoroutine != null)
            {
                StopCoroutine(followPathCoroutine);
            }
            followPathCoroutine = StartCoroutine(FollowDragPath(hitPoints, hitSet));
        }

        private IEnumerator FollowDragPath(Queue<Vector3> hitPoints, HashSet<GameObject> hitSet)
        {
            isFollowingDragPath = true;
            player.EntityMove.UpdateVelocity(200);
            player.GetComponent<BoxCollider>().isTrigger = true;
            player.GetComponent<Rigidbody>().useGravity = false;
            while (hitPoints.Count > 0)
            {
                Vector3 targetPoint = hitPoints.Peek();
                while (Vector3.Distance(player.transform.position, targetPoint) > 0.6f)
                {
                    Vector3 direction = (targetPoint - player.transform.position).normalized;
                    player.EntityMove.Move(direction);
                    yield return null;
                }
                hitPoints.Dequeue();
            }

            isFollowingDragPath = false;
            player.GetComponent<BoxCollider>().isTrigger = false;
            player.GetComponent<Rigidbody>().useGravity = true;
            foreach (var obj in hitSet)
            {

            }
            Util.SetTimeScale(1f);
            player.EntityMove.UpdateVelocity(10);
        }
    }
}