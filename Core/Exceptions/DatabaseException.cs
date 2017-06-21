//-----------------------------------------------------------------------------
// <copyright file="DatabaseException.cs" 
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
    ///     DatabaseException is an exception that covers all types of db 
    ///     exceptions we are encountering. It is used by the snap-in framework
    ///     to differentiate between other exceptions. When this exception is 
    ///     encountered, the shell will ask the user if they want to attempt to 
    ///     log out and back in rather than just shut the whole application 
    ///     down.
    /// </summary>
    /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
    ///     Class created
    /// </revision>
    public class DatabaseException : Exception
    {
        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="DatabaseException"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public DatabaseException()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DatabaseException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <externalUnit/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public DatabaseException(string message) 
            : base(message)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DatabaseException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        /// <externalUnit/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public DatabaseException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        #endregion

        #region Properties

        #endregion

        #region Methods

        #endregion
    }
}
