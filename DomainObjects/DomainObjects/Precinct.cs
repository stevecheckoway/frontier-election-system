// -----------------------------------------------------------------------------
// <copyright file="Precinct.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the Precinct class.
// </summary>
// <revision revisor="dev14" date="8/31/2009" version="1.0.16.11">
//     File Created
// </revision>
// <revision revisor="dev13" date="10/21/2009" version="1.1.1.21">
//     File modified
// </revision>
// -----------------------------------------------------------------------------

namespace Sequoia.DomainObjects
{
    #region Using directives

    using System;
    using System.Xml.Serialization;

    #endregion

    /// <summary>
    ///     Precinct is a class that represents a voting precinct
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev14" date="8/31/2009" version="1.0.16.11">
    ///     Class created.
    /// </revision>
    /// <revision revisor="dev13" date="10/21/2009" version="1.1.1.21">
    ///     Added external reference key and made the class serializable.
    /// </revision>
    [Serializable]
    [XmlRoot("Precinct")]
    public class Precinct : DomainObject
    {
        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Precinct"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="8/31/2009" version="1.0.16.11">
        ///     Member Created
        /// </revision>
        public Precinct()
        {
        }

        #endregion

        #region Public Properties
        /// <summary>
        ///     Gets or sets the precinct id.
        /// </summary>
        /// <value>The precinct id.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="8/31/2009" version="1.0.16.11">
        ///     Member Created
        /// </revision>
        [XmlAttribute("Id")]
        public int Id
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the precinct name.
        /// </summary>
        /// <value>The precinct name.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="8/31/2009" version="1.0.16.11">
        ///     Member Created
        /// </revision>
        [XmlAttribute("Name")]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the precinct code.
        /// </summary>
        /// <value>The precinct code.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="8/31/2009" version="1.0.16.11">
        ///     Member Created
        /// </revision>
        [XmlAttribute("Code")]
        public string Code
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the precinct's state code.
        /// </summary>
        /// <value>The precinct code.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="8/31/2009" version="1.0.16.11">
        ///     Member Created
        /// </revision>
        [XmlAttribute("StateCode")]
        public string StateCode
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the list order.
        /// </summary>
        /// <value>The list order.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="8/31/2009" version="1.0.16.11">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev13" date="10/21/2009" version="1.1.1.21">
        ///     Changed to xml attribute from xml ignore
        /// </revision>
        [XmlAttribute("ListOrder")]
        public int ListOrder
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the precinct portions.
        /// </summary>
        /// <value>The precinct portions.</value>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="10/21/2009" version="1.1.1.21">
        ///     Member Created
        /// </revision>
        [XmlArray("Portions")]
        [XmlArrayItem("Portion")]
        public DomainObjectList<Portion> Portions
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the external reference key. This is the original
        ///     key from the system the data was imported from.
        /// </summary>
        /// <value>The external reference key.</value>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="10/21/2009" version="1.1.1.21">
        ///     Member Created
        /// </revision>
        [XmlAttribute("ExtRefKey")]
        public string ExtRefKey
        {
            get;
            set;
        }

        #endregion

        #region Public Methods

        #endregion

        #region Public Events

        #endregion

        #region Private Methods

        #endregion
    }
}
