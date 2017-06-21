// -----------------------------------------------------------------------------
// <copyright file="ReportGroup.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the ReportGroup class.
// </summary>
// <revision revisor="dev14" date="2/26/2009" version="1.0.8.09">
//     File Created
// </revision>
// <revision revisor="dev14" date="3/3/2009" version="1.0.8.13">
//     File Modified
// </revision>
// <revision revisor="dev14" date="4/13/2009" version="1.0.11.03">
//     File Modified
// </revision>
// <revision revisor="dev14" date="8/31/2009" version="1.0.16.11">
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
    ///     <para>
    ///     ReportGroup is a class that represents a canvassing level or
    ///     location that would be interested in the aggregation of system
    ///     reporting.  At the lowest level, a ReportGroup might be an 
    ///     individual polling place.  At the highest level, it might be an
    ///     entire nation.  It is not, however, limited to geographic places.
    ///     </para>
    ///     <para>
    ///     ReportGroups are aggregated by <see cref="ReportGroupCategory"/>
    ///     </para>
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev14" date="2/26/2009" version="1.0.8.09">
    ///     Class created.
    /// </revision>
    /// <revision revisor="dev14" date="2/26/2009" version="1.0.8.09">
    ///     Class modified.  Did most of the implementation of this class
    /// </revision>
    /// <revision revisor="dev14" date="4/13/2009" version="1.0.11.03">
    ///     Class modified.  Added Processed Property
    /// </revision>
    [Serializable]
    [XmlRoot("ReportGroup")]
    public class ReportGroup : DomainObject
    {
        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ReportGroup"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="2/26/2009" version="1.0.8.09">
        ///     Member Created
        /// </revision>
        public ReportGroup()
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the object id.
        /// </summary>
        /// <value>The group id.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="3/2/2009" version="1.0.8.12">
        ///     Member Created
        /// </revision>
        [XmlAttribute("Id")]
        public int Id
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the group name.
        /// </summary>
        /// <value>The Report group name.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="3/2/2009" version="1.0.8.12">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev14" date="8/31/2009" version="1.0.16.11">
        ///     Added Validation
        /// </revision>
        /// <revision revisor="dev14" date="9/30/2009" version="1.0.17.22">
        ///     Moved validation into the builder class
        /// </revision>
        [XmlAttribute("Name")]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the group category id.
        /// </summary>
        /// <value>The category id.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="3/2/2009" version="1.0.8.12">
        ///     Member Created
        /// </revision>
        [XmlAttribute("CategoryId")]
        public int CategoryId
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the query key.
        /// </summary>
        /// <value>The query key.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="3/2/2009" version="1.0.8.12">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev14" date="8/31/2009" version="1.0.16.11">
        ///     Added Validation
        /// </revision>
        [XmlAttribute("QueryKey")]
        public string QueryKey
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the child groups.
        /// </summary>
        /// <value>The child groups.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="3/3/2009" version="1.0.8.13">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev14" date="9/10/2009" version="1.0.17.03">
        ///     Changed type to DomainObjectList{ReportGroup}
        /// </revision>
        [XmlAttribute("ChildGroups")]
        public DomainObjectList<ReportGroup> ChildGroups
        { 
            get; 
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this 
        /// <see cref="ReportGroup"/> has been reported/canvassed.
        /// </summary>
        /// <value><c>true</c> if report is in; otherwise, <c>false</c>.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="4/13/2009" version="1.0.11.03">
        ///     Member Created
        /// </revision>
        public bool Processed
        {
            get; 
            set;
        }

        /// <summary>
        /// Gets or sets the report group's parent id. This holds the id of the
        /// parent group for creating or moving a report group
        /// </summary>
        /// <value>The parent ID.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="3/3/2009" version="1.0.8.14">
        ///     Member Created
        /// </revision>
        public int ParentId
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
