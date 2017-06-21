// -----------------------------------------------------------------------------
// <copyright file="ServiceClient.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2010 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the ServiceClient class.
// </summary>
// <revision revisor="dev14" date="2/1/2010 2:08:22 PM" version="1.0.?.0">
//     File Created
// </revision>  
// -----------------------------------------------------------------------------

using Sequoia.DBAuthService.Asynchronous;

namespace DBAuthService.Tests.Acceptance.Fixtures
{
    #region Using directives

    using System;

    using Sequoia.DBAuthService;

    #endregion

    /// <summary>
    ///	    Summary description for ServiceClient
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev14" date="2/1/2010 2:08:22 PM" version="1.0.?.0">
    ///     Class created.
    /// </revision>
    public partial class ServiceClient : System.ServiceModel.ClientBase<IDBAuthService>, IDBAuthService
    {

        public ServiceClient()
        {
        }

        public ServiceClient(string endpointConfigurationName) :
            base(endpointConfigurationName)
        {
        }

        public ServiceClient(string endpointConfigurationName, string remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public ServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public ServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
        {
        }

        public DBAuthorization GetAuthorization(AuthorizationRequest request)
        {
            // return base.Channel.GetAuthorization(request);
            return new DBAuthorization();
        }

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
        public IAsyncResult BeginGetAuthorization(AuthorizationRequest request, AsyncCallback callback, object state)
        {
            throw new System.NotImplementedException();
        }

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
        public DBAuthorization EndGetAuthorization(IAsyncResult result)
        {
            throw new System.NotImplementedException();
        }
    }
}
