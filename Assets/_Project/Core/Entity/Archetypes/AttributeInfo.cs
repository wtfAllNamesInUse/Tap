using System;

namespace TapTapTap.Core
{
    [Serializable]
    public class AttributeInfo
    {
        public AttributeDefinition attribute;
        public float value;

        public AttributeInfo DeepCopy()
        {
            var info = new AttributeInfo {
                attribute = attribute,
                value = value
            };

            return info;
        }
    }
}