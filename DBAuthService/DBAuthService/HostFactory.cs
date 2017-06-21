// -----------------------------------------------------------------------------
// <copyright file="HostFactory.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2010 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the HostFactory class.
// </summary>
// <revision revisor="dev14" date="2/4/2010" version="1.1.6.07">
//     File Created
// </revision>  
// -----------------------------------------------------------------------------

namespace Sequoia.DBAuthService
{
    #region Using directives

    using System;
    using System.ServiceModel;
    using System.ServiceModel.Activation;

    #endregion

    /// <summary>
    ///	    HostFactory is a factory class to create a custom service host for
    ///     hosting the DBAuthService. To gain access to the methods of 
    ///     ServiceHost when hosting a WCF service in IIS, an instance of a
    ///     custom ServiceHost that overrides chosen methods must be specified
    ///     as an attribute in the @ServiceHost specifier in the .svc file
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev14" date="2/4/2010" version="1.1.6.07">
    ///     Class created.
    /// </revision>
    public class HostFactory : ServiceHostFactory
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="HostFactory"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="2/4/2010" version="1.1.6.07">
        ///     Member Created
        /// </revision>	
        public HostFactory()
        {
        }

        #endregion

        #region Public Properties

        #endregion

        #region Public/Protected Methods

        /// <summary>
        ///     Creates a <see cref="T:System.ServiceModel.ServiceHost"/> for a 
        ///     specified type of service with a specific base address.
        /// </summary>
        /// <param name="serviceType">
        ///     Specifies the type of service to host.
        /// </param>
        /// <param name="baseAddresses">
        ///     The <see cref="T:System.Array"/> of type 
        ///     <see cref="T:System.Uri"/> that contains the base addresses for 
        ///     the service hosted.
        /// </param>
        /// <returns>
        ///     A <see cref="T:System.ServiceModel.ServiceHost"/> for the type 
        ///     of service specified with a specific base address.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="2/4/2010" version="1.1.6.07">
        ///     Member Created
        /// </revision>
        protected override ServiceHost CreateServiceHost(
            Type serviceType, Uri[] baseAddresses)
        {
            // create custom authorization host
            var serviceHost = new AuthorizationCustomHost(
                serviceType, baseAddresses);
            
            // return the host
            return serviceHost;
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
