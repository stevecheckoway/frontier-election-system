// -----------------------------------------------------------------------------
// <copyright file="AuthorizationResetFixture.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2010 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the AuthorizationResetFixture test class.
// </summary>
// <revision revisor="dev14" date="3/1/2010" version="1.1.7.08">
//     File Created
// </revision>
// -----------------------------------------------------------------------------

using System.Threading;

using Sequoia.DBAuthService.Asynchronous;
using Sequoia.DBAuthService.Data;

namespace Sequoia.DBAuthService.DBAuthServiceTests
{
    using System;
    using NUnit.Framework;

    /// <summary>
    ///     AuthorizationResetFixture is a test fixture for running tests against <!-- Insert details here. -->
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev14" date="3/1/2010" version="1.1.7.08">
    ///     Class created.
    /// </revision>
    [TestFixture]
    public class AuthorizationResetFixture
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
        public void TestCreateTimer()
        {
            const int wait = 0;
            const int interval = 1000;

            string dbname = "TestDB";
            string username = "fredAstaire";
            string password = "myPwd";

            // lock the authorization
            Assert.IsTrue(AuthorizationLock.Credentials.Count == 0);

            var cred1 = new Credential(dbname, username, password);

            AuthorizationManager.AddCredential(cred1);

            Assert.AreEqual(1, AuthorizationLock.Credentials.Count);

            // create a timer
            var reset = new AuthorizationReset(new AuthorizationDataService(), wait, interval);
            
            // make sure timer created
            Assert.IsNotNull(reset);

            var savedCredential = AuthorizationLock.Credentials[0];

            Assert.AreEqual(dbname, savedCredential.DatabaseName);
            Assert.AreEqual(username, savedCredential.CurrentUser);
            Assert.AreEqual(password, savedCredential.CurrentPassword);

            // sleep the given time before timer should fire
            Thread.Sleep(interval);

            // assert that the authorization is not still locked
            var newCredential = AuthorizationLock.Credentials[0];

            Assert.AreEqual(dbname, newCredential.DatabaseName);
            Assert.AreNotEqual(username, newCredential.CurrentUser);
        }

        #endregion
    }
}
