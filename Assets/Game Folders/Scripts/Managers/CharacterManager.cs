using UnityEngine;
using System;
using Sirenix.OdinInspector;
using CharacterController = Character.CharacterController;

namespace Managers
{
    public class CharacterManager : MonoBehaviour
    {
        public static CharacterManager Instance;

        public static event Action OnCharacterSpawned;

        [SerializeField] private CharacterController _playerCharacter;
        [ReadOnly] public CharacterController Player;


        private void Awake()
        {
            Instance = this;
        }

        private void OnEnable()
        {
            BaseGameManager.OnLevelStart += StartCharacter;
        }

        private void OnDisable()
        {
            BaseGameManager.OnLevelStart -= StartCharacter;
        }

        public void SpawnPlayer()
        {
            Player = Instantiate(_playerCharacter, Vector3.zero, Quaternion.identity);
        }

        public void DestroyCharacter()
        {
            Destroy(Player.gameObject);
        }

        private void StartCharacter()
        {
            Player.SetState(Player.StackState);
        }
    }
}

