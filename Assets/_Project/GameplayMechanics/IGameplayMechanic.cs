namespace TapTapTap.GameplayMechanics
{
    public interface IGameplayMechanic<TGameplayMechanicModel>
        where TGameplayMechanicModel : BaseGameplayMechanicModel
    {
        string Id { get; }
        TGameplayMechanicModel Model { get; }
        void Execute();
    }
}