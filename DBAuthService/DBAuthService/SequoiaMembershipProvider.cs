// -----------------------------------------------------------------------------
// <copyright file="SequoiaMembershipProvider.cs" 
//     company="Sequoia Voting Systems"> 
//     Copyright (c) 2010 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the SequoiaMembershipProvider class.
// </summary>
// <revision revisor="dev13" date="3/18/2010" version="1.1.7.26">
//     File Created
// </revision>  
// -----------------------------------------------------------------------------

namespace Sequoia.DBAuthService
{
    #region Using directives

    using System;
    using System.Reflection;
    using System.Web.Security;

    #endregion

    /// <summary>
    ///	    SequoiaMembershipProvider extends SqlMembershipProvider to override
    ///     the initialize method for use of a custom connection string.
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev13" date="3/18/2010" version="1.1.7.26">
    ///     Class created.
    /// </revision>
    public class SequoiaMembershipProvider : SqlMembershipProvider
    {
        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="SequoiaMembershipProvider"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="3/18/2010" version="1.1.7.26">
        ///     Member Created
        /// </revision>	
        public SequoiaMembershipProvider()
        {
        }

        #endregion

        #region Public Properties

        #endregion

        #region Public Methods

        /// <summary>
        ///     Initializes the SQL Server membership provider with the property 
        ///     values specified in the ASP.NET application's configuration 
        ///     file. This method is not intended to be used directly from 
        ///     your code.
        /// </summary>
        /// <param name="name">
        ///     The name of the 
        ///     <see cref="T:System.Web.Security.SqlMembershipProvider"/> 
        ///     instance to initialize.
        /// </param>
        /// <param name="config">
        ///     A <see cref="T:System.Collections.Specialized.NameValueCollection"/> 
        ///     that contains the names and values of configuration options for 
        ///     the membership provider.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        /// 	<paramref name="config"/> is null.
        /// </exception>
        /// <exception cref="T:System.Configuration.Provider.ProviderException">
        ///     The enablePasswordRetrieval, enablePasswordReset, 
        ///     requiresQuestionAndAnswer, or requiresUniqueEmail attribute is 
        ///     set to a value other than a Boolean.
        ///     - or -
        ///     The maxInvalidPasswordAttempts or the passwordAttemptWindow 
        ///     attribute is set to a value other than a positive integer.
        ///     - or -
        ///     The minRequiredPasswordLength attribute is set to a value other 
        ///     than a positive integer, or the value is greater than 128.
        ///     - or -
        ///     The minRequiredNonalphanumericCharacters attribute is set to a 
        ///     value other than zero or a positive integer, or the value is 
        ///     greater than 128.
        ///     - or -
        ///     The value for the passwordStrengthRegularExpression attribute is 
        ///     not a valid regular expression.
        ///     - or -
        ///     The applicationName attribute is set to a value that is greater 
        ///     than 256 characters.
        ///     - or -
        ///     The passwordFormat attribute specified in the application 
        ///     configuration file is an invalid 
        ///     <see cref="T:System.Web.Security.MembershipPasswordFormat"/> 
        ///     enumeration.
        ///     - or -
        ///     The passwordFormat attribute is set to 
        ///     <see cref="F:System.Web.Security.MembershipPasswordFormat.Hashed"/> 
        ///     and the enablePasswordRetrieval attribute is set to true in the 
        ///     application configuration.
        ///     - or -
        ///     The passwordFormat attribute is set to Encrypted and the 
        ///     machineKey configuration element specifies AutoGenerate for the 
        ///     decryptionKey attribute.
        ///     - or -
        ///     The connectionStringName attribute is empty or does not exist in 
        ///     the application configuration.
        ///     - or -
        ///     The value of the connection string for the connectionStringName 
        ///     attribute value is empty, or the specified connectionStringName 
        ///     does not exist in the application configuration file.
        ///     - or -
        ///     The value for the commandTimeout attribute is set to a value 
        ///     other than zero or a positive integer.
        ///     - or -
        ///     The application configuration file for this 
        ///     <see cref="T:System.Web.Security.SqlMembershipProvider"/> 
        ///     instance contains an unrecognized attribute.
        /// </exception>
        /// <exception cref="T:System.Web.HttpException">
        ///     The current trust level is less than Low.
        /// </exception>
        /// <exception cref="T:System.InvalidOperationException">
        ///     The provider has already been initialized prior to the current 
        ///     call to the 
        ///     <see cref="M:System.Web.Security.SqlMembershipProvider.Initialize(System.String,System.Collections.Specialized.NameValueCollection)"/> 
        ///     method.
        /// </exception>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="3/18/2010" version="1.1.7.26">
        ///     Member Created
        /// </revision>
        public override void Initialize(
            string name, 
            System.Collections.Specialized.NameValueCollection config)
        {
            // call base initialization
            base.Initialize(name, config);

            // get the database name from the authorization manager
            var databaseName = AuthorizationManager.Instance.GetDbName();

            // create the connection string
            var connectionString =
                "Data Source=.;Initial Catalog=" + databaseName
                + ";Integrated Security=SSPI;";

            // set private property of Membership provider
            var connectionStringField =
                GetType().BaseType.GetField(
                    "_sqlConnectionString",
                    BindingFlags.Instance | BindingFlags.NonPublic);
            connectionStringField.SetValue(this, connectionString);
        }

        #endregion

        #region Public Events

        #endregion

        #region Private Methods

        #endregion
    }
}
