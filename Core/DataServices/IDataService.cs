//-----------------------------------------------------------------------------
// <copyright file="IDataService.cs" 
//  company="Sequoia Voting Systems">
//     Copyright (c) 2008 Sequoia Voting Systems. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <revision revisor="dev13" date="10/6/2008" version="1.0.0.0">
//     File created
// </revision>
// <revision revisor="dev13" date="10/16/2008" version="1.0.0.0">
//     File modified
// </revision>
// <revision revisor="dev13" date="10/21/2008" version="1.0.0.0">
//     File modified
// </revision>
// <revision revisor="dev22" date="03/09/2010" version="1.1.7.16">
//     File modified
// </revision>
//-----------------------------------------------------------------------------

namespace Sequoia.EMS.Core.DataServices
{
    #region Using directives

    using System;

    #endregion

    /// <summary>
    /// Defines members common to objects that provide data access
    /// functionality.
    /// </summary>
    /// <revision revisor="dev06" date="10/16/2008" version="1.0.0.0">
    ///     Interface created.
    /// </revision>
    /// <revision revisor="dev22" date="03/08/2010" version="1.1.7.15">
    /// Removed obsolete and unused members and redefined as an actual
    /// basic data service that can be implemented by all data services
    /// (work for US577).
    /// </revision>
    /// <revision revisor="dev22" date="03/09/2010" version="1.1.7.16">
    /// Changed to extend <see cref="IAuthorizationRequestor"/>, see that
    /// interface's documentation for more. This interface may still be useful
    /// for handling data services generically (more members can be added
    /// later).
    /// </revision>
    public interface IDataService : IAuthorizationRequestor
    {
        
    }
}
