// -----------------------------------------------------------------------------
// <copyright file="SignedArchiveTests.cs" company="Sequoia Voting Systems">
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the SignedArchiveTests test class.
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
// <revision revisor="dev05" date="09/09/09" version="1.0.17.2">
//     File modified.
// </revision>
// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
//     File modified.
// </revision>
// -----------------------------------------------------------------------------

namespace UtilityTests
{
    #region Using directives

    using System;
    using System.IO;
    using System.Security.Cryptography;

    using NUnit.Framework;

    using Sequoia.DomainObjects;
    using Sequoia.Utilities;
    using Sequoia.Utilities.Compression;
    using Sequoia.Utilities.Encryption;
    using Sequoia.Utilities.IO;

    #endregion

    /// <summary>
    ///	    SignedArchiveTests is a test fixture for running tests against the
    ///	    SignedArchive class.
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor = "dev05" date="03/06/09" version="1.0.8.17">
    ///     Class created.
    /// </revision>
    /// <revision revisor = "dev05" date="03/16/09" version="1.0.8.27">
    ///     Pass a timestamp to the output archive.
    /// </revision>
    /// <revision revisor = "dev05" date="03/16/09" version="1.0.8.27">
    ///     Added ManifestTest().
    /// </revision>
    /// <revision revisor = "dev05" date="03/19/09" version="1.0.9.3">
    ///     A couple of methods now return OperationResult instead of bool.
    /// </revision>
    /// <revision revisor = "dev05" date="03/19/09" version="1.0.9.3">
    ///     Formatting changes
    /// </revision>
    [TestFixture]
    public class SignedArchiveTests
    {
        #region Fields

        /// <summary>Base TestData path</summary>
        private string basePath = String.Empty;

        /// <summary>TestData\CompressTestFolder</summary>
        private string tempPath = String.Empty;

        #endregion

        #region Fixture Setup
        /// <summary>
        ///     This method runs once for the entire test fixture. Place any 
        ///     logic that needs to happen before this test fixture is run in 
        ///     this method.
        /// </summary>
        [TestFixtureSetUp]
        public void FixtureSetup()
        {
            this.basePath = Path.GetFullPath("../../TestData");
            this.tempPath = Path.Combine(this.basePath, "CompressTestFolder");
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
            //this.CleanTempPath(true);
        }
        #endregion

        #region Tests

        /// <summary>
        ///     Make a SignedArchive, and confirm that we can extract and verify
        ///     all files from it.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev05" date="03/06/09" version="1.0.8.17">
        ///     Method created.
        /// </revision>
        /// <revision revisor = "dev05" date="03/19/09" version="1.0.9.3">
        ///     SignedArchive now returns an OperationResult.
        /// </revision>
        [Test]
        public void ExtractTest()
        {
            var rsa = new RSACryptoServiceProvider();

            this.CleanTempPath(true);
            this.PopulateTempPath();
            this.MakeArchive(rsa);

            string publicKey = rsa.ToXmlString(false);
            rsa.FromXmlString(publicKey);

            this.CleanTempPath(false);
            OperationResult result = SignedArchive.ExtractAll(
                this.tempPath,
                Path.Combine(this.tempPath, "archive.zip"),
                rsa);

            Assert.IsTrue(result.Succeeded);
        }

        /// <summary>
        ///     Extracts the encrypted archive test.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        [Test]
        public void ExtractEncryptedArchiveTest()
        {
            this.CleanTempPath(true);
            this.PopulateTempPath();
            
            this.MakeEncryptedArchive();

             //this.CleanTempPath(false);
             OperationResult result = EncryptedArchive.ExtractArchive(
                 this.tempPath,
                 Path.Combine(this.tempPath, "archive.enc"),
                 new SequoiaCryptoProvider(false));
            
            Assert.AreEqual(string.Empty, result.Details);   
            Assert.IsTrue(result.Succeeded);
        }

        /// <summary>
        ///     Extracts the encrypted archive to zip test.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        [Test]
        public void ExtractEncryptedArchiveToZipTest()
        {
            this.CleanTempPath(true);
            this.PopulateTempPath();

            this.MakeEncryptedArchiveHavingZip();

            //this.CleanTempPath(false);
            OperationResult result = EncryptedArchive.ExtractArchiveToZip(
                this.tempPath,
                Path.Combine(this.tempPath, "archive.enc"),
                new SequoiaCryptoProvider());

            Assert.AreEqual(string.Empty, result.Details);
            Assert.IsTrue(result.Succeeded);
        }

        /// <summary>
        ///     Tests the signature using embedded keys.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        [Test]
        public void TestSignatureUsingEmbeddedKeys()
        {
            this.CleanTempPath(true);
            this.PopulateTempPath();

            //this.MakeEncryptedArchive(rsa);
            this.MakeEncryptedArchive();

            // should now have archive.enc and archive.enc.sig at temppath
            // let's verify the sig file
            var crypto = new SequoiaCryptoProvider(false);

            string pathToArchive = Path.Combine(this.tempPath, "archive.enc");
            string pathToSig = Path.Combine(this.tempPath, "archive.enc.sig");

            bool verified = crypto.VerifySignature(pathToSig, pathToArchive);

            Assert.IsTrue(verified);
        }

        /// <summary>
        ///     Tests the make archive from byte data.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        [Test]
        public void TestMakeArchiveFromByteData()
        {
            this.CleanTempPath(true);
            this.PopulateTempPath();

            this.MakeEncryptedArchiveFromBytes();

            //this.CleanTempPath(false);
            OperationResult result = EncryptedArchive.ExtractArchive(
                this.tempPath,
                Path.Combine(this.tempPath, "archive.enc"),
               new SequoiaCryptoProvider());

            Assert.AreEqual(string.Empty, result.Details);
            Assert.IsTrue(result.Succeeded);
        }

