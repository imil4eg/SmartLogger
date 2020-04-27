using System;

namespace SmartLogger
{
    public sealed class EnumComparator
    {
        public static bool CompareNames<TFirstValue, TSecondValue>(TFirstValue firstValue, TSecondValue secondValue)
            where TFirstValue : Enum
            where TSecondValue : Enum
        {
            return string.Equals(Enum.GetName(typeof(TFirstValue), firstValue), Enum.GetName(typeof(TSecondValue), secondValue));
        }
    }
}
