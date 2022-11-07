using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class FailUIController : UIController<FailUIController>
    {
        [SerializeField] private Button _retryButton;

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
            HideInstant();
        }
    }
}
