using UnityEngine;
using Zenject;

namespace TapTapTap.Core
{
    public class Movement : MonoBehaviour
    {
        public bool IsMoving { get; set; }

        private Attributes attributes;

        [Inject]
        public void Inject(Attributes attributes)
        {
            this.attributes = attributes;
        }

        private void Update()
        {
            if (!IsMoving) {
                return;
            }

            var speed = attributes.GetAttributeValue(AttributeDefinition.Speed);
            transform.Translate(speed * Time.deltaTime, 0.0f, 0.0f);
        }
    }
}