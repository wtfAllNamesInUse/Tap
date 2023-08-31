using UnityEngine;
using Zenject;

namespace TapTapTap.Ui
{
    public class ScreenController
    {
        private IUiPrefabProvider screenProvider;

        [Inject]
        public void Inject(
            IUiPrefabProvider screenProvider)
        {
            this.screenProvider = screenProvider;
        }

        public Screen ShowScreen<TObject>(Transform parent = null) where TObject : Screen
        {
            if (GetScreen<TObject>(parent, out var screen)) {
                return null;
            }

            var screenComponent = screen.GetComponent<Screen>();
            screenComponent.OnScreenInitialized();

            return screenComponent;
        }

        public Screen ShowScreen<TObject, TData>(TData data, Transform parent = null) where TObject : Screen
        {
            if (GetScreen<TObject>(parent, out var screen)) {
                return null;
            }

            var screenComponent = screen.GetComponent<ScreenWithData<TData>>();
            screenComponent.SetData(data);
            screenComponent.OnScreenInitialized();

            return screenComponent;
        }

        private bool GetScreen<TObject>(Transform parent, out GameObject screen) where TObject : Screen
        {
            screen = screenProvider.GetUiPrefab(typeof(TObject));
            if (screen == null) {
                return true;
            }

            if (parent != null) {
                screen.transform.SetParent(parent);
            }
            return false;
        }
    }
}