// -----------------------------------------------------------------------------
// <copyright file="OutstackConditionList.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the OutstackConditionList class.
// </summary>
// <revision revisor="dev13" date="8/24/2009" version="1.0.16.04">
//     File Created
// </revision>  
// -----------------------------------------------------------------------------

namespace Sequoia.DomainObjects
{
    #region Using directives

    using System.Collections.Generic;

    #endregion

    /// <summary>
    ///     OutstackConditionList is a <see cref="List{T}" /> of <see cref="OutstackCondition" /> objects. 
    /// </summary>
    /// <externalUnit cref="OutstackCondition"/>
    /// <revision revisor="dev13" date="8/24/2009" version="1.0.16.04">
    ///     Class created.
    /// </revision>
    public class OutstackConditionList : List<OutstackCondition>
    {
        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="OutstackConditionList"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="8/24/2009" version="1.0.16.04">
        ///     Member Created
        /// </revision> 
        public OutstackConditionList()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="OutstackConditionList"/> class.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        /// <externalUnit cref="List{T}(int)"/>
        /// <revision revisor="dev13" date="8/24/2009" version="1.0.16.04">
        ///     Member Created
        /// </revision>
        public OutstackConditionList(int capacity)
            : base(capacity)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="OutstackConditionList"/> class.
        /// </summary>
        /// <param name="conditions">The conditions.</param>
        /// <externalUnit cref="OutstackCondition"/>
        /// <externalUnit cref="List{T}(IEnumerable{T})"/>
        /// <externalUnit cref="IEnumerable{T}"/>
        /// <revision revisor="dev13" date="8/24/2009 3:05:07 PM" version="1.0.?.0">
        ///     Member Created
        /// </revision>
        public OutstackConditionList(IEnumerable<OutstackCondition> conditions)
            : base(conditions)
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
