using System;
using System.Runtime.Serialization;

namespace ValueObjects.ContactDetail
{
    [Serializable]
    public class InvalidPhoneNumberException : Exception
    {
        public InvalidPhoneNumberException()
        {
        }

        public InvalidPhoneNumberException(string message)
            : base(message)
        {
        }

        public InvalidPhoneNumberException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected InvalidPhoneNumberException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }

        internal static InvalidPhoneNumberException InvalidFormat(string rawPhoneNumber)
        {
            return new InvalidPhoneNumberException(String.Format("The phone number [{0}] appears to be invalid", rawPhoneNumber));
        }

        internal static InvalidPhoneNumberException InvalidAreaCode(string rawPhoneNumber)
        {
            return new InvalidPhoneNumberException(String.Format("The area code for the phone number [{0}] is invalid.", rawPhoneNumber));
        }

        internal static InvalidPhoneNumberException InvalidNumberOfDigits(string rawPhoneNumber)
        {
            return new InvalidPhoneNumberException(String.Format("The phone number [{0}] has an incorrect length", rawPhoneNumber));
        }
    }
}