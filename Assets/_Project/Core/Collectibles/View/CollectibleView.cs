using System;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace TapTapTap.Core
{
    public class CollectibleView : MonoBehaviour
    {
        private static readonly int IsOpenedAnimHash = Animator.StringToHash("IsOpened");
        private static readonly int OpenedAnimHash = Animator.StringToHash("Opened");
        // private static readonly int CloseAnimHash = Animator.StringToHash("Close");
        // private static readonly int ClosedAnimHash = Animator.StringToHash("Closed");

        [SerializeField]
        private Animator animator;

        [SerializeField]
        private AnimatorEventsEmitter animatorEventsEmitter;

        private TaskCompletionSource<bool> openAnimationTcs;

        public async Task PlayOpen()
        {
            openAnimationTcs = new TaskCompletionSource<bool>();
            animator.SetBool(IsOpenedAnimHash, true);

            await openAnimationTcs.Task;
        }

        public void PlayClose()
        {
            animator.SetBool(IsOpenedAnimHash, false);
        }

        private void Awake()
        {
            animatorEventsEmitter.StateEntered += OnStateEntered;
        }

        private void OnDestroy()
        {
            animatorEventsEmitter.StateEntered -= OnStateEntered;
        }

        private void OnStateEntered(int stateHash)
        {
            if (stateHash == OpenedAnimHash) {
                openAnimationTcs.SetResult(true);
            }
        }

        public class Factory : PlaceholderFactory<Object, CollectibleView>
        {
        }
    }
}