// -----------------------------------------------------------------------------
// <copyright file="LogHelper.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the LogHelper class.
// </summary>
// <revision revisor="dev01" date="2/26/2010" version="1.1.7.4">
//     File Created
// </revision>
// <revision revisor="dev14" date="4/8/2010" version="1.1.7">
//     File Modified
// </revision>  
// -----------------------------------------------------------------------------

namespace Sequoia.DBAuthService.Data
{
    #region Using Directives
    using System;
    using System.Configuration;
    using System.Diagnostics;
    using Sequoia.EMS.Core.DataServices;
    #endregion

    /// <summary>
    /// Help log events in the Database
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev01" date="2/26/2010" version="1.1.7.4">
    ///     Member Created
    /// </revision>
    public class LogHelper
    {
        #region Fields

        /// <summary>
        /// EMS infraestructure Log Service
        /// </summary>
        private static LogDataService logService = null;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LogHelper"/> class.
        /// </summary>
        /// <param name="authService">
        /// The auth Service.
        /// </param>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="2/26/2010" version="1.1.7.4">
        ///     Member Created
        /// </revision>
        public LogHelper(AuthorizationDataService authService)
        {
            // get database credentials
            var definition = authService.DataServiceDefinition;
            
            // check that we have access to the database
            if (definition.ValidateForServerConnection())
            {
                // create reference to the data service
                logService = new LogDataService(definition);
            }
        }

        #endregion

        #region Public Properties

        #endregion

        #region Public Methods

        /// <summary>
        /// Inits the log.
        /// </summary>
        /// <param name="dataService">The data service.</param>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="3/3/2010" version="1.1.7.10">
        ///     Member Created
        /// </revision>
        public static void InitLog(IAuthorizationDataService dataService)
        {
            // create new reference to data service
            logService = new LogDataService(null);

            // connect data service to database
            logService.CreateDatabase(
                "Initial Catalog=Master;Data Source=.;Integrated Security=SSPI");
        }

        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="2/26/2010" version="1.1.7.4">
        ///     Member Created
        /// </revision>
        public static void Log(string message)
        {
            try
            {
                // attempt to add log event to database
                LogError(message);
            }
            catch (Exception exception)
            {
                // tries to write to db - if it did not work - 
                // try and write to windows event log
                WriteToEventLog(
                    "Error writing to log: " + exception
                    + "\r\nOriginal Message: " + message,
                    EventLogEntryType.Error);
            }
        }

        /// <summary>
        /// Logs the error.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="2/26/2010" version="1.1.7.4">
        ///     Member Created
        /// </revision>
        public static void LogError(string errorMessage)
        {
            // check that log is initialized, data service created
            if (IsLogInitialized())
            {
                // add log event to database
                logService.AddLogMessage(
                    errorMessage,
                    (int)EventSource.DBAuthService,
                    (int)EventSeverity.Error,
                    "CartridgeProcessingQueue");
            }
        }

        #endregion

        #region Public Events

        #endregion

        #region Private Methods

        /// <summary>
        /// Determines whether [is log initialized].
        /// </summary>
        /// <returns>
        /// <c>true</c> if [is log initialized]; otherwise, <c>false</c>.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="2/26/2010" version="1.1.7.4">
        ///     Member Created
        /// </revision>
        private static bool IsLogInitialized()
        {
            return logService != null;
        }

        /// <summary>
        /// Writes to event log.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="entryType">Type of the entry.</param>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="2/26/2010" version="1.1.7.4">
        ///     Member Created
        /// </revision>
        private static void WriteToEventLog(
            string message,
            EventLogEntryType entryType)
        {
            try
            {
                // check that app is set to write to event log
                if (EventLog.Exists("Application")
                    && EventLog.SourceExists("Sequoia DB Auth Service"))
                {
                    // get event logs
                    var logs = EventLog.GetEventLogs();

                    // iterate through event logs
                    foreach (var log in logs)
                    {
                        // write to application log when we come to it
                        if (log.LogDisplayName.Equals(
                            "Application",
                            StringComparison.CurrentCultureIgnoreCase))
                        {
                            // log application name as source
                            log.Source = "Sequoia DB Auth Service";
                            
                            // write the log message to the app log
                            log.WriteEntry(message, entryType);
                        }
                    }
                }
            }
            catch (Exception)
            {
                // read whether to suppress errors
                string suppressAsString =
                    ConfigurationManager.AppSettings["SuppressEventLogFailures"]
                    ?? "false";

                // initialize an out variable
                bool suppressEventLogFailures = false;

                // read config string as a boolean
                bool.TryParse(
                    suppressAsString, out suppressEventLogFailures);

                // check if we should suppress the error
                if (!suppressEventLogFailures)
                {
                    // can't log anywhere at this point - need to at least 
                    // let iis know there is a problem ieven if it kills app
                    throw;
                }
            }
        }

        #endregion
    }
}
