// -----------------------------------------------------------------------------
// <copyright file="NullPersister.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the NullPersister class.
// </summary>
// <revision revisor="dev06" date="1/30/2009" version="1.0.5.11">
//     File Created
// </revision>
// <revision revisor="dev05" date="03/19/09" version="1.0.9.3">
//     File modified.
// </revision>
// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
//     File modified.
// </revision>
// -----------------------------------------------------------------------------

namespace Sequoia.Utilities.Persistence
{
    using System;

    using Sequoia.DomainObjects;
    using Sequoia.DomainObjects.Persistence;

    /// <summary>
    ///	    NullPersister is a class that is used by persistible items when 
    ///     they don't need to be persisted. i.e. If you want to load in memory 
    ///     a cartridge container, do some investigation, but don't need the 
    ///     actual persistence functionality of the container.
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev06" date="1/30/2009" version="1.0.5.11">
    ///     Class created.
    /// </revision>
    public class NullPersister : IPersister
    {
        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="NullPersister"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev06" date="1/30/2009" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public NullPersister()
        {
        }

        #endregion

        #region Public Properties

        #endregion

        #region Public Methods

        /// <summary>
        ///     Persists the specified item.
        /// </summary>
        /// <typeparam name="T">The type of item to persist.</typeparam>
        /// <param name="item">The item to persist.</param>
        /// <returns>
        ///     An <see cref="OperationResult"/> specifying true in this case, 
        ///     as there is nothing to persist.
        /// </returns>
        /// <externalUnit cref="IPersistible"/>
        /// <externalUnit cref="OperationResult"/>
        /// <revision revisor="dev06" date="1/30/2009" version="1.0.5.11">
        ///     Member Created
        /// </revision>
        public OperationResult Persist<T>(T item) where T : class, IPersistible
        {
            return new OperationResult(true);
        }

        /// <summary>
        ///     Recreates the default instance of the type using the persister.
        /// </summary>
        /// <typeparam name="T">The type of item to persist.</typeparam>
        /// <returns>
        ///     A default instance of the specified type as there is nothing 
        ///     to recreate.
        /// </returns>
        /// <externalUnit cref="IPersistible"/>
        /// <revision revisor="dev06" date="1/30/2009" version="1.0.5.11">
        ///     Member Created
        /// </revision>
        public T Recreate<T>() where T : class, IPersistible
        {
            return default(T);
        }

        #endregion

        #region Public Events

        #endregion

        #region Private Methods

        #endregion
    }
}
