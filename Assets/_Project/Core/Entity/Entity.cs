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
        public EntityArchetype Archetype => archetype;

        public bool IsPlayer => Archetype.Fraction == EntityFraction.Player;
        public bool IsAlive => isAlive;

        public bool IsAttacking => StateMachine.CurrentState?.StateID == EntityStates.Attack;
        public bool IsRunning => StateMachine.CurrentState?.StateID == EntityStates.Run;

        private EntityView entityView;

        [SerializeField]
        private Attributes attributes;

        [SerializeField]
        private Movement movement;

        private EntityArchetype archetype;
        private EntityStateMachine stateMachine;
        private EntityGatherer entityGatherer;
        private HealthBar healthBar;
        private IEncounterResolver encounterResolver;

        private bool isAlive = true;

        private const int DieMsDelay = 1500;

        [Inject]
        public void Inject(
            EntityArchetype archetype,
            EntityStateMachine.Factory stateMachineFactory,
            EntityGatherer entityGatherer,
            EntityView.Factory entityViewFactory,
            HealthBar.Factory healthBarFactory,
            IEncounterResolver encounterResolver)
        {
            this.archetype = archetype;
            this.entityGatherer = entityGatherer;
            this.entityGatherer.Register(this);
            this.encounterResolver = encounterResolver;

            // TODO: healthBar factory is needed because we need to bind inside this gameObject subContainer instead of sceneContext or projectContext
            // TODO: is it possible to use screenController and bind to this gameObject subContainer?
            healthBar = healthBarFactory.Create();

            attributes.DoInit(Archetype.Attributes, OnAttributeHasChanged);

            entityView = entityViewFactory.Create(Archetype.Prefab);
            entityView.transform.SetParent(transform);

            stateMachine = stateMachineFactory.Create();
            stateMachine.Initialize();
        }

        public bool TryAttack()
        {
            var myFraction = Archetype.Fraction;
            var enemy = entityGatherer.GetClosestEntityMatchingPredicate(transform,
                p => p.Archetype.Fraction != myFraction, 1.5f);
            if (enemy != null) {
                if (IsAttacking) {
                    return true;
                }

                Attack(enemy);
                return true;
            }

            return false;
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

        public void Interact(IInteractable interactingWith)
        {
            if (!IsPlayer) {
                return;
            }

            if (interactingWith.IsResolvingRequired) {
                StateMachine.ChangeState(EntityStates.Idle);
                encounterResolver.PushEncounter(interactingWith);
            }
            else {
                interactingWith.ExecuteInteraction(this, InteractionResolveState.Unresolved);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var collidedWithInteractable = collision.gameObject.GetComponent<IInteractable>();
            if (collidedWithInteractable != null) {
                Interact(collidedWithInteractable);
                return;
            }

            var collidedWithEntity = collision.gameObject.GetComponent<Entity>();
            if (collidedWithEntity != null) {
                if (IsPlayer) {
                    StateMachine.ChangeState(EntityStates.Idle);
                }
                else {
                    StateMachine.Blackboard.TargetEntity = collidedWithEntity;
                    StateMachine.ChangeState(EntityStates.Attack);
                }
            }
        }

        public class Factory : PlaceholderFactory<EntityArchetype, Entity>
        {
        }
    }
}