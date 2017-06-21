// -----------------------------------------------------------------------------
// <copyright file="VoterContest.cs" company="Sequoia Voting Systems">
//     Copyright (c) 2008 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the VoterContest class.
// </summary>
// <revision revisor="dev06" date="10/13/2008" version="1.0.0.0">
//     File Created
// </revision>
// <revision revisor="dev14" date="2/11/2009" version="1.0.6.6">
//     File modified
// </revision>
// <revision revisor="dev13" date="8/24/2009" version="1.0.16.04">
//     File modified
// </revision>
// <revision revisor="dev05" date="09/02/09" version="1.0.16.12">
//     File modified.
// </revision>
// <revision revisor="dev01" date="09/03/09" version="1.0.16.13">
//     File modified.
// </revision>
// <revision revisor="dev05" date="09/11/09" version="1.0.17.4">
//     File modified.
// </revision>
// <revision revisor="dev05" date="10/26/09" version="1.1.2.4">
//     File modified.
// </revision>
// <revision revisor="dev05" date="11/17/09" version="1.1.3.5">
//     File modified.
// </revision>
// -----------------------------------------------------------------------------

namespace Sequoia.DomainObjects.BallotStorage
{
    #region Using directives

    using System;
    using System.Xml.Serialization;

    using Sequoia.DomainObjects;

    #endregion

    /// <summary>
    ///     VoterContest represents a contest on a ballot.
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev06" date="10/13/2008" version="1.0.0.0">
    ///     Class created.
    /// </revision>
    /// <revision revisor="dev05" date="09/11/09" version="1.0.17.4">
    ///     Moved in from BallotStorage and renamed from "Contest".
    /// </revision>
    /// <revision revisor="dev05" date="11/17/09" version="1.1.3.5">
    ///     Removed Overvotes and Undervotes properties, to break
    ///     JurisdictionalRulesEngine dependency.
    /// </revision>
    [Serializable]
    [XmlRoot("Contest")]
    public class VoterContest : ICloneable, IContest
    {
        #region Fields

        /// <summary>
        ///     Create var for storing contest id
        /// </summary>
        private int id = 0;

        /// <summary>
        ///     Create var for storing the contest votefor value
        /// </summary>
        private int voteFor = 0;

        /// <summary>
        ///     Store candidate count
        /// </summary>
        private int candidateCount = 0;

