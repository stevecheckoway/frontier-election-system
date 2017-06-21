// -----------------------------------------------------------------------------
// <copyright file="UserDataService.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2010 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the UserDataService class.
// </summary>
// <revision revisor="dev14" date="1/25/2010" version="1.1.5.22">
//     File Created
// </revision>
// <revision revisor="dev14" date="1/29/2010" version="1.1.6.01">
//     File Modified
// </revision>   
// -----------------------------------------------------------------------------

namespace Sequoia.EMS.Core.DataServices
{
    #region Using directives

    using System;
    using System.Data.SqlClient;
    using System.Web.Security;

    #endregion

    /// <summary>
    ///	    UserDataService gets membership data from the user databases
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev14" date="1/25/2010" version="1.1.5.22">
    ///     Class created.
    /// </revision>
    /// <revision revisor="dev22" date="03/08/2010" version="1.1.7.15">
    /// Removed references to deleted base method(s) that took a boolean
    /// parameter that would hide exceptions.
    /// </revision>
    public class UserDataService : BaseDataService, IUserDataService
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="UserDataService"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="1/25/2010" version="1.1.5.22">
        ///     Member Created
        /// </revision>	
        public UserDataService()
        {
        }

        #endregion

        #region Public Properties

        #endregion

        #region Public Methods

        /// <summary>
        ///     Gets all application users.
        /// </summary>
        /// <param name="providerName">Name of the provider.</param>
        /// <param name="pageIndex">Index of the page of users to return</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalRecords">The total records.</param>
        /// <returns>The collection of users</returns>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="1/25/2010" version="1.1.5.22">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev14" date="1/29/2010" version="1.1.6.01">
        ///     Added Comments
        /// </revision>
        public MembershipUserCollection GetAllUsers(
            string providerName,
            int pageIndex, 
            int pageSize, 
            out int totalRecords)
        {
            // Set the profile (user) database as current database
            this.SetTargetDatabaseAsProfile();

            // initialize a collection of Membership users
            var users = new MembershipUserCollection();
            
            // initialize the number of records found to 0
            totalRecords = 0;

            // get a SqlDataReader and pass GetAllUsers proc
            using (var reader = (SqlDataReader)GetReader(
                "aspnet_Membership_GetAllUsers", "/", pageIndex, pageSize))
            {
                // Get the column indices to read values from reader
                int usernameIndex = reader.GetOrdinal("UserName");
                int emailIndex = reader.GetOrdinal("Email");
                int questionIndex = reader.GetOrdinal("PasswordQuestion");
                int commentIndex = reader.GetOrdinal("Comment");
                int approvedIndex = reader.GetOrdinal("IsApproved");
                int createIndex = reader.GetOrdinal("CreateDate");
                int lastLoginIndex = reader.GetOrdinal("LastLoginDate");
                int lastActiveIndex = reader.GetOrdinal("LastActivityDate");
                int lastPasswordIndex = reader.GetOrdinal("LastPasswordChangedDate");
                int userIdIndex = reader.GetOrdinal("UserId");
                int isLockedIndex = reader.GetOrdinal("IsLockedOut");
                int lastLockedIndex = reader.GetOrdinal("LastLockoutDate");

                // iterate through reader data
                while (reader.Read())
                {
                    // get the user's username
                    string username = 
                        this.GetNullableString(reader, usernameIndex);
                    
                    // get the user's email address
                    string email = this.GetNullableString(reader, emailIndex);
                    
                    // get the question to ask the user if they forget their pwd
                    string passwordQuestion = 
                        this.GetNullableString(reader, questionIndex);
                    
                    // get the application-specific info about the user
                    string comment = this.GetNullableString(reader, commentIndex);

                    // get whether user is approved to use application
                    bool isApproved = reader.GetBoolean(approvedIndex);

                    // get date user was added to membership data store
                    DateTime dtCreate = 
                        reader.GetDateTime(createIndex).ToLocalTime();

                    // get data and time the user was last authenticated
                    DateTime dtLastLogin = 
                        reader.GetDateTime(lastLoginIndex).ToLocalTime();

                    // get date and time user last authenticated or accessed the
                    // application
                    DateTime dtLastActivity = 
                        reader.GetDateTime(lastActiveIndex).ToLocalTime();
                    
                    // get date and time the user last changed their password
                    DateTime dtLastPassChange = 
                        reader.GetDateTime(lastPasswordIndex).ToLocalTime();

                    // get user's unique identifier
                    Guid userId = reader.GetGuid(userIdIndex);

                    // get whether user is locked out because of invalid 
                    // validation attempts
                    bool isLockedOut = reader.GetBoolean(isLockedIndex);

                    // get date time user was last locked out
                    DateTime dtLastLockoutDate = 
                        reader.GetDateTime(lastLockedIndex).ToLocalTime();

                    // create the user object
                    var newUser = new MembershipUser(
                                        providerName,
                                        username,
                                        userId,
                                        email,
                                        passwordQuestion,
                                        comment,
                                        isApproved,
                                        isLockedOut,
                                        dtCreate,
                                        dtLastLogin,
                                        dtLastActivity,
                                        dtLastPassChange,
                                        dtLastLockoutDate);
                    
                    // add the user to our collection of all users
                    users.Add(newUser);
                }
            }

            // return the collection of users
            return users;
        }

        /// <summary>
        ///     Sets the data definition, which has the database connection
        ///     credentials.
        /// </summary>
        /// <param name="definition">The data definition.</param>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="1/25/2010" version="1.1.5.22">
        ///     Member Created
        /// </revision>
        public void SetDefinition(DataServiceDefinition definition)
        {
            // set local reference to the data definition
            this.DataServiceDefinition = definition;
        }

        #endregion

        #region Public Events

        #endregion

        #region Fields

        #endregion

        #region Private Methods

        /// <summary>
        ///     Gets the value of a column, which can be either a string or NULL
        /// </summary>
        /// <param name="reader">The data reader.</param>
        /// <param name="columnIndex">The index of the column to read.</param>
        /// <returns>The string value of the column, or NULL</returns>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="1/25/2010" version="1.1.3.04">
        ///     Member Created
        /// </revision>
        private string GetNullableString(SqlDataReader reader, int columnIndex)
        {
            // intialize return value to null
            string columnValue = null;

            // check that the gi9ven column has a value
            if (reader.IsDBNull(columnIndex) == false)
            {
                // set the return value to the non-null value of the column read
                columnValue = reader.GetString(columnIndex);
            }

            // return the value of the column
            return columnValue;
        }

        #endregion
    }
}
