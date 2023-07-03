using System.Threading.Tasks;

namespace TapTapTap.Core
{
    public interface ITutorialsContainer
    {
        bool IsAnyTutorialActive { get; }
        Task TryShowTutorials();
    }
}