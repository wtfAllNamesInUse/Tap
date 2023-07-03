using UnityEngine;

namespace TapTapTap.Core
{
    [CreateAssetMenu(fileName = "GameplaySettings", menuName = "ScriptableObjects/GameplaySettings")]
    public class GameplaySettings : ScriptableObject
    {
        public float LevelTimeS => levelTimeS;
        public float HealthRemovalTimerS => healthRemovalTimerS;
        public float ExecuteBelowSpeed => executeBelowSpeed;
        public float ExecuteAboveSpeed => executeAboveSpeed;
        public float HealthToRemove => healthToRemove;
        public float DelayHealthRemovalFromLevelStart => delayHealthRemovalFromLevelStart;

        public float SpeedIncreaseOnTap => speedIncreaseOnTap;
        public float MaxSpeed => maxSpeed;
        public float SpeedRemovalTimerS => speedRemovalTimerS;
        public float SpeedRemovalValue => speedRemovalValue;
        
        [Header("Level")]
        [SerializeField]
        private float levelTimeS;

        [Header("Health Removal")]
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

        [Header("Speed Increase On Tap")]
        [SerializeField]
        private float speedIncreaseOnTap;
        [SerializeField]
        private float maxSpeed;
        
        [Header("Speed Removal")]
        [SerializeField]
        private float speedRemovalTimerS;
        [SerializeField]
        private float speedRemovalValue;
    }
}