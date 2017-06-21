// -----------------------------------------------------------------------------
// <copyright file="FileManifestItem.cs" company="Sequoia Voting Systems">
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     FileManifestItem class implementation.
// </summary>
// <revision revisor="dev05" date="03/16/09" version="1.0.8.27">
//     File created.
// </revision>
// <revision revisor="dev05" date="03/19/09" version="1.0.9.3">
//     File modified.
// </revision>
// <revision revisor="dev05" date="09/09/09" version="1.0.17.2">
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
    using System.IO;
    using System.Security.Cryptography;
    using System.Xml.Serialization;

    using Sequoia.DomainObjects;
    using Sequoia.Utilities;

    #endregion

    /// <summary>
    ///	    One item in a manifest of files, with the name of the file and its
    ///	    hash.
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev05" date="03/16/09" version="1.0.8.27">
    ///     Class created.
    /// </revision>
    /// <revision revisor="dev05" date="03/19/09" version="1.0.9.3">
    ///     Made IsValid() return an OperationResult.
    /// </revision>
    /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
    ///     Formatting changes
    /// </revision>
    [Serializable]
    public class FileManifestItem
    {
        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="FileManifestItem"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev05" date="03/16/09" version="1.0.8.27">
        ///     Member created.
        /// </revision>
        public FileManifestItem()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="FileManifestItem"/> class.
        /// </summary>
        /// <param name="basePath">Path containing manifest files</param>
        /// <param name="fileName">File name relative to base path</param>
        /// <externalUnit/>
        /// <revision revisor="dev05" date="03/16/09" version="1.0.8.27">
        ///     Member created.
        /// </revision>
        public FileManifestItem(string basePath, string fileName)
        {
            this.FileName = fileName;

            FileInfo fi = new FileInfo(Path.Combine(basePath, fileName));
            using (FileStream fs = fi.OpenRead())
            {
                var hasher = new SHA256Managed();

                this.Hash = hasher.ComputeHash(fs);
            }
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="FileManifestItem"/> class.
        /// </summary>
        /// <param name="fileBytes">The file bytes.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/12/2009" version="1.1.2.22">
        ///     Added documentation header
        /// </revision>
        public FileManifestItem(byte[] fileBytes, string fileName)
        {
            this.FileName = fileName;

            var hasher = new SHA256Managed();

            this.Hash = hasher.ComputeHash(fileBytes);
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the file name for the item.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev05" date="03/16/09" version="1.0.8.27">
        ///     Property created.
        /// </revision>
        [XmlAttribute]
        public string FileName
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the hash of the file.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev05" date="03/16/09" version="1.0.8.27">
        ///     Property created.
        /// </revision>
        public byte[] Hash
        {
            get;
            set;
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Validate the hash of the item's file.
        /// </summary>
        /// <param name="basePath">Path containing the file</param>
        /// <returns>true if the hash is valid, false otherwise</returns>
        /// <externalUnit/>
        /// <revision revisor="dev05" date="03/16/09" version="1.0.8.27">
        ///     Method created.
        /// </revision>
        /// <revision revisor="dev05" date="03/19/09" version="1.0.9.3">
        ///     Return an OperationResult.
        /// </revision>
        public OperationResult IsValid(string basePath)
        {
            // Returned result.
            var result = new OperationResult(true);

            // Full pathname of file.
            string pathName = Path.Combine(basePath, this.FileName);

            // Info on file.
            var fi = new FileInfo(pathName);

            if (fi.Exists)
            {
                using (FileStream fs = fi.OpenRead())
                {
                    // Hash provider.
                    var hasher = new SHA256Managed();

                    // Computed hash of the file.
                    byte[] computedHash = hasher.ComputeHash(fs);

                    // Compare computed hash with the item's hash.
                    if (computedHash.Length == this.Hash.Length)
                    {
                        for (int i = 0;
                             result.Succeeded && i < computedHash.Length; i++)
                        {
                            if (computedHash[i] != this.Hash[i])
                            {
                                result = new OperationResult(
                                  false,
                                  String.Format(
                                    "Manifest hash value mismatch for file {0}",
                                    pathName));
                            }
                        }
                    }
                    else
                    {
                        result = new OperationResult(
                            false,
                            String.Format(
                                "Manifest hash length mismatch for file {0}",
                                pathName));
                    }
                }
            }
            else
            {
                result = new OperationResult(
                    false,
                    String.Format(
                        "Manifest file not found: {0}",
                        pathName));
            }

            return result;
        }

        #endregion
    }
}
