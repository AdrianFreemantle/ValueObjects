using System.Globalization;

namespace ValueObjects
{
    public static class FormatInfo
    {
        public static NumberFormatInfo DefaultNumberFormat { get; set; }

        static FormatInfo()
        {
            DefaultNumberFormat = new NumberFormatInfo
            {
                NumberDecimalSeparator = ".",
                NumberDecimalDigits = 2,
                NumberGroupSeparator = ",",
                NumberNegativePattern = 1,
                CurrencyDecimalDigits = 2,
                CurrencyDecimalSeparator = ".",
                CurrencyGroupSeparator = ",",
                CurrencySymbol = "R",
                CurrencyNegativePattern = 1
            };
        }
    }
}