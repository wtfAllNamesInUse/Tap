namespace TapTapTap.Core
{
    public class SpeedRemovalMechanic : TimerBasedMechanic
    {
        private readonly ITimer timer;

        public SpeedRemovalMechanic(
            ITimersContainer timersContainer,
            GameStateData gameStateData,
            GameplaySettings gameplaySettings)
            : base(timersContainer, gameStateData, gameplaySettings)
        {
            timer = timersContainer.GetTimer(GameplayTimersContainer.SpeedRemovalTimer);
        }

        public override void Tick()
        {
            if (Player == null || !Player.Movement.IsMoving) {
                timer.Start();
                return;
            }

            Execute();
        }

        public override void Execute()
        {
            var elapsedS = timer.ElapsedSeconds;
            if (elapsedS < GameplaySettings.SpeedRemovalTimerS) {
                return;
            }

            timer.Start();
            var speed = Player.Attributes.GetAttributeValue(AttributeDefinition.Speed);
            if (speed + GameplaySettings.SpeedRemovalValue >= 0.0f) {
                Player.Attributes.ApplyAttributeModifier(AttributeDefinition.Speed, GameplaySettings.SpeedRemovalValue);
            }
        }
    }
}