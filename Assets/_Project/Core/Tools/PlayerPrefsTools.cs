using UnityEditor;
using UnityEngine;

namespace TapTapTap.Core
{
    public class PlayerPrefsTools
    {
        [MenuItem("Tools/Delete PP")]
        public static void DeleteAll()
        {
            PlayerPrefs.DeleteAll();
        }
        
        [MenuItem("Tools/Delete EP")]
        public static void DeleteAllEditor()
        {
            EditorPrefs.DeleteAll();
        }
    }
}