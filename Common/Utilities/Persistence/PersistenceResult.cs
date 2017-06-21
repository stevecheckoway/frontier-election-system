// -----------------------------------------------------------------------------
// <copyright file="PersistenceResult.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2008 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the PersistenceResult class.
// </summary>
// <revision revisor="dev06" date="11/12/2008" version="1.0.0.0">
//     File Created
// </revision>  
// -----------------------------------------------------------------------------

namespace Sequoia.Utilities.Persistence
{
    #region Using directives

    using System;

    #endregion

    /// <summary>
    ///	    Class for persistence result
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev13" date="11/9/2008" version="1.0.0.0">
    ///     Class created.
    /// </revision>
    public class PersistenceResult
    {
        #region Fields

        /// <summary>
        ///     param for operation succeeded result
        /// </summary>
        private bool operationSucceeded = false;

        /// <summary>
        ///     param for operation details
        /// </summary>
        private string details;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="PersistenceResult"/> class.
        /// </summary>
        /// <param name="operationSucceeded">
        ///     if set to <c>true</c> [operation succeeded].
        /// </param>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/9/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public PersistenceResult(bool operationSucceeded)
        {
            this.operationSucceeded = operationSucceeded;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets a value indicating whether [operation succeeded].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [operation succeeded]; otherwise, <c>false</c>.
        /// </value>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/9/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public bool OperationSucceeded
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the details.
        /// </summary>
        /// <value>The details.</value>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/9/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public string Details
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
