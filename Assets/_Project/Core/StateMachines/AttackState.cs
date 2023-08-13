using UnityEngine;
using Zenject;

namespace TapTapTap.Core.FSM
{
    public class AttackState : State
    {
        public override int StateID => EntityStates.Attack;

        private EntityGatherer entityGatherer;

        private static readonly int Attacking = Animator.StringToHash("IsAttacking");

        private EntityView entityView;
        private Attributes attributes;
        private EntityStateMachine stateMachine;
        private EntityData entityData;

        [Inject]
        public void Initialize(
            EntityGatherer entityGatherer, 
            EntityView entityView, 
            Attributes attributes,
            EntityStateMachine stateMachine,
            EntityData entityData)
        {
            this.entityGatherer = entityGatherer;
            this.entityView = entityView;
            this.attributes = attributes;
            this.stateMachine = stateMachine;
            this.entityData = entityData;
        }

        public override void OnEnter()
        {
            base.OnEnter();

            attributes.ApplyAttributeModifier(AttributeDefinition.Speed, -0.25f);

            entityView.Animator.SetTrigger(Attacking);
            entityView.AnimatorCallbacks.OnAnimationStateHasChanged += OnAnimationStateHasChanged;

            entityView.WeaponRoot.SetActive(true);
        }

        public override void OnExit()
        {
            base.OnExit();

            entityView.AnimatorCallbacks.OnAnimationStateHasChanged -= OnAnimationStateHasChanged;
            entityView.WeaponRoot.SetActive(false);
        }

        private void OnAnimationStateHasChanged(AnimatorCallbacks.AnimationState animationState)
        {
            if (animationState == AnimatorCallbacks.AnimationState.Finished) {
                var myFraction = entityData.EntityArchetype.Fraction;
                var enemy = entityGatherer.GetClosestEntityMatchingPredicate(Owner.transform,
                    p => p.Data.EntityArchetype.Fraction != myFraction, 1.5f);

                if (enemy != null) {
                    stateMachine.EnqueueState(Owner.IsPlayer ? EntityStates.Idle : EntityStates.Attack);
                }

                if (Owner.IsPlayer) {
                    stateMachine.FinishState(EntityStates.Run);
                }
                else {
                    stateMachine.FinishState(EntityStates.Idle);
                }
            }
            else if (animationState == AnimatorCallbacks.AnimationState.Attack) {
                if (Blackboard.TargetEntity != null) {
                    Blackboard.TargetEntity.Attributes.ApplyAttributeModifier(AttributeDefinition.Health,
                        -attributes.GetAttributeValue(AttributeDefinition.Damage),
                        AttributeModifierFlag.ClampedZeroMax);
                }
            }
        }

        public class Factory : PlaceholderFactory<AttackState>
        {
        }
    }
}