// -----------------------------------------------------------------------------
// <copyright file="VoterCandidate.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2008 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the VoterCandidate class.
// </summary>
// <revision revisor="dev06" date="10/13/2008" version="1.0.0.0">
//     File Created
// </revision>  
// <revision revisor="dev14" date="2/10/2009" version="1.0.6.6">
//     File modified
// </revision>  
// <revision revisor="dev13" date="8/20/2009" version="1.0.15.13">
//     File modified
// </revision>  
// <revision revisor="dev13" date="8/24/2009" version="1.0.16.04">
//     File modified
// </revision>  
// <revision revisor="dev05" date="08/27/09" version="1.0.16.6">
//     File modified.
// </revision>
// <revision revisor="dev05" date="09/11/09" version="1.0.17.4">
//     File modified.
// </revision>
// <revision revisor="dev13" date="11/24/2009" version="1.1.3.11">
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
    ///     VoterCandidate represents a candidate on a ballot.
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev06" date="10/13/2008" version="1.0.0.0">
    ///     Class created.
    /// </revision>
    /// <revision revisor="dev05" date="09/11/09" version="1.0.17.4">
    ///     Moved in from BallotStorage and renamed from "Candidate".
    /// </revision>
    [Serializable]
    [XmlRoot("Candidate")]
    public class VoterCandidate : ICloneable
    {
        #region Fields

        /// <summary>
        ///     Create var for candidate id
        /// </summary>
        private int id = 0;

        #endregion
        
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="VoterCandidate"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev06" date="10/13/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision> 
        public VoterCandidate()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="VoterCandidate"/> class.
        /// </summary>
        /// <param name="id">The candidate id.</param>
        /// <externalUnit cref="Id"/>
        /// <revision revisor="dev06" date="10/13/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public VoterCandidate(int id)
        {
            // set the properties
            this.Id = id;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        /// <value>The candidate id.</value>
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
        ///     Gets or sets the candidate type.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev05" date="08/27/09" version="1.0.16.6">
        ///     Property created.
        /// </revision>
        [XmlAttribute("CandidateType")]
        public CandidateType CandidateType
        {
            get;

            set;
        }

        /// <summary>
        ///     Gets or sets the name of the write in.
        /// </summary>
        /// <value>The name of the write in.</value>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="8/20/2009" version="1.0.15.13">
        ///     Member Created
        /// </revision>
        [XmlAttribute("WriteInName")]
        public string WriteInName
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the value. Value represents the ranking
        ///     or range value in a proportional election.
        /// </summary>
        /// <value>The name of the write in.</value>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/24/2009" version="1.1.3.11">
        ///     Member Created
        /// </revision>
        [XmlAttribute("Value")]
        public double Value
        { 
            get;
            set;
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
        public object Clone()
        {
            return MemberwiseClone() as VoterCandidate;
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
        ///     The <paramref name="obj"/> parameter is null.
        /// </exception>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="10/9/2009" version="1.1.1.9">
        ///     Member Created
        /// </revision>
        public override bool Equals(object obj)
        {
            return this.Id == ((VoterCandidate)obj).Id;
        }

        #endregion

        #region Public Events

        #endregion

        #region Private Methods

        #endregion
    }
}
