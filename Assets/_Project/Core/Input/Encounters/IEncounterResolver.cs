namespace TapTapTap.Core
{
    public interface IEncounterResolver
    {
        bool IsResolving { get; }
        void PushEncounter(IInteractable encounter);
        void ProcessInput(InputEventBase inputEvent);
    }
}