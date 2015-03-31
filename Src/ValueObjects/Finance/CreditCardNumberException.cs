using System;
using System.Runtime.Serialization;

namespace ValueObjects.Finance
{
    [Serializable]
    public class CreditCardNumberException : Exception
    {
        public CreditCardNumberException()
        {
        }

        public CreditCardNumberException(string message)
            : base(message)
        {
        }

        public CreditCardNumberException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected CreditCardNumberException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }
}