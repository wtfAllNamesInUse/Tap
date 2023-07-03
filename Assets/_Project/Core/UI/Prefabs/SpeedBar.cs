using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace TapTapTap.Core
{
    public class SpeedBar : MonoBehaviour, IDisposable
    {
        [SerializeField]
        private Slider slider;

        private Entity entity;

        private GameplaySettings gameplaySettings;

        [Inject]
        public void Inject(GameplaySettings gameplaySettings)
        {
            this.gameplaySettings = gameplaySettings;
        }

        public void InitWithEntity(
            Entity entity)
        {
            this.entity = entity;

            entity.Attributes.OnAttributeHasChanged += OnAttributeHasChanged;
            SetIsVisible(true);
        }

        public void SetIsVisible(bool visible)
        {
            gameObject.SetActive(visible);
        }

        private void OnAttributeHasChanged(AttributeDefinition attribute, float currentValue, float previousValue)
        {
            if (attribute == AttributeDefinition.Speed) {
                var attributeInfo = entity.Attributes.GetAttribute(AttributeDefinition.Speed);
                slider.value = attributeInfo.CurrentValue / gameplaySettings.MaxSpeed;
            }
        }

        public void Dispose()
        {
            SetIsVisible(false);

            entity.Attributes.OnAttributeHasChanged -= OnAttributeHasChanged;
            Destroy(gameObject);
        }
    }
}