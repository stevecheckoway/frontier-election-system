// -----------------------------------------------------------------------------
// <copyright file="AuthorizationCustomHost.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2010 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the AuthorizationCustomHost class.
// </summary>
// <revision revisor="dev14" date="2/4/2010" version="1.1.6.07">
//     File Created
// </revision>
// <revision revisor="dev14" date="2/12/2010" version="1.1.6.15">
//     File modified
// </revision>
// <revision revisor="dev14" date="3/1/2010" version="1.1.7.08">
//     File modified
// </revision>
// <revision revisor="dev14" date="3/3/2010" version="1.1.7.10">
//     File modified
// </revision>
// <revision revisor="dev14" date="3/9/2010" version="1.1.7.16">
//     File modified
// </revision>
// <revision revisor="dev14" date="4/8/2010" version="1.1.7">
//     File modified
// </revision>        
// -----------------------------------------------------------------------------

namespace Sequoia.DBAuthService
{
    #region Using directives

    using System;
    using System.Configuration;
    using System.ServiceModel;
    using Sequoia.DBAuthService.Asynchronous;
    using Sequoia.DBAuthService.Data;

    #endregion

    /// <summary>
    ///	    AuthorizationCustomHost is a custom implementation of ServiceHost
    ///     in order to override certain methods to have a behavior different
    ///     than the instance of ServiceHost that IIS uses by default
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev14" date="2/4/2010" version="1.1.6.07">
    ///     Class created.
    /// </revision>
    /// <revision revisor="dev14" date="3/1/2010" version="1.1.7.08">
    ///     Removed [ChangeConfiguration]
    /// </revision>
    public class AuthorizationCustomHost : ServiceHost
    {
        #region Fields

        /// <summary>
        ///     Conversion from minutes to milliseconds
        /// </summary>
        private const int MIN_TO_MSEC = 60000;

        /// <summary>
        ///     Timer to fire to reset credentials
        /// </summary>
        private static AuthorizationReset resetTimer;
        
        /// <summary>
        ///     service for database interaction
        /// </summary>
        private static IAuthorizationDataService dataService
            = new AuthorizationDataService();

        /// <summary>
        ///     The number of minutes to wait before resetting the credentials.
        ///     Configurable ion Web.config of the AuthService
        /// </summary>
        private int timeoutMinutes = 1;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="AuthorizationCustomHost"/> class.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <param name="baseAddresses">
        ///     The base addresses of the service
        /// </param>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="2/4/2010" version="1.1.6.07">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev14" date="3/3/2010" version="1.1.7.10">
        ///     Added reset timer
        /// </revision>
        /// <revision revisor="dev14" date="3/9/2010" version="1.1.7.16">
        ///     Added call to reset users
        /// </revision>
        /// <revision revisor="dev14" date="4/8/2010" version="1.1.7">
        ///     Moved log messge to resource file
        /// </revision>
        public AuthorizationCustomHost(Type serviceType, params Uri[] baseAddresses)
            : base(serviceType, baseAddresses)
        {
            // initialize log service
            LogHelper.InitLog(dataService);

            // log that host being created
            LogHelper.Log(Properties.Resources.LogHostCreated);
            
            // Tell the manager to remove all the users.
            var manager = AuthorizationManager.Instance;
            manager.ResetAuthUsers();

            // set the timer to reset the credentials
            this.SetTimer();
        }

        #endregion

        #region Public Properties

        #endregion

        #region Public Methods

        /// <summary>
        ///     Applies certain changes to the application configuration and 
        ///     then loads the service description information from the 
        ///     configuration file and applies it to the runtime being constructed.
        /// </summary>
        /// <exception cref="T:System.InvalidOperationException">
        ///     The description of the service hosted is null.
        /// </exception>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="2/4/2010" version="1.1.6.07">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev14" date="2/12/2010" version="1.1.6.15">
        ///     Removed configuration change on start
        /// </revision>
        protected override void ApplyConfiguration()
        {
            // refresh the application settings
            ConfigurationManager.RefreshSection("appSettings");

            // apply the configuration to the instance of the service
            base.ApplyConfiguration();
        }

        #endregion

        #region Public Events

        #endregion

        #region Private Methods

        /// <summary>
        ///     Sets the timer to reset the user credentials.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="3/9/2010" version="1.1.7.01">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev14" date="4/8/2010" version="1.1.7">
        ///     Removed Logging of timer set
        /// </revision>
        private void SetTimer()
        {
            // TODO: Change this so it is not so easily changed - dev14 3/3
            // get the timeout from the web config
            Int32.TryParse(
                ConfigurationManager.AppSettings["AuthorizationTimeout"],
                out this.timeoutMinutes);

            // convert the timeout minutes to milliseconds
            int interval = this.timeoutMinutes * MIN_TO_MSEC;

            // add the reset timer
            resetTimer = new AuthorizationReset(dataService, 0, interval);
        }

        #endregion
    }
}
