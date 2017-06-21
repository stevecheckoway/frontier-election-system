// -----------------------------------------------------------------------------
// <copyright file="VoterCandidateList.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2008 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the VoterCandidateList class.
// </summary>
// <revision revisor="dev06" date="10/13/2008" version="1.0.0.0">
//     File Created
// </revision>  
// <revision revisor="dev13" date="8/24/2009" version="1.0.16.04">
//     File modified
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
    ///     VoterCandidateList is a <see cref="List{T}"/> of <see cref="Candidate"/>s.
    /// </summary>
    /// <externalUnit cref="Candidate"/>
    /// <revision revisor="dev06" date="10/13/2008" version="1.0.0.0">
    ///     Class created.
    /// </revision>
    /// <revision revisor="dev05" date="09/11/09" version="1.0.17.4">
    ///     Moved in from BallotStorage and renamed from "CandidateList".
    /// </revision>
    [Serializable]
    [XmlRoot("")]
    public class VoterCandidateList : List<VoterCandidate>
    {
        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="VoterCandidateList"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev06" date="10/13/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision> 
        public VoterCandidateList()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="VoterCandidateList"/> class.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        /// <externalUnit cref="List{T}(int)"/>
        /// <revision revisor="dev06" date="10/13/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public VoterCandidateList(int capacity)
            : base(capacity)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="VoterCandidateList"/> class.
        /// </summary>
        /// <param name="candidates">The candidates.</param>
        /// <externalUnit cref="Candidate"/>
        /// <externalUnit cref="IEnumerable{T}"/>
        /// <externalUnit cref="List{T}(IEnumerable{T})"/>
        /// <revision revisor="dev06" date="10/13/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public VoterCandidateList(IEnumerable<VoterCandidate> candidates)
            : base(candidates)
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
