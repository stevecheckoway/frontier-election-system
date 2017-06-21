// -----------------------------------------------------------------------------
// <copyright file="PdfLayoutParam.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the PdfLayoutParam class.
// </summary>
// <revision revisor="dev16" date="1/24/2009" version="1.0.5.5">
//     File Created
// </revision>
// <revision revisor="dev14" date="6/17/2009" version="1.0.13.13">
//     File Modified
// </revision> 
// -----------------------------------------------------------------------------

namespace Sequoia.DomainObjects
{
    #region Using directives

    using System;

    #endregion

    /// <summary>
    ///     PdfLayoutParam is a param object for managing the pdf layout 
    ///     parameters stored in the db.
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev16" date="1/24/2009" version="1.0.5.5">
    ///     Class created.
    /// </revision>
    /// <revision revisor="dev14" date="6/17/2009" version="1.0.13.13">
    ///     Added PropertyValueId and PropertyValue Properties
    /// </revision>
    public class PdfLayoutParam : DomainObject, ISysIxParam
    {
        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="PdfLayoutParam"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev16" date="1/24/2009" version="1.0.5.5">
        ///     Member Created
        /// </revision>
        public PdfLayoutParam() 
            : this(0, string.Empty, string.Empty, string.Empty)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="PdfLayoutParam"/> class.
        /// </summary>
        /// <param name="id">The param id.</param>
        /// <param name="name">The name of the param.</param>
        /// <param name="description">The description.</param>
        /// <param name="value">The value.</param>
        /// <externalUnit/>
        /// <revision revisor="dev16" date="1/24/2009" version="1.0.5.5">
        ///     Member created.
        /// </revision>
        public PdfLayoutParam(
            int id, 
            string name, 
            string description, 
            string value)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.Value = value;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the parameter id.
        /// </summary>
        /// <value>The param id.</value>
        /// <externalUnit/>
        /// <revision revisor="dev16" date="1/24/2009" version="1.0.5.5">
        ///     Member created.
        /// </revision>
        public int Id
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the parameter name.
        /// </summary>
        /// <value>The param name.</value>
        /// <externalUnit/>
        /// <revision revisor="dev16" date="1/24/2009" version="1.0.5.5">
        ///     Member created.
        /// </revision>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the parameter description.
        /// </summary>
        /// <value>The description.</value>
        /// <externalUnit/>
        /// <revision revisor="dev16" date="1/24/2009" version="1.0.5.5">
        ///     Member created.
        /// </revision>
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the value.
        /// </summary>
        /// <value>
        ///     The value.
        /// </value>
        /// <externalUnit/>
        /// <revision revisor="dev16" date="1/24/2009" version="1.0.5.5">
        ///     Member created.
        /// </revision>
        public string Value
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the parameter value id.
        /// </summary>
        /// <value>The parameter value id.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="6/17/2009" version="1.0.13.13">
        ///     Member Created
        /// </revision>
        public int ParameterValueId
        {
            get; 
            set;
        }

        /// <summary>
        /// Gets or sets the type of the parameter used to determine input 
        /// display and allowable values for the option
        /// </summary>
        /// <value>The Parameter type.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="6/17/2009" version="1.0.13.13">
        ///     Member Created
        /// </revision>
        public ParameterTypeValue ParameterValue
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
