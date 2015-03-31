using System;
using System.Runtime.Serialization;

namespace ValueObjects.People
{
    [Serializable]
    public class IdentityNumberException : Exception
    {
        public IdentityNumberException()
        {
        }

        public IdentityNumberException(string message) : base(message)
        {
        }

        public IdentityNumberException(string message, Exception inner) : base(message, inner)
        {
        }

        protected IdentityNumberException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}