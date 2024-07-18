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
        private Queue<Vector3> dragPath;
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

        private void OnDragEnd(Queue<Vector3> hitPoints)
        {
            dragPath = new Queue<Vector3>(hitPoints);
            if (followPathCoroutine != null)
            {
                StopCoroutine(followPathCoroutine);
            }
            followPathCoroutine = StartCoroutine(FollowDragPath());
        }

        private IEnumerator FollowDragPath()
        {
            isFollowingDragPath = true;

            while (dragPath.Count > 0)
            {
                Vector3 targetPoint = dragPath.Peek();
                while (Vector3.Distance(player.transform.position, targetPoint) > 0.6f)
                {
                    Vector3 direction = (targetPoint - player.transform.position).normalized;
                    player.EntityMove.Move(direction);
                    yield return null; // Wait for the next frame
                    Debug.Log(dragPath.Count);
                }
                dragPath.Dequeue();
            }

            isFollowingDragPath = false;
        }
    }
}