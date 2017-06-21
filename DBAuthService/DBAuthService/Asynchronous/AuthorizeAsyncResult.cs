// -----------------------------------------------------------------------------
// <copyright file="AuthorizeAsyncResult.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2010 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the AuthorizeAsyncResult class.
// </summary>
// <revision revisor="dev14" date="2/16/2010" version="1.1.6.19">
//     File Created
// </revision>  
// -----------------------------------------------------------------------------

namespace Sequoia.DBAuthService.Asynchronous
{
    #region Using directives

    using System;

    #endregion

    /// <summary>
    ///	    Has methods and properties to handle asynchronous operation of
    ///     the authorization service
    /// </summary>
    /// <externalUnit cref="AsyncResult" />
    /// <revision revisor="dev14" date="2/16/2010" version="1.1.6.19">
    ///     Class created.
    /// </revision>
    public class AuthorizeAsyncResult : AsyncResult
    {
        #region Fields

        /// <summary>
        ///     Authorization request from the service client
        /// </summary>
        private AuthorizationRequest authRequest;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizeAsyncResult"/> class.
        /// </summary>
        /// <param name="authRequest">
        ///     The authorization request from the service's client.
        /// </param>
        /// <param name="callback">
        ///     The callback delegate.
        /// </param>
        /// <param name="state">
        ///     The operation state. This object can contain </param>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="2/16/2010" version="1.1.6.19">
        ///     Member Created
        /// </revision>
        public AuthorizeAsyncResult(AuthorizationRequest authRequest, AsyncCallback callback, object state)
            : base(callback, state)
        {
            // set local reference to the authorization request
            this.authRequest = authRequest;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the database authorization. This will contain the
        ///     result of the service's authorization method and will pass
        ///     it back to the client in an ansynchronous manner
        /// </summary>
        /// <value>The authorization.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="2/16/2010" version="1.1.6.19">
        ///     Member Created
        /// </revision>
        public DBAuthorization Authorization
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets the authorization request.
        /// </summary>
        /// <value>The authorization request.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="2/16/2010" version="1.1.6.19">
        ///     Member Created
        /// </revision>
        public AuthorizationRequest AuthRequest
        {
            get
            {
                return this.authRequest;
            }
        }

        #endregion

        #region Public Methods

        #endregion

        #region Public Events

        #endregion

        #region Private Methods

        #endregion
    }
}
