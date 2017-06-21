// -----------------------------------------------------------------------------
// <copyright file="GroupCategoryBuilder.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the GroupCategoryBuilder class.
// </summary>
// <revision revisor="dev14" date="9/30/2009" version="1.0.17.22">
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
    ///     GroupCategoryBuilder is a class that creates 
    ///     a <see cref="ReportGroupCategory" /> instance
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev14" date="9/30/2009" version="1.0.17.22">
    ///     Class created.
    /// </revision>
    public class GroupCategoryBuilder : ISequoiaBuilder<ReportGroupCategory>
    {
        #region Fields

        /// <summary>
        ///     Reference to a category that we are updating or building
        /// </summary>
        private ReportGroupCategory category;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="GroupCategoryBuilder"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="9/30/2009" version="1.0.17.22">
        ///     Member Created
        /// </revision>
        public GroupCategoryBuilder() : this(new ReportGroupCategory())
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="GroupCategoryBuilder"/> class.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="9/30/2009" version="1.0.17.22">
        ///     Member Created
        /// </revision>
        public GroupCategoryBuilder(ReportGroupCategory category)
        {
            // set the local reference to the category
            this.category = category;

            // set the local category id value
            this.Id = category.Id;

            // set the local name value
            this.Name = category.Name;

            // set the local parentId name
            this.ParentId = category.ParentId;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the id of the category.
        /// </summary>
        /// <value>The id of the group category.</value>
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
        ///     Gets or sets the name of the category.
        /// </summary>
        /// <value>The name of the group category.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="9/30/2009" version="1.0.17.22">
        ///     Member Created
        /// </revision>
        [StringLengthValidator(
            1,
            100,
            MessageTemplateResourceName = "ReportGroupCategoryNameLengthMessage",
            MessageTemplateResourceType = typeof(Resources))]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the parent id.
        /// </summary>
        /// <value>The parent id.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="9/30/2009" version="1.0.17.22">
        ///     Member Created
        /// </revision>
        public int ParentId
        {
            get;
            set;
        }

        #endregion

        #region Public Methods

        #region Implementation of ISequoiaBuilder<T>

        /// <summary>
        ///     Builds an instance of a <see cref="ReportGroupCategory"/>.
        /// </summary>
        /// <returns>The category instance</returns>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="9/29/2009" version="1.0.17.21">
        ///     Member Created
        /// </revision>
        public ReportGroupCategory Build()
        {
            // set the category's name to the local value
            this.category.Name = this.Name;

            // set the category's parent to the local value
            this.category.ParentId = this.ParentId;

            // return the contest
            return this.category;
        }

        #endregion

        #endregion

        #region Public Events

        #endregion

        #region Private Methods

        #endregion
    }
}
