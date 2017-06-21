// -----------------------------------------------------------------------------
// <copyright file="party.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the party class.
// </summary>
// <revision revisor="dev01" date="8/17/2009" version="1.0.15.9">
//     File Created
// </revision>
// <revision revisor="dev14" date="9/29/2009" version="1.0.17.21">
//     File Modified
// </revision>
// <revision revisor="dev13" date="10/29/2009" version="1.1.2.7">
//     File Modified
// </revision>
// <revision revisor="dev01" date="11/16/2009" version="1.1.3.4">
//     file Modified
// </revision>
// -----------------------------------------------------------------------------

namespace Sequoia.DomainObjects
{
    #region Using directives

    using System;
    using System.Xml.Serialization;

    #endregion

    /// <summary>
    /// party represents a party in an election definition    
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev01" date="8/17/2009" version="1.0.15.9">
    ///     Class created.
    /// </revision>
    /// <revision revisor="dev01" date="11/16/2009" version="1.1.3.4">
    ///     Added non-partisan attribute
    /// </revision>
    [Serializable]
    [XmlRoot("Party")]
    public class Party : DomainObject
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Party"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="8/17/2009" version="1.0.15.9">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev01" date="11/16/2009" version="1.1.3.4">
        ///     change call to parameterized constructor  
        /// </revision>
        public Party() : this(0, String.Empty, string.Empty, 1, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Party"/> class.
        /// </summary>
        /// <param name="id">
        /// The Party id.
        /// </param>
        /// <param name="name">
        /// The Party name.
        /// </param>
        /// <param name="abbreviation">
        /// The abbreviation.
        /// </param>
        /// <param name="listOrder">
        /// The list Order.
        /// </param>
        /// <param name="isNonPartisan">
        /// The is Non Partisan.
        /// </param>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="8/17/2009 7:49:51 AM" version="1.0.15.9">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev01" date="11/16/2009" version="1.1.3.4">
        ///     Added nonpartisan parameter
        /// </revision>
        public Party(int id, string name, string abbreviation, int listOrder, bool isNonPartisan)
        {
            this.Id = id;
            this.Name = name;
            this.Abbreviation = abbreviation;
            this.ListOrder = listOrder;
            this.IsNonPartisan = isNonPartisan;
           }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the party id.
        /// </summary>
        /// <value>The party id.</value>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="8/17/2009" version="1.0.15.9">
        ///     Member Created
        /// </revision>
        [XmlAttribute("Id")]
        public int Id
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the party name.
        /// </summary>
        /// <value>The party name.</value>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="8/17/2009" version="1.0.15.9">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev14" date="8/28/2009" version="1.0.16.08">
        ///     Moved validation message to Resources
        /// </revision>
        /// <revision revisor="dev14" date="9/29/2009" version="1.0.17.21">
        ///     Removed validation attribute to move to Builder object
        /// </revision>
        [XmlAttribute("Name")]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the party abbreviation.
        /// </summary>
        /// <value>The party abbreviation</value>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="8/17/2009" version="1.0.15.9">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev14" date="9/29/2009" version="1.0.17.21">
        ///     Removed validation attribute to move to Builder object
        /// </revision>
        [XmlAttribute("Abbreviation")]
        public string Abbreviation
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the list order.
        /// </summary>
        /// <value>The list order.</value>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="8/17/2009" version="1.0.15.9">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev14" date="9/29/2009" version="1.0.17.21">
        ///     Removed validation attribute to move to Builder object
        /// </revision>
        [XmlAttribute("ListOrder")]
        public int ListOrder
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is non partisan.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is non partisan; otherwise, <c>false</c>.
        /// </value>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="11/16/2009" version="1.1.3.4">
        ///     Member Created
        /// </revision>
        [XmlAttribute("IsNonPartisan")]
        public bool IsNonPartisan
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the external reference key.
        /// </summary>
        /// <value>The external reference key.</value>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="10/29/2009" version="1.1.2.7">
        ///     Member Created
        /// </revision>
        [XmlAttribute("ExtRefKey")]
        public string ExtRefKey
        {
            get;
            set;
        }

        #endregion
    }
}
