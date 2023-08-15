using System;
using Zenject;

namespace TapTapTap.Core
{
    public class PerksApplier : IInitializable, IDisposable
    {
        private readonly SavedData savedData;
        private readonly SignalBus signalBus;
        private readonly GameStateData gameStateData;
        private readonly PerksConfig perksConfig;

        public PerksApplier(
            SavedData savedData,
            SignalBus signalBus,
            GameStateData gameStateData,
            PerksConfig perksConfig)
        {
            this.savedData = savedData;
            this.signalBus = signalBus;
            this.gameStateData = gameStateData;
            this.perksConfig = perksConfig;
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
            var allPerks = perksConfig.Perks;
            foreach (var perkArchetype in allPerks) {
                var count = savedData.GetPerkCount(perkArchetype.PerkName);
                Apply(perkArchetype, count);
            }
        }

        public void Apply(PerkArchetype perkArchetype, int count)
        {
            var playerAttributes = gameStateData.Player.Attributes;
            foreach (var attribute in perkArchetype.Attributes) {
                playerAttributes.ApplyAttributeModifier(attribute.attribute, attribute.value * count);
            }
        }
    }
}