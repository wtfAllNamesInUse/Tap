using System.Threading.Tasks;
using TapTapTap.Inventory;
using UnityEngine;
using Zenject;

namespace TapTapTap.Core
{
    public class Collectible : MonoBehaviour, ICollectible
    {
        public bool IsResolvingRequired => data.IsResolvingRequired;    

        private CollectibleArchetype data;
        private IInventory inventory;

        [Inject]
        public void Inject(
            CollectibleArchetype data,
            IInventory inventory)
        {
            this.data = data;
            this.inventory = inventory;
        }

        public virtual Task ExecuteInteraction(Entity interactingWith, InteractionResolveState interactionResolveState)
        {
            foreach (var attribute in data.Attributes) {
                interactingWith.Attributes.ApplyAttributeModifier(attribute.attribute, attribute.value);
            }

            foreach (var itemModel in data.Items) {
                inventory.Add(itemModel);
            }

            return Task.CompletedTask;
        }
    }
}