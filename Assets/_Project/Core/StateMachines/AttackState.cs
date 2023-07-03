using UnityEngine;
using Zenject;

namespace TapTapTap.Core.FSM
{
    public class AttackState : EntityState
    {
        public override int StateID => EntityStates.Attack;

        private EntityGatherer entityGatherer;

        private static readonly int Attacking = Animator.StringToHash("IsAttacking");

        [Inject]
        public void Initialize(EntityGatherer entityGatherer)
        {
            this.entityGatherer = entityGatherer;
        }

        public override void OnEnter()
        {
            base.OnEnter();

            Owner.Attributes.ApplyAttributeModifier(AttributeDefinition.Speed, -0.25f);

            Owner.Animator.SetTrigger(Attacking);
            Owner.AnimatorCallbacks.OnAnimationStateHasChanged += OnAnimationStateHasChanged;

            Owner.WeaponRoot.SetActive(true);
        }

        public override void OnExit()
        {
            base.OnExit();

            Owner.AnimatorCallbacks.OnAnimationStateHasChanged -= OnAnimationStateHasChanged;
            Owner.WeaponRoot.SetActive(false);
        }

        private void OnAnimationStateHasChanged(AnimatorCallbacks.AnimationState animationState)
        {
            if (animationState == AnimatorCallbacks.AnimationState.Finished) {
                var myFraction = Owner.Data.EntityArchetype.Fraction;
                var enemy = entityGatherer.GetClosestEntityMatchingPredicate(Owner.transform,
                    p => p.Data.EntityArchetype.Fraction != myFraction, 1.5f);

                if (enemy != null) {
                    Owner.StateMachine.EnqueueState(Owner.IsPlayer ? EntityStates.Idle : EntityStates.Attack);
                }

                if (Owner.IsPlayer) {
                    Owner.StateMachine.FinishState(EntityStates.Run);
                }
                else {
                    Owner.StateMachine.FinishState(EntityStates.Idle);
                }
            }
            else if (animationState == AnimatorCallbacks.AnimationState.Attack) {
                if (Blackboard.TargetEntity != null) {
                    Blackboard.TargetEntity.Attributes.ApplyAttributeModifier(AttributeDefinition.Health,
                        -Owner.Attributes.GetAttributeValue(AttributeDefinition.Damage),
                        AttributeModifierFlag.ClampedZeroMax);
                }
            }
        }

        public class Factory : PlaceholderFactory<AttackState>
        {
        }
    }
}