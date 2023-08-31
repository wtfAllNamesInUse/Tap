using Zenject;

namespace TapTapTap.Core
{
    public abstract class TimerBasedMechanic<TGameplayMechanicModel>
        : IGameplayMechanic<TGameplayMechanicModel>, ITickable
        where TGameplayMechanicModel : BaseGameplayMechanicModel
    {
        public abstract string Id { get; }
        public TGameplayMechanicModel Model { get; }

        protected readonly ITimer GlobalTimer;
        protected readonly GameStateData GameStateData;
        protected readonly GameplayMechanicModelsContainer ModelsContainer;

        protected Entity Player => GameStateData.Player;

        protected TimerBasedMechanic(
            ITimersContainer gameplayTimers,
            GameStateData gameStateData,
            GameplayMechanicModelsContainer modelsContainer)
        {
            GlobalTimer = gameplayTimers.GetTimer(GameplayTimersContainer.GlobalTimer);
            GameStateData = gameStateData;
            ModelsContainer = modelsContainer;

            Model = ModelsContainer.GetModel(Id) as TGameplayMechanicModel;
        }

        public abstract void Tick();
        public abstract void Execute();
    }
}