namespace SE1728_HE173252_A3.Utils
{
    public class CustomConverter
    {
        public static string GetFormatedDateTime(DateTime dateTime)
        {
            return dateTime.ToString("dd/MM/yyyy HH:mm:ss");
        }
    }
}
