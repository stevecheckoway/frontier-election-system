// -----------------------------------------------------------------------------
// <copyright file="SequoiaCryptoProvider.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the SequoiaCryptoProvider class.
// </summary>
// <revision revisor="dev01" date="1/26/2009" version="1.0.5.7">
//     File Created
// </revision>  
// <revision revisor="dev13" date="3/23/2009" version="1.0.9.0701">
//     File modified
// </revision>
// <revision revisor="dev13" date="11/17/2009" version="1.1.3.5">
//     File modified
// </revision> 
// -----------------------------------------------------------------------------

namespace Sequoia.Utilities.Encryption
{
    #region Using directives

    using System;
    using System.Configuration;
    using System.IO;
    using System.Reflection;
    using System.Security.Cryptography;

    #endregion

    /// <summary>
    ///	    Encryption Library
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev01" date="1/26/2009" version="1.0.5.7">
    ///     Class created.
    /// </revision>
    /// <revision revisor="dev13" date="11/17/2009" version="1.1.3.5">
    ///     Formatting changes
    /// </revision>
    public class SequoiaCryptoProvider : IDisposable
    {
        #region Fields

        /// <summary>
        /// Assymetric key size
        /// </summary>
        public const int AsymmetricKeySize = 2048;

        /// <summary>
        /// Assymetric key size
        /// </summary>
        public const int AsymmetricShortKeySize = 512;

        /// <summary>
        /// Size of the Assymetrically encrypted blaock
        /// </summary>
        private const int EncryptedBlockSize = 256;

        /// <summary>
        /// Size of the Assymetrically encrypted blaock
        /// </summary>
        private const int EncryptedShortBlockSize = 64;
        
        /// <summary>
        /// How many bits in a byte
        /// </summary>
        private const int BitsInByte = 8;

        /// <summary>
        ///     If using short or long keys
        /// </summary>
        private bool shortKeys = false;

        /// <summary>
        /// Symmetric  crypto
        /// </summary>
        private SymmetricAlgorithm symmetricCrypto = null;

        /// <summary>
        /// Asymmetric crypto
        /// </summary>
        private AsymmetricAlgorithm asymmetricCrypto = null;

