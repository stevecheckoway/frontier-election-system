// -----------------------------------------------------------------------------
// <copyright file="IPersistible.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2008 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the IPersistible interface.
// </summary>
// <revision revisor="dev06" date="12/12/2008" version="1.0.0.0">
//     File Created
// </revision>
// <revision revisor="dev13" date="11/25/2009" version="1.0.0.0">
//     File modified
// </revision>
// -----------------------------------------------------------------------------

namespace Sequoia.DomainObjects.Persistence
{
    #region Using directives

    using System;

    #endregion

    /// <summary>
    ///     IPersistible is an interface containing a contract for 
    ///     persistible classes.
    /// </summary>
    /// <revision revisor="dev06" date="12/12/2008" version="1.0.0.0">
    ///     Interface created.
    /// </revision>
    public interface IPersistible
    {
        #region Properties

        /// <summary>
        ///     Gets the serialized data.
        /// </summary>
        /// <value>The serialized data.</value>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/25/2009" version="1.1.3.12">
        ///     Added documentation header
        /// </revision>
        string SerializedData
        {
            get;
        }
        #endregion

        #region Methods

        /// <summary>
        ///     Sets the persister.
        /// </summary>
        /// <param name="persister">The persister.</param>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/25/2009" version="1.1.3.12">
        ///     Added documentation header
        /// </revision>
        void SetPersister(IPersister persister);

        #endregion

        #region Events

        #endregion
    }
}
