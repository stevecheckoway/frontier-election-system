// -----------------------------------------------------------------------------
// <copyright file="FileManifest.cs" company="Sequoia Voting Systems">
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the FileManifest class.
// </summary>
// <revision revisor = "dev05" date="03/16/09" version="1.0.8.27">
//     File created.
// </revision>
// <revision revisor = "dev05" date="03/19/09" version="1.0.9.3">
//     File modified.
// </revision>
// <revision revisor="dev05" date="03/25/09" version="1.0.9.9">
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
    using System.Collections.Generic;
    using System.IO;
    using System.Xml.Serialization;

    using Sequoia.DomainObjects;
    using Sequoia.Utilities;

    #endregion

    /// <summary>
    ///     Serializable manifest for authenticating a list of files.
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev05" date="03/16/09" version="1.0.8.27">
    ///     Class created.
    /// </revision>
    /// <revision revisor="dev05" date="03/19/09" version="1.0.9.3">
    ///     Return an OperationResult from IsValid().
    /// </revision>
    /// <revision revisor="dev05" date="03/25/09" version="1.0.9.9">
    ///     Added SortedUpdateFile method.
    /// </revision>
    /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
    ///     Formatting changes
    /// </revision>
    [Serializable]
    public class FileManifest
    {
        #region Fields

        /// <summary>The path containing the files</summary>
        private string dataPath = String.Empty;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="FileManifest"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor = "dev05" date="03/16/09" version="1.0.8.27">
        ///     Method created.
        /// </revision>
        public FileManifest()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="FileManifest"/> class.
        /// </summary>
        /// <param name="dataPath">The path containing the files</param>
        /// <externalUnit/>
        /// <revision revisor = "dev05" date="03/16/09" version="1.0.8.27">
        ///     Method created.
        /// </revision>
        public FileManifest(string dataPath)
        {
            this.dataPath = dataPath;
            this.ItemList = new FileManifestItemList();
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the list of files in the manifest.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev05" date="03/16/09" version="1.0.8.27">
        ///     Property created.
        /// </revision>
        public FileManifestItemList ItemList
        {
            get;
            set;
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Create a manifest from an XML file.
        /// </summary>
        /// <param name="fileName">Full pathname of XML file</param>
        /// <returns>Deserialized FileManifest object</returns>
        /// <externalUnit/>
        /// <revision revisor="dev05" date="03/16/09" version="1.0.8.27">
        ///     Method created.
        /// </revision>
        public static FileManifest FromXmlFile(string fileName)
        {
            FileManifest result = null;

            if (File.Exists(fileName))
            {
                // Input XML serializer.
                var serializer = new XmlSerializer(typeof(FileManifest));

                using (var fs = new FileStream(fileName, FileMode.Open))
                {
                    result = (FileManifest) serializer.Deserialize(fs);
                    result.dataPath = Path.GetDirectoryName(fileName);
                }
            }

            return result;
        }

        /// <summary>
        ///     Serialize to an XML file.
        /// </summary>
        /// <param name="fileName">Full pathname of XML file</param>
        /// <returns>
        ///     <c>true</c> if file exists; otherwise, <c>false</c>.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev05" date="03/16/09" version="1.0.8.27">
        ///     Method created.
        /// </revision>
        public bool ToXmlFile(string fileName)
        {
            // Output XML serializer.
            var serializer = new XmlSerializer(typeof(FileManifest));

            // Output file stream writer.
            using (var writer = new StreamWriter(fileName))
            {
                serializer.Serialize(writer, this);
            }

            return File.Exists(fileName);
        }

        /// <summary>
        ///     Transform XML string to bytes
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>
        ///     The byte array containing the XML.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        public byte[] ToXmlFileBytes(string fileName)
        {
            byte[] xmlBytes = null;

            // Output XML serializer.
            var serializer = new XmlSerializer(typeof(FileManifest));

            using (var stream = new MemoryStream())
            {
                // Output file stream writer.
                using (var writer = new StreamWriter(stream))
                {
                    serializer.Serialize(writer, this);

                    xmlBytes = stream.ToArray();
                }
            }

            return xmlBytes;
        }

        /// <summary>
        ///     Add a file to the manifest.
        /// </summary>
        /// <param name="fileName">Relative filename</param>
        /// <externalUnit/>
        /// <revision revisor="dev05" date="03/16/09" version="1.0.8.27">
        ///     Method created.
        /// </revision>
        public void AddFile(string fileName)
        {
            this.ItemList.Add(new FileManifestItem(this.dataPath, fileName));
        }

        /// <summary>
        ///     Adds the file.
        /// </summary>
        /// <param name="fileData">The file data.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>
        ///     <c>true</c> if the file was added; otherwise, <c>false</c>.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        public bool AddFile(byte[] fileData, string fileName)
        {
            bool failed = false;

            int countBefore = this.ItemList.Count;

            try
            {
                this.ItemList.Add(new FileManifestItem(fileData, fileName));
            }
            catch (Exception)
            {
                failed = true;
            }

            return (this.ItemList.Count > countBefore) && (failed == false);
        }

        /// <summary>
        ///     Determines whether [contains] [the specified filename].
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <returns>
        /// 	<c>true</c> if [contains] [the specified filename]; 
        ///     otherwise, <c>false</c>.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        public bool Contains(string filename)
        {
            bool containsItem = false;

            foreach (var item in this.ItemList)
            {
                if (item.FileName.Equals(
                    filename, StringComparison.CurrentCultureIgnoreCase))
                {
                    containsItem = true;
                    
                    // found it, so stop looking
                    break;
                }
            }

            return containsItem;
        }

        /// <summary>
        ///     Removes the item.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <returns>
        ///     <c>true</c> if item was removed; otherwise, <c>false</c>.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        public bool RemoveItem(string filename)
        {
            bool removed = false;

            FileManifestItem manifestItem = null;

            foreach(var item in this.ItemList)
            {
                if (item.FileName.Equals(
                    filename, StringComparison.CurrentCultureIgnoreCase))
                {
                    manifestItem = item;

                    // found it, so stop looking
                    break;
                }
            }

            if (manifestItem != null)
            {
                removed = this.ItemList.Remove(manifestItem);
            }
            else
            {
                // not there so removal succeeds ok.
                removed = true;
            }

            return removed;
        }

        /// <summary>
        ///     Add a file to the manifest, in lexical ordered position by
        ///     filename, updating its entry if it's already there. We use this
        ///     ordering to avoid giving away voter order in the tabulator
        ///     artifact manifest.
        /// </summary>
        /// <param name="fileName">Relative filename</param>
        /// <returns>
        ///     <c>true</c> if updated; otherwise, <c>false</c>.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev05" date="03/25/09" version="1.0.9.9">
        ///     Method created.
        /// </revision>
        /// <revision revisor="dev06" date="4/22/09" version="1.0.11.10">
        ///     Method updated to return bool for success..
        /// </revision>
        public bool SortedUpdateFile(string fileName)
        {
            bool updated = false;

            try
            {
                // New manifest item for this file.
                var newItem = new FileManifestItem(this.dataPath, fileName);

                // Index of first entry whose filename is greater than or 
                // equal to the current filename.
                int index = this.ItemList.FindIndex(
                    delegate(FileManifestItem item)
                        {
                            return String.Compare(item.FileName, fileName) >= 0;
                        });

                if (index >= 0)
                {
                    // Remove entry if we already have it.
                    if (this.ItemList[index].FileName == fileName)
                    {
                        this.ItemList.RemoveAt(index);
                    }

                    this.ItemList.Insert(index, newItem);
                }
                else
                {
                    // Add new item to end.
                    this.ItemList.Add(newItem);
                }

                updated = true;
            }
            catch (Exception)
            {
                updated = false;
            }

            return updated;
        }

        /// <summary>
        ///     Validate the hashes of all files in the manifest.
        /// </summary>
        /// <returns>true if all hashes are valid, false otherwise</returns>
        /// <externalUnit/>
        /// <revision revisor="dev05" date="03/16/09" version="1.0.8.27">
        ///     Method created.
        /// </revision>
        /// <revision revisor = "dev05" date="03/19/09" version="1.0.9.3">
        ///     Return an OperationResult.
        /// </revision>
        public OperationResult IsValid()
        {
            // Returned result.
            var result = new OperationResult(true);

            foreach (FileManifestItem item in this.ItemList)
            {
                result = item.IsValid(this.dataPath);
                if (!result.Succeeded)
                {
                    break;
                }
            }

            return result;
        }

        #endregion
    }
}
