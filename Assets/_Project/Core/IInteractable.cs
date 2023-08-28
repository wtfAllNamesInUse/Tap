namespace TapTapTap.Core
{
    public interface IInteractable
    {
        void ExecuteInteraction(Entity interactingWith, InteractionResolveState interactionResolveState);
    }
}
