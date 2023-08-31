using NaughtyAttributes;
using UnityEngine;

namespace TapTapTap.Core
{
    [CreateAssetMenu(fileName = "GameplaySettings", menuName = "ScriptableObjects/GameplaySettings")]
    public class GameplaySettings : ScriptableObject
    {
        public float LevelTimeS => levelTimeS;
        public float MaxSpeed => maxSpeed;

        [Header("Level")]
        [SerializeField]
        private float levelTimeS;

        [SerializeField]
        [ShowIf("isSpeedIncreaseOnTapEnabled")]
        private float maxSpeed;
    }
}