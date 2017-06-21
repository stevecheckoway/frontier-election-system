// -----------------------------------------------------------------------------
// <copyright file="IPersister.cs" company="Sequoia Voting Systems">
//     Copyright (c) 2008 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the IPersister interface.
// </summary>
// <revision revisor="dev06" date="11/12/2008" version="1.0.0.0">
//     File Created
// </revision>
// <revision revisor="dev05" date="9/9/2009" version="1.0.17.2">
//     File modified.
// </revision>
// <revision revisor="dev13" date="11/25/2009" version="1.1.3.12">
//     File modified.
// </revision>
// -----------------------------------------------------------------------------

namespace Sequoia.DomainObjects.Persistence
{
    #region Using directives

    using System;

    #endregion

    /// <summary>
    ///     IPersister is an interface defining a contract for persisters
    /// </summary>
    /// <revision revisor="dev06" date="11/12/2008" version="1.0.0.0">
    ///     Interface created.
    /// </revision>
    public interface IPersister
    {
        #region Properties

        #endregion

        #region Methods

        /// <summary>
        ///     Persists the specified item.
        /// </summary>
        /// <typeparam name="T">The type of object to persist.</typeparam>
        /// <param name="item">The item to persist.</param>
        /// <returns>
        ///     An <see cref="OperationResult" /> containing the results of 
        ///     the operation.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/25/2009" version="1.1.3.12">
        ///     Added documentation header
        /// </revision>
        OperationResult Persist<T>(T item) where T : class, IPersistible;

        /// <summary>
        ///     Recreates this instance.
        /// </summary>
        /// <typeparam name="T">The type of the object to recreate.</typeparam>
        /// <returns>
        ///     The object.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/25/2009" version="1.1.3.8">
        ///     Added documentation header
        /// </revision>
        T Recreate<T>() where T : class, IPersistible;

        #endregion

        #region Events

        #endregion
    }
}
