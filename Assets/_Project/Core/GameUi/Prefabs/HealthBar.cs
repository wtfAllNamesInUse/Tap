using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Screen = TapTapTap.Ui.Screen;

namespace TapTapTap.Core
{
    public class HealthBar : Screen, IDisposable
    {
        [SerializeField]
        private Slider slider;

        private Transform entityTransform;
        private new Camera camera;
        private bool isVisible;

        private Attributes attributes;

        [Inject]
        public void Inject(
            Transform entityTransform,
            Camera camera,
            Attributes attributes)
        {
            this.entityTransform = entityTransform;
            this.camera = camera;
            this.attributes = attributes;

            Initialize();
        }

        private void Initialize()
        {
            attributes.OnAttributeHasChanged += OnAttributeHasChanged;
            SetIsVisible(true);
            UpdatePosition();
        }

        public void SetIsVisible(bool visible)
        {
            gameObject.SetActive(visible);
            isVisible = visible;
        }

        private void OnAttributeHasChanged(AttributeDefinition attribute, float currentValue, float previousValue)
        {
            if (attribute == AttributeDefinition.Health) {
                var attributeInfo = attributes.GetAttribute(AttributeDefinition.Health);
                slider.value = attributeInfo.CurrentValue / attributeInfo.MaxValue;
            }
        }

        private void Update()
        {
            if (!isVisible) {
                return;
            }

            UpdatePosition();
        }

        private void UpdatePosition()
        {
            transform.position = camera.WorldToScreenPoint(entityTransform.position);
        }

        public void Dispose()
        {
            SetIsVisible(false);

            attributes.OnAttributeHasChanged -= OnAttributeHasChanged;
            Destroy(gameObject);
        }

        public class Factory : PlaceholderFactory<HealthBar>
        {
        }
    }
}