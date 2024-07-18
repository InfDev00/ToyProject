using System;
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

        
        private void Start()
        {
            ui.UpdateTimerDisplay(gamePlayTime);
            ui.jumpButton.onClick.AddListener(player.Jump);
        }

        private void FixedUpdate()
        {
            player.EntityMove.MovePlayer(ui.joyStick.InputVector); //플레이어 이동
            gamePlayTime -= Time.fixedDeltaTime;
            
            ui.UpdateTimerDisplay(gamePlayTime);
        }
    }
}