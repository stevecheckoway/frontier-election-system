// -----------------------------------------------------------------------------
// <copyright file="Credential.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2010 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the Credential class.
// </summary>
// <revision revisor="dev14" date="3/1/2010" version="1.1.7.08">
//     File Created
// </revision>
// <revision revisor="dev14" date="4/8/2010" version="1.1.7">
//     File Modified
// </revision>  
// -----------------------------------------------------------------------------

namespace Sequoia.DBAuthService
{
    #region Using directives

    using System;

    #endregion

    /// <summary>
    ///	    Holds user credentials for a given datbase
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev14" date="3/1/2010" version="1.1.7.08">
    ///     Class created.
    /// </revision>
    public class Credential
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Credential"/> class.
        /// </summary>
        /// <param name="dbname">The database name the credentials are good for.</param>
        /// <param name="user">The user.</param>
        /// <param name="pwd">The PWD.</param>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="3/1/2010" version="1.1.7.08">
        /// Member Created
        /// </revision>
        public Credential(string dbname, string user, string pwd)
        {
            // set datbase name for the credential
            this.DatabaseName = dbname;

            // set username for the credential
            this.CurrentUser = user;

            // set password for the credential
            this.CurrentPassword = pwd;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the current credential password.
        /// </summary>
        /// <value>The current password.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="3/2/2010" version="1.1.7.09">
        ///     Member Created
        /// </revision>
        public string CurrentPassword
        {
            get;
            private set;
        }

        /// <summary>
        ///     Gets the current credential username.
        /// </summary>
        /// <value>The current user.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="3/2/2010" version="1.1.7.01">
        ///     Member Created
        /// </revision>
        public string CurrentUser
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the name of the database to connect to
        /// </summary>
        /// <value>The name of the database.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="4/8/2010" version="1.1.7">
        ///     Member Created
        /// </revision>
        public string DatabaseName
        {
            get;
            private set;
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
