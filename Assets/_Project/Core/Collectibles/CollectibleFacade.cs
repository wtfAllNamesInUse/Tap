using UnityEngine;
using Zenject;

namespace TapTapTap.Core
{
    public class CollectibleFacade : MonoBehaviour
    {
        private CollectibleData data;

        [Inject]
        public void Inject(
            CollectibleData data)
        {
            this.data = data;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // TODO: collect
        }

        public class Factory : PlaceholderFactory<CollectibleData, CollectibleFacade>
        {
        }
    }
}
