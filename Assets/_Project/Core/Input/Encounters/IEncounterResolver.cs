namespace TapTapTap.Core
{
    public interface IEncounterResolver
    {
        bool IsResolving { get; }

        void PushEncounter(IInteractable encounter);
        bool TryResolve();
        void ResolveInteraction(IInteractable interactingWith, InteractionResolveState interactionResolveState);
        void RegisterInput(InputEventBase inputEvent);
    }
}