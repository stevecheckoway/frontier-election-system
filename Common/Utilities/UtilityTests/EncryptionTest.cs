// -----------------------------------------------------------------------------
// <copyright file="EncryptionTest.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the EncryptionTest test class.
// </summary>
// <revision revisor="dev01" date="1/26/2009" version="1.0.0.0">
//     File Created
// </revision>
// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
//     File modified
// </revision>
// -----------------------------------------------------------------------------

namespace UtilityTests
{
    #region Using directives

    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    using NUnit.Framework;

    using Sequoia.Utilities.Encryption;

    #endregion

    /// <summary>
    ///	    EncryptionTest is a test fixture for running 
    ///     tests against Encryption
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev01" date="1/26/2009" version="1.0.0.0">
    ///     Class created.
    /// </revision>
    /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
    ///     Formatting changes
    /// </revision>
    [TestFixture]
    public class EncryptionTest
    {
        #region Fixture Setup
        /// <summary>
        ///     This method runs once for the entire test fixture. Place any 
        ///     logic that needs to happen before this test fixture is run in 
        ///     this method.
        /// </summary>
        [TestFixtureSetUp]
        public void FixtureSetup()
        {
        }

        #endregion

        #region Fixture Teardown
        /// <summary>
        ///     This method runs once for the entire test fixture. Place any 
        ///     logic that needs to happen when this test fixture is unloaded.
        /// </summary>
        [TestFixtureTearDown]
        public void FixtureTeardown()
        {
        }
        #endregion

        #region Test Setup
        /// <summary>
        ///     This method runs once for every test in the entire test fixture. 
        ///     Place any logic that needs to happen before every test is loaded 
        ///     in this method.
        /// </summary>
        [SetUp]
        public void TestSetup()
        {
            this.CleanUp();
        }
        #endregion

        #region Test Teardown
        /// <summary>
        ///     This method runs once for every test in the entire test fixture. 
        ///     Place any logic that needs to happen when every test is unloaded 
        ///     in this method.
        /// </summary>
        [TearDown]
        public void TestTeardown()
        {
            this.CleanUp();
        }
        #endregion

        #region Tests
        /// <summary>
        ///     Encrypts the decrypt file.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        [Test]
        public void EncryptDecryptFile()
        {
            string testPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                @"..\..\TestData\Encryption");
            string publicKey =
                File.ReadAllText(Path.Combine(testPath, "PublicKey.xml"));
            string privateKey =
                File.ReadAllText(Path.Combine(testPath, "PrivateKey.xml"));

            SequoiaCryptoProvider pro = new SequoiaCryptoProvider(false);

            string originalFile = Path.Combine(
                testPath,
                "Results.xml");

            string encryptedFile = Path.Combine(
                testPath,
                "Results.xml.enc");

            string decryptedFile = Path.Combine(
                testPath,
                "DecryptedResults.xml");

            pro.EncryptFile(
                originalFile,
                encryptedFile,
                publicKey,
                false);

            Assert.IsTrue(File.Exists(encryptedFile));

            pro = new SequoiaCryptoProvider(false);


            pro.DecryptFile(
                encryptedFile,
                decryptedFile,
                privateKey,
                true);

            Assert.IsTrue(File.Exists(decryptedFile));
            Assert.IsTrue(this.CompareFiles(decryptedFile, originalFile));
        }

