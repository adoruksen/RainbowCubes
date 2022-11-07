using Managers;
using UnityEngine;

namespace InteractionSystem
{
    public class Interactor : MonoBehaviour
    {
        private IInteractor _controller;
        public bool CanInteract;

        private void Awake()
        {
            _controller = GetComponentInParent<IInteractor>(); // interactor atilmis objenin parent`in interface IInteractor`u ceker.
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!CanInteract || !GameManager.Instance.IsPlaying) return;
            if (!other.CompareTag(tag)) return;

            var hasBeginInteractable = other.TryGetComponent<IBeginInteract>(out var interactable);
            if (hasBeginInteractable && interactable.IsInteractable) interactable.OnInteractBegin(_controller);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!CanInteract || !GameManager.Instance.IsPlaying) return;
            if (!other.CompareTag(tag)) return;

            var hasEndInteractable = other.TryGetComponent<IEndInteract>(out var interactable);
            if (hasEndInteractable && interactable.IsInteractable) interactable.OnInteractEnd(_controller);
        }
    }
}