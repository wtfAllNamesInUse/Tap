using System.Collections.Generic;
using UnityEngine;

namespace TapTapTap.Core
{
    [CreateAssetMenu(fileName = "BuiltInLevelsContainer", menuName = "ScriptableObjects/BuiltInLevelsContainer")]
    public class BuiltInLevelsContainer : ScriptableObject
    {
        public IReadOnlyList<TextAsset> BuiltInLevels => builtInLevels;

        [SerializeField]
        private List<TextAsset> builtInLevels;
    }
}