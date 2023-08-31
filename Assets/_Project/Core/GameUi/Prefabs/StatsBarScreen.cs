using System;
using TapTapTap.ConfigurableTickables;
using TapTapTap.Inventory;
using TapTapTap.Inventory.Views;
using TMPro;
using UnityEngine;
using Zenject;
using Screen = TapTapTap.Ui.Screen;

namespace TapTapTap.Core
{
    public class StatsBarScreen : Screen
    {
        [SerializeField]
        private TextMeshProUGUI timeLeftS;
        [SerializeField]
        private TextMeshProUGUI distance;
        [SerializeField]
        private SpeedBar speedBar;
        [SerializeField]
        private CoinsViewCounter coinsViewCounter;

        private ITimer globalTimer;
        private DistanceEvaluator distanceEvaluator;
        private GameplaySettings gameplaySettings;
        private IInventory inventory;
        private EverySecondTickable everySecondTickable;

        [Inject]
        public void Inject(
            GameplayTimersContainer gameplayTimersContainer,
            DistanceEvaluator distanceEvaluator,
            GameplaySettings gameplaySettings,
            GameStateData gameStateData,
            IInventory inventory,
            EverySecondTickable everySecondTickable)
        {
            this.distanceEvaluator = distanceEvaluator;
            this.gameplaySettings = gameplaySettings;
            this.inventory = inventory;
            this.everySecondTickable = everySecondTickable;

            globalTimer = gameplayTimersContainer.GetTimer(GameplayTimersContainer.GlobalTimer);
            speedBar.InitWithEntity(gameStateData.Player);

            inventory.ItemModified += OnItemModified;
            everySecondTickable.Tick += OnTick;
        }

        private void OnItemModified(InventoryItemModel _)
        {
            coinsViewCounter.Refresh();
        }

        private void OnTick()
        {
            if (!globalTimer.IsRunning) {
                return;
            }

            timeLeftS.text =
                (TimeSpan.FromSeconds(gameplaySettings.LevelTimeS) - globalTimer.ElapsedTime).ToString(@"ss\:ff");
            distance.text = distanceEvaluator.Distance.ToString("0000");
        }

        public override void OnDestroy()
        {
            inventory.ItemModified -= OnItemModified;
            everySecondTickable.Tick -= OnTick;
        }
    }
}