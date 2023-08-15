using System;
using Zenject;

namespace TapTapTap.Core
{
    // TODO: we can use some interface here like IEntityAttributeTracker, should it be a part of gameplayMechanic?
    public class EntityAttributeTracker : IInitializable, IDisposable
    {
        private readonly Entity entity;
        private readonly ScreenController screenController;

        public EntityAttributeTracker(
            Entity entity,
            ScreenController screenController)
        {
            this.entity = entity;
            this.screenController = screenController;
        }

        public void Initialize()
        {
            entity.Attributes.OnAttributeHasChanged += OnOnAttributeHasChanged;
        }

        public void Dispose()
        {
            entity.Attributes.OnAttributeHasChanged -= OnOnAttributeHasChanged;
        }

        private void OnOnAttributeHasChanged(AttributeDefinition attribute, float currentValue, float previousValue)
        {
            if (attribute != AttributeDefinition.Health) {
                return;
            }
            
            var value = previousValue - currentValue;
            if (value <= 0) {
                return;
            }

            // TODO: lets use doTween to animate
            screenController.ShowScreen<DamagePopup, DamagePopupData>(
                new DamagePopupData {
                    Damage = previousValue - currentValue
                });
        }
    }
}