namespace TapTapTap.Core
{
    public class HealthRemovalMechanic : TimerBasedMechanic
    {
        private readonly ITimer timer;
        private readonly ITutorialsContainer tutorialsContainer;

        public HealthRemovalMechanic(
            ITimersContainer timersContainer,
            GameStateData gameStateData,
            GameplaySettings gameplaySettings,
            ITutorialsContainer tutorialsContainer)
            : base(timersContainer, gameStateData, gameplaySettings)
        {
            this.tutorialsContainer = tutorialsContainer;
            
            timer = timersContainer.GetTimer(GameplayTimersContainer.HealthRemovalTimer);
        }

        public override void Tick()
        {
            if (Player == null 
                || !Player.IsAlive
                || tutorialsContainer.IsAnyTutorialActive
                || GlobalTimer.ElapsedSeconds < GameplaySettings.DelayHealthRemovalFromLevelStart - GameplaySettings.HealthRemovalTimerS
                || Player.StateMachine.CurrentState?.StateID == EntityStates.Attack) {
                timer.Start();
                return;
            }

            Execute();
        }

        public override void Execute()
        {
            var elapsedS = timer.ElapsedSeconds;
            if (elapsedS >= GameplaySettings.HealthRemovalTimerS) {
                timer.Start();

                var speed = Player.Attributes.GetAttributeValue(AttributeDefinition.Speed);
                if (speed < GameplaySettings.ExecuteBelowSpeed || speed > GameplaySettings.ExecuteAboveSpeed) {
                    Player.Attributes.ApplyAttributeModifier(AttributeDefinition.Health,
                        GameplaySettings.HealthToRemove);
                }
            }
        }
    }
}