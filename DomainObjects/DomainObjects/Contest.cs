// -----------------------------------------------------------------------------
// <copyright file="Contest.cs" company="Sequoia Voting Systems">
//     Copyright (c) 2008 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the Contest class.
// </summary>
// <revision revisor="dev16" date="12/22/2008" version="1.0.0.0">
//     File Created
// </revision>
// <revision revisor="dev14" date="6/15/2009" version="1.0.13.11">
//     File Modified
// </revision>
// <revision revisor="dev05" date="08/27/09" version="1.0.16.6">
//     File modified.
// </revision>
// <revision revisor="dev14" date="8/28/2009" version="1.0.16.08">
//     File Modified
// </revision>
// <revision revisor="dev01" date="10/26/2009" version="1.1.2.4">
//     File Modified
// </revision>
// <revision revisor="dev05" date="10/30/09" version="1.1.2.8">
//     File modified.
// </revision>
// <revision revisor="dev01" date="10/30/2009" version="1.1.2.8">
//     File Modified
// </revision>
// <revision revisor="dev01" date="11/16/2009" version="1.1.3.4">
//     File Modified
// </revision>
// <revision revisor="dev13" date="11/24/2009" version="1.1.3.11">
//     File Modified
// </revision>
// -----------------------------------------------------------------------------

namespace Sequoia.DomainObjects
{
    #region Using directives

    using System;
    using System.Xml.Serialization;

    #endregion

    /// <summary>
    ///     Contest type enumeration. These values need to match the ContestType
    ///     table in the database.
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev05" date="08/27/09" version="1.0.16.6">
    ///     Enum created.
    /// </revision>
    /// <revision revisor="dev13" date="11/24/2009" version="1.1.3.11">
    ///     Added selective primary type.
    /// </revision>
    public enum ContestType
    {
        /// <summary>
        ///     Standard contest
        /// </summary>
        Standard = 0,

        /// <summary>
        ///     Straight Party contest
        /// </summary>
        StraightParty = 2,

        /// <summary>
        ///     Ranked Choice Contest (IRV, STV, etc)
        /// </summary>
        RankedChoice = 3,

        /// <summary>
        ///     Proposal type contest
        /// </summary>
        Proposal = 4,

        /// <summary>
        ///     Recall Contest
        /// </summary>
        Recall = 5,

        /// <summary>
        ///     Selective primary contest
        /// </summary>
        SelectivePrimary = 6
    }

    /// <summary>
    ///     Contest represents a contest in an election definition
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev16" date="12/22/2008" version="1.0.0.0">
    ///     Class created.
    /// </revision>
    /// <revision revisor="dev14" date="6/15/2009" version="1.0.13.11">
    ///     Added attributes for validation
    /// </revision>
    /// <revision revisor="dev01" date="10/26/2009" version="1.1.2.4">
    ///     Added District, Office and StateCode Properties
    /// </revision>
    /// <revision revisor="dev01" date="10/30/2009" version="1.1.2.8">
    ///     Modify Contructor
    /// </revision>
    /// <revision revisor="dev01" date="11/16/2009" version="1.1.3.4">
    ///     Modify Party Property
    /// </revision>
    [Serializable]
    [XmlRoot("Contest")]
    public class Contest : DomainObject
    {
        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Contest"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev16" date="12/22/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public Contest() : this(0, 0, String.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Contest"/> class.
        /// </summary>
        /// <param name="id">The contest id.</param>
        /// <param name="voteFor">The vote for.</param>
        /// <param name="name">The contest name.</param>
        /// <externalUnit/>
        /// <revision revisor="dev16" date="12/22/2008" version="1.0.0.0">
        /// Member Created
        /// </revision>
        /// <revision revisor="dev06" date="12/22/2008" version="1.0.0.0">
        /// pdate constructor to init candidate collection
        /// </revision>
        /// <revision revisor="dev01" date="10/30/2009" version="1.1.2.8">
        ///     Added creation of new district
        /// </revision>
        public Contest(int id, int voteFor, string name)
        {
            this.Id = id;
            this.VoteFor = voteFor;
            this.Name = name;

            // initialize candidates
            this.Candidates = new CandidateList();

            // init for now...until we know what the real values should be...
            this.ContestType = 1;
            this.ListOrder = 1;
            this.BallotStatusId = 1;
            this.Party = new Party();
            this.District = new District();
        }

