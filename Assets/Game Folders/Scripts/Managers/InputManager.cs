using UnityEngine;

namespace Managers
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance;

        private Touch _touch;

        private float _xPos;
        public float XPos => _xPos;
   
        private float _width;

        private void Awake()
        {
            Instance = this;

            _xPos = 0f;
            _width = (float)Screen.width / 2f;
        }
        private void Update()
        {
            if (!GameManager.Instance.IsPlaying) return;
            TouchInput();
        }
        public void TouchInput()
        {
            if (Input.touchCount > 0)   //touch the screen with one finger at least
            {
                _touch = Input.GetTouch(0);

                if (_touch.phase == TouchPhase.Moved)
                {
                    var pos = _touch.position;
                    _xPos = (pos.x - _width) / _width;
                }

                else
                {
                    _xPos = 0f;   //otherwise it keeps moving even if you don't touch it
                }
            }
        }

    }
}

