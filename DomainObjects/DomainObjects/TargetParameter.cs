// -----------------------------------------------------------------------------
// <copyright file="TargetParameter.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the TargetParameter class.
// </summary>
// <revision revisor="dev14" date="4/5/2009" version="1.0.10.02">
//     File Created
// </revision>
// <revision revisor="dev14" date="6/17/2009" version="1.0.13.17">
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
    ///     TargetParameter is a domain object representing the settings for
    ///     ballot target information
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev14" date="4/5/2009" version="1.0.10.02">
    ///     Class created.
    /// </revision>
    /// <revision revisor="dev14" date="6/17/2009" version="1.0.13.17">
    ///     Added the [ParameterType]
    /// </revision>
    [Serializable]
    [XmlRoot("TargetParameter")]
    public class TargetParameter : DomainObject, ISysIxParam
    {
        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="TargetParameter"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="4/5/2009" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public TargetParameter()
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id of the parameter.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="4/6/2009" version="1.0.10.03">
        ///     Member Created
        /// </revision>
        [XmlIgnore]
        public int Id
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name of the parameter.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="4/6/2009" version="1.0.10.03">
        /// Member created.
        /// </revision>
        [XmlAttribute("Name")]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description of the parameter.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="4/6/2009" version="1.0.10.03">
        /// Member created.
        /// </revision>
        [XmlAttribute("Description")]
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="4/6/2009" version="1.0.10.03">
        /// Member created.
        /// </revision>
        [XmlAttribute("Value")]
        public string Value
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the target type id.
        /// </summary>
        /// <value>The target type id.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="4/6/2009" version="1.0.10.03">
        ///     Member Created
        /// </revision>
        [XmlAttribute("TargetType")]
        public int TargetType
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
        [XmlIgnore]
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
        [XmlIgnore]
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
