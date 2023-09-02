using Zenject;

namespace TapTapTap.Core
{
    public class GameplayTimersContainer : TimersContainer
    {
        public const string GlobalTimer = "global_timer";
        public const string HealthRemovalTimer = "health_removal_timer";
        public const string HealthAdderTimer = "health_adder_timer";
        public const string SpeedRemovalTimer = "speed_removal_timer";

        private readonly SignalBus signalBus;

        public GameplayTimersContainer(
            Timer.Factory timerFactory,
            SignalBus signalBus) : base(timerFactory)
        {
            this.signalBus = signalBus;

            AddTimer(HealthRemovalTimer);
            AddTimer(HealthAdderTimer);
            AddTimer(SpeedRemovalTimer);
            AddTimer(GlobalTimer);

            signalBus.Subscribe<GameStateChangedSignal>(OnGameStateChanged);
        }

        public override void Dispose()
        {
            base.Dispose();

            signalBus.Unsubscribe<GameStateChangedSignal>(OnGameStateChanged);
        }

        private void OnGameStateChanged(GameStateChangedSignal signal)
        {
            if (signal.NewGameState != GameState.NewGame) {
                return;
            }

            GetTimer(GlobalTimer).Start();
        }
    }
}