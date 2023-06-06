using UnityEngine;

namespace TapTapTap.Core
{
    public class Movement : MonoBehaviour
    {
        public bool IsMoving { get; set; }

        private Entity entity;

        public void Init(Entity entity)
        {
            this.entity = entity;
        }

        private void Update()
        {
            if (!IsMoving) {
                return;
            }

            var speed = entity.Attributes.GetAttributeValue(AttributeDefinition.Speed);
            transform.Translate(speed * Time.deltaTime, 0.0f, 0.0f);
        }
    }
}