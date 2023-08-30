using System.Threading.Tasks;
using UnityEngine;

namespace TapTapTap.Core
{
    public class ChestCollectibleView : CollectibleView
    {
        private static readonly int IsOpenedAnimHash = Animator.StringToHash("IsOpened");
        private static readonly int OpenedAnimHash = Animator.StringToHash("Opened");

        [SerializeField]
        private Animator animator;

        [SerializeField]
        private AnimatorEventsEmitter animatorEventsEmitter;

        private TaskCompletionSource<bool> openAnimationTcs;

        public override async Task BeginInteraction()
        {
            openAnimationTcs = new TaskCompletionSource<bool>();
            animator.SetBool(IsOpenedAnimHash, true);

            await openAnimationTcs.Task;
        }

        public override async Task FinishInteraction()
        {
            animator.SetBool(IsOpenedAnimHash, false);
            await Task.Delay(1000);
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
    }
}