// -----------------------------------------------------------------------------
// <copyright file="AuthorizationManager.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2010 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the AuthorizationManager class.
// </summary>
// <revision revisor="dev14" date="2/5/2010" version="1.1.6.08">
//     File Created
// </revision>
// <revision revisor="dev14" date="2/10/2010" version="1.1.6.13">
//     File Modified
// </revision>
// <revision revisor="dev14" date="2/18/2010" version="1.1.6.21">
//     File Modified
// </revision>
// <revision revisor="dev14" date="2/19/2010" version="1.1.6.22">
//     File Modified
// </revision>
// <revision revisor="dev14" date="2/25/2010" version="1.1.7.05">
//     File Modified
// </revision>
// <revision revisor="dev14" date="3/2/2010" version="1.1.7.09">
//     File Modified
// </revision>
// <revision revisor="dev13" date="3/18/2010" version="1.1.7.26">
//     File Modified
// </revision>
// <revision revisor="dev14" date="4/8/2010" version="1.1.7">
//     File Modified
// </revision>
// -----------------------------------------------------------------------------

namespace Sequoia.DBAuthService
{
    #region Using directives

    using System;
    using System.Collections.Generic;

    using Sequoia.DBAuthService.Data;
    using Sequoia.Utilities.Encryption;
    
    #endregion

    /// <summary>
    ///	    The AuthorizationManager handles business logic for the 
    ///     Authorization Service
    /// </summary> 
    /// <externalUnit/>
    /// <revision revisor="dev14" date="2/5/2010" version="1.1.6.08">
    ///     Class created.
    /// </revision>
    /// <revision revisor="dev14" date="2/19/2010" version="1.1.6.22">
    ///     Added sleep times as class constants
    /// </revision>
    /// <revision revisor="dev14" date="2/25/2010" version="1.1.7.05">
    ///     Changed long sleep time from ten seconds to one
    /// </revision>
    /// <revision revisor="dev14" date="3/9/2010" version="1.1.7.16">
    ///     Made this into a singleton
    /// </revision>
    public class AuthorizationManager
    {
        #region Fields

        /// <summary>
        ///     The number of milliseconds in a long thread sleep time
        /// </summary>
        public const int LONG_SLEEP = 1000;

        /// <summary>
        ///     The number of milliseconds in a short thread sleep time
        /// </summary>
        public const int SHORT_SLEEP = 100;

        /// <summary>
        ///     The number of milliseconds in a very short thread sleep time
        /// </summary>
        public const int VERY_SHORT_SLEEP = 50;

        /// <summary>
        /// The minimum password length
        /// </summary>
        private const int MIN_PASSWORD_LENGTH = 48;

        /// <summary>
        /// The maximum password length
        /// </summary>
        private const int MAX_PASSWORD_LENGTH = 128;

        /// <summary>
        /// The minimum login length
        /// </summary>
        private const int MIN_LOGIN_LENGTH = 16;

        /// <summary>
        /// The maximum login length
        /// </summary>
        private const int MAX_LOGIN_LENGTH = 24;

        /// <summary>
        ///     A singleton instance of the manager
        /// </summary>
        private static AuthorizationManager instance;

        /// <summary>
        /// Configuration reference to save the login info
        /// </summary>
        private static IAuthorizationDataService dataService = null;

        /// <summary>
        ///     The name of the database
        /// </summary>
        private static string databaseName = string.Empty;

        #endregion

        #region Constructors

        /// <summary>
        ///     Prevents a default instance of the <see cref="AuthorizationManager"/> class from being created
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="2/5/2010" version="1.1.6.08">
        /// Member Created
        /// </revision>
        private AuthorizationManager()
        {
            dataService = new AuthorizationDataService();
        }
        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets a singelton instance of the manager.
        /// </summary>
        /// <value>The instance.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="3/9/2010" version="1.1.7.16">
        ///     Member Created
        /// </revision>
        public static AuthorizationManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AuthorizationManager();
                }

                return instance;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Adds the credential to a collection in memory
        /// </summary>
        /// <param name="credential">The credential.</param>
        /// <returns>The success of the transaction</returns>
        /// <externalUnit cref="AuthorizationLock"/>
        /// <externalUnit cref="Credential"/>
        /// <revision revisor="dev14" date="3/1/2010" version="1.1.7.08">
        ///     Member Created
        /// </revision>
        public static bool AddCredential(Credential credential)
        {
            // value showing success. Using variable so that we can change
            // when adding additional error handling
            bool success = false;

            // remove any previous credential that exists for that database
            AuthorizationLock.Credentials.Remove(
                AuthorizationLock.Credentials.Find(
                        cred => cred.DatabaseName == credential.DatabaseName));
            
            // add the new credential
            AuthorizationLock.Credentials.Add(credential);

            // everything successful
            success = true;

            // return success
            return success;
        }

        /// <summary>
        ///     Gets the current credentials for a given database from the 
        ///     collection in memory.
        /// </summary>
        /// <param name="dbname">The election database name.</param>
        /// <returns>The credentials</returns>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="3/1/2010" version="1.1.7.08">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev14" date="3/2/2010" version="1.1.7.09">
        ///     Added call to [GetNewCredential] if none found in memory
        /// </revision>
        public Credential GetCurrentCredential(string dbname)
        {
            // find the credential in memory
            var credential = AuthorizationLock.Credentials.Find(
                cred => cred.DatabaseName == dbname);

            // if the credential was not in memory, add a new one
            if (credential == null)
            {
                credential = this.GetNewCredential(dbname);
            }
            
            // return the credential
            return credential;
        }

