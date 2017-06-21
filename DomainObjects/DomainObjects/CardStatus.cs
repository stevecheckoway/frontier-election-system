// -----------------------------------------------------------------------------
// <copyright file="CardStatus.cs" company="Sequoia Voting Systems">
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     CardStatus class implementation.
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
    using System.Xml.Serialization;

    #endregion

    /// <summary>
    ///     Card status for a voter session. This consists of a card ID and a
    ///     session status.
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev05" date="01/17/09" version="1.0.4.18">
    ///     Class created.
    /// </revision>
    [Serializable]
    [XmlRoot("CardStatus")]
    public class CardStatus : DomainObject
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="CardStatus"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev16" date="12/22/2008" version="1.0.?.0">
        ///     Member Created
        /// </revision>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="10/7/2009" version="1.1.1.7">
        ///     Member Created
        /// </revision>
        public CardStatus()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="CardStatus"/> class.
        /// </summary>
        /// <param name="cardId">The card id.</param>
        /// <param name="sessionStatus">The session status.</param>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="10/7/2009" version="1.1.1.7">
        ///     Member Created
        /// </revision>
        public CardStatus(int cardId, int sessionStatus)
        {
            this.CardId = cardId;
            this.SessionStatus = sessionStatus;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the card id.
        /// </summary>
        /// <value>The card id.</value>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="10/7/2009" version="1.1.1.7">
        ///     Member Created
        /// </revision>
        [XmlAttribute("CardId")]
        public int CardId
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the session status.
        /// </summary>
        /// <value>The session status.</value>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="10/7/2009" version="1.1.1.7">
        ///     Member Created
        /// </revision>
        [XmlAttribute("SessionStatus")]
        public int SessionStatus
        {
            get;
            set;
        }

        #endregion
    }
}
