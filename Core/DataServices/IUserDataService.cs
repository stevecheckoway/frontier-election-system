// -----------------------------------------------------------------------------
// <copyright file="IUserDataService.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2010 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the IUserDataService interface.
// </summary>
// <revision revisor="dev14" date="1/25/2010" version="1.1.5.22">
//     File Created
// </revision>
// -----------------------------------------------------------------------------

namespace Sequoia.EMS.Core.DataServices
{
    using System.Web.Security;

    /// <summary>
    ///	    IUserDataService is the interface between a custom membership
    ///     provider and the data service that gets the membership data
    /// </summary>
    /// <revision revisor="dev14" date="1/25/2010" version="1.1.5.22">
    ///     Interface created.
    /// </revision>
    public interface IUserDataService : IDataService
    {
        #region Properties

        #endregion

        #region Methods

        /// <summary>
        /// Gets all application users.
        /// </summary>
        /// <param name="providerName">Name of the provider.</param>
        /// <param name="pageIndex">Index of the page of users to return</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalRecords">The total records.</param>
        /// <returns>The collection of users</returns>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="1/25/2010" version="1.1.5.22">
        /// Member Created
        /// </revision>
        MembershipUserCollection GetAllUsers(
            string providerName, 
            int pageIndex, 
            int pageSize, 
            out int totalRecords);

        /// <summary>
        ///     Sets the data definition, which has the database connection
        ///     credentials.
        /// </summary>
        /// <param name="definition">The data definition.</param>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="1/25/2010" version="1.1.5.22">
        ///     Member Created
        /// </revision>
        void SetDefinition(DataServiceDefinition definition);

        #endregion

        #region Events

        #endregion
    }
}
