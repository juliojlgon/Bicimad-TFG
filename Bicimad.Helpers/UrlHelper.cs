using System;
using System.Globalization;
using System.Text;

namespace Bicimad.Helpers
{
    public static class UrlHelper
    {
        public static string Sanitize(this string value)
        {
            var sb = (new StringBuilder(RemoveDiacritics(value.ToLower())))
                .Replace(' ', '-')
                .Replace('/', '-')
                .Replace('%', '-')
                .Replace('&', '-')
                .Replace('#', '-')
                .Replace('¿', '-')
                .Replace('?', '-')
                .Replace(',', '-')
                .Replace(';', '-')
                .Replace('!', '-')
                .Replace('¡', '-')
                .Replace('.', '-')
                .Replace('[', '-')
                .Replace(']', '-')
                .Replace('ñ', 'n')
                .Replace('ç', 'c')
                .Replace(':', '-')
                .Replace('+', '-')
                .Replace('<', '-')
                .Replace('>', '-')
                .Replace('*', '-')
                .Replace('/', '-')
                .Replace('\\', '-');

            var sanitized = sb.ToString();
            var repeated = sanitized.IndexOf("--", StringComparison.Ordinal);

            while (repeated >= 0)
            {
                sanitized = sanitized.Replace("--", "-");
                repeated = sanitized.IndexOf("--", StringComparison.Ordinal);
            }

            return sanitized.Trim('-');
        }

        private static string RemoveDiacritics(this string strThis)
        {
            if (strThis == null) return null;

            var sb = new StringBuilder();
            foreach (var c in strThis.Normalize(NormalizationForm.FormD))
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }
    }
}