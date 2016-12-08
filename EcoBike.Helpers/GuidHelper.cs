using System;

namespace Bicimad.Helpers
{
    public class GuidHelper
    {
        public static string GenerateId()
        {
            return Guid.NewGuid().ToString().Replace("-", "").Substring(0, 13);
        }
    }
}