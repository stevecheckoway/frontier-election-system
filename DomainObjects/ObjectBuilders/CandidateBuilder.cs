// -----------------------------------------------------------------------------
// <copyright file="CandidateBuilder.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the Candidate class.
// </summary>
// <revision revisor="dev14" date="9/24/2009" version="1.0.17.16">
//     File created
// </revision>
// -----------------------------------------------------------------------------

namespace Sequoia.DomainObjects.ObjectBuilders
{
    #region Using directives

    using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
    using Properties;

    #endregion

    /// <summary>
    ///     Is used to create a <see cref="Candidate" /> instance
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev14" date="9/24/2009" version="1.0.17.16">
    ///     Member Created
    /// </revision>
    public class CandidateBuilder : ISequoiaBuilder<Candidate>
    {
        #region fields

        /// <summary>
        ///     The order the candidate appears in in a list
        /// </summary>
        private int listOrder;

        /// <summary>
        ///     A reference to a candidate if we are to update one.
        /// </summary>
        private Candidate candidate;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="CandidateBuilder"/> class.
        /// </summary>
        /// <externalUnit cref="CandidateBuilder(Candidate)"/>
        /// <revision revisor="dev14" date="9/28/2009" version="1.0.17.20">
        ///     Member Created
        /// </revision>
        public CandidateBuilder() : this(new Candidate())
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="CandidateBuilder"/> class.
        /// </summary>
        /// <param name="candidate">The candidate.</param>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="9/28/2009" version="1.0.17.20">
        ///     Member Created
        /// </revision>
        public CandidateBuilder(Candidate candidate)
        {
            // set the builder's reference to the given candidate
            this.candidate = candidate;
            
            // set the local Name property
            this.Name = candidate.Name;

            // set the local value for the candidate type
            this.CandidateType = candidate.CandidateType;

            // set the local party id property
            this.PartyId = candidate.PartyId;
            
            // set the local contest id property
            this.ContestId = candidate.ContestId;

            // set the local ballot status property
            this.BallotStatusId = candidate.BallotStatusId;
            
            // set the local list order property
            this.ListOrder = candidate.ListOrder;
        }

        #endregion

        /// <summary>
        ///     Gets or sets the type of the candidate.
        /// </summary>
        /// <value>The type of the candidate.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="9/28/2009" version="1.0.17.20">
        ///     Member Created
        /// </revision>
        public int CandidateType
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the candidate's contest id.
        /// </summary>
        /// <value>The contest id.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="9/28/2009" version="1.0.17.20">
        ///     Member Created
        /// </revision>
        public int ContestId
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the candidate name.
        /// </summary>
        /// <value>The name of the candidate.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="9/24/2009" version="1.0.17.16">
        ///     Member Created
        /// </revision>
        [StringLengthValidator(
            1,
            100,
            MessageTemplateResourceName = "CandidateNameLengthMessage",
            MessageTemplateResourceType = typeof(Resources))]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the list order.
        /// </summary>
        /// <value>The list order.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="9/24/2009" version="1.0.17.16">
        ///     Member Created
        /// </revision>
        [RangeValidator(
            1, RangeBoundaryType.Inclusive,
            999, RangeBoundaryType.Ignore,
            MessageTemplateResourceName = "ListOrderMessage",
            MessageTemplateResourceType = typeof(Resources))]
        public int ListOrder
        {
            get
            {
                // the minimum listOrder is 1
                return this.listOrder < 1 ? 1 : this.listOrder;
            }

            set
            {
                this.listOrder = value;
            }
        }

        /// <summary>
        ///     Gets or sets the party id of the candidate
        /// </summary>
        /// <value>The party id.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="9/28/2009" version="1.0.17.20">
        ///     Member Created
        /// </revision>
        public int PartyId
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the ballot status id that determines whether a
        ///     candidate should be displayed on the ballot
        /// </summary>
        /// <value>The ballot status id.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="9/28/2009" version="1.0.17.20">
        ///     Member Created
        /// </revision>
        public int BallotStatusId
        {
            get;
            set;
        }

        #region Implementation of ISequoiaBuilder<T>

        /// <summary>
        ///     Builds an instance of a <see cref="Candidate" />.
        /// </summary>
        /// <returns>The candidate instance</returns>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="9/24/2009" version="1.0.17.16">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev14" date="9/28/2009" version="1.0.17.20">
        ///     Set additional properties
        /// </revision>
        public Candidate Build()
        {
            // set the candidate's name to the local value
            this.candidate.Name = this.Name;

            // set the candidate's type to the local value
            this.candidate.CandidateType = this.CandidateType;

            // set the candidate's list order to the local value
            this.candidate.ListOrder = this.ListOrder;
            
            // set the candidate's party to the local value
            this.candidate.PartyId = this.PartyId;

            // set the candidate's contest to the local value
            this.candidate.ContestId = this.ContestId;

            // set the candidate's ballot status to the local value
            this.candidate.BallotStatusId = this.BallotStatusId;

            // return the candidate
            return this.candidate;
        }

        #endregion
    }
}
