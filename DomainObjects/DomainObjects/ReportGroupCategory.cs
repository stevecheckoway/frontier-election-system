// -----------------------------------------------------------------------------
// <copyright file="ReportGroupCategory.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the ReportGroupCategory class.
// </summary>
// <revision revisor="dev14" date="2/26/2009" version="1.0.8.09">
//     File Created
// </revision>
// <revision revisor="dev14" date="3/16/2009" version="1.0.8.2601">
//     File Modified
// </revision>
// <revision revisor="dev14" date="9/10/2009" version="1.0.17.03">
//     File Modified
// </revision>
// -----------------------------------------------------------------------------

namespace Sequoia.DomainObjects
{
    #region Using directives

    using System;
    using System.Xml.Serialization;
    
    #endregion

    /// <summary>
    ///     ReportGroupCategory is a class that is used to categorize 
    ///     <see cref="ReportGroup"/> objects
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev14" date="2/26/2009" version="1.0.8.09">
    ///     Class created.
    /// </revision>
    [Serializable]
    [XmlRoot("ReportGroupCategory")]
    public class ReportGroupCategory : DomainObject
    {
        #region Fields

        /// <summary>
        /// Holds any subcateogries of this category
        /// </summary>
        /// <revision revisor="dev14" date="2/26/2009" version="1.0.8.09">
        ///     Changed type to DomainObjectList{ReportGroupCategory}
        /// </revision>
        private DomainObjectList<ReportGroupCategory> children;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ReportGroupCategory"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="2/26/2009" version="1.0.8.09">
        ///     Member Created
        /// </revision>
        public ReportGroupCategory()
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the object id.
        /// </summary>
        /// <value>The group category id.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="2/26/2009" version="1.0.8.09">
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
        /// <value>The name.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="2/26/2009" version="1.0.8.09">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev14" date="8/31/2009" version="1.0.16.11">
        ///     Added Validation
        /// </revision>
        [XmlAttribute("Name")]
        public string Name
        {
            get; 
            set;
        }

        /// <summary>
        ///     Gets or sets the parent id of the category.
        /// </summary>
        /// <value>The parent id.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="2/26/2009" version="1.0.8.09">
        ///     Member Created
        /// </revision>
        public int ParentId
        {
            get; 
            set;
        }

        /// <summary>
        ///     Gets or sets the child categories.
        /// </summary>
        /// <value>The child categories.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="3/16/2009" version="1.0.8.2601">
        ///     Member Created
        /// </revision>
        [XmlAttribute("Children")]
        public DomainObjectList<ReportGroupCategory> Children
        {
            get
            {
                // if the list of children hasn't been set then 
                // create a new list
                if (this.children == null)
                {
                    this.children = new DomainObjectList<ReportGroupCategory>();
                }
                
                // return the children
                return this.children;
            }

            set
            {
                this.children = value;   
            }
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
