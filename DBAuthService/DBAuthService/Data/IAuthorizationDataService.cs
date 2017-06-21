// -----------------------------------------------------------------------------
// <copyright file="IAuthorizationDataService.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2010 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the IAuthorzationDataService interface.
// </summary>
// <revision revisor="dev14" date="2/24/2010" version="1.1.7.03>
//     File Created
// </revision>
// -----------------------------------------------------------------------------

namespace Sequoia.DBAuthService.Data
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    ///	    IAuthorizationDataService is an interface used to define a contract 
    ///     between an <see cref="AuthorizationManager"/> and a data service
    /// </summary>
    /// <revision revisor="dev14" date="2/24/2010" version="1.1.7.03>
    ///     Interface created.
    /// </revision>
    public interface IAuthorizationDataService
    {
        #region Properties

        #endregion

        #region Methods

        /// <summary>
        /// Deletes the user login from the given database
        /// </summary>
        /// <param name="name">The name of the election database.</param>
        /// <param name="user">The username to remove.</param>
        /// <returns>The success of the transaction</returns>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="3/9/2010" version="1.1.7.16">
        /// Member Created
        /// </revision>
        bool DeleteUser(string name, string user);

        /// <summary>
        ///     Gets the database user for the last dbuser that the
        ///     auth service created for the given election database in order to
        ///     update that user in that database to create a new authorization
        /// </summary>
        /// <param name="dbname">The dbname.</param>
        /// <returns>The string</returns>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="2/24/2010" version="1.1.7.03">
        ///     Member Created
        /// </revision>
        string GetDatabaseUser(string dbname);

        /// <summary>
        ///     Gets all database users created by the auth service
        /// </summary>
        /// <returns>The list of user credentials</returns>
        /// <externalUnit cref="Credential" />
        /// <revision revisor="dev14" date="3/9/2010" version="1.1.7.16">
        ///     Member Created
        /// </revision>
        List<Credential> GetAllDatabaseUsers();

        /// <summary>
        ///     Creates or Updates the 
        /// </summary>
        /// <param name="dbName">The name of the election database.</param>
        /// <param name="newLogin">The new login.</param>
        /// <param name="password">The password.</param>
        /// <returns>The boolean</returns>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="2/24/2010" version="1.1.7.03">
        ///     Member Created
        /// </revision>
        bool UpdateAuthDBUser(
            string dbName, 
            string newLogin, 
            string password);

        #endregion

        #region Events

        #endregion
    }
}
