// -----------------------------------------------------------------------------
// <copyright file="SignedArchive.cs" company="Sequoia Voting Systems">
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     SignedArchive class implementation.
// </summary>
// <revision revisor="dev05" date="03/06/09" version="1.0.8.17">
//     File created.
// </revision>
// <revision revisor="dev05" date="03/16/09" version="1.0.8.27">
//     File modified.
// </revision>
// <revision revisor="dev05" date="03/19/09" version="1.0.9.3">
//     File modified.
// </revision>
// <revision revisor="dev05" date="03/25/09" version="1.0.9.9">
//     File modified.
// </revision>
// <revision revisor="dev13" date="11/17/2009" version="1.1.3.5">
//     File modified.
// </revision>
// -----------------------------------------------------------------------------
namespace Sequoia.Utilities.Compression
{
    #region Using directives
    
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Security.Cryptography;

    using ICSharpCode.SharpZipLib.Zip;

    using Sequoia.DomainObjects;
    using Sequoia.Utilities;
    using Sequoia.Utilities.Encryption;
    using Sequoia.Utilities.IO;

    #endregion

    /// <summary>
    ///     A zip file, with a corresponding signature file for each file added.
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev05" date="03/06/09" version="1.0.8.17">
    ///     Class created.
    /// </revision>
    /// <revision revisor="dev05" date="03/16/09" version="1.0.8.27">
    ///     Pass in a time stamp for output zip files and their entries.
    /// </revision>
    /// <revision revisor="dev05" date="03/16/09" version="1.0.8.27">
    ///     Added a manifest to archives.
    /// </revision>
    /// <revision revisor="dev05" date="03/19/09" version="1.0.9.3">
    ///     Return an OperationResult, instead of a bool, from various methods.
    /// </revision>
    /// <revision revisor="dev05" date="03/25/09" version="1.0.9.9">
    ///     StyleCop updates.
    /// </revision>
    /// <revision revisor="dev13" date="11/17/2009" version="1.1.3.5">
    ///     Formatting changes.
    /// </revision>
    public class SignedArchive : IDisposable
    {
        #region Fields

        /// <summary>I/O buffer length</summary>
        private const int BufferLength = 4096;

        /// <summary>
        ///     RSA provider for asymmetric encryption and decryption
        /// </summary>
        private RSACryptoServiceProvider rsaProvider = null;

        /// <summary>Provider for signing and verification</summary>
        private SequoiaCryptoProvider sequoiaProvider = null;

        /// <summary>Output timestamp for zip file and its entries</summary>
        private DateTime timeStamp = DateTime.Now;

        /// <summary>
        ///     Manifest, for validating the set of files as a whole
        /// </summary>
        private FileManifest manifest = null;

        /// <summary>SharpZipLib output stream</summary>
        private ZipOutputStream outputStream = null;

        /// <summary>SharpZipLib input stream</summary>
        private ZipInputStream inputStream = null;

        /// <summary>Path containing archive input/output files</summary>
        private string dataPath = String.Empty;

        /// <summary>Archive full pathname</summary>
        private string archiveName = String.Empty;

