using System.Collections.Generic;
using System.Linq;
using TapTapTap.Ui;

namespace TapTapTap.Core
{
    public class EncounterResolver : IEncounterResolver
    {
        public bool IsResolving => activeEncounters.Count > 0;

        private readonly IList<EncounterData> activeEncounters = new List<EncounterData>();

        private readonly ScreenController screenController;
        private readonly GameStateData gameStateData;

        private Screen interactionScreen;

        public EncounterResolver(
            ScreenController screenController,
            GameStateData gameStateData)
        {
            this.screenController = screenController;
            this.gameStateData = gameStateData;
        }

        public void PushEncounter(IInteractable encounter)
        {
            activeEncounters.Add(new EncounterData {
                Encounter = encounter,
                InputEvents = new List<InputEventBase>(),
            });

            interactionScreen = screenController.ShowScreen<InteractionScreen>();
        }

        public void ProcessInput(InputEventBase inputEvent)
        {
            if (!IsResolving) {
                return;
            }

            RegisterInput(inputEvent);
            TryResolve();
        }

        private void TryResolve()
        {
            foreach (var encounterData in activeEncounters) {
                var state = TryInternalResolve(encounterData);
                if (state == InteractionResolveState.Unresolved) {
                    continue;
                }
                
                ResolveInteraction(encounterData.Encounter, state);
                activeEncounters.Remove(encounterData);
                return;
            }
        }

        private void ResolveInteraction(IInteractable interactingWith, InteractionResolveState interactionResolveState)
        {
            if (interactionResolveState != InteractionResolveState.Skip) {
                interactingWith.ExecuteInteraction(gameStateData.Player, interactionResolveState);
            }

            interactionScreen.Close();
        }

        private void RegisterInput(InputEventBase inputEvent)
        {
            activeEncounters[0].InputEvents.Add(inputEvent);
        }

        private InteractionResolveState TryInternalResolve(EncounterData encounterData)
        {
            var inputEvents = encounterData.InputEvents;
            var swipeInputEvent = inputEvents.Where(p => p.EventType == EventType.Swipe).Select(p => (SwipeInputEvent)p)
                .FirstOrDefault();

            return swipeInputEvent?.SwipeDirection switch {
                SwipeDirection.DownToUp => InteractionResolveState.Open,
                SwipeDirection.LeftToRight => InteractionResolveState.Skip,
                _ => InteractionResolveState.Unresolved
            };
        }
    }
}