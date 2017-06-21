// -----------------------------------------------------------------------------
// <copyright file="VoterRecordStep.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the VoterRecordStep class.
// </summary>
// <revision revisor="dev13" date="9/4/2009" version="1.0.16.14">
//     File Created
// </revision>  
// <revision revisor="dev01" date="11/11/2009" version="1.1.2.20">
//     Changed file name from VoterRecordAction to VoterRecordStep
// </revision>
// -----------------------------------------------------------------------------

namespace Sequoia.DomainObjects
{
    #region Using directives

    using System;
    using System.Xml.Serialization;

    #endregion

    /// <summary>
    ///     enum for voter record action types
    /// </summary>
    /// <revision revisor="dev01" date="11/11/2009" version="1.1.2.20">
    ///     Changed name of enum from VoterRecordActiontype to 
    ///     VoterRecordStepType, changed existing values and added 2 new values.
    /// </revision>
    public enum VoterRecordStepType
    {
        /// <summary>
        ///     add a candidate
        /// </summary>
        AddCandidate,

        /// <summary>
        ///     delete a candidate
        /// </summary>
        DeleteCandidate,

        /// <summary>
        /// Adds a contest
        /// </summary>
        AddContest,

        /// <summary>
        ///     delete a contest
        /// </summary>
        DeleteContest
    }

    /// <summary>
    ///     Class for voter record actions
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev13" date="9/4/2009" version="1.0.16.14">
    ///     Class created.
    /// </revision>
    /// <revision revisor="dev01" date="11/11/2009" version="1.1.2.20">
    ///     Changed Class name from votreRecordAction to 
    ///     VoterRecordStep, Changed XML tags, removed the Ipersistible 
    ///     interface, renamed CandidateId to AffectedId and added Reason 
    ///     and ContestVoteFor property 
    /// </revision>
    [Serializable]
    [XmlRoot("VoterRecordStep")]
    public class VoterRecordStep : DomainObject
    {
        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="VoterRecordStep"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="9/4/2009" version="1.0.16.14">
        ///     Member Created
        /// </revision> 
        public VoterRecordStep()
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the serialized data.
        /// </summary>
        /// <value>The serialized data.</value>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="9/4/2009" version="1.0.16.14">
        ///     Member Created
        /// </revision>
        public string SerializedData
        {
            get
            {
                return Serialize(this);
            }
        }

        /// <summary>
        ///     Gets or sets the type of the action.
        /// </summary>
        /// <value>The type of the action.</value>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="9/4/2009" version="1.0.16.14">
        ///     Member Created
        /// </revision>
        [XmlAttribute("Type")]
        public VoterRecordStepType Type
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the candidates.
        /// </summary>
        /// <value>The candidates.</value>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="9/4/2009" version="1.0.16.14">
        ///     Member Created
        /// </revision>
        [XmlAttribute("AffectedId")]
        public int AffectedId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the contest vote for.
        /// </summary>
        /// <value>The contest vote for.</value>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="11/11/2009" version="1.1.2.20">
        ///     Member Created
        /// </revision>
        [XmlAttribute("ContestVoteFor")]
        public int ContestVoteFor
        {
            get;
            set;
        }
        
        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>The session date.</value>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="11/11/2009" version="1.1.2.20">
        ///     Member Created
        /// </revision>
        [XmlAttribute("Date")]
        public DateTime Date
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the reason.
        /// </summary>
        /// <value>The reason.</value>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="11/11/2009" version="1.1.2.20">
        ///     Member Created
        /// </revision>
        [XmlElement("Reason")]
        public string Reason
        {
            get;
            set;
        }

        #endregion

        #region Public Methods

        #endregion

        #region Public Events

        #endregion

        #region Private Methods

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
        /// <revision revisor="dev01" date="11/12/2009" version="1.1.2.21">
        ///     Member Created
        /// </revision>
        public override bool Equals(object obj)
        {
            return this.AffectedId == ((VoterRecordStep)obj).AffectedId 
                    && this.Type == ((VoterRecordStep)obj).Type
                    && this.Reason == ((VoterRecordStep)obj).Reason 
                    && this.ContestVoteFor == ((VoterRecordStep)obj).ContestVoteFor
                    && this.Date == ((VoterRecordStep)obj).Date;
        }

        #endregion
    }
}
