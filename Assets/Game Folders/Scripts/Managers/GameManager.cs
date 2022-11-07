using System;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public static event Action OnGameStarted;
        public static event Action OnGameEnd;

        public bool IsPlaying { get; private set; }

        private void Awake()
        {
            Instance = this;
        }
        private void Start()
        {
            InitializeGame();
        }

        private void InitializeGame()
        {
            IsPlaying = true;
            OnGameStarted?.Invoke();
        }
    }
}

