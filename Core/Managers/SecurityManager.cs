//-----------------------------------------------------------------------------
// <copyright file="SecurityManager.cs" 
//  company="Sequoia Voting Systems">
//     Copyright (c) 2008 Sequoia Voting Systems. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <revision revisor="dev13" date="10/6/2008" version="1.0.0.0">
//     File created
// </revision>
// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
//     File created
// </revision>
// <revision revisor="dev13" date="10/9/2008" version="1.0.0.0">
//     File created
// </revision>
//-----------------------------------------------------------------------------

namespace Sequoia.EMS.Core.Managers
{
    #region Using directives

    using System.Security.Principal;
    using System.Web.Security;
    using Microsoft.Practices.EnterpriseLibrary.Security;
    using Sequoia.EMS.Core.DesignByContract;

    #endregion

    /// <summary>
    ///     SecurityManager is a class that provides security services.
    /// </summary>
    /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">\
    ///     Class created
    /// </revision>
    public sealed class SecurityManager
    {
        #region Private Members

        /// <summary>
        ///     param for rule provider
        /// </summary>
        private IAuthorizationProvider ruleProvider;

        /// <summary>
        ///     param for security cache to handle tokens
        /// </summary>
        private ISecurityCacheProvider cache;

        /// <summary>
        ///     param for identity for authenticated users
        /// </summary>
        private IIdentity identity;

        /// <summary>
        ///     param for valid identity token
        /// </summary>
        private IToken token;

        /// <summary>
        ///     param for the server to connect to
        /// </summary>
        private string serverName = string.Empty;

        /// <summary>
        ///     param for the database name to connect to
        /// </summary>
        private string databaseName = string.Empty;

        /// <summary>
        ///     param for the user name to use to authenticate
        /// </summary>
        private string username = string.Empty;

        /// <summary>
        ///     param for the password to use to authenticate
        /// </summary>
        private string password = string.Empty;

        #endregion

        #region Constructors

