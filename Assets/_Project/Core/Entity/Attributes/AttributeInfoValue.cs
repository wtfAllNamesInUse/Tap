using UnityEngine;

namespace TapTapTap.Core
{
    public class AttributeInfoValue
    {
        public readonly AttributeDefinition Attribute;

        public float CurrentValue;
        public float MaxValue;

        public AttributeInfoValue(AttributeDefinition attribute, float value)
        {
            Attribute = attribute;
            CurrentValue = MaxValue = value;
        }

        public void Add(float value, AttributeModifierFlag flags = 0)
        {
            var newValueAdd = Helpers.Round_3DP(flags.HasFlag(AttributeModifierFlag.Percent) ? value * MaxValue : value);
            CurrentValue += newValueAdd;

            if (flags.HasFlag(AttributeModifierFlag.ModifyMaxValue)) {
                MaxValue = CurrentValue;
            }
            
            if (flags.HasFlag(AttributeModifierFlag.ClampedZeroMax)) {
                CurrentValue = Mathf.Clamp(CurrentValue, 0.0f, MaxValue);
            }
        }
    }
}