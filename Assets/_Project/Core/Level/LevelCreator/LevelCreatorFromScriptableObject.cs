using System.IO;
using NaughtyAttributes;
using Unity.Plastic.Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

namespace TapTapTap.Core
{
    [CreateAssetMenu(fileName = "LevelCreatorFromScriptableObject", menuName = "ScriptableObjects/LevelCreatorFromScriptableObject")]
    public class LevelCreatorFromScriptableObject : ScriptableObject, ILevelCreator
    {
        [SerializeField]
        private LevelDescription levelDescription;

        [Button]
        public void SaveLevel()
        {
            var serializeObject = JsonConvert.SerializeObject(levelDescription);
            var savePath = Path.Combine(Application.dataPath, "Levels", "Level_00.json");
            File.WriteAllText(savePath, serializeObject);
            AssetDatabase.Refresh();
        }
    }
}