// -----------------------------------------------------------------------------
// <copyright file="OperationResult.cs" company="Sequoia Voting Systems">
//     Copyright (c) 2008 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the OperationResult class.
// </summary>
// <revision revisor="dev06" date="12/12/2008" version="1.0.0.0">
//     File Created
// </revision>
// <revision revisor = "dev05" date="03/19/09" version="1.0.9.3">
//     File modified.
// </revision>
// <revision revisor="dev05" date="09/09/09" version="1.0.17.2">
//     File modified.
// </revision>
// -----------------------------------------------------------------------------

namespace Sequoia.DomainObjects
{
    #region Using directives

    #endregion

    /// <summary>
    ///     Result of an operation
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev06" date="12/12/2008" version="1.0.0.0">
    ///     Class created.
    /// </revision>
    /// <revision revisor = "dev05" date="03/19/09" version="1.0.9.3">
    ///     Moved here from ObjectPersistence.
    /// </revision>
    public class OperationResult
    {
        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="OperationResult"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev06" date="12/12/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public OperationResult() : this(false, string.Empty)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="OperationResult"/> class.
        /// </summary>
        /// <param name="succeeded">if set to <c>true</c> [succeeded].</param>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="10/7/2009" version="1.1.1.7">
        ///     Member Created
        /// </revision>
        public OperationResult(bool succeeded) : this(succeeded, string.Empty)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="OperationResult"/> class.
        /// </summary>
        /// <param name="succeeded">if set to <c>true</c> [succeeded].</param>
        /// <param name="details">The details.</param>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="10/7/2009" version="1.1.1.7">
        ///     Member Created
        /// </revision>
        public OperationResult(bool succeeded, string details)
        {
            this.Succeeded = succeeded;
            this.Details = details;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the details.
        /// </summary>
        /// <value>The details.</value>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="10/7/2009" version="1.1.1.7">
        ///     Member Created
        /// </revision>
        public string Details
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="OperationResult"/> is succeeded.
        /// </summary>
        /// <value><c>true</c> if succeeded; otherwise, <c>false</c>.</value>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="10/7/2009" version="1.1.1.7">
        ///     Member Created
        /// </revision>
        public bool Succeeded
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
