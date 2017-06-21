// -----------------------------------------------------------------------------
// <copyright file="OutstackCondition.cs" company="Sequoia Voting Systems">
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the OutstackCondition class.
// </summary>
// <revision revisor="dev13" date="8/24/2009" version="1.0.16.04">
//     File Created
// </revision>
// <revision revisor="dev13" date="8/25/2009" version="1.0.16.05">
//     File modified
// </revision>
// <revision revisor="dev05" date="08/27/09" version="1.0.16.6">
//     File modified.
// </revision>
// <revision revisor="dev13" date="08/31/09" version="1.0.16.10">
//     File modified.
// </revision>
// <revision revisor="dev05" date="09/03/09" version="1.0.16.13">
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
    ///     enum for outstack condition types
    /// </summary>
    public enum OutstackConditionId
    {
        /// <summary>
        ///     undefined outstack condition -- 1
        /// </summary>
        Undefined = 1 << 0,

        /// <summary>
        ///     overvote outstack condition -- 2
        /// </summary>
        Overvote = 1 << 1,

        /// <summary>
        ///     undervote outstack condition -- 4
        /// </summary>
        Undervote = 1 << 2,

        /// <summary>
        ///     write-in outstack condition -- 8
        /// </summary>
        WriteIn = 1 << 3,

        /// <summary>
        ///     provisional outstack condition -- 16
        /// </summary>
        Provisional = 1 << 4,

        /// <summary>
        ///     challenge outstack condition -- 32
        /// </summary>
        Challenge = 1 << 5,

        /// <summary>
        ///     blank ballot or contest outstack condition -- 64
        /// </summary>
        Blank = 1 << 6,

        /// <summary>
        ///     broken straight party outstack condition -- 128
        /// </summary>
        BrokenStraightParty = 1 << 7,

        /// <summary>
        ///     party crossover primary outstack condition -- 256
        /// </summary>
        PartyCrossoverPrimary = 1 << 8,

        /// <summary>
        ///     Invalid ballot (failed UV) -- 512
        /// </summary>
        InvalidBallot = 1 << 9
    }

    /// <summary>
    ///     Domain object for an outstack condition
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev13" date="8/24/2009" version="1.0.16.04">
    ///     Class created.
    /// </revision>
    [Serializable]
    [XmlRoot("OutstackCondition")]
    public class OutstackCondition : DomainObject
    {
        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="OutstackCondition"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="8/24/2009" version="1.0.16.04">
        ///     Member Created
        /// </revision>
        public OutstackCondition() :
            this(OutstackConditionId.Undefined, false)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="OutstackCondition"/> class.
        /// </summary>
        /// <param name="conditionType">Type of the condition.</param>
        /// <externalUnit/>
        /// <revision revisor="dev05" date="08/27/09" version="1.0.16.6">
        ///     Method created.
        /// </revision>
        public OutstackCondition(OutstackConditionId conditionType) :
            this(conditionType, false)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="OutstackCondition"/> class.
        /// </summary>
        /// <param name="conditionType">Type of the condition.</param>
        /// <param name="adjudicated">if set to <c>true</c> [adjudicated].</param>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="8/24/2009" version="1.0.16.04">
        ///     Member Created
        /// </revision>
        public OutstackCondition(OutstackConditionId conditionType, bool adjudicated)
        {
            this.ConditionType = conditionType;
            this.Adjudicated = adjudicated;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the type of the condition.
        /// </summary>
        /// <value>The type of the condition.</value>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="8/24/2009" version="1.0.16.04">
        ///     Member Created
        /// </revision>
        [XmlAttribute("ConditionType")]
        public OutstackConditionId ConditionType
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets a value indicating whether this <see cref="OutstackCondition"/> is adjudicated.
        /// </summary>
        /// <value><c>true</c> if adjudicated; otherwise, <c>false</c>.</value>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="8/24/2009" version="1.0.16.04">
        ///     Member Created
        /// </revision>
        [XmlAttribute("Adjudicated")]
        public bool Adjudicated
        {
            get;
            set;
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="8/31/2009" version="1.0.16.10">
        ///     Member Created
        /// </revision>
        public override string ToString()
        {
            return this.ConditionType.ToString();
        }

        #endregion

        #region Public Events

        #endregion

        #region Private Methods

        #endregion
    }
}
