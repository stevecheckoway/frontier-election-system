// -----------------------------------------------------------------------------
// <copyright file="Enums.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2008 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the Enums class.
// </summary>
// <revision revisor="dev11" date="10/14/2008" version="1.0.0.0">
//     File Created
// </revision>  
// -----------------------------------------------------------------------------

namespace UnitTests
{
    #region Using directives

    using Sequoia.Utilities;

    #endregion

    /// <summary>
    ///	    Testing enums
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev11" date="10/14/2008" version="1.0.0.0">
    ///     Class created.
    /// </revision>
    public enum EnumTest
    {
        /// <summary>
        ///     Testing enum text attributes
        /// </summary>
        [EnumText(Text = "Testing Enum Text Attributes")]
        EnumTypeWithTextAttribute,

        /// <summary>
        ///     Testing enum without text attribute
        /// </summary>
        EnumTypeWithNoTextAttribute,

        /// <summary>
        ///     Test enum with another bit of text
        /// </summary>
        [EnumText(Text = "Another text here")]
        EnumTypeWithAnotherText
    }
}