        #endregion

        #region Public Properties
        /// <summary>
        ///     Gets or sets the contest id.
        /// </summary>
        /// <value>The contest id.</value>
        /// <externalUnit/>
        /// <revision revisor="dev16" date="12/22/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        [XmlAttribute("Id")]
        public int Id
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the number of candidates to vote for.
        /// </summary>
        /// <value>The number to vote for.</value>
        /// <externalUnit/>
        /// <revision revisor="dev16" date="12/22/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev14" date="6/15/2009" version="1.0.13.11">
        ///     Added Validator Attributes
        /// </revision>
        /// <revision revisor="dev14" date="8/28/2009" version="1.0.16.08">
        ///     Moved the validator message to resource file
        /// </revision>
        [XmlAttribute("VoteFor")]
        public int VoteFor
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the candidates.
        /// </summary>
        /// <value>The candidates.</value>
        /// <externalUnit/>
        /// <revision revisor="dev16" date="12/22/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        [XmlArray("Candidates")]
        [XmlArrayItem("Candidate")]
        public CandidateList Candidates
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the contest name.
        /// </summary>
        /// <value>The contest name.</value>
        /// <externalUnit/>
        /// <revision revisor="dev16" date="12/22/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev14" date="9/29/2009" version="1.0.17.21">
        ///     Removed validation attribute to move to builder
        /// </revision>
        [XmlAttribute("Name")]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the ballot status id.
        /// </summary>
        /// <value>The ballot status id.</value>
        /// <externalUnit/>
        /// <revision revisor="dev06" date="12/22/2008" version="1.0.0.0">Member Created</revision>
        [XmlAttribute("BallotStatusId")]
        public int BallotStatusId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the type of the contest.
        /// </summary>
        /// <value>The type of the contest.</value>
        /// <externalUnit/>
        /// <revision revisor="dev06" date="12/22/2008" version="1.0.0.0">Member Created</revision>
        [XmlAttribute("ContestType")]
        public int ContestType
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the party id.
        /// </summary>
        /// <value>The party id.</value>
        /// <externalUnit/>
        /// <revision revisor="dev06" date="12/22/2008" version="1.0.0.0">Member Created</revision>
        /// <revision revisor="dev01" date="11/16/2009" version="1.1.3.4">
        ///     Changed name, type and xmlattribute to xmlelement
        /// </revision>
        [XmlElement("Party")]
        public Party Party
        {
            get;
            set;
        }
        
        /// <summary>
        /// Gets or sets the list order.
        /// </summary>
        /// <value>The list order.</value>
        /// <externalUnit/>
        /// <revision revisor="dev06" date="12/22/2008" version="1.0.0.0">Member Created</revision>
        [XmlAttribute("ListOrder")]
        public int ListOrder
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the district.
        /// </summary>
        /// <value>The district.</value>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="10/26/2009" version="1.1.2.4">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev05" date="10/30/09" version="1.1.2.8">
        ///     Serialize as element, not attribute.
        /// </revision>
        [XmlElement("District")]
        public District District
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the State code.
        /// </summary>
        /// <value>The State code.</value>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="10/26/2009" version="1.1.2.4">
        ///     Member Created
        /// </revision>
        [XmlAttribute("StateCode")]
        public string StateCode
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Office Id.
        /// </summary>
        /// <value>The Office Id.</value>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="10/26/2009" version="1.1.2.4">
        ///     Member Created
        /// </revision>
        [XmlAttribute("OfficeId")]
        public int OfficeId
        {
            get;
            set;
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <param name="obj">The <see cref="T:System.Object"/> to compare with the current <see cref="T:System.Object"/>.</param>
        /// <returns>
        /// true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
        /// </returns>
        /// <exception cref="T:System.NullReferenceException">
        /// The <paramref name="obj"/> parameter is null.
        /// </exception>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="9/1/2009" version="1.0.16.11">
        ///     Member Created
        /// </revision>
        public override bool Equals(object obj)
        {
            return this.Id.Equals(((Contest)obj).Id);
        }
        #endregion

        #region Private Methods

        #endregion
    }
}
