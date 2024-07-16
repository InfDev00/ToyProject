using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace UI
{
    public class GamePlayUI : MonoBehaviour
    {
        [Header("Controller")] 
        public Button jumpButton;
        public JoyStick joyStick;
        
        [Header("UpperUI")] 
        public Button pauseButton;
        public TextMeshProUGUI timerText;
        public TextMeshProUGUI goldText;
        // 자원의 추가 및 정정에 따라 이 부분 수정 예정

        [Header("PopupUI")] 
        public GameObject popupUI;
        public Toggle soundToggle;
        public Slider soundSlider;
        public Button resumeButton;
        public Button exitButton;

        private float _lastNonZeroVolume = 1f;
        private bool _isAdjusting;
        
        private void Awake()
        {
            soundSlider.onValueChanged.AddListener(OnVolumeChanged);
            soundToggle.onValueChanged.AddListener(OnSoundToggled);
            pauseButton.onClick.AddListener(Pause);
            resumeButton.onClick.AddListener(Resume);

            soundSlider.value = 1;
            _lastNonZeroVolume = 1;
            
            popupUI.SetActive(false);
        }

        public void UpdateTimerDisplay(float time)
        {
            var minutes = Mathf.FloorToInt(time / 60);
            var seconds = Mathf.FloorToInt(time % 60);

            timerText.text = $"{minutes:00}:{seconds:00}";
        }

        private void OnVolumeChanged(float volume)
        {
            if (_isAdjusting) return;

            _isAdjusting = true;
    
            if (volume > 0)
            {
                _lastNonZeroVolume = volume;
                soundToggle.isOn = true;
            }
            else
            {
                soundToggle.isOn = false;
            }

            _isAdjusting = false;
        }

        private void OnSoundToggled(bool isOn)
        {
            if (_isAdjusting) return;

            _isAdjusting = true;

            if (isOn)
            {
                soundSlider.value = _lastNonZeroVolume;
            }
            else
            {
                soundSlider.value = 0;
            }

            _isAdjusting = false;
        }

        private void Pause()
        {
            Util.SetTimeScale(0);
            popupUI.SetActive(true);
        }

        private void Resume()
        {
            Util.SetTimeScale(1);
            popupUI.SetActive(false);
        }
    }
}