using UnityEngine;
using Managers;
using Managers.GameModes;
using UnityEngine.UI;

namespace UI
{
    public class WinUIController : UIController<WinUIController>
    {
        [SerializeField] private Button _nextButton;
        [SerializeField] private GameMode _gameMode;

        private void OnEnable()
        {
            _nextButton.onClick.AddListener(NextButtonPressed);
        }

        private void OnDisable()
        {
            _nextButton.onClick.RemoveListener(NextButtonPressed);
        }

        private void NextButtonPressed()
        {
            GameManager.Instance.InitializeGameMode(_gameMode);
            HideInstant();
        }
    }
}