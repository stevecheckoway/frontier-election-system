//-----------------------------------------------------------------------------
// <copyright file="DataServiceDefinition.cs" 
//  company="Sequoia Voting Systems">
//     Copyright (c) 2008 Sequoia Voting Systems. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
//     File created
// </revision>
//-----------------------------------------------------------------------------

namespace Sequoia.EMS.Core.DataServices
{
    #region Using directives

    using System;

    #endregion

    /// <summary>
    ///     DataServiceDefinition is a class that defines the current db
    ///     connection parameters.
    /// </summary>
    /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
    ///     Class created
    /// </revision>
    public class DataServiceDefinition : ICloneable
    {
        #region Fields

        /// <summary>
        ///     param for server name
        /// </summary>
        private string serverName = string.Empty;

        /// <summary>
        ///     param for name of profile database
        /// </summary>
        private string profileDatabaseName = string.Empty;

        /// <summary>
        ///     param for name of election database
        /// </summary>
        private string electionDatabaseName = string.Empty;

        /// <summary>
        ///     param for user name
        /// </summary>
        private string username = string.Empty;

        /// <summary>
        ///     param for password
        /// </summary>
        private string password = string.Empty;

        /// <summary>
        ///     param for which database is in use
        /// </summary>
        private bool isUsingProfileDatabase = true;

        /// <summary>
        ///     param for if we are using master database
        /// </summary>
        private bool useMasterDatabase = false;

        /// <summary>
        ///     param for the hashed username
        /// </summary>
        private string secureUsername = string.Empty;

