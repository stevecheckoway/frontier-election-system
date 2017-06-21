// -----------------------------------------------------------------------------
// <copyright file="UsbKeyLoadedEventArgs.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2008 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the UsbKeyLoadedEventArgs class.
// </summary>
// <revision revisor="dev06" date="11/12/2008" version="1.0.0.0">
//     File Created
// </revision>  
// -----------------------------------------------------------------------------

namespace Sequoia.Utilities.IO
{
    #region Using directives

    using System;

    #endregion

    /// <summary>
    /// UsbKeyLoadedEventArgs class
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev06" date="11/12/2008" version="1.0.0.0">
    ///     Class created.
    /// </revision>
    public class UsbKeyLoadedEventArgs : EventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="UsbKeyLoadedEventArgs"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/9/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public UsbKeyLoadedEventArgs()
            : this(string.Empty)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="UsbKeyLoadedEventArgs"/> class.
        /// </summary>
        /// <param name="pathToUsbKey">The path to voter key.</param>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/9/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public UsbKeyLoadedEventArgs(string pathToUsbKey)
        {
            this.PathToUsbKey = pathToUsbKey;
        }

        /// <summary>
        ///     Gets or sets the path to voter key.
        /// </summary>
        /// <value>The path to voter key.</value>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/9/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public string PathToUsbKey
        {
            get;
            set;
        }
    }
}
