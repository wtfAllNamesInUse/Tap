using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace TapTapTap.Core
{
    public class PerkChooserScreen : ScreenWithData<LevelCompletedData>
    {
        private SavedData savedData;
        private IPerkProvider perkProvider;

        private const int PerksCount = 2;

        private IList<PerkArchetype> perks;

        [SerializeField]
        private ConfigurableButton[] buttons;

        [Inject]
        public void Inject(
            SavedData savedData,
            IPerkProvider perkProvider)
        {
            this.savedData = savedData;
            this.perkProvider = perkProvider;
        }

        public override void OnScreenInitialized()
        {
            base.OnScreenInitialized();

            InitializeButtons();
        }

        private void InitializeButtons()
        {
            perks = perkProvider.GetPerks(PerksCount);

            for (var i = 0; i < buttons.Length; i++) {
                var button = buttons[i];
                button.Initialize(perks[i].PerkName, OnPerkButtonClicked);
            }
        }

        private void OnPerkButtonClicked(string perkName)
        {
            savedData.IncrementPerk(perkName);
            RestartGame();
        }

        public void RestartGame()
        {
            SceneManager.LoadScene("Main");
        }
    }
}