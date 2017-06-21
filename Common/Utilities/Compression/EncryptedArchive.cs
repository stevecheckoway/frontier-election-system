// -----------------------------------------------------------------------------
// <copyright file="EncryptedArchive.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the EncryptedArchive class.
// </summary>
// <revision revisor="dev06" date="4/16/2009" version="1.0.11.6">
//     File Created
// </revision>
// <revision revisor="dev13" date="11/17/2009" version="1.1.3.5">
//     File Created
// </revision>
// -----------------------------------------------------------------------------

namespace Sequoia.Utilities.Compression
{
    #region Using directives

    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Security.Cryptography;
    using System.Threading;

    using ICSharpCode.SharpZipLib.Zip;

    using Sequoia.DomainObjects;
    using Sequoia.Utilities;
    using Sequoia.Utilities.Encryption;
    using Sequoia.Utilities.IO;

    #endregion

    /// <summary>
    ///	    EncryptedArchive is a class that is based on the signed archive, 
    ///     except files are added unencrypted and are not signed - once all 
    ///     files are added to the archive, the archive itself is automatically 
    ///     encrypted and a sign file is created of the encrypted archive.
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev06" date="4/16/2009" version="1.0.11.6">
    ///     Class created.
    /// </revision>
    /// <revision revisor="dev13" date="11/17/2009" version="1.1.3.5">
    ///     Formatting changes
    /// </revision>
    public class EncryptedArchive : IDisposable
    {
        #region Fields

        /// <summary>I/O buffer length</summary>
        private const int BufferLength = 4096;

        /// <summary>Path containing archive input/output files</summary>
        private readonly string dataPath = String.Empty;

        /// <summary>Archive full pathname</summary>
        private readonly string archiveName = String.Empty;

        /// <summary>Provider for signing and verification</summary>
        private SequoiaCryptoProvider sequoiaProvider = null;

        /// <summary>Output timestamp for zip file and its entries</summary>
        private DateTime timeStamp = DateTime.Now;

        /// <summary>
        ///     param for auto sign
        /// </summary>
        private bool autoSign = false;

        /// <summary>
        ///     Manifest, for validating the set of files as a whole
        /// </summary>
        private FileManifest manifest = null;

        /// <summary>SharpZipLib output stream</summary>
        private ZipOutputStream outputStream = null;

        /// <summary>SharpZipLib input stream</summary>
        private ZipInputStream inputStream = null;

        /// <summary>File I/O buffer</summary>
        private byte[] buffer = null;

        /// <summary>
        ///     param for the base zip stream
        /// </summary>
        private MemoryStream baseZipStream = null;

        /// <summary>
        ///     param for whether to write to zip
        /// </summary>
        private bool writeToZip = false;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="EncryptedArchive"/> class.
        /// </summary>
        /// <param name="dataPath">The data path.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="crypto">The crypto.</param>
        /// <externalUnit/>
        /// <revision revisor="dev06" date="4/16/2009" version="1.0.11.6">
        ///     Member Created
        /// </revision>
        public EncryptedArchive(
            string dataPath, 
            string fileName, 
            SequoiaCryptoProvider crypto)
        {
            this.dataPath = dataPath;
            this.archiveName = Path.Combine(dataPath, fileName);
            this.sequoiaProvider = crypto;
        }

        #endregion

        #region Public Properties

        #endregion

        #region Public Methods

        /// <summary>
        ///     Extracts the archive.
        /// </summary>
        /// <param name="extractionLocation">The extraction location.</param>
        /// <param name="fullPathToArchive">The full path to archive.</param>
        /// <param name="crypto">The crypto.</param>
        /// <returns>
        ///     An <see cref="OperationResult" /> containing the result of 
        ///     the operation.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/17/2009" version="1.1.3.5">
        ///     Added documentation header
        /// </revision>
        public static OperationResult ExtractArchive(
            string extractionLocation,
            string fullPathToArchive,
            SequoiaCryptoProvider crypto)
        {
            OperationResult result = new OperationResult();

            using (var sa = new EncryptedArchive(
                extractionLocation, fullPathToArchive, crypto))
            {
                byte[] decryptedBytes = sa.Decrypt(
                    fullPathToArchive);

                if (decryptedBytes != null)
                {
                    sa.OpenRead(decryptedBytes);

                    result = sa.ExtractAllFiles(false);
                }
                else
                {
                    result.Succeeded = false;
                    result.Details = "Failed to decrypt archive.";
                }
            }

            return result;
        }

