using Zenject;

namespace TapTapTap.Core
{
    public class EntityStateMachineCustomFactory : IFactory<EntityStateMachine>
    {
        private readonly IInstantiator instantiator;
        private readonly DiContainer diContainer;

        public EntityStateMachineCustomFactory(
            IInstantiator instantiator,
            DiContainer diContainer)
        {
            this.instantiator = instantiator;
            this.diContainer = diContainer;
        }

        public EntityStateMachine Create()
        {
            var entityStateMachine = instantiator.Instantiate<EntityStateMachine>();

            // Bind to subContainer
            diContainer.BindInstance(entityStateMachine);
            return entityStateMachine;
        }
    }
}