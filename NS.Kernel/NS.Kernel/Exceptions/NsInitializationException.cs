using System;
using System.Linq;

namespace NS.Kernel.Exceptions
{
    [Serializable]
    public class NsInitializationException : NsException
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public NsInitializationException()
        {

        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">Exception message</param>
        public NsInitializationException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner exception</param>
        public NsInitializationException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }


}
