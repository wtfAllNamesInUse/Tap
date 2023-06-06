using System;
using UnityEngine;

namespace TapTapTap.Core
{
    public class AnimatorCallbacks : MonoBehaviour
    {
        public enum AnimationState
        {
            Finished,
            Attack,
        }

        public event Action<AnimationState> OnAnimationStateHasChanged;

        public void AnimationStateHasChanged(AnimationState animationState)
        {
            OnAnimationStateHasChanged?.Invoke(animationState);
        }
    }
}