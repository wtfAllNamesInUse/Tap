using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace TapTapTap.Core
{
    public class CollectibleFacade : MonoBehaviour, IInteractable
    {
        public bool IsResolvingRequired => collectible.IsResolvingRequired;

        private CollectibleView view;
        private ICollectible collectible;

        [Inject]
        public void Inject(
            CollectibleArchetype data,
            CollectibleView.Factory viewFactory)
        {
            view = viewFactory.Create(data.Prefab);
            collectible = view.GetComponent<ICollectible>();
            view.transform.SetParent(transform);
        }

        public async Task ExecuteInteraction(Entity interactingWith, InteractionResolveState interactionResolveState)
        {
            await view.BeginInteraction();
            await collectible.ExecuteInteraction(interactingWith, interactionResolveState);
            await view.FinishInteraction();
        }

        public class Factory : PlaceholderFactory<CollectibleArchetype, CollectibleFacade>
        {
        }
    }
}