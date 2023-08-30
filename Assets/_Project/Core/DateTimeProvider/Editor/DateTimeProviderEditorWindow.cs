using UnityEngine;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using Zenject;

namespace TapTapTap.DateTimeProvider.Editor
{
    public class DateTimeProviderEditorWindow : ZenjectEditorWindow
    {
        private DateTimeProviderModel dateTimeProviderModel;

        [MenuItem("Window/DateTimeProviderEditorWindow")]
        public static void GetOrCreateWindow()
        {
            var window = GetWindow<DateTimeProviderEditorWindow>();
            window.titleContent = new GUIContent("DateTimeProviderEditorWindow");
        }

        public override void OnEnable()
        {
            base.OnEnable();
            dateTimeProviderModel = DateTimeProviderModel.LoadModel();
        }
        
        public override void OnDisable()
        {
            base.OnDisable();
            DateTimeProviderModel.SaveModel(dateTimeProviderModel);
        }

        public override void InstallBindings()
        {
        }

        public void CreateGUI()
        {
            var root = rootVisualElement;
            
            var isEnabled = new Toggle {
                label = "IsEnabled",
                value = dateTimeProviderModel.isEnabled,
            };
            isEnabled.RegisterValueChangedCallback(OnIsEnabledChanged);

            var deltaTime = new FloatField {
                label = "Delta Time: ",
                value = dateTimeProviderModel.deltaTime,
            };
            deltaTime.RegisterValueChangedCallback(OnDeltaTimeChanged);
            
            var minutesOffset = new IntegerField {
                label = "Minutes Offset: ",
                value = dateTimeProviderModel.minutesOffset,
            };
            minutesOffset.RegisterValueChangedCallback(OnMinutesOffsetChanged);
            
            root.Add(isEnabled);
            root.Add(deltaTime);
            root.Add(minutesOffset);
        }

        private void OnMinutesOffsetChanged(ChangeEvent<int> evt)
        {
            dateTimeProviderModel.minutesOffset = evt.newValue;
        }

        private void OnDeltaTimeChanged(ChangeEvent<float> evt)
        {
            dateTimeProviderModel.deltaTime = evt.newValue;
        }

        private void OnIsEnabledChanged(ChangeEvent<bool> evt)
        {
            dateTimeProviderModel.isEnabled = evt.newValue;
        }
    }
}