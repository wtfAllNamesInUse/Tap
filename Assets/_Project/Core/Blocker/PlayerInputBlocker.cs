namespace TapTapTap.Core
{
    public class PlayerInputBlocker : Blocker
    {
        public override string Reason => "player_input_blocked";

        public override bool IsBlocked => Player == null || base.IsBlocked;

        private readonly GameStateData gameStateData;

        private Entity Player => gameStateData.Player;

        public PlayerInputBlocker(GameStateData gameStateData)
        {
            this.gameStateData = gameStateData;
        }
    }
}