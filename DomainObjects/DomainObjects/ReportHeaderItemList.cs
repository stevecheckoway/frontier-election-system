// -----------------------------------------------------------------------------
// <copyright file="ReportHeaderItemList.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the ReportHeaderItemList class.
// </summary>
// <revision revisor="dev13" date="4/22/2009" version="1.0.11.10">
//     File Created
// </revision>  
// -----------------------------------------------------------------------------

namespace Sequoia.DomainObjects
{
    #region Using directives

    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    #endregion

    /// <summary>
    ///     ReportHeaderItemList is a <see cref="List{T}" /> of <see cref="ReportHeaderItem" /> objects. 
    /// </summary>
    /// <externalUnit cref="ReportHeaderItem"/>
    /// <revision revisor="dev13" date="4/22/2009" version="1.0.11.10">
    ///     Class created.
    /// </revision>
    [Serializable]
    [XmlRoot("ReportHeaderItems")]
    public class ReportHeaderItemList : DomainObjectList<ReportHeaderItem>
    {
        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ReportHeaderItemList"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="4/22/2009" version="1.0.11.10">
        ///     Member Created
        /// </revision> 
        public ReportHeaderItemList()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ReportHeaderItemList"/> class.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        /// <externalUnit cref="List{T}(int)"/>
        /// <revision revisor="dev13" date="4/22/2009" version="1.0.11.10">
        ///     Member Created
        /// </revision>
        public ReportHeaderItemList(int capacity)
            : base(capacity)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ReportHeaderItemList"/> class.
        /// </summary>
        /// <param name="reportHeaderItems">The report header items.</param>
        /// <externalUnit cref="ReportHeaderItem"/>
        /// <externalUnit cref="List{T}(IEnumerable{T})"/>
        /// <externalUnit cref="IEnumerable{T}"/>
        /// <revision revisor="dev13" date="4/22/2009" version="1.0.11.10">
        ///     Member Created
        /// </revision>
        public ReportHeaderItemList(
            IEnumerable<ReportHeaderItem> reportHeaderItems)
            : base(reportHeaderItems)
        {
        }

        #endregion

        #region Public Properties

        #endregion

        #region Public Methods

        #endregion

        #region Public Events

        #endregion

        #region Private Methods

        #endregion
    }
}