        /// <summary>
        /// Hash Algorithm
        /// </summary>
        private HashAlgorithm hasher = null;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="SequoiaCryptoProvider" /> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="1/26/2009 1:23:15 PM" version="1.0.5.7">
        ///     Member Created
        /// </revision>	
        public SequoiaCryptoProvider()
        {
            // Level of encryption
            string level = "low";

            try
            {
                level =
                    ConfigurationSettings.AppSettings["EncryptionLevel"];
            }
            catch
            {
                level = "low";
            }
            
            switch(level)
            {
                case "high":
                    this.shortKeys = false;
                    break;
                case "low":
                    this.shortKeys = true;
                    break;
                default:
                    this.shortKeys = true;
                    break;
            }
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="SequoiaCryptoProvider"/> class.
        /// </summary>
        /// <param name="shortKeys">if set to <c>true</c> [short keys].</param>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="4/21/2009" version="1.0.11.9">
        ///     Member Created
        /// </revision>
        public SequoiaCryptoProvider(bool shortKeys)
        {
            this.shortKeys = shortKeys;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the asymmetric crypto.
        /// </summary>
        /// <value>The asymmetric crypto.</value>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="1/30/2009" version="1.0.5.11">
        ///     Member Created
        /// </revision>
        public RSACryptoServiceProvider AsymmetricCrypto
        {
            get
            {
                if (this.asymmetricCrypto == null)
                {
                    this.asymmetricCrypto =
                        new RSACryptoServiceProvider(
                            this.shortKeys
                                ?
                                    SequoiaCryptoProvider.AsymmetricShortKeySize
                                :
                                    SequoiaCryptoProvider.AsymmetricKeySize);
                }

                return (RSACryptoServiceProvider)this.asymmetricCrypto;
            }
        }

        #endregion

        #region Internal properties

        /// <summary>
        ///     Gets the symmetric crypto.
        /// </summary>
        /// <value>The symmetric crypto.</value>
        /// <externalUnit cref="SymmetricAlgorithm"/>
        /// <externalUnit cref="RijndaelManaged"/>
        /// <revision revisor="dev01" date="1/26/2009" version="1.0.5.7">
        ///     Member Created
        /// </revision>
        internal RijndaelManaged SymmetricCrypto
        {
            get
            {
                if (this.symmetricCrypto == null)
                {
                    this.symmetricCrypto =
                        new RijndaelManaged { Padding = PaddingMode.PKCS7 };
                    this.symmetricCrypto.GenerateKey();
                    this.symmetricCrypto.GenerateIV();
                }

                return (RijndaelManaged)this.symmetricCrypto;
            }
        }

        /// <summary>
        ///     Gets the size of the transport key.
        /// </summary>
        /// <value>The size of the transport key.</value>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="4/23/2009" version="1.0.11.10">
        ///     Member Created
        /// </revision>
        internal int TransportKeySize
        {
            get
            {
                return this.shortKeys
                           ? SequoiaCryptoProvider.EncryptedShortBlockSize * 2
                           :
                               SequoiaCryptoProvider.EncryptedBlockSize * 2;
            }
        }

        /// <summary>
        ///     Gets a value indicating whether [short keys].
        /// </summary>
        /// <value><c>true</c> if [short keys]; otherwise, <c>false</c>.</value>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/17/2009" version="1.1.3.5">
        ///     Added documentation header
        /// </revision>
        internal bool ShortKeys
        {
            get
            {
                return this.shortKeys;
            }
        }

        #endregion

        #region Private properties

        /// <summary>
        ///     Gets the hasher.
        /// </summary>
        /// <value>The hasher.</value>
        /// <externalUnit cref="SymmetricAlgorithm"/>
        /// <externalUnit cref="SHA512Managed"/>
        /// <revision revisor="dev01" date="1/26/2009 1:23:15 PM" version="1.0.5.7">
        ///     Member Created
        /// </revision>
        private HashAlgorithm Hasher
        {
            get
            {
                if (this.hasher == null)
                {
                    this.hasher = new SHA512Managed();
                }

                return this.hasher;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Creates a sub version of the array
        /// </summary>
        /// <returns>The piece of the array</returns>
        /// <param name="mainArray">First Array to merge</param>
        /// <param name="index">Index to start the subarray </param>
        /// <param name="length">size of the subarrays</param>
        /// <revision revisor="dev01" date="1/28/2009" version="1.0.5.9">
        ///     Method created.
        /// </revision>
        public static byte[] Subarray(
            byte[] mainArray,
            int index,
            int length)
        {
            // REVIEW: add checks on array lengths before copying
            // Array placeholder were both arrays will live
            byte[] subArray =
                new byte[length];
            Buffer.BlockCopy(
                mainArray,
                index,
                subArray,
                0,
                length);
            return subArray;
        }

        /// <summary>
        ///     Creates a sub version of the array
        /// </summary>
        /// <returns>The piece of the array</returns>
        /// <param name="mainArray">First Array to merge</param>
        /// <param name="index">Index to start the subarray </param>
        /// <revision revisor="dev01" date="1/28/2009" version="1.0.5.9">
        ///     Method created.
        /// </revision>
        public static byte[] Subarray(
            byte[] mainArray,
            int index)
        {
            return SequoiaCryptoProvider.Subarray(
                mainArray,
                index,
                mainArray.Length - index);
        }

        /// <summary>
        ///     Appends 2 arrays allocating only one
        /// </summary>
        /// <returns>The Merged byte array</returns>
        /// <param name="firstArray">First Array to merge</param>
        /// <param name="secondArray">Array to append</param>
        /// <revision revisor="dev01" date="1/28/2009" version="1.0.5.9">
        ///     Method created.
        /// </revision>
        public static byte[] AppendArrays(
            byte[] firstArray,
            byte[] secondArray)
        {
            byte[] mergedArray =
                new byte[firstArray.Length + secondArray.Length];
            Buffer.BlockCopy(
                firstArray,
                0,
                mergedArray,
                0,
                firstArray.Length);
            Buffer.BlockCopy(
                secondArray,
                0,
                mergedArray,
                firstArray.Length,
                secondArray.Length);
            return mergedArray;
        }

        #region Encryption

        /// <summary>
        ///     Symmetrically encrypts the content.
        /// </summary>
        /// <param name="plainData">The plain data.</param>
        /// <returns>
        ///     The encrypted content.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="1/26/2009" version="1.0.5.7">
        ///     Member Created
        /// </revision>
        public byte[] SymmetricallyEncryptContent(byte[] plainData)
        {
            //REVIEW: was leaving method without closing  and killing of stream.
            byte[] encryptedContent = null;

            // Memory stream to write the data
            using (var memoryStream = new MemoryStream())
            {
                // Define cryptographic stream (always use Write 
                // mode for encryption).
                var cryptoStream =
                    new CryptoStream(
                        memoryStream,
                        this.SymmetricCrypto.CreateEncryptor(),
                        CryptoStreamMode.Write);

                // Start encrypting.
                cryptoStream.Write(plainData, 0, plainData.Length);

                // Finish encrypting.
                cryptoStream.FlushFinalBlock();

                // Convert our encrypted data from a memory stream 
                // into a byte array.
                encryptedContent = memoryStream.ToArray();
            }

            return encryptedContent;
        }
        
        /// <summary>
        ///     Encrypts the specified plain data.
        /// </summary>
        /// <param name="plainData">The plain data.</param>
        /// <returns>
        ///     The public key.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="1/26/2009" version="1.0.5.7">
        ///     Member Created
        /// </revision>
        public byte[] Encrypt(
          byte[] plainData)
        {
            // TODO: Change this implementation with Key Container
            // Public Key placeholder
            string publicKey = string.Empty;

            return this.Encrypt(plainData, this.GetPublicKey());
        }

        /// <summary>
        ///     Encrypts the specified plain data.
        /// </summary>
        /// <param name="plainData">The plain data.</param>
        /// <param name="remotePublicKeyXML">The remote public key XML.</param>
        /// <returns>
        ///     The encrypted data.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="1/26/2009" version="1.0.5.7">
        ///     Member Created
        /// </revision>
        public byte[] Encrypt(
            byte[] plainData,
            string remotePublicKeyXML)
        {
            // Ecnrypted data placeholder
            byte[] encryptedData = null;

            //Calculate the hash and append it to the data
            byte[] dataPlusHash =
                SequoiaCryptoProvider.AppendArrays(
                    this.CalculateHash(plainData),
                    plainData);

            // Symmetrically Encrypt data and hash with random key and IV
            byte[] encryptedDataPlusHash =
                this.SymmetricallyEncryptContent(dataPlusHash);

            // Create an Encrypted transport Key
            var transKey = new TransportKey(
                this.SymmetricCrypto.Key,
                this.SymmetricCrypto.IV,
                this);

            // Add transport Key to ecnrypted data
            encryptedData =
                SequoiaCryptoProvider.AppendArrays(
                    transKey.GenerateTransportKey(remotePublicKeyXML),
                    encryptedDataPlusHash);

            return encryptedData;
        }

        /// <summary>
        ///     Encrypts the file.
        /// </summary>
        /// <param name="plainFilePath">The plain file path.</param>
        /// <param name="encryptedFilePath">The encrypted file path.</param>
        /// <param name="publicKeyXML">The public key XML.</param>
        /// <param name="overWrite">if set to <c>true</c> [over write].</param>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="1/26/2009" version="1.0.5.7">
        ///     Member Created
        /// </revision>
        public void EncryptFile(
            string plainFilePath,
            string encryptedFilePath,
            string publicKeyXML,
             bool overWrite)
        {
            if(string.IsNullOrEmpty(publicKeyXML))
            {
                publicKeyXML = this.GetPublicKey();
            }

            byte[] plainContent;
            using (var file = new FileStream(plainFilePath, FileMode.Open))
            {
                plainContent = new byte[file.Length];
                file.Read(plainContent, 0, plainContent.Length);
            }

            byte[] encryptedData = this.Encrypt(plainContent, publicKeyXML);
            if (overWrite == true)
            {
                using(var file = new FileStream(
                    encryptedFilePath, FileMode.Create))
                {
                    file.Write(encryptedData, 0, encryptedData.Length);
                    file.Flush();
                }
            }
            else
            {
                using(var file = new FileStream(
                    encryptedFilePath, FileMode.CreateNew))
                {
                    file.Write(encryptedData, 0, encryptedData.Length);
                    file.Flush();
                }
            }
        }

        /// <summary>
        ///     Encrypts the file data.
        /// </summary>
        /// <param name="fileData">The file data.</param>
        /// <param name="encryptedFilePath">The encrypted file path.</param>
        /// <param name="publicKeyXML">The public key XML.</param>
        /// <param name="overWrite">if set to <c>true</c> [over write].</param>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/17/2009" version="1.1.3.5">
        ///     Added documentation header
        /// </revision>
        public void EncryptFileData(
            byte[] fileData,
            string encryptedFilePath,
            string publicKeyXML,
             bool overWrite)
        {
            if (string.IsNullOrEmpty(publicKeyXML))
            {
                publicKeyXML = this.GetPublicKey();
            }

            byte[] encryptedData = this.Encrypt(fileData, publicKeyXML);
            if (overWrite == true)
            {
                using (var file = new FileStream(
                    encryptedFilePath, FileMode.Create))
                {
                    file.Write(encryptedData, 0, encryptedData.Length);
                    file.Flush();
                }
            }
            else
            {
                using (var file = new FileStream(
                    encryptedFilePath, FileMode.CreateNew))
                {
                    file.Write(encryptedData, 0, encryptedData.Length);
                    file.Flush();
                }
            }
        }

        #endregion

        #region Decryption

        /// <summary>
        /// Gets the public key.
        /// </summary>
        /// <returns>
        ///     The public key.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="4/21/2009" version="1.0.11.9">
        ///     Member Created
        /// </revision>
        public string GetPublicKey()
        {
            string publicKey = string.Empty;

            if (this.shortKeys == true)
            {
                publicKey = this.GetPublicShortKey();
            }
            else
            {
                Assembly assembly = Assembly.GetExecutingAssembly();

                using (var stream =
                    assembly.GetManifestResourceStream(
                        "Sequoia.Utilities.Encryption.Keys.PublicKey.xml"))
                {
                    using (var streamReader = new StreamReader(stream))
                    {
                        publicKey = streamReader.ReadToEnd();
                    }
                }
            }

            return publicKey;
        }
        
        /// <summary>
        ///     Decrypts the specified plain data.
        /// </summary>
        /// <param name="plainData">The plain data.</param>
        /// <returns>
        ///     The private key.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="1/26/2009" version="1.0.5.7">
        ///     Member Created
        /// </revision>
        public byte[] Decrypt(
             byte[] plainData)
        {
            // TODO: Change this implementation with Key Container
            // Private Key placeholder
            string privateKey = this.GetPrivateKey();

            return this.Decrypt(plainData, privateKey);
        }

        /// <summary>
        ///     Gets the private key.
        /// </summary>
        /// <returns>
        ///     The private key.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="4/21/2009" version="1.0.11.9">
        ///     Member Created
        /// </revision>
        public string GetPrivateKey()
        {
            string privateKey = string.Empty;

            if (this.shortKeys == true)
            {
                privateKey = this.GetPrivateShortKey();
            }
            else
            {
                // REVIEW: use using for streams and readers when possible
                Assembly assembly = Assembly.GetExecutingAssembly();

                using (var stream =
                    assembly.GetManifestResourceStream(
                        "Sequoia.Utilities.Encryption.Keys.PrivateKey.xml"))
                {
                    using (var streamReader = new StreamReader(stream))
                    {
                        privateKey = streamReader.ReadToEnd();
                    }
                }
            }

            return privateKey;
        }

        /// <summary>
        ///     Decrypts the specified complete encrypted data.
        /// </summary>
        /// <param name="completeEncryptedData">The complete encrypted data.</param>
        /// <param name="localPrivateKeyXML">The local private key XML.</param>
        /// <returns>
        ///     The decrypted data.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="1/26/2009 1:23:15 PM" version="1.0.5.7">
        ///     Member Created
        /// </revision>
        public byte[] Decrypt(
              byte[] completeEncryptedData,
              string localPrivateKeyXML)
        {
            byte[] decryptedData = null;
            byte[] hash = null;
            try
            {
                // Generate the Transport Key
                TransportKey transportKey = new TransportKey(
                    SequoiaCryptoProvider.Subarray(
                        completeEncryptedData,
                        0,
                        this.TransportKeySize), 
                        this);

                // Extract encrypted data (total - transport key)
                byte[] encryptedData =
                    SequoiaCryptoProvider.Subarray(
                        completeEncryptedData,
                        this.TransportKeySize);

                // Decrypt the data and Hash with decrypted symmetric key and IV
                byte[] decryptedDataPlusHash =
                    this.SymmetricallyDecryptContent(
                        encryptedData,
                        transportKey.GetSymmetricKeyFromTransportKey(
                            localPrivateKeyXML),
                        transportKey.GetSymmetricIVFromTransportKey(
                            localPrivateKeyXML));

                 hash =
                    SequoiaCryptoProvider.Subarray(
                        decryptedDataPlusHash,
                        0,
                        this.Hasher.HashSize / BitsInByte);

                // Extarct the final data
                decryptedData =
                    SequoiaCryptoProvider.Subarray(
                        decryptedDataPlusHash,
                        this.Hasher.HashSize / BitsInByte);
            }
            catch (Exception exc)
            {
                throw new CryptographicException("Wrong data to decrypt.", exc);
            }

            if (SequoiaCryptoProvider.CompareHashes(
                   hash,
                     this.CalculateHash(decryptedData)) != true)
            {
                throw new CryptographicException("Hash Mismatch.");
            }

            return decryptedData;
        }

        /// <summary>
        ///     Decrypts the file.
        /// </summary>
        /// <param name="encryptedFilePath">The encrypted file path.</param>
        /// <param name="plainFilePath">The plain file path.</param>
        /// <param name="privateKeyXML">The private key XML.</param>
        /// <param name="overWrite">if set to <c>true</c> [over write].</param>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="1/26/2009" version="1.0.5.7">
        ///     Member Created
        /// </revision>
        public void DecryptFile(
            string encryptedFilePath,
            string plainFilePath,
            string privateKeyXML,
            bool overWrite)
        {
            if(string.IsNullOrEmpty(privateKeyXML))
            {
                privateKeyXML = this.GetPrivateKey();
            }

            // File to decrypt
            var file = new FileStream(encryptedFilePath, FileMode.Open);

            // Binary content of the file
            var encryptedContent = new byte[file.Length];

            // Placeholder for decrypted Data
            byte[] decryptedData = null;
            file.Read(encryptedContent, 0, encryptedContent.Length);
            file.Close();

            // Decrypt the ocntent
            decryptedData = this.Decrypt(encryptedContent, privateKeyXML);

            // We create the file handle to write teh decrypted file, 
            // depending on the overwrite flag
            if (overWrite == true)
            {
                file =
                    new FileStream(plainFilePath, FileMode.Create);
            }
            else
            {
                file =
                    new FileStream(plainFilePath, FileMode.CreateNew);
            }
            
            // Write decrypted data to file
            file.Write(decryptedData, 0, decryptedData.Length);
            file.Flush();
            file.Close();
            file.Dispose();
        }

        /// <summary>
        ///     Decrypts the file to bytes.
        /// </summary>
        /// <param name="encryptedFilePath">The encrypted file path.</param>
        /// <param name="privateKeyXML">The private key XML.</param>
        /// <returns>
        ///     The decrypted data.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/17/2009" version="1.1.3.5">
        ///     Added documentation header
        /// </revision>
        public byte[] DecryptFileToBytes(
            string encryptedFilePath,
            string privateKeyXML)
        {
            if (string.IsNullOrEmpty(privateKeyXML))
            {
                privateKeyXML = this.GetPrivateKey();
            }

            // Placeholder for decrypted Data
            byte[] decryptedData = null;

            // File to decrypt
            using(var file = new FileStream(encryptedFilePath, FileMode.Open))
            {
                // Binary content of the file
                var encryptedContent = new byte[file.Length];
                
                file.Read(encryptedContent, 0, encryptedContent.Length);
                
                // Decrypt the ocntent
                decryptedData = this.Decrypt(encryptedContent, privateKeyXML);
            }

            return decryptedData;
        }

        #endregion

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, 
        ///     releasing, or resetting unmanaged resources.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="1/26/2009" version="1.0.5.7">
        ///     Member Created
        /// </revision>
        public void Dispose()
        {
            if (this.symmetricCrypto != null)
            {
                this.symmetricCrypto.Clear();
                this.symmetricCrypto = null;
            }

            if (this.hasher != null)
            {
                this.hasher.Clear();
                this.hasher = null;
            }
        }

        #endregion

        #region Public Events

        /// <summary>
        ///     Creates a digital signature from file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>
        ///     <c>true</c> if signature created; otherwise, <c>false</c>.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev03" date="2/19/2009" version="1.0.8.6">
        ///     Method created.
        /// </revision>
        /// <revision revisor="dev13" date="3/23/2009" version="1.0.9.0701">
        ///     Formatting changes
        /// </revision>
        public bool CreateSignature(string fileName)
        {
            return this.CreateSignature(fileName, this.GetPrivateKey());
        }

        /// <summary>
        ///     Creates a digital signature from file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>
        ///     <c>true</c> if short signature created; otherwise, <c>false</c>.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev03" date="02/19/09" version="1.0.8.6">
        ///     Method created.
        /// </revision>
        /// <revision revisor="dev13" date="3/23/2009" version="1.0.9.0701">
        ///     Formatting changes
        /// </revision>
        public bool CreateShortSignature(string fileName)
        {
            return this.CreateSignature(fileName, this.GetPrivateShortKey());
        }

        /// <summary>
        ///     Creates a digital signature from file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="privateKey">The private key.</param>
        /// <returns>
        ///     <c>true</c> if signature created; otherwise, <c>false</c>.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev03" date="02/19/09" version="1.0.8.6">
        ///     Method created.
        /// </revision>
        /// <revision revisor="dev13" date="3/23/2009" version="1.0.9.0701">
        ///     Formatting changes
        /// </revision>
        /// <revision revisor="dev06" date="4/22/2009" version="1.0.11.10">
        ///     Updated to return a bool for success
        /// </revision>
        public bool CreateSignature(string fileName, string privateKey)
        {
            bool signatureCreated = false;

            try
            {
                // local variable declarations
                byte[] fileHash;

                this.AsymmetricCrypto.FromXmlString(privateKey);

                // create file hash 
                using (var file =
                    new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    //SHA1 sha1 = new SHA1CryptoServiceProvider();
                    SHA1 sha1 = new SHA1CryptoServiceProvider();
                    fileHash = sha1.ComputeHash(file);
                }

                // create digital signature
                var rsaFormatter =
                    new RSAPKCS1SignatureFormatter(this.AsymmetricCrypto);
                rsaFormatter.SetHashAlgorithm("SHA1");
                byte[] signature = rsaFormatter.CreateSignature(fileHash);

                string sigFileName = fileName + ".sig";

                // write out signature file
                using (var sigFile =
                    new FileStream(
                       sigFileName,
                        FileMode.Create,
                        FileAccess.Write))
                {
                    using (var binaryWriter = new BinaryWriter(sigFile))
                    {
                        binaryWriter.Write(signature);
                    }
                }

                signatureCreated = File.Exists(sigFileName);
            }
            catch (Exception)
            {
                // just set as failed
                signatureCreated = false;
            }

            return signatureCreated;
        }

        /// <summary>
        ///     Verify a digital signature from file.
        /// </summary>
        /// <param name="sigFileName">Name of the sig file.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>
        ///     <c>true</c> on verified signature; otherwise, <c>false</c>.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev03" date="02/19/09" version="1.0.8.6">
        ///     Method created.
        /// </revision>
        public bool VerifySignature(string sigFileName, string fileName)
        {
            return this.VerifySignature(
                sigFileName, fileName, this.GetPublicKey());
        }

        /// <summary>
        ///     Verify a digital signature from file.
        /// </summary>
        /// <param name="sigFileName">Name of the sig file.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>
        ///     <c>true</c> on verified signature; otherwise, <c>false</c>.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev03" date="02/19/09" version="1.0.8.6">
        ///     Method created.
        /// </revision>
        public bool VerifyShortSignature(string sigFileName, string fileName)
        {
            return this.VerifySignature(
                sigFileName, fileName, this.GetPublicShortKey());
        }

        /// <summary>
        ///     Verify a digital signature from file.
        /// </summary>
        /// <param name="sigFileName">Name of the sig file.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="publicKey">The public key.</param>
        /// <returns>
        ///     <c>true</c> on verified signature; otherwise, <c>false</c>.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev03" date="02/19/09" version="1.0.8.6">
        ///     Method created.
        /// </revision>
        public bool VerifySignature(
            string sigFileName, 
            string fileName, 
            string publicKey)
        {
            // local variable declarations
            bool verified;
            byte[] fileHash;
            byte[] signature;

            this.AsymmetricCrypto.FromXmlString(publicKey);

            // validate signature and file 
            if (File.Exists(sigFileName) == false ||
                File.Exists(fileName) == false)
            {
                return false;
            }
 
            // create file hash 
            using (FileStream file = new FileStream(
                fileName,
                FileMode.Open, 
                FileAccess.Read))
            {
                SHA1 sha1 = new SHA1CryptoServiceProvider();
                fileHash = sha1.ComputeHash(file);
            }

            // read in digital signature
            using (FileStream file = new FileStream(
                sigFileName,
                FileMode.Open, 
                FileAccess.Read))
            {
                using (BinaryReader reader = new BinaryReader(file))
                {
                    FileInfo f = new FileInfo(sigFileName);
                    signature = reader.ReadBytes((int)f.Length); 
                }
            }

            // verify digital signature
            var rsaDeformatter =
                new RSAPKCS1SignatureDeformatter(this.AsymmetricCrypto);
            rsaDeformatter.SetHashAlgorithm("SHA1");
            verified = false;
            if (rsaDeformatter.VerifySignature(fileHash, signature))
            {
                verified = true;
            }

            return verified;
        }

        #endregion

        #region Private Methods

        /// <summary>
        ///     Compares the hashes.
        /// </summary>
        /// <param name="firstHash">The first hash.</param>
        /// <param name="secondHash">The second hash.</param>
        /// <returns>
        ///     <c>true</c> if hashes were equal; otherwise, <c>false</c>.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="1/26/2009" version="1.0.5.7">
        ///     Member Created
        /// </revision>
        private static bool CompareHashes(byte[] firstHash, byte[] secondHash)
        {
            // Return Value
            bool returnValue = false;

            if (firstHash != null && secondHash != null)
            {
                returnValue = true;

                for (int i = 0; i <= secondHash.Length - 1; i++)
                {
                    if (firstHash[i] != secondHash[i])
                    {
                        returnValue = false;

                        // hashes not equal so exit compare
                        break;
                    }
                }
            }

            return returnValue;
        }
        
        /// <summary>
        ///     Gets the private key.
        /// </summary>
        /// <returns>
        ///     The private short key.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="4/21/2009" version="1.0.11.9">
        ///     Member Created
        /// </revision>
        private string GetPrivateShortKey()
        {
            string privateKey = string.Empty;

            Assembly assembly = Assembly.GetExecutingAssembly();

            using (var stream =
                 assembly.GetManifestResourceStream(
                 "Sequoia.Utilities.Encryption.Keys.PrivateShortKey.xml"))
            {
                using (var streamReader = new StreamReader(stream))
                {
                    privateKey = streamReader.ReadToEnd();
                }
            }

            return privateKey;
        }

        /// <summary>
        ///     Gets the public key.
        /// </summary>
        /// <returns>
        ///     The public short key.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="4/21/2009" version="1.0.11.9">
        ///     Member Created
        /// </revision>
        private string GetPublicShortKey()
        {
            string publicKey = string.Empty;

            Assembly assembly = Assembly.GetExecutingAssembly();

            using (var stream =
                 assembly.GetManifestResourceStream(
                 "Sequoia.Utilities.Encryption.Keys.PublicShortKey.xml"))
            {
                using (var streamReader = new StreamReader(stream))
                {
                    publicKey = streamReader.ReadToEnd();
                }
            }

            return publicKey;
        }
        
        /// <summary>
        ///     Calculates the hash.
        /// </summary>
        /// <param name="dataToHash">The data to hash.</param>
        /// <returns>
        ///     The hash value.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="1/26/2009 1:23:15 PM" version="1.0.5.7">
        ///     Member Created
        /// </revision>
        private byte[] CalculateHash(byte[] dataToHash)
        {
            return this.Hasher.ComputeHash(dataToHash);
        }

        /// <summary>
        ///     Symmetrically decrypt the content.
        /// </summary>
        /// <param name="encryptedData">The encrypted data.</param>
        /// <param name="symmetricKey">The symmetric key.</param>
        /// <param name="symmetricIV">The symmetric IV.</param>
        /// <returns>
        ///     The decrypted content.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="1/26/2009 1:23:15 PM" version="1.0.5.7">
        ///     Member Created
        /// </revision>
        private byte[] SymmetricallyDecryptContent(
            byte[] encryptedData,
            byte[] symmetricKey,
                byte[] symmetricIV)
        {
            // Memory Stream to read the encrypted data
            byte[] decryptedBytes = null;
            int decryptRealSize = 0;

            // Cryptostream to decrypt teh memory stream
            using (var memoryStream = new MemoryStream(encryptedData))
            {
                using (var cryptoStream =
                    new CryptoStream(
                        memoryStream,
                        this.SymmetricCrypto.CreateDecryptor(
                            symmetricKey,
                            symmetricIV),
                        CryptoStreamMode.Read))
                {
                    // Placeholder for decrypted data (size it with the 
                    // encrypted data length (maybe larger but then 
                    // will crop it)
                    decryptedBytes = new byte[encryptedData.Length];

                    // Start decrypting.
                    decryptRealSize =
                        cryptoStream.Read(
                            decryptedBytes, 0, encryptedData.Length);
                }
            }
           
            // REVIEW: null check on decrypted bytes before proceeding.
            
            // Need subarray, because placeholder array maybe larger than the
            // actual data
            return SequoiaCryptoProvider.Subarray(
                decryptedBytes,
                0,
                decryptRealSize);
        }

        #endregion
    }
}
