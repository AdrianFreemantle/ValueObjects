using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;

namespace ValueObjects.ContactDetail
{
    [DataContract(Name = "EmailAddress", Namespace = "ContactDetails")]
    public struct EmailAddress : IEquatable<EmailAddress>
    {
        [DataMember(Name = "address")]
        private readonly string address;

        public EmailAddress(string address)
        {
            if (String.IsNullOrWhiteSpace(address))
                throw new EmailAddressException("Please provide a non empty value");

            if (!IsValid(address))
                throw new EmailAddressException(String.Format("The email address [{0}] is not valid", address));

            this.address = address;
        }

        public static EmailAddress Parse(string value)
        {
            try
            {
                return new EmailAddress(value);
            }
            catch (EmailAddressException)
            {
                string cleandedAddress = TryCleanAddress(value);
                return new EmailAddress(cleandedAddress);
            }
        }

        public static bool TryParse(string value, out EmailAddress emailAddress)
        {
            emailAddress = new EmailAddress();

            try
            {
                emailAddress = Parse(value);
                return true;
            }
            catch (EmailAddressException)
            {
                return false;
            }
        }

        private static string TryCleanAddress(string address)
        {
            var cleanedAddress = address.Trim().Replace(" ", "").Replace(",", ".").Trim(',', '.');

            if (cleanedAddress.EndsWith("gmail", StringComparison.InvariantCultureIgnoreCase))
                cleanedAddress = cleanedAddress + ".com";

            cleanedAddress = cleanedAddress.Replace("@@", "@");

            if (!cleanedAddress.Contains("@"))
            {
                if (cleanedAddress.Contains("2"))
                {
                    cleanedAddress = cleanedAddress.ReplaceLastInstanceOf('2', '@'); //common typing mistake is to miss pressing shift and getting a 2
                }
                else if (cleanedAddress.Count(c => c == '.') >= 2)
                {
                    cleanedAddress.ReplaceFirstInstanceOf('.', '@');
                }
            }

            return cleanedAddress;
        }

        public static bool IsValid(string address)
        {
            if (String.IsNullOrWhiteSpace(address))
                return false;

            var emailValidation = new EmailAddressAttribute();

            return emailValidation.IsValid(address);
        }

        public override int GetHashCode()
        {
            return (address != null ? address.GetHashCode() : 0);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;

            return obj is EmailAddress && Equals((EmailAddress)obj);
        }

        public bool Equals(EmailAddress other)
        {
            return other.address == address;
        }

        public static bool operator ==(EmailAddress left, EmailAddress right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(EmailAddress left, EmailAddress right)
        {
            return !Equals(left, right);
        }

        public static implicit operator string(EmailAddress address)
        {
            return address.ToString();
        }

        public override string ToString()
        {
            return address ?? String.Empty;
        }
    }
}
