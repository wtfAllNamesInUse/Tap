using System.Threading.Tasks;
using TapTapTap.Core.FSM;
using UnityEngine;
using Zenject;

namespace TapTapTap.Core
{
    [SelectionBase]
    public class Entity : MonoBehaviour
    {
        public Attributes Attributes => attributes;
        public Movement Movement => movement;
        public EntityStateMachine StateMachine => stateMachine;
        public EntityData Data => data;

        public bool IsPlayer => Data.EntityArchetype.Fraction == EntityFraction.Player;
        public bool IsAlive => isAlive;

        public bool IsAttacking => StateMachine.CurrentState?.StateID == EntityStates.Attack;
        public bool IsRunning => StateMachine.CurrentState?.StateID == EntityStates.Run;

        private EntityView entityView;

        [SerializeField]
        private Attributes attributes;

        [SerializeField]
        private Movement movement;

        private EntityData data;
        private EntityStateMachine stateMachine;
        private EntityGatherer entityGatherer;
        private HealthBar healthBar;

        private bool isAlive = true;

        private const int DieMsDelay = 1500;

        [Inject]
        public void Inject(
            EntityData data,
            EntityStateMachine.Factory stateMachineFactory,
            EntityGatherer entityGatherer,
            EntityView.Factory entityViewFactory,
            HealthBar.Factory healthBarFactory)
        {
            this.data = data;
            this.entityGatherer = entityGatherer;
            this.entityGatherer.Register(this);

            // TODO: healthBar factory is needed because we need to bind inside this gameObject subContainer instead of sceneContext or projectContext
            // TODO: is it possible to use screenController and bind to this gameObject subContainer?
            healthBar = healthBarFactory.Create();

            attributes.DoInit(this.data.EntityArchetype.Attributes, OnAttributeHasChanged);

            entityView = entityViewFactory.Create(data.EntityArchetype.Prefab);
            entityView.transform.SetParent(transform);

            stateMachine = stateMachineFactory.Create();
            stateMachine.Initialize();
        }

        public void TryAttack()
        {
            var myFraction = data.EntityArchetype.Fraction;
            var enemy = entityGatherer.GetClosestEntityMatchingPredicate(transform,
                p => p.Data.EntityArchetype.Fraction != myFraction, 1.5f);
            if (enemy != null) {
                if (IsAttacking) {
                    return;
                }

                Attack(enemy);
                return;
            }

            if (!IsRunning) {
                Run();
            }
        }

        public void Attack(Entity enemy)
        {
            stateMachine.Blackboard.TargetEntity = enemy;
            stateMachine.ChangeState(EntityStates.Attack);
        }

        public void Run()
        {
            stateMachine.ChangeState(EntityStates.Run);
        }

        private void OnAttributeHasChanged(AttributeDefinition attribute, float currentValue, float previousValue)
        {
            if (attribute != AttributeDefinition.Health) {
                return;
            }

            if (currentValue <= 0.0f) {
                Die();
            }
            else {
                //Animator.SetTrigger(IsHit);
            }
        }

        private async void Die()
        {
            isAlive = false;

            entityView.PlayDieAnimation();

            entityGatherer.Unregister(this);
            StateMachine.Dispose();
            healthBar.Dispose();

            await Task.Delay(DieMsDelay);

            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (IsPlayer) {
                StateMachine.ChangeState(EntityStates.Idle);
            }
            else {
                StateMachine.Blackboard.TargetEntity = collision.gameObject.GetComponent<Entity>();
                StateMachine.ChangeState(EntityStates.Attack);
            }
        }

        public class Factory : PlaceholderFactory<EntityData, Entity>
        {
        }
    }
}