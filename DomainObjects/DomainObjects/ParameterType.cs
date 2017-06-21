// -----------------------------------------------------------------------------
// <copyright file="ParameterType.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the ParameterType enumeration.
// </summary>
// <revision revisor="dev14" date="6/15/2009" version="1.0.13.11">
//     File Created
// </revision>
// <revision revisor="dev01" date="11/18/2009" version="1.1.3.6">
//     File modified
// </revision>
// -----------------------------------------------------------------------------

namespace Sequoia.DomainObjects
{
    #region Using directives

    using System;

    #endregion

    /// <summary>
    ///     ParameterType is an enumeration that maps a parameter type to an integer
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev14" date="6/15/2009" version="1.0.13.11">
    ///     Enumeration created.
    /// </revision>
    /// <revision revisor="dev01" date="11/18/2009" version="1.1.3.6">
    ///     Added arraytoint value
    /// </revision>
    public enum ParameterType
    {
        /// <summary>
        /// any character string
        /// </summary>
        String = 1,
        
        /// <summary>
        /// a range of numeric values
        /// </summary>
        Range = 2,

        /// <summary>
        /// an integer
        /// </summary>
        Int = 3,

        /// <summary>
        /// a numeric value that can have a fractional part
        /// </summary>
        Decimal = 4,

        /// <summary>
        /// date or datetime value
        /// </summary>
        DateTime = 5,

        /// <summary>
        /// true/false value
        /// </summary>
        Boolean = 6,

        /// <summary>
        /// an array of values specified in a pipe-delimited string
        /// </summary>
        Array = 7,

        /// <summary>
        /// an array of values specified in a pipe-delimited string
        /// </summary>
        ArrayToInt = 8
    }
}