        /// <summary>
        ///     Extracts the archive to zip.
        /// </summary>
        /// <param name="extractionLocation">The extraction location.</param>
        /// <param name="fullPathToArchive">The full path to archive.</param>
        /// <param name="crypto">The crypto.</param>
        /// <returns>
        ///     An <see cref="OperationResult" /> containing the results
        ///     of the operation.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/17/2009" version="1.1.3.5">
        ///     Added documentation header
        /// </revision>
        public static OperationResult ExtractArchiveToZip(
            string extractionLocation,
            string fullPathToArchive,
            SequoiaCryptoProvider crypto)
        {
            OperationResult result = new OperationResult();

            string decryptedArchiveName =
                Path.ChangeExtension(
                Path.GetFileName(fullPathToArchive), "zip");

            using (var sa = new EncryptedArchive(
                extractionLocation, decryptedArchiveName, crypto))
            {
                bool decrypted = sa.DecryptToFile(
                    fullPathToArchive,
                    Path.Combine(extractionLocation, decryptedArchiveName),
                    crypto);

                if (decrypted)
                {
                    sa.OpenRead();

                    result = sa.ExtractAllFiles(false);
                }
                else
                {
                    result.Succeeded = false;
                    result.Details = "Failed to decrypt archive.";
                }
            }

            return result;
        }
        
        /// <summary>
        ///     Opens to write.
        /// </summary>
        /// <param name="timeStamp">The time stamp.</param>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/17/2009" version="1.1.3.5">
        ///     Added documentation header
        /// </revision>
        public void OpenWrite(DateTime timeStamp)
        {
            this.OpenWrite(timeStamp, false);
        }

        /// <summary>
        ///     Opens to write to zip.
        /// </summary>
        /// <param name="timeStamp">The time stamp.</param>
        /// <param name="autoSign">if set to <c>true</c> [auto sign].</param>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/17/2009" version="1.1.3.5">
        ///     Added documentation header
        /// </revision>
        public void OpenWriteToZip(DateTime timeStamp, bool autoSign)
        {
            this.writeToZip = true;
            this.autoSign = autoSign;
            this.timeStamp = timeStamp;
            this.manifest = new FileManifest(this.dataPath);

            this.outputStream = new ZipOutputStream(
                File.Create(this.archiveName));
            
            this.outputStream.SetLevel(9); // 0-9, 9 being the highest compression
            this.buffer = new byte[BufferLength];
        }

        /// <summary>
        ///     Opens to write.
        /// </summary>
        /// <param name="timeStamp">The time stamp.</param>
        /// <param name="autoSign">if set to <c>true</c> [auto sign].</param>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/17/2009" version="1.1.3.5">
        ///     Added documentation header
        /// </revision>
        public void OpenWrite(DateTime timeStamp, bool autoSign)
        {
            this.autoSign = autoSign;
            this.timeStamp = timeStamp;
            this.manifest = new FileManifest(this.dataPath);

            this.baseZipStream = new MemoryStream();

            this.outputStream = new ZipOutputStream(this.baseZipStream);
            
            this.outputStream.SetLevel(9); // 0-9, 9 being the highest compression
            this.buffer = new byte[BufferLength];
        }

        /// <summary>
        ///     Adds the file.
        /// </summary>
        /// <param name="fileData">The file data.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>
        ///     <c>true</c> if file was added.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/17/2009" version="1.1.3.5">
        ///     Added documentation header
        /// </revision>
        public bool AddFile(byte[] fileData, string fileName)
        {
            bool added = this.AddOneFileFromBytes(fileData, fileName);

            if(added)
            {
                added = this.manifest.AddFile(fileData, fileName);
            }

            return added;
        }

