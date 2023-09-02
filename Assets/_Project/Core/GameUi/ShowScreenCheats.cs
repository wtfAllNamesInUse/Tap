using TapTapTap.Inventory.Backpack.Views;
using TapTapTap.Ui;

namespace TapTapTap.Core
{
    public class ShowScreenCheats
    {
        private readonly ScreenController screenController;

        public ShowScreenCheats(
            ScreenController screenController)
        {
            this.screenController = screenController;
        }

        public void ShowBackpackScreen()
        {
            screenController.ShowScreen<BackpackScreen>();
        }
    }
}