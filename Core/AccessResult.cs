//-----------------------------------------------------------------------------
// <copyright file="AccessResult.cs" 
//  company="Sequoia Voting Systems">
//     Copyright (c) 2008 Sequoia Voting Systems. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
//     File created
// </revision>
//-----------------------------------------------------------------------------

namespace Sequoia.EMS.Core
{
    /// <summary>
    ///     AccessResult is an object used to return the results of snap-in
    ///     access and permissions test.
    /// </summary>
    /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
    ///     Member created
    /// </revision>
    public sealed class AccessResult
    {
        #region Fields

        /// <summary>
        ///     param for whether user can access the snap in
        /// </summary>
        private bool canUserAccessSnapIn = false;

        /// <summary>
        ///     param for the snap in components access key.
        /// </summary>
        private int accessKey = 0;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="AccessResult"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public AccessResult()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="AccessResult"/> class.
        /// </summary>
        /// <param name="canUserAccessSnapIn">
        ///     if set to <c>true</c> [can user access snap in].
        /// </param>
        /// <param name="accessKey">The access key.</param>
        /// <externalUnit cref="CanUserAccessSnapIn"/>
        /// <externalUnit cref="AccessKey"/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public AccessResult(bool canUserAccessSnapIn, int accessKey)
        {
            // set the properties
            this.CanUserAccessSnapIn = canUserAccessSnapIn;
            this.AccessKey = accessKey;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets a value indicating whether the 
        ///     current user can access the snap in component.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance can user access snap in; 
        ///     otherwise, <c>false</c>.
        /// </value>
        /// <externalUnit cref="canUserAccessSnapIn"/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public bool CanUserAccessSnapIn
        {
            get { return this.canUserAccessSnapIn; }
            set { this.canUserAccessSnapIn = value; }
        }

        /// <summary>
        ///     Gets or sets the access key for the snap in component.
        ///     This is stored as an int, but is used as a binary flag,
        ///     and binary 'anded' to dtermine permission level.
        /// </summary>
        /// <value>The access key.</value>
        /// <externalUnit cref="accessKey"/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public int AccessKey
        {
            get { return this.accessKey; }
            set { this.accessKey = value; }
        }

        #endregion
    }
}
