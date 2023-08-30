using System;

namespace TapTapTap.DateTimeProvider
{
    public class EditorDateTimeProvider : IDateTimeProvider
    {
        private readonly IDateTimeProvider dateTimeProvider;
        private readonly DateTimeProviderModel dateTimeProviderModel;

        public EditorDateTimeProvider(
            IDateTimeProvider dateTimeProvider)
        {
            this.dateTimeProvider = dateTimeProvider;

            dateTimeProviderModel = DateTimeProviderModel.LoadModel();
        }

        public DateTime CurrentDateTime =>
            dateTimeProvider.CurrentDateTime.AddMinutes(dateTimeProviderModel.minutesOffset);
        public DateTime CurrentUtcDateTime =>
            dateTimeProvider.CurrentUtcDateTime.AddMinutes(dateTimeProviderModel.minutesOffset);
        public float DeltaTime => dateTimeProvider.DeltaTime + dateTimeProviderModel.deltaTime;
    }
}