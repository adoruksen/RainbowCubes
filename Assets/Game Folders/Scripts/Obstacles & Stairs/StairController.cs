using System;
using StackSystem;
using Sirenix.OdinInspector;
using InteractionSystem;
using UnityEngine;
using CharacterController = Character.CharacterController;

namespace Stairs
{
    public class StairController : MonoBehaviour, IBeginInteract
    {
        public event Action<CharacterController> OnInteracted;

        private Collider _collider;
        private float _colliderYSize;
        [ShowInInspector, ReadOnly] public bool IsInteractable { get; private set; } = true;

        private void OnEnable()
        {
            _collider = GetComponent<BoxCollider>();
            _colliderYSize = _collider.bounds.size.y;
        }
        public void OnInteractBegin(IInteractor interactor)
        {
            var controller = (Stacker)interactor;
            FinishSequence(controller);
        }


        private void FinishSequence(Stacker controller)
        {
            controller.CharacterController.IsStairs = true;
            if (_colliderYSize <= controller.StackController.Stack)
            {
                for (int i = 0; i < _colliderYSize; i++)
                {
                    controller.StackController.UseStack();
                    controller.Rigidbody.detectCollisions = true;
                }
            }
            if (controller.StackController.Stack <= 0) controller.CharacterController.SetState(controller.CharacterController.WinState);
        }
    }
}

