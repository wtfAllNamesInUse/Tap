namespace TapTapTap.Core
{
    public class PlayerInputHandler : InputHandler
    {
        private readonly Entity player;
        private readonly IBlocker playerInputBlocker;

        public PlayerInputHandler(
            ClickDetector clickDetector,
            SwipeDetector swipeDetector,
            IEncounterResolver encounterResolver,
            IInputResolver inputResolver,
            Entity player,
            IBlocker playerInputBlocker)
            : base(clickDetector, swipeDetector, encounterResolver, inputResolver)
        {
            this.player = player;
            this.playerInputBlocker = playerInputBlocker;
        }

        protected override void OnInput(InputEventBase inputEvent)
        {
            if (playerInputBlocker.IsBlocked || !player.IsAlive || inputEvent == null) {
                return;
            }

            if (EncounterResolver.IsResolving) {
                EncounterResolver.RegisterInput(inputEvent);
                EncounterResolver.TryResolve();
                return;
            }

            HandleClick(inputEvent);
        }

        private void HandleClick(InputEventBase inputEvent)
        {
            if (inputEvent.EventType != EventType.Click) {
                return;
            }

            var didAttack = player.TryAttack();
            if (didAttack) {
                return;
            }

            if (!player.IsRunning) {
                player.Run();
            }
        }
    }
}