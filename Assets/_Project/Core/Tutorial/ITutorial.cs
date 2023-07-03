using System.Threading.Tasks;

namespace TapTapTap.Core
{
    public interface ITutorial
    {
        public bool ShouldShow();
        public Task ShowTutorial();
        public Task HideTutorial();
    }
}