        /// <summary>
        ///     Create var to store candidates
        /// </summary>
        private VoterCandidateList candidateList = new VoterCandidateList();

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="VoterContest"/> class.
        /// </summary>
        /// <externalUnit cref="VoterContest(int, int)"/>
        /// <revision revisor="dev06" date="10/13/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public VoterContest()
            : this(0, 0)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="VoterContest"/> class.
        /// </summary>
        /// <param name="id">The contest id.</param>
        /// <param name="voteFor">The vote for.</param>
        /// <externalUnit cref="id"/>
        /// <externalUnit cref="voteFor"/>
        /// <revision revisor="dev06" date="10/13/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public VoterContest(int id, int voteFor)
        {
            // set the property values
            this.Id = id;
            this.VoteFor = voteFor;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the outstack mask.
        /// </summary>
        /// <value>The outstack mask.</value>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="9/3/2009" version="1.0.16.11">
        ///     Member Created
        /// </revision>
        [XmlIgnore]
        public int OutstackMask
        {
            get
            {
                int returnValue = 0;
                if (this.OutstackConditions != null)
                {
                    foreach (OutstackCondition cond in this.OutstackConditions)
                    {
                        returnValue = returnValue | (int)cond.ConditionType;
                    }
                }

                return returnValue;
            }
        }

        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        /// <value>The contest id.</value>
        /// <externalUnit cref="id"/>
        /// <revision revisor="dev06" date="10/13/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        [XmlAttribute("Id")]
        public int Id
        {
            get
            {
                return this.id;
            }

            set
            {
                this.id = value;
            }
        }

        /// <summary>
        ///     Gets or sets the contest type.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev05" date="08/27/09" version="1.0.16.6">
        ///     Property created.
        /// </revision>
        [XmlAttribute("ContestType")]
        public ContestType ContestType
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the vote for.
        /// </summary>
        /// <value>The vote for.</value>
        /// <externalUnit cref="voteFor"/>
        /// <revision revisor="dev06" date="10/13/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        [XmlAttribute("VoteFor")]
        public int VoteFor
        {
            get
            {
                return this.voteFor;
            }

            set
            {
                this.voteFor = value;
            }
        }

        /// <summary>
        ///     Gets or sets the list of outstack conditions.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev05" date="09/02/09" version="1.0.16.12">
        ///     Property created.
        /// </revision>
        [XmlArray("OutstackConditions")]
        [XmlArrayItem("OutstackCondition")]
        public OutstackConditionList OutstackConditions
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the candidates.
        /// </summary>
        /// <value>The candidates.</value>
        /// <externalUnit cref="candidateList"/>
        /// <externalUnit cref="CandidateList"/>
        /// <exception cref="ArgumentException">
        ///     This will be thrown if code attempts to set the Candidate
        ///     collection to null.
        /// </exception>
        /// <revision revisor="dev06" date="10/13/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        [XmlArray("Candidates")]
        [XmlArrayItem("Candidate")]
        public VoterCandidateList Candidates
        {
            get
            {
                return this.candidateList;
            }

            set
            {
                // make sure incoming value is not null
                if (value == null)
                {
                    throw new ArgumentException(
                        "Candidates cannot be set to a null value.");
                }

                this.candidateList = value;
            }
        }

        /// <summary>
        ///     Gets or sets the candidate count.
        /// </summary>
        /// <value>The candidate count.</value>
        /// <externalUnit cref="candidateCount"/>
        /// <externalUnit cref="candidateList"/>
        /// <revision revisor="dev06" date="10/13/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev05" date="08/27/09" version="1.0.16.6">
        ///     Don't include in serialization.
        /// </revision>
        [XmlIgnore]
        public int CandidateCount
        {
            get
            {
                if (this.Candidates != null)
                {
                    this.candidateCount = this.Candidates.Count;
                }

                return this.candidateCount;
            }

            set
            {
                this.candidateCount = value;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        ///     A new object that is a copy of this instance.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="2/11/2009" version="1.0.6.6">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev05" date="09/11/09" version="1.0.17.4">
        ///     Renamed classes.
        /// </revision>
        public object Clone()
        {
            var clonedContest = MemberwiseClone() as VoterContest;
            var clonedCandidates = new VoterCandidateList();

            foreach (var candidate in this.Candidates)
            {
                clonedCandidates.Add(candidate.Clone() as VoterCandidate);
            }

            clonedContest.Candidates = clonedCandidates;
            return clonedContest;
        }

        /// <summary>
        ///     Add a condition to the outstack conditions list.
        /// </summary>
        /// <param name="conditionId">Outstack condition ID</param>
        /// <externalUnit/>
        /// <revision revisor="dev05" date="09/02/09" version="1.0.16.12">
        ///     Method created.
        /// </revision>
        public void AddOutstackCondition(OutstackConditionId conditionId)
        {
            // We leave the condition list as null until we add a condition.
            // This saves a little space in results.xml.
            if (this.OutstackConditions == null)
            {
                this.OutstackConditions = new OutstackConditionList();
            }

            this.OutstackConditions.Add(new OutstackCondition(conditionId));
        }

        /// <summary>
        ///     Determines whether the specified <see cref="T:System.Object"/> 
        ///     is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <param name="obj">
        ///     The <see cref="T:System.Object"/> to compare with the current 
        ///     <see cref="T:System.Object"/>.
        /// </param>
        /// <returns>
        ///     <c>true</c> if the specified <see cref="T:System.Object"/> is 
        ///     equal to the current <see cref="T:System.Object"/>; 
        ///     otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="T:System.NullReferenceException">
        /// The <paramref name="obj"/> parameter is null.
        /// </exception>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="9/3/2009" version="1.0.16.13">
        ///     Member Created
        /// </revision>
        public override bool Equals(object obj)
        {
            return this.Id.Equals(((VoterContest)obj).Id);
        }

        /// <summary>
        ///     Adds a candidate to the list if it's not already there.
        /// </summary>
        /// <param name="id">The candidate id.</param>
        /// <externalUnit/>
        /// <revision revisor="dev05" date="10/26/09" version="1.1.2.4">
        ///     Method created.
        /// </revision>
        public void AddCandidate(int id)
        {
            bool alreadyThere = false;

            foreach (var candidate in this.candidateList)
            {
                if (candidate.Id == id)
                {
                    alreadyThere = true;
                    break;
                }
            }

            if (!alreadyThere)
            {
                this.candidateList.Add(new VoterCandidate(id));
            }
        }

        /// <summary>
        ///     Removes a candidate from the list.
        /// </summary>
        /// <param name="id">The candidate id.</param>
        /// <returns>
        ///     <c>true</c> if candidate was removed, 
        ///     <c>false</c> if it wasn't there.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev05" date="10/26/09" version="1.1.2.4">
        ///     Method created.
        /// </revision>
        public bool RemoveCandidate(int id)
        {
            var candidate = new VoterCandidate(id);

            return this.candidateList.Remove(candidate);
        }

        #endregion

        #region Public Events

        #endregion

        #region Private Methods

        #endregion
    }
}
