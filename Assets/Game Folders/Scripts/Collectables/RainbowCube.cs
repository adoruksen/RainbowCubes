using System;
using Sirenix.OdinInspector;
using InteractionSystem;
using CharacterController = Character.CharacterController;
using DG.Tweening;
using UnityEngine;

namespace StackSystem
{
    public class RainbowCube : MonoBehaviour, IBeginInteract
    {
        public event Action<CharacterController> OnCollected;
        [ShowInInspector, ReadOnly] public bool IsInteractable { get; private set; } = true;
        [SerializeField] private Collider _collider;

        private CharacterController _controller;

        private Color _color;

        private void OnEnable()
        {
            OnCollected += CharacterControllerStuff;
            _color = GetComponentInChildren<Renderer>().material.color;
        }
        private void OnDisable()
        {
            OnCollected -= CharacterControllerStuff;
        }
        public void OnInteractBegin(IInteractor interactor)
        {
            var controller = (Stacker)interactor;
            Collect(controller);
        }

        private void Collect(Stacker controller)
        {
            controller.AudioSource.Play();
            controller.StackController.AddStack(this);
            OnCollected?.Invoke(controller.CharacterController);
            controller.GetRandomEmoji();
            controller.SetTrail(_color);
            SetInteractable(false);
        }

        public void SetInteractable(bool interactable)
        {
            IsInteractable = interactable;
        }

        public void SetLost()
        {
            transform.SetParent(Managers.GameManager.Instance.DefaultParent);
            CharacterControllerStuff(_controller);
        }

        private void CharacterControllerStuff(CharacterController controller)
        {
            _controller = controller;
            _controller.FinishConditions();
            _controller.ModelTransformSetter();
        }

    }

}
