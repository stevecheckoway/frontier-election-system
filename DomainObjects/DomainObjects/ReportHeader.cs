// -----------------------------------------------------------------------------
// <copyright file="ReportHeader.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the ReportHeader class.
// </summary>
// <revision revisor="dev13" date="4/22/2009" version="1.0.0.0">
//     File Created
// </revision>  
// -----------------------------------------------------------------------------

namespace Sequoia.DomainObjects
{
    #region Using directives

    using System;
    using System.Xml.Serialization;

    using Sequoia.DomainObjects.Persistence;

    #endregion

    /// <summary>
    ///     Class for the report header
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev13" date="4/22/2009" version="1.0.11.10">
    ///     Class created.
    /// </revision>
    [Serializable]
    [XmlRoot("ReportHeader")]
    public class ReportHeader : DomainObject, IPersistible
    {
        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ReportHeader"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="4/22/2009" version="1.0.11.10">
        ///     Member Created
        /// </revision> 
        public ReportHeader()
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the report header items.
        /// </summary>
        /// <value>The report header items.</value>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="4/22/2009" version="1.0.11.10">
        ///     Member Created
        /// </revision>
        [XmlArray("ReportHeaderItems")]
        [XmlArrayItem("ReportHeaderItem")]
        public ReportHeaderItemList ReportHeaderItems
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the serial number.
        /// </summary>
        /// <value>The serial number.</value>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="4/27/2009" version="1.0.11.15">
        ///     Member Created
        /// </revision>
        [XmlAttribute("SerialNumber")]
        public int SerialNumber
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

        #region IPersistible Members

        /// <summary>
        ///     Gets the serialized data.
        /// </summary>
        /// <value>The serialized data.</value>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="4/22/2009" version="1.0.11.10">
        ///     Member Created
        /// </revision>
        public string SerializedData
        {
            get
            {
                return Serialize(this);
            }
        }

        /// <summary>
        ///     Sets the persister.
        /// </summary>
        /// <param name="persister">The persister.</param>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="4/22/2009" version="1.0.11.10">
        ///     Member Created
        /// </revision>
        public void SetPersister(IPersister persister)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
