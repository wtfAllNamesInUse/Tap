using System;
using Zenject;

namespace TapTapTap.Core
{
    public class PerksApplier : IInitializable, IDisposable
    {
        private readonly SavedData savedData;
        private readonly SignalBus signalBus;
        private readonly GameStateData gameStateData;

        public PerksApplier(
            SavedData savedData,
            SignalBus signalBus,
            GameStateData gameStateData)
        {
            this.savedData = savedData;
            this.signalBus = signalBus;
            this.gameStateData = gameStateData;
        }

        public void Initialize()
        {
            signalBus.Subscribe<GameStateChangedSignal>(OnGameStateChanged);
        }

        public void Dispose()
        {
            signalBus.Unsubscribe<GameStateChangedSignal>(OnGameStateChanged);
        }

        private void OnGameStateChanged(GameStateChangedSignal signal)
        {
            var playerAttributes = gameStateData.Player.Attributes;
            for (var i = 0; i < savedData.Damage10PerksCount; i++) {
                playerAttributes.ApplyAttributeModifier(AttributeDefinition.Damage, 10.0f);
            }

            for (var i = 0; i < savedData.Health25PerksCount; i++) {
                playerAttributes.ApplyAttributeModifier(AttributeDefinition.Health, 25.0f);
            }
        }
    }
}