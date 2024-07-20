using System;
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

        public void SetGamePlayTime(Stage stageInfo)
        {
            gamePlayTime = stageInfo.TotalStageTime;
        }

        private void Awake()
        {
            StageManager.OnStageChanged += SetGamePlayTime;
        }

        private void Start()
        {
            ui.UpdateTimerDisplay(gamePlayTime);
            ui.jumpButton.onClick.AddListener(player.Jump);
            StageManager.OnStageChanged?.Invoke(stageManager.currentStage);
        }

        private void FixedUpdate()
        {
            player.EntityMove.MovePlayer(ui.joyStick.InputVector); //플레이어 이동
            gamePlayTime -= Time.fixedDeltaTime;
            
            ui.UpdateTimerDisplay(gamePlayTime);
            ui.UpdateStageDisplay(stageManager.currentStage, enemyManager.UpdateEnemyCount());

            stageManager.UpdateStage(stageManager.currentStage.type, gamePlayTime, enemyManager.UpdateEnemyCount());
        }

        private void OnDestroy()
        {
            StageManager.OnStageChanged -= SetGamePlayTime;
        }
    }
}