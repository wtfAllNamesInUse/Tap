using UnityEngine.SceneManagement;
using Zenject;

namespace TapTapTap.Core
{
    public class PerkChooserScreen : ScreenWithData<LevelCompletedData>
    {
        private SavedData savedData;

        [Inject]
        public void Inject(
            SavedData savedData)
        {
            this.savedData = savedData;
        }

        public void OnHpPerkClicked()
        {
            savedData.Health25PerksCount++;
            RestartGame();
        }

        public void OnDmgPerkClicked()
        {
            savedData.Damage10PerksCount++;
            RestartGame();
        }

        public void RestartGame()
        {
            SceneManager.LoadScene("Main");
        }
    }
}