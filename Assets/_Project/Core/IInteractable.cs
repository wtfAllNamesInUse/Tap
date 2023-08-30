namespace TapTapTap.Core
{
    public interface IInteractable
    {
        bool IsResolvingRequired { get; }
        void ExecuteInteraction(Entity interactingWith, InteractionResolveState interactionResolveState);
    }
}
