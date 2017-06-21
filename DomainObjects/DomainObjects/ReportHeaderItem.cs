// -----------------------------------------------------------------------------
// <copyright file="ReportHeaderItem.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the ReportHeaderItem class.
// </summary>
// <revision revisor="dev13" date="4/22/2009" version="1.0.11.10">
//     File Created
// </revision>  
// -----------------------------------------------------------------------------

namespace Sequoia.DomainObjects
{
    #region Using directives

    using System;
    using System.Xml.Serialization;

    #endregion

    /// <summary>
    ///     Class for report header item
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev13" date="4/22/2009" version="1.0.11.10">
    ///     Class created.
    /// </revision>
    [Serializable]
    [XmlRoot("HeaderItem")]
    public class ReportHeaderItem : DomainObject
    {
        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ReportHeaderItem"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="4/22/2009" version="1.0.11.10">
        ///     Member Created
        /// </revision> 
        public ReportHeaderItem()
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>The item name.</value>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="4/22/2009" version="1.0.11.10">
        ///     Member Created
        /// </revision>
        [XmlAttribute("name")]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="4/22/2009" version="1.0.11.10">
        ///     Member Created
        /// </revision>
        [XmlAttribute("value")]
        public string Value
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
