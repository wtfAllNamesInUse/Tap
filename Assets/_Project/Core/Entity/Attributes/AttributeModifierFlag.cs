using System;

namespace TapTapTap.Core
{
    [Flags]
    public enum AttributeModifierFlag
    {
        Percent = 0x1,
        ClampedZeroMax = 0x2,
    }
}