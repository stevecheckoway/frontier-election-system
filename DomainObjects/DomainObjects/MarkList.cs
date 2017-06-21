// -----------------------------------------------------------------------------
// <copyright file="MarkList.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2008 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the MarkList class.
// </summary>
// <revision revisor="dev13" date="12/12/2008" version="1.0.0.0">
//     File Created
// </revision>  
// <revision revisor="dev16" date="12/22/2008" version="1.0.0.0">
//     File Modified
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
    ///     MarkList is a <see cref="List{T}" /> of <see cref="Mark" /> objects. 
    /// </summary>
    /// <externalUnit cref="MarkList"/>
    /// <revision revisor="dev13" date="12/12/2008" version="1.0.0.0">
    ///     Class created.
    /// </revision>
    /// <revision revisor="dev16" date="12/22/2008" version="1.0.0.0">
    ///     Modified class to inherit from DomainObjectList
    /// </revision>
    [Serializable]
    [XmlRoot("Marks")]
    public class MarkList : DomainObjectList<Mark>
    {
        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="MarkList"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="12/12/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision> 
        public MarkList()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="MarkList"/> class.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        /// <externalUnit cref="List{T}(int)"/>
        /// <revision revisor="dev13" date="12/12/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public MarkList(int capacity)
            : base(capacity)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="MarkList"/> class.
        /// </summary>
        /// <param name="marks">The marks.</param>
        /// <externalUnit cref="MarkList"/>
        /// <externalUnit cref="List{T}(IEnumerable{T})"/>
        /// <externalUnit cref="IEnumerable{T}"/>
        /// <revision revisor="dev13" date="12/12/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public MarkList(IEnumerable<Mark> marks)
            : base(marks)
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
