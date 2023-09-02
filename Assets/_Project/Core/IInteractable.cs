using System.Threading.Tasks;

namespace TapTapTap.Core
{
    public interface IInteractable
    {
        bool IsResolvingRequired { get; }
        Task ExecuteInteraction(Entity interactingWith, InteractionResolveState interactionResolveState);
    }
}
