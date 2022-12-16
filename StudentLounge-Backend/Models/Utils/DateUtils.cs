namespace StudentLounge_Backend.Models.Utils
{
    public class DateUtils
    {
        public static string ToUtcString(DateTime date)
        {
            return $"{date.ToUniversalTime:O}";
        }
    }
}
