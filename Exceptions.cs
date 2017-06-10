using System;
using System.Runtime.Serialization;

namespace NFSScript
{
    /// <summary/>
    public class DoesNotExistException : Exception
    {
        /// <summary/>
        public DoesNotExistException() : base()
        { }

        /// <summary/>
        public DoesNotExistException(string message) : base(message)
        { }

        /// <summary/>
        public DoesNotExistException(string message, Exception innerException) : base(message, innerException)
        { }

        /// <summary/>
        protected DoesNotExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}
