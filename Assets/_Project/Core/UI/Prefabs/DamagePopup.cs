using System;
using System.Globalization;
using System.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Zenject;

namespace TapTapTap.Core
{
    public class DamagePopup : ScreenWithData<DamagePopupData>, IPoolable<IMemoryPool>, IDisposable
    {
        [SerializeField]
        private TextMeshProUGUI text;

        [SerializeField]
        private new Transform transform;

        [SerializeField]
        private CanvasGroup canvasGroup;

        private new Camera camera;
        private IMemoryPool pool;

        [Inject]
        public void Inject(Camera camera)
        {
            this.camera = camera;
        }

        public override async void OnScreenInitialized()
        {
            text.text = CustomData.Damage.ToString(CultureInfo.InvariantCulture);
            transform.position = camera.WorldToScreenPoint(CustomData.WorldSpacePosition);

            await RunAnimation();
        }

        public void OnSpawned(IMemoryPool pool)
        {
            this.pool = pool;
        }

        public void OnDespawned()
        {
            DOTween.Kill(this);
            canvasGroup.alpha = 1.0f;

            pool = null;
        }

        public void Dispose()
        {
            pool.Despawn(this);
        }

        private async Task RunAnimation()
        {
            const float duration = 0.33f;
            const float delay = 0.35f;

            var firstMoveBy = Vector3.up * 350.0f;
            var secondMoveBy = Vector3.up * 1350.0f;
            
            var sequence = DOTween.Sequence()
                .Append(transform.DOBlendableLocalMoveBy(firstMoveBy, duration))
                .AppendInterval(delay)
                .Append(transform.DOBlendableLocalMoveBy(secondMoveBy, duration))
                .Join(canvasGroup.DOFade(0.0f, duration))
                .OnComplete(Dispose);

            await sequence.AsyncWaitForCompletion();
        }

        public class Factory : PlaceholderFactory<DamagePopup>
        {
        }
    }
}