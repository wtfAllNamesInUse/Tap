using System.Threading.Tasks;

namespace TapTapTap.Core
{
    public class BaseTutorial : ITutorial
    {
        protected readonly ScreenController ScreenController;

        public BaseTutorial(ScreenController screenController)
        {
            ScreenController = screenController;
        }

        public virtual bool ShouldShow()
        {
            return false;
        }

        public virtual Task ShowTutorial()
        {
            return Task.CompletedTask;
        }

        public virtual Task HideTutorial()
        {
            return Task.CompletedTask;
        }
    }
}