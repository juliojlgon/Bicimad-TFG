using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Bicimad.Helpers
{
    public static class StringHelper
    {
        public static string ToUtf8(this string text)
        {
            if (string.IsNullOrEmpty(text)) return text;

            var textByte = Encoding.Default.GetBytes(text);
            return Encoding.UTF8.GetString(textByte);
        }

        public static string NoHtml(this string text, bool decode = true)
        {
            if (string.IsNullOrEmpty(text)) return text;

            var reg = new Regex("<[^>]+>", RegexOptions.IgnoreCase);
            var stripped = reg.Replace(text, string.Empty);
            return decode ? HttpUtility.HtmlDecode(stripped) : stripped;
        }
    }
}