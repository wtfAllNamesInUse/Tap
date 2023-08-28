using System;
using Zenject;

namespace TapTapTap.Core
{
    public abstract class InputHandler : IInitializable, IDisposable, ILateTickable
    {
        private readonly ClickDetector clickDetector;
        private readonly SwipeDetector swipeDetector;

        protected readonly IEncounterResolver EncounterResolver;
        protected readonly IInputResolver InputResolver;

        protected InputHandler(
            ClickDetector clickDetector,
            SwipeDetector swipeDetector,
            IEncounterResolver encounterResolver,
            IInputResolver inputResolver)
        {
            this.clickDetector = clickDetector;
            this.swipeDetector = swipeDetector;

            EncounterResolver = encounterResolver;
            InputResolver = inputResolver;
        }

        public void Initialize()
        {
            clickDetector.OnClick += OnClick;
            swipeDetector.OnSwipe += OnSwipe;
        }

        public void Dispose()
        {
            clickDetector.OnClick -= OnClick;
            swipeDetector.OnSwipe -= OnSwipe;
        }

        public void LateTick()
        {
            var result = InputResolver.TryResolve();
            if (result != null) {
                OnInput(result);
            }
        }

        private void OnClick()
        {
            InputResolver.RegisterClick();
        }

        private void OnSwipe(SwipeDirection swipeDirection)
        {
            InputResolver.RegisterSwipe(swipeDirection);
        }

        protected abstract void OnInput(InputEventBase inputEvent);
    }
}