using System;
using Sirenix.OdinInspector;
using InteractionSystem;
using UnityEngine;

namespace StackSystem
{
    public class RainbowCube : MonoBehaviour, IBeginInteract
    {
        public event Action OnCollected;
        [ShowInInspector, ReadOnly] public bool IsInteractable { get; private set; }
        [SerializeField] private Collider _collider;

        public void OnInteractBegin(IInteractor interactor)
        {
            var controller = (Stacker)interactor;
            Collect(controller);
        }

        private void Collect(Stacker controller)
        {
            OnCollected?.Invoke();
            controller.AudioSource.Play();
            controller.StackController.AddStack(this);
            controller.CharacterController.ModelTransformSetter();
            SetInteractable(false);
        }

        public void SetInteractable(bool interactable)
        {
            IsInteractable = interactable;
        }

        public void SetLost()
        {
            transform.SetParent(null);
            SetInteractable(true);
        }

    }

}
