// -----------------------------------------------------------------------------
// <copyright file="FileManagerException.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the FileManagerException class.
// </summary>
// <revision revisor="dev06" date="2/2/2009" version="1.0.5.14">
//     File Created
// </revision>
// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
//     File modified
// </revision>
// -----------------------------------------------------------------------------

namespace Sequoia.Utilities.IO
{
    using System;

    /// <summary>
    ///	    FileManagerException is a class that wraps io exceptions from the 
    ///     viewpoint of the utilities consumer, yet conserves the stack trace 
    ///     for real troubleshooting. The gaols is that there is only one 
    ///     exception to manage in terms of response to the user.
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev06" date="2/2/2009" version="1.0.5.14">
    ///     Class created.
    /// </revision>
    /// <revision revisor="dev14" date="2/3/2009" version="1.0.5.15">
    ///     Class modified:marked Serializable and added constructor to handles
    ///     serialization
    /// </revision>
    /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
    ///     Formatting changes
    /// </revision>
    [Serializable]
    public class FileManagerException : Exception
    {
        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="FileManagerException"/> class.
        /// </summary>
        /// <externalUnit cref="FileManagerException(string, Exception)"/>
        /// <revision revisor="dev06" date="2/2/2009" version="1.0.5.14">
        ///     Member Created
        /// </revision>
        public FileManagerException() : this(string.Empty, null)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="FileManagerException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <externalUnit cref="FileManagerException(string, Exception)"/>
        /// <revision revisor="dev06" date="2/2/2009" version="1.0.5.14">
        ///     Member Created
        /// </revision>
        public FileManagerException(string message) : this(message, null)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="FileManagerException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        /// <externalUnit/>
        /// <revision revisor="dev06" date="2/2/2009" version="1.0.5.14">
        ///     Member Created
        /// </revision>
        public FileManagerException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="FileManagerException"/> class.
        ///     Handles serialization of type to adhere to .Net best practices
        /// </summary>
        /// <param name="info">
        ///     The 
        ///     <see cref="T:System.Runtime.Serialization.SerializationInfo"/> 
        ///     that holds the serialized object data about the exception 
        ///     being thrown.</param>
        /// <param name="context">
        ///     The 
        ///     <see cref="T:System.Runtime.Serialization.StreamingContext"/> 
        ///     that contains contextual information about the source or 
        ///     destination.</param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     The <paramref name="info"/> parameter is null.
        /// </exception>
        /// <exception cref="T:System.Runtime.Serialization.SerializationException">
        ///     The class name is null or 
        ///     <see cref="P:System.Exception.HResult"/> is zero (0). 
        /// </exception>
        public FileManagerException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
                : base(info, context)
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
