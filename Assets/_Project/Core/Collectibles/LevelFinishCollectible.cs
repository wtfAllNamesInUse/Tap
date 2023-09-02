using System.Threading.Tasks;
using Zenject;

namespace TapTapTap.Core
{
    public class LevelFinishCollectible : Collectible
    {
        private SignalBus signalBus;

        [Inject]
        public void InjectChild(
            SignalBus signalBus)
        {
            this.signalBus = signalBus;
        }

        public override Task ExecuteInteraction(Entity interactingWith, InteractionResolveState interactionResolveState)
        {
            signalBus.Fire(new GameStateChangedSignal {
                NewGameState = GameState.Finish
            });
            
            return Task.CompletedTask;
        }
    }
}