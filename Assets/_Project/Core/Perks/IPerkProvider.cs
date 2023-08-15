using System.Collections.Generic;

namespace TapTapTap.Core
{
    public interface IPerkProvider
    {
        IList<PerkArchetype> GetPerks(int perksCount);
    }
}