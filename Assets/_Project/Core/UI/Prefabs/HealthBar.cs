using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace TapTapTap.Core
{
    public class HealthBar : MonoBehaviour, IDisposable
    {
        [SerializeField]
        private Slider slider;

        private Entity entity;
        private new Camera camera;
        private bool isVisible;

        [Inject]
        public void Inject(
            Entity entity,
            Camera camera)
        {
            this.entity = entity;
            this.camera = camera;

            Initialize();
        }

        private void Initialize()
        {
            entity.Attributes.OnAttributeHasChanged += OnAttributeHasChanged;
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
                var attributeInfo = entity.Attributes.GetAttribute(AttributeDefinition.Health);
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
            transform.position = camera.WorldToScreenPoint(entity.transform.position);
        }

        public void Dispose()
        {
            SetIsVisible(false);

            entity.Attributes.OnAttributeHasChanged -= OnAttributeHasChanged;
            Destroy(gameObject);
        }

        public class Factory : PlaceholderFactory<HealthBar>
        {
        }
    }
}