using System;
using UnityEngine;

namespace TapTapTap.Core
{
    [CreateAssetMenu(fileName = "HealthRemovalMechanicModel",
        menuName = "ScriptableObjects/HealthRemovalMechanicModel")]
    public class HealthRemovalMechanicModel : GameplayMechanicModelWithConfig<HealthRemovalMechanicConfig>
    {
    }

    [Serializable]
    public class HealthRemovalMechanicConfig
    {
        public float HealthRemovalTimerS => healthRemovalTimerS;
        public float ExecuteBelowSpeed => executeBelowSpeed;
        public float ExecuteAboveSpeed => executeAboveSpeed;
        public float HealthToRemove => healthToRemove;
        public float DelayHealthRemovalFromLevelStart => delayHealthRemovalFromLevelStart;
        
        [SerializeField]
        private float healthRemovalTimerS;
        [SerializeField]
        private float executeBelowSpeed;
        [SerializeField]
        private float executeAboveSpeed;
        [SerializeField]
        private float healthToRemove;
        [SerializeField]
        private float delayHealthRemovalFromLevelStart;
    }
}