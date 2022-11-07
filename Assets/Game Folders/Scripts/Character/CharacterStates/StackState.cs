using System;

namespace Character.StateMachine
{
    [Serializable]
    public class StackState : State
    {
        public override void OnStateFixedUpdate(CharacterController controller)
        {
            controller.Movement.Move();
        }
    }
}
