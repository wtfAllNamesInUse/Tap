using System;
using UnityEngine;

namespace TapTapTap.Core
{
    [Serializable]
    public class GameplayMechanicModelWithConfig<TConfig> : BaseGameplayMechanicModel
    {
        public TConfig Config => config;

        [SerializeField]
        private TConfig config;
    }
}