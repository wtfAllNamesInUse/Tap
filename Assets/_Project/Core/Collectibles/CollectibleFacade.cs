using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace TapTapTap.Core
{
    public class CollectibleFacade : MonoBehaviour, IInteractable
    {
        private CollectibleView view;
        private CollectibleArchetype data;

        [Inject]
        public void Inject(
            CollectibleArchetype data,
            CollectibleView.Factory viewFactory)
        {
            this.data = data;

            view = viewFactory.Create(data.Prefab);
            view.transform.SetParent(transform);
        }

        public async void ExecuteInteraction(Entity interactingWith, InteractionResolveState interactionResolveState)
        {
            await view.PlayOpen();

            foreach (var attribute in data.Attributes) {
                interactingWith.Attributes.ApplyAttributeModifier(attribute.attribute, attribute.value);
            }

            await Task.Delay(1000);
            view.PlayClose();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var entity = collision.gameObject.GetComponent<Entity>();
            if (!entity.IsPlayer) {
                return;
            }

            foreach (var attribute in data.Attributes) {
                entity.Attributes.ApplyAttributeModifier(attribute.attribute, attribute.value);
            }
        }

        public class Factory : PlaceholderFactory<CollectibleArchetype, CollectibleFacade>
        {
        }
    }
}