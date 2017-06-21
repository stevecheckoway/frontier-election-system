// -----------------------------------------------------------------------------
// <copyright file="AuthorizationManagerFixture.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2010 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the AuthorizationManagerFixture test class.
// </summary>
// <revision revisor="dev14" date="2/5/2010" version="1.1.6.08">
//     File Created
// </revision>
// <revision revisor="dev14" date="2/25/2010" version="1.1.7.04">
//     File Modified
// </revision>
// -----------------------------------------------------------------------------

namespace Sequoia.DBAuthService.DBAuthServiceTests
{
    using System;
    using System.Configuration;
    using System.Diagnostics;
    using System.IO;
    using NUnit.Framework;
    using Sequoia.DBAuthService;
    using Sequoia.DBAuthService.Data;
    using Sequoia.DBAuthService.DBAuthServiceTests.Mocks;

    /// <summary>
    ///     AuthorizationManagerFixture is a test fixture for running tests against <!-- Insert details here. -->
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev14" date="2/5/2010" version="1.1.6.08">
    ///     Class created.
    /// </revision>
    [TestFixture]
    public class AuthorizationManagerFixture
    {
        #region Fixture Setup
        /// <summary>
        /// This method runs once for the entire test fixture. Place any logic 
        /// that needs to happen before this test fixture is run in this method.
        /// </summary>
        [TestFixtureSetUp]
        public void FixtureSetup()
        {
        }

        #endregion

        #region Fixture Teardown
        /// <summary>
        /// This method runs once for the entire test fixture. Place any logic 
        /// that needs to happen when this test fixture is unloaded.
        /// </summary>
        [TestFixtureTearDown]
        public void FixtureTeardown()
        {
        }
        #endregion

        #region Test Setup
        /// <summary>
        /// This method runs once for every test in the entire test fixture. 
        /// Place any logic that needs to happen before every test is loaded in
        /// this method.
        /// </summary>
        [SetUp]
        public void TestSetup()
        {
        }
        #endregion

        #region Test Teardown
        /// <summary>
        /// This method runs once for every test in the entire test fixture. 
        /// Place any logic that needs to happen when every test is unloaded in
        /// this method.
        /// </summary>
        [TearDown]
        public void TestTeardown()
        {
        }
        #endregion

        #region Tests

        [Test]
        public void TestCreateManager()
        {
            var manager = AuthorizationManager.Instance;
        }

        [Test]
        public void TestAddCredential()
        {
            Assert.AreEqual(0, AuthorizationLock.Credentials.Count);

            Credential cred1 = new Credential("TestDB", "fredAstaire", "myPwd");

            AuthorizationManager.AddCredential(cred1);

            Assert.AreEqual(1, AuthorizationLock.Credentials.Count);

            Credential cred2 = new Credential("TestDB", "NewUser", "newPwd");

            AuthorizationManager.AddCredential(cred2);

            Assert.AreEqual(1, AuthorizationLock.Credentials.Count);

            Credential cred3 = new Credential("NewDB", "NewUser", "newPwd");

            AuthorizationManager.AddCredential(cred3);

            Assert.AreEqual(2, AuthorizationLock.Credentials.Count);

            AuthorizationLock.Credentials.Clear();
        }

        [Test]
        public void TestGetCurrentCredential()
        {
            string dbname = "TestDB";
            Credential currentCredential;
            IAuthorizationDataService service = new MockAuthDataService();
            var manager = AuthorizationManager.Instance;

            Assert.AreEqual(0, AuthorizationLock.Credentials.Count);

            Credential cred1 = new Credential(dbname, "fredAstaire", "myPwd");

            currentCredential =
                manager.GetCurrentCredential(dbname);

            Assert.IsNull(currentCredential);

            AuthorizationManager.AddCredential(cred1);

            currentCredential =
                manager.GetCurrentCredential(dbname);

            Assert.IsNotNull(currentCredential);

            Assert.AreEqual(dbname, currentCredential.DatabaseName);
        }

        [Test]
        public void TestGetNewAuthorization()
        {
            var manager = AuthorizationManager.Instance;

            string dbName = "E109111";

            Credential newAuth = manager.GetNewCredential(dbName);

            Assert.AreEqual(dbName, newAuth.DatabaseName);
        }

        #endregion
    }
}
