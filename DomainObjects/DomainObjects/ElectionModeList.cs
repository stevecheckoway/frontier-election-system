// -----------------------------------------------------------------------------
// <copyright file="ElectionModeList.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the ElectionModeList class.
// </summary>
// <revision revisor="dev06" date="4/19/2009" version="1.0.11.7">
//     File Created
// </revision>  
// -----------------------------------------------------------------------------

namespace Sequoia.DomainObjects
{
    using System.Collections.Generic;

    /// <summary>
    ///     ElectionModeList is a <see cref="List{T}" /> of <see cref="ElectionMode" /> objects. 
    /// </summary>
    /// <externalUnit cref="ElectionMode"/>
    /// <revision revisor="dev06" date="4/19/2009" version="1.0.0.0">
    ///     Class created.
    /// </revision>
    public class ElectionModeList : List<ElectionMode>
    {
        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ElectionModeList"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev06" date="4/19/2009" version="1.0.11.7">
        ///     Member Created
        /// </revision> 
        public ElectionModeList()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ElectionModeList"/> class.
        /// </summary>
        /// <param name="capacity">
        /// The capacity.
        /// </param>
        /// <externalUnit cref="List{T}(int)"/>
        /// <revision revisor="dev06" date="4/19/2009" version="1.0.11.7">
        ///     Member Created
        /// </revision>
        public ElectionModeList(int capacity)
            : base(capacity)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ElectionModeList"/> class.
        /// </summary>
        /// <param name="electionModes">
        /// The election Modes.
        /// </param>
        /// <externalUnit cref="ElectionMode"/>
        /// <externalUnit cref="List{T}(IEnumerable{T})"/>
        /// <externalUnit cref="IEnumerable{T}"/>
        /// <revision revisor="dev06" date="4/19/2009" version="1.0.11.7">
        ///     Member Created
        /// </revision>
        public ElectionModeList(IEnumerable<ElectionMode> electionModes)
            : base(electionModes)
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