        /// <summary>
        ///     Tests the extract zipped archive using stream extractor.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        [Test]
        public void TestExtractZippedArchiveUsingStreamExtractor()
        {
            this.CleanTempPath(true);
            this.PopulateTempPath();

            this.MakeEncryptedArchiveHavingZip();

            //this.CleanTempPath(false);
            OperationResult result = EncryptedArchive.ExtractArchive(
                this.tempPath,
                Path.Combine(this.tempPath, "archive.enc"),
                new SequoiaCryptoProvider());

            Assert.AreEqual(string.Empty, result.Details);
            Assert.IsTrue(result.Succeeded);
        }

        /// <summary>
        ///     Test creation and validation of a file manifest.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev05" date="03/16/09" version="1.0.8.27">
        ///     Method created.
        /// </revision>
        /// <revision revisor = "dev05" date="03/19/09" version="1.0.9.3">
        ///     Manifest now returns an OperationResult.
        /// </revision>
        [Test]
        public void ManifestTest()
        {
            this.CleanTempPath(true);
            this.PopulateTempPath();

            var manifest = new FileManifest(this.tempPath);

            manifest.AddFile("Ballots.xml");
            manifest.AddFile("Cards.xml");
            manifest.AddFile("Faces.xml");

            manifest.ToXmlFile(Path.Combine(this.tempPath, "Manifest.xml"));

            manifest = FileManifest.FromXmlFile(
                Path.Combine(this.tempPath, "Manifest.xml"));

            string fileName = String.Empty;
            OperationResult result = manifest.IsValid();
            Assert.IsTrue(result.Succeeded);
        }

        #endregion

        #region Private methods

        /// <summary>
        ///     Make an archive of the files in TestData\CompressTestFolder
        /// </summary>
        /// <param name="rsa">The RSA provider.</param>
        /// <externalUnit/>
        /// <revision revisor="dev05" date="03/06/09" version="1.0.8.17">
        ///     Method created.
        /// </revision>
        /// <revision revisor="dev05" date="03/16/09" version="1.0.8.27">
        ///     Pass a timestamp to OpenWrite().
        /// </revision>
        private void MakeArchive(RSACryptoServiceProvider rsa)
        {
            SignedArchive sa = new SignedArchive(this.tempPath, "archive.zip");

            sa.OpenWrite(rsa, DateTime.Parse("03/19/2009"));
            foreach (string pathName in Directory.GetFiles(this.tempPath))
            {
                string fileName = Path.GetFileName(pathName);

                if (fileName == "archive.zip")
                {
                    continue;
                }

                sa.AddFile(fileName);
            }

            sa.Close();
        }

        /// <summary>
        ///     Makes the encrypted archive.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        private void MakeEncryptedArchive()
        {
            using(var sa = new EncryptedArchive(
                this.tempPath, "archive.zip", new SequoiaCryptoProvider(false)))
            {
                sa.OpenWrite(DateTime.Today, true);
                foreach (string pathName in Directory.GetFiles(this.tempPath))
                {
                    string fileName = Path.GetFileName(pathName);

                    if (fileName == "archive.zip")
                    {
                        continue;
                    }

                    sa.AddFile(fileName);
                }
            }
        }

        /// <summary>
        ///     Makes the encrypted archive having zip.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        private void MakeEncryptedArchiveHavingZip()
        {
            using (var sa = new EncryptedArchive(
                this.tempPath, "archive.zip", new SequoiaCryptoProvider()))
            {
                sa.OpenWriteToZip(DateTime.Today, true);
                foreach (string pathName in Directory.GetFiles(this.tempPath))
                {
                    string fileName = Path.GetFileName(pathName);

                    if (fileName == "archive.zip")
                    {
                        continue;
                    }

                    sa.AddFile(fileName);
                }
            }
        }

        /// <summary>
        ///     Makes the encrypted archive from bytes.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        private void MakeEncryptedArchiveFromBytes()
        {
            using (var sa = new EncryptedArchive(
                this.tempPath, "archive.zip", new SequoiaCryptoProvider()))
            {
                sa.OpenWrite(DateTime.Today, true);
                foreach (string pathName in Directory.GetFiles(this.tempPath))
                {
                    string fileName = Path.GetFileName(pathName);

                    if (fileName == "archive.zip")
                    {
                        continue;
                    }

                    byte[] fileBytes = File.ReadAllBytes(pathName);
                    sa.AddFile(fileBytes, fileName);
                }
            }
        }

        /// <summary>
        ///     Put some files into TestData\CompressTestFolder, by unzipping
        ///     TestData\Test.zip.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev05" date="03/06/09" version="1.0.8.17">
        ///     Method created.
        /// </revision>
        private void PopulateTempPath()
        {
            string fileName = Path.Combine(this.basePath, "Test.zip");

            ZipHelper.DeCompressFiles(fileName, this.tempPath);
        }

        /// <summary>
        ///     Delete files from TestData\CompressTestFolder, except maybe
        ///     archive.zip.
        /// </summary>
        /// <param name="deleteArchive">
        ///     <c>true</c> to delete archive.zip; otherwise, <c>false</c>.
        /// </param>
        /// <externalUnit/>
        /// <revision revisor="dev05" date="03/06/09" version="1.0.8.17">
        ///     Method created.
        /// </revision>
        private void CleanTempPath(bool deleteArchive)
        {
            foreach (string fileName in Directory.GetFiles(this.tempPath))
            {
                if (deleteArchive || Path.GetFileName(fileName) 
                    != "archive.zip")
                {
                    File.Delete(fileName);
                }
            }
        }

        #endregion
    }
}
