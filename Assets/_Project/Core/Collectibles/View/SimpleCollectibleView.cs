using System.Threading.Tasks;

namespace TapTapTap.Core
{
    public class SimpleCollectibleView : CollectibleView
    {
        public override Task BeginInteraction()
        {
            return Task.CompletedTask;
        }

        public override Task FinishInteraction()
        {
            return Task.CompletedTask;
        }
    }
}