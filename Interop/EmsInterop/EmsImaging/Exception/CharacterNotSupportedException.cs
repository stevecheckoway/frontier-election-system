// -----------------------------------------------------------------------------
// <copyright file="CharacterNotSupportedException.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the CharacterNotSupportedException class.
// </summary>
// <revision revisor="dev11" date="2/11/2009" version="1.0.6.7">
//     File Created
// </revision>
// <revision revisor="dev13" date="11/19/2009" version="1.1.3.7">
//     File modified
// </revision> 
// -----------------------------------------------------------------------------

namespace Sequoia.Ems.Imaging.Exception
{
    #region Using directives

    using System;

    #endregion

    /// <summary>
    ///	    Class for CharacterNotSupportedException
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev11" date="2/11/2009" version="1.0.6.7">
    ///     Class created.
    /// </revision>
    /// <revision revisor="dev13" date="11/19/2009" version="1.1.3.7">
    ///     Formatting changes.
    /// </revision>
    public class CharacterNotSupportedException : Exception
    {
        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="CharacterNotSupportedException"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="2/11/2009" version="1.0.6.7">
        ///     Member Created
        /// </revision>	
        public CharacterNotSupportedException()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="CharacterNotSupportedException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/19/2009" version="1.1.3.7">
        ///     Added documentation header
        /// </revision>
        public CharacterNotSupportedException(string message) : base(message)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="CharacterNotSupportedException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/19/2009" version="1.1.3.7">
        ///     Added documentation header
        /// </revision>
        public CharacterNotSupportedException(
            string message, Exception innerException) : 
            base(message, innerException)
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

        #region Constants

        #endregion
    }
}
