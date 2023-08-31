namespace TapTapTap.Core
{
    public interface IGameplayMechanic<TGameplayMechanicModel>
        where TGameplayMechanicModel : BaseGameplayMechanicModel
    {
        string Id { get; }
        TGameplayMechanicModel Model { get; }
        void Execute();
    }
}