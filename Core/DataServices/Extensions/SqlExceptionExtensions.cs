// -----------------------------------------------------------------------------
// <copyright file="SqlExceptionExtensions.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2010 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the SqlExceptionExtensions class.
// </summary>
// <revision revisor="dev22" date="03/05/2010" version="1.1.7.12">
//     File Created
// </revision>
// -----------------------------------------------------------------------------

namespace Sequoia.EMS.Core.DataServices.Extensions
{
    #region Using Directives
    
    using System;
    using System.Data.SqlClient;

    using Sequoia.EMS.Core.Exceptions;

    #endregion

    /// <summary>
    /// Contains methods that extend the <see cref="SqlException"/> type.
    /// </summary>
    /// <revision revisor="dev22" date="03/05/2010" version="1.1.7.12">
    /// Class added.
    /// </revision>
    public static class SqlExceptionExtensions
    {
        /// <summary>
        /// Gets a value that indicates if the exception is due to a login
        /// problem.
        /// </summary>
        /// <param name="exception">The exception to evaluate.</param>
        /// <returns>True if the exception is due to a login problem; false
        /// otherwise.
        /// </returns>
        /// <revision revisor="dev22" date="03/05/2010" version="1.1.7.12">
        /// Member added.
        /// </revision>
        public static Boolean IsLoginError(this SqlException exception)
        {
            const int SqlLoginErrorId = 18456;
            return exception.Number == SqlLoginErrorId;
        }

        /// <summary>
        /// Wraps the exception in a <see cref="DatabaseException"/> using a
        /// default message.
        /// </summary>
        /// <param name="exception">The exception to wrap.</param>
        /// <returns>A new <see cref="DatabaseException"/> containing the
        /// original exception and a default message explaining that a database
        /// error occurred.
        /// </returns>
        /// <revision revisor="dev22" date="03/08/2010" version="1.1.7.15">
        /// Member added.
        /// </revision>
        public static DatabaseException ToDBError(this SqlException exception)
        {
            return ToDBError(
                         exception,
                         Properties.Resources.GeneralExceptionMessage);
        }

        /// <summary>
        /// Wraps the exception in a <see cref="DatabaseException"/>.
        /// </summary>
        /// <param name="exception">The exception to wrap.</param>
        /// <param name="message">The message to use when creating the wrapper
        /// exception.
        /// </param>
        /// <returns>A new <see cref="DatabaseException"/> containing the
        /// original exception and the provided message.
        /// </returns>
        /// <revision revisor="dev22" date="03/08/2010" version="1.1.7.15">
        /// Member added.
        /// </revision>
        public static DatabaseException ToDBError(this SqlException exception,
                                                  String message)
        {
            return new DatabaseException(message, exception);
        }
    }
}