        /// <summary>
        ///     Prevents a default instance of the <see cref="SecurityManager"/> class from being created.
        /// </summary>
        /// <externalUnit cref="AuthorizationFactory"/>
        /// <externalUnit cref="SecurityCacheFactory"/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">\
        ///     Member created
        /// </revision>
        private SecurityManager()
        {
            // Get the default security manager.
            // Initializes the Enterprise Library authorization and security 
            // caching providers. The ASP.NET Membership and Profile providers 
            // do not need to be initialized in this way
            // TODO: remove strings to settings file
            this.ruleProvider =
                AuthorizationFactory.GetAuthorizationProvider("RuleProvider");
            this.cache =
                SecurityCacheFactory.GetSecurityCacheProvider(
                    "Caching Store Provider");
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the singleton instance of the SecurityManager.
        /// </summary>
        /// <value>The instance of the SecurityManager.</value>
        /// <externalUnit cref="Nested"/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">\
        ///     Member created
        /// </revision>
        public static SecurityManager Instance
        {
            get
            {
                // return the nested class used to ensure it is the only instance
                return Nested.Instance;
            }
        }
        
        /// <summary>
        ///     Gets the rule provider.
        /// </summary>
        /// <value>The rule provider.</value>
        /// <externalUnit cref="ruleProvider"/>
        /// <externalUnit cref="IAuthorizationProvider"/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public IAuthorizationProvider RuleProvider
        {
            get { return this.ruleProvider; }
        }

        /// <summary>
        ///     Gets the cache.
        /// </summary>
        /// <value>The cache.</value>
        /// <externalUnit cref="cache"/>
        /// <externalUnit cref="ISecurityCacheProvider"/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public ISecurityCacheProvider Cache
        {
            get { return this.cache; }
        }

        /// <summary>
        ///     Gets a value indicating whether this instance is token valid.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is token valid; 
        ///     otherwise, <c>false</c>.
        /// </value>
        /// <externalUnit cref="token"/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public bool IsTokenValid
        {
            get { return this.token != null; }
        }

        /// <summary>
        ///     Gets or sets the name of the server.
        /// </summary>
        /// <value>The name of the server.</value>
        /// <externalUnit cref="serverName"/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public string ServerName
        {
            get { return this.serverName; }
            set { this.serverName = value; }
        }

        /// <summary>
        ///     Gets or sets the name of the database.
        /// </summary>
        /// <value>The name of the database.</value>
        /// <externalUnit cref="databaseName"/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public string DatabaseName
        {
            get { return this.databaseName; }
            set { this.databaseName = value; }
        }

        /// <summary>
        ///     Gets or sets the username.
        /// </summary>
        /// <value>The username.</value>
        /// <externalUnit cref="username"/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public string Username
        {
            get { return this.username; }
            set { this.username = value; }
        }

        /// <summary>
        ///     Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        /// <externalUnit cref="password"/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public string Password
        {
            get { return this.password; }
            set { this.password = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Logs the out current user.
        /// </summary>
        /// <externalUnit cref="Cache"/>
        /// <externalUnit cref="token"/>
        /// <externalUnit cref="identity"/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public void LogOutCurrentUser()
        {
            // check to see if we can find the current user's security token
            if (this.Cache != null && this.token != null)
            {
                // we can, so force the token to expire
                this.Cache.ExpireIdentity(this.token);
            }

            // set the token and identity to null.
            this.token = null;
            this.identity = null;
        }

        /// <summary>
        ///     Validates the user.
        /// </summary>
        /// <returns>
        ///     <c>true</c> if the user was authenticated; 
        ///     otherwise <c>false</c>.
        /// </returns>
        /// <externalUnit cref="Contract"/>
        /// <externalUnit cref="DataManager"/>
        /// <externalUnit cref="token"/>
        /// <externalUnit cref="identity"/>
        /// <externalUnit cref="Cache"/>
        /// <externalUnit cref="username"/>
        /// <exception cref="PreconditionException">
        ///     To be handled by caller or caught by default exception handler.
        /// </exception>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public bool ValidateUser()
        {
            // ensure contracts are met
            Contract.Require(
               DataManager.Instance.DataServiceDefinition != null,
               "Cannot validate user.  DataServiceDefinition cannot be null.");

            // try to authenticate the user and password
            bool authenticated =
                Membership.ValidateUser(
                    DataManager.Instance.DataServiceDefinition.Username,
                    DataManager.Instance.DataServiceDefinition.Password);

            // check to see if the credentials worked
            if (authenticated == true)
            {
                // they did, so let's create an identity for the user
                this.identity =
                    new GenericIdentity(
                        DataManager.Instance.DataServiceDefinition.Username,
                        Membership.Provider.Name);

                // create the user token by saving the new identity in the cache
                this.token = this.Cache.SaveIdentity(this.identity);
            }
            
            // return whether or not the user's credentails worked
            return authenticated;
        }

        /// <summary>
        ///     Checks to see if the user is allowed in the given access rule.
        /// </summary>
        /// <param name="accessRule">The access rule.</param>
        /// <returns>
        ///     <c>true</c> if the user has access rights based on the 
        ///     access rule; otherwise <c>false</c>.
        /// </returns>
        /// <externalUnit cref="token"/>
        /// <externalUnit cref="Cache"/>
        /// <externalUnit cref="RuleProvider"/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public bool UserCanAccess(string accessRule)
        {
            // set up a parm to store the authorization result
            bool authorized = false;

            // make sure the user is currently authenticated
            if (this.token != null)
            {
                // get the currernt identity of the user
                IIdentity currentIdentity = this.Cache.GetIdentity(this.token);

                // make sure the identity is not null
                if (currentIdentity != null)
                {
                    // create the principal to validate authorization 
                    // against the access rule
                    IPrincipal principal =
                        new GenericPrincipal(
                            currentIdentity,
                            Roles.GetRolesForUser(currentIdentity.Name));

                    // Determine whether user is authorized for the rule 
                    // defined as accessRule.
                    authorized = this.RuleProvider.Authorize(
                        principal, accessRule);
                }
            }

            // return whether or not the user was authorized for the access rule 
            return authorized;
        }

        #endregion

        #region Nested Class

        /// <summary>
        ///     A private class used to create the singleton.
        /// </summary>
        private class Nested
        {
            /// <summary>
            ///     ensures this is a singleton, thread safe
            /// </summary>
            internal static readonly SecurityManager Instance =
                new SecurityManager();

            /// <summary>
            ///     Initializes static members of the <see cref="Nested"/> class.
            ///     Explicit static constructor to tell C# compiler not to mark
            ///     type as beforefieldinit.
            /// </summary>
            /// <externalUnit/>
            /// <revision revisor="dev13" date="10/9/2008" version="1.0.0.0">
            ///     Member Created
            /// </revision>
            static Nested()
            {
            }
        }

        #endregion
    }
}