        /// <summary>
        ///     Creates and returns a new set of credentials for the database
        ///     user for the given database.
        /// </summary>
        /// <param name="dbname">
        ///     The name of the election database
        /// </param>
        /// <returns>
        ///     The new credentials
        /// </returns>
        /// <externalUnit cref="Credential" />
        /// <externalUnit cref="GetNewLogin" />
        /// <externalUnit cref="GetPassword" />
        /// <revision revisor="dev14" date="2/5/2010" version="1.1.6.08">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev14" date="3/2/2010" version="1.1.7.09">
        ///     Changed return type and renamed from [GetNewAuthorization]
        /// </revision>
        public Credential GetNewCredential(string dbname)
        {
            // generate a new login
            string newUsername = GetNewLogin();

            // generate a new password
            string password = GetPassword();

            // create a new credential object, setting the db username 
            // and password from the random string generation
            var databaseAuth = new Credential(
                dbname, 
                newUsername,
                password);

            try
            {
                // update the database and credentials collection
                if (! this.UpdateDatabaseCredentials(
                    dbname,
                    newUsername,
                    password))
                {
                    // update failed, log error
                    LogHelper.LogError(
                        Properties.Resources.UpdateCredentialsError);
                }
            }
            catch (Exception exc)
            {
                // log the exception
                LogHelper.LogError(exc.Message);
                
                // rethrow exception
                throw;
            }

            return databaseAuth;
        }

        /// <summary>
        ///     Removes the database users/logins that were created by the
        ///     authorization service the next time the service starts up
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="3/9/2010" version="1.1.7.16">
        ///     Member Created
        /// </revision>
        public void ResetAuthUsers()
        {
            // get all the users
            List<Credential> users = dataService.GetAllDatabaseUsers();

            // remove them
            foreach (Credential credential in users)
            {
                dataService.DeleteUser(
                    credential.DatabaseName, credential.CurrentUser);
            }
        }

        /// <summary>
        /// Updates the data base credentials.
        /// </summary>
        /// <param name="dbname">The name of the election database.</param>
        /// <param name="newLogin">The new login.</param>
        /// <param name="password">The password.</param>
        /// <returns>True if the chnage was successful, false otherwise</returns>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="2/5/2010" version="1.1.6.8">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev14" date="2/10/2010" version="1.1.6.13">
        ///     Separated the query being executed from the settings update
        /// </revision>
        /// <revision revisor="dev14" date="3/2/2010" version="1.1.7.09">
        ///     Changed to write credentials to memory as well as datbase
        /// </revision>
        /// <revision revisor="dev14" date="4/8/2010" version="1.1.7">
        ///     Updated logging issues
        /// </revision>
        public bool UpdateDatabaseCredentials(
            string dbname,
            string newLogin,
            string password)
        {
            // initialize the return value to false
            bool returnValue = false;

            try
            {
                // create new credential
                var credential = new Credential(dbname, newLogin, password);
                
                // update the dbUser
                if (AddCredential(credential))
                {
                    returnValue = dataService.UpdateAuthDBUser(
                        dbname, newLogin, password);
                }
            }
            catch (Exception exception)
            {
                // Add message to include database name.
                string message = String.Format(
                    Properties.Resources.UpdateAuthUserError, 
                    dbname);
                
                // throw the exception
                throw new Exception(message, exception);
            }

            // return success of transaction
            return returnValue;
        }

        /// <summary>
        ///     Sets the name of the db.
        /// </summary>
        /// <param name="name">The name of the database.</param>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="3/18/2010" version="1.1.7.26">
        ///     Member Created
        /// </revision>
        public void SetDbName(string name)
        {
            databaseName = name;
        }

        /// <summary>
        ///     Gets the name of the db.
        /// </summary>
        /// <returns>
        ///     The name of the database.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="3/18/2010" version="1.1.7.26">
        ///     Member Created
        /// </revision>
        public string GetDbName()
        {
            return databaseName;
        }

        #endregion

        #region Public Events

        #endregion

        #region Private Methods

        /// <summary>
        ///     Gets a string suitable to be used as a SQL login.
        /// </summary>
        /// <returns>The string to serve as login</returns>
        /// <externalUnit see="RandomStringGenerator" />
        /// <externalUnit see="StringComplexity.Complexity" />
        /// <revision revisor="dev14" date="2/5/2010" version="1.1.6.8">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev14" date="2/18/2010" version="1.1.6.21">
        ///     Modified to use Medium complexity
        /// </revision>
        /// <revision revisor="dev14" date="2/25/2010" version="1.1.7.04">
        ///     Renamed to distinguish it from getting old loging from db
        /// </revision>
        private static string GetNewLogin()
        {
            // get the new login name from the random string generator
            string newLogin = RandomStringGenerator.Current.GenerateString(
                MIN_LOGIN_LENGTH,
                MAX_LOGIN_LENGTH,
                StringComplexity.Complexity.Medium);

            // return the new login name
            return newLogin;
        }

        /// <summary>
        /// Gets an string suitable to be used as a SQL login.
        /// </summary>
        /// <returns>The string to serve as login</returns>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="2/5/2010" version="1.1.6.8">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev14" date="2/18/2010" version="1.1.6.21">
        ///     Modified to use Strong complexity and remove semi-colons
        /// </revision>
        private static string GetPassword()
        {
            // get the new user's password from the random string generator
            string rawPassword = RandomStringGenerator.Current.GenerateString(
                MIN_PASSWORD_LENGTH,
                MAX_PASSWORD_LENGTH,
                StringComplexity.Complexity.Strong);

            // db connection string d/n like semi-colons
            string newPassword = rawPassword.Replace(';', '0');

            return newPassword;
        }

        #endregion
    }
}
