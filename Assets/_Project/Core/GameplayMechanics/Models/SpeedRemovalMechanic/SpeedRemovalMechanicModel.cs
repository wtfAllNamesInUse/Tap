using System;
using UnityEngine;

namespace TapTapTap.Core
{
    [CreateAssetMenu(fileName = "SpeedRemovalMechanicModel",
        menuName = "ScriptableObjects/SpeedRemovalMechanicModel")]
    public class SpeedRemovalMechanicModel : GameplayMechanicModelWithConfig<SpeedRemovalMechanicConfig>
    {
    }

    [Serializable]
    public class SpeedRemovalMechanicConfig
    {
        public float SpeedRemovalTimerS => speedRemovalTimerS;
        public float SpeedRemovalValue => speedRemovalValue;
        
        [SerializeField]
        private float speedRemovalTimerS;
        [SerializeField]
        private float speedRemovalValue;
    }
}
