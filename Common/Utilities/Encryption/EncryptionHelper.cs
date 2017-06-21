// -----------------------------------------------------------------------------
// <copyright file="EncryptionHelper.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the EncryptionHelper class.
// </summary>
// <revision revisor="dev01" date="1/28/2009 10:54:43 PM" version="1.0.5.9">
//     File Created
// </revision>
// <revision revisor="dev13" date="11/17/2009" version="1.1.3.5">
//     File modified
// </revision>
// -----------------------------------------------------------------------------

namespace Sequoia.Utilities.Encryption
{
    /// <summary>
    ///     EncryptionHelper is a utility which Encryptes and deEncryptes data.
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev01" date="1/28/2009" version="1.0.5.9">
    ///     Class created.
    /// </revision>
    /// <revision revisor="dev13" date="11/17/2009" version="1.1.3.5">
    ///     Formatting changes.
    /// </revision>
    public class EncryptionHelper
    {
        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        ///     Encrypts the specified string to Encrypt.
        /// </summary>
        /// <param name="bytesToEncrypt">The string to Encrypt.</param>
        /// <param name="shortKey">if set to <c>true</c> [short key].</param>
        /// <returns>
        ///     A <see cref="EncryptedItem"/> containing details about
        ///     the Encrypted string.
        /// </returns>
        /// <externalUnit cref="EncryptedItem"/>
        /// <externalUnit cref="SequoiaCryptoProvider"/>
        /// <revision revisor="dev01" date="1/28/2009" version="1.0.5.9">
        ///     Member Created
        /// </revision>
        public static EncryptedItem Encrypt(
            byte[] bytesToEncrypt,
            bool shortKey)
        {
            // create parm for return
            var encryptedItem = new EncryptedItem();

            using (var crypto = new SequoiaCryptoProvider(shortKey))
            {
                encryptedItem.Data = crypto.Encrypt(
                    bytesToEncrypt);
            }

            return encryptedItem;
        }

        /// <summary>
        ///     Encrypts the specified string to Encrypt.
        /// </summary>
        /// <param name="bytesToEncrypt">The string to Encrypt.</param>
        /// <returns>
        ///     A <see cref="EncryptedItem"/> containing details about 
        ///     the Encrypted string.
        /// </returns>
        /// <externalUnit cref="EncryptedItem"/>
        /// <externalUnit cref="SequoiaCryptoProvider"/>
        /// <revision revisor="dev01" date="1/28/2009" version="1.0.5.9">
        ///     Member Created
        /// </revision>
        public static EncryptedItem Encrypt(byte[] bytesToEncrypt)
        {
            return EncryptionHelper.Encrypt(bytesToEncrypt, true);
        }
        #endregion

        /// <summary>
        ///     Gets the public key.
        /// </summary>
        /// <param name="shortKey">if set to <c>true</c> [short key].</param>
        /// <returns>A string containing the public key</returns>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="4/23/2009" version="1.0.11.10">
        ///     Member Created
        /// </revision>
        public static string GetPublicKey(bool shortKey)
        {
            string publicKey = string.Empty;

            using (var crypto = new SequoiaCryptoProvider(shortKey))
            {
                publicKey = crypto.GetPrivateKey();
            }

            return publicKey;
        }

        /// <summary>
        ///     Gets the private key.
        /// </summary>
        /// <param name="shortKey">if set to <c>true</c> [short key].</param>
        /// <returns>A string containing the private key.</returns>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="4/23/2009" version="1.0.11.10">
        ///     Member Created
        /// </revision>
        public static string GetPrivateKey(bool shortKey)
        {
            string privateKey = string.Empty;

            using (var crypto = new SequoiaCryptoProvider(shortKey))
            {
                privateKey = crypto.GetPublicKey();
            }

            return privateKey;
        }

        #region Decrypt

        /// <summary>
        ///     DeEncryptes the specified Encrypted item.
        /// </summary>
        /// <param name="encryptedItem">The Encrypted item.</param>
        /// <param name="shortKey">if set to <c>true</c> [short key].</param>
        /// <returns>
        ///     The unencrypted value of the Encrypted data.
        /// </returns>
        /// <externalUnit cref="EncryptedItem"/>
        /// <externalUnit cref="SequoiaCryptoProvider"/>
        /// <revision revisor="dev01" date="1/28/2009" version="1.0.5.9">
        ///     Member Created
        /// </revision>
        public static byte[] Decrypt(
            EncryptedItem encryptedItem,
            bool shortKey)
        {
            // Review: should this be in Try/catch block? 

            // Create return param
            byte[] results = null;

            using (var crypto = new SequoiaCryptoProvider(shortKey))
            {
                results = crypto.Decrypt(encryptedItem.Data);
            }

            return results;
        }

        /// <summary>
        ///     DeEncryptes the specified Encrypted item.
        /// </summary>
        /// <param name="encryptedItem">The Encrypted item.</param>
        /// <returns>The unEncrypted value of the Encrypted data.</returns>
        /// <externalUnit cref="EncryptedItem"/>
        /// <externalUnit cref="SequoiaCryptoProvider"/>
        /// <revision revisor="dev01" date="1/28/2009" version="1.0.5.9">
        ///     Member Created
        /// </revision>
        public static byte[] Decrypt(EncryptedItem encryptedItem)
        {
            return EncryptionHelper.Decrypt(encryptedItem, true);
        }

        #endregion

        #region Public Events

        #endregion

        #region Private Methods

        #endregion
    }
}
