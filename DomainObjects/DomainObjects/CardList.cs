// -----------------------------------------------------------------------------
// <copyright file="CardList.cs" company="Sequoia Voting Systems">
//     Copyright (c) 2008 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the CardList class.
// </summary>
// <revision revisor = "dev05" date="12/19/08" version="1.0.0.0">
//     Implement IPersistible.
// </revision>
// <revision revisor="dev13" date="12/12/2008" version="1.0.0.0">
//     File Created
// </revision>
// <revision revisor="dev16" date="12/22/2008" version="1.0.0.0">
//     File Modified
// </revision>
// -----------------------------------------------------------------------------

namespace Sequoia.DomainObjects
{
    #region Using directives

    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    using Persistence;

    #endregion

    /// <summary>
    ///     CardList is a <see cref="List{T}" /> of <see cref="Card" /> objects.
    /// </summary>
    /// <externalUnit cref="CardList"/>
    /// <revision revisor="dev13" date="12/12/200" version="1.0.0.0">
    ///     Class created.
    /// </revision>
    /// <revision revisor="dev16" date="12/22/2008" version="1.0.0.0">
    ///     Modified class to inherit from DomainObjectList. 
    ///     Removed ToString() override method
    /// </revision>
    [Serializable]
    [XmlRoot("Cards")]
    public class CardList : DomainObjectList<Card>, IPersistible
    {
        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="CardList"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="12/12/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public CardList()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="CardList"/> class.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        /// <externalUnit cref="List{T}(int)"/>
        /// <revision revisor="dev13" date="12/12/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public CardList(int capacity)
            : base(capacity)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="CardList"/> class.
        /// </summary>
        /// <param name="cards">The cards.</param>
        /// <externalUnit cref="CardList"/>
        /// <externalUnit cref="List{T}(IEnumerable{T})"/>
        /// <externalUnit cref="IEnumerable{T}"/>
        /// <revision revisor="dev13" date="12/12/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public CardList(IEnumerable<Card> cards)
            : base(cards)
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

        #region IPersistible Members

        /// <summary>
        ///     Gets the serialized data.
        /// </summary>
        /// <value>The serialized data.</value>
        /// <externalUnit/>
        /// <revision revisor="dev05" date="12/19/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev16" date="12/22/2008" version="1.0.0.0">
        ///     modified to use domain object list Serialize method
        /// </revision>
        public string SerializedData
        {
            get
            {
                return Serialize(this);
            }
        }

        /// <summary>
        ///     Sets the persister.
        /// </summary>
        /// <param name="persister">The persister.</param>
        /// <externalUnit/>
        /// <revision revisor="dev05" date="12/19/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public void SetPersister(IPersister persister)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
