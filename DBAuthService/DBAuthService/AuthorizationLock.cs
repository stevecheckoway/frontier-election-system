// -----------------------------------------------------------------------------
// <copyright file="AuthorizationLock.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2010 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the AuthorizationLock class.
// </summary>
// <revision revisor="dev14" date="2/16/2010" version="1.1.6.19">
//     File Created
// </revision>
// <revision revisor="dev14" date="2/22/2010" version="1.1.7.01">
//     File Modified
// </revision>
// <revision revisor="dev14" date="3/1/2010" version="1.1.7.08">
//     File Modified
// </revision>    
// -----------------------------------------------------------------------------

namespace Sequoia.DBAuthService
{
    #region Using directives

    using System;
    using System.Collections.Generic;

    #endregion

    /// <summary>
    ///	    Manages the expiration of the current database authorization
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev14" date="2/16/2010" version="1.1.6.19">
    ///     Class created.
    /// </revision>
    /// <revision revisor="dev14" date="3/1/2010" version="1.1.7.08">
    ///     Added [lockObject] and [Credentials]
    /// </revision>
    public class AuthorizationLock
    {
        #region Fields
        /// <summary>
        ///     Object to lock threads that are modifying authoirization
        ///     credentials.
        /// </summary>
        public static object lockObject = new object();

        /// <summary>
        ///     Time to hold the lock until
        /// </summary>
        private static DateTime expiration = DateTime.MinValue;

        /// <summary>
        ///     Hold a collection of credential information
        /// </summary>
        private static List<Credential> credentials = new List<Credential>();

        #endregion
        
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="AuthorizationLock"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="2/16/2010" version="1.1.6.19">
        ///     Member Created
        /// </revision>	
        public AuthorizationLock()
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the expiration time.
        /// </summary>
        /// <value>The expiration time.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="2/22/2010" version="1.1.7.01">
        ///     Member Created
        /// </revision>
        public static DateTime ExpirationTime
        {
            get
            {
                return expiration;
            }
        }

        /// <summary>
        /// Gets the credentials.
        /// </summary>
        /// <value>The credentials.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="3/1/2010" version="1.1.7.01">
        ///     Member Created
        /// </revision>
        public static List<Credential> Credentials
        {
            get
            {
                return credentials;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Determines whether the authorization is locked.
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if lock has not expired; otherwise, <c>false</c>.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="2/16/2010" version="1.1.6.19">
        ///     Member Created
        /// </revision>
        public static bool IsLocked()
        {
            // return isLocked;
            return expiration > DateTime.Now;
        }

        /// <summary>
        ///     Locks the authorization until the given expiration date/time
        /// </summary>
        /// <param name="expirationTime">
        ///     The time at which the authorization expires.
        /// </param>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="2/16/2010" version="1.1.6.19">
        ///     Member Created
        /// </revision>
        public static void Lock(DateTime expirationTime)
        {
            expiration = expirationTime;
        }

        #endregion

        #region Private Methods

        #endregion
    }
}
