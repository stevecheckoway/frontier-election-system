// -----------------------------------------------------------------------------
// <copyright file="IDataAccessObject.cs" company="Sequoia Voting Systems">
//     Copyright (c) 2008 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the IDataAccessObject interface.
// </summary>
// <revision revisor="dev06" date="12/12/2008 3:14:22 PM" version="1.0.?.0">
//     File Created
// </revision>
// <revision revisor="dev05" date="03/19/09" version="1.0.9.3">
//     File modified.
// </revision>
// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
//     File modified.
// </revision>
// -----------------------------------------------------------------------------

namespace Sequoia.Utiltities.Persistence
{
    #region Using directives

    using System;

    using Sequoia.DomainObjects;

    #endregion

    /// <summary>
    ///     IDataAccessObject is a contract for classes which want to implement
    ///     data access object functionality.
    /// </summary>
    /// <typeparam name="T">The type of the object.</typeparam>
    /// <revision revisor="dev06" date="12/12/2008" version="1.0.0.0">
    ///     Interface created.
    /// </revision>
    /// <revision revisor="dev06" date="12/12/2008" version="1.0.0.0">
    ///     Formatting changes.
    /// </revision>
    public interface IDataAccessObject<T>
    {
        #region Properties

        #endregion

        #region Methods

        /// <summary>
        ///     Saves the specified item.
        /// </summary>
        /// <param name="item">The item to save.</param>
        /// <returns>
        ///     An <see cref="OperationResult" /> containing the results
        ///     of the operation.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        OperationResult Save(T item);

        #endregion

        #region Events

        #endregion
    }
}
