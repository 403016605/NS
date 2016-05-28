using System;

namespace NS.Kernel.Exceptions
{
    [Serializable]
    public class NsException : Exception
    {
        /// <summary>
        ///     Creates a new <see cref="AbpException" /> object.
        /// </summary>
        public NsException()
        {
        }

        /// <summary>
        ///     Creates a new <see cref="AbpException" /> object.
        /// </summary>
        /// <param name="message">Exception message</param>
        public NsException(string message)
            : base(message)
        {
        }

        /// <summary>
        ///     Creates a new <see cref="AbpException" /> object.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner exception</param>
        public NsException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}