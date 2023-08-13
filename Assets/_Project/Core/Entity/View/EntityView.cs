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

        public class Factory : PlaceholderFactory<Object, EntityView>
        {
        }
    }
}