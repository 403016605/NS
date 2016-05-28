using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NS.Kernel.Exceptions
{
    /// <summary>
    /// This exception type is used to throws validation exceptions.
    /// </summary>
    [Serializable]
    public class NsValidationException : NsException, Logging.IHasLogSeverity
    {
        /// <summary>
        /// Detailed list of validation errors for this exception.
        /// </summary>
        public List<ValidationResult> ValidationErrors { get; set; }

        /// <summary>
        /// Exception severity.
        /// Default: Warn.
        /// </summary>
        public Logging.LogSeverity Severity { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public NsValidationException()
        {
            this.ValidationErrors = new List<ValidationResult>();
            this.Severity = Logging.LogSeverity.Warn;
        }


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">Exception message</param>
        public NsValidationException(string message)
            : base(message)
        {
            this.ValidationErrors = new List<ValidationResult>();
            this.Severity = Logging.LogSeverity.Warn;
        }


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="validationErrors">Validation errors</param>
        public NsValidationException(string message, List<ValidationResult> validationErrors)
            : base(message)
        {
            this.ValidationErrors = validationErrors;
            this.Severity = Logging.LogSeverity.Warn;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner exception</param>
        public NsValidationException(string message, Exception innerException)
            : base(message, innerException)
        {
            this.ValidationErrors = new List<ValidationResult>();
            this.Severity = Logging.LogSeverity.Warn;
        }
    }
}
