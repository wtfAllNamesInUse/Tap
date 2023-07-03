namespace TapTapTap.Core
{
    public enum LevelCompletedResult
    {
        TimesUp,
        Defeated,
        Won,
    }

    public class LevelCompletedData
    {
        public LevelCompletedResult LevelCompletedResult;
    }
}