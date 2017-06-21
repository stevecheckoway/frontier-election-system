// -----------------------------------------------------------------------------
// <copyright file="FileSystemPersister.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2008 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the FileSystemPersister class.
// </summary>
// <revision revisor="dev06" date="11/12/2008 11:50:17 AM" version="1.0.?.0">
//     File Created
// </revision>  
// <revision revisor="dev13" date="1/23/2009" version="1.0.5.5">
//     Added overloaded constructor with overwrite flag
// </revision> 
// <revision revisor="dev16" date="1/26/2009" version="1.0.5.7">
//  File Modified
//  </revision>
// <revision revisor = "dev05" date="02/05/09" version="1.0.6.1">
//     Replace cryptic deserialization exception message thrown by
//     XmlSerializer.Deserialize().
// </revision>
// <revision revisor = "dev05" date="02/05/09" version="1.0.6.1">
//     File modified
// </revision>
// <revision revisor="dev13" date="02/27/09" version="1.0.8.1001">
//     File modified
// </revision>
// <revision revisor="dev05" date="03/02/09" version="1.0.8.13">
//     File modified.
// </revision>
// <revision revisor="dev13" date="3/2/2009" version="1.0.8.1301">
//     File modified
// </revision>
// <revision revisor="dev05" date="03/06/09" version="1.0.8.17">
//     File modified.
// </revision>
// <revision revisor="dev05" date="03/19/09" version="1.0.9.3">
//     File modified.
// </revision>
// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
//     File modified.
// </revision>
// -----------------------------------------------------------------------------

namespace Sequoia.Utilities.Persistence
{
    #region Using directives

    using System;
    using System.IO;
    using System.Security;
    using System.Security.Cryptography;
    using System.Text;
    using System.Xml.Serialization;

    using Sequoia.DomainObjects;
    using Sequoia.DomainObjects.Persistence;
    using Sequoia.Utilities.Encryption;

    #endregion

    /// <summary>
    ///	    Class for persisting to the file system
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev06" date="11/12/2008" version="1.0.0.0">
    ///     Class created.
    /// </revision>
    /// <revision revisor="dev06" date="11/18/2009" version="1.1.3.6">
    ///     Formatting changes
    /// </revision>
    public class FileSystemPersister : IPersister
    {
        #region Fields

        /// <summary>
        ///     param for the path to the file on the USB voter key
        /// </summary>
        private string pathToFile = string.Empty;

        /// <summary>
        /// param for storing the name of the file which will be saved
        /// </summary>
        private string nameOfPersistedFile = string.Empty;

        /// <summary>
        ///     param for whether or not any existing file should be overwritten.
        /// </summary>
        private bool overwrite = false;

