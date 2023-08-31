using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TapTapTap.Core
{
    [CreateAssetMenu(fileName = "GameplayMechanicModelsContainer",
        menuName = "ScriptableObjects/GameplayMechanicModelsContainer")]
    public class GameplayMechanicModelsContainer : ScriptableObject
    {
        [SerializeField]
        private List<BaseGameplayMechanicModel> models;
        
        public BaseGameplayMechanicModel GetModel(string id)
        {
            return models.FirstOrDefault(p => p.id.Equals(id));
        }
    }
}