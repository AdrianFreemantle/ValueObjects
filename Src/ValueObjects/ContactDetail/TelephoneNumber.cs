using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace ValueObjects.ContactDetail
{   
    [DataContract(Name = "TelephoneNumber", Namespace = "ContactDetail")]
    public struct TelephoneNumber : IEquatable<TelephoneNumber>
    {
        enum PhoneNumberType
        {
            Unknown = 0,
            Cellular = 1,
            Landline = 2,
            Sharecall = 3,
            Maxinet = 4,
            Pagers = 5,
            TollFree = 6,
            ValueAddedServices = 7,
        }

        private const string SouthAfricaInternationalCode = "27";
        private const int InternationalPhoneNumberLength = 11;
        private const int LocalPhoneNumberLength = 10;
        private const int AreaCodeDigitIndex = 1;
        private const int TrunkPrefixDigitIndex = 0; 
        private const string TrunkPrefix = "0";

        private static readonly string[] LandlinePrefix = { "01", "02", "03", "04", "05" };
        private static readonly string[] CellularPrefix = { "06", "07", "081", "082", "083", "084", "085" };
        private static readonly string[] TollFreePrefix = { "080" };
        private static readonly string[] SharecallPrefix = { "086" };
        private static readonly string[] ValueAddedServicesPrefix = { "087" };
        private static readonly string[] PagersPrefix = { "088" };
        private static readonly string[] MaxinetPrefix = { "089" };

        [DataMember(Name = "TelephoneNumber")]
        private readonly string phoneNumber;

        [DataMember(Name = "phoneNumberType")]
        private readonly PhoneNumberType phoneNumberType;

        public static TelephoneNumber Empty { get { return new TelephoneNumber(); } }

        public static TelephoneNumber FromString(string phoneNumber)
        {
            return new TelephoneNumber(phoneNumber);
        }

        public TelephoneNumber(string rawPhoneNumber)
        {
            if (String.IsNullOrWhiteSpace(rawPhoneNumber))
            {
                phoneNumber = String.Empty;
                phoneNumberType = PhoneNumberType.Unknown;
                return;
            }

            string phoneNumberDigits = GetPhoneNumberDigits(rawPhoneNumber);

            ValidatePhoneNumber(rawPhoneNumber, phoneNumberDigits);

            phoneNumber = phoneNumberDigits;
            phoneNumberType = GetPhoneNumberType(phoneNumberDigits);
        }

        public static TelephoneNumber Parse(string value)
        {
            return new TelephoneNumber(value);
        }

        public static bool TryParse(string value, out TelephoneNumber telephoneNumber)
        {
            telephoneNumber = Empty;

            try
            {
                telephoneNumber = new TelephoneNumber(value);
                return true;
            }
            catch (InvalidPhoneNumberException)
            {
                return false;
            }
        }

        public static void ValidateNumberCanRecieveSms(string phoneNumber)
        {
            Mandate.ParameterNotNullOrEmpty(phoneNumber, "TelephoneNumber");

            var telephoneNumber = FromString(phoneNumber);

            if (telephoneNumber.CanRecieveSms())
                return;

            throw new PhoneNumberUnableToReceiveSmsException(telephoneNumber, telephoneNumber.phoneNumberType.GetDescription());
        }

        private static string GetPhoneNumberDigits(string rawPhoneNumber)
        {
            var splitPhoneNumber = rawPhoneNumber
                .ToLower()
                .Split(new[] { "x", "X", "ex", "ext", "/", "\\", ".", " ", "-" }, StringSplitOptions.RemoveEmptyEntries);

            string phoneNumberDigits = String.Empty;

            foreach (var s in splitPhoneNumber)
            {
                phoneNumberDigits = phoneNumberDigits + s.ToAlphanumeric().GetAllDigits();

                if(phoneNumberDigits.Length >= InternationalPhoneNumberLength || phoneNumberDigits.Length >= LocalPhoneNumberLength)
                    break;
            }

            return IsInternationFormattedPhoneNumber(phoneNumberDigits)
                ? ToLocalFormat(phoneNumberDigits)
                : phoneNumberDigits;
        }

        private static void ValidatePhoneNumber(string rawPhoneNumber, string phoneNumberDigits)
        {
            ValidateFormat(rawPhoneNumber, phoneNumberDigits);
            ValidateAreaCode(rawPhoneNumber, phoneNumberDigits);
            ValidateNumberOfDigits(rawPhoneNumber, phoneNumberDigits);
        }

        private static void ValidateFormat(string rawPhoneNumber, string phoneNumberDigits)
        {
            if (!IsLocalFormattedPhoneNumber(phoneNumberDigits))
                throw InvalidPhoneNumberException.InvalidFormat(rawPhoneNumber);
        }

        private static void ValidateAreaCode(string rawPhoneNumber, string phoneNumberDigits)
        {
            if (phoneNumberDigits[AreaCodeDigitIndex] == '0' || phoneNumberDigits[TrunkPrefixDigitIndex] != '0')
                throw InvalidPhoneNumberException.InvalidAreaCode(rawPhoneNumber);
        }

        private static void ValidateNumberOfDigits(string rawPhoneNumber, string phoneNumberDigits)
        {
            if (phoneNumberDigits.Length != InternationalPhoneNumberLength && phoneNumberDigits.Length != LocalPhoneNumberLength)
                throw InvalidPhoneNumberException.InvalidNumberOfDigits(rawPhoneNumber);
        }

        private static string ToLocalFormat(string phoneNumberDigits)
        {
            return String.Format("{0}{1}", TrunkPrefix, phoneNumberDigits.Remove(0, 2));
        }

        private static bool IsInternationFormattedPhoneNumber(string phoneNumberDigits)
        {
            return phoneNumberDigits.Length == InternationalPhoneNumberLength && phoneNumberDigits.StartsWith(SouthAfricaInternationalCode);
        }

        private static bool IsLocalFormattedPhoneNumber(string phoneNumberDigits)
        {
            return phoneNumberDigits.Length == LocalPhoneNumberLength && phoneNumberDigits.StartsWith(TrunkPrefix);
        }

        private static PhoneNumberType GetPhoneNumberType(string phoneNumber)
        {
            if (LandlinePrefix.Any(phoneNumber.StartsWith))
                return PhoneNumberType.Landline;

            if (CellularPrefix.Any(phoneNumber.StartsWith))
                return PhoneNumberType.Cellular;

            if (TollFreePrefix.Any(phoneNumber.StartsWith))
                return PhoneNumberType.TollFree;

            if (SharecallPrefix.Any(phoneNumber.StartsWith))
                return PhoneNumberType.Sharecall;

            if (ValueAddedServicesPrefix.Any(phoneNumber.StartsWith))
                return PhoneNumberType.ValueAddedServices;

            if (PagersPrefix.Any(phoneNumber.StartsWith))
                return PhoneNumberType.Pagers;

            if (MaxinetPrefix.Any(phoneNumber.StartsWith))
                return PhoneNumberType.Maxinet;

            return PhoneNumberType.Unknown;
        }

        public bool CanRecieveSms()
        {
            return phoneNumberType == PhoneNumberType.Cellular;
        }

        public static bool CanReceiveSms(string phoneNumber)
        {
            var number = FromString(phoneNumber);
            return number.CanRecieveSms();
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = phoneNumber.GetHashCode();
                return (result * 397) ^ phoneNumberType.GetHashCode();
            }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            return obj is TelephoneNumber && Equals((TelephoneNumber)obj);
        }

        public bool Equals(TelephoneNumber other)
        {
            return other.phoneNumber == phoneNumber && other.phoneNumberType == phoneNumberType;
        }

        public static bool operator ==(TelephoneNumber left, TelephoneNumber right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(TelephoneNumber left, TelephoneNumber right)
        {
            return !Equals(left, right);
        }

        public static implicit operator string(TelephoneNumber telephoneNumber)
        {
            return telephoneNumber.ToString();
        }

        public override string ToString()
        {
            return phoneNumber ?? String.Empty;
        }

        public string ToMaskedString(int index, int length, char mask)
        {
            var characters = ToString().ToCharArray();

            int end = index + length;

            for (int i = index; i < characters.Length && i < end; i++)
            {
                characters[i] = mask;
            }

            return new String(characters);
        }
    }
}