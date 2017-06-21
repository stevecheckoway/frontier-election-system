//-----------------------------------------------------------------------------
// <copyright file="InvalidInstallationException.cs" 
//  company="Sequoia Voting Systems">
//     Copyright (c) 2008 Sequoia Voting Systems. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <revision revisor="dev06" date="10/6/2008" verison="1.0.0.0">
//     File created
// </revision>
//-----------------------------------------------------------------------------

namespace Sequoia.EMS.Core.Exceptions
{
    #region Using directives

    using System;

    #endregion

    /// <summary>
    ///     This exception gets thrown if the application determines that it or
    ///     the installation has been tampered with.
    /// </summary>
    /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
    ///     Class created
    /// </revision>
    public class InvalidInstallationException : Exception
    {
        #region Fields

        /// <summary>
        ///     param for failed item
        /// </summary>
        private string failedItem = string.Empty;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="InvalidInstallationException"/> class.
        /// </summary>
        /// <param name="failedItem">The failed item.</param>
        /// <externalUnit/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public InvalidInstallationException(string failedItem) 
            : this(failedItem, string.Empty, null)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="InvalidInstallationException"/> class.
        /// </summary>
        /// <param name="failedItem">The failed item.</param>
        /// <param name="message">The message.</param>
        /// <externalUnit/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public InvalidInstallationException(string failedItem, string message) 
            : this(failedItem, message, null)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="InvalidInstallationException"/> class.
        /// </summary>
        /// <param name="failedItem">The failed item.</param>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        /// <externalUnit cref="FailedItem"/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public InvalidInstallationException(
            string failedItem, 
            string message, 
            Exception innerException) 
            : base(message, innerException)
        {
            // set properties
            this.FailedItem = failedItem;
        }
        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the failed item.
        /// </summary>
        /// <value>The failed item.</value>
        /// <externalUnit cref="failedItem"/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public string FailedItem
        {
            get { return this.failedItem; }
            set { this.failedItem = value; }
        }

        #endregion
    }
}
