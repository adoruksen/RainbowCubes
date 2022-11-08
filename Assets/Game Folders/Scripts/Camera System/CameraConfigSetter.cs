using Managers;
using CharacterController = Character.CharacterController;
using UnityEngine;

namespace CameraSystem
{
    public class CameraConfigSetter : MonoBehaviour
    {
        [SerializeField] private CameraConfig _stackConfig;
        [SerializeField] private CameraConfig _finishConfig;

        private CharacterController _registeredCharacter;

        private void OnEnable()
        {
            GameManager.OnGameInitialized += OnInitialize;
            GameManager.OnGameEnded += OnEnd;
        }
        private void OnDisable()
        {
            GameManager.OnGameInitialized -= OnInitialize;
            GameManager.OnGameEnded -= OnEnd;
        }
        private void OnInitialize()
        {
            RegisterPlayer();
            CameraController.Instance.ResetCamera();
        }

        private void OnEnd()
        {
            UnregisterPlayer();
            SetFinishCamera();
        }
        private void RegisterPlayer()
        {
            _registeredCharacter = CharacterManager.Instance.Player;
            _registeredCharacter.StackState.OnStateEntered += SetStackCamera;
        }

        private void UnregisterPlayer()
        {
            _registeredCharacter.StackState.OnStateEntered -= SetStackCamera;
        }
        private void SetStackCamera(CharacterController obj)
        {
            CameraController.Instance.SetConfig(_stackConfig);
        }

        private void SetFinishCamera()
        {
            CameraController.Instance.SetConfig(_finishConfig);
        }
    }
}

