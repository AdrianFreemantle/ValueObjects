using System;
using System.Runtime.Serialization;

namespace ValueObjects.ContactDetail
{
    [Serializable]
    public class PhoneNumberUnableToReceiveSmsException : Exception
    {
        public PhoneNumberUnableToReceiveSmsException()
        {
        }

        public PhoneNumberUnableToReceiveSmsException(string phoneNumber, string phoneNumberType)
            : base(FormatErrorMessage(phoneNumber, phoneNumberType))
        {
        }

        public PhoneNumberUnableToReceiveSmsException(string message)
            : base(message)
        {
        }

        public PhoneNumberUnableToReceiveSmsException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected PhoneNumberUnableToReceiveSmsException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }

        private static string FormatErrorMessage(string phoneNumber, string phoneNumberType)
        {
            return String.Format("The phone number {0} is a {1} and can therefore not receive SMS messages", phoneNumber, phoneNumberType);
        }
    }
}