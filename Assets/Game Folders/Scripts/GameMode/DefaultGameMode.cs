using System;
using CameraSystem;
using LevelSystem;
using StackSystem;
using UI;
using DG.Tweening;
using UnityEngine;

namespace Managers.GameModes
{
    [CreateAssetMenu(menuName = "Game/GameMode/DefaultGameMode", fileName = "DefaultGameMode", order = -399)]
    public class DefaultGameMode : GameMode
    {
        public LevelConfig[] Levels;

        [SerializeField] private CameraConfig _introConfig;


        [SerializeField] private bool _playCameraAnimation;
        [SerializeField] private float _cameraAnimationSpeed;
        [SerializeField] private AnimationCurve _cameraAnimationCurve;

        public override void InitializeGameMode()
        {
            var config = Levels[GameManager.Instance.GetSavedLevel() % Levels.Length];
            LevelManager.Instance.SpawnLevel(config.Parts);
            CharacterManager.Instance.SpawnPlayer();
            var character = CharacterManager.Instance.Player;
            var startArea = LevelManager.Instance.Level.GameAreas[0];
            character.Movement.Bounds = startArea.PlayArea;
            startArea.OnCharacterEntered(character);
            character.SetState(character.IdleState);
            IntroUIController.instance.ShowInstant();
        }

        public override void StartGameMode(Action levelStart)
        {
            IntroUIController.instance.Hide();
            if (!_playCameraAnimation)
            {
                StartGame();
                return;
            }
            var sequence = DOTween.Sequence();
            var gameAreas = LevelManager.Instance.Level.GameAreas;
            CameraController.Instance.SetConfig(_introConfig);
            sequence.OnComplete(StartGame);

            void StartGame()
            {
                CameraController.Instance.SetTarget(CharacterManager.Instance.Player.GetComponent<CameraFollowTarget>());
                levelStart.Invoke();
            }
        }

        public override void CompleteGameMode()
        {
            GameManager.Instance.SaveLevel(GameManager.Instance.GetSavedLevel() + 1);
            DOVirtual.DelayedCall(1f, WinUIController.instance.Show, false);
        }

        public override void FailGameMode()
        {
            DOVirtual.DelayedCall(2f, FailUIController.instance.Show, false);
        }

        public override void SkipGameMode()
        {
            GameManager.Instance.SaveLevel(GameManager.Instance.GetSavedLevel() + 1);
        }

        public override void DeinitializeGameMode()
        {
            LevelManager.Instance.DestroyLevel();
            CharacterManager.Instance.DestroyCharacter();
        }
    }
}