        /// <summary>
        ///     Tests for the IO exception with no override option.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        [Test]
        [ExpectedException("System.IO.IOException")]
        public void TestforIOExceptionWithNoOverrideOption()
        {
            string testPath = Path.Combine(
               AppDomain.CurrentDomain.BaseDirectory,
               @"..\..\TestData\Encryption");
            string publicKey =
                File.ReadAllText(Path.Combine(testPath, "PublicKey.xml"));
            string privateKey =
                File.ReadAllText(Path.Combine(testPath, "PrivateKey.xml"));

            SequoiaCryptoProvider pro = new SequoiaCryptoProvider(false);

            string originalFile = Path.Combine(
                testPath,
                "Results.xml");

            string encryptedFile = Path.Combine(
                testPath,
                "Results.xml.enc");

            string decryptedFile = Path.Combine(
                testPath,
                "DecryptedResults.xml");

            pro.EncryptFile(
                originalFile,
                encryptedFile,
                publicKey,
                false);

            Assert.IsTrue(File.Exists(encryptedFile));

            pro = new SequoiaCryptoProvider(false);


            pro.DecryptFile(
                encryptedFile,
                decryptedFile,
                privateKey,
                true);

                pro.DecryptFile(
                encryptedFile,
                decryptedFile,
                privateKey,
                false);
        }

        /// <summary>
        ///     Test with the wrong hash.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        [Test]
        [ExpectedException(
            "System.Security.Cryptography.CryptographicException",
            ExpectedMessage = "Hash Mismatch.")]
        public void WrongHashTest()
        {
            byte[] encryptedData = null;
            string testString = "普選正式選票2008年11月4日，星期二伊利諾州芝加哥市";
            string testPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                @"..\..\TestData\Encryption");
            string publicKey =
                File.ReadAllText(Path.Combine(testPath, "PublicKey.xml"));
            var plainData =
                Encoding.Unicode.GetBytes(testString);
            var crypto = new SequoiaCryptoProvider();

            var hasher = new SHA512Managed();

            var hash = hasher.ComputeHash(plainData);

            // Change one byte of the hash
            hash[0] = (byte)0;

            //Calculate the hash and append it to the data
            byte[] dataPlusHash =
                SequoiaCryptoProvider.AppendArrays(
                    hash,
                    plainData);

            // Symmetrically Encrypt data and hash with random key and IV
            byte[] encryptedDataPlusHash =
                crypto.SymmetricallyEncryptContent(dataPlusHash);

            // Create an Encrypted transport Key
            var transKey = new TransportKey(crypto);

            // Add transport Key to ecnrypted data
            encryptedData =
                SequoiaCryptoProvider.AppendArrays(
                    transKey.GenerateTransportKey(publicKey),
                    encryptedDataPlusHash);


            EncryptedItem item = new EncryptedItem();
            item.Data = encryptedData;

