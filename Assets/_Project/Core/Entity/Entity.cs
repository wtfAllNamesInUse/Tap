using System.Threading.Tasks;
using TapTapTap.Core.FSM;
using UnityEngine;
using Zenject;

namespace TapTapTap.Core
{
    [SelectionBase]
    public class Entity : MonoBehaviour
    {
        public Animator Animator => entityView.Animator;
        public Attributes Attributes => attributes;
        public Movement Movement => movement;
        public EntityStateMachine StateMachine => stateMachine;
        public EntityData Data => data;

        public bool IsPlayer => Data.EntityArchetype.Fraction == EntityFraction.Player;
        public bool IsAlive => isAlive;

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

        private static readonly int IsHit = Animator.StringToHash("IsHit");
        private static readonly int IsDie = Animator.StringToHash("IsDie");

        private const int DieMsDelay = 1500;

        [Inject]
        public void Inject(
            EntityData data,
            EntityStateMachine.Factory stateMachineFactory,
            EntityGatherer entityGatherer,
            EntityView.Factory entityViewFactory,
            DiContainer container, // TODO: move this to factories
            HealthBar.Factory healthBarFactory)
        {
            this.data = data;
            this.entityGatherer = entityGatherer;
            this.entityGatherer.Register(this);

            healthBar = healthBarFactory.Create();

            attributes.DoInit(this.data.EntityArchetype.Attributes, OnAttributeHasChanged);
            movement.Init(this);

            entityView = entityViewFactory.Create(data.EntityArchetype.Prefab);
            entityView.transform.SetParent(transform);

            stateMachine = stateMachineFactory.Create();
            container.BindInstance(stateMachine);
            stateMachine.Initialize();
        }

        private void OnAttributeHasChanged(AttributeDefinition attribute, float currentValue, float previousValue)
        {
            if (attribute == AttributeDefinition.Health)
            {
                if (currentValue <= 0.0f)
                {
                    Die();
                }
                else
                {
                    //Animator.SetTrigger(IsHit);
                }
            }
        }

        private async void Die()
        {
            isAlive = false;

            Animator.SetTrigger(IsHit);
            Animator.SetBool(IsDie, true);

            entityGatherer.Unregister(this);
            StateMachine.Dispose();
            healthBar.Dispose();

            await Task.Delay(DieMsDelay);

            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (IsPlayer)
            {
                StateMachine.ChangeState(EntityStates.Idle);
            }
            else
            {
                StateMachine.Blackboard.TargetEntity = collision.gameObject.GetComponent<Entity>();
                StateMachine.ChangeState(EntityStates.Attack);
            }
        }

        public class Factory : PlaceholderFactory<EntityData, Entity>
        {
        }
    }
}