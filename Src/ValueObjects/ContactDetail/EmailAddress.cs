using System;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;

namespace ValueObjects.ContactDetail
{
    [DataContract(Name = "EmailAddress", Namespace = "ContactDetails")]
    public struct EmailAddress : IEquatable<EmailAddress>
    {
        private const string RegexPattern = @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?";
        private static readonly Regex EmailPattern = new Regex(RegexPattern); 

        [DataMember(Name = "address")]
        private readonly string address;

        public EmailAddress(string address)
        {
            if (String.IsNullOrWhiteSpace(address))
                throw new EmailAddressException("Please provide a non empty value");

            var cleanedAddress = address.ToLower().Replace(" ", "").Replace(",", ".");

            if (!IsValid(cleanedAddress))
                throw new EmailAddressException(String.Format("The email address [{0}] is not valid", address));

            this.address = cleanedAddress;
        }

        public static EmailAddress Parse(string value)
        {
            return new EmailAddress(value);
        }

        public static bool TryParse(string value, out EmailAddress emailAddress)
        {
            emailAddress = new EmailAddress();

            try
            {
                emailAddress = new EmailAddress(value);
                return true;
            }
            catch (EmailAddressException)
            {
                return false;
            }
        }

        public static bool IsValid(string address)
        {
            if (String.IsNullOrWhiteSpace(address))
                return false;

            return EmailPattern.IsMatch(address.ToLower());
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
            return address.address;
        }

        public override string ToString()
        {
            return address ?? String.Empty;
        }
    }
}
