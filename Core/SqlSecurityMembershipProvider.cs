//-----------------------------------------------------------------------------
// <copyright file="SqlSecurityMembershipProvider.cs" 
//  company="Sequoia Voting Systems">
//     Copyright (c) 2008 Sequoia Voting Systems. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
//     File created
// </revision>
// <revision revisor="dev13" date="11/17/2009" version="1.0.0.0">
//     File modified
// </revision>
//-----------------------------------------------------------------------------

namespace Sequoia.EMS.Core
{
    #region Using directives

    using System;
    using System.Web.Security;
    using Microsoft.SqlServer.Management.Common;
    using Sequoia.EMS.Core.DesignByContract;
    using Sequoia.EMS.Core.Managers;

    #endregion

    /// <summary>
    ///     SqlSecurityMembershipProvider implements the Micorosft Membership
    ///     provider. Its goal is to allow us to use the standard Sql security
    ///     membership as our membership provider. We are using this to allow us
    ///     to tie into the default .net membership provider while still using 
    ///     the existing app secuirty model. Therefore we have most of the 
    ///     members of the interface as 'not implemented.' We are not using any
    ///     of these features.
    /// </summary>
    /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
    ///     Member created
    /// </revision>
    public sealed class SqlSecurityMembershipProvider : MembershipProvider
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="SqlSecurityMembershipProvider"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public SqlSecurityMembershipProvider()
        {
        }

        #endregion

        #region MembershipProvider Members

        #region Unimplemented Members of MembershipProvider interface

