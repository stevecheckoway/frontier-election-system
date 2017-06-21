// -----------------------------------------------------------------------------
// <copyright file="ContestTests.cs" company="Sequoia Voting Systems"> 
//    Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//    Distribution of source code is allowable only under the terms of the
//    license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//    This file contains the ContestTests test class.
// </summary>
// <revision revisor="dev11" date="1/7/2009" version="1.0.0.0">
//    File Created
// </revision>
// <revision revisor="dev13" date="11/20/2009" version="1.1.3.8">
//    File modified
// </revision>
// -----------------------------------------------------------------------------

namespace UnitTests
{
    #region Using directives

    using System;
    using System.Drawing;

    using NUnit.Framework;

    using Sequoia.Ballot;
    using Sequoia.Ballot.Data;

    #endregion

    /// <summary>
    ///     ContestTests is a test fixture for running tests against ballot
    ///     Contests.
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev11" date="1/7/2009" version="1.0.0.0">
    ///     Class created.
    /// </revision>
    /// <revision revisor="dev13" date="11/20/2009" version="1.1.3.8">
    ///     Formatting changes.
    /// </revision>
    [TestFixture]
    public class ContestTests
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
        ///     Tests the contest constructor
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/20/2009" version="1.1.3.8">
        ///     Added documentation header
        /// </revision>
        [Test]
        public void ContestClassConstructorTests()
        {
            int id = 23;
            string name = "This is the name of the contest";

            Contest cont = new Contest(id, name);
            Assert.AreEqual(cont.Id, id);
            Assert.AreEqual(cont.Name, name);
            Assert.AreEqual(cont.Break, BreakType.None);
            Assert.AreEqual(cont.Page, 0);
        }

        /// <summary>
        ///     Tests contest properties.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/20/2009" version="1.1.3.8">
        ///     Added documentation header
        /// </revision>
        [Test]
        public void ContestPropertyTests()
        {
            Contest cont = new Contest(0);

            cont.Break = BreakType.Column;
            Assert.AreEqual(cont.Break, BreakType.Column);
        }

        #endregion

        #region Methods

        #endregion
    }
}
