// -----------------------------------------------------------------------------
// <copyright file="Enums.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the Enums class.
// </summary>
// <revision revisor="dev11" date="9/16/2009" version="1.0.0.0">
//     File Created
// </revision>
// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
//     File modified
// </revision>
// -----------------------------------------------------------------------------

namespace Sequoia.Ems.Data.Custom
{
    #region Using directives

    using System;

    #endregion

    /// <summary>
    ///	    Has all codes for generic error messages related to EntrySet 
    ///     operations. To see actual messages, see 
    ///     EmsShell/Data/ErrorInformationStore.xml file in the 
    ///     EMS Shell project.
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev11" date="9/16/2009" version="1.0.0.0">
    ///     Class created.
    /// </revision>
    /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
    ///     Formatting changes
    /// </revision>
    public enum EntrySetErrorCode
    {
        /// <summary>
        /// There was a problem loading data from the database
        /// </summary>
        LoadError = 11001,

        /// <summary>
        /// These was a problem saving changes to the database
        /// </summary>
        SaveChangesError = 11002,

        /// <summary>
        /// Unexpected error working with the data
        /// </summary>
        UnexpectedError = 11003
    }
}
