// -----------------------------------------------------------------------------
// <copyright file="IContest.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the IContest interface.
// </summary>
// <revision revisor="dev06" date="2/10/2009" version="1.0.0.0">
//     File Created
// </revision>
// -----------------------------------------------------------------------------

namespace Sequoia.DomainObjects.BallotStorage
{
    /// <summary>
    ///     Icontest interface for Jurisdictional rules
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev01" date="11/20/2009" version="1.1.3.7">
    ///     Member Created
    /// </revision>
    public interface IContest
    {  
        /// <summary>
        ///     Gets the candidate count.
        /// </summary>
        /// <value>The candidate count.</value>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="11/20/2009" version="1.1.3.7">
        ///     Member Created
        /// </revision>
        int CandidateCount
        {
            get;
        }

        /// <summary>
        ///     Gets the vote for.
        /// </summary>
        /// <value>The vote for.</value>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="11/20/2009" version="1.1.3.7">
        ///     Member Created
        /// </revision>
        int VoteFor
        {
            get;
        }
    }
}
