using System;
using System.Runtime.Serialization;

namespace ValueObjects.Date
{
    [Serializable]
    public class CalendarMonthException : Exception
    {
        public CalendarMonthException()
        {
        }

        public CalendarMonthException(string message) : base(message)
        {
        }

        public CalendarMonthException(string message, Exception inner) : base(message, inner)
        {
        }

        protected CalendarMonthException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}