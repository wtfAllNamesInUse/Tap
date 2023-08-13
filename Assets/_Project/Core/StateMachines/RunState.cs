using UnityEngine;
using Zenject;

namespace TapTapTap.Core.FSM
{
    public class RunState : State
    {
        private static readonly int IsRunning = Animator.StringToHash("IsRunning");
        public override int StateID => EntityStates.Run;

        private Movement movement;
        private EntityView entityView;

        [Inject]
        protected void Initialize(Movement movement, EntityView entityView)
        {
            this.movement = movement;
            this.entityView = entityView;
        }

        public override void OnEnter()
        {
            entityView.Animator.SetBool(IsRunning, true);
            movement.IsMoving = true;
        }

        public override void Tick()
        {
        }

        public override void OnExit()
        {
            entityView.Animator.SetBool(IsRunning, false);
            movement.IsMoving = false;
        }

        public class Factory : PlaceholderFactory<RunState>
        {
        }
    }
}