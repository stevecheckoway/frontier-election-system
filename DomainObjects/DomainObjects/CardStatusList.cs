// -----------------------------------------------------------------------------
// <copyright file="CardStatusList.cs" company="Sequoia Voting Systems">
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     CardStatusList class implementation.
// </summary>
// <revision revisor="dev05" date="01/17/09" version="1.0.4.18">
//     File Created
// </revision>
// <revision revisor="dev05" date="01/17/09" version="1.0.5.14">
//     Moved from voter activation to domain objects
// </revision>
// -----------------------------------------------------------------------------
namespace Sequoia.DomainObjects
{
    #region Using directives

    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    using Sequoia.DomainObjects.Persistence;

    #endregion

    /// <summary>
    ///     A list of CardStatus objects.
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev05" date="01/17/09" version="1.0.4.18">
    ///     Class created.
    /// </revision>
    [Serializable]
    [XmlRoot("CardStatuses")]
    public class CardStatusList : DomainObjectList<CardStatus>, IPersistible
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="CardStatusList"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor = "dev05" date="01/17/09" version="1.0.4.18">
        ///     Member created.
        /// </revision>
        public CardStatusList()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="CardStatusList"/> class.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        /// <externalUnit cref="List{T}(int)"/>
        /// <revision revisor = "dev05" date="01/17/09" version="1.0.4.18">
        ///     Member created.
        /// </revision>
        public CardStatusList(int capacity)
            : base(capacity)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="CardStatusList"/> class.
        /// </summary>
        /// <param name="cardStatuses">The card status objects.</param>
        /// <externalUnit cref="CardStatusList"/>
        /// <externalUnit cref="List{T}(IEnumerable{T})"/>
        /// <externalUnit cref="IEnumerable{T}"/>
        /// <revision revisor = "dev05" date="01/17/09" version="1.0.4.18">
        ///     Member created.
        /// </revision>
        public CardStatusList(IEnumerable<CardStatus> cardStatuses)
            : base(cardStatuses)
        {
        }

        #endregion

        #region IPersistible Members

        /// <summary>
        ///     Gets the serialized data.
        /// </summary>
        /// <value>The serialized data.</value>
        /// <externalUnit/>
        /// <revision revisor = "dev05" date="01/17/09" version="1.0.4.18">
        ///     Member created.
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
        /// <revision revisor = "dev05" date="01/17/09" version="1.0.4.18">
        ///     Member created.
        /// </revision>
        public void SetPersister(IPersister persister)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
