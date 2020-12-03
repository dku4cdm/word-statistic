using System;
using System.Runtime.Serialization;

namespace WordStatistic.Application.Exceptions
{
    [Serializable]
    public class DataException : Exception
    {
        public DataException()
        {
        }

        public DataException(string message) : base(message)
        {
        }

        public DataException(Guid id) : base(id.ToString())
        {
        }

        public DataException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DataException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
