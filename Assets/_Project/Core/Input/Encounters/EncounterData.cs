using System.Collections.Generic;

namespace TapTapTap.Core
{
    public class EncounterData
    {
        public IInteractable Encounter { get; set; }
        public IList<InputEventBase> InputEvents { get; set; } = new List<InputEventBase>();
    }
}