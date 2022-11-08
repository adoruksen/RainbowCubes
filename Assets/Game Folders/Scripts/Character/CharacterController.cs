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
        [SerializeReference, BoxGroup("Winn", false), HorizontalGroup("Winn/Group")] public State WinState;
        [SerializeReference, BoxGroup("Fail", false), HorizontalGroup("Fail/Group")] public State FailState;

        [ShowInInspector, ReadOnly, BoxGroup("States", false)] public State CurrentState { get; private set; }

        public Transform ModelTransform { get; private set; }
        public Rigidbody Rigidbody { get; private set; }
        public CharacterMovement Movement { get; private set; }

        public StackerLeft leftStacker;
        public StackerRight rightStacker;
        public bool IsStairs { get; set; } = false;

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
            Movement = GetComponent<CharacterMovement>();
            ModelTransform = transform.GetChild(0).transform;
            SetState(IdleState);
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
            if (IsStairs) return;
            if (leftStacker.StackController.Stack > rightStacker.StackController.Stack) ModelTransform.position = new Vector3(transform.position.x, leftStacker.StackController.Stack +.75f, transform.position.z);
            else if (rightStacker.StackController.Stack > leftStacker.StackController.Stack) ModelTransform.position = new Vector3(transform.position.x, rightStacker.StackController.Stack +.75f, transform.position.z);
            else ModelTransform.position = new Vector3(transform.position.x, rightStacker.StackController.Stack + .75f, transform.position.z); ;
        }

        public void FinishConditions()
        {
            if (Mathf.Abs(leftStacker.StackController.Stack - rightStacker.StackController.Stack) >= 3) SetState(FailState);
        }
    }
}

