using System;
using UnityEngine;

namespace TapTapTap.Core
{
    [CreateAssetMenu(fileName = "SpeedGainerMechanicModel",
        menuName = "ScriptableObjects/SpeedGainerMechanicModel")]
    public class SpeedGainerMechanicModel : GameplayMechanicModelWithConfig<SpeedGainerMechanicConfig>
    {
    }

    [Serializable]
    public class SpeedGainerMechanicConfig
    {
        public float SpeedIncreaseOnTap => speedIncreaseOnTap;
        public float MaxSpeed => maxSpeed;
        
        [SerializeField]
        private float speedIncreaseOnTap;
        [SerializeField]
        private float maxSpeed;
    }
}
