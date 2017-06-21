// -----------------------------------------------------------------------------
// <copyright file="DBAuthService.svc.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2010 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the DBAuthService class.
// </summary>
// <revision revisor="dev14" date="1/8/2010" version="1.1.5.05">
//     File Created
// </revision>
// <revision revisor="dev14" date="1/18/2010" version="1.1.5.15">
//     File Modified
// </revision>
// <revision revisor="dev14" date="2/10/2010" version="1.1.6.13">
//     File Modified
// </revision>
// <revision revisor="dev14" date="2/19/2010" version="1.1.6.22">
//     File Modified
// </revision>
// <revision revisor="dev14" date="2/22/2010" version="1.1.7.01">
//     File Modified
// </revision>
// <revision revisor="dev14" date="2/25/2010" version="1.1.7.04">
//     File Modified
// </revision>
// <revision revisor="dev14" date="2/26/2010" version="1.1.7.05">
//     File Modified
// </revision>
// <revision revisor="dev14" date="3/2/2010" version="1.1.7.09">
//     File Modified
// </revision>  
// <revision revisor="dev01" date="3/3/2010" version="1.1.7.10">
//     File modified
// </revision>
// <revision revisor="dev13" date="3/18/2010" version="1.1.7.26">
//     File modified
// </revision>
// <revision revisor="dev14" date="4/8/2010" version="1.1.7">
//     File modified
// </revision>
// -----------------------------------------------------------------------------

namespace Sequoia.DBAuthService
{
    #region Using Directives

    using System;
    using System.Configuration;
    using System.Threading;
    using System.Web.Security;

    using Sequoia.DBAuthService.Asynchronous;
    using Sequoia.DBAuthService.Data;

    #endregion

    /// <summary>
    ///     Implementation of the Database Authorization interface to the EMS.
    ///     The service accepts authorization requests and returns basic user
    ///     authentication information as well as the database connection
    ///     information for the EMS to connect to the requested election 
    ///     database. This service cannot interface the election data directly.
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev14" date="1/8/2010" version="1.1.5.05">
    ///     Member Created
    /// </revision>
    /// <revision revisor="dev14" date="3/1/2010" version="1.1.7.08">
    ///     Removed [IsAuthorizationLocked]
    /// </revision>
    /// <revision revisor="dev14" date="4/8/2010" version="1.1.7">
    ///     Removed [GetLastCredential]
    /// </revision>
    public class DBAuthService : IDBAuthService
    {
        #region Fields

        /// <summary>
        ///     object to hold lock on thread to call authorication
        /// </summary>
        private static object lockObject = new object();

        /// <summary>
        ///     service to handle database interaction
        /// </summary>
        private IAuthorizationDataService dataService;

        #endregion
        
