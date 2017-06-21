// -----------------------------------------------------------------------------
// <copyright file="AuthorizationReset.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2010 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the AuthorizationReset class.
// </summary>
// <revision revisor="dev14" date="3/1/2010" version="1.1.7.08">
//     File Created
// </revision>
// <revision revisor="dev14" date="3/2/2010" version="1.1.7.09">
//     File Modified
// </revision>
// <revision revisor="dev14" date="4/08/2010" version="1.1.7">
//     File Modified
// </revision>     
// -----------------------------------------------------------------------------

namespace Sequoia.DBAuthService.Asynchronous
{
    #region Using directives

    using System;
    using System.Threading;
    using Sequoia.DBAuthService.Data;

    #endregion

    /// <summary>
    ///	    Holds a timer that at a set interval will update the db credentials
    ///     to something new.
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev14" date="3/1/2010" version="1.1.7.08">
    ///     Class created.
    /// </revision>
    public class AuthorizationReset
    {
        /// <summary>
        ///     service for database interaction
        /// </summary>
        private static IAuthorizationDataService dataService;

        /// <summary>
        /// Timer that calls method to update db credentials
        /// </summary>
        private static Timer timer;

        /// <summary>
        ///     The time to wait before starting the timed reset;
        /// </summary>
        private int waitTime = 0;

        /// <summary>
        ///     The time to wait between resetting
        /// </summary>
        private int interval = 60000;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationReset"/> class.
        /// </summary>
        /// <param name="service">The data service.</param>
        /// <param name="waitTime">The time to wait before beginning the timer.</param>
        /// <param name="intervalTime">The interval at which the timer calls the reset method</param>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="3/1/2010" version="1.1.7.08">
        /// Member Created
        /// </revision>
        /// <revision revisor="dev14" date="4/08/2010" version="1.1.7">
        /// Changes to logging.
        /// </revision>
        public AuthorizationReset(IAuthorizationDataService service, int waitTime, int intervalTime)
        {
            // set local reference to data service
            dataService = service;

            // set waitTime to what is passed in
            this.waitTime = waitTime;
            
            // set interval to what is passed in, but don't allow zero interval
            if (intervalTime != 0)
            {
                this.interval = intervalTime;
            }

            // create a callback reference to the reset method
            var callback = new TimerCallback(ResetAuthorization);

            // set the timer to run the reset
            timer = new Timer(callback, null, this.waitTime, this.interval);

            // Log message that timer is set
            LogHelper.Log(Properties.Resources.LogTimerSet);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="AuthorizationReset"/> class.
        /// Releases unmanaged resources and performs other cleanup operations 
        /// before the object is reclaimed by garbage collection.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="3/10/2010" version="1.1.7.01">
        /// Member Created
        /// </revision>
        ~AuthorizationReset()
        {
            // dispose the timer
            timer.Dispose();
        }

        #endregion

        #region Public Properties

        #endregion

        #region Public Methods

        #endregion

        #region Public Events

        #endregion

        #region Fields

        #endregion

        #region Private Methods

        /// <summary>
        ///     Resets the authorization credentials
        /// </summary>
        /// <param name="state">The state.</param>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="3/2/2010" version="1.1.7.01">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev14" date="4/08/2010" version="1.1.7">
        /// Changes to logging.
        /// </revision>
        private static void ResetAuthorization(object state)
        {
            // get the authorization manager instance
            var manager = AuthorizationManager.Instance;

            // Log that reset was called
            LogHelper.Log(Properties.Resources.LogResetCalled);

            // get a lock on the AuthorizationLock
            lock (AuthorizationLock.lockObject)
            {
                // iterate through the credentials and change them
                AuthorizationLock.Credentials.ForEach(
                    credential => 
                        manager.GetNewCredential(credential.DatabaseName));
            }

            // Log that reset is complete
            LogHelper.Log(Properties.Resources.LogResetComplete);
        }

        #endregion
    }
}
