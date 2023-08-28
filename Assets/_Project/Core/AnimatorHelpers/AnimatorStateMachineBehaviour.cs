using System;
using UnityEngine;

namespace TapTapTap.Core
{
    public class AnimatorStateMachineBehaviour : StateMachineBehaviour
    {
        public event Action<int> AnimatorStateEnter;
        public event Action<int> AnimatorStateExit;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);
            AnimatorStateEnter?.Invoke(stateInfo.shortNameHash);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateExit(animator, stateInfo, layerIndex);
            AnimatorStateExit?.Invoke(stateInfo.shortNameHash);
        }
    }
}