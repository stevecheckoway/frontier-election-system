// -----------------------------------------------------------------------------
// <copyright file="LogDataService.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2010 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the LogDataService class.
// </summary>
// <revision revisor="dev14" date="4/8/2010" version="1.1.7">
//     File Created
// </revision>  
// -----------------------------------------------------------------------------

namespace Sequoia.DBAuthService.Data
{
    #region Using directives

    using System;
    using Sequoia.EMS.Core.DataServices;

    #endregion
    /// <summary>
    /// Event source for message logging
    /// </summary>
    public enum EventSource
    {
        /// <summary>
        /// Event source for message logging
        /// </summary>
        DBAuthService = 3
    }

    /// <summary>
    /// event severity for message logging
    /// </summary>
    public enum EventSeverity
    {
        /// <summary>
        /// error message
        /// </summary>
        Error = 3
    }

    /// <summary>
    ///	    LogDataService provides database access for the logging service.
    ///     This class and the enumerations above were copied out of 
    ///     Sequoia.Infrastructure to remove a circular dependency. The
    ///     enumeration values correspond to those in Sequoia infrastructure
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev14" date="4/8/2010" version="1.1.7">
    ///     Class created.
    /// </revision>
    public class LogDataService : BaseDataService
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LogDataService"/> class.
        /// </summary>
        /// <param name="definition">The definition.</param>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="4/8/2010" version="1.1.7">
        /// Member Created
        /// </revision>
        public LogDataService(DataServiceDefinition definition)
        {
            // set the database credentials
            this.DataServiceDefinition = definition;
        }

        #endregion

        #region Public Properties

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds the log message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="sourceId">The source application.</param>
        /// <param name="severityLevel">The severity level.</param>
        /// <param name="database">The database that has the log table</param>
        /// <returns>[true] if messge logged successfully, else false</returns>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="4/8/2010" version="1.1.7">
        ///     Member Created
        /// </revision>
        public bool AddLogMessage(
           string message,
           int sourceId,
           int severityLevel,
           string database)
        {
            // run the stored proc on the given database
            int result = this.RunNonQueryProcedure(
                string.Format(
                    "[{0}].[dbo].[upInsertLogMessage]",
                    database),
                message,
                sourceId,
                severityLevel);

            // return the success of the transaction
            return result == 0;
        }

        #endregion

        #region Public Events

        #endregion

        #region Fields

        #endregion

        #region Private Methods

        #endregion
    }
}
