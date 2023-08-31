namespace TapTapTap.Core
{
    public class HealthRemovalMechanic : TimerBasedMechanic<HealthRemovalMechanicModel>
    {
        public override string Id => "health_removal";

        private readonly ITimer timer;
        private readonly ITutorialsContainer tutorialsContainer;

        public HealthRemovalMechanic(
            ITimersContainer timersContainer,
            GameStateData gameStateData,
            ITutorialsContainer tutorialsContainer,
            GameplayMechanicModelsContainer modelsContainer)
            : base(timersContainer, gameStateData, modelsContainer)
        {
            this.tutorialsContainer = tutorialsContainer;

            timer = timersContainer.GetTimer(GameplayTimersContainer.HealthRemovalTimer);
        }

        public override void Tick()
        {
            if (!Model.isEnabled) {
                return;
            }

            if (Player == null
                || !Player.IsAlive
                || tutorialsContainer.IsAnyTutorialActive
                || GlobalTimer.ElapsedSeconds <
                Model.Config.DelayHealthRemovalFromLevelStart - Model.Config.HealthRemovalTimerS
                || Player.IsAttacking) {
                timer.Start();
                return;
            }

            Execute();
        }

        public override void Execute()
        {
            var elapsedS = timer.ElapsedSeconds;
            if (elapsedS >= Model.Config.HealthRemovalTimerS) {
                timer.Start();

                var speed = Player.Attributes.GetAttributeValue(AttributeDefinition.Speed);
                if (speed < Model.Config.ExecuteBelowSpeed || speed > Model.Config.ExecuteAboveSpeed) {
                    Player.Attributes.ApplyAttributeModifier(AttributeDefinition.Health,
                        Model.Config.HealthToRemove);
                }
            }
        }
    }
}