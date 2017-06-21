// -----------------------------------------------------------------------------
// <copyright file="AuthorizationEventArgs.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2010 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the AuthorizationEventArgs class.
// </summary>
// <revision revisor="dev22" date="03/02/2010" version="1.1.7.09">
// Changed to added.
// </revision>
// -----------------------------------------------------------------------------

namespace Sequoia.EMS.Core.DataServices
{
    using System;
    
    /// <summary>
    /// Contains information for data service authorization events.
    /// </summary>
    public class AuthorizationEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the AuthorizationEventArgs class.
        /// </summary>
        /// <param name="current">The object that defines the current
        /// authorization information (this will be cloned for use by this
        /// object).</param>
        public AuthorizationEventArgs(DataServiceDefinition current)
        {
            if (current != null)
            {
                this.Current = (DataServiceDefinition)current.Clone();
            }
        }

        /// <summary>
        /// Gets an object that defines the authorization information at the
        /// time the event originated (which may be obsolete).
        /// </summary>
        public DataServiceDefinition Current
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets an object that defines the new authorization
        /// information after the event has been processed.
        /// </summary>
        public DataServiceDefinition New
        {
            get;
            set;
        }
    }
}
