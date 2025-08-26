using Assets.Project.Scripts.Core.Interfaces;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace Assets.Project.Scripts.Core.Managers
{
    public class UIManager : MonoBehaviour, IUIManager
    {
        [SerializeField] private TMP_Text _weightText;
        [SerializeField] private TMP_Text _timerText;
        [SerializeField] private TMP_Text _passengerCountText;
        [SerializeField] private Slider _waterLevel;
        [SerializeField] private GameObject _startPanel;
        [SerializeField] private GameObject _pausePanel;

        private string _weightTextFormat = "{0} kg";
        private string _timerTextFormat = "{0} s";
        private string _passengerCountTextFormat = "{0} p";
        private bool _isPaused = false;
        private const int SecondsToWin = 120;
        public static bool IsChangingScene;
        private UIAction _input;

        [Inject] private IShipManager _shipManager;

        private void Awake()
        {
            _input = new UIAction();
            _input.UI.Pause.performed += Pause;
        }

        private void Start()
        {
            IsChangingScene = false;
            _timerText.text = string.Format(_timerTextFormat, SecondsToWin);
            if (!PlayerPrefs.HasKey("IsFirstTime"))
            {
                Time.timeScale = 0f;
            }
            else
            {
                StartGame();
            }
        }

        public void UpdateWeightText(float weight)
        {
            _weightText.text = string.Format(_weightTextFormat, weight);
        }

        private IEnumerator Timer()
        {
            int seconds = SecondsToWin;
            _timerText.text = string.Format(_timerTextFormat, seconds);
            while (seconds > 0)
            {
                yield return new WaitForSeconds(1f);
                seconds--;
                _timerText.text = string.Format(_timerTextFormat, seconds);
            }

            _input.UI.Pause.performed -= Pause;
            _input.Disable();
            IsChangingScene = true;
            SceneManager.LoadScene("WinWindow");
        }

        private IEnumerator RaiseWaterLevel()
        {
            while(_waterLevel.value != _waterLevel.maxValue)
            {
                yield return null;

                _waterLevel.value += _shipManager.TotalWeight / 1000f * Time.deltaTime;
            }

            _input.UI.Pause.performed -= Pause;
            _input.Disable();
            SceneManager.LoadScene("LoseWindow");
        }

        public void UpdatePassengerText(int amount)
        {
            _passengerCountText.text = string.Format(_passengerCountTextFormat, amount);
        }

        public void StartGame()
        {
            Time.timeScale = 1f;
            _startPanel.SetActive(false);
            StartCoroutine(RaiseWaterLevel());
            StartCoroutine(Timer());
            _input.Enable();
            PlayerPrefs.SetInt("IsFirstTime", 1);
        }

        public void Pause(InputAction.CallbackContext callback)
        {
            _isPaused = !_isPaused;
            if (_isPaused)
            {
                _pausePanel.SetActive(true);
                Time.timeScale = 0f;
            }
            else
            {
                _pausePanel.SetActive(false);
                Time.timeScale = 1f;
            }
        }

        public void ExitGame()
        {
            Application.Quit();
        }
    }
}