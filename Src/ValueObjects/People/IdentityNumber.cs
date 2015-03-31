using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;

namespace ValueObjects.People
{
    [DataContract(Name = "IdentityNumber", Namespace = "People")]
    public struct IdentityNumber : IEquatable<IdentityNumber>
    {
        private const int ValidLength = 13;
        private const int ControlDigitLocation = 12;
        private const int GenderDigitLocation = 6;
        private const int ControlDigitCheckValue = 10;
        private const int ControlDigitCheckExceptionValue = 9;
        private const string RegexIdPattern = "(?<Year>[0-9][0-9])(?<Month>([0][1-9])|([1][0-2]))(?<Day>([0-2][0-9])|([3][0-1]))(?<Gender>[0-9])(?<Series>[0-9]{3})(?<Citizenship>[0-9])(?<Uniform>[0-9])(?<Control>[0-9])";
        private static readonly Regex IdPattern = new Regex(RegexIdPattern); 
        
        [DataMember(Name = "idNumber")]
        private readonly string idNumber;

        [DataMember(Name = "dateOfBirth")]
        private readonly DateOfBirth dateOfBirth;

        [DataMember(Name = "gender")]
        private readonly Gender gender;

        public static IdentityNumber Empty { get { return new IdentityNumber(); } }
        public DateOfBirth DateOfBirth { get { return dateOfBirth; } }
        public Gender Gender { get { return gender; } }
        public string Number { get { return idNumber; } }

        public IdentityNumber(string idNumber)
        {
            if (!IsValid(idNumber))
                throw new IdentityNumberException(String.Format("The Identity Number [{0}] is not valid", idNumber));
            
            this.idNumber = idNumber;
            this.dateOfBirth = GetDateOfBirth(idNumber);
            this.gender = GetGender(idNumber);
        }

        public static IdentityNumber Parse(string s)
        {
            return new IdentityNumber(s);
        }

        public static bool TryParse(string s, out IdentityNumber idNumber)
        {
            idNumber = new IdentityNumber();

            try
            {
                idNumber = new IdentityNumber(s);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        private static DateOfBirth GetDateOfBirth(string idNumber)
        {
            string year = idNumber.Substring(0, 2);
            string month = idNumber.Substring(2, 2);
            string day = idNumber.Substring(4, 2);

            return new DateOfBirth(year, month, day);
        }

        private static Gender GetGender(string idNumber)
        {
            var genderDigit = Convert.ToInt32(idNumber[GenderDigitLocation]);

            return genderDigit >= 5 ? Gender.Male : Gender.Female;
        }

        public static bool IsValid(string identityNumber)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(identityNumber))
                    return false;

                if (identityNumber.Length != ValidLength)
                    return false;

                if (!IdentityNumberPatternIsValid(identityNumber))
                    return false;

                //incidents have been found where the date of birth is not a valid date.
                GetDateOfBirth(identityNumber);

                return ControlDigitIsValid(identityNumber);
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static bool ControlDigitIsValid(string identityNumber)
        {
            var checkDigit = CalculateCheckDigit(identityNumber);

            int controlDigit = Int32.Parse(identityNumber[ControlDigitLocation].ToString(CultureInfo.InvariantCulture));

            if (checkDigit == controlDigit)
                return true;

            if (checkDigit > ControlDigitCheckExceptionValue)
            {
                return controlDigit == 0;
            }

            return false;
        }

        private static int CalculateCheckDigit(string identityNumber)
        {
            int sumOfOddDigits = SumOfOddDigits(identityNumber);
            int sumOfEventDigits = SumOfEvenDigits(identityNumber);

            int sumOfEvenAndOddDigits = sumOfOddDigits + sumOfEventDigits;
            var firstDigitOfSum = (int)Char.GetNumericValue(sumOfEvenAndOddDigits.ToString(CultureInfo.InvariantCulture)[1]);

            return ControlDigitCheckValue - firstDigitOfSum;
        }

        private static int SumOfEvenDigits(string identityNumber)
        {
            int sumOfEventDigits = 0;
            var evenDigits = new StringBuilder();

            for (int i = 1; i < ValidLength - 1; i = i + 2)
            {
                evenDigits.Append(identityNumber[i]);
            }

            int tmp = Int32.Parse(evenDigits.ToString()) * 2;
            string evenResult = tmp.ToString(CultureInfo.InvariantCulture);

            for (int i = 0; i < evenResult.Length; i++)
            {
                sumOfEventDigits = sumOfEventDigits + (int)Char.GetNumericValue(evenResult[i]);
            }

            return sumOfEventDigits;
        }

        private static int SumOfOddDigits(string identityNumber)
        {
            int sumOfOddDigits = 0;

            for (int i = 0; i < ValidLength - 1; i = i + 2)
            {

                sumOfOddDigits += (int)Char.GetNumericValue(identityNumber[i]);
            }

            return sumOfOddDigits;
        }

        private static bool IdentityNumberPatternIsValid(string identityNumber)
        {
            if (IdPattern.IsMatch(identityNumber))
            {
                //00 will slip through the regex and checksum
                return identityNumber.Substring(4, 2) != "00";
            }

            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = idNumber.GetHashCode();
                result = (result * 397) ^ dateOfBirth.GetHashCode();
                result = (result * 397) ^ gender.GetHashCode();
                return result;    
            }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            return obj is IdentityNumber && Equals((IdentityNumber)obj);
        }

        public bool Equals(IdentityNumber other)
        {
            return other.idNumber == idNumber
                && other.dateOfBirth == dateOfBirth
                && other.gender == gender;
        }

        public static bool operator ==(IdentityNumber left, IdentityNumber right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(IdentityNumber left, IdentityNumber right)
        {
            return !Equals(left, right);
        }

        public static implicit operator string(IdentityNumber identityNumber)
        {
            return identityNumber.Number;
        }

        public override string ToString()
        {
            return Number;
        }
    }
}

