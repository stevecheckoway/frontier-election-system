// -----------------------------------------------------------------------------
// <copyright file="MockAuthDataService.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2010 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the MockAuthDataService class.
// </summary>
// <revision revisor="dev14" date="2/24/2010" version="1.1.7.02>
//     File Created
// </revision>  
// -----------------------------------------------------------------------------

using System.Collections.Generic;

namespace Sequoia.DBAuthService.DBAuthServiceTests.Mocks
{
    #region Using directives

    using System;
    using Sequoia.DBAuthService.Data;

    #endregion

    /// <summary>
    ///	    Summary description for MockAuthDataService
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev14" date="2/24/2010" version="1.1.7.02>
    ///     Class created.
    /// </revision>
    public class MockAuthDataService : IAuthorizationDataService 
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="MockAuthDataService"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="2/24/2010 3:14:18 PM" version="1.0.?.0">
        ///     Member Created
        /// </revision>	
        public MockAuthDataService()
        {
        }

        #endregion

        #region Public Properties

        #endregion

        #region Public Methods

        #endregion

        #region Public Events

        #endregion

        #region Fields

        #endregion

        #region Private Methods

        #endregion

        #region Implementation of IAuthorizationDataService
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
        public bool DeleteUser(string name, string user)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        ///     Gets the database user for the last dbuser that the
        ///     auth service created for the given election database in order to
        ///     update that user in that database to create a new authorization
        /// </summary>
        /// <param name="dbname">The dbname.</param>
        /// <returns>The string</returns>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="2/24/2010" version="1.1.7.02">
        ///     Member Created
        /// </revision>
        public string GetDatabaseUser(string dbname)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        ///     Gets all database users created by the auth service
        /// </summary>
        /// <returns>The list of user credentials</returns>
        /// <externalUnit cref="Credential" />
        /// <revision revisor="dev14" date="3/9/2010" version="1.1.7.16">
        ///     Member Created
        /// </revision>
        public List<Credential> GetAllDatabaseUsers()
        {
            throw new System.NotImplementedException();
        }

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
        public bool UpdateAuthDBUser(string dbName, string newLogin, string password)
        {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}
