//-----------------------------------------------------------------------------
// <copyright file="CacheMap.cs" 
//  company="Sequoia Voting Systems">
//     Copyright (c) 2008 Sequoia Voting Systems. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
//     File created
// </revision>
//-----------------------------------------------------------------------------

namespace Sequoia.EMS.Core.Managers
{
    #region Using directives

    using System;
    using Microsoft.Practices.EnterpriseLibrary.Caching;
    using Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;

    #endregion

    /// <summary>
    ///     CacheMap is a utility class that just encapsulates some of the 
    ///     caching functionality.
    /// </summary>
    /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
    ///     Class created
    /// </revision>
    public sealed class CacheMap
    {
        #region Methods

        /// <summary>
        ///     Gets the cached object for specified key.
        /// </summary>
        /// <param name="key">The cache key.</param>
        /// <returns>a cache object</returns>
        /// <externalUnit cref="CacheManager"/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public static object Get(string key)
        {
            return CacheManager.Instance.Cache.GetData(key);
        }

        /// <summary>
        ///     Sets an object in cache using the specified key.
        /// </summary>
        /// <param name="key">The cache key.</param>
        /// <param name="value">The cache value.</param>
        /// <externalUnit cref="CacheItemPriority"/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public static void Set(string key, object value)
        {
            Set(
                key,
                value,
                CacheItemPriority.Normal,
                null,
                new SlidingTime(TimeSpan.FromMinutes(30)));
        }

        /// <summary>
        ///     Sets an object in cache using the specified key.  
        ///     Allows the user to set some other options so that 
        ///     they have better control of the cached items.
        /// </summary>
        /// <param name="key">The cache key.</param>
        /// <param name="value">The cache value.</param>
        /// <param name="scavengingPriority">The scavenging priority.</param>
        /// <param name="refreshAction">The refresh action.</param>
        /// <param name="expirations">The expirations.</param>
        /// <externalUnit cref="CacheManager"/>
        /// <externalUnit cref="CacheItemPriority"/>
        /// <externalUnit cref="ICacheItemRefreshAction"/>
        /// <externalUnit cref="ICacheItemExpiration"/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public static void Set(
            string key,
            object value,
            CacheItemPriority scavengingPriority,
            ICacheItemRefreshAction refreshAction,
            params ICacheItemExpiration[] expirations)
        {
            CacheManager.Instance.Cache.Add(
                key, value, scavengingPriority, refreshAction, expirations);
        }

        #endregion
    }
}
