// -----------------------------------------------------------------------------
// <copyright file="DBAuthorization.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2010 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the DBAuthorization class.
// </summary>
// <revision revisor="dev14" date="1/8/2010" version="1.1.5.05">
//     File Created
// </revision>
// <revision revisor="dev14" date="1/12/2010" version="1.1.5.09">
//     File Modified
// </revision>    
// -----------------------------------------------------------------------------

namespace Sequoia.DBAuthService
{
    #region Using directives

    using System;
    using System.Runtime.Serialization;

    #endregion

    /// <summary>
    ///	    Authorization response from DBAuthorization Service. A data contract
    ///     between the service and its clients. The response is a custom type 
    ///     that can be expressed agnostically in a WSDL document.
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev14" date="1/8/2010" version="1.1.5.05">
    ///     Class created.
    /// </revision>
    /// <revision revisor="dev14" date="1/12/2010" version="1.1.5.09">
    ///     Broke down ConnectionString into its components
    /// </revision>
    [DataContract]
    public class DBAuthorization
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="DBAuthorization"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="1/8/2010 2:25:46 PM" version="1.0.?.0">
        ///     Member Created
        /// </revision>	
        public DBAuthorization()
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets a value indicating whether the user is 
        ///     authenticated.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if user is authenticated; otherwise, <c>false</c>.
        /// </value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="1/8/2010" version="1.1.5.05">
        ///     Member Created
        /// </revision>
        [DataMember]
        public bool IsAuthenticated
        {
            get; 
            set;
        }

        /// <summary>
        ///     Gets or sets the application roles the user is a member of
        /// </summary>
        /// <value>The array of names of roles the user is part of.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="1/8/2010" version="1.1.5.05">
        ///     Member Created
        /// </revision>
        [DataMember]
        public string[] Roles
        {
            get; 
            set;
        }

        /// <summary>
        ///     Gets or sets the name of the DB server.
        /// </summary>
        /// <value>The DB server name.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="1/12/2010" version="1.1.5.09">
        ///     Member Created
        /// </revision>
        [DataMember]
        public string DBServer
        {
            get; 
            set;
        }

        /// <summary>
        ///     Gets or sets the election database name. This value is supplied
        ///     on installation of the service and is used to key the
        ///     application settings that hold the database configuration info
        /// </summary>
        /// <value>The database name.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="1/12/2010" version="1.1.5.09">
        ///     Member Created
        /// </revision>
        [DataMember]
        public string DBName
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the DB username for the requested database
        ///     connection information
        /// </summary>
        /// <value>The DB user.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="1/12/2010" version="1.1.5.09">
        ///     Member Created
        /// </revision>
        [DataMember]
        public string DBUser
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the DB password for the requested database
        ///     connection information
        /// </summary>
        /// <value>The DB password.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="1/12/2010" version="1.1.5.09">">
        ///     Member Created
        /// </revision>
        [DataMember]
        public string DBPassword
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the time the authorization was initially created.
        /// </summary>
        /// <value>The time authorization was created.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="2/16/2010" version="1.1.6.19">
        ///     Member Created
        /// </revision>
        [DataMember]
        public DateTime CreatedTime
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the expiration time of the authorization.
        /// </summary>
        /// <value>The expiration time.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="2/16/2010" version="1.1.6.19">
        ///     Member Created
        /// </revision>
        [DataMember]
        public DateTime ExpirationTime
        {
            get;
            set;
        }

        #endregion

        #region Public Methods

        #endregion

        #region Public Events

        #endregion

        #region Fields

        #endregion

        #region Private Methods

        #endregion
    }
}
