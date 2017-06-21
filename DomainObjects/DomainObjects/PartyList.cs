// -----------------------------------------------------------------------------
// <copyright file="PartyList.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2008 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the PartyList class.
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
    ///     PartyList is a <see cref="List{T}" /> of <see cref="Party" /> objects.
    /// </summary>
    /// <externalUnit cref="PartyList"/>
    /// <revision revisor="dev01" date="8/17/2009" version="1.0.15.9">
    ///     Class created.
    /// </revision>
    [Serializable]
    [XmlRoot("Parties")]
    public class PartyList : DomainObjectList<Party>
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="PartyList"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="8/17/2009" version="1.0.15.9">
        ///     Member Created
        /// </revision>
        public PartyList()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="PartyList"/> class.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        /// <externalUnit cref="List{T}(int)"/>
        /// <revision revisor="dev01" date="8/17/2009" version="1.0.15.9">
        ///     Member Created
        /// </revision>
        public PartyList(int capacity)
            : base(capacity)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="PartyList"/> class.
        /// </summary>
        /// <param name="parties">The parties.</param>
        /// <externalUnit cref="PartyList"/>
        /// <externalUnit cref="List{T}(IEnumerable{T})"/>
        /// <externalUnit cref="IEnumerable{T}"/>
        /// <revision revisor="dev01" date="8/17/2009" version="1.0.15.9">
        ///     Member Created
        /// </revision>
        public PartyList(IEnumerable<Party> parties)
            : base(parties)
        {
        }

        #endregion
    }
}
