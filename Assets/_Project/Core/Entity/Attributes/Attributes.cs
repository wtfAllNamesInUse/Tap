using System.Collections.Generic;
using UnityEngine;

namespace TapTapTap.Core
{
    public class Attributes : MonoBehaviour
    {
        public delegate void OnAttributeHasChangedDelegate(AttributeDefinition attribute, float currentValue,
            float previousValue);

        public event OnAttributeHasChangedDelegate OnAttributeHasChanged;

        private readonly Dictionary<AttributeDefinition, AttributeInfoValue> attributes = new();

        public void DoInit(IEnumerable<AttributeInfo> attributesList,
            OnAttributeHasChangedDelegate onAttributeHasChanged = null)
        {
            foreach (var info in attributesList) {
                attributes[info.attribute] = new AttributeInfoValue(info.attribute, info.value);
            }

            OnAttributeHasChanged += onAttributeHasChanged;
        }

        public float GetAttributeValue(AttributeDefinition attribute)
        {
            return GetAttribute(attribute).CurrentValue;
        }

        public AttributeInfoValue GetAttribute(AttributeDefinition attribute)
        {
            attributes.TryGetValue(attribute, out AttributeInfoValue result);
            return result;
        }

        public void ApplyAttributeModifier(AttributeDefinition attribute, float value, AttributeModifierFlag flags = 0)
        {
            var attributeInfo = GetAttribute(attribute);
            if (attributeInfo == null) {
                return;
            }

            var previousAttributeValue = attributeInfo.CurrentValue;
            attributeInfo.Add(value, flags);
            OnAttributeHasChanged?.Invoke(attributeInfo.Attribute, attributeInfo.CurrentValue, previousAttributeValue);
        }
    }

    public static class Helpers
    {
        public static float Round_3DP(float value)
        {
            return Mathf.Round(value * 1000.0f) / 1000.0f;
        }
    }
}