        /// <summary>
        ///     Adds the file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/17/2009" version="1.1.3.5">
        ///     Added documentation header
        /// </revision>
        public void AddFile(string fileName)
        {
            // Path name of file.
            string pathName = Path.Combine(this.dataPath, fileName);
            
            // add the file to the compressed archive
            this.AddOneFile(fileName);

            // add the file on the manifest
            this.manifest.AddFile(fileName);
        }

        /// <summary>
        ///     Opens to read.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/17/2009" version="1.1.3.5">
        ///     Added documentation header
        /// </revision>
        public void OpenRead()
        {
            this.inputStream = new ZipInputStream(
                File.OpenRead(this.archiveName));
            this.buffer = new byte[BufferLength];
        }

        /// <summary>
        ///     Opens to read.
        /// </summary>
        /// <param name="compressedData">The compressed data.</param>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/17/2009" version="1.1.3.5">
        ///     Added documentation header
        /// </revision>
        public void OpenRead(byte[] compressedData)
        {
            var stream = new MemoryStream(compressedData);

            this.inputStream = new ZipInputStream(stream);

            this.buffer = new byte[BufferLength];
        }

        /// <summary>
        ///     Closes this instance.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/17/2009" version="1.1.3.5">
        ///     Added documentation header
        /// </revision>
        public void Close()
        {
            if (this.outputStream != null)
            {
                // Add manifest file and its signature file to the archive.
                byte[] manifestData =
                    this.manifest.ToXmlFileBytes("Manifest.xml");
                
                this.AddOneFileFromBytes(manifestData, "Manifest.xml");

                this.outputStream.CloseEntry();

                byte[] zippedBytes = null;

                if (this.baseZipStream != null)
                {
                    zippedBytes = this.baseZipStream.ToArray();
                }

                this.outputStream.Finish();
                this.outputStream.Close();
                this.outputStream.Dispose();
                this.outputStream = null;

                string encryptedArchiveName =
                    Path.ChangeExtension(this.archiveName, "enc");

                bool encryptedCopySaved = false;
                int maxTries = 10;
                int attempt = 0;
                do
                {
                    try
                    {
                        if (this.writeToZip)
                        {
                            this.sequoiaProvider.EncryptFile(
                                this.archiveName,
                                encryptedArchiveName,
                                this.sequoiaProvider.GetPublicKey(),
                                true);
                        }
                        else
                        {
                            this.sequoiaProvider.EncryptFileData(
                                zippedBytes,
                                encryptedArchiveName,
                               this.sequoiaProvider.GetPublicKey(),
                                true);
                        }

                        // now delete the zip file
                        File.Delete(this.archiveName);

                        FileManager.TouchFile(
                                encryptedArchiveName, this.timeStamp);

                        if (this.autoSign)
                        {
                            this.sequoiaProvider.CreateSignature(
                                encryptedArchiveName);

                            FileManager.TouchFile(
                                encryptedArchiveName + ".sig", this.timeStamp);
                        }

                        encryptedCopySaved = true;
                    }
                    catch
                    {
                        // can't delete the zip file - set result item with path 
                        // so that the app tries to clean all fiels before 
                        // exiting the process
                        attempt++;
                    }
                }
                while (attempt < maxTries && encryptedCopySaved == false);
            }

            if (this.inputStream != null)
            {
                this.inputStream.Close();
                this.inputStream.Dispose();
                this.inputStream = null;
            }

            this.buffer = null;
        }

