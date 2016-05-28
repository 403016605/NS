using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NS.Kernel.Logging;

namespace NS.Kernel.Exceptions
{
    /// <summary>
    ///     This exception type is used to throws validation exceptions.
    /// </summary>
    [Serializable]
    public class NsValidationException : NsException, IHasLogSeverity
    {
        /// <summary>
        ///     Constructor.
        /// </summary>
        public NsValidationException()
        {
            ValidationErrors = new List<ValidationResult>();
            Severity = LogSeverity.Warn;
        }


        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="message">Exception message</param>
        public NsValidationException(string message)
            : base(message)
        {
            ValidationErrors = new List<ValidationResult>();
            Severity = LogSeverity.Warn;
        }


        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="validationErrors">Validation errors</param>
        public NsValidationException(string message, List<ValidationResult> validationErrors)
            : base(message)
        {
            ValidationErrors = validationErrors;
            Severity = LogSeverity.Warn;
        }

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner exception</param>
        public NsValidationException(string message, Exception innerException)
            : base(message, innerException)
        {
            ValidationErrors = new List<ValidationResult>();
            Severity = LogSeverity.Warn;
        }

        /// <summary>
        ///     Detailed list of validation errors for this exception.
        /// </summary>
        public List<ValidationResult> ValidationErrors { get; set; }

        /// <summary>
        ///     Exception severity.
        ///     Default: Warn.
        /// </summary>
        public LogSeverity Severity { get; set; }
    }
}