        /// <summary>
        ///     param for whether or not to digitally sign the file on persist
        /// </summary>
        private bool sign = false;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="FileSystemPersister"/> class.
        /// </summary>
        /// <param name="pathToFile">The path to file.</param>
        /// <param name="nameOfPersistedFile">The name of persisted file.</param>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/9/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public FileSystemPersister(
            string pathToFile, string nameOfPersistedFile)
        {
            this.pathToFile = pathToFile;
            this.nameOfPersistedFile = nameOfPersistedFile;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="FileSystemPersister"/> class.
        /// </summary>
        /// <param name="pathToFile">The path to file.</param>
        /// <param name="nameOfPersistedFile">The name of persisted file.</param>
        /// <param name="overwrite">if set to <c>true</c> [overwrite].</param>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="1/23/2009" version="1.0.5.5">
        ///     Member Created
        /// </revision>
        public FileSystemPersister(
            string pathToFile, 
            string nameOfPersistedFile, 
            bool overwrite)
        {
            this.pathToFile = pathToFile;
            this.nameOfPersistedFile = nameOfPersistedFile;
            this.overwrite = overwrite;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="FileSystemPersister"/> class.
        /// </summary>
        /// <param name="pathToFile">The path to file.</param>
        /// <param name="nameOfPersistedFile">
        ///     The name of persisted file.
        /// </param>
        /// <param name="overwrite">if set to <c>true</c> [overwrite].</param>
        /// <param name="sign">if set to <c>true</c> [sign].</param>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="2/27/2009" version="1.0.8.1001">
        ///     Member Created
        /// </revision>
        public FileSystemPersister(
            string pathToFile, 
            string nameOfPersistedFile, 
            bool overwrite, 
            bool sign)
        {
            this.pathToFile = pathToFile;
            this.nameOfPersistedFile = nameOfPersistedFile;
            this.overwrite = overwrite;
            this.sign = sign;
        }

        #endregion

        #region Public Properties

        #endregion

        #region Public Methods

        #region Persist<T>
        /// <summary>
        ///     Persists this instance.
        /// </summary>
        /// <typeparam name="T">The type of the item to persist.</typeparam>
        /// <param name="item">The item whic is being persisted.</param>
        /// <returns>
        ///     a <see cref="PersistenceResult"/> object.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/10/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev16" date="1/26/2009" version="1.0.5.7">
        ///     Modified method to use SerializedData instead of ToString() method
        /// </revision>
        /// <revision revisor="dev13" date="2/27/2009" verison="1.0.8.1001">
        ///     Added check for whether the file should be signed
        /// </revision>
        /// <revision revisor="dev13" date="3/2/2009" verison="1.0.8.1301">
        ///     Changed position of signing check so that it doesn't crash
        ///     the persister.
        /// </revision>
        OperationResult IPersister.Persist<T>(T item)
        {
            var result = new OperationResult(false);

            string itemSerializedXml = item.SerializedData;

            try
            {
                DirectoryInfo directoryInfo =
                    new DirectoryInfo(this.pathToFile);

                if (directoryInfo.Exists == false)
                {
                    // try to create
                    directoryInfo.Create();
                }
                
                if (this.overwrite)
                {
                    FileInfo fileInfo =
                        new FileInfo(
                            Path.Combine(
                                this.pathToFile, this.nameOfPersistedFile));

                    if (fileInfo.Exists)
                    {
                        fileInfo.Delete();
                    }
                }

                FileStream xmlStream = File.Open(
                    Path.Combine(this.pathToFile, this.nameOfPersistedFile),
                    FileMode.OpenOrCreate,
                    FileAccess.ReadWrite);

                StreamWriter writer = new StreamWriter(
                    xmlStream, Encoding.Unicode);

                writer.Write(itemSerializedXml);

                // close the writer and release the resources
                writer.Close();
                xmlStream.Close();
                xmlStream.Dispose();

                // completely get rid of the writer
                writer.Dispose();

                result.Succeeded = true;
                result.Details = string.Format(
                    "Results file saved at {0}", this.pathToFile);

                if (this.sign)
                {
                    this.CreateSignature(this.nameOfPersistedFile);
                }
            }
            catch (DirectoryNotFoundException exception)
            {
                result.Details = exception.ToString(); 
            }
            catch (SecurityException exception)
            {
                result.Details = exception.ToString();
            }
            catch (IOException exception)
            {
                result.Details = exception.ToString();
            }
            catch (Exception exception)
            {
                result.Details = exception.ToString();
            }

            return result;
        }
        #endregion

        #region Recreate<T>
        /// <summary>
        ///     Recreates this instance.
        /// </summary>
        /// <typeparam name="T">The type of the item to recreate.</typeparam>
        /// <returns>A value of type T.</returns>
        /// <externalUnit cref="pathToFile"/>
        /// <externalUnit cref="nameOfPersistedFile"/>
        /// <externalUnit cref="FileInfo"/>
        /// <externalUnit cref="FileStream"/>
        /// <externalUnit cref="StreamReader"/>
        /// <externalUnit cref="XmlSerializer"/>
        /// <exception cref="IOException">
        ///     We don't throw this, but it can happen, but this is an 
        ///     inappropriate place to handle it, so notifying the caller.
        /// </exception>  
        /// <exception cref="SecurityException">
        ///     We don't throw this, but it can happen, but this is an 
        ///     inappropriate place to handle it, so notifying the caller.
        /// </exception>
        /// <revision revisor="dev06" date="11/12/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev05" date="02/05/09" version="1.0.6.1">
        ///     Rethrow cryptic exceptions with a more helpful message.
        /// </revision>
        T IPersister.Recreate<T>()
        {
            // get the full path to the persisted file
            string pathToPersistedFile = Path.Combine(
                this.pathToFile, this.nameOfPersistedFile);

            // create an item we are deserializing and set to its default value
            // - could be null
            T item = default(T);

            try
            {
                // check to see if the file exists and reload
                FileInfo info = new FileInfo(pathToPersistedFile);

                // check to see if our file exists
                if (info.Exists == true && info.Length > 0)
                {
                    // open the file to get the contents
                    FileStream stream = info.OpenRead();

                    // read the file and deserialize
                    using (var reader = new StreamReader(stream))
                    {
                        // create xml serializer to transform sample xml 
                        // into cartridge store
                        var serializer = new XmlSerializer(typeof (T));

                        // recreate the cartridge store object from the xml data
                        item = (T) serializer.Deserialize(reader);
                    }
                }
            }
            catch (InvalidOperationException)
            {
                // If the deserialization fails, the exception message is 
                // something like, "There is an error in XML document (2, 2)". 
                // It's more helpful to know which file it was.
                throw new InvalidOperationException(
                    "Error reading XML file " + pathToPersistedFile);
            }

            // return the deserialized item
            return item;
        }
        #endregion

        #endregion

        #region Public Events

        #endregion

        #region Private Methods

        /// <summary>
        ///     Creates the signature.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="2/27/2009" version="1.0.8.1001">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev05" date="03/02/09" version="1.0.8.13">
        ///     Pass full pathname to SequoiaCryptoProvider.
        /// </revision>
        /// <revision revisor="dev05" date="03/06/09" version="1.0.8.17">
        ///     Tabulator private key now lives in "PrivateKey.xml".
        /// </revision>
        private void CreateSignature(string fileName)
        {
            // create signature
            var sequoiaCryptoProvider = new SequoiaCryptoProvider();
            sequoiaCryptoProvider.CreateSignature(
                Path.Combine(this.pathToFile, fileName));
        }

        #endregion
    }
}