        /// <summary>File I/O buffer</summary>
        private byte[] buffer = null;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="SignedArchive"/> class.
        /// </summary>
        /// <param name="dataPath">Path where archive and files live.</param>
        /// <param name="fileName">Archive file name, without path.</param>
        /// <param name="shortKey">whether or not to use the short key.</param>
        /// <externalUnit/>
        /// <revision revisor="dev05" date="03/06/09" version="1.0.8.17">
        ///     Method created.
        /// </revision>
        public SignedArchive(
            string dataPath,
            string fileName,
            bool shortKey)
        {
            this.dataPath = dataPath;
            this.archiveName = Path.Combine(dataPath, fileName);
            this.sequoiaProvider = new SequoiaCryptoProvider(shortKey);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="SignedArchive"/> class.
        /// </summary>
        /// <param name="dataPath">Path where archive and files live</param>
        /// <param name="fileName">Archive file name, without path</param>
        /// <externalUnit/>
        /// <revision revisor="dev05" date="03/06/09" version="1.0.8.17">
        ///     Method created.
        /// </revision>
        public SignedArchive(
            string dataPath,
            string fileName)
            : this(dataPath, fileName, true)
        {
        }

        #endregion

        #region Public methods

        /// <summary>
        ///     Extract and verify all files from an archive.
        /// </summary>
        /// <param name="path">Destination path for extracted files</param>
        /// <param name="fileName">Archive file pathname</param>
        /// <param name="rsa">RSA provider, with public key</param>
        /// <returns>Operation result</returns>
        /// <externalUnit/>
        /// <revision revisor="dev05" date="03/06/09" version="1.0.8.17">
        ///     Method created.
        /// </revision>
        /// <revision revisor="dev05" date="03/19/09" version="1.0.9.3">
        ///     Return an OperationResult.
        /// </revision>
        public static OperationResult ExtractAll(
            string path,
            string fileName,
            RSACryptoServiceProvider rsa)
        {
            return SignedArchive.ExtractAll(
                path,
                fileName,
                rsa,
                true);
        }

        /// <summary>
        ///     Extract and verify all files from an archive.
        /// </summary>
        /// <param name="path">Destination path for extracted files</param>
        /// <param name="fileName">Archive file pathname</param>
        /// <param name="rsa">RSA provider, with public key</param>
        /// <param name="shortKey">if set to <c>true</c> [short key].</param>
        /// <returns>Operation result</returns>
        /// <externalUnit/>
        /// <revision revisor="dev05" date="03/06/09" version="1.0.8.17">
        ///     Method created.
        /// </revision>
        /// <revision revisor="dev05" date="03/19/09" version="1.0.9.3">
        /// Return an OperationResult.
        /// </revision>
        public static OperationResult ExtractAll(
            string path,
            string fileName,
            RSACryptoServiceProvider rsa,
            bool shortKey)
        {
            using (var sa = new SignedArchive(path, fileName, shortKey))
            {
                sa.OpenRead(rsa);
                return sa.ExtractAllFiles(false);
            }
        }

        /// <summary>
        ///     Verify all files from an archive.
        /// </summary>
        /// <param name="path">Destination path for extracted files</param>
        /// <param name="fileName">Archive file pathname</param>
        /// <param name="rsa">RSA provider, with public key</param>
        /// <param name="shortKey">if set to <c>true</c> [short key].</param>
        /// <returns>Operation result</returns>
        /// <externalUnit/>
        /// <revision revisor="dev05" date="03/06/09" version="1.0.8.17">
        ///     Method created.
        /// </revision>
        /// <revision revisor="dev05" date="03/19/09" version="1.0.9.3">
        ///     Return an OperationResult.
        /// </revision>
        public static OperationResult VerifyAll(
            string path,
            string fileName,
            RSACryptoServiceProvider rsa,
            bool shortKey)
        {
            using (var sa = new SignedArchive(path, fileName, shortKey))
            {
                sa.OpenRead(rsa);
                return sa.ExtractAllFiles(true);
            }
        }

        /// <summary>
        ///     Verify all files from an archive.
        /// </summary>
        /// <param name="path">Destination path for extracted files</param>
        /// <param name="fileName">Archive file pathname</param>
        /// <param name="rsa">RSA provider, with public key</param>
        /// <returns>Operation result</returns>
        /// <externalUnit/>
        /// <revision revisor="dev05" date="03/06/09" version="1.0.8.17">
        ///     Method created.
        /// </revision>
        /// <revision revisor="dev05" date="03/19/09" version="1.0.9.3">
        ///     Return an OperationResult.
        /// </revision>
        public static OperationResult VerifyAll(
            string path,
            string fileName,
            RSACryptoServiceProvider rsa)
        {
            return SignedArchive.VerifyAll(
                path,
                fileName,
                rsa,
                true);
        }

        /// <summary>
        ///     Open an archive for adding files.
        /// </summary>
        /// <param name="rsa">RSA encryption/decryption provider</param>
        /// <param name="timeStamp">
        ///     Timestamp for zip file and its entries
        /// </param>
        /// <externalUnit/>
        /// <revision revisor="dev05" date="03/06/09" version="1.0.8.17">
        ///     Method created.
        /// </revision>
        /// <revision revisor="dev05" date="03/16/09" version="1.0.8.27">
        ///     Pass in timestamp.
        /// </revision>
        /// <revision revisor="dev05" date="03/16/09" version="1.0.8.27">
        ///     Added manifest to archive.
        /// </revision>
        public void OpenWrite(RSACryptoServiceProvider rsa, DateTime timeStamp)
        {
            this.rsaProvider = rsa;
            this.timeStamp = timeStamp;
            this.manifest = new FileManifest(this.dataPath);
            this.outputStream = new ZipOutputStream(
                File.Create(this.archiveName));

            // 0-9, 9 being the highest compression
            this.outputStream.SetLevel(9);
            this.buffer = new byte[BufferLength];
        }

        /// <summary>
        ///     Add a file, plus a corresponding signature file, to the archive.
        /// </summary>
        /// <param name="fileName">File name, without path</param>
        /// <externalUnit/>
        /// <revision revisor="dev05" date="03/06/09" version="1.0.8.17">
        ///     Method created.
        /// </revision>
        /// <revision revisor="dev05" date="03/16/09" version="1.0.8.27">
        ///     Add files to the manifest.
        /// </revision>
        public void AddFile(string fileName)
        {
            // Path name of file.
            string pathName = Path.Combine(this.dataPath, fileName);
            
            this.sequoiaProvider.CreateSignature(pathName);
            this.AddOneFile(fileName);
            this.AddOneFile(fileName + ".sig");
            this.manifest.AddFile(fileName);
            this.manifest.AddFile(fileName + ".sig");
        }

        /// <summary>
        ///     Open archive for extracting files.
        /// </summary>
        /// <param name="rsa">RSA encryption/decryption provider</param>
        /// <externalUnit/>
        /// <revision revisor="dev05" date="03/06/09" version="1.0.8.17">
        ///     Method created.
        /// </revision>
        public void OpenRead(RSACryptoServiceProvider rsa)
        {
            this.rsaProvider = rsa;
            this.inputStream = new ZipInputStream(
                File.OpenRead(this.archiveName));
            this.buffer = new byte[SignedArchive.BufferLength];
        }

        /// <summary>
        ///     Extract a file, plus its corresponding signature file, from the
        ///     archive, and verify the signature.
        /// </summary>
        /// <param name="fileName">Name of extracted file (output)</param>
        /// <param name="verified">
        ///     true if file is verified against its signature file, false
        ///     otherwise (output).
        /// </param>
        /// <returns>
        ///     true if we extracted the file pair, false if we reached the end
        ///     of the archive.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev05" date="03/06/09" version="1.0.8.17">
        ///     Method created.
        /// </revision>
        public bool ExtractFile(ref string fileName, ref bool verified)
        {
            bool result = false;    // Returned result

            verified = false;
            fileName = this.ExtractOneFile();

            if (fileName != String.Empty)
            {
                // Next filename from archive, should be the signature for the
                // current file.
                string sigFileName = this.ExtractOneFile();

                if (sigFileName != fileName + ".sig")
                {
                    throw new Exception(
                        String.Format(
                            "Signed archive {0} is missing signature for file "
                            + "{1}",
                            this.archiveName,
                            fileName));
                }

                result = true;
                verified = this.sequoiaProvider.VerifySignature(
                    Path.Combine(this.dataPath, sigFileName),
                    Path.Combine(this.dataPath, fileName));
            }

            return result;
        }

        /// <summary>
        ///     Close the archive.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev05" date="03/06/09" version="1.0.8.17">
        ///     Method created.
        /// </revision>
        /// <revision revisor="dev05" date="03/16/09" version="1.0.8.27">
        ///     Give the zip file the timestamp that was passed in.
        /// </revision>
        /// <revision revisor="dev05" date="03/16/09" version="1.0.8.27">
        ///     Add manifest and its signature to output archive.
        /// </revision>
        public void Close()
        {
            if (this.outputStream != null)
            {
                // Full pathname of manifest file.
                string manifestName =
                    Path.Combine(this.dataPath, "Manifest.xml");

                // Add manifest file and its signature file to the archive.
                this.manifest.ToXmlFile(manifestName);
                this.sequoiaProvider.CreateSignature(manifestName);
                this.AddOneFile("Manifest.xml");
                this.AddOneFile("Manifest.xml.sig");

                this.outputStream.Finish();
                this.outputStream.Close();
                this.outputStream = null;

                FileManager.TouchFile(this.archiveName, this.timeStamp);
            }

            if (this.inputStream != null)
            {
                this.inputStream.Close();
                this.inputStream = null;
            }

            this.buffer = null;
        }

        #endregion

        #region IDisposable implementation

        /// <summary>
        ///     Release resources.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev05" date="03/06/09" version="1.0.8.17">
        ///     Method created.
        /// </revision>
        public void Dispose()
        {
            this.Close();
        }

        #endregion

        #region Private methods

        /// <summary>
        ///     Add one file to the archive.
        /// </summary>
        /// <param name="fileName">Name of file, without path</param>
        /// <externalUnit/>
        /// <revision revisor="dev05" date="03/06/09" version="1.0.8.17">
        ///     Method created.
        /// </revision>
        /// <revision revisor="dev05" date="03/16/09" version="1.0.8.27">
        ///     Give the entry the passed in timestamp.
        /// </revision>
        private void AddOneFile(string fileName)
        {
            // Full path name of file.
            string pathName = Path.Combine(this.dataPath, fileName);

            // Zip file entry.
            ZipEntry entry = new ZipEntry(fileName);

            entry.DateTime = this.timeStamp;
            entry.Size = (new FileInfo(pathName)).Length;
            this.outputStream.PutNextEntry(entry);

            using (FileStream fs = File.OpenRead(pathName))
            {
                int sourceBytes;    // Source bytes in current chunk

                do
                {
                    sourceBytes = fs.Read(this.buffer, 0, this.buffer.Length);
                    this.outputStream.Write(this.buffer, 0, sourceBytes);
                }
                while (sourceBytes > 0);
            }
        }

        /// <summary>
        ///     Extract and verify all files from the archive, and maybe delete
        ///     them.
        /// </summary>
        /// <param name="deleteFiles">
        ///     true to delete regular files, in addition to .sig files.
        /// </param>
        /// <returns>Operation result</returns>
        /// <externalUnit/>
        /// <revision revisor="dev05" date="03/06/09" version="1.0.8.17">
        ///     Method created.
        /// </revision>
        /// <revision revisor="dev05" date="03/16/09" version="1.0.8.27">
        ///     Added manifest validation.
        /// </revision>
        /// <revision revisor="dev05" date="03/19/09" version="1.0.9.3">
        ///     Return an OperationResult.
        /// </revision>
        /// <revision revisor="dev05" date="03/25/09" version="1.0.9.9">
        ///     StyleCop updates.
        /// </revision>
        private OperationResult ExtractAllFiles(bool deleteFiles)
        {
            var result = new OperationResult(true); // Returned result
            string fileName = String.Empty;     // Current file name
            bool verified = true;               // Is current file verified?

            // List of archive filenames, not including signature files.
            List<string> fileNames = new List<string>();

            while (this.ExtractFile(ref fileName, ref verified))
            {
                if (!verified)
                {
                    string error = String.Format(
                        "Archive verification failed for {0}",
                        fileName);

                    result = new OperationResult(false, error);
                }

                if (fileName == "Manifest.xml")
                {
                    // Recreate the manifest from the XML file.
                    this.manifest = FileManifest.FromXmlFile(
                        Path.Combine(this.dataPath, "Manifest.xml"));
                }
                else
                {
                    fileNames.Add(fileName);
                }
            }

            if (this.manifest == null)
            {
                string error = String.Format(
                    "Manifest missing for {0}",
                    this.archiveName);

                // We should have a manifest
                result = new OperationResult(false, error);
            }
            else if (result.Succeeded)
            {
                // Validate the manifest against the rest of the archive files.
                result = this.ValidateManifest(fileNames);
            }

            // Delete signature files, plus optionally the data files.
            foreach (string name in fileNames)
            {
                string pathName = Path.Combine(this.dataPath, name);

                if (deleteFiles)
                {
                    File.Delete(pathName);
                }

                // The .sig file has done its job, so delete it.
                File.Delete(pathName + ".sig");
            }

            File.Delete(Path.Combine(this.dataPath, "Manifest.xml"));
            File.Delete(Path.Combine(this.dataPath, "Manifest.xml.sig"));

            return result;
        }

        /// <summary>
        ///     Extract one file from the archive.
        /// </summary>
        /// <returns>File name, or empty string if end of archive</returns>
        /// <externalUnit/>
        /// <revision revisor="dev05" date="03/06/09" version="1.0.8.17">
        ///     Method created.
        /// </revision>
        private string ExtractOneFile()
        {
            // Returned filename.
            string result = String.Empty;

            // Zip file entry.
            ZipEntry entry = this.inputStream.GetNextEntry();

            if (entry != null)
            {
                result = entry.Name;
                if (result != String.Empty)
                {
                    using (FileStream writer = File.Create(
                               Path.Combine(this.dataPath, result)))
                    {
                        int size = BufferLength;    // Number of bytes read
                        while (size > 0)
                        {
                            size = this.inputStream.Read(
                                this.buffer,
                                0,
                                BufferLength);
                            if (size > 0)
                            {
                                writer.Write(this.buffer, 0, size);
                            }
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        ///     Validate the manifest against the files and the archive. The
        ///     files must match the hashes, and the list of files in the
        ///     archive and manifest must match.
        /// </summary>
        /// <param name="archiveFileNames">
        ///     List of archive file names, not including signatures
        /// </param>
        /// <returns>Operation result</returns>
        /// <externalUnit/>
        /// <revision revisor="dev05" date="03/16/09" version="1.0.8.27">
        ///     Method created.
        /// </revision>
        /// <revision revisor="dev05" date="03/19/09" version="1.0.9.3">
        ///     Return an OperationResult.
        /// </revision>
        /// <revision revisor="dev05" date="03/25/09" version="1.0.9.9">
        ///     StyleCop updates.
        /// </revision>
        private OperationResult ValidateManifest(List<string> archiveFileNames)
        {
            // Returned result.
            OperationResult result = this.manifest.IsValid();

            if (result.Succeeded)
            {
                // List of manifest filenames.
                var manifestFileNames = new List<string>();

                // Make list of manifest file names, and check them against the
                // archive file names.
                foreach (FileManifestItem item in this.manifest.ItemList)
                {
                    if (item.FileName.EndsWith(".sig"))
                    {
                        // Skip signature files.
                        continue;
                    }

                    manifestFileNames.Add(item.FileName);
                    if (!archiveFileNames.Contains(item.FileName))
                    {
                        string error = String.Format(
                            "Archive is missing entry {0}",
                            item.FileName);

                        result = new OperationResult(false, error);
                        break;
                    }
                }

                if (result.Succeeded)
                {
                    // Make sure that all archive files appear in the manifest.
                    foreach (string name in archiveFileNames)
                    {
                        if (!manifestFileNames.Contains(name))
                        {
                            string error = String.Format(
                                "Manifest is missing entry {0}",
                                name);

                            result = new OperationResult(false, error);
                            break;
                        }
                    }
                }
            }

            return result;
        }

        #endregion
    }
}
