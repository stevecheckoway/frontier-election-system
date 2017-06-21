// -----------------------------------------------------------------------------
// <copyright file="PartyBuilder.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the PartyBuilder class.
// </summary>
// <revision revisor="dev14" date="9/29/2009" version="1.0.17.21">
//     File Created
// </revision>
// <revision revisor="dev01" date="11/16/2009" version="1.1.3.4">
//     File Modified
// </revision>
// -----------------------------------------------------------------------------

namespace Sequoia.DomainObjects.ObjectBuilders
{
    #region Using directives

    using System;
    using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
    using Properties;

    #endregion

    /// <summary>
    ///     PartyBuilder is a class that creates a <see cref="Party" /> instance
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev14" date="9/29/2009" version="1.0.17.21">
    ///     Class created.
    /// </revision>
    /// <revision revisor="dev01" date="11/16/2009" version="1.1.3.4">
    ///     Added non-partisan attribute
    /// </revision>
    public class PartyBuilder : ISequoiaBuilder<Party>
    {
        #region Fields

        /// <summary>
        ///     The order the contest appears in in a list
        /// </summary>
        private int listOrder;

        /// <summary>
        ///     A reference to a party if we are to update one.
        /// </summary>
        private Party party;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="PartyBuilder"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="9/29/2009" version="1.0.17.21">
        ///     Member Created
        /// </revision>
        public PartyBuilder() : this(new Party())
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="PartyBuilder"/> class.
        /// </summary>
        /// <param name="party">The party.</param>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="9/29/2009" version="1.0.17.21">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev01" date="11/16/2009" version="1.1.3.4">
        ///     Added non-partisan attribute
        /// </revision>
        public PartyBuilder(Party party)
        {
            // set the builder's reference to the given candidate
            this.party = party;
            
            // set the local Name property
            this.Name = party.Name;

            // set the local ballot status property
            this.Abbreviation = party.Abbreviation;
            
            // set the local list order property
            this.ListOrder = party.ListOrder;

            // set the local non-partisan attribute
            this.IsNonPartisan = party.IsNonPartisan;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the party name.
        /// </summary>
        /// <value>The party name.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="9/29/2009" version="1.0.17.21">
        ///     Member Created
        /// </revision>
        [StringLengthValidator(
            1,
            100,
            MessageTemplateResourceName = "PartyNameLengthMessage",
            MessageTemplateResourceType = typeof(Resources))]
        public string Name
        {
            get; 
            set;
        }

        /// <summary>
        ///     Gets or sets the party abbreviation.
        /// </summary>
        /// <value>The party abbreviation.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="9/29/2009" version="1.0.17.21">
        ///     Member Created
        /// </revision>
        [StringLengthValidator(
            1,
            10,
            MessageTemplateResourceName = "PartyAbbreviationLengthMessage",
            MessageTemplateResourceType = typeof(Resources))]
        public string Abbreviation
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is 
        ///     non partisan.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is non partisan; 
        ///     otherwise, <c>false</c>.
        /// </value>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="11/16/2009" version="1.1.3.4">
        ///     Member Created
        /// </revision>
        public bool IsNonPartisan
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
        ///     Added non-partisan attribute
        /// </revision>
        public Party Build()
        {
            // set the party's name to the local value
            this.party.Name = this.Name;

            // set the party's abbreviation to the local value
            this.party.Abbreviation = this.Abbreviation;

            // set the party's list order to the local value
            this.party.ListOrder = this.ListOrder;

            // set party's non-partisan attribute
            this.party.IsNonPartisan = this.IsNonPartisan;

            // return the party
            return this.party;
        }

        #endregion

        #endregion

        #region Public Events

        #endregion

        #region Private Methods

        #endregion
    }
}