        /// <summary>
        ///     param for the hashed password
        /// </summary>
        private string securePassword = string.Empty;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="DataServiceDefinition"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public DataServiceDefinition()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DataServiceDefinition"/> class.
        /// </summary>
        /// <param name="serverName">Name of the server.</param>
        /// <param name="profileDatabaseName">
        ///     Name of the profile database.
        /// </param>
        /// <param name="electionDatabaseName">
        ///     Name of the election database.
        /// </param>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <externalUnit cref="ServerName"/>
        /// <externalUnit cref="ProfileDatabaseName"/>
        /// <externalUnit cref="ElectionDatabaseName"/>
        /// <externalUnit cref="Username"/>
        /// <externalUnit cref="Password"/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public DataServiceDefinition(
            string serverName,
            string profileDatabaseName,
            string electionDatabaseName,
            string username,
            string password)
        {
            // set the properties
            this.ServerName = serverName;
            this.ProfileDatabaseName = profileDatabaseName;
            this.ElectionDatabaseName = electionDatabaseName;
            this.Username = username;
            this.Password = password;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the name of the server.
        /// </summary>
        /// <value>The name of the server.</value>
        /// <externalUnit cref="serverName"/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public string ServerName
        {
            get { return this.serverName; }
            set { this.serverName = value; }
        }

        /// <summary>
        ///     Gets or sets the name of the profile database.
        /// </summary>
        /// <value>The name of the profile database.</value>
        /// <externalUnit cref="profileDatabaseName"/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public string ProfileDatabaseName
        {
            get { return this.profileDatabaseName; }
            set { this.profileDatabaseName = value; }
        }

        /// <summary>
        ///     Gets or sets the name of the election database.
        /// </summary>
        /// <value>The name of the election database.</value>
        /// <externalUnit cref="electionDatabaseName"/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public string ElectionDatabaseName
        {
            get { return this.electionDatabaseName; }
            set { this.electionDatabaseName = value; }
        }

        /// <summary>
        /// Gets the name of the database that this object represents.
        /// </summary>
        /// <revision revisor="dev22" date="03/08/2010" version="1.1.7.15">
        /// Member added.
        /// </revision>
        public string DatabaseName
        {
            get
            {
                return isUsingProfileDatabase? profileDatabaseName :
                                               electionDatabaseName;
            }
        }

        /// <summary>
        ///     Gets or sets the username.
        /// </summary>
        /// <value>The username.</value>
        /// <externalUnit cref="username"/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public string Username
        {
            get { return this.username; }
            set { this.username = value; }
        }

        /// <summary>
        ///     Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        /// <externalUnit cref="password"/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public string Password
        {
            get { return this.password; }
            set { this.password = value; }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is using 
        ///     profile database.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is using profile database; 
        ///     otherwise, <c>false</c>.
        /// </value>
        /// <externalUnit cref="isUsingProfileDatabase"/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public bool IsUsingProfileDatabase
        {
            get { return this.isUsingProfileDatabase; }
            set { this.isUsingProfileDatabase = value; }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is using 
        ///     election database.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is using election database; 
        ///     otherwise, <c>false</c>.
        /// </value>
        /// <externalUnit cref="isUsingProfileDatabase"/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public bool IsUsingElectionDatabase
        {
            get { return !this.isUsingProfileDatabase; }
            set { this.isUsingProfileDatabase = !value; }
        }

        /// <summary>
        ///     Gets the connection string.
        /// </summary>
        /// <value>The connection string.</value>
        /// <externalUnit cref="IsUsingProfileDatabase"/>
        /// <externalUnit cref="ProfileDatabaseName"/>
        /// <externalUnit cref="ElectionDatabaseName"/>
        /// <externalUnit cref="ServerName"/>
        /// <externalUnit cref="Username"/>
        /// <externalUnit cref="Password"/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public string ConnectionString
        {
            get
            {
                // get the db name we are supposed to use for the connection
                string databaseName = (this.IsUsingProfileDatabase == true)
                                          ? this.ProfileDatabaseName
                                          : this.ElectionDatabaseName;
                
                // retur nthe connection string.
                return string.Format(
                    "Data Source={0};Initial Catalog={1};"
                    + "User ID={2};Password={3}",
                    this.ServerName,
                    databaseName,
                    this.SecureUsername,//Username,
                    this.SecurePassword);//Password);
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether to 
        ///     [use master database]. This will not be used as part of the 
        ///     ConnectionString property or anything like that, as this is only 
        ///     used for application entry validation and for smo operations.  
        /// </summary>
        /// <value>
        ///     <c>true</c> if [use master database]; 
        ///     otherwise, <c>false</c>.
        /// </value>
        /// <externalUnit cref="useMasterDatabase"/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public bool UseMasterDatabase
        {
            get { return this.useMasterDatabase; }
            set { this.useMasterDatabase = value; }
        }

        /// <summary>
        ///     Gets or sets the secure username.
        /// </summary>
        /// <value>The secure username.</value>
        /// <externalUnit cref="secureUsername"/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public string SecureUsername
        {
            get { return this.secureUsername; }
            set { this.secureUsername = value; }
        }

        /// <summary>
        ///     Gets or sets the secure password.
        /// </summary>
        /// <value>The secure password.</value>
        /// <externalUnit cref="securePassword"/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public string SecurePassword
        {
            get { return this.securePassword; }
            set { this.securePassword = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Validates for server connection.Do we have enough info to create
        ///     a connection string.
        /// </summary>
        /// <returns>
        ///     <c>true</c> if we can create a connection string for a server; 
        ///     otherwise <c>false</c>.
        /// </returns>
        /// <externalUnit cref="ServerName"/>
        /// <externalUnit cref="Username"/>
        /// <externalUnit cref="Password"/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public bool ValidateForServerConnection()
        {
            // do we have enough info to build the connection string.
            bool isValid = !string.IsNullOrEmpty(this.ServerName) 
                && !string.IsNullOrEmpty(this.Username) 
                && !string.IsNullOrEmpty(this.Password);

            // return the result if we can create the connection
            return isValid;
        }

        /// <summary>
        ///     Validates for catalog connection.  Do we have enough info to 
        ///     create a connection string.
        /// </summary>
        /// <returns>
        ///     <c>true</c> if we have enough info to create connection string 
        ///     for a database; otherwise <c>false</c>.
        /// </returns>
        /// <externalUnit cref="IsUsingProfileDatabase"/>
        /// <externalUnit cref="ProfileDatabaseName"/>
        /// <externalUnit cref="ElectionDatabaseName"/>
        /// <externalUnit cref="ValidateForServerConnection"/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public bool ValidateForCatalogConnection()
        {
            // get the db name to use
            string databaseName = (this.IsUsingProfileDatabase == true)
                                          ? this.ProfileDatabaseName
                                          : this.ElectionDatabaseName;

            // do we have enough info to build the connection string.
            bool isValid = this.ValidateForServerConnection()
                && !string.IsNullOrEmpty(databaseName);

            // return the result if we can create the connection
            return isValid;
        }

        /// <summary>
        ///     Uses the profile database.
        /// </summary>
        /// <externalUnit cref="IsUsingProfileDatabase"/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public void UseProfileDatabase()
        {
            // set to use profile db
            this.IsUsingProfileDatabase = true;
        }

        /// <summary>
        ///     Uses the election database.
        /// </summary>
        /// <externalUnit cref="IsUsingElectionDatabase"/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public void UseElectionDatabase()
        {
            // set to use election db
            this.IsUsingElectionDatabase = true;
        }

        /// <summary>
        ///     Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        ///     A new object that is a copy of this instance.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public object Clone()
        {
            // all members are simple types, so only need shallow copy
            return MemberwiseClone();
        }

        #region Clear
        /// <summary>
        ///     Clears this instance; resets db info.
        /// </summary>
        /// <externalUnit cref="IsUsingProfileDatabase"/>
        /// <externalUnit cref="UseMasterDatabase"/>
        /// <externalUnit cref="ServerName"/>
        /// <externalUnit cref="ProfileDatabaseName"/>
        /// <externalUnit cref="ElectionDatabaseName"/>
        /// <externalUnit cref="SecurePassword"/>
        /// <externalUnit cref="SecureUsername"/>
        /// <externalUnit cref="Password"/>
        /// <externalUnit cref="Username"/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public void Clear()
        {
            // reset all vars to defaults
            this.IsUsingProfileDatabase = true;
            this.UseMasterDatabase = false;
            this.ServerName = string.Empty;
            this.ProfileDatabaseName = string.Empty;
            this.ElectionDatabaseName = string.Empty;
            this.SecurePassword = string.Empty;
            this.SecureUsername = string.Empty;
            this.Password = string.Empty;
            this.Username = string.Empty;
        }
        #endregion

        #endregion
    }
}
