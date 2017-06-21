// -----------------------------------------------------------------------------
// <copyright file="TransportKey.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the TransportKey class.
// </summary>
// <revision revisor="dev01" date="1/30/2009" version="1.0.5.11">
//     File Created
// </revision>
// <revision revisor="dev13" date="11/17/2009" version="1.1.3.5">
//     File modified
// </revision> 
// -----------------------------------------------------------------------------

namespace Sequoia.Utilities.Encryption
{
    #region Using directives

    using System;
    using System.Security.Cryptography;

    #endregion

    /// <summary>
    ///     Class that abstracts the transport Key
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev01" date="1/30/2009" version="1.0.5.11">
    ///     Member Created
    /// </revision>
    public class TransportKey
    {
        #region Fields

        /// <summary>
        ///     param for short keys flag
        /// </summary>
        private bool shortKeys = true;

        /// <summary>
        ///     Asymmetric crypto
        /// </summary>
        private AsymmetricAlgorithm asymmetricCrypto = null;

        /// <summary>
        ///     the encrypted Key
        /// </summary>
        private byte[] encryptedData;

        /// <summary>
        ///     Encryption Key
        /// </summary>
        private byte[] key;

        /// <summary>
        ///     Initialization Vector
        /// </summary>
        private byte[] initializationVector;

