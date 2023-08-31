using System.Threading.Tasks;
using TapTapTap.Blockers;
using TapTapTap.Ui;

namespace TapTapTap.Core
{
    public class FirstLaunchTutorial : BaseTutorial
    {
        private readonly SavedData savedData;
        private readonly IBlocker playerInputBlocker;

        private Screen tutorialScreen;

        public FirstLaunchTutorial(
            ScreenController screenController,
            SavedData savedData,
            IBlocker playerInputBlocker)
            : base(screenController)
        {
            this.savedData = savedData;
            this.playerInputBlocker = playerInputBlocker;
        }

        public override bool ShouldShow()
        {
            return !savedData.DidShowFirstLaunchTutorial;
        }

        public override async Task ShowTutorial()
        {
            tutorialScreen = ScreenController.ShowScreen<SimplePopupScreen, SimplePopupScreenData>(
                new SimplePopupScreenData {
                    Text = "TAP TO MOVE\nTAP TO ATTACK\nTRY TO STAY IN THE SPEED RANGE",
                    AcceptButtonText = "OK",
                    AcceptButtonClicked = OnAcceptClicked,
                });

            await tutorialScreen.CloseTask;
        }

        private void OnAcceptClicked()
        {
            savedData.DidShowFirstLaunchTutorial = true;
            tutorialScreen.Close();

            playerInputBlocker.Unblock();
        }
    }
}