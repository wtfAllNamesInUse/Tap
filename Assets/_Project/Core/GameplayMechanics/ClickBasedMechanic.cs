using System;
using TapTapTap.Blockers;
using Zenject;

namespace TapTapTap.Core
{
    public abstract class ClickBasedMechanic<TGameplayMechanicModel>
        : IGameplayMechanic<TGameplayMechanicModel>, IInitializable, IDisposable
        where TGameplayMechanicModel : BaseGameplayMechanicModel
    {
        public abstract string Id { get; }
        public TGameplayMechanicModel Model { get; protected set; }

        private readonly GameplayMechanicModelsContainer modelsContainer;
        private readonly ClickDetector clickDetector;
        private readonly IBlocker playerInputBlocker;
        private readonly GameStateData gameStateData;

        protected Entity Player => gameStateData.Player;
        
        protected ClickBasedMechanic(
            GameplayMechanicModelsContainer modelsContainer,
            ClickDetector clickDetector,
            IBlocker playerInputBlocker,
            GameStateData gameStateData)
        {
            this.modelsContainer = modelsContainer;
            this.clickDetector = clickDetector;
            this.playerInputBlocker = playerInputBlocker;
            this.gameStateData = gameStateData;
        }

        public void Initialize()
        {
            Model = modelsContainer.GetModel(Id) as TGameplayMechanicModel;
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

            Execute();
        }
        
        public abstract void Execute();
    }
}