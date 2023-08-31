using System.Threading.Tasks;
using TapTapTap.Blockers;

namespace TapTapTap.Core
{
    public class GameplayTutorialsContainer : ITutorialsContainer
    {
        public bool IsAnyTutorialActive { get; private set; }

        private readonly ITutorial[] tutorials;
        private readonly IBlocker playerInputBlocker;
        private readonly ITimer globalTimer;

        public GameplayTutorialsContainer(
            ITutorial[] tutorials,
            IBlocker playerInputBlocker,
            ITimersContainer gameplayTimers)
        {
            this.tutorials = tutorials;
            this.playerInputBlocker = playerInputBlocker;

            globalTimer = gameplayTimers.GetTimer(GameplayTimersContainer.GlobalTimer);
        }

        public async Task TryShowTutorials()
        {
            globalTimer.Stop();
            playerInputBlocker.Block();

            foreach (var tutorial in tutorials) {
                if (!tutorial.ShouldShow()) {
                    continue;
                }

                IsAnyTutorialActive = true;
                
                await tutorial.ShowTutorial();
                await tutorial.HideTutorial();
            }

            IsAnyTutorialActive = false;
            playerInputBlocker.Unblock();
            globalTimer.Start();
        }
    }
}