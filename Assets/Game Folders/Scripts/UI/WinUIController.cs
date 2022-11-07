using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class WinUIController : UIController<WinUIController>
    {
        [SerializeField] private Button _nextButton;

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
            HideInstant();
        }
    }
}