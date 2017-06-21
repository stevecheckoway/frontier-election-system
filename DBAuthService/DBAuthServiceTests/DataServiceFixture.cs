// -----------------------------------------------------------------------------
// <copyright file="DataServiceFixture.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2010 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the DataServiceFixture test class.
// </summary>
// <revision revisor="dev14" date="2/24/2010M" version="1.1.7.02">
//     File Created
// </revision>
// -----------------------------------------------------------------------------

using Sequoia.DBAuthService.Data;

namespace Sequoia.DBAuthService.DBAuthServiceTests
{
    using System;
    using NUnit.Framework;

    /// <summary>
    ///     DataServiceFixture is a test fixture for running tests against
    ///     <see cref="Sequoia.DBAuthService.Data.AuthorizationDataService" />
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev14" date="2/24/2010" version="1.1.7.02">
    ///     Class created.
    /// </revision>
    [TestFixture]
    public class DataServiceFixture
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

        /// <summary>
        ///     Tests the [GetDatabaseUser] method of the data service
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="2/24/2010" version="1.1.7.02">
        ///     Member Created
        /// </revision>
        [Test]
        public void TestGetDatabaseUser()
        {
            string dbName = "TestDB";
            var dataService = new AuthorizationDataService();
            string username = dataService.GetDatabaseUser(dbName);
            Assert.IsNotNull(username);
            Assert.IsNotEmpty(username);
        }

        /// <summary>
        ///     Tests the [CreateDatabase] method of the data service.
        ///     This may now be redundant as the service's constructor should
        ///     create the database instance, but may be some time we want this
        ///     available publicly. Leaving until I rule that out.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="2/24/2010" version="1.1.7.01">
        ///     Member Created
        /// </revision>
        [Test]
        public void TestCreateDatabase()
        {
            var dataService = new AuthorizationDataService();
            dataService.CreateDatabase(
                "Initial Catalog=Master;Data Source=.;Integrated Security=SSPI");
            string dbConnection = dataService.GetCurrentConnection();
            Assert.IsNotEmpty(dbConnection);
        }

        #endregion
    }
}
