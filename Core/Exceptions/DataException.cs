//-----------------------------------------------------------------------------
// <copyright file="DataException.cs" 
//  company="Sequoia Voting Systems">
//     Copyright (c) 2008 Sequoia Voting Systems. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <revision revisor="dev01" date="11/16/2009" version="1.1.3.4">
//     File Created
// </revision>
//-----------------------------------------------------------------------------

namespace Sequoia.EMS.Core.Exceptions
{
    #region Using directives

    using System;

    #endregion

    /// <summary>
    ///     Enum to identify Data exceptions
    /// </summary>
    /// <revision revisor="dev01" date="11/16/2009" version="1.1.3.4">
    ///     Member Created
    /// </revision>
    public enum DataExceptionType
    {
        /// <summary>
        ///     When updating or inserting parties there can only be 1 with
        ///     the non-partisan flag
        /// </summary>
        MultipleNonPartisanParties
    }

    /// <summary>
    ///     DataException is an exception that covers all types of data
    ///     exceptions we are encountering. 
    /// </summary>
    /// <revision revisor="dev01" date="11/16/2009" version="1.1.3.4">
    ///     Member Created
    /// </revision>
    public class DataException : Exception
    {
        #region Constructors
        
        /// <summary>
        ///     Initializes a new instance of the <see cref="DataException"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="11/16/2009" version="1.1.3.4">
        ///     Member Created
        /// </revision>
        public DataException()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DataException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="11/16/2009" version="1.1.3.4">
        ///     Member Created
        /// </revision>
        public DataException(string message) 
            : base(message)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DataException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="11/16/2009" version="1.1.3.4">
        ///     Member Created
        /// </revision>
        public DataException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the type.
        /// </summary>
        /// <value>The exception type.</value>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="11/16/2009" version="1.1.3.4">
        ///     Member Created
        /// </revision>
        public DataExceptionType Type
        {
            get;
            set;
        }
        #endregion

        #region Methods

        #endregion
    }
}
