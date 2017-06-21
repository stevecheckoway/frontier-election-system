//-----------------------------------------------------------------------------
// <copyright file="Delegates.cs" 
//  company="Sequoia Voting Systems">
//     Copyright (c) 2008 Sequoia Voting Systems. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
//     File created
// </revision>
//-----------------------------------------------------------------------------

namespace Sequoia.EMS.Core.Logging
{
    /// <summary>
    ///     This is a delegate for log events.
    /// </summary>
    /// <param name="logEventArgs">
    ///     Information that is used by the logger. Contains that which to log.
    /// </param>
    /// <externalUnit cref="LogEventArgs"/>
    /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
    ///     Delegate created
    /// </revision>
    public delegate void LogEventHandler(LogEventArgs logEventArgs);
}