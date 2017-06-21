// -----------------------------------------------------------------------------
// <copyright file="PaperFace.cs" company="Sequoia Voting Systems"> 
//    Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//    Distribution of source code is allowable only under the terms of the
//    license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//    This class contains the minimal information to uniquely identify a 
//    paper ballot face. By doing that, common faces can be identified and
//    not duplicated
// </summary>
// <revision revisor="dev11" date="2/20/2009" version="1.0.8.0301">
//    File Created
// </revision>  
// -----------------------------------------------------------------------------

namespace Sequoia.Ballot.Data
{
    #region Using directives

    using System;
    using System.Collections.Generic;

    using Sequoia.DomainObjects;

    #endregion

    /// <summary>
    ///     This class contains the minimal information to uniquely identify a 
    ///     paper ballot face. By doing that, common faces can be identified and
    ///     not duplicated. In order to generate an MD5 hash off of an instance 
    ///     of this class, it needs to be serializable.
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev11" date="2/20/2009" version="1.0.8.0301">
    ///     Class created.
    /// </revision>
    [Serializable]
    public class PaperFace
    {
        #region Fields

        /// <summary>
        ///     A paper face also contains a collection of artifacts. This 
        ///     collection is as well in a specific order so that it can be 
        ///     compared to other faces. 
        /// </summary>
        private List<BallotShape> artifacts;

        /// <summary>
        ///     A paper face contains a collection of candidates. The collection
        ///     is in a specific order so that it can be compared to other 
        ///     faces.
        ///     NOTE (Assumption):
        ///     There's no need to include the positioning info since no two 
        ///     faces can be different with the same candidates in the same 
        ///     order and the same artifacts. If the candidates are the same, 
        ///     but are offset it is because at least 1 artifact is different 
        ///     removing the need of having the candidate coordinates.
        /// </summary>
        private List<int> candidates;

        #endregion
        
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="PaperFace"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="2/20/2009" version="1.0.8.0301">
        ///     Member Created
        /// </revision>
        public PaperFace()
        {
            this.artifacts = new List<BallotShape>();
            this.candidates = new List<int>();
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the collection of artifacts.
        /// </summary>
        /// <value>The artifacts.</value>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="2/23/2009" version="1.0.8.0601">
        ///     Member Created
        /// </revision>
        public List<BallotShape> Artifacts
        {
            get
            {
                return this.artifacts;
            }
        }

        /// <summary>
        ///     Gets the candidates ids
        /// </summary>
        /// <value>The candidates ids.</value>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="2/23/2009" version="1.0.8.0601">
        ///     Member Created
        /// </revision>
        public List<int> Candidates
        {
            get
            {
                return this.candidates;
            }
        }

        #endregion

        #region Public Methods

        #endregion

        #region Public Events

        #endregion

        #region Private Methods

        #endregion

        #region Constants

        #endregion
    }
}
