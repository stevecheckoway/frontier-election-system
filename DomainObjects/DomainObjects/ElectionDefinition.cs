// -----------------------------------------------------------------------------
// <copyright file="ElectionDefinition.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2008 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the ElectionDefinition class.
// </summary>
// <revision revisor="dev06" date="12/18/2008" version="1.0.0.0">
//     File Created
// </revision>  
// -----------------------------------------------------------------------------

namespace Sequoia.DomainObjects
{
    #region Using directives

    #endregion

    /// <summary>
    ///     ElectionDefinition is an attempt at defining an object graph which 
    ///     ultimately represents the entire set of data needed to define 
    ///     an election.
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev06" date="12/18/2008" version="1.0.0.0">
    ///     Class created.
    /// </revision>
    public class ElectionDefinition
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ElectionDefinition"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev06" date="12/18/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision> 
        public ElectionDefinition()
        {
            // init contests
            this.Contests = new ContestList();
            this.Parties = new PartyList();
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the contests.
        /// </summary>
        /// <value>The contests.</value>
        /// <externalUnit cref="ContestList"/>
        /// <revision revisor="dev06" date="12/18/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public ContestList Contests
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the parties.
        /// </summary>
        /// <value>The Parties.</value>
        /// <externalUnit cref="ContestList"/>
        /// <revision revisor="dev01" date="8/17/2009" version="1.0.15.9">
        ///     Member Created
        /// </revision>
        public PartyList Parties
        {
            get;
            set;
        }

        #endregion
    }
}
