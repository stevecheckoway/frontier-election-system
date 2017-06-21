// -----------------------------------------------------------------------------
// <copyright file="PdfIOException.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the PdfIOException class.
// </summary>
// <revision revisor="dev11" date="1/6/2009" version="1.0.0.0">
//     File Created
// </revision>
// <revision revisor="dev13" date="11/19/2009" version="1.1.3.7">
//     File modified
// </revision>
// -----------------------------------------------------------------------------

namespace Sequoia.Ems.Interop.Exception
{
    #region Using directives

    using System;

    #endregion

    /// <summary>
    ///	    Class for PdfIOException
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev11" date="1/6/2009" version="1.0.0.0">
    ///     Class created.
    /// </revision>
    /// <revision revisor="dev13" date="11/19/2009" version="1.1.3.7">
    ///     Formatting changes.
    /// </revision>
    public class PdfIOException : Exception
    {
        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="PdfIOException"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="1/6/2009" version="1.0.0.0">
        ///     Member Created
        /// </revision>	
        public PdfIOException()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="PdfIOException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/19/2009" version="1.1.3.7">
        ///     Added documentation header
        /// </revision>
        public PdfIOException(string message) : base(
            message)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="PdfIOException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/19/2009" version="1.1.3.7">
        ///     Added documentation header
        /// </revision>
        public PdfIOException(string message, Exception innerException) : base(
            message, innerException)
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
