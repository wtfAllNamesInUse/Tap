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
    }
}