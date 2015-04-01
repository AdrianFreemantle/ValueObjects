using System;
using System.Runtime.Serialization;

namespace ValueObjects.Date
{
    [DataContract(Name = "CalendarMonth", Namespace = "CalendarMonth")]
    public struct CalendarMonth : IEquatable<CalendarMonth>
    {       
        private const int MinMonth = 1;
        private const int MaxMonth = 12;
        private const int MinYear = 1900;
        private const int MaxYear = 2099;

        private const string IncorrectDateFormat = "Please provide a date in the format mm/yyyy";

        [DataMember(Name = "year")]
        private readonly int year;

        [DataMember(Name = "month")]
        private readonly int month;

        public static CalendarMonth Empty { get { return new CalendarMonth() ;} }

        public CalendarMonth(int year, int month)
        {
            Validate(year, month);

            this.year = year;
            this.month = month;
        }

        public CalendarMonth(DateTime dateTime)
        {
            Validate(dateTime.Year, dateTime.Month);

            month = dateTime.Month;
            year = dateTime.Year;
        }
        
        private CalendarMonth(string calendarMonth)
        {
            var trimedcalendarMonth = ExtractNumericValues(calendarMonth);

            int monthValue = ParseLeadingMonth(trimedcalendarMonth);
            var yearValue = ParseTrailingYear(trimedcalendarMonth);

            try
            {
                Validate(yearValue, monthValue);
            }
            catch (CalendarMonthException)
            {
                monthValue = ParseTrailingMonth(trimedcalendarMonth);
                yearValue = ParseLeadingYear(trimedcalendarMonth);
            }

            month = monthValue;
            year = yearValue;
        }

        public static CalendarMonth Parse(string value)
        {
            return new CalendarMonth(value);
        }

        public static bool TryParse(string value, out CalendarMonth calendarMonth)
        {
            calendarMonth = Empty;
            
            try
            {
                calendarMonth = new CalendarMonth(value);
                return true;
            }
            catch (CalendarMonthException)
            {
                return false;
            }
        }

        private static int ParseTrailingYear(string trimedcalendarMonth)
        {
            string yearString = trimedcalendarMonth.Substring(2, 4);
            return Int32.Parse(yearString);
        }

        private static int ParseLeadingMonth(string trimedcalendarMonth)
        {
            string monthString = trimedcalendarMonth.Substring(0, 2);
            return Int32.Parse(monthString);
        }

        private static int ParseLeadingYear(string trimedcalendarMonth)
        {
            string yearString = trimedcalendarMonth.Substring(0, 4);
            return Int32.Parse(yearString);
        }

        private static int ParseTrailingMonth(string trimedcalendarMonth)
        {
            string monthString = trimedcalendarMonth.Substring(4, 2);
            return Int32.Parse(monthString);
        }

        private static string ExtractNumericValues(string calendarMonth)
        {
            if (String.IsNullOrWhiteSpace(calendarMonth))
                throw new CalendarMonthException(IncorrectDateFormat);

            string trimedcalendarMonth = calendarMonth.GetAllDigits();

            if (trimedcalendarMonth.Length == 5)
                trimedcalendarMonth = String.Format("0{0}", trimedcalendarMonth);

            if (trimedcalendarMonth.Length != 6)
                throw new CalendarMonthException(IncorrectDateFormat);

            return trimedcalendarMonth;
        }

        public static bool IsValid(int month, int year)
        {
            try
            {
                Validate(month, year);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        
        private static void Validate(int year, int month)
        {
            ValidateYear(year);
            ValidateMonth(month);
        }

        private static void ValidateMonth(int month)
        {
            if (month < MinMonth || month > MaxMonth)
            {
                throw new CalendarMonthException(String.Format("The year {0} is outside of the allowed range of {1}-{2}", month, MinMonth, MaxMonth));
            }
        }

        private static void ValidateYear(int year)
        {
            if (year < MinYear || year > MaxYear)
            {
                throw new CalendarMonthException(String.Format("The year {0} is outside of the allowed range of {1}-{2}", year, MinYear, MaxYear));
            }
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (year * 397) ^ month;
            }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            return obj is CalendarMonth && Equals((CalendarMonth)obj);
        }

        public bool Equals(CalendarMonth other)
        {
            return month == other.month && year == other.year;
        }

        public static bool operator ==(CalendarMonth left, CalendarMonth right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(CalendarMonth left, CalendarMonth right)
        {
            return !Equals(left, right);
        }

        public static implicit operator DateTime(CalendarMonth calendarMonth)
        {
            return new DateTime(calendarMonth.year, calendarMonth.month, 1);
        }

        public static implicit operator string(CalendarMonth calendarMonth)
        {
            return calendarMonth.ToString();
        }

        public override string ToString()
        {
            if (year == 0 || month == 0)
            {
                return String.Empty;
            }

            return String.Format("{0:0000}-{1:00}", year, month);
        }

        public string ToString(string format)
        {
            return new DateTime(year, month, 1).ToString(format);
        }

        public string ToString(IFormatProvider formatProvider)
        {
            return new DateTime(year, month, 1).ToString(formatProvider);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return new DateTime(year, month, 1).ToString(format, formatProvider);
        }
    }
}