        /// <summary>
        ///     Is the Key Ecnrypted or not?
        /// </summary>
        private bool encrypted = false;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="TransportKey"/> class.
        /// </summary>
        /// <param name="encryptionKey">The encryption key.</param>
        /// <param name="initializationVector">
        ///     The initialization vector.
        /// </param>
        /// <param name="crypto">the crypto provider.</param>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="1/30/2009" version="1.0.5.11">
        ///     Member Created
        /// </revision>
        public TransportKey(
            byte[] encryptionKey,
            byte[] initializationVector,
            SequoiaCryptoProvider crypto)
        {
            this.asymmetricCrypto = crypto.AsymmetricCrypto;
            this.key = encryptionKey;
            this.initializationVector = initializationVector;
            this.encrypted = false;
            this.shortKeys = crypto.ShortKeys;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="TransportKey"/> class.
        /// </summary>
        /// <param name="crypto">The crypto.</param>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="2/5/2009" version="1.0.6.1">
        ///     Member Created
        /// </revision>
        public TransportKey(
            SequoiaCryptoProvider crypto)
        {
            this.key = crypto.SymmetricCrypto.Key;
            this.asymmetricCrypto = crypto.AsymmetricCrypto;
            this.initializationVector = crypto.SymmetricCrypto.IV;
            this.encrypted = false;
            this.shortKeys = crypto.ShortKeys;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="TransportKey"/> class.
        /// </summary>
        /// <param name="encryptedKey">The encrypted key.</param>
        /// <param name="crypto">The crypto provider.</param>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="1/30/2009" version="1.0.5.11">
        ///     Member Created
        /// </revision>
        public TransportKey(
            byte[] encryptedKey, SequoiaCryptoProvider crypto)
        {
                    this.encryptedData = encryptedKey;

                    this.encrypted = true;
                    this.asymmetricCrypto = crypto.AsymmetricCrypto;
                    this.shortKeys = crypto.ShortKeys;
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
        private RSACryptoServiceProvider AsymmetricCrypto
        {
            get
            {
                return (RSACryptoServiceProvider)this.asymmetricCrypto;
            }
        }
    
        #endregion

        #region Public Methods

        /// <summary>
        ///     Generates the transport key.
        /// </summary>
        /// <param name="remotePublicKeyXML">The remote public key XML.</param>
        /// <returns>
        ///     The transport key.
        /// </returns>
        /// <externalUnit cref="AsymmetricAlgorithm"/>
        /// <revision revisor="dev01" date="1/30/2009" version="1.0.5.11">
        ///     Member Created
        /// </revision>
        public byte[] GenerateTransportKey(string remotePublicKeyXML)
        {
            if (encrypted == false)
            {
                this.encryptedData =
                    SequoiaCryptoProvider.AppendArrays(
                        this.AsymmetricallyEncryptContent(
                            this.key, remotePublicKeyXML),
                        this.AsymmetricallyEncryptContent(
                            this.initializationVector, remotePublicKeyXML));
                encrypted = true;
            }

            return this.encryptedData;
        }

        /// <summary>
        ///     Decrypts the transport key.
        /// </summary>
        /// <param name="privateKeyXML">The private key XML.</param>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="1/30/2009" version="1.0.5.11">
        ///     Member Created
        /// </revision>
        public void DecryptTransportKey(string privateKeyXML)
        {
            if (this.encrypted == true)
            {
                this.key = this.AsymmetricallyDecryptContent(
                    SequoiaCryptoProvider.Subarray(
                        this.encryptedData,
                        0,
                        this.AsymmetricCrypto.KeySize / 8),
                    privateKeyXML);
                this.initializationVector =
                    this.AsymmetricallyDecryptContent(
                        SequoiaCryptoProvider.Subarray(
                            this.encryptedData,
                            this.AsymmetricCrypto.KeySize / 8),
                        privateKeyXML);
                this.encrypted = false;
            }
        }

        #endregion

        #region Public Events

        #endregion

        #region Internal methods

        /// <summary>
        ///     Gets the symmetric key from transport key.
        /// </summary>
        /// <param name="privateKeyXML">The private key XML.</param>
        /// <returns>
        ///     The symmetric key.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="1/30/2009" version="1.0.5.11">
        ///     Member Created
        /// </revision>
        internal byte[] GetSymmetricKeyFromTransportKey(
              string privateKeyXML)
        {
            this.DecryptTransportKey(privateKeyXML);
            return this.key;
        }

        /// <summary>
        ///     Gets the symmetric IV from transport key.
        /// </summary>
        /// <param name="privateKeyXML">The private key XML.</param>
        /// <returns>
        ///     The symmetric initialization vector.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="1/30/2009" version="1.0.5.11">
        ///     Member Created
        /// </revision>
        internal byte[] GetSymmetricIVFromTransportKey(
            string privateKeyXML)
        {
            this.DecryptTransportKey(privateKeyXML);
            return this.initializationVector;
        }

        #endregion

        #region Private Methods

        /// <summary>
        ///     Asymmetrically encrypt the content
        /// </summary>
        /// <param name="plainData">The plain data.</param>
        /// <param name="privateKeyXML">The private key XML.</param>
        /// <returns>
        ///     The encrypted content.
        /// </returns>
        /// <externalUnit cref="AsymmetricAlgorithm"/>
        /// <revision revisor="dev01" date="1/30/2009" version="1.0.5.11">
        ///     Member Created
        /// </revision>
        private byte[] AsymmetricallyEncryptContent(
            byte[] plainData,
            string privateKeyXML)
        {
            this.AsymmetricCrypto.FromXmlString(privateKeyXML);
            return
                this.AsymmetricCrypto.Encrypt(
                    plainData,
                    false);
        }

        /// <summary>
        ///     Asymmetrically decrypt the content
        /// </summary>
        /// <param name="encrypData">The encryp data.</param>
        /// <param name="privateKeyXML">The private key XML.</param>
        /// <returns>
        ///     The decrypted content.
        /// </returns>
        /// <externalUnit cref="AsymmetricAlgorithm"/>
        /// <revision revisor="dev01" date="1/30/2009" version="1.0.5.11">
        ///     Member Created
        /// </revision>
        private byte[] AsymmetricallyDecryptContent(
                 byte[] encrypData,
                 string privateKeyXML)
        {
            this.AsymmetricCrypto.FromXmlString(privateKeyXML);
            return
                this.AsymmetricCrypto.Decrypt(
                    encrypData,
                    false);
        }
        #endregion
      
        #region Public Events

        #endregion
    }
}
