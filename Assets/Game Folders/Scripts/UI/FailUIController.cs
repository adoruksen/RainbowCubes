using Managers;
using Managers.GameModes;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class FailUIController : UIController<FailUIController>
    {
        [SerializeField] private Button _retryButton;
        [SerializeField] private GameMode _gameMode;

        private void OnEnable()
        {
            _retryButton.onClick.AddListener(RetryButtonPressed);
        }

        private void OnDisable()
        {
            _retryButton.onClick.RemoveListener(RetryButtonPressed);
        }

        private void RetryButtonPressed()
        {
            GameManager.Instance.InitializeGameMode(_gameMode);
            HideInstant();
        }
    }
}
