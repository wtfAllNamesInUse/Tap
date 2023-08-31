namespace TapTapTap.Ui
{
    public class ScreenWithData<TData> : Screen
    {
        protected TData CustomData;

        public void SetData(TData data)
        {
            CustomData = data;
        }
    }
}