// -----------------------------------------------------------------------------
// <copyright file="VoterRecord.cs" company="Sequoia Voting Systems">
//     Copyright (c) 2008 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the VoterRecord class.
// </summary>
// <revision revisor="dev06" date="10/13/2008" version="1.0.0.0">
//     File Created
// </revision>
// <revision revisor="dev14" date="2/11/2009" version="1.0.6.6">
//     File Modified
// </revision>
// <revision revisor = "dev05" date="02/18/09" version="1.0.8.1">
//     File Modified
// </revision>
// <revision revisor = "dev05" date="02/18/09" version="1.0.8.1">
//     File modified.
// </revision>
// <revision revisor = "dev14" date="05/22/09" version="1.0.12.12">
//     File modified.
// </revision>
// <revision revisor="dev13" date="8/24/2009" version="1.0.16.04">
//     File modified.
// </revision>
// <revision revisor="dev05" date="08/27/09" version="1.0.16.6">
//     File modified.
// </revision>
// <revision revisor="dev05" date="09/02/09" version="1.0.16.12">
//     File modified.
// </revision>
// <revision revisor="dev05" date="09/03/09" version="1.0.16.13">
//     File modified.
// </revision>
// <revision revisor="dev05" date="09/11/09" version="1.0.17.4">
//     File modified.
// </revision>
// <revision revisor="dev05" date="10/26/09" version="1.1.2.4">
//     File modified.
// </revision>
// <revision revisor="dev01" date="11/12/2009" version="1.1.2.21">
//     File Modified
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
    ///     Class containing voter record details
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev06" date="10/13/2008" version="1.0.0.0">
    ///     Class created.
    /// </revision>
    /// <revision revisor = "dev05" date="02/18/09" version="1.0.8.1">
    ///     Overrode GetHashCode, Equals, and ToString, to redefine equality.
    /// </revision>
    /// <revision revisor="dev05" date="09/11/09" version="1.0.17.4">
    ///     Moved in from BallotStorage and renamed child object classes.
    /// </revision>
    /// <revision revisor="dev05" date="11/17/09" version="1.1.3.5">
    ///     Moved AddOutstackConditions and RunVoterActions methods out to
    ///     JurisdictionalRulesEngine, to break dependency.
    /// </revision>
    [Serializable]
    [XmlRoot("VoterRecord")]
    public class VoterRecord : DomainObject, ICloneable
    {
        #region Fields
        /// <summary>
        ///     Create var to store the ballot id
        /// </summary>
        private int ballotId;

        /// <summary>ID of the card that was voted</summary>
        private int cardId;

        /// <summary>
        ///     Create var for storing default value of contest count
        /// </summary>
        private int contestCount;

        /// <summary>
        ///     Create var to store contests for voter record
        /// </summary>
        private VoterContestList contestList = new VoterContestList();

        /// <summary>
        ///     Create var for storing the voter session id
        /// </summary>
        private long id;

        /// <summary>
        ///     Create var to store the party id
        /// </summary>
        private int partyId;

        /// <summary>
        ///     Create var to store precinct id
        /// </summary>
        private int precinctId;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="VoterRecord"/> class.
        /// </summary>
        /// <externalUnit cref="VoterRecord(int, int, int, int)"/>
        /// <revision revisor="dev06" date="10/13/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public VoterRecord() : this(0, 0, 0, 0)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="VoterRecord"/> class.
        /// </summary>
        /// <param name="id">The voter id.</param>
        /// <param name="precinctId">The precinct id.</param>
        /// <param name="ballotId">The ballot id.</param>
        /// <param name="partyId">The party id.</param>
        /// <revision revisor="dev14" date="10/13/2008" version="1.0.6.6">
        ///     Member Created
        /// </revision>
        public VoterRecord(int id, int precinctId, int ballotId, int partyId)
            : this(id, precinctId, ballotId, partyId, false)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="VoterRecord"/> class.
        /// </summary>
        /// <param name="id">The voter id.</param>
        /// <param name="precinctId">The precinct id.</param>
        /// <param name="ballotId">The ballot id.</param>
        /// <param name="partyId">The party id.</param>
        /// <param name="isValid">Is Record Valid</param>
        /// <externalUnit cref="PrecinctId"/>
        /// <externalUnit cref="BallotId"/>
        /// <externalUnit cref="Id"/>
        /// <externalUnit cref="PartyId"/>
        /// <revision revisor="dev06" date="10/13/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public VoterRecord(
            int id,
            int precinctId,
            int ballotId,
            int partyId,
            bool isValid)
        {
            // set the properties from input
            this.Id = id;
            this.PrecinctId = precinctId;
            this.BallotId = ballotId;
            this.PartyId = partyId;
            this.IsValid = isValid;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the ballot id.
        /// </summary>
        /// <value>The ballot id.</value>
        /// <externalUnit cref="ballotId"/>
        /// <revision revisor="dev06" date="10/13/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        [XmlAttribute("BallotId")]
        public int BallotId
        {
            get
            {
                return this.ballotId;
            }

            set
            {
                this.ballotId = value;
            }
        }

        /// <summary>
        ///     Gets or sets the card ID.
        /// </summary>
        /// <value>The card id.</value>
        /// <externalUnit cref="cardId"/>
        /// <revision revisor = "dev05" date="01/17/09" version="1.0.4.18">
        ///     Property created.
        /// </revision>
        [XmlAttribute("CardId")]
        public int CardId
        {
            get
            {
                return this.cardId;
            }

            set
            {
                this.cardId = value;
            }
        }

        /// <summary>
        ///     Gets or sets the contest count.
        /// </summary>
        /// <value>The contest count.</value>
        /// <externalUnit cref="contestCount"/>
        /// <externalUnit cref="Contests"/>
        /// <revision revisor="dev06" date="10/13/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        [XmlAttribute("ContestCount")]
        public int ContestCount
        {
            get
            {
                if (this.Contests != null)
                {
                    this.contestCount = this.Contests.Count;
                }

                return this.contestCount;
            }

            set
            {
                this.contestCount = value;
            }
        }

        /// <summary>
        ///     Gets or sets the contests.
        /// </summary>
        /// <value>The contests.</value>
        /// <externalUnit cref="VoterContestList"/>
        /// <externalUnit cref="contestList"/>
        /// <exception cref="ArgumentException">
        ///     This will be thrown if code attempts to set the Contest 
        ///     collection to null.
        /// </exception>
        /// <revision revisor="dev06" date="10/13/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        [XmlArray("Contests")]
        [XmlArrayItem("Contest")]
        public VoterContestList Contests
        {
            get
            {
                return this.contestList;
            }

            set
            {
                // make sure incoming value is not null
                if (value == null)
                {
                    throw new ArgumentException(
                        "Contests cannot be set to a null value.");
                }

                this.contestList = value;
            }
        }

        /// <summary>
        ///     Gets the outstack mask.
        /// </summary>
        /// <value>The outstack mask.</value>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="10/9/2009" version="1.1.1.7">
        ///     Member Created
        /// </revision>
        [XmlIgnore]
        public int OutstackMask
        {
            get
            {
                int returnValue = 0;
                foreach (OutstackCondition cond in this.OutstackConditions)
                {
                    returnValue = returnValue | (int)cond.ConditionType;
                }

                return returnValue;
            }
        }

        /// <summary>
        ///     Gets or sets the XML representation.
        /// </summary>
        /// <value>The XML representation.</value>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="10/7/2009" version="1.0.17.14">
        ///     Member Created
        /// </revision>
        [XmlIgnore]
        public string XMLRepresentation
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the outstack conditions.
        /// </summary>
        /// <value>The outstack conditions.</value>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="8/24/2009" version="1.0.16.04">
        ///     Member Created
        /// </revision>
        [XmlArray("OutstackConditions")]
        [XmlArrayItem("OutstackCondition")]
        public OutstackConditionList OutstackConditions
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        /// <value>The voter id.</value>
        /// <externalUnit cref="id"/>
        /// <revision revisor="dev06" date="10/13/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        [XmlAttribute("Id")]
        public long Id
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
        ///     Gets or sets the party id.
        /// </summary>
        /// <value>The party id.</value>
        /// <externalUnit cref="partyId"/>
        /// <revision revisor="dev06" date="10/13/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        [XmlAttribute("PartyId")]
        public int PartyId
        {
            get
            {
                return this.partyId;
            }

            set
            {
                this.partyId = value;
            }
        }

        /// <summary>
        ///     Gets or sets the precinct id.
        /// </summary>
        /// <value>The precinct id.</value>
        /// <externalUnit cref="precinctId"/>
        /// <revision revisor="dev06" date="10/13/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        [XmlAttribute("PrecinctId")]
        public int PrecinctId
        {
            get
            {
                return this.precinctId;
            }

            set
            {
                this.precinctId = value;
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether this Voter Record is 
        ///     valid.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </value>
        /// <revision revisor="dev14" date="2/11/2008" version="1.0.6.6">
        ///     Member Created
        /// </revision>
        [XmlAttribute("IsValid")]
        public bool IsValid
        {
            get;
            set;
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Sets the outstack conditions.
        /// </summary>
        /// <param name="mask">The Outstack mask.</param>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="10/9/2009" version="1.1.1.7">
        ///     Member Created
        /// </revision>
        public void SetOutstackConditions(int mask)
        {
            var myList = new OutstackConditionList();
            foreach (OutstackConditionId cond in 
                Enum.GetValues(typeof(OutstackConditionId)))
            {
                if ((mask & (int)cond) == (int)cond)
                {
                    myList.Add(new OutstackCondition(cond, false));
                }
            }

            this.OutstackConditions = myList;
        }

        /// <summary>
        ///     Return a hash value corresponding to the value of the 
        ///     voter record.
        /// </summary>
        /// <returns>The hash value</returns>
        /// <externalUnit/>
        /// <revision revisor = "dev05" date="02/18/09" version="1.0.8.1">
        ///     Method created.
        /// </revision>
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        /// <summary>
        ///     Return a string representing the value of the voter record.
        /// </summary>
        /// <returns>The string value</returns>
        /// <externalUnit/>
        /// <revision revisor = "dev05" date="02/18/09" version="1.0.8.1">
        ///     Method created.
        /// </revision>
        public override string ToString()
        {
            return String.Format("{0}:{1}", this.id, this.cardId);
        }

        /// <summary>
        ///     Determine whether a given object is equal to the current voter
        ///     record.
        /// </summary>
        /// <param name="obj">The other object</param>
        /// <returns>true if the objects are equal, false otherwise.</returns>
        /// <externalUnit/>
        /// <revision revisor = "dev05" date="02/18/09" version="1.0.8.1">
        ///     Method created.
        /// </revision>
        public override bool Equals(object obj)
        {
            bool result = false;            // Result of comparison
            var other = obj as VoterRecord;  // Other voter record, or null

            if (other != null)
            {
                result = this.Id.Equals(other.Id)
                         && this.CardId.Equals(other.CardId);
            }

            return result;
        }

        /// <summary>
        ///     Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        ///     A new object that is a copy of this instance.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="5/22/2009" version="1.0.11.12">
        ///     Added documentation header
        /// </revision>
        /// <revision revisor="dev05" date="09/11/09" version="1.0.17.4">
        ///     Renamed child object classes.
        /// </revision>
        public object Clone()
        {
            var clonedRecord = MemberwiseClone() as VoterRecord;
            if (clonedRecord != null)
            {
                var clonedContests = new VoterContestList();
                foreach (var contest in this.Contests)
                {
                    clonedContests.Add(contest.Clone() as VoterContest);
                }

                clonedRecord.Contests = clonedContests;
            }

            return clonedRecord;
        }

        #endregion

        #region Private Methods

        #endregion
    }
}