        /// <summary>
        /// Initializes a new instance of the <see cref="DBAuthService"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="2/1/2010" version="1.1.6.04">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev01" date="3/3/2010" version="1.1.7.10">
        ///     Added logging support
        /// </revision>
        public DBAuthService()
        {
            // create new data service
            this.dataService = new AuthorizationDataService();
            
            // initialize log service with data service
            LogHelper.InitLog(this.dataService);
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
        /// <revision revisor="dev14" date="2/26/2010" version="1.1.7.05">
        ///     Added lock for concurrent callers
        /// </revision>
        /// <revision revisor="dev01" date="3/3/2010" version="1.1.7.10">
        ///     Added logging support
        /// </revision>
        public IAsyncResult BeginGetAuthorization(
            AuthorizationRequest request, AsyncCallback callback, object state)
        {
            // initialize the result
            var result = new AuthorizeAsyncResult(request, callback, state);

            // Get static lock object to make other callers wait 
            lock (AuthorizationLock.lockObject)
            {
                // get the authorization
                this.CallAuthorize(result);
            }

            return result;
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
        /// <revision revisor="dev01" date="3/3/2010" version="1.1.7.10">
        ///     Added logging support
        /// </revision>
        /// <revision revisor="dev14" date="3/9/2010" version="1.1.7.16">
        ///     Removed logging of the username
        /// </revision>
        public DBAuthorization EndGetAuthorization(IAsyncResult result)
        {
            // initialize a blank authorization
            DBAuthorization authorization = null;

            // check that BeginGetAuthorization has returned a valid result
            if (result != null)
            {
                // cast the result as an AuthorizeAsyncResult
                // We will dispose of the result when we are finished
                using (var asyncResult = result as AuthorizeAsyncResult)
                {
                    // check that the result returned by BeginGetAuthorization
                    // is a true AuthorizeAsyncResult and not some other
                    // implementation of IAsyncResult
                    if (asyncResult == null)
                    {
                        throw new ArgumentNullException(
                            "result",
                            Properties.Resources.IAsyncResultError);
                    }

                    // block reset event until freed (?)
                    asyncResult.AsyncWait.WaitOne();

                    // set authorization to return to what GetAuthorization
                    // has asynchronously returned
                    authorization = asyncResult.Authorization;

                    // Log that we returned a new authorization
                    LogHelper.Log(
                        string.Format(
                            Properties.Resources.LogAuthorizationReturned,
                            Environment.NewLine,
                            authorization.DBName));
                }
            }

            // return the authorization to the service client
            return authorization;
        }

        /// <summary>
        ///     Checks the membership provider to authenticate the user and
        ///     add the user roles to the authorization response
        /// </summary>
        /// <param name="authorization">The authorization response.</param>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="2/25/2010" version="1.1.7.04">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev13" date="3/18/2010" version="1.1.7.26">
        ///     Changed membership provider to custom provider
        /// </revision>
        private void GetAuthentication(
            DBAuthorization authorization, string username, string password)
        {
            // get the membership provider
            var membership =
                Membership.
                Providers["Sequoia.DBAuthService.SequoiaMembershipProvider"];

            // check that the membership provider was set in config
            if (membership == null)
            {
                // no provider so cannot authenticate
                authorization.IsAuthenticated = false;
                authorization.Roles = new string[0];
            }
            else
            {
                try
                {
                    // authenticate given username/password
                    authorization.IsAuthenticated =
                        membership.ValidateUser(
                            username,
                            password);

                    // check whether user is authenticated
                    if (authorization.IsAuthenticated)
                    {
                        // get the membership roles the user belongs to from
                        // the Roles provider
                        authorization.Roles = Roles.GetRolesForUser(username);
                    }
                }
                catch (Exception ex)
                {
                    // TODO: log this error?
                    var error = ex.Message + ex.StackTrace;

                    // rethrow error
                    throw;
                }
            }
        }

        /// <summary>
        ///     Handles an authorization request. Uses 
        ///     <see cref="Membership.Provider" /> to handle authentication of
        ///     the user and returns connection information if the user is 
        ///     authorized to connect to the database.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>The authorization</returns>
        /// <externalUnit see="GetAuthentication" />
        /// <externalUnit see="SetAuthorizationDBInfo" />
        /// <revision revisor="dev14" date="1/8/2010" version="1.1.5.05">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev14" date="2/19/2010" version="1.1.6.22">
        ///     Fixed setting expiration time.
        /// </revision>
        /// <revision revisor="dev14" date="2/25/2010" version="1.1.7.04">
        ///     Extracted [GetAuthentication]
        /// </revision>
        /// <revision revisor="dev14" date="2/25/2010" version="1.1.7.04">
        ///     Removed setting expiration
        /// </revision>
        /// <revision revisor="dev13" date="3/18/2010" version="1.1.7.26">
        ///     Added call to SetDbName on Authorization manager so that the
        ///     custom membership provider will have access to this property.
        /// </revision>
        private DBAuthorization GetAuthorization(AuthorizationRequest request)
        {
            var manager = AuthorizationManager.Instance;
            manager.SetDbName(request.DatabaseName);
            
            // create a new authorization response
            var authorization = new DBAuthorization();

            // set the authorization creation time
            authorization.CreatedTime = DateTime.Now;

            // get the application username from the request. This is the user
            // that is authenticated with the MS Membership provider. It is NOT
            // the database username.
            string username = request.Username;

            // get the password for application user
            string password = request.Password;

            // get the database name that the user wants to get the 
            // connection for
            string databaseName = request.DatabaseName;

            // check membership provider and set roles if authenticated
            this.GetAuthentication(authorization, username, password);

            // add the database information to the authorization response
            this.SetAuthorizationDBInfo(authorization, databaseName);

            // return the authorization to the service client
            return authorization;
        }

        /// <summary>
        /// Sets the authorization DB info.
        /// </summary>
        /// <param name="authorization">The authorization.</param>
        /// <param name="dbName">Name of the db.</param>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="1/18/2010" version="1.1.5.15">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev14" date="1/21/2010" version="1.1.5.18">
        ///     Removed dynamic appConfig keys. Just one username and pwd
        ///     for the database now.
        /// </revision>
        /// <revision revisor="dev14" date="2/10/2010" version="1.1.6.13">
        ///     Changed to get authorization uname and pwd from authorization
        ///     manager after it has been updated.
        /// </revision>
        /// <revision revisor="dev14" date="3/2/2010" version="1.1.7.09">
        ///     Gets a credential object from the manager instead of a
        ///     different authorization object
        /// </revision>
        private void SetAuthorizationDBInfo(
            DBAuthorization authorization, string dbName)
        {
            var manager = AuthorizationManager.Instance;

            // get authorization from the manager class
            var newCredential = manager.GetCurrentCredential(dbName);

            // TODO: Should be able to configure database server name - dev14
            // set db settings on authorization
            authorization.DBServer = ".";
            authorization.DBName = dbName;
            authorization.DBUser = newCredential.CurrentUser;
            authorization.DBPassword = newCredential.CurrentPassword;
        }

        /// <summary>
        ///     Calls the GetAuthorize method when any previous authorization
        ///     expires
        /// </summary>
        /// <param name="state">The state.</param>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="2/16/2010" version="1.1.6.19">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev14" date="3/1/2010" version="1.1.7.08">
        ///     Removed waiting for authorizationLock and resetting the
        ///     authorization expiration
        /// </revision>
        /// <revision revisor="dev14" date="4/8/2010" version="1.1.7">
        ///     Moved error messages to resource file
        /// </revision>
        private void CallAuthorize(object state)
        {
            // lock the service to complete authorization
            lock (this)
            {
                var asyncResult = state as AuthorizeAsyncResult;
                try
                {
                    // call the synchronous GetAuthorization method
                    asyncResult.Authorization =
                        this.GetAuthorization(asyncResult.AuthRequest);
                }
                catch (Exception exception)
                {
                    // put type of exception encountered into error msg
                    // because service will return general service fault but
                    // the error message, currently
                    string exceptionTypeName = typeof(Exception).FullName;
                    string errorMsg = string.Format(
                        Properties.Resources.ServiceErrorExceptionType,
                        exceptionTypeName);
                    LogHelper.LogError(
                        string.Format(
                            Properties.Resources.CallAuthorizeError,
                            Environment.NewLine,
                            errorMsg));
                    throw new Exception(errorMsg, exception);
                }
                finally
                {
                    // signal operation complete
                    asyncResult.Complete();
                }
            }
        }
    }
}
