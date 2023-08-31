using TapTapTap.Ui;
using Zenject;

namespace TapTapTap.Core
{
    public class WelcomeScreen : Screen
    {
        private SignalBus signalBus;

        [Inject]
        public void Inject(SignalBus signalBus)
        {
            this.signalBus = signalBus;
        }

        public void OnPlayButtonClicked()
        {
            signalBus.Fire(new GameStateChangedSignal() { NewGameState = GameState.NewGame });
            Close();
        }
    }
}