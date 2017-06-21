// -----------------------------------------------------------------------------
// <copyright file="Candidate.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2008 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the Candidate class.
// </summary>
// <revision revisor="dev16" date="12/22/2008" version="1.0.0.0">
//     File created
// </revision>
// <revision revisor="dev13" date="8/10/2009" version="1.0.15.04">
//     File modified
// </revision>    
// <revision revisor="dev05" date="08/27/09" version="1.0.16.6">
//     File modified.
// </revision>
// <revision revisor="dev14" date="9/29/2009" version="1.0.17.21">
//     File modified.
// </revision>
// -----------------------------------------------------------------------------

namespace Sequoia.DomainObjects
{
    #region Using directives

    using System;
    using System.Xml.Serialization;

    #endregion

    /// <summary>
    ///     Candidate type enumeration. These values need to match the
    ///     CandidateType table in the database.
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev05" date="08/27/09" version="1.0.16.6">
    ///     Enumeration created.
    /// </revision>
    /// <revision revisor="dev05" date="08/27/09" version="1.0.16.6">
    ///     Renamed Response from ProposalResponse, to agree with previous enum.
    /// </revision>
    public enum CandidateType
    {
        /// <summary>Regular candidate</summary>
        Standard = 0,

        /// <summary>Straight party candidate</summary>
        StraightParty = 1,

        /// <summary>Cross-endorsed candidate</summary>
        Endorsed = 2,

        /// <summary>Write-in candidate</summary>
        WriteIn = 3,

        /// <summary>Proposal response</summary>
        Response = 4,

        /// <summary>pick-a-party type</summary>
        SelectivePrimary = 5,

        /// <summary>None of these candidates</summary>
        None = 6,

        /// <summary>Proportional voting candidate</summary>
        Proportional = 7,

        /// <summary>Resolved write-in candidate</summary>
        ResolvedWriteIn = 9,

        /// <summary>Print-only candidate</summary>
        PrintOnly = 255
    }

    /// <summary>
    /// Candidate represents a candidate in an election definition
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev16" date="12/22/2008" version="1.0.0.0">
    ///     Class created.
    /// </revision>
    [Serializable]
    [XmlRoot("Candidate")]
    public class Candidate : DomainObject
    {
        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Candidate"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev16" date="12/22/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public Candidate() : this(0, string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Candidate"/> class.
        /// </summary>
        /// <param name="id">The candidate id.</param>
        /// <param name="name">The candidate name.</param>
        /// <externalUnit/>
        /// <revision revisor="dev16" date="12/22/2008" version="1.0.0.0">
        /// Member Created
        /// </revision>
        public Candidate(int id, string name)
        {
            // set the candidate's id
            this.Id = id;
            
            // set the candidate's name
            this.Name = name;

            // set the candidate's contest
            this.ContestId = 0;
        }

        #endregion

        #region Public Properties
        /// <summary>
        ///     Gets or sets the candidate id.
        /// </summary>
        /// <value>The candidate id.</value>
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
        ///     Gets or sets the candidate name.
        /// </summary>
        /// <value>The candidate name.</value>
        /// <externalUnit/>
        /// <revision revisor="dev16" date="12/22/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev14" date="8/28/2009" version="1.0.16.08">
        ///     Added validation attribute
        /// </revision>
        /// <revision revisor="dev14" date="9/29/2009" version="1.0.17.21">
        ///     Removed validation to move into the builder class
        /// </revision>
        [XmlAttribute("Name")]
        public string Name
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
        [XmlAttribute("PartyId")]
        public int PartyId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the type of the candidate.
        /// </summary>
        /// <value>The type of the candidate.</value>
        /// <externalUnit/>
        /// <revision revisor="dev06" date="12/22/2008" version="1.0.0.0">Member Created</revision>
        [XmlAttribute("CandidateType")]
        public int CandidateType
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
        ///     Gets or sets the name of the write in. This is only filled if 
        ///     an audio write-in has occurred on this candidate id
        /// </summary>
        /// <value>The name of the write in.</value>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="8/10/2009" version="1.0.15.04">
        ///     Member Created
        /// </revision>
        [XmlAttribute("WriteInName")]
        public string WriteInName
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the contest id.
        /// </summary>
        /// <value>The contest id.</value>
        /// <externalUnit/>
        /// <revision revisor="dev06" date="12/22/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        [XmlIgnore]
        public int ContestId
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the list order.
        /// </summary>
        /// <value>The list order.</value>
        /// <externalUnit/>
        /// <revision revisor="dev06" date="12/22/2008" version="1.0.15.04">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev14" date="8/28/2009" version="1.0.16.08">
        ///     Added validation attribute
        /// </revision>
        /// <revision revisor="dev14" date="9/29/2009" version="1.0.17.21">
        ///     Removed validation to move into the builder class
        /// </revision>
        [XmlIgnore]
        public int ListOrder
        {
            get;
            set;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="9/3/2009" version="1.0.16.13">
        ///     Member Created
        /// </revision>
        public override string ToString()
        {
            return this.Name;
        }

        #endregion

        #region Public Events

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
            return this.Id.Equals(((Candidate)obj).Id);
        }
        #endregion

        #region Private Methods

        #endregion
    }
}