        /// <summary>
        ///     Gets or sets the name of the application using the 
        ///     custom membership provider.
        /// </summary>
        /// <value></value>
        /// <returns>
        ///     The name of the application using the custom membership provider.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/17/2009" version="1.1.3.5">
        ///     Added documentation header
        /// </revision>
        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException(
                    "The method or operation is not implemented.");
            }

            set
            {
                throw new NotImplementedException(
                    "The method or operation is not implemented.");
            }
        }

        /// <summary>
        ///     Gets a value indicating whether the membership provider is configured to 
        ///     allow users to reset their passwords.
        /// </summary>
        /// <value></value>
        /// <returns>
        ///     <c>true</c> if the membership provider supports password reset; 
        ///     otherwise, <c>false</c>. The default is <c>true</c>.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/17/2009" version="1.1.3.5">
        ///     Added documentation header
        /// </revision>
        public override bool EnablePasswordReset
        {
            get
            {
                throw new NotImplementedException(
                    "The method or operation is not implemented.");
            }
        }

        /// <summary>
        ///     Gets a value indicating whether the membership provider 
        ///     is configured to allow users to retrieve their passwords.
        /// </summary>
        /// <value></value>
        /// <returns>
        ///     true if the membership provider is configured to support 
        ///     password retrieval; otherwise, false. The default is false.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/17/2009" version="1.1.3.5">
        ///     Added documentation header
        /// </revision>
        public override bool EnablePasswordRetrieval
        {
            get
            {
                throw new NotImplementedException(
                    "The method or operation is not implemented.");
            }
        }

        /// <summary>
        ///     Gets the number of invalid password or password-answer attempts 
        ///     allowed before the membership user is locked out.
        /// </summary>
        /// <value></value>
        /// <returns>
        ///     The number of invalid password or password-answer attempts 
        ///     allowed before the membership user is locked out.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/17/2009" version="1.1.3.5">
        ///     Added documentation header
        /// </revision>
        public override int MaxInvalidPasswordAttempts
        {
            get
            {
                throw new NotImplementedException(
                    "The method or operation is not implemented.");
            }
        }

        /// <summary>
        ///     Gets the minimum number of special characters that must be 
        ///     present in a valid password.
        /// </summary>
        /// <value></value>
        /// <returns>
        ///     The minimum number of special characters that must be present 
        ///     in a valid password.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/17/2009" version="1.1.3.5">
        ///     Added documentation header
        /// </revision>
        public override int MinRequiredNonAlphanumericCharacters
        {
            get
            {
                throw new NotImplementedException(
                    "The method or operation is not implemented.");
            }
        }

        /// <summary>
        ///     Gets the minimum length required for a password.
        /// </summary>
        /// <value></value>
        /// <returns>
        ///     The minimum length required for a password.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/17/2009" version="1.1.3.5">
        ///     Added documentation header
        /// </revision>
        public override int MinRequiredPasswordLength
        {
            get
            {
                throw new NotImplementedException(
                    "The method or operation is not implemented.");
            }
        }

        /// <summary>
        ///     Gets the number of minutes in which a maximum number of invalid 
        ///     password or password-answer attempts are allowed before 
        ///     the membership user is locked out.
        /// </summary>
        /// <value></value>
        /// <returns>
        ///     The number of minutes in which a maximum number of invalid 
        ///     password or password-answer attempts are allowed before the 
        ///     membership user is locked out.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/17/2009" version="1.1.3.5">
        ///     Added documentation header
        /// </revision>
        public override int PasswordAttemptWindow
        {
            get
            {
                throw new NotImplementedException(
                    "The method or operation is not implemented.");
            }
        }

        /// <summary>
        ///     Gets a value indicating the format for storing passwords in the 
        ///     membership data store.
        /// </summary>
        /// <value></value>
        /// <returns>
        ///     One of the 
        ///     <see cref="T:System.Web.Security.MembershipPasswordFormat"/> 
        ///     values indicating the format for storing passwords in 
        ///     the data store.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/17/2009" version="1.1.3.5">
        ///     Added documentation header
        /// </revision>
        public override MembershipPasswordFormat PasswordFormat
        {
            get
            {
                throw new NotImplementedException(
                    "The method or operation is not implemented.");
            }
        }

        /// <summary>
        ///     Gets the regular expression used to evaluate a password.
        /// </summary>
        /// <value></value>
        /// <returns>
        ///     A regular expression used to evaluate a password.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/17/2009" version="1.1.3.5">
        ///     Added documentation header
        /// </revision>
        public override string PasswordStrengthRegularExpression
        {
            get
            {
                throw new NotImplementedException(
                    "The method or operation is not implemented.");
            }
        }

        /// <summary>
        ///     Gets a value indicating whether the membership provider is 
        ///     configured to require the user to answer a password question 
        ///     for password reset and retrieval.
        /// </summary>
        /// <value></value>
        /// <returns>
        ///     <c>true</c> if a password answer is required for password reset 
        ///     and retrieval; otherwise, <c>false</c>. 
        ///     The default is <c>true</c>.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/17/2009" version="1.1.3.5">
        ///     Added documentation header
        /// </revision>
        public override bool RequiresQuestionAndAnswer
        {
            get
            {
                throw new NotImplementedException(
                    "The method or operation is not implemented.");
            }
        }

        /// <summary>
        ///     Gets a value indicating whether the membership provider is 
        ///     configured to require a unique e-mail address for each user name.
        /// </summary>
        /// <value></value>
        /// <returns>
        ///     <c>true</c> if the membership provider requires a unique 
        ///     e-mail address; otherwise, <c>false</c>. 
        ///     The default is <c>true</c>.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/17/2009" version="1.1.3.5">
        ///     Added documentation header
        /// </revision>
        public override bool RequiresUniqueEmail
        {
            get
            {
                throw new NotImplementedException(
                    "The method or operation is not implemented.");
            }
        }

        /// <summary>
        ///     Processes a request to update the password for a membership user.
        /// </summary>
        /// <param name="username">The user to update the password for.</param>
        /// <param name="oldPassword">
        ///     The current password for the specified user.
        /// </param>
        /// <param name="newPassword">
        ///     The new password for the specified user.
        /// </param>
        /// <returns>
        /// true if the password was updated successfully; otherwise, false.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/17/2009" version="1.1.3.5">
        ///     Added documentation header
        /// </revision>
        public override bool ChangePassword(
            string username, 
            string oldPassword, 
            string newPassword)
        {
            throw new NotImplementedException(
                "The method or operation is not implemented.");
        }

        /// <summary>
        ///     Processes a request to update the password question and answer 
        ///     for a membership user.
        /// </summary>
        /// <param name="username">
        ///     The user to change the password question and answer for.
        /// </param>
        /// <param name="password">
        ///     The password for the specified user.
        /// </param>
        /// <param name="newPasswordQuestion">
        ///     The new password question for the specified user.
        /// </param>
        /// <param name="newPasswordAnswer">
        ///     The new password answer for the specified user.
        /// </param>
        /// <returns>
        ///     <c>true</c> if the password question and answer are 
        ///     updated successfully; otherwise, <c>false</c>.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/17/2009" version="1.1.3.5">
        ///     Added documentation header
        /// </revision>
        public override bool ChangePasswordQuestionAndAnswer(
            string username, 
            string password, 
            string newPasswordQuestion, 
            string newPasswordAnswer)
        {
            throw new NotImplementedException(
                "The method or operation is not implemented.");
        }

        /// <summary>
        ///     Adds a new membership user to the data source.
        /// </summary>
        /// <param name="username">The user name for the new user.</param>
        /// <param name="password">The password for the new user.</param>
        /// <param name="email">The e-mail address for the new user.</param>
        /// <param name="passwordQuestion">
        ///     The password question for the new user.
        /// </param>
        /// <param name="passwordAnswer">
        ///     The password answer for the new user
        /// </param>
        /// <param name="isApproved">
        ///     Whether or not the new user is approved to be validated.
        /// </param>
        /// <param name="providerUserKey">
        ///     The unique identifier from the membership data source for 
        ///     the user.
        /// </param>
        /// <param name="status">
        ///     A <see cref="T:System.Web.Security.MembershipCreateStatus"/> 
        ///     enumeration value indicating whether the user was 
        ///     created successfully.
        /// </param>
        /// <returns>
        ///     A <see cref="T:System.Web.Security.MembershipUser"/> object 
        ///     populated with the information for the newly created user.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/17/2009" version="1.1.3.5">
        ///     Added documentation header
        /// </revision>
        public override MembershipUser CreateUser(
            string username, 
            string password, 
            string email, 
            string passwordQuestion, 
            string passwordAnswer, 
            bool isApproved, 
            object providerUserKey, 
            out MembershipCreateStatus status)
        {
            throw new NotImplementedException(
                "The method or operation is not implemented.");
        }

        /// <summary>
        ///     Removes a user from the membership data source.
        /// </summary>
        /// <param name="username">The name of the user to delete.</param>
        /// <param name="deleteAllRelatedData">
        ///     <c>true</c> to delete data related to the user 
        ///     from the database; <c>false</c> to leave data related to the 
        ///     user in the database.
        /// </param>
        /// <returns>
        ///     <c>true</c> if the user was successfully deleted; 
        ///     otherwise, <c>false</c>.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/17/2009" version="1.1.3.5">
        ///     Added documentation header
        /// </revision>
        public override bool DeleteUser(
            string username, 
            bool deleteAllRelatedData)
        {
            throw new NotImplementedException(
                "The method or operation is not implemented.");
        }

        /// <summary>
        ///     Gets a collection of membership users where the e-mail address 
        ///     contains the specified e-mail address to match.
        /// </summary>
        /// <param name="emailToMatch">The e-mail address to search for.</param>
        /// <param name="pageIndex">
        ///     The index of the page of results to return. 
        ///     <paramref name="pageIndex"/> is zero-based.
        /// </param>
        /// <param name="pageSize">
        ///     The size of the page of results to return.
        /// </param>
        /// <param name="totalRecords">
        ///     The total number of matched users.
        /// </param>
        /// <returns>
        ///     A <see cref="T:System.Web.Security.MembershipUserCollection"/> 
        ///     collection that contains a page of <paramref name="pageSize"/>
        ///     <see cref="T:System.Web.Security.MembershipUser"/> objects 
        ///     beginning at the page specified by <paramref name="pageIndex"/>.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/17/2009" version="1.1.3.5">
        ///     Added documentation header
        /// </revision>
        public override MembershipUserCollection FindUsersByEmail(
            string emailToMatch, 
            int pageIndex, 
            int pageSize, 
            out int totalRecords)
        {
            throw new NotImplementedException(
                "The method or operation is not implemented.");
        }

        /// <summary>
        ///     Gets a collection of membership users where the user name 
        ///     contains the specified user name to match.
        /// </summary>
        /// <param name="usernameToMatch">The user name to search for.</param>
        /// <param name="pageIndex">
        ///     The index of the page of results to return. 
        ///     <paramref name="pageIndex"/> is zero-based.
        /// </param>
        /// <param name="pageSize">
        ///     The size of the page of results to return.
        /// </param>
        /// <param name="totalRecords">
        ///     The total number of matched users.
        /// </param>
        /// <returns>
        ///     A <see cref="T:System.Web.Security.MembershipUserCollection"/> 
        ///     collection that contains a page of <paramref name="pageSize"/>
        ///     <see cref="T:System.Web.Security.MembershipUser"/> objects 
        ///     beginning at the page specified by <paramref name="pageIndex"/>.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/17/2009" version="1.1.3.5">
        ///     Added documentation header
        /// </revision>
        public override MembershipUserCollection FindUsersByName(
            string usernameToMatch, 
            int pageIndex, 
            int pageSize, 
            out int totalRecords)
        {
            throw new NotImplementedException(
                "The method or operation is not implemented.");
        }

        /// <summary>
        ///     Gets a collection of all the users in the data source in 
        ///     pages of data.
        /// </summary>
        /// <param name="pageIndex">
        ///     The index of the page of results to return. 
        ///     <paramref name="pageIndex"/> is zero-based.
        /// </param>
        /// <param name="pageSize">
        ///     The size of the page of results to return.
        /// </param>
        /// <param name="totalRecords">
        ///     The total number of matched users.
        /// </param>
        /// <returns>
        ///     A <see cref="T:System.Web.Security.MembershipUserCollection"/> 
        ///     collection that contains a page of <paramref name="pageSize"/>
        ///     <see cref="T:System.Web.Security.MembershipUser"/> objects 
        ///     beginning at the page specified by <paramref name="pageIndex"/>.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/17/2009" version="1.1.3.5">
        ///     Added documentation header
        /// </revision>
        public override MembershipUserCollection GetAllUsers(
            int pageIndex, 
            int pageSize, 
            out int totalRecords)
        {
            throw new NotImplementedException(
                "The method or operation is not implemented.");
        }

        /// <summary>
        ///     Gets the number of users currently accessing the application.
        /// </summary>
        /// <returns>
        ///     The number of users currently accessing the application.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/17/2009" version="1.1.3.5">
        ///     Added documentation header
        /// </revision>
        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException(
                "The method or operation is not implemented.");
        }

        /// <summary>
        ///     Gets the password for the specified user name from the 
        ///     data source.
        /// </summary>
        /// <param name="username">
        ///     The user to retrieve the password for.
        /// </param>
        /// <param name="answer">The password answer for the user.</param>
        /// <returns>
        ///     The password for the specified user name.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/17/2009" version="1.1.3.5">
        ///     Added documentation header
        /// </revision>
        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException(
                "The method or operation is not implemented.");
        }

        /// <summary>
        ///     Gets information from the data source for a user. 
        ///     Provides an option to update the last-activity date/time 
        ///     stamp for the user.
        /// </summary>
        /// <param name="username">
        ///     The name of the user to get information for.
        /// </param>
        /// <param name="userIsOnline">
        ///     <c>true</c> to update the last-activity date/time stamp for 
        ///     the user; <c>false</c> to return user information without 
        ///     updating the last-activity date/time stamp for the user.
        /// </param>
        /// <returns>
        ///     A <see cref="T:System.Web.Security.MembershipUser"/> object 
        ///     populated with the specified user's information from 
        ///     the data source.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/17/2009" version="1.1.3.5">
        ///     Added documentation header
        /// </revision>
        public override MembershipUser GetUser(
            string username, 
            bool userIsOnline)
        {
            throw new NotImplementedException(
                "The method or operation is not implemented.");
        }

        /// <summary>
        ///     Gets user information from the data source based on the unique 
        ///     identifier for the membership user. Provides an option to update 
        ///     the last-activity date/time stamp for the user.
        /// </summary>
        /// <param name="providerUserKey">
        ///     The unique identifier for the membership user to get 
        ///     information for.
        /// </param>
        /// <param name="userIsOnline">
        ///     <c>true</c> to update the last-activity date/time stamp for 
        ///     the user; <c>false</c> to return user information without 
        ///     updating the last-activity date/time stamp for the user.
        /// </param>
        /// <returns>
        ///     A <see cref="T:System.Web.Security.MembershipUser"/> object 
        ///     populated with the specified user's information from 
        ///     the data source.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/17/2009" version="1.1.3.5">
        ///     Added documentation header
        /// </revision>
        public override MembershipUser GetUser(
            object providerUserKey, 
            bool userIsOnline)
        {
            throw new NotImplementedException(
                "The method or operation is not implemented.");
        }

        /// <summary>
        ///     Gets the user name associated with the specified e-mail address.
        /// </summary>
        /// <param name="email">The e-mail address to search for.</param>
        /// <returns>
        ///     The user name associated with the specified e-mail address. 
        ///     If no match is found, return null.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/17/2009" version="1.1.3.5">
        ///     Added documentation header
        /// </revision>
        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException(
                "The method or operation is not implemented.");
        }

        /// <summary>
        ///     Resets a user's password to a new, automatically 
        ///     generated password.
        /// </summary>
        /// <param name="username">The user to reset the password for.</param>
        /// <param name="answer">
        ///     The password answer for the specified user.
        /// </param>
        /// <returns>
        ///     The new password for the specified user.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/17/2009" version="1.1.3.5">
        ///     Added documentation header
        /// </revision>
        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException(
                "The method or operation is not implemented.");
        }

        /// <summary>
        ///     Clears a lock so that the membership user can be validated.
        /// </summary>
        /// <param name="userName">
        ///     The membership user whose lock status you want to clear.
        /// </param>
        /// <returns>
        ///     <c>true</c> if the membership user was successfully unlocked; 
        ///     otherwise, <c>false</c>.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/17/2009" version="1.1.3.5">
        ///     Added documentation header
        /// </revision>
        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException(
                "The method or operation is not implemented.");
        }

        /// <summary>
        ///     Updates information about a user in the data source.
        /// </summary>
        /// <param name="user">
        ///     A <see cref="T:System.Web.Security.MembershipUser"/> object that 
        ///     represents the user to update and the updated information 
        ///     for the user.
        /// </param>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/17/2009" version="1.1.3.5">
        ///     Added documentation header
        /// </revision>
        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException(
                "The method or operation is not implemented.");
        }

        #endregion

        /// <summary>
        ///     Verifies that the specified user name and password exist 
        ///     in the data source.
        /// </summary>
        /// <param name="username">The name of the user to validate.</param>
        /// <param name="password">The password for the specified user.</param>
        /// <returns>
        ///     true if the specified username and password are valid; 
        ///     otherwise, false.
        /// </returns>
        /// <externalUnit cref="Contract"/>
        /// <externalUnit cref="DataManager"/>
        /// <externalUnit cref="FixedServerRoles"/>
        /// <externalUnit cref="ServerConnection"/>
        /// <exception cref="DesignByContractException">
        ///     Failed contracts will cause exception to be thrown.  Handled by 
        ///     caller or framework.
        /// </exception>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public override bool ValidateUser(string username, string password)
        {
            var message = "DataServiceDefinition cannot be null - " +
                          "credentials not being captured correctly.";
            
            // make sure the DataServiceDefinition Exists
            Contract.Require(
                DataManager.Instance.DataServiceDefinition != null,
               message);
            
            // create param to see if authentication succeeds
            bool areCredentialsValid = false;

            // create a new server connection
            ServerConnection serverConn = new ServerConnection();
          
            // Fill in necessary information
            // set the server name
            serverConn.ServerInstance =
                DataManager.Instance.DataServiceDefinition.ServerName;

            // Set connection timeout
            serverConn.ConnectTimeout = 30;

            // Use SQL Server authentication
            serverConn.LoginSecure = false;

            // check to see if we are trying to log into the master database
            bool useMasterAsSysAdmin =
                DataManager.Instance.DataServiceDefinition.UseMasterDatabase;

            // check to see what the profile db name is
            string profileName =
                DataManager.Instance.DataServiceDefinition.ProfileDatabaseName;

            // get the current user name
            string currentUsername =
                DataManager.Instance.DataServiceDefinition.Username;

            // get the current password
            string currentPassword =
                    DataManager.Instance.DataServiceDefinition.Password;

            // if we are not trying to log into the master db
            if (useMasterAsSysAdmin == false)
            {
                //// create new security helper
                // SecurityHelper securityHelper = new SecurityHelper();
                //// hash the username
                // DataManager.Instance.DataServiceDefinition.SecureUsername
                //    =
                //    securityHelper.CreateHashWithSalt(
                //        currentUsername, profileName);
               //// hash the password
                // DataManager.Instance.DataServiceDefinition.SecurePassword =
                //    securityHelper.CreateHashWithSalt(
                //        currentPassword, profileName);
            }
            else
            {
                // use the current user name as is
                DataManager.Instance.DataServiceDefinition.SecureUsername =
                    currentUsername;

                // use the current password as is
                DataManager.Instance.DataServiceDefinition.SecurePassword =
                    currentPassword;
            }

            // set the user name
            serverConn.Login =
                DataManager.Instance.DataServiceDefinition.SecureUsername;

            // set the password
            serverConn.Password =
                DataManager.Instance.DataServiceDefinition.SecurePassword;

            // set the db to connect to
            serverConn.DatabaseName =
                DataManager.Instance.DataServiceDefinition.ElectionDatabaseName;

            try
            {
                // Go ahead and connect
                serverConn.Connect();

                // if connected, check to make sure the conencted user is part 
                // of the sysadmin role
                if (useMasterAsSysAdmin == true 
                  && serverConn.IsInFixedServerRole(FixedServerRoles.SysAdmin)
                    == false)
                {
                    // user is not part of the sysadmin role
                    // force a disconnect
                    serverConn.Disconnect();
                }
            }
            catch (ConnectionFailureException)
            {
                // an exception will be thrown if the user is not logged in.
                // sql doesn't return a nice message saying the login failed, 
                // it throws an exception.  

                // this message is relayed to the user saying they were 
                // not successful (via the return param) and the invalid attempt 
                // is logged in the local application event log.
                // we do nothing with it here, as it serves its sole purpose in 
                // telling us that the attempt failed, and it has the hashed 
                // username, which we don't want to report.
            }
            finally
            {
                // set whether or not the connection worked
                areCredentialsValid = serverConn.IsOpen;

                // disconnect from the connection, the credentials were good, 
                // we'll use them later
                serverConn.Disconnect();
            }

            // return whether or not the credentials worked
            return areCredentialsValid;
        }

        #endregion
    }
}
