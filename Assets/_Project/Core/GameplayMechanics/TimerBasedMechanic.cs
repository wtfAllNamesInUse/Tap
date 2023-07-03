using Zenject;

namespace TapTapTap.Core
{
    public class TimerBasedMechanic : IGameplayMechanic, ITickable
    {
        protected readonly ITimer GlobalTimer;
        protected readonly GameStateData GameStateData;
        protected readonly GameplaySettings GameplaySettings;

        protected Entity Player => GameStateData.Player;

        protected TimerBasedMechanic(
            ITimersContainer gameplayTimers,
            GameStateData gameStateData,
            GameplaySettings gameplaySettings)
        {
            GlobalTimer = gameplayTimers.GetTimer(GameplayTimersContainer.GlobalTimer);
            GameStateData = gameStateData;
            GameplaySettings = gameplaySettings;
        }

        public virtual void Tick()
        {
        }

        public virtual void Execute()
        {
        }
    }
}