using System.Collections.Generic;
using System.Linq;
using Managers;
using CharacterController = Character.CharacterController;

namespace LevelSystem
{
    public class StackAreaManager : GameAreaManager
    {
        public override void OnCharacterEntered(CharacterController character)
        {
            if (!GameManager.Instance.IsPlaying) return;

            character.SetState(character.StackState);
        }

    }
}