// -----------------------------------------------------------------------------
// <copyright file="AuthorizationDataService.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2010 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the AuthorizationDataService class.
// </summary>
// <revision revisor="dev14" date="2/24/2010" version="1.1.7.03">
//     File Created
// </revision>
// <revision revisor="dev14" date="4/08/2010" version="1.1.7">
//     File Modified
// </revision>    
// -----------------------------------------------------------------------------

namespace Sequoia.DBAuthService.Data
{
    #region Using directives

    using System;
    using System.Collections.Generic;
    using System.Data.Common;
    using Sequoia.EMS.Core.DataServices;
    using Sequoia.EMS.Core.Exceptions;

    #endregion

    /// <summary>
    ///	    Provides data access for the authorization service
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev14" date="2/24/2010" version="1.1.7.03">
    ///     Class created.
    /// </revision>
    public class AuthorizationDataService : BaseDataService, IAuthorizationDataService
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="AuthorizationDataService"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="2/24/2010" version="1.1.7.03">
        ///     Member Created
        /// </revision>	
        public AuthorizationDataService()
        {
            this.CreateDatabase(
                "Initial Catalog=Master;Data Source=.;Integrated Security=SSPI");
        }

        #endregion

        #region Public Properties

        #endregion

        #region Public Methods
        /// <summary>
        /// Deletes the user login from the given database
        /// </summary>
        /// <param name="dbname">The name of the database.</param>
        /// <param name="username">The usernaem to remove.</param>
        /// <returns>The boolean</returns>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="3/9/2010" version="1.1.7.16">
        /// Member Created
        /// </revision>
        public bool DeleteUser(string dbname, string username)
        {
            int result = this.RunNonQueryProcedure(
                "[CartridgeProcessingQueue].[dbo].[upElectionUserDelete]",
                username,
                dbname);

            return result == 0;
        }

        /// <summary>
        ///     Gets the database user for the last dbuser that the
        ///     auth service created for the given election database in order to
        ///     update that user in that database to create a new authorization
        /// </summary>
        /// <param name="dbname">The dbname.</param>
        /// <returns>The string</returns>
        /// <exception cref="DatabaseException">
        ///     Throws database exception if the target database is not set
        ///     or if the stored procedure cannot be found.
        /// </exception>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="2/24/2010" version="1.1.7.03">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev22" date="03/08/2010" version="1.1.7.15">
        /// Changed since method <see cref="BaseDataService.GetReader"/> no
        /// longer takes a boolean parameter.
        /// </revision>
        public string GetDatabaseUser(string dbname)
        {
            if (this.db == null)
            {
                throw new DatabaseException("Target database is not set.");
            }

            string username = String.Empty;

            using (var reader = this.GetReader(
                "[CartridgeProcessingQueue].[dbo].[upAuthDbUsernameGet]",
                dbname))
            {
                // set index of the column to read
                int DbUserIndex = reader.GetOrdinal("DbUser");

                // make sure we have data
                while (reader.Read())
                {
                    if (reader.IsDBNull(DbUserIndex) == false)
                    {
                        // it does, set the id
                        username = reader.GetString(DbUserIndex);
                    }
                }
            }

            return username;
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
            // check that the database instance is set
            if (this.db == null)
            {
                throw new DatabaseException(
                    Properties.Resources.DatabaseNotSetMsg);
            }

            // initialize an empty list
            var credentials = new List<Credential>();

            // get the db users from the database
            using (var reader = this.GetReader(
                "[CartridgeProcessingQueue].[dbo].[upAuthDbUserGet]"))
            {
                // set indices of the columns to read
                int DbNameIndex = reader.GetOrdinal("DbName");
                int DbUserIndex = reader.GetOrdinal("DbUser");

                // initialize variables to hold username and database name
                string username;
                string dbname;
                
                // we don't store the password, so set the credential password
                // to an empty string
                string password = String.Empty;

                // make sure we have data
                while (reader.Read())
                {
                    // only create credential if we get both username and db name
                    if ((reader.IsDBNull(DbUserIndex) == false) &&
                        (reader.IsDBNull(DbNameIndex) == false))
                    {
                        // set the username
                        username = reader.GetString(DbUserIndex);
                        
                        // set the database name
                        dbname = reader.GetString(DbNameIndex);
                        
                        // add a new credential to our list
                        credentials.Add(
                            new Credential(dbname, username, password));
                    }
                }
            }

            // return the list of credentials
            return credentials;
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
        /// <revision revisor="dev22" date="03/08/2010" version="1.1.7.15">
        /// Changed since method <see cref="BaseDataService.GetReader"/> no
        /// longer takes a boolean parameter.
        /// </revision>
        public bool UpdateAuthDBUser(
            string dbName,
            string newLogin,
            string password)
        {
            // set return result
            bool result = false;
            
            // use current db connection
            using (DbConnection connection = db.CreateConnection())
            {
                // open connection
                connection.Open();
                
                // try to insert the record
                try
                {
                    // try to update the user in the service database
                    var returnVal = (int)this.RunScalarProcedure(
                        "[CartridgeProcessingQueue].[dbo].[upElectionUserAlter]",
                        newLogin,
                        password,
                        dbName);

                    if (returnVal != 0)
                    {
                        throw new DatabaseException(
                            Properties.Resources.UpdateUserErrorMsg);
                    }

                    result = true;
                }
                catch (Exception exception)
                {
                    // rethrow the exception, preserving call stack
                    throw;
                }
                finally
                {
                    // close the connection
                    connection.Close();
                }

                // return indication of whether xaction was successful
                return result;
            }
        }

        /// <summary>
        ///     Gets the current connection.
        ///     Method used to test connection creation
        /// </summary>
        /// <returns>The string</returns>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="2/24/2010" version="1.1.7.03">
        ///     Member Created
        /// </revision>
        public string GetCurrentConnection()
        {
            return this.db.ConnectionString;
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
