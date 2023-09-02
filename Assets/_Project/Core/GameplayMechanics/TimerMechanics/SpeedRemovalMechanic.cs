namespace TapTapTap.Core
{
    public class SpeedRemovalMechanic : TimerBasedMechanic<SpeedRemovalMechanicModel>
    {
        public override string Id => "speed_removal";
        
        private readonly ITimer timer;

        public SpeedRemovalMechanic(
            ITimersContainer timersContainer,
            GameStateData gameStateData,
            GameplayMechanicModelsContainer modelsContainer)
            : base(timersContainer, gameStateData, modelsContainer)
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
            if (elapsedS < Model.Config.SpeedRemovalTimerS) {
                return;
            }

            timer.Start();
            var speed = Player.Attributes.GetAttributeValue(AttributeDefinition.Speed);
            if (speed + Model.Config.SpeedRemovalValue >= 0.0f) {
                Player.Attributes.ApplyAttributeModifier(AttributeDefinition.Speed, Model.Config.SpeedRemovalValue);
            }
        }
    }
}