// -----------------------------------------------------------------------------
// <copyright file="FileManagerTest.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the FileManagerTest test class.
// </summary>
// <revision revisor="dev01" date="1/23/2009" version="1.0.5.4">
//     File Created
// </revision>  
// -----------------------------------------------------------------------------

namespace UtilityTests
{
    #region Using directives

    using System;
    using System.IO;
    
    using NUnit.Framework;

    using Sequoia.Utilities.IO;

    #endregion

    /// <summary>
    ///	    FileManagerTest is a test fixture for running tests against
    ///     <see cref="FileManager"/>
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev01" date="1/23/2009" version="1.0.5.4">
    ///     Class created.
    /// </revision>
    /// <revision revisor="dev14" date="2/3/2009" version="1.0.5.15">
    ///     Added a series of unit tests to test FileManager refactoring
    /// </revision>
    [TestFixture]
    public class FileManagerTest
    {
        /// <summary>
        /// A string to hold the path to a directory to test with
        /// </summary>
        private string testPath = String.Empty;
        
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
            // set the test path
            this.testPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                @"..\..\TestData\FileManagerTests");

            // create a directory to test in
            if (!Directory.Exists(this.testPath))
            {
                Directory.CreateDirectory(this.testPath);
            }
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
        }
        #endregion

        #region Tests

        /// <summary>
        ///     Tests <see cref="FileManager.DeleteAllFilesInFolder(string)" /> 
        ///     and <see cref="FileManager.DeleteAllFilesInFolder(string,string)" />
        /// </summary>
        [Test]
        public void TestDeleteAllFilesInFolder()
        {
            string folderWithFiles = Path.Combine(this.testPath, "TestFolder");

            // create the Test Folder if it does not exist
            if (! Directory.Exists(folderWithFiles))
            {
                Directory.CreateDirectory(folderWithFiles);
            }

            // create the file to delete
            string fileName = Path.Combine(folderWithFiles, "testFile.txt");
            if (!File.Exists(fileName))
            {
                FileStream fileStream = File.Create(fileName);
                fileStream.Close();
                fileStream.Dispose();   
            }

            Assert.AreEqual(
                Directory.GetFiles(folderWithFiles).Length, 1);

            Assert.IsTrue(FileManager.DeleteAllFilesInFolder(folderWithFiles));
            Assert.AreEqual(
                Directory.GetFiles(folderWithFiles).Length, 0);
        }

        /// <summary>
        ///     Tests <see cref="FileManager.DeleteFiles" />
        /// </summary>
        [Test]
        public void TestDeleteFiles()
        {
            // confirm that the test directory exists
            Assert.IsTrue(Directory.Exists(this.testPath));

            string testFileName = Path.Combine(this.testPath, "testFile.txt");
            
            // create a test file if none exists
            if (! File.Exists(testFileName))
            {
                FileStream fileStream = File.Create(testFileName);
                fileStream.Close();
                fileStream.Dispose();
            }

            // confirm that the test file exists
            Assert.IsTrue(File.Exists(testFileName));

            var files = new string[1];
            files[0] = testFileName;
            bool deletedFiles = FileManager.DeleteFiles(files);
            Assert.IsTrue(deletedFiles);
            
            Assert.IsFalse(File.Exists(testFileName));
            Assert.IsTrue(Directory.Exists(this.testPath));
        }

        /// <summary>
        ///     Tests <see cref="FileManager.FullFolderRestore"/>
        /// </summary>
        [Test]
        public void TestFullFolderRestore()
        {
            // set the name of the directory to restore
            string topLevel = Path.Combine(this.testPath, "RestoreToHere");

            // remove the directory if it exists
            if (Directory.Exists(topLevel))
            {
                Directory.Delete(topLevel);
            }

            // restore the directory
            bool foldersRestored = FileManager.FullFolderRestore(topLevel);

            Assert.IsTrue(foldersRestored);
            Assert.IsTrue(Directory.Exists(topLevel));
        }

        /// <summary>
        ///     Tests that <see cref="FileManager.FullFolderRestore"/> throws a
        ///     <see cref="System.ArgumentNullException"/> when toplevelName 
        ///     is blank.
        /// </summary>
        [Test]
        [ExpectedException(
            "System.ArgumentNullException",
            ExpectedMessage = 
            "No path was supplied for recursive folder creation.")]
        public void TestFullFolderRestoreThrowsArgException()
        {
            // set an empty name for the top level directory
            string topLevel = String.Empty;

            // calling RecursiveDelete should throw an exception
            FileManager.FullFolderRestore(topLevel);
        }

        /// <summary>
        /// Tests <see cref="FileManager.RecreateFolders"/>.
        /// </summary>
        [Test]
        public void TestRecreateFolders()
        {
            // set the second level folder name
            string secondLevel = Path.Combine(this.testPath, "RecreateToHere");
            
            // create a directory 3 levels down
            string thirdLevel = Path.Combine(secondLevel, "NotHere");
            
            // create folders down to third level
            FileManager.FullFolderRestore(thirdLevel);

            // confirm our third level directory exists
            Assert.IsTrue(Directory.Exists(thirdLevel));

            // Restore the folders to clean state
            bool foldersRestored = FileManager.RecreateFolders(
                                this.testPath,
                                secondLevel);

            // confirm operation successful
            Assert.IsTrue(foldersRestored);
            
            // confirm second level directory exists
            Assert.IsTrue(Directory.Exists(secondLevel));

            // confirm third level directory does not exist
            Assert.IsFalse(Directory.Exists(thirdLevel));
        }

        /// <summary>
        ///     Tests that <see cref="FileManager.RecreateFolders"/> throws IO 
        ///     exception when trying to re-create a read-only directory
        /// </summary>
        [Test]
        [ExpectedException(
            "Sequoia.Utilities.IO.FileManagerException",
            ExpectedMessage = "IO Failure")]
        public void TestRecreateFoldersThrowsIOException()
        {
            // set the protected folder name
            string protectedFolder = Path.Combine(
                this.testPath,
                @"..\ReadOnlyTestDir");

            // create the protected folder if it doesn't exist
            if (!Directory.Exists(protectedFolder))
            {
                Directory.CreateDirectory(protectedFolder);
            }

            var directoryInfo = new DirectoryInfo(protectedFolder);
            directoryInfo.Attributes = FileAttributes.ReadOnly;

            string secondLevel = Path.Combine(protectedFolder, "RestoreToHere");

            FileManager.RecreateFolders(protectedFolder, secondLevel);
        }

        /// <summary>
        ///     Tests <see cref="FileManager.RecursiveDelete"/>
        /// </summary>
        [Test]
        public void TestRecursiveDelete()
        { 
            // set the name of the top level directory to one that does not exist
            string topLevel = Path.Combine(
                    this.testPath, 
                    "NonExistantDirectory");
            
            // try to delete a nonexistant directory
            bool directoryDeleted = FileManager.RecursiveDelete(topLevel);
            
            // We don't care that it can't find a non-existant directory 
            Assert.IsTrue(directoryDeleted);
            
            // ensure that a directory exists
            topLevel = Path.Combine(
                    this.testPath,
                    "FileManagerTests");
            if (!Directory.Exists(topLevel))
            {
                Directory.CreateDirectory(topLevel);
            }

            // try to delete an existing directory
            directoryDeleted = FileManager.RecursiveDelete(topLevel);
            Assert.IsTrue(directoryDeleted);
            Assert.IsFalse(Directory.Exists(topLevel));
        }

        /// <summary>
        ///     Tests that <see cref="FileManager.RecursiveDelete"/> throws a
        ///     <see cref="System.ArgumentNullException"/> when toplevelName 
        ///     is blank.
        /// </summary>
        [Test]
        [ExpectedException(
            "System.ArgumentNullException", 
            ExpectedMessage = 
            "No path was supplied for recursive folder deletion.")]
        public void TestRecursiveDeleteThrowsArgException()
        {
            // set an empty name for the top level directory
            string topLevel = String.Empty;
            
            // calling RecursiveDelete should throw an exception
            FileManager.RecursiveDelete(topLevel);
        }

        /// <summary>
        /// Tests that <see cref="FileManager.RecursiveDelete"/> throws IO 
        /// exception when trying to remove a read-only directory
        /// </summary>
        [Test]
        [ExpectedException(
            "Sequoia.Utilities.IO.FileManagerException",
            ExpectedMessage = "IO Failure")]
        public void TestRecursiveDeleteThrowsIOException()
        {
            // set the protected folder name
            string protectedFolder = Path.Combine(
                this.testPath, 
                @"..\ReadOnlyTestDir");
            
            // create the protected folder if it doesn't exist
            if (!Directory.Exists(protectedFolder))
            {
                Directory.CreateDirectory(protectedFolder);
            }

            var directoryInfo = new DirectoryInfo(protectedFolder);
            directoryInfo.Attributes = FileAttributes.ReadOnly;

            FileManager.RecursiveDelete(protectedFolder);
        }

        /// <summary>
        ///     Tests throwing file manager exception.
        /// </summary>
        [Test]
        [ExpectedException(
               "Sequoia.Utilities.IO.FileManagerException", 
               ExpectedMessage = "test exception")]
        public void TestThrowFileManagerException()
        {
            throw new FileManagerException("test exception");
        }

        #endregion
    }
}
