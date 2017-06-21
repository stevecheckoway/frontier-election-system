// -----------------------------------------------------------------------------
// <copyright file="VoterContestList.cs" company="Sequoia Voting Systems">
//     Copyright (c) 2008 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the VoterContestList class.
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
// <revision revisor="dev05" date="10/26/09" version="1.1.2.4">
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
    ///     VoterContestList is a <see cref="List{T}"/> of 
    ///     <see cref="VoterContest"/> objects.
    /// </summary>
    /// <externalUnit cref="VoterContest"/>
    /// <revision revisor="dev06" date="10/13/2008" version="1.0.0.0">
    ///     Class created.
    /// </revision>
    /// <revision revisor="dev05" date="09/11/09" version="1.0.17.4">
    ///     Moved in from BallotStorage, and renamed from "ContestList".
    /// </revision>
    [Serializable]
    [XmlRoot("")]
    public class VoterContestList : List<VoterContest>
    {
        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="VoterContestList"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev06" date="10/13/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public VoterContestList()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="VoterContestList"/> class.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        /// <externalUnit cref="List{T}(int)"/>
        /// <revision revisor="dev06" date="10/13/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public VoterContestList(int capacity)
            : base(capacity)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="VoterContestList"/> class.
        /// </summary>
        /// <param name="contests">The contests.</param>
        /// <externalUnit cref="VoterContest"/>
        /// <externalUnit cref="IEnumerable{T}"/>
        /// <externalUnit cref="List{T}(IEnumerable{T})"/>
        /// <revision revisor="dev06" date="10/13/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public VoterContestList(IEnumerable<VoterContest> contests)
            : base(contests)
        {
        }

        #endregion

        #region Public Properties

        #endregion

        #region Public Methods

        /// <summary>
        ///     Find the contest with a given ID.
        /// </summary>
        /// <param name="id">Contest ID</param>
        /// <returns>Voter contest with matchining ID, or null if none</returns>
        /// <externalUnit/>
        /// <revision revisor="dev05" date="10/26/09" version="1.1.2.4">
        ///     Method created.
        /// </revision>
        public VoterContest FindContest(int id)
        {
            VoterContest result = null;     // Returned contest

            // Search for contest with matching ID.
            foreach (var contest in this)
            {
                if (contest.Id == id)
                {
                    result = contest;
                    break;
                }
            }

            return result;
        }

        #endregion

        #region Public Events

        #endregion

        #region Private Methods

        #endregion
    }
}
