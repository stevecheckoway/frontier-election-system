// -----------------------------------------------------------------------------
// <copyright file="AuthorizationRequest.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2010 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the AuthorizationRequest class.
// </summary>
// <revision revisor="dev14" date="1/11/2010" version="1.1.5.08">
//     File Created
// </revision>
// <revision revisor="dev14" date="1/18/2010" version="1.1.5.15">
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
    ///	    An AuthorizationRequest is a data contract between the
    ///     database authorization service and its clients. The request is a
    ///     custom type that can be expressed agnostically in a WSDL document.
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev14" date="1/11/2010" version="1.1.5.08">
    ///     Class created.
    /// </revision>
    /// <revision revisor="dev14" date="1/18/2010" version="1.1.5.15">
    ///     Added [DatabaseName]
    /// </revision>
    [DataContract]
    public class AuthorizationRequest
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="AuthorizationRequest"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="1/11/2010" version="1.1.5.08">
        ///     Member Created
        /// </revision>	
        public AuthorizationRequest()
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the name of the election database for which a
        ///     connection is requested
        /// </summary>
        /// <value>The name of the database.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="1/18/2010" version="1.1.5.15">
        ///     Member Created
        /// </revision>
        [DataMember]
        public string DatabaseName
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the EMS application username to authenticate
        /// </summary>
        /// <value>The username.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="1/18/2010" version="1.1.5.15">
        ///     Member Created
        /// </revision>
        [DataMember]
        public string Username
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the EMS user's password with which to authenticate
        /// </summary>
        /// <value>The password.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="1/18/2010" version="1.1.5.15">
        ///     Member Created
        /// </revision>
        [DataMember]
        public string Password
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
