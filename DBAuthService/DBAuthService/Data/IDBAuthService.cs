// -----------------------------------------------------------------------------
// <copyright file="IDBAuthService.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2010 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the IDBAuthService interface.
// </summary>
// <revision revisor="dev14" date="1/8/2010" version="1.1.5.05">
//     File Created
// </revision>
// <revision revisor="dev14" date="2/22/2010" version="1.1.7.01">
//     File Modified
// </revision>       
// -----------------------------------------------------------------------------

namespace Sequoia.DBAuthService
{
    #region Using Directives
    using System;
    using System.ServiceModel;
    using Sequoia.DBAuthService.Asynchronous;
    #endregion

    /// <summary>
    ///     Defines the WCF service interface for the Database Authorization
    ///     Service. Methods of the service available for consumption are
    ///     marked with the [OperationContract] attribute.
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev14" date="1/8/2010" version="1.1.5.05">
    ///     Member Created
    /// </revision>
    /// <revision revisor="dev14" date="4/8/2010" version="1.1.5.05">
    ///     Removed [GetLastCredential]
    /// </revision>
    [ServiceContract]
    public interface IDBAuthService
    {
        /// <summary>
        ///     Allows for asynchronous operation of [GetAuthorization]
        /// </summary>
        /// <param name="request">
        ///     The authorization request which contains the application 
        ///     username, password and datbase name.
        /// </param>
        /// <param name="callback">
        ///     The callback delegate. The proxy client will create this and
        ///     listen to wait for a response from the service
        /// </param>
        /// <param name="state">
        ///     The operation state.
        /// </param>
        /// <returns>
        ///     An <see cref="AuthorizeAsyncResult" /> 
        /// </returns>
        /// <externalUnit cref="IAsyncResult" />
        /// <externalUnit cref="AuthorizationRequest" />
        /// <externalUnit cref="AsyncCallback" />
        /// <revision revisor="dev14" date="2/16/2010" version="1.1.6.19">
        ///     Member Created
        /// </revision>
        [OperationContract(AsyncPattern = true)]
        IAsyncResult BeginGetAuthorization(
            AuthorizationRequest request, AsyncCallback callback, object state);

        /// <summary>
        ///     The compliment to [BeginGetAuthorization] in order to handle
        ///     asynchronous operation of [GetAuthorization]
        /// </summary>
        /// <param name="result">
        ///     An instance of <see cref="AuthorizeAsyncResult" />, the result
        ///     of the [BeginGetAuthorization] call.</param>
        /// <returns>The authorization</returns>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="2/16/2010" version="1.1.6.19">
        ///     Member Created
        /// </revision>
        DBAuthorization EndGetAuthorization(IAsyncResult result);
    }
}
