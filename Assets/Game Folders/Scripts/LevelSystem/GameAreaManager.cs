using UnityEngine;
using CharacterController = Character.CharacterController;

namespace LevelSystem
{
    public class GameAreaManager : MonoBehaviour
    {
        public Bounds PlayArea => _playArea;
        [SerializeField] private Bounds _playArea;

        public void MoveArea(Vector3 position)
        {
            _playArea.center += position - transform.position;
            transform.position = position;
        }


        public virtual void OnCharacterEntered(CharacterController character) { }
        public virtual void OnCharacterExited(CharacterController character) { }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = new Color(0f, 1f, 1f, 1f);
            Gizmos.DrawWireCube(PlayArea.center, PlayArea.size);
            Gizmos.color = new Color(0f, 1f, 1f, .5f);
            Gizmos.DrawCube(PlayArea.center, PlayArea.size);
        }
    }
}