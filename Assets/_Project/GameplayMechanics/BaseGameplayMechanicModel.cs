using System;
using UnityEngine;

namespace TapTapTap.GameplayMechanics
{
    [Serializable]
    public class BaseGameplayMechanicModel : ScriptableObject
    {
        public string id;
        public bool isEnabled;
    }
}