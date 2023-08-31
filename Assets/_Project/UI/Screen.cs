using System.Threading.Tasks;
using UnityEngine;

namespace TapTapTap.Ui
{
    public class Screen : MonoBehaviour
    {
        public Task CloseTask => closeTcs.Task;

        private readonly TaskCompletionSource<bool> closeTcs = new TaskCompletionSource<bool>();

        public void Close()
        {
            Destroy(gameObject);
            closeTcs.SetResult(true);
        }

        public virtual void OnScreenInitialized()
        {
        }

        public virtual void OnDestroy()
        {
        }
    }
}