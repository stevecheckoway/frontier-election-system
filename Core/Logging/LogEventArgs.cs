//-----------------------------------------------------------------------------
// <copyright file="LogEventArgs.cs" 
//  company="Sequoia Voting Systems">
//     Copyright (c) 2008 Sequoia Voting Systems. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
//     File created
// </revision>
//-----------------------------------------------------------------------------

namespace Sequoia.EMS.Core.Logging
{
    #region Using directives

    using System;
    using System.Diagnostics;

    #endregion

    /// <summary>
    ///     LogEventArgs is a container for information that is being sent to
    ///     the logger.
    /// </summary>
    /// <externalUnit cref="LoggingPriority"/>
    /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
    ///     Class created
    /// </revision>
    public sealed class LogEventArgs : EventArgs
    {
        #region Fields

        /// <summary>
        ///     param for log message
        /// </summary>
        private string message = string.Empty;

        /// <summary>
        ///     param for log event id
        /// </summary>
        private int eventId = 0;

        /// <summary>
        ///     param for logging priority
        /// </summary>
        private LoggingPriority priority = LoggingPriority.Information;

        /// <summary>
        ///     param for trace event type
        /// </summary>
        private TraceEventType severity = TraceEventType.Information;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="LogEventArgs"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public LogEventArgs()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="LogEventArgs"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="eventId">The event id.</param>
        /// <param name="priority">The priority.</param>
        /// <param name="severity">The severity.</param>
        /// <externalUnit cref="Message"/>
        /// <externalUnit cref="EventId"/>
        /// <externalUnit cref="Priority"/>
        /// <externalUnit cref="Severity"/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public LogEventArgs(
            string message, 
            int eventId, 
            LoggingPriority priority, 
            TraceEventType severity)
        {
            // set the properties
            this.Message = message;
            this.EventId = eventId;
            this.Priority = priority;
            this.Severity = severity;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        /// <externalUnit cref="message"/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public string Message
        {
            get { return this.message; }
            set { this.message = value; }
        }

        /// <summary>
        ///     Gets or sets the event id.
        /// </summary>
        /// <value>The event id.</value>
        /// <externalUnit cref="eventId"/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public int EventId
        {
            get { return this.eventId; }
            set { this.eventId = value; }
        }

        /// <summary>
        ///     Gets or sets the priority.
        /// </summary>
        /// <value>The priority.</value>
        /// <externalUnit cref="priority"/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public LoggingPriority Priority
        {
            get { return this.priority; }
            set { this.priority = value; }
        }

        /// <summary>
        ///     Gets or sets the severity.
        /// </summary>
        /// <value>The severity.</value>
        /// <externalUnit cref="severity"/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public TraceEventType Severity
        {
            get { return this.severity; }
            set { this.severity = value; }
        }

        #endregion
    }
}
