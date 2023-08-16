using System.Collections.Generic;
using UnityEngine;

namespace TapTapTap.Core
{
    [CreateAssetMenu(fileName = "PerkArchetype", menuName = "ScriptableObjects/PerkArchetype")]
    public class PerkArchetype : ScriptableObject
    {
        public string PerkName => perkName;
        public IReadOnlyList<AttributeInfo> Attributes => attributes;

        [SerializeField]
        private string perkName;

        [SerializeField]
        private List<AttributeInfo> attributes = new();
    }
}