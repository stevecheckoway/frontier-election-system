// -----------------------------------------------------------------------------
// <copyright file="TabulatorUserList.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the TabulatorUserList class.
// </summary>
// <revision revisor="dev13" date="4/17/2009" version="1.0.11.07">
//     File Created
// </revision>  
// -----------------------------------------------------------------------------

namespace Sequoia.DomainObjects
{
    #region Using directives

    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    #endregion

    /// <summary>
    ///     TabulatorUserList is a <see cref="List{T}" /> of <see cref="TabulatorUser" /> objects. 
    /// </summary>
    /// <externalUnit cref="TabulatorUser"/>
    /// <revision revisor="dev13" date="4/17/2009" version="1.0.11.07">
    ///     Class created.
    /// </revision>
    [Serializable]
    [XmlRoot("TabulatorUser")]
    public class TabulatorUserList : DomainObjectList<TabulatorUser>
    {
        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="TabulatorUserList"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="4/17/2009" version="1.0.11.07">
        ///     Member Created
        /// </revision> 
        public TabulatorUserList()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="TabulatorUserList"/> class.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        /// <externalUnit cref="List{T}(int)"/>
        /// <revision revisor="dev13" date="4/17/2009" version="1.0.11.07">
        ///     Member Created
        /// </revision>
        public TabulatorUserList(int capacity)
            : base(capacity)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="TabulatorUserList"/> class.
        /// </summary>
        /// <param name="tabulatorUsers">The tabulator users.</param>
        /// <externalUnit cref="TabulatorUser"/>
        /// <externalUnit cref="List{T}(IEnumerable{T})"/>
        /// <externalUnit cref="IEnumerable{T}"/>
        /// <revision revisor="dev13" date="4/17/2009" version="1.0.11.07">
        ///     Member Created
        /// </revision>
        public TabulatorUserList(IEnumerable<TabulatorUser> tabulatorUsers)
            : base(tabulatorUsers)
        {
        }

        #endregion

        #region Public Properties

        #endregion

        #region Public Methods

        #endregion

        #region Public Events

        #endregion

        #region Private Methods

        #endregion
    }
}
