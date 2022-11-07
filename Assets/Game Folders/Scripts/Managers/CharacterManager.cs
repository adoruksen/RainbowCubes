using UnityEngine;
using CharacterController = Character.CharacterController;

namespace Managers
{
    public class CharacterManager : MonoBehaviour
    {
        public static CharacterManager Instance;

        public CharacterController playerCharacter;

        private void Awake()
        {
            Instance = this;
        }
    }
}

