using System;
using Unity.Plastic.Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

namespace TapTapTap.DateTimeProvider
{
    [Serializable]
    public class DateTimeProviderModel
    {
        [SerializeField]
        public bool isEnabled;
        [SerializeField]
        public float deltaTime;
        [SerializeField]
        public int minutesOffset;

        public DateTimeProviderModel()
        {
        }

        public DateTimeProviderModel(DateTimeProviderModel dateTimeProviderModel)
        {
            isEnabled = dateTimeProviderModel.isEnabled;
            deltaTime = dateTimeProviderModel.deltaTime;
            minutesOffset = dateTimeProviderModel.minutesOffset;
        }

        public static DateTimeProviderModel LoadModel()
        {
            var text = EditorPrefs.GetString("DateTimeModel");
            return string.IsNullOrEmpty(text)
                ? new DateTimeProviderModel()
                : JsonConvert.DeserializeObject<DateTimeProviderModel>(text);
        }

        public static void SaveModel(DateTimeProviderModel dateTimeProviderModel)
        {
            if (dateTimeProviderModel == null) {
                return;
            }

            var text = JsonConvert.SerializeObject(dateTimeProviderModel);
            EditorPrefs.SetString("DateTimeModel", text);
        }
    }
}