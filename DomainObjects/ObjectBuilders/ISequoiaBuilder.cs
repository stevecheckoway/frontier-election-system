// -----------------------------------------------------------------------------
// <copyright file="ISequoiaBuilder.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the ISequoiaBuilder interface.
// </summary>
// <revision revisor="dev14" date="9/24/2009" version="1.0.17.16">
//     File Created
// </revision>
// -----------------------------------------------------------------------------

namespace Sequoia.DomainObjects.ObjectBuilders
{
    #region Using directives

    using System;

    #endregion

    /// <summary>
    ///     ISequoiaBuilder is an interface used to define a contract between a
    ///     domain object builder and its consumer
    /// </summary>
    /// <typeparam name="T">The type of DomainObject to be built</typeparam>
    /// <revision revisor="dev14" date="9/24/2009" version="1.0.17.16">
    /// Interface created.
    /// </revision>
    public interface ISequoiaBuilder<T> where T : DomainObject
    {
        #region Methods

        /// <summary>
        ///     Builds an instance of a <typeparamref name="T" />.
        /// </summary>
        /// <returns>The Domain Object</returns>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="9/24/2009" version="1.0.17.16">
        ///     Member Created
        /// </revision>
        T Build();

        #endregion

        #region Events

        #endregion
    }
}
