using System.Threading.Tasks;
using TapTapTap.Core.FSM;
using UnityEngine;
using Zenject;

namespace TapTapTap.Core
{
    [SelectionBase]
    public class Entity : MonoBehaviour, IOwner
    {
        public Entity Owner => this;
        public Animator Animator => animator;
        public AnimatorCallbacks AnimatorCallbacks => animatorCallbacks;
        public GameObject WeaponRoot => weaponRoot;
        public Attributes Attributes => attributes;
        public Movement Movement => movement;
        public EntityStateMachine StateMachine => stateMachine;
        public EntityData Data => data;

        public bool IsPlayer => Data.EntityArchetype.Fraction == EntityFraction.Player;
        public bool IsAlive => isAlive;

        [SerializeField]
        private Animator animator;

        [SerializeField]
        private AnimatorCallbacks animatorCallbacks;

        [SerializeField]
        private GameObject weaponRoot;

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
            HealthBar healthBar)
        {
            this.data = data;
            stateMachine = stateMachineFactory.Create(this);
            attributes.DoInit(this.data.EntityArchetype.Attributes, OnAttributeHasChanged);
            this.entityGatherer = entityGatherer;
            this.entityGatherer.Register(this);
            this.healthBar = healthBar;
            movement?.Init(this);

            Initialize();
        }

        private void Initialize()
        {
            SetDirection(data.Direction);
        }

        private void OnAttributeHasChanged(AttributeDefinition attribute, float currentValue, float previousValue)
        {
            if (attribute == AttributeDefinition.Health) {
                if (currentValue <= 0.0f) {
                    Die();
                }
                else {
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

        public void SetDirection(EntityDirection direction)
        {
            var currentDirection = transform.localScale;
            currentDirection.x *= (direction == EntityDirection.Right) ? -1 : 1;
            transform.localScale = currentDirection;

            data.Direction = direction;
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

        public class Factory : PlaceholderFactory<Object, EntityData, Entity>
        {
        }
    }
}