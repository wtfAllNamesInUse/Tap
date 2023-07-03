using UnityEngine;

namespace TapTapTap.Core
{
    public class SavedData
    {
        public bool DidShowFirstLaunchTutorial {
            get => PlayerPrefs.GetInt(DidShowFirstLaunchTutorialKey, 0) > 0;
            set => PlayerPrefs.SetInt(DidShowFirstLaunchTutorialKey, value ? 1 : 0);
        }

        public int Health25PerksCount {
            get => PlayerPrefs.GetInt(Health25, 0);
            set => PlayerPrefs.SetInt(Health25, value);
        }

        public int Damage10PerksCount {
            get => PlayerPrefs.GetInt(Damage10, 0);
            set => PlayerPrefs.SetInt(Damage10, value);
        }

        private const string DidShowFirstLaunchTutorialKey = "did_show_first_launch_tutorial";
        private const string Health25 = "health_25";
        private const string Damage10 = "damage_10";
    }
}