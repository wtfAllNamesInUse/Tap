using UnityEngine;
using Zenject;

namespace TapTapTap.Core
{
    public class ScreenController
    {
        private IUiPrefabProvider screenProvider;
        private Canvas rootCanvas;

        [Inject]
        public void Inject(
            IUiPrefabProvider screenProvider,
            Canvas rootCanvas)
        {
            this.screenProvider = screenProvider;
            this.rootCanvas = rootCanvas;
        }

        public Screen ShowScreen<TObject>() where TObject : Screen
        {
            var screenPrefab = screenProvider.GetUiPrefab(typeof(TObject));
            if (screenPrefab != null) {
                var screen = Object.Instantiate(screenPrefab, rootCanvas.transform);
                var screenComponent = screen.GetComponent<Screen>();
                screenComponent.OnScreenInitialized();

                return screenComponent;
            }

            return null;
        }

        public Screen ShowScreen<TObject, TData>(TData data) where TObject : Screen
        {
            var screenPrefab = screenProvider.GetUiPrefab(typeof(TObject));
            if (screenPrefab != null) {
                var screen = Object.Instantiate(screenPrefab, rootCanvas.transform);
                var screenComponent = screen.GetComponent<ScreenWithData<TData>>();
                screenComponent.SetData(data);
                screenComponent.OnScreenInitialized();

                return screenComponent;
            }

            return null;
        }
    }
}