        /// <summary>
        ///     Extracts the file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>
        ///     <c>true</c> if file extracted; otherwise, <c>false</c>.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/17/2009" version="1.1.3.5">
        ///     Added documentation header
        /// </revision>
        public bool ExtractFile(ref string fileName)
        {
            bool result = false;    // Returned result

            try
            {
                fileName = this.ExtractOneFile();

                result = !string.IsNullOrEmpty(fileName);
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, 
        ///     releasing, or resetting unmanaged resources.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/17/2009" version="1.1.3.5">
        ///     Added documentation header
        /// </revision>
        public void Dispose()
        {
            this.Close();
        }

        #endregion

        #region Public Events

        #endregion

        #region Private Methods

        /// <summary>
        ///     Adds one file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/17/2009" version="1.1.3.5">
        ///     Added documentation header
        /// </revision>
        private void AddOneFile(string fileName)
        {
            // Full path name of file.
            string pathName = Path.Combine(this.dataPath, fileName);

            byte[] fileData = File.ReadAllBytes(pathName);

            this.AddOneFileFromBytes(fileData, fileName);
        }

        /// <summary>
        ///     Adds one file from bytes.
        /// </summary>
        /// <param name="fileData">The file data.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>
        ///     <c>true</c> if file added.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/17/2009" version="1.1.3.5">
        ///     Added documentation header
        /// </revision>
        private bool AddOneFileFromBytes(byte[] fileData, string fileName)
        {
            bool added = false;

            try
            {
                // Zip file entry.
                var entry = new ZipEntry(fileName);

                entry.DateTime = this.timeStamp;

                entry.Size = fileData.Length;

                this.outputStream.PutNextEntry(entry);

                this.outputStream.Write(fileData, 0, fileData.Length);

                added = true;
            }
            catch(Exception)
            {
                added = false;
            }

            return added;
        }

        /// <summary>
        ///     Decrypts the specified full path to archive.
        /// </summary>
        /// <param name="fullPathToArchive">The full path to archive.</param>
        /// <returns>
        ///     The decrypted data.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/17/2009" version="1.1.3.5">
        ///     Added documentation header
        /// </revision>
        private byte[] Decrypt(string fullPathToArchive)
        {
            byte[] decryptedData = null;

            try
            {
                // decrypt bytes so we have the zip stream again
                decryptedData = this.sequoiaProvider.DecryptFileToBytes(
                    fullPathToArchive,
                    this.sequoiaProvider.GetPrivateKey());
            }
            catch (Exception)
            {
            }

            return decryptedData;
        }

        /// <summary>
        ///     Decrypts to file.
        /// </summary>
        /// <param name="fullPathToArchive">The full path to archive.</param>
        /// <param name="pathToDecryptedFile">The path to decrypted file.</param>
        /// <param name="crypto">The crypto.</param>
        /// <returns>
        ///     <c>true</c> if decrypted; otherwise, <c>false</c>.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/17/2009" version="1.1.3.5">
        ///     Added documentation header
        /// </revision>
        private bool DecryptToFile(
            string fullPathToArchive, 
            string pathToDecryptedFile,
            SequoiaCryptoProvider crypto)
        {
            bool decrypted = false;

            try
            {
                // decrypt bytes so we have the zip stream again
                this.sequoiaProvider.DecryptFile(
                    fullPathToArchive, 
                    pathToDecryptedFile, 
                    crypto.GetPrivateKey(), 
                    true);

                decrypted = File.Exists(pathToDecryptedFile);
            }
            catch (Exception)
            {
            }

            return decrypted;
        }

        /// <summary>
        ///     Extracts all files.
        /// </summary>
        /// <param name="deleteFiles">if set to <c>true</c> [delete files].</param>
        /// <returns>
        ///     An <see cref="OperationResult" /> containing the results 
        ///     of the operation.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/17/2009" version="1.1.3.5">
        ///     Added documentation header
        /// </revision>
        private OperationResult ExtractAllFiles(bool deleteFiles)
        {
            var result = new OperationResult(true); // Returned result
            string fileName = String.Empty;     // Current file name

            // List of archive filenames, not including signature files.
            List<string> fileNames = new List<string>();

            while (this.ExtractFile(ref fileName))
            {
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
                    "Manifest missing for encrypted archive {0}",
                    this.archiveName);

                // We should have a manifest
                result = new OperationResult(false, error);
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

            //File.Delete(Path.Combine(this.dataPath, "Manifest.xml"));
            File.Delete(Path.Combine(this.dataPath, "Manifest.xml.sig"));

            return result;
        }

        /// <summary>
        ///     Extracts one file.
        /// </summary>
        /// <returns>
        ///     A string containing the file name.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/17/2009" version="1.1.3.5">
        ///     Added documentation header
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
        ///     Validates the manifest.
        /// </summary>
        /// <param name="archiveFileNames">The archive file names.</param>
        /// <returns>
        ///     An <see cref="OperationResult" /> containing the results
        ///     of the operation.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/17/2009" version="1.1.3.5">
        ///     Member Created
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
