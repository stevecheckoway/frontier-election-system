// -----------------------------------------------------------------------------
// <copyright file="MachineList.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the MachineList class.
// </summary>
// <revision revisor="dev16" date="1/28/2009" version="1.0.5.9">
//     File Created
// </revision>  
// -----------------------------------------------------------------------------

namespace Sequoia.DomainObjects
{
     using System.Collections.Generic;

    /// <summary>
    ///     MachineList is a <see cref="List{T}" /> of <see cref="Machine" /> objects. 
    /// </summary>
    /// <externalUnit cref="Machine"/>
    /// <revision revisor="dev16" date="1/28/2009" version="1.0.5.9">
    ///     Class created.
    /// </revision>
    public class MachineList : DomainObjectList<Machine>
    {
        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="MachineList"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev16" date="1/28/2009" version="1.0.5.9">
        ///     Member Created
        /// </revision> 
        public MachineList()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="MachineList"/> class.
        /// </summary>
        /// <param name="capacity">
        ///     The capacity.
        /// </param>
        /// <externalUnit cref="List{T}(int)"/>
        /// <revision revisor="dev16" date="1/28/2009" version="1.0.5.9">
        ///     Member Created
        /// </revision>
        public MachineList(int capacity)
            : base(capacity)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="MachineList"/> class.
        /// </summary>
        /// <param name="machines">
        ///     The machines.
        /// </param>
        /// <externalUnit cref="Machine"/>
        /// <externalUnit cref="List{T}(IEnumerable{T})"/>
        /// <externalUnit cref="IEnumerable{T}"/>
        /// <revision revisor="dev16" date="1/28/2009" version="1.0.5.9">
        ///     Member Created
        /// </revision>
        public MachineList(IEnumerable<Machine> machines) : base(machines)
        {
        }

        #endregion

        #region Public Properties

        #endregion

        #region Public Methods

        #endregion

        #region Public Events

        #endregion

        #region Private Methods

        #endregion
    }
}
