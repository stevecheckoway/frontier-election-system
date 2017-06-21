// -----------------------------------------------------------------------------
// <copyright file="MachineAssignment.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the MachineAssignment class.
// </summary>
// <revision revisor="dev14" date="3/9/2009" version="1.0.8.19">
//     File Created
// </revision>
// -----------------------------------------------------------------------------

namespace Sequoia.DomainObjects
{
    using System;

    /// <summary>
    ///     MachineAssignment is a class that represents the assignment of
    ///     a <see cref="Machine" /> to a <see cref="ReportGroup" />
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev14" date="3/9/2009" version="1.0.8.19">
    ///     Class created.
    /// </revision>
    public class MachineAssignment : DomainObject
    {
        #region Fields

        #endregion

        #region Constructors

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        /// <value>The Machine Assignment id.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="3/9/2009" version="1.0.8.19">
        ///     Member Created
        /// </revision>
        public int Id
        {
            get; 
            set;
        }

        /// <summary>
        ///     Gets or sets the machine id.
        /// </summary>
        /// <value>The machine id.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="3/9/2009" version="1.0.8.19">
        ///     Member Created
        /// </revision>
        public int MachineId
        {
            get; 
            set;
        }

        /// <summary>
        ///     Gets or sets the location id.  This is the id of the 
        /// <see cref="ReportGroup" /> that the machine is assigned to
        /// </summary>
        /// <value>The location id.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="3/9/2009" version="1.0.8.19">
        ///     Member Created
        /// </revision>
        public int LocationId
        {
            get; 
            set;
        }

        /// <summary>
        ///     Gets or sets the tally type id.
        /// </summary>
        /// <value>The tally type id.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="3/9/2009" version="1.0.8.19">
        ///     Member Created
        /// </revision>
        public int TallyTypeId
        {
            get; 
            set;
        }

        /// <summary>
        ///     Gets or sets the burn date.
        /// </summary>
        /// <value>The burn date.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="3/9/2009" version="1.0.8.19">
        ///     Member Created
        /// </revision>
        public DateTime BurnDate
        {
            get; 
            set;
        }

        /// <summary>
        ///     Gets or sets the session range value.
        /// </summary>
        /// <value>The session range value.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="3/9/2009" version="1.0.8.19">
        ///     Member Created
        /// </revision>
        public int SessionRangeValue
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
