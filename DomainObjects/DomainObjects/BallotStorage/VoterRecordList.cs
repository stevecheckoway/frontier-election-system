// -----------------------------------------------------------------------------
// <copyright file="VoterRecordList.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2008 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the VoterRecordList class.
// </summary>
// <revision revisor="dev06" date="10/13/2008" version="1.0.0.0">
//     File Created
// </revision> 
// <revision revisor="dev13" date="8/24/2009" version="1.0.16.04">
//     File Created
// </revision>  
// <revision revisor="dev05" date="09/11/09" version="1.0.17.4">
//     File modified.
// </revision>
// -----------------------------------------------------------------------------

namespace Sequoia.DomainObjects.BallotStorage
{
    #region Using directives

    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    #endregion

    /// <summary>
    ///     VoterRecordList is a <see cref="List{T}" /> of 
    ///     <see cref="VoterRecord" /> objects. 
    /// </summary>
    /// <externalUnit cref="VoterRecord"/>
    /// <revision revisor="dev06" date="10/13/2008" version="1.0.0.0">
    ///     Class created.
    /// </revision>
    /// <revision revisor="dev05" date="09/11/09" version="1.0.17.4">
    ///     Moved in from BallotStorage.
    /// </revision>
    [Serializable]
    [XmlRoot("")]
    public class VoterRecordList : List<VoterRecord>
    {
        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="VoterRecordList"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev06" date="10/13/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision> 
        public VoterRecordList()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="VoterRecordList"/> class.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        /// <externalUnit cref="List{T}(int)"/>
        /// <revision revisor="dev06" date="10/13/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public VoterRecordList(int capacity)
            : base(capacity)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="VoterRecordList"/> class.
        /// </summary>
        /// <param name="voterRecords">The voter records.</param>
        /// <externalUnit cref="VoterRecord"/>
        /// <externalUnit cref="List{T}(IEnumerable{T})"/>
        /// <externalUnit cref="IEnumerable{T}"/>
        /// <revision revisor="dev06" date="10/13/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public VoterRecordList(IEnumerable<VoterRecord> voterRecords)
            : base(voterRecords)
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
