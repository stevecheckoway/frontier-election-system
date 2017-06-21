//-----------------------------------------------------------------------------
// <copyright file="CacheManager.cs" 
//  company="Sequoia Voting Systems">
//     Copyright (c) 2008 Sequoia Voting Systems. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
//     File created
// </revision>
// <revision revisor="dev13" date="10/8/2008" version="1.0.0.0">
//     File modified
// </revision>
//-----------------------------------------------------------------------------

namespace Sequoia.EMS.Core.Managers
{
    #region Using directives

    using Microsoft.Practices.EnterpriseLibrary.Caching;

    using MicrosoftCacheManager = 
        Microsoft.Practices.EnterpriseLibrary.Caching.CacheManager;

    #endregion

    /// <summary>
    ///     Supplies access to caching.
    /// </summary>
    /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
    ///     Class created
    /// </revision>
    public sealed class CacheManager
    {
        #region Private Members

        /// <summary>
        ///     param for returning the cache manager
        /// </summary>
        private MicrosoftCacheManager cache;

        #endregion

        #region Constructors

        /// <summary>
        ///     Prevents a default instance of the <see cref="CacheManager"/> class from being created.
        /// </summary>
        /// <externalUnit cref="CacheFactory"/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        private CacheManager()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the singleton instance of the CacheManager.
        /// </summary>
        /// <value>The single instance of the CacheManager.</value>
        /// <externalUnit cref="Nested"/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public static CacheManager Instance
        {
            get
            {
                // return the nested class used to ensure it is the only instance
                return Nested.instance;
            }
        }

        /// <summary>
        ///     Gets the cache.
        /// </summary>
        /// <value>The cache.</value>
        /// <externalUnit cref="MicrosoftCacheManager"/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public MicrosoftCacheManager Cache
        {
            get { return this.cache; }
        }

        #endregion

        /// <summary>
        ///     A private class used to create the singleton.
        /// </summary>
        private class Nested
        {
            /// <summary>
            ///     Initializes static members of the <see cref="Nested" /> 
            ///     class. Explicit static constructor to tell C# compiler not 
            ///     to mark type as beforefieldinit.
            /// </summary>
            /// <externalUnit/>
            /// <revision revisor="dev13" date="10/8/2008" version="1.0.0.0">
            ///     Member Created
            /// </revision>
            static Nested()
            {
            }

            /// <summary>
            ///     ensures this is a singleton, thread safe
            /// </summary>
            internal static readonly CacheManager instance = 
                new CacheManager();
        }
    }
}
