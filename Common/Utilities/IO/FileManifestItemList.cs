// -----------------------------------------------------------------------------
// <copyright file="FileManifestItemList.cs" company="Sequoia Voting Systems">
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the ManifestItemList class.
// </summary>
// <revision revisor="dev05" date="03/16/09" version="1.0.8.27">
//     File created.
// </revision>
// <revision revisor="dev05" date="03/19/09" version="1.0.9.3">
//     File modified.
// </revision>
// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
//     File modified.
// </revision>
// -----------------------------------------------------------------------------

namespace Sequoia.Utilities.IO
{
    #region Using directives

    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml.Serialization;

    #endregion

    /// <summary>
    ///	    A list of files in a manifest.
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev05" date="03/16/09" version="1.0.8.27">
    ///     Class created.
    /// </revision>
    /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
    ///     Formatting changes
    /// </revision>
    [Serializable]
    [XmlRoot("FileManifestItems")]
    public class FileManifestItemList : List<FileManifestItem>
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="FileManifestItemList"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev05" date="03/16/09" version="1.0.8.27">
        ///     Member created.
        /// </revision>
        public FileManifestItemList()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="FileManifestItemList"/> class.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        public FileManifestItemList(int capacity) : base(capacity)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="FileManifestItemList"/> class.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        public FileManifestItemList(IEnumerable<FileManifestItem> items) : 
            base(items)
        {
        }

        #endregion
    }
}
