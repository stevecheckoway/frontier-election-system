// -----------------------------------------------------------------------------
// <copyright file="MachineTypeList.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the MachineTypeList class.
// </summary>
// <revision revisor="dev06" date="4/19/2009" version="1.0.11.7">
//     File Created
// </revision>  
// -----------------------------------------------------------------------------

namespace Sequoia.DomainObjects
{
    #region Using directives

    using System.Collections.Generic;

    #endregion

    /// <summary>
    ///     MachineTypeList is a <see cref="List{T}" /> of <see cref="MachineType" /> objects. 
    /// </summary>
    /// <externalUnit cref="MachineType"/>
    /// <revision revisor="dev06" date="4/19/2009" version="1.0.11.7">
    ///     Class created.
    /// </revision>
    public class MachineTypeList : List<MachineType>
    {
        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="MachineTypeList"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev06" date="4/19/2009" version="1.0.11.7">
        ///     Member Created
        /// </revision> 
        public MachineTypeList()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MachineTypeList"/> class.
        /// </summary>
        /// <param name="capacity">
        /// The capacity.
        /// </param>
        /// <externalUnit cref="List{T}(int)"/>
        /// <revision revisor="dev06" date="4/19/2009" version="1.0.11.7">
        ///     Member Created
        /// </revision>
        public MachineTypeList(int capacity)
            : base(capacity)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MachineTypeList"/> class.
        /// </summary>
        /// <param name="machineTypes">
        /// The machine Types.
        /// </param>
        /// <externalUnit cref="MachineType"/>
        /// <externalUnit cref="List{T}(IEnumerable{T})"/>
        /// <externalUnit cref="IEnumerable{T}"/>
        /// <revision revisor="dev06" date="4/19/2009" version="1.0.11.7">
        ///     Member Created
        /// </revision>
        public MachineTypeList(IEnumerable<MachineType> machineTypes)
            : base(machineTypes)
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