            var result = EncryptionHelper.Decrypt(item,false);
        }

        /// <summary>
        ///     Test with the wrong data hash.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        [Test]
        [ExpectedException(
            "System.Security.Cryptography.CryptographicException",
            ExpectedMessage = "Hash Mismatch.")]
        public void WrongDataHashTest()
        {
            byte[] encryptedData = null;
            string testString = "普選正式選票2008年11月4日，星期二伊利諾州芝加哥市";
            string testPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                @"..\..\TestData\Encryption");
            string publicKey =
                File.ReadAllText(Path.Combine(testPath, "PublicKey.xml"));
            var plainData =
                Encoding.Unicode.GetBytes(testString);
            var crypto = new SequoiaCryptoProvider();

            var hasher = new SHA512Managed();

            var hash = hasher.ComputeHash(plainData);

            // Change one byte of the hash
            plainData[0] = (byte)0;

            //Calculate the hash and append it to the data
            byte[] dataPlusHash =
                SequoiaCryptoProvider.AppendArrays(
                    hash,
                    plainData);

            // Symmetrically Encrypt data and hash with random key and IV
            byte[] encryptedDataPlusHash =
                crypto.SymmetricallyEncryptContent(dataPlusHash);

            // Create an Encrypted transport Key
            var transKey = new TransportKey(crypto);

            // Add transport Key to ecnrypted data
            encryptedData =
                SequoiaCryptoProvider.AppendArrays(
                    transKey.GenerateTransportKey(publicKey),
                    encryptedDataPlusHash);


            EncryptedItem item = new EncryptedItem();
            item.Data = encryptedData;

            var result = EncryptionHelper.Decrypt(item,false);
        }

        /// <summary>
        ///     Tests the helper encrypte decrypt serialize.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        [Test]
        public void TestHelperEncrypteDecryptSerialize()
        {
            #region longstring for testing
            string testString =
                @"
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ  
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ  
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ  
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ  
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ  
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ
                    ABCDEFGHIJKLMNOPQRSTUVWXYZ      
                    ";
            #endregion

            int size = 0;

            EncryptedItem item = EncryptionHelper.Encrypt(
                Encoding.Unicode.GetBytes(testString));
            size = item.EncryptedSize;

            string testPath = Path.Combine(
             AppDomain.CurrentDomain.BaseDirectory,
             @"..\..\TestData\Encryption\TestingSerialize.enc");

            File.WriteAllBytes(
                testPath, Encoding.Unicode.GetBytes(item.ToString()));

            byte[] encryptedFile = File.ReadAllBytes(testPath);

            EncryptedItem tmpItem =
                EncryptedItem.FromXml(
                    Encoding.Unicode.GetString(encryptedFile));

            Assert.AreEqual(size, tmpItem.EncryptedSize);
            string result =
                Encoding.Unicode.GetString(EncryptionHelper.Decrypt(item));

            Assert.AreNotEqual(
                testString, Encoding.Unicode.GetString(item.Data));
            Assert.AreEqual(testString, result);
            File.Delete(testPath);

        }

        /// <summary>
        ///     Test decrypting the wrong data.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        [Test]
        [ExpectedException(
            "System.Security.Cryptography.CryptographicException",
            ExpectedMessage = "Wrong data to decrypt.")]
        public void WrongDataDecrypt()
        {
            string result = string.Empty;
            string testString = "普選正式選票2008年11月4日，星期二伊利諾州芝加哥市";
            EncryptedItem item = EncryptionHelper.Encrypt(
                Encoding.Unicode.GetBytes(testString));
            byte[] data = new byte[item.Data.Length - 1];
            Array.Copy(
                (Array) (item.Data), 1, (Array) data, 0, item.Data.Length - 1);
            item.Data = data;
            Encoding.Unicode.GetString(EncryptionHelper.Decrypt(item));
        }


        /// <summary>
        ///     Tests the helper unicode.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        [Test]
        public void TestHelperUnicode()
        {
            string testString = "普選正式選票2008年11月4日，星期二伊利諾州芝加哥市";
            EncryptedItem item = EncryptionHelper.Encrypt(
               Encoding.Unicode.GetBytes(testString));
            string result =
             Encoding.Unicode.GetString(EncryptionHelper.Decrypt(item));

            Assert.AreNotEqual(
                testString, Encoding.Unicode.GetString(item.Data));
            Assert.AreEqual(testString, result);

        }

        #endregion

        #region Private Methods

        /// <summary>
        ///     Cleans up.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        private void CleanUp()
        {
            string[] files =
                new string[]
                    {
                        "Results.xml.enc",
                        "DecryptedResults.xml",
                        "TestingSerialize.enc"
                    };

            string testPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                @"..\..\TestData\Encryption");
            foreach (string file in files)

            {
                string filePath = Path.Combine(testPath, file);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
        }

        /// <summary>
        ///     Compares the files.
        /// </summary>
        /// <param name="File1">The file1.</param>
        /// <param name="File2">The file2.</param>
        /// <returns></returns>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        private bool CompareFiles(string File1, string File2)
        {
            FileInfo FI1 = new FileInfo(File1);
            FileInfo FI2 = new FileInfo(File2);

            if (FI1.Length != FI2.Length)
                return false;

            byte[] bytesFile1 = File.ReadAllBytes(File1);
            byte[] bytesFile2 = File.ReadAllBytes(File2);

            if (bytesFile1.Length != bytesFile2.Length)
                return false;

            for (int i = 0; i <= bytesFile2.Length - 1; i++)
            {
                if (bytesFile1[i] != bytesFile2[i])
                    return false;
            }
            return true;
        }

        #endregion
    }
}
