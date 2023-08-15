using UnityEngine;

namespace TapTapTap.Core
{
    // TODO: Convert to json and save whole file
    public class SavedData
    {
        public void IncrementPerk(string perkName)
        {
            var key = GetPerkKey(perkName);
            var count = PlayerPrefs.GetInt(key, 0);
            PlayerPrefs.SetInt(key, count + 1);
        }
        
        public int GetPerkCount(string perkName)
        {
            var key = GetPerkKey(perkName);
            return PlayerPrefs.GetInt(key, 0);
        }

        public bool DidShowFirstLaunchTutorial {
            get => PlayerPrefs.GetInt(DidShowFirstLaunchTutorialKey, 0) > 0;
            set => PlayerPrefs.SetInt(DidShowFirstLaunchTutorialKey, value ? 1 : 0);
        }

        private const string DidShowFirstLaunchTutorialKey = "did_show_first_launch_tutorial";
        private const string ActivePerkPrefix = "active_perk_";
        
        private string GetPerkKey(string perkName) => $"{ActivePerkPrefix}_{perkName}";
    }
}