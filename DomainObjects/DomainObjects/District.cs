// -----------------------------------------------------------------------------
// <copyright file="District.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the District class.
// </summary>
// <revision revisor="dev14" date="10/16/2009" version="1.1.1.16">
//     File Created
// </revision>
// <revision revisor="dev14" date="10/20/2009" version="1.1.1.20">
//     File modified
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
    ///	    District is a class that represents a voting district
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev14" date="10/16/2009" version="1.1.1.16">
    ///     Class created.
    /// </revision>
    [Serializable]
    [XmlRoot("District")]
    public class District : DomainObject
    {
        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="District"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="10/16/2009" version="1.1.1.16">
        ///     Member Created
        /// </revision>
        public District()
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the district id.
        /// </summary>
        /// <value>The district id.</value>
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
        /// Gets or sets the district id.
        /// </summary>
        /// <value>The district id.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="10/20/2009" version="1.1.1.20">
        ///     Member Created
        /// </revision>
        [XmlIgnore]
        public int DistrictCategoryId
        {
            get; 
            set;
        }

        /// <summary>
        ///     Gets or sets the district name.
        /// </summary>
        /// <value>The district name.</value>
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
        ///     the district.
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
        /// <revision revisor="dev13" date="10/21/2009" version="1.1.1.21">
        ///     Changed to xml ignore since this only applies internally to 
        ///     reference back to what was originally imported.
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
        [XmlAttribute("ListOrder")]
        public int ListOrder
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