using System;
using UnityEngine;

namespace TapTapTap.Core
{
    [RequireComponent(typeof(Animator))]
    public class AnimatorEventsEmitter : MonoBehaviour
    {
        public event Action<int> StateEntered;
        public event Action<int> StateExited;
        
        private Animator animator;
        private AnimatorStateMachineBehaviour[] stateBehaviours;

        private void OnEnable()
        {
            if (animator == null) {
                animator = GetComponent<Animator>();
            }

            stateBehaviours = animator.GetBehaviours<AnimatorStateMachineBehaviour>();

            foreach (var behaviour in stateBehaviours) {
                behaviour.AnimatorStateEnter += NotifyStateEntered;
                behaviour.AnimatorStateExit += NotifyStateExited;
            }
        }

        private void OnDisable()
        {
            foreach (var behaviour in stateBehaviours) {
                behaviour.AnimatorStateEnter -= NotifyStateEntered;
                behaviour.AnimatorStateExit -= NotifyStateExited;
            }
        }

        private void NotifyStateEntered(int stateNameHash)
        {
            StateEntered?.Invoke(stateNameHash);
        }

        private void NotifyStateExited(int stateNameHash)
        {
            StateExited?.Invoke(stateNameHash);
        }
    }
}