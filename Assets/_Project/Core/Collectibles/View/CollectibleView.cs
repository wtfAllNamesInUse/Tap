using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace TapTapTap.Core
{
    public abstract class CollectibleView : MonoBehaviour
    {
        public abstract Task BeginInteraction();
        public abstract Task FinishInteraction();

        public class Factory : PlaceholderFactory<Object, CollectibleView>
        {
        }
    }
}