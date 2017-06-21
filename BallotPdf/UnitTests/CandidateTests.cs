// -----------------------------------------------------------------------------
// <copyright file="CandidateTests.cs" company="Sequoia Voting Systems"> 
//    Copyright (c) 2008 Sequoia Voting Systems, Inc. All Rights Reserved.
//    Distribution of source code is allowable only under the terms of the
//    license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//    This file contains the CandidateTests test class.
// </summary>
// <revision revisor="dev11" date="12/30/2008" version="1.0.0.0">
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
    using System.Xml;
    using System.Xml.XPath;

    using NUnit.Framework;

    using Sequoia.Ballot.Data;

    #endregion

    /// <summary>
    ///     CandidateTests is a test fixture for running tests against the
    ///     ballot candidate
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev11" date="12/30/2008" version="1.0.0.0">
    ///     Class created.
    /// </revision>
    /// <revision revisor="dev13" date="11/20/2009" version="1.1.3.8">
    ///     Formatting changes.
    /// </revision>
    [TestFixture]
    public class CandidateTests
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
        ///     logic  that needs to happen when this test fixture is unloaded.
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
        ///     Candidate constructor test
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/20/2009" version="1.1.3.8">
        ///     Added documentation header
        /// </revision>
        [Test]
        public void CandidateConstructorTests()
        {
            int id = 17;
            string name = "John Doe";

            Candidate cand = new Candidate(id, name);
            this.AssertCandidate(cand, id, name, false, false);
            
            cand = new Candidate(id, name, true, true);
            this.AssertCandidate(cand, id, name, true, true);
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Asserts the candidate.
        /// </summary>
        /// <param name="cand">The candidate.</param>
        /// <param name="id">The candidate id.</param>
        /// <param name="name">The candidate name.</param>
        /// <param name="voted">if set to <c>true</c> [voted].</param>
        /// <param name="columnBreak">
        ///     if set to <c>true</c> [column break].
        /// </param>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/20/2009" version="1.1.3.8">
        ///     Added documentation header
        /// </revision>
        private void AssertCandidate(
            Candidate cand, int id, string name, bool voted, bool columnBreak)
        {
            Assert.AreEqual(cand.Id, id);
            Assert.AreEqual(cand.Name, name);
            Assert.AreEqual(cand.Voted, voted);
            Assert.AreEqual(cand.StartNewColumn, columnBreak);
        }

        #endregion
    }
}
