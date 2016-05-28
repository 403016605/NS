﻿using System;
using System.Linq;
using Castle.Core.Logging;
using NS.Kernel.Dependency.Impl;
using NS.Kernel.Exceptions;
using NS.Kernel.Extensions;

namespace NS.Kernel.Logging
{
    /// <summary>
    ///     This class can be used to write logs from somewhere where it's a hard to get a reference to the
    ///     <see cref="ILogger" />.
    ///     Normally, use <see cref="ILogger" /> with property injection wherever it's possible.
    /// </summary>
    public static class LogHelper
    {
        static LogHelper()
        {
            Logger = IocManager.Instance.IsRegistered(typeof (ILoggerFactory))
                ? IocManager.Instance.Resolve<ILoggerFactory>().Create(typeof (LogHelper))
                : NullLogger.Instance;
        }

        /// <summary>
        ///     A reference to the logger.
        /// </summary>
        public static ILogger Logger { get; }

        public static void LogException(Exception ex)
        {
            LogException(Logger, ex);
        }

        public static void LogException(ILogger logger, Exception ex)
        {
            var severity = (ex is IHasLogSeverity)
                ? (ex as IHasLogSeverity).Severity
                : LogSeverity.Error;

            logger.Log(severity, ex.Message, ex);

            LogValidationErrors(logger, ex);
        }

        private static void LogValidationErrors(ILogger logger, Exception exception)
        {
            //Try to find inner validation exception
            if (exception is AggregateException && exception.InnerException != null)
            {
                var aggException = exception as AggregateException;
                if (aggException.InnerException is NsValidationException)
                {
                    exception = aggException.InnerException;
                }
            }

            if (!(exception is NsValidationException))
            {
                return;
            }

            var validationException = exception as NsValidationException;
            if (validationException.ValidationErrors.IsNullOrEmpty())
            {
                return;
            }

            logger.Log(validationException.Severity,
                "There are " + validationException.ValidationErrors.Count + " validation errors:");
            foreach (var validationResult in validationException.ValidationErrors)
            {
                var memberNames = "";
                if (validationResult.MemberNames != null && validationResult.MemberNames.Any())
                {
                    memberNames = " (" + string.Join(", ", validationResult.MemberNames) + ")";
                }

                logger.Log(validationException.Severity, validationResult.ErrorMessage + memberNames);
            }
        }
    }
}