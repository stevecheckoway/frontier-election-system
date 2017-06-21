// -----------------------------------------------------------------------------
// <copyright file="ContestBuilder.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the ContestBuilder class.
// </summary>
// <revision revisor="dev14" date="9/29/2009" version="1.0.17.21">
//     File Created
// </revision>
// <revision revisor="dev01" date="10/28/2009" version="1.1.2.6">
//     File Modified
// </revision>
// <revision revisor="dev01" date="11/16/2009" version="1.1.3.4">
//     File Modified
// </revision>
// -----------------------------------------------------------------------------

namespace Sequoia.DomainObjects.ObjectBuilders
{
    #region Using directives

    using System;
    using Microsoft.Practices.EnterpriseLibrary.Validation;
    using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
    using Properties;

    #endregion

    /// <summary>
    ///     ContestBuilder is a class that creates 
    ///     a <see cref="Contest" /> instance
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev14" date="9/29/2009" version="1.0.17.21">
    ///     Class created.
    /// </revision>
    /// <revision revisor="dev01" date="11/16/2009" version="1.1.3.4">
    ///     Modify build() methods
    /// </revision>
    public class ContestBuilder : ISequoiaBuilder<Contest>
    {
        #region Fields

        /// <summary>
        ///     The order the contest appears in in a list
        /// </summary>
        private int listOrder;

        /// <summary>
        ///     A reference to a candidate if we are to update one.
        /// </summary>
        private Contest contest;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ContestBuilder"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="9/29/2009" version="1.0.17.21">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev01" date="10/28/2009" version="1.1.2.6">
        ///     Added distrcitId
        /// </revision>
        public ContestBuilder() : this(new Contest())
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ContestBuilder"/> class.
        /// </summary>
        /// <param name="contest">The contest.</param>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="9/29/2009" version="1.0.17.21">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev01" date="11/16/2009" version="1.1.3.4">
        ///     Modify Party fetching
        /// </revision>
        public ContestBuilder(Contest contest)
        {
            // set the builder's reference to the given candidate
            this.contest = contest;
            
            // set the local Name property
            this.Name = contest.Name;

            // set the local value for the contest type
            this.ContestType = contest.ContestType;

            // set the local party id property
            this.PartyId = contest.Party.Id;
            
            // set the local vote for property
            this.VoteFor = contest.VoteFor;

            // set the local ballot status property
            this.BallotStatusId = contest.BallotStatusId;
            
            // set the local list order property
            this.ListOrder = contest.ListOrder;

            // set the distrcitId
            this.DistrictId = contest.District.Id;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the contest name.
        /// </summary>
        /// <value>The contest name.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="9/29/2009" version="1.0.17.21">
        ///     Member Created
        /// </revision>
        [StringLengthValidator(
            1,
            100,
            MessageTemplateResourceName = "ContestNameLengthMessage",
            MessageTemplateResourceType = typeof(Resources))]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the ballot status id.
        /// </summary>
        /// <value>The ballot status id.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="9/29/2009" version="1.0.17.21">
        ///     Member Created
        /// </revision>
        public int BallotStatusId
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the District id.
        /// </summary>
        /// <value>The District id.</value>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="10/28/2009" version="1.1.2.6">
        ///     Member Created
        /// </revision>
        public int DistrictId
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the type of the contest.
        /// </summary>
        /// <value>The type of the contest.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="9/29/2009" version="1.0.17.21">
        ///     Member Created
        /// </revision>
        public int ContestType
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the list order.
        /// </summary>
        /// <value>The list order.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="9/29/2009" version="1.0.17.21">
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
        ///     Gets or sets the party id.
        /// </summary>
        /// <value>The party id.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="9/29/2009" version="1.0.17.21">
        ///     Member Created
        /// </revision>
        public int PartyId
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the number of candidates to vote for.
        /// </summary>
        /// <value>The number to vote for.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="9/29/2009" version="1.0.17.21">
        ///     Member Created
        /// </revision>
        [NotNullValidator]
        public int VoteFor
        {
            get;
            set;
        }

        #endregion

        #region Public Methods

        #region Implementation of ISequoiaBuilder<T>

        /// <summary>
        ///     Builds an instance of a <see cref="Contest"/>.
        /// </summary>
        /// <returns>The contest instance</returns>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="9/29/2009" version="1.0.17.21">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev01" date="11/16/2009" version="1.1.3.4">
        ///     Changed Party fetching
        /// </revision>
        public Contest Build()
        {
            // set the contest's name to the local value
            this.contest.Name = this.Name;

            // set the contest's type to the local value
            this.contest.ContestType = this.ContestType;

            // set the contest's list order to the local value
            this.contest.ListOrder = this.ListOrder;
       
            // set the contest's ballot status to the local value
            this.contest.BallotStatusId = this.BallotStatusId;

            // set the contest's vote for value to the local value
            this.contest.VoteFor = this.VoteFor;

            // return the contest
            return this.contest;
        }

        /// <summary>
        ///     Builds the specified districts.
        /// </summary>
        /// <param name="districts">
        ///     The districts.
        /// </param>
        /// <param name="parties">
        ///     The parties.
        /// </param>
        /// <returns>
        ///     The contest Object
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="10/28/2009" version="1.1.2.6">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev01" date="11/16/2009" version="1.1.3.4">
        ///     Changed Party fetching and added new parameter (list of parties)
        /// </revision>
        public Contest Build(
            DomainObjectList<District> districts,
            DomainObjectList<Party> parties)
        {
            // set the contest's name to the local value
            this.contest.Name = this.Name;

            // set the contest's type to the local value
            this.contest.ContestType = this.ContestType;

            // set the contest's list order to the local value
            this.contest.ListOrder = this.ListOrder;

            if (parties.Exists(d => d.Id == this.PartyId))
            {
                this.contest.Party = parties.Find(d => d.Id == this.PartyId);
            }
            else
            {
                this.contest.Party =
                    new Party(
                        this.PartyId, string.Empty, string.Empty, 0, false);
            }

            // set the contest's ballot status to the local value
            this.contest.BallotStatusId = this.BallotStatusId;

            // set the contest's vote for value to the local value
            this.contest.VoteFor = this.VoteFor;

            if (districts.Exists(d => d.Id == this.DistrictId))
            {
                this.contest.District =
                    districts.Find(d => d.Id == this.DistrictId);
            }
            else
            {
                this.contest.District = new District();
            }

            // return the contest
            return this.contest;
        }

        #endregion

        #endregion

        #region Public Events

        #endregion

        #region Private Methods

        #endregion
    }
}
