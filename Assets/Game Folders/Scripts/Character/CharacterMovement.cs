using Managers;
using UnityEngine;

namespace Character
{
    public class CharacterMovement : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        public float moveSpeed;
        public bool IsActive;

        public bool UseBounds;
        public Bounds Bounds;

        private InputManager _input;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _input = InputManager.Instance;
        }

        public void Move()
        {
            if (!IsActive) return;

            var playerMove = new Vector3(_input.XPos, 0, 1);
            playerMove = playerMove.normalized * moveSpeed * Time.fixedDeltaTime;
            _rigidbody.MovePosition(_rigidbody.position + playerMove);
            if (UseBounds) _rigidbody.position = Bounds.ClosestPoint(_rigidbody.position);
        }
    }
}

