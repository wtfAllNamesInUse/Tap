using System;
using TapTapTap.GameplayMechanics;
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