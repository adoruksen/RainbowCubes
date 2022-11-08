using InteractionSystem;
using StackSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Obstacles
{
    public class ObstacleController : MonoBehaviour, IBeginInteract
    {
        [ShowInInspector,ReadOnly] public bool IsInteractable { get; private set; } = true;

        private Collider _collider;
        private float _colliderYSize;

        private void OnEnable()
        {
            _collider = GetComponent<BoxCollider>();
            _colliderYSize = _collider.bounds.size.y;
        }
        public void OnInteractBegin(IInteractor interactor)
        {
            var controller = (Stacker)interactor;
            LostCube(controller);
        }

        private void LostCube(Stacker controller)
        {
            if (_colliderYSize <= controller.StackController.Stack) 
            {
                if (controller.StackController.Stack <= 0) 
                {
                    controller.CharacterController.SetState(controller.CharacterController.FailState);
                    return; 
                } 
                for (int i = 0; i < _colliderYSize; i++)
                {
                    controller.StackController.LoseStack();
                    controller.Rigidbody.detectCollisions = true;
                }
            }
        }
    }
}

