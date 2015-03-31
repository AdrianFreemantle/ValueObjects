using System;
using System.Runtime.Serialization;

namespace ValueObjects.ContactDetail
{
    [Serializable]
    public class EmailAddressException : Exception
    {
        public EmailAddressException()
        {
        }

        public EmailAddressException(string message)
            : base(message)
        {
        }

        public EmailAddressException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected EmailAddressException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }
}