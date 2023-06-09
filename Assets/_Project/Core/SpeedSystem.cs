using System;
using Zenject;

namespace TapTapTap.Core
{
    public class SpeedSystem : IInitializable, IDisposable
    {
        private readonly ClickDetector clickDetector;
        private readonly GameStateData gameStateData;
        private readonly IBlocker playerInputBlocker;
        private readonly GameplaySettings gameplaySettings;

        private Entity Player => gameStateData.Player;

        public SpeedSystem(
            ClickDetector clickDetector,
            GameStateData gameStateData,
            IBlocker playerInputBlocker,
            GameplaySettings gameplaySettings)
        {
            this.clickDetector = clickDetector;
            this.gameStateData = gameStateData;
            this.playerInputBlocker = playerInputBlocker;
            this.gameplaySettings = gameplaySettings;
        }

        public void Initialize()
        {
            clickDetector.OnClick += OnClick;
        }

        public void Dispose()
        {
            clickDetector.OnClick -= OnClick;
        }

        private void OnClick()
        {
            if (playerInputBlocker.IsBlocked || !Player.Movement.IsMoving) {
                return;
            }

            var speed = Player.Attributes.GetAttribute(AttributeDefinition.Speed);
            var speedIncrease = gameplaySettings.SpeedIncreaseOnTap;
            if (speed.CurrentValue + speedIncrease < gameplaySettings.MaxSpeed) {
                Player.Attributes.ApplyAttributeModifier(AttributeDefinition.Speed, speedIncrease);
            }
        }
    }
}