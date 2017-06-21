//-----------------------------------------------------------------------------
// <copyright file="DataManager.cs" 
//  company="Sequoia Voting Systems">
//     Copyright (c) 2008 Sequoia Voting Systems. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <revision revisor="dev13" date="10/6/2008" version="1.0.0.0">
//     File created
// </revision>
// <revision revisor="dev06" date="10/16/2008" version="1.0.0.0">
//     File created
// </revision>
// <revision revisor="dev14" date="5/21/2009" version="1.0.12.11">
//     File modified
// </revision>
//-----------------------------------------------------------------------------

namespace Sequoia.EMS.Core.Managers
{
    #region Using directives

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Data.SqlTypes;
    using Microsoft.SqlServer.Management.Common;
    using Microsoft.SqlServer.Management.Smo;
    using Sequoia.EMS.Core.DataServices;
    using Sequoia.EMS.Core.Utilities;

    #endregion

    /// <summary>
    ///     DataManager provides access services for the framework.
    /// </summary>
    /// <externalUnit cref="DataServiceDefinition"/>
    /// <externalUnit cref="IDataService"/>
    /// <revision revisor="dev06" date="10/16/2008" version="1.0.0.0">
    ///     Class created
    /// </revision>
    /// <revision revisor="dev14" date="5/21/2009" version="1.0.12.11">
    ///     Cleaned up some build warnings
    /// </revision>
    public sealed class DataManager : BaseDataService, IDataService
    {
        #region Constructor

        /// <summary>
        ///     Prevents a default instance of the <see cref="DataManager"/> class from being created.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev06" date="2007-03-19" version="1.0.0.0">
        ///     Member created
        /// </revision>
        private DataManager()
        {
        }

        #endregion

        #region Fields

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the singleton instance of the DataManager.
        /// </summary>
        /// <value>The instance of the DataManager.</value>
        /// <externalUnit cref="Nested"/>
        /// <revision revisor="dev06" date="10/16/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public static DataManager Instance
        {
            get
            {
                // return the nested class used to ensure it is the only instance
                return Nested.instance;
            }
        }

        #endregion

        #region Methods

        #region SMO Service Members

        #region GetAllDatabaseNamesOnServer
        /// <summary>
        ///     Gets all database names on server.
        /// </summary>
        /// <param name="dataServiceDefinition">
        ///     The data service definition.
        /// </param>
        /// <returns>
        ///     A list of all the database names found on the server as strings.
        /// </returns>
        /// <externalUnit cref="DataServiceDefinition"/>
        /// <externalUnit cref="ServerConnection"/>
        /// <externalUnit cref="ConnectionFailureException"/>
        /// <externalUnit cref="MessageHelper"/>
        /// <externalUnit cref="Server"/>
        /// <externalUnit cref="Database"/>
        /// <revision revisor="dev06" date="10/16/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public List<string> GetAllDatabaseNamesOnServer(
            DataServiceDefinition dataServiceDefinition)
        {
            // create storage for database names
            List<string> databaseNames = new List<string>();

            // create server connection
            ServerConnection serverConn = new ServerConnection();

            // Fill in necessary information
            serverConn.ServerInstance = dataServiceDefinition.ServerName;

            // Set connection timeout
            serverConn.ConnectTimeout = 30;

            // Use SQL Server authentication
            serverConn.LoginSecure = false;

            // set sql credentials
            serverConn.Login = dataServiceDefinition.SecureUsername;
            serverConn.Password = dataServiceDefinition.SecurePassword;

            try
            {
                // Go ahead and connect
                serverConn.Connect();
            }
            catch (ConnectionFailureException exception)
            {
                // tell user the connection failed.  
                MessageHelper.ShowFatal(
                    "The credentials you supplied are incorrect.  "
                    + "Please try again.",
                    "Invalid Credentials");
            }

            // if the connection succeeded
            if (serverConn.IsOpen == true)
            {
                // create new server from connection
                Server server = new Server(serverConn);

                // check each db to determine if it is a profile db
                foreach (Database database in server.Databases)
                {
                    // add it to the list if it is a profile db
                    databaseNames.Add(database.Name);
                }

                // disconnect from server
                serverConn.Disconnect();
            }

            // return the list of profile database names
            return databaseNames;
        }
        #endregion

        #region GetSmoServerConnection

        /// <summary>
        ///     Gets the smo server connection.
        /// </summary>
        /// <param name="newDataServiceDefinition">
        ///     The new data service definition.
        /// </param>
        /// <returns>
        ///     An <see cref="ServerConnection"/> created from the credentials 
        ///     in the supplied data service definition.
        /// </returns>
        /// <externalUnit cref="DataServiceDefinition"/>
        /// <externalUnit cref="ConnectionFailureException"/>
        /// <externalUnit cref="MessageHelper"/>
        /// <revision revisor="dev06" date="10/16/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public ServerConnection GetSmoServerConnection(
            DataServiceDefinition newDataServiceDefinition)
        {
            ServerConnection serverConnection = null;
            DataServiceDefinition definitionToUse;

            if (newDataServiceDefinition != null)
            {
                definitionToUse = newDataServiceDefinition;
            }
            else if (DataServiceDefinition != null)
            {
                definitionToUse = DataServiceDefinition;
            }
            else
            {
                // they are both null
                definitionToUse = null;
            }

            if (definitionToUse != null)
            {
                serverConnection = new ServerConnection();

                // Fill in necessary information
                serverConnection.ServerInstance =
                    definitionToUse.ServerName;

                // Set connection timeout
                serverConnection.ConnectTimeout = 30;

                // Use SQL Server authentication
                serverConnection.LoginSecure = false;

                // set sql credentials
                serverConnection.Login = definitionToUse.SecureUsername;
                serverConnection.Password = definitionToUse.SecurePassword;

                try
                {
                    // Go ahead and connect
                    serverConnection.Connect();
                }
                catch (ConnectionFailureException)
                {
                    // tell user the connection failed.  
                    MessageHelper.ShowFatal(
                        "The credentials you supplied are incorrect.  "
                        + "Please try again.",
                        "Invalid Credentials");
                }

                if (serverConnection.IsOpen == false)
                {
                    serverConnection = null;
                }
            }

            return serverConnection;
        }
        #endregion

        #region GetAvailableSqlServers
        /// <summary>
        ///     Gets the available SQL servers.
        /// </summary>
        /// <returns>
        ///     A list of all available sql server names as strings.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev06" date="10/16/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public List<string> GetAvailableSqlServers()
        {
            // param for database names
            List<string> databaseNames = new List<string>();

            // get all servers we can find, not just local
            DataTable dataTable = 
                SmoApplication.EnumAvailableSqlServers(false);

            // are any found?
            if (dataTable.Rows.Count > 0)
            {
                // Load server names into combo box
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    // add to list
                    databaseNames.Add(dataRow["Name"].ToString());
                }
            }
            
            // retun list of found servers
            return databaseNames;
        }
        #endregion

        #endregion

        #region SubmitCartridgeImageToTally

        /// <summary>
        ///     Submits the cartridge image to tally.
        /// </summary>
        /// <param name="serialNumber">The serial number.</param>
        /// <param name="tallyTypeId">The tally type id.</param>
        /// <param name="modeImageMap">The mode image map.</param>
        /// <param name="imageHash">The cartridge image hash.</param>
        /// <returns>
        /// 	<c>true</c> if record was inserted; otherwise <c>false</c>.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="10/16/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public bool SubmitCartridgeImageToTally(
            int serialNumber, 
            int tallyTypeId, 
            Dictionary<int, string> modeImageMap,
            string imageHash)
        {
            // param for success or not
            bool succeeded = false;

            // check contracts and use election db.
            SetTargetDatabaseAsElection();

            // use current db connection
            using (DbConnection connection = db.CreateConnection())
            {
                // open connection
                connection.Open();

                // begin transaction - submit needs to be all or nothing and 
                // several operations must conclude successfully
                DbTransaction transaction = connection.BeginTransaction();

                int result = -1;

                try
                {
                    foreach (KeyValuePair<int, string> modeImagePair in 
                        modeImageMap)
                    {
                        // insert the record
                        result = RunNonQueryProcedure(
                            transaction, 
                            true,
                            "upCartImageInsert",
                            serialNumber,
                            tallyTypeId,
                            modeImagePair.Key,
                            modeImagePair.Value,
                            imageHash);

                        if(result != 0)
                        {
                            // then failed
                            break;
                        }
                    }

                    if (result != 0)
                    {
                        transaction.Rollback();
                    }
                    else
                    {
                        // Commit the transaction
                        transaction.Commit();

                        // set result to true
                        succeeded = true;
                    }
                }
                catch
                {
                    // roll back the transaction
                    transaction.Rollback();

                    // rethrow the exception, preserving call stack
                    throw;
                }
                finally
                {
                    // close the connection
                    connection.Close();
                }
            }

            // return the operation success mode.
            return succeeded;
        }

        #endregion

        /// <summary>
        ///     Clears the tally.
        /// </summary>
        /// <returns>
        ///     <c>true</c> if successful; otherwise, <c>false</c>
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/17/2009" version="1.1.3.5">
        ///     Added documentation header
        /// </revision>
        public bool ClearTally()
        {
            // check contracts and use election db.
            SetTargetDatabaseAsElection();

            int result = RunNonQueryProcedure(true, "upTallyClear");

            return result == 0;
        }

        #region ProcessTallyResults

        /// <summary>
        ///     Processes the tally results.
        /// </summary>
        /// <returns>
        ///     <c>true</c> if tally results processed successfully; 
        ///     otherwise <c>false</c>.
        /// </returns>
        /// <externalUnit cref="BaseDataService.SetTargetDatabaseAsElection"/>
        /// <externalUnit cref="BaseDataService.RunNonQueryProcedure(bool,string,object[])"/>
        /// <externalUnit dbref="upTallyProcess"/>
        /// <revision revisor="dev13" date="10/21/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev06" date="2/10/2009" version="1.0.6.7">
        ///     Changed to use base methods rather then do all the connection 
        /// stuff here.
        /// </revision>
        public bool ProcessTallyResults()
        {
            // check contracts and use election db.
            SetTargetDatabaseAsElection();

            // use the base members
            // we won't rethrow a db exception, but if it fails for any reason 
            // we'll know it didn't work and the gui can respond appropriately
            // the dba will have to check the db logs
            int result = RunNonQueryProcedure(false, "upTallyProcess");
            
            // return the operation success mode.
            return result == 0;
        }

        #endregion

        #region ResetServerConnection
        /// <summary>
        ///     Resets the server connection.
        /// </summary>
        /// <externalUnit cref="DataServiceDefinition"/>
        /// <revision revisor="dev06" date="10/16/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public void ResetServerConnection()
        {
            // reset the dataservice definition so it 
            // must be rebuilt on next attempt to connect
            DataServiceDefinition = null;
        }
        #endregion

        #endregion

        #region Nested Class

        /// <summary>
        ///     A private class used to create the singleton.
        /// </summary>
        private class Nested
        {
            /// <summary>
            ///     param to ensure this is a singleton, thread safe
            /// </summary>
            internal static readonly DataManager instance = new DataManager();

            /// <summary>
            ///     Initializes the <see cref="Nested"/> class. Explicit static
            ///     constructor to tell C# compiler not to mark type as 
            ///     beforefieldinit.
            /// </summary>
            /// <externalUnit/>
            /// <revision revisor="dev13" date="10/8/2008" version="1.0.0.0">
            ///     Member Created
            /// </revision>
            static Nested()
            {
            }
        }

        #endregion
    }
    
}

