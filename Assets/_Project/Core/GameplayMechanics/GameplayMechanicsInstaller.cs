using System;
using TapTapTap.GameplayMechanics;
using UnityEngine;
using Zenject;

namespace TapTapTap.Core
{
    public class GameplayMechanicsInstaller : MonoInstaller<GameplayMechanicsInstaller>
    {
        [SerializeField]
        private GameplayMechanicModelsContainer gameplayMechanicModelsContainer;

        public override void InstallBindings()
        {
            var healthRemovalMechanic = gameplayMechanicModelsContainer.GetModel("health_removal");
            if (healthRemovalMechanic != null && healthRemovalMechanic.isEnabled) {
                Container.Bind(typeof(IGameplayMechanic<HealthRemovalMechanicModel>), typeof(ITickable))
                    .To<HealthRemovalMechanic>().AsSingle();
            }

            var speedRemovalMechanic = gameplayMechanicModelsContainer.GetModel("speed_removal");
            if (speedRemovalMechanic != null && speedRemovalMechanic.isEnabled) {
                Container.Bind(typeof(IGameplayMechanic<SpeedRemovalMechanicModel>), typeof(ITickable))
                    .To<SpeedRemovalMechanic>().AsSingle();
            }

            var speedGainerMechanic = gameplayMechanicModelsContainer.GetModel("speed_gainer");
            if (speedGainerMechanic != null && speedGainerMechanic.isEnabled) {
                Container.Bind(
                        typeof(IGameplayMechanic<SpeedGainerMechanicModel>),
                        typeof(IInitializable),
                        typeof(IDisposable))
                    .To<SpeedGainerMechanic>().AsSingle();
            }
        }
    }
}