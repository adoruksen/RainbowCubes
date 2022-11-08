using DG.Tweening;
using Managers;

namespace Character.StateMachine
{
    public class FailState : State
    {
        protected override void OnStateEnter(CharacterController controller)
        {
            controller.DOKill();
            controller.Rigidbody.isKinematic = true;
            GameManager.Instance.FailGameMode();
        }
    }
}
