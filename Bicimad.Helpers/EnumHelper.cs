using System;
using System.Collections.Generic;

namespace Bicimad.Helpers
{
    public class EnumHelper
    {
        public static IReadOnlyList<T> GetValues<T>()
        {
            return (T[]) Enum.GetValues(typeof (T));
        }
    }
}