// -----------------------------------------------------------------------------
// <copyright file="DistrictCategory.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the DistrictCategory class.
// </summary>
// <revision revisor="dev14" date="10/16/2009" version="1.1.1.16">
//     File Created
// </revision>
// <revision revisor="dev14" date="10/20/2009" version="1.1.1.20">
//     File Modified
// </revision>
// <revision revisor="dev14" date="10/21/2009" version="1.1.1.21">
//     File Modified
// </revision>
// <revision revisor="dev13" date="10/21/2009" version="1.1.1.21">
//     File Modified
// </revision>
// -----------------------------------------------------------------------------

namespace Sequoia.DomainObjects
{
    #region Using Directives

    using System;
    using System.Xml.Serialization;

    #endregion

    /// <summary>
    ///	DistrictCategory is a class that represents a type of voting district
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev14" date="10/16/2009" version="1.1.1.16">
    ///     Class created.
    /// </revision>
    /// <revision revisor="dev13" date="10/21/2009" version="1.1.1.21">
    ///     Changed districts from xmlattribute to xmlarray for serialization.
    /// </revision>
    [Serializable]
    [XmlRoot("DistrictCategory")]
    public class DistrictCategory : DomainObject
    {
        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="DistrictCategory"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="10/16/2009" version="1.1.1.16">
        ///     Member Created
        /// </revision>
        public DistrictCategory()
        {
            this.Districts = new DomainObjectList<District>();
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the category id.
        /// </summary>
        /// <value>The category id.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="10/20/2009" version="1.1.1.20">
        ///     Member Created
        /// </revision>
        [XmlAttribute("Id")]
        public int Id
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the category name.
        /// </summary>
        /// <value>The category name.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="10/20/2009" version="1.1.1.20">
        ///     Member Created
        /// </revision>
        [XmlAttribute("Name")]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the code that the customer (state) has assigned to
        ///     the category.
        /// </summary>
        /// <value>The state code.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="10/20/2009" version="1.1.1.20">
        ///     Member Created
        /// </revision>
        [XmlAttribute("StateCode")]
        public string StateCode
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the external reference key for the category.
        /// </summary>
        /// <value>The external key.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="10/20/2009" version="1.1.1.20">
        ///     Member Created
        /// </revision>
        [XmlAttribute("ExtRefKey")]
        public string ExtRefKey
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the order in which the category should be displayed
        ///     when in a list.
        /// </summary>
        /// <value>The list order.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="10/20/2009" version="1.1.1.20">
        ///     Member Created
        /// </revision>
        [XmlIgnore]
        public int ListOrder
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the districts assigned to this category
        /// </summary>
        /// <value>The <see cref="DomainObjectList{D}" /> of districts</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="10/20/2009" version="1.1.1.20">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev14" date="10/21/2009" version="1.1.1.21">
        ///     Made setter public
        /// </revision>
        /// <revision revisor="dev14" date="10/21/2009" version="1.1.1.21">
        ///     Made setter public
        /// </revision>
        /// <revision revisor="dev13" date="10/21/2009" version="1.1.1.21">
        ///     Changed to xml array
        /// </revision>
        [XmlArray("Districts")]
        [XmlArrayItem("District")]
        public DomainObjectList<District> Districts
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
