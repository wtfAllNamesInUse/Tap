using TapTapTap.Inventory;
using UnityEngine;
using Zenject;

namespace TapTapTap.Core
{
    public class CollectibleFacade : MonoBehaviour, IInteractable
    {
        public bool IsResolvingRequired => data.IsResolvingRequired;

        private CollectibleView view;
        private CollectibleArchetype data;
        private IInventory inventory;

        [Inject]
        public void Inject(
            CollectibleArchetype data,
            CollectibleView.Factory viewFactory,
            IInventory inventory)
        {
            this.data = data;
            this.inventory = inventory;

            view = viewFactory.Create(data.Prefab);
            view.transform.SetParent(transform);
        }

        public async void ExecuteInteraction(Entity interactingWith, InteractionResolveState interactionResolveState)
        {
            await view.BeginInteraction();

            foreach (var attribute in data.Attributes) {
                interactingWith.Attributes.ApplyAttributeModifier(attribute.attribute, attribute.value);
            }

            foreach (var itemModel in data.Items) {
                inventory.Add(itemModel);
            }

            await view.FinishInteraction();
        }

        public class Factory : PlaceholderFactory<CollectibleArchetype, CollectibleFacade>
        {
        }
    }
}