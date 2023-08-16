using UnityEngine;
using Zenject;

namespace TapTapTap.Core
{
    public class EntityView : MonoBehaviour
    {
        public Animator Animator => animator;
        public AnimatorCallbacks AnimatorCallbacks => animatorCallbacks;
        public GameObject WeaponRoot => weaponRoot;

        [SerializeField]
        private Animator animator;

        [SerializeField]
        private AnimatorCallbacks animatorCallbacks;

        [SerializeField]
        private GameObject weaponRoot;
        
        private static readonly int IsHit = Animator.StringToHash("IsHit");
        private static readonly int IsDie = Animator.StringToHash("IsDie");

        public void PlayDieAnimation()
        {
            Animator.SetTrigger(IsHit);
            Animator.SetBool(IsDie, true);
        }
        
        public class Factory : PlaceholderFactory<Object, EntityView>
        {
        }
    }
}