// -----------------------------------------------------------------------------
// <copyright file="Portion.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the Portion class.
// </summary>
// <revision revisor="dev13" date="10/21/2009" version="1.1.1.19">
//     File Created
// </revision> 
// <revision revisor="dev13" date="10/29/2009" version="1.1.2.7">
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
    ///	    Portion is a class that represents a precinct portion
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev13" date="10/21/2009" version="1.1.1.19">
    ///     Class created.
    /// </revision>
    [Serializable]
    [XmlRoot("Portion")]
    public class Portion : DomainObject
    {
        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Portion"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="10/21/2009" version="1.1.1.19">
        ///     Member Created
        /// </revision>	
        public Portion()
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        /// <value>The portion id.</value>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="10/21/2009" version="1.1.1.19">
        ///     Member Created
        /// </revision>
        [XmlAttribute("Id")]
        public int Id
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>The portion name.</value>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="10/21/2009" version="1.1.1.19">
        ///     Member Created
        /// </revision>
        [XmlAttribute("Name")]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the external reference key.
        /// </summary>
        /// <value>The external reference key.</value>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="10/21/2009" version="1.1.1.19">
        ///     Member Created
        /// </revision>
        [XmlAttribute("ExtRefKey")]
        public string ExtRefKey
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the list order.
        /// </summary>
        /// <value>The list order.</value>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="10/21/2009" version="1.1.1.7">
        ///     Member Created
        /// </revision>
        [XmlAttribute("ListOrder")]
        public int ListOrder
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the code.
        /// </summary>
        /// <value>The portion code.</value>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="10/29/2009" version="1.1.2.7">
        ///     Member Created
        /// </revision>
        [XmlAttribute("Code")]
        public string Code
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is valid.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </value>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="10/29/2009" version="1.1.2.5">
        ///     Member Created
        /// </revision>
        [XmlAttribute("IsValid")]
        public bool IsValid
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets a value indicating whether [user defined].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [user defined]; otherwise, <c>false</c>.
        /// </value>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="10/29/2009" version="1.1.2.7">
        ///     Member Created
        /// </revision>
        [XmlAttribute("UserDefined")]
        public bool UserDefined
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the districts.
        /// </summary>
        /// <value>The districts.</value>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="10/21/2009" version="1.1.1.21">
        ///     Member Created
        /// </revision>
        [XmlArray("Districts")]
        [XmlArrayItem("District")]
        public DomainObjectList<District> Districts
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the precinct id. This is a reference to the parent
        ///     id for ease of lookup.
        /// </summary>
        /// <value>The precinct id.</value>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="10/21/2009" version="1.1.1.19">
        ///     Member Created
        /// </revision>
        [XmlIgnore]
        public int PrecinctId
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
