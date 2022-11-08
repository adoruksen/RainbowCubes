using Managers.GameModes;
using UnityEngine;

namespace Managers
{
    public class GameManager : BaseGameManager
    {
        public static GameManager Instance;

        public static event GameEvents OnGameInitialized;
        public static event GameEvents OnGameEnded;

        [SerializeField] private GameMode _defaultGameMode;
        private GameMode _currentGameMode;

        public Transform DefaultParent;

        public bool IsPlaying { get; private set; }

        private void Awake()
        {
            Instance = this;
        }
        private void Start()
        {
            InitializeGameMode(_defaultGameMode);
        }

        public void InitializeGameMode(GameMode gameMode)
        {
            if (_currentGameMode != null) _currentGameMode.DeinitializeGameMode();
            ClearDefaultParent();
            _currentGameMode = gameMode;
            _currentGameMode.InitializeGameMode();
            LevelInitialize();
        }

        public void StartGameMode()
        {
            _currentGameMode.StartGameMode(LevelStart);
            IsPlaying = true;
        }

        public void CompleteGameMode()
        {
            LevelEnd();
            IsPlaying = false;
            _currentGameMode.CompleteGameMode();
        }

        public void FailGameMode()
        {
            IsPlaying = false;
            _currentGameMode.FailGameMode();
            LevelEnd();
        }

        public override int GetLevel()
        {
            return GetSavedLevel();
        }

        public override string GetLevelString()
        {
            return GetLevel().ToString();
        }

        protected void LevelInitialize()
        {
            OnGameInitialized?.Invoke();
        }

        protected void LevelEnd()
        {
            OnGameEnded?.Invoke();
        }

        private void ClearDefaultParent()
        {
            for (var i = 0; i < DefaultParent.childCount; i++)
            {
                Destroy(DefaultParent.GetChild(i).gameObject);
            }
        }
    }
}

