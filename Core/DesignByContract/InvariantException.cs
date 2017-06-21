//-----------------------------------------------------------------------------
// <copyright file="InvariantException.cs" 
//  company="Sequoia Voting Systems">
//     Copyright (c) 2008 Sequoia Voting Systems. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
//     File created
// </revision>
//-----------------------------------------------------------------------------

namespace Sequoia.EMS.Core.DesignByContract
{
    #region Using directives

    using System;

    #endregion

    /// <summary>
    ///     Exception raised when an invariant fails.
    /// </summary>
    /// <externalUnit cref="DesignByContractException"/>
    /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
    ///     Class created
    /// </revision>
    public sealed class InvariantException : DesignByContractException
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="InvariantException"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public InvariantException()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="InvariantException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <externalUnit/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public InvariantException(string message) : base(message)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="InvariantException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner exception.</param>
        /// <externalUnit/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public InvariantException(string message, Exception inner) 
            : base(message, inner)
        {
        }

        #endregion
    }
}
