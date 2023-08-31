using TapTapTap.Blockers;

namespace TapTapTap.Core
{
    public class SpeedGainerMechanic : ClickBasedMechanic<SpeedGainerMechanicModel>
    {
        public override string Id => "speed_gainer";

        public SpeedGainerMechanic(
            GameplayMechanicModelsContainer modelsContainer,
            ClickDetector clickDetector,
            IBlocker playerInputBlocker,
            GameStateData gameStateData)
            : base(modelsContainer, clickDetector, playerInputBlocker, gameStateData)
        {
        }

        public override void Execute()
        {
            var playerAttributes = Player.Attributes;
            var speed = playerAttributes.GetAttribute(AttributeDefinition.Speed);
            var speedIncrease = Model.Config.SpeedIncreaseOnTap;
            if (speed.CurrentValue + speedIncrease < Model.Config.MaxSpeed) {
                playerAttributes.ApplyAttributeModifier(AttributeDefinition.Speed, speedIncrease);
            }
        }
    }
}