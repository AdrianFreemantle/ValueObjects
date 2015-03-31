using System;
using System.Runtime.Serialization;

namespace ValueObjects.People
{
    [Serializable]
    public class PersonAgeException : Exception
    {
        public PersonAgeException()
        {
        }

        public PersonAgeException(string message) : base(message)
        {
        }

        public PersonAgeException(string message, Exception inner) : base(message, inner)
        {
        }

        protected PersonAgeException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}