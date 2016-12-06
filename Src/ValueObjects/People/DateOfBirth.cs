using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace ValueObjects.People
{
    [DataContract(Name = "DateOfBirth", Namespace = "People")]
    public struct DateOfBirth : IEquatable<DateOfBirth>
    {
        [DataMember(Name = "birthDate")]
        private readonly DateTime birthDate;

        public static DateOfBirth Empty { get { return new DateOfBirth(); } }

        public DateOfBirth(DateTime dateOfBith)
        {
            Mandate.ParameterNotDefaut(dateOfBith, "dateOfBirth");

            birthDate = dateOfBith.Date;
        }

        public DateOfBirth(int year, int month, int day)
        {
            birthDate = new DateTime(year, month, day);
        }

        public DateOfBirth(string yearValue, string monthValue, string dayValue)
        {

            int year = Convert.ToInt32(yearValue);
            int month = Convert.ToInt32(monthValue);
            int day = Convert.ToInt32(dayValue);

            year = AdjustYearValue(year);

            birthDate = new DateOfBirth(year, month, day);

            Mandate.ParameterNotNullOrEmpty(yearValue, "yearValue");
            Mandate.ParameterNotNullOrEmpty(monthValue, "monthValue");
            Mandate.ParameterNotNullOrEmpty(dayValue, "dayValue");
        }

        public static DateOfBirth Parse(string dateOfBirth)
        {
            return new DateOfBirth(DateTime.Parse(dateOfBirth));
        }

        public static DateOfBirth Parse(string dateOfBirth, IFormatProvider formatProvider)
        {
            return new DateOfBirth(DateTime.Parse(dateOfBirth, formatProvider));
        }

        public static bool TryParse(string s, out DateOfBirth dateOfBirth)
        {
            dateOfBirth = new DateOfBirth();
            DateTime date;

            if(DateTime.TryParse(s, out date))
            {
                dateOfBirth = new DateOfBirth(date);
                return true;
            }

            return false;
        }

        public static bool TryParse(string s, IFormatProvider formatProvider, out DateOfBirth dateOfBirth)
        {
            dateOfBirth = new DateOfBirth();
            DateTime date;

            if (DateTime.TryParse(s, formatProvider, DateTimeStyles.None, out date))
            {
                dateOfBirth = new DateOfBirth(date);
                return true;
            }

            return false;
        }

        private static int AdjustYearValue(int year)
        {
            if (year < 100)
                year += 1900;    

            if (year <= DateTime.Now.Year - 100)
            {
                year += 100;
            }

            return year;
        }

        public PersonAge CurrentAge()
        {
            return AgeAtDate(DateTime.Now);
        } 

        public PersonAge AgeAtDate(DateTime date)
        {
            return PersonAge.AgeAtDate(this, date);
        }

        public override int GetHashCode()
        {
            return birthDate.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            return obj is DateOfBirth && Equals((DateOfBirth)obj);
        }

        public bool Equals(DateOfBirth other)
        {
            return other.birthDate == birthDate;
        }

        public static bool operator ==(DateOfBirth left, DateOfBirth right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(DateOfBirth left, DateOfBirth right)
        {
            return !Equals(left, right);
        }

        public static implicit operator DateTime(DateOfBirth dateOfBirth)
        {
            return dateOfBirth.birthDate;
        }

        public static implicit operator string(DateOfBirth dateOfBirth)
        {
            return dateOfBirth.ToString();
        }

        public override string ToString()
        {
            return birthDate.ToShortDateString();
        }

        public string ToString(string format)
        {
            return birthDate.ToString(format);
        }

        public string ToString(IFormatProvider formatProvider)
        {
            return birthDate.ToString(formatProvider);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return birthDate.ToString(format, formatProvider);
        }
    }
}