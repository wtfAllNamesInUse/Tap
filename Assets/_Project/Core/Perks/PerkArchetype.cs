using System.Collections.Generic;
using UnityEngine;

namespace TapTapTap.Core
{
    [CreateAssetMenu(fileName = "PerkArchetype", menuName = "ScriptableObjects/PerkArchetype")]
    public class PerkArchetype : ScriptableObject
    {
        public string PerkName => perkName;

        [SerializeField]
        private string perkName;

        [SerializeField]
        private List<AttributeInfo> attributes = new();
    }
}