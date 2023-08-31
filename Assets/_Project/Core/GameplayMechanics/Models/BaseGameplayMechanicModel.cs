using System;
using UnityEngine;

namespace TapTapTap.Core
{
    [Serializable]
    public class BaseGameplayMechanicModel : ScriptableObject //, IGameplayMechanicModel
    {
        public string id;
        public bool isEnabled;
    }
}