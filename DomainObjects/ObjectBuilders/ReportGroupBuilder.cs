// -----------------------------------------------------------------------------
// <copyright file="ReportGroupBuilder.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the ReportGroupBuilder class.
// </summary>
// <revision revisor="dev14" date="9/30/2009 12:45:22 PM" version="1.0.?.0">
//     File Created
// </revision>
// -----------------------------------------------------------------------------

namespace Sequoia.DomainObjects.ObjectBuilders
{
    #region Using directives

    using System;
    using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
    using Properties;

    #endregion

    /// <summary>
    ///     ReportGroupBuilder is a class that creates 
    ///     a <see cref="ReportGroup" /> instance
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev14" date="9/30/2009" version="1.0.17.22">
    ///     Class created.
    /// </revision>
    public class ReportGroupBuilder : ISequoiaBuilder<ReportGroup>
    {
        #region Fields

        /// <summary>
        ///     Reference to the report group we are trying to build or update
        /// </summary>
        private ReportGroup group;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ReportGroupBuilder"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="9/30/2009" version="1.0.17.22">
        ///     Member Created
        /// </revision>
        public ReportGroupBuilder() : this(new ReportGroup())
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ReportGroupBuilder"/> class.
        /// </summary>
        /// <param name="reportGroup">The report group.</param>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="9/30/2009" version="1.0.17.22">
        ///     Member Created
        /// </revision>
        public ReportGroupBuilder(ReportGroup reportGroup)
        {
            // set the local reference to the report group
            this.group = reportGroup;

            // set the local id value to the report group id
            this.Id = reportGroup.Id;

            // set the local name variable to the report group name
            this.Name = reportGroup.Name;

            // set the local category id value to the report group category id
            this.CategoryId = reportGroup.CategoryId;

            // set the local query key to the report group query key
            this.QueryKey = reportGroup.QueryKey;

            // parent ID
            this.ParentId = reportGroup.ParentId;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the parent id.
        /// </summary>
        /// <value>The parent id.</value>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="11/19/2009" version="1.1.3.7">
        ///     Member Created
        /// </revision>
        public int ParentId
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets the group id.
        /// </summary>
        /// <value>The group id.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="9/30/2009" version="1.0.17.22">
        ///     Member Created
        /// </revision>
        public int Id
        {
            get;
            private set;
        }

        /// <summary>
        ///     Gets or sets the group name.
        /// </summary>
        /// <value>The name of the report group.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="9/30/2009" version="1.0.17.22">
        ///     Member Created
        /// </revision>
        [StringLengthValidator(
            1,
            100,
            MessageTemplateResourceName = "ReportGroupNameLengthMessage",
            MessageTemplateResourceType = typeof(Resources))]
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
        /// <revision revisor="dev14" date="9/30/2009" version="1.0.17.22">
        ///     Member Created
        /// </revision>
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
        /// <revision revisor="dev14" date="9/30/2009" version="1.0.17.22">
        ///     Member Created
        /// </revision>
        [StringLengthValidator(
            1,
            50,
            MessageTemplateResourceName = "ReportGroupKeyLengthMessage",
            MessageTemplateResourceType = typeof(Resources))]
        public string QueryKey
        {
            get;
            set;
        }

        #endregion

        #region Public Methods

        #region Implementation of ISequoiaBuilder<T>

        /// <summary>
        ///     Builds an instance of a <see cref="ReportGroup"/>.
        /// </summary>
        /// <returns>The report group instance</returns>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="9/30/2009" version="1.0.17.22">
        ///     Member Created
        /// </revision>
        public ReportGroup Build()
        {
            // set the report group's name to the local value
            this.group.Name = this.Name;

            // set the report group's report group category to the local value
            this.group.CategoryId = this.CategoryId;

            // set the report group's query key to the local value
            this.group.QueryKey = this.QueryKey;

            // set the Parent Id
            this.group.ParentId = this.ParentId;

            // return the report group
            return this.group;
        }

        #endregion

        #endregion

        #region Public Events

        #endregion

        #region Private Methods

        #endregion
    }
}
