using System;
using Character.StateMachine;
using Sirenix.OdinInspector;
using StackSystem;
using UnityEngine;

namespace Character
{
    public class CharacterController : MonoBehaviour
    {
        [SerializeReference, BoxGroup("Idle", false), HorizontalGroup("Idle/Group")] public State IdleState;
        [SerializeReference, BoxGroup("Stack", false), HorizontalGroup("Stack/Group")] public StackState StackState;
        [SerializeReference, BoxGroup("Fnsh", false), HorizontalGroup("Fnsh/Group")] public FinishState FinishState;

        [ShowInInspector, ReadOnly, BoxGroup("States", false)] public State CurrentState { get; private set; }

        public Transform ModelTransform { get; private set; }
        public Rigidbody Rigidbody { get; private set; }
        public CharacterMovement Movement { get; private set; }

        public StackerLeft leftStacker;
        public StackerRight rightStacker;

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
            Movement = GetComponent<CharacterMovement>();
            ModelTransform = transform.GetChild(0).transform;
            SetState(StackState);
        }

        private void FixedUpdate()
        {
            CurrentState?.OnStateFixedUpdate(this);
        }

        public void ExitState()
        {
            CurrentState?.StateExit(this);
        }

        public void SetState(State newState)
        {
            ExitState();
            CurrentState = newState;
            CurrentState.StateEnter(this);
        }

        public void ModelTransformSetter()
        {
            if (leftStacker.StackController.Stack > rightStacker.StackController.Stack) ModelTransform.position = Vector3.up * leftStacker.StackController.Stack;
            else if (rightStacker.StackController.Stack > leftStacker.StackController.Stack) ModelTransform.position = Vector3.up * rightStacker.StackController.Stack;
            else ModelTransform.position = Vector3.up * rightStacker.StackController.Stack;
        }

    }
}

