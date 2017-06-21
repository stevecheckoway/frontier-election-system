// -----------------------------------------------------------------------------
// <copyright file="GenericTypeList.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2008 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the GenericTypeList class.
// </summary>
// <revision revisor="dev01" date="8/17/2009" version="1.0.15.9">
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
    ///     GenericTypeList is a <see cref="List{T}" /> of <see cref="Party" /> objects.
    /// </summary>
    /// <externalUnit cref="GenericTypeList"/>
    /// <revision revisor="dev01" date="8/17/2009" version="1.0.15.9">
    ///     Class created.
    /// </revision>
    [Serializable]
    [XmlRoot("Parties")]
    public class GenericTypeList : DomainObjectList<GenericType>
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="GenericTypeList"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="8/17/2009" version="1.0.15.9">
        ///     Member Created
        /// </revision>
        public GenericTypeList()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="GenericTypeList"/> class.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        /// <externalUnit cref="List{T}(int)"/>
        /// <revision revisor="dev01" date="8/17/2009" version="1.0.15.9">
        ///     Member Created
        /// </revision>
        public GenericTypeList(int capacity)
            : base(capacity)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="GenericTypeList"/> class.
        /// </summary>
        /// <param name="types">The types.</param>
        /// <externalUnit cref="GenericTypeList"/>
        /// <externalUnit cref="List{T}(IEnumerable{T})"/>
        /// <externalUnit cref="IEnumerable{T}"/>
        /// <revision revisor="dev01" date="8/17/2009" version="1.0.15.9">
        ///     Member Created
        /// </revision>
        public GenericTypeList(IEnumerable<GenericType> types)
            : base(types)
        {
        }

        #endregion
    }
}
