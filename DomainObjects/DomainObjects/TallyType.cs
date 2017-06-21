// -----------------------------------------------------------------------------
// <copyright file="TallyType.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the TallyType class.
// </summary>
// <revision revisor="dev14" date="9/15/2009" version="1.0.17.08">
//     File Created
// </revision>
// -----------------------------------------------------------------------------

namespace Sequoia.DomainObjects
{
    #region Using directives

    using System;

    #endregion

    /// <summary>
    ///     The Tally category groups tally types to give rules for their use
    ///     The TallyCategory table in the election database should have the same
    ///     relavent values
    /// </summary>
    /// <revision revisor="dev14" date="9/15/2009" version="1.0.17.08">
    ///     Class created.
    /// </revision>
    public enum TallyCategory
    {
        /// <summary>
        /// Used day of election
        /// </summary>
        ElectionDay = 1,

        /// <summary>
        /// Used to tally absentee votes
        /// </summary>
        Absentee = 2,

        /// <summary>
        /// Used to tally early votes
        /// </summary>
        EarlyVote = 3
    }

    /// <summary>
    ///     TallyType is a class that represents how and when a machine is
    ///     deployed
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev14" date="9/15/2009" version="1.0.17.08">
    ///     Class created.
    /// </revision>
    public class TallyType : DomainObject
    {
        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="TallyType"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="9/15/2009" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public TallyType()
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the tally category id.
        /// </summary>
        /// <value>The tally category id.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="9/15/2009" version="1.0.17.08">
        ///     Member Created
        /// </revision>
        public TallyCategory TallyCategory
        {
            get; 
            set;
        }

        /// <summary>
        ///     Gets or sets the name of the tally type
        /// </summary>
        /// <value>The name of the tally type.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="9/15/2009" version="1.0.17.08">
        ///     Member Created
        /// </revision>
        public string Name
        {
            get; 
            set;
        }

        /// <summary>
        ///     Gets or sets the order in which the tally type should appear 
        ///     when displayed in a list
        /// </summary>
        /// <value>The list order.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="9/15/2009" version="1.0.17.08">
        ///     Member Created
        /// </revision>
        public int ListOrder
        {
            get; 
            set;
        }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is rotated.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is rotated; otherwise, <c>false</c>.
        /// </value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="9/15/2009" version="1.0.17.08">
        ///     Member Created
        /// </revision>
        public bool IsRotated
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

        #endregion
    }
}
