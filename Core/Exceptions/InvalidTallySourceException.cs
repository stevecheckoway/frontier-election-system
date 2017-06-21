//-----------------------------------------------------------------------------
// <copyright file="InvalidTallySourceException.cs" 
//  company="Sequoia Voting Systems">
//     Copyright (c) 2008 Sequoia Voting Systems. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
//     File created
// </revision>
//-----------------------------------------------------------------------------

namespace Sequoia.EMS.Core.Exceptions
{
    #region Using directives

    using System;

    #endregion

    /// <summary>
    ///     InvalidTallySourceException is an exception that gets thrown when an
    ///     invalid tally source id is encountered during tally souce sensitive
    ///     operations.
    /// </summary>
    /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
    ///     Class created
    /// </revision>
    public class InvalidTallySourceException : Exception
    {
        #region Fields

        /// <summary>
        ///     param to store the invalied tally source id
        /// </summary>
        private int invalidTallySourceId = 0;

        /// <summary>
        ///     param to store the default error message
        /// </summary>
        private string message = 
            "An invalid tally source id was specified.\r\n";

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="InvalidTallySourceException"/> class.
        /// </summary>
        /// <param name="tallySourceId">The tally source id.</param>
        /// <externalUnit cref="invalidTallySourceId"/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public InvalidTallySourceException(int tallySourceId)
        {
            // set the invalid tally source id
            this.invalidTallySourceId = tallySourceId;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the invalid tally source id.
        /// </summary>
        /// <value>The invalid tally source id.</value>
        /// <externalUnit cref="invalidTallySourceId"/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public int InvalidTallySourceId
        {
            get { return this.invalidTallySourceId; }
            set { this.invalidTallySourceId = value; }
        }

        /// <summary>
        ///     Gets a message that describes the current exception.
        /// </summary>
        /// <value></value>
        /// <returns>
        ///     The error message that explains the reason for the exception, 
        ///     or an empty string("").
        /// </returns>
        /// <externalUnit cref="message"/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public override string Message
        {
            get { return this.message + base.Message; }
        }

        #endregion

        #region Methods

        #endregion
    }
}
