using UnityEngine;
using Zenject;

namespace TapTapTap.Core.FSM
{
    public class RunState : EntityState
    {
        private static readonly int IsRunning = Animator.StringToHash("IsRunning");
        public override int StateID => EntityStates.Run;

        public override void OnEnter()
        {
            Owner.Animator.SetBool(IsRunning, true);
            Owner.Movement.IsMoving = true;
        }

        public override void Tick()
        {
        }

        public override void OnExit()
        {
            Owner.Animator.SetBool(IsRunning, false);
            Owner.Movement.IsMoving = false;
        }

        public class Factory : PlaceholderFactory<RunState>
        {
        }
    }
}