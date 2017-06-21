//-----------------------------------------------------------------------------
// <copyright file="Enums.cs" 
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
    ///     LoggingPriority is used to assign a priority to a log event. This in
    ///     turn will determine what actions the logger takes to handle the 
    ///     event. Different actions are defined in the config file for 
    ///     different levels of priority and severity.
    /// </summary>
    /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
    ///     Enum created
    /// </revision>
    public enum LoggingPriority
    {
        /// <summary>
        ///     Information logging priority
        /// </summary>
        Information = 1,

        /// <summary>
        ///     Warning logging priority
        /// </summary>
        Warning = 2,

        /// <summary>
        ///     Error logging priority
        /// </summary>
        Error = 3,

        /// <summary>
        ///     Critical logging priority
        /// </summary>
        Critical = 4
    }
}