using HtmlAgilityPack;

namespace SE1728_HE173252_A3.Utils
{
    public class CustomConverter
    {
        public static string GetFormatedDateTime(DateTime dateTime)
        {
            return dateTime.ToString("dd/MM/yyyy HH:mm:ss");
        }

        public static string ConvertHtmlToText(string html)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            return doc.DocumentNode.InnerText;
        }
    }
}
