// -----------------------------------------------------------------------------
// <copyright file="BuilderTests.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the BuilderTests test class.
// </summary>
// <revision revisor="dev14" date="9/24/2009" version="1.0.17.16">
//     File Created
// </revision>
// -----------------------------------------------------------------------------

namespace DomainObjectsTests
{
    #region Using directives

    using System;
    using Microsoft.Practices.EnterpriseLibrary.Validation;
    using NUnit.Framework;
    using Sequoia.DomainObjects;
    using Sequoia.DomainObjects.ObjectBuilders;

    #endregion

    /// <summary>
    ///     BuilderTests is a test fixture for running tests against domain
    ///     object builders
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev14" date="9/24/2009" version="1.0.17.16">
    ///     Class created.
    /// </revision>
    [TestFixture]
    public class BuilderTests
    {
        #region Fixture Setup
        /// <summary>
        ///     This method runs once for the entire test fixture. Place any 
        ///     logic that needs to happen before this test fixture is run 
        ///     in this method.
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

        #region CandidateBuilder
        /// <summary>
        ///     Tests the build method of the <see cref="CandidateBuilder"/>.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="9/28/2009" version="1.0.17.20">
        ///     Member Created
        /// </revision>
        [Test]
        public void TestBuildCandidate()
        {
            var builder = new CandidateBuilder();
            builder.Name = "Test Candidate";

            ValidationResults r = Validation.Validate(builder);
            Assert.IsTrue(r.IsValid);

            var candidate = builder.Build();
            Assert.AreEqual("Test Candidate", candidate.Name);
        }

        /// <summary>
        ///     Tests that building a candidate with no name fails.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="9/28/2009" version="1.0.17.20">
        ///     Member Created
        /// </revision>
        [Test]
        public void TestBuildCandidateNoNameFails()
        {
            var builder = new CandidateBuilder();

            ValidationResults r = Validation.Validate(builder);

            Assert.IsFalse(r.IsValid);
        }

        /// <summary>
        ///     Tests the build candidate long name fails.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="9/28/2009" version="1.0.17.20">
        ///     Member Created
        /// </revision>
        [Test]
        public void TestBuildCandidateLongNameFails()
        {
            var builder = new CandidateBuilder();

            builder.Name = "xxx yyyyxxx yyyyxxx yyyyxxx yyyyxxx yyyyxxx" + 
                " yyyyxxx yyyyxxx yyyyxxx yyyyxxx yyyyxxx yyyyxxx yyyyxxx" + 
                " yyyyxxx yyyyxxx yyyy";

            ValidationResults r = Validation.Validate(builder);

            Assert.IsFalse(r.IsValid);

            foreach (ValidationResult result in r)
            {
                Assert.AreEqual(
                    "The Candidate display name must be " +
                    "between 1 and 100 alpha-numeric characters.",
                    result.Message);
            }  
        }

        /// <summary>
        ///     Tests the setting the list order on the candidate.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="9/28/2009" version="1.0.17.20">
        ///     Member Created
        /// </revision>
        [Test]
        public void TestBuildCandidateListOrder()
        {
            var builder = new CandidateBuilder();

            builder.Name = "test";

            builder.ListOrder = 45;

            ValidationResults r = Validation.Validate(builder);

            Assert.IsTrue(r.IsValid);

            var candidate = builder.Build();

            Assert.AreEqual(45, candidate.ListOrder);
        }

        /// <summary>
        ///     Tests that a candidate will be built with a list order of at least 1.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="9/28/2009" version="1.0.17.20">
        ///     Member Created
        /// </revision>
        [Test]
        public void TestBuildCandidateListOrderMinLength()
        {
            var builder = new CandidateBuilder();

            builder.Name = "test";

            builder.ListOrder = 0;

            ValidationResults r = Validation.Validate(builder);

            Assert.IsTrue(r.IsValid);

            var candidate = builder.Build();

            Assert.AreEqual(1, candidate.ListOrder);
        }

        #endregion

        #region ContestBuilder

        /// <summary>
        ///     Tests the build method of the <see cref="ContestBuilder"/>.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="9/29/2009" version="1.0.17.21">
        ///     Member Created
        /// </revision>
        [Test]
        public void TestBuildContest()
        {
            var builder = new ContestBuilder();
            builder.Name = "President";
            builder.VoteFor = 1;
            builder.ContestType = 1;
            builder.PartyId = 0;
            builder.ListOrder = 1;

            ValidationResults r = Validation.Validate(builder);
            Assert.IsTrue(r.IsValid);

            Contest contest = builder.Build();
            Assert.AreEqual("President", contest.Name);
        }

        /// <summary>
        ///     Tests that a candidate will be built with a list order of at least 1.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="9/28/2009" version="1.0.17.20">
        ///     Member Created
        /// </revision>
        [Test]
        public void TestBuildContestListOrderMinLength()
        {
            var builder = new ContestBuilder();

            builder.Name = "test";

            builder.ListOrder = 0;

            ValidationResults r = Validation.Validate(builder);

            Assert.IsTrue(r.IsValid);

            var candidate = builder.Build();

            Assert.AreEqual(1, candidate.ListOrder);
        }

        #endregion

        #region PartyBuilder

        /// <summary>
        ///     Tests the build method of the <see cref="PartyBuilder"/>.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="9/29/2009" version="1.0.17.21">
        ///     Member Created
        /// </revision>
        [Test]
        public void TestBuildParty()
        {
            var builder = new PartyBuilder();
            builder.Name = "President";
            builder.Abbreviation = "PRES";
            builder.ListOrder = 1;

            ValidationResults r = Validation.Validate(builder);
            Assert.IsTrue(r.IsValid);

            Party party = builder.Build();
            Assert.AreEqual("President", party.Name);
            Assert.AreEqual("PRES", party.Abbreviation);
            Assert.AreEqual(1, party.ListOrder);
        }

        /// <summary>
        ///     Tests that a candidate will be built with a list order of at least 1.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="9/29/2009" version="1.0.17.21">
        ///     Member Created
        /// </revision>
        [Test]
        public void TestBuildPartyListOrderMinLength()
        {
            var builder = new PartyBuilder();

            builder.Name = "test";
            builder.Abbreviation = "T";
            builder.ListOrder = 0;

            ValidationResults r = Validation.Validate(builder);

            Assert.IsTrue(r.IsValid);

            Party party = builder.Build();

            Assert.AreEqual(1, party.ListOrder);
        }

        /// <summary>
        ///     Tests the that the party builder requires an abbreviation 
        ///     of at least one char
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="9/29/2009" version="1.0.17.21">
        ///     Member Created
        /// </revision>
        [Test]
        public void TestBuildPartyMinAbbreviation()
        {
            var builder = new PartyBuilder();

            builder.Name = "test";
            builder.Abbreviation = string.Empty;
            builder.ListOrder = 0;

            ValidationResults r = Validation.Validate(builder);

            Assert.IsFalse(r.IsValid);
        }

        #endregion

        #region GroupCategoryBuilder

        /// <summary>
        ///     Tests the build method of the <see cref="GroupCategoryBuilder"/>.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="9/30/2009" version="1.0.17.22">
        ///     Member Created
        /// </revision>
        [Test]
        public void TestBuildCategory()
        {
            var builder = new GroupCategoryBuilder();
            builder.Name = "Precinct";
            builder.ParentId = 5;

            ValidationResults r = Validation.Validate(builder);
            Assert.IsTrue(r.IsValid);

            ReportGroupCategory category = builder.Build();
            Assert.AreEqual("Precinct", category.Name);
            Assert.AreEqual(5, category.ParentId);
        }

        #endregion

        #region ReportGroupBuilder

        /// <summary>
        ///     Tests the build method of the <see cref="ReportGroupBuilder"/>.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="9/30/2009" version="1.0.17.22">
        ///     Member Created
        /// </revision>
        [Test]
        public void TestBuildGroup()
        {
            var builder = new ReportGroupBuilder();
            builder.Name = "Somewhereville";
            builder.CategoryId = 8;
            builder.QueryKey = "Z";

            ValidationResults r = Validation.Validate(builder);
            Assert.IsTrue(r.IsValid);

            ReportGroup group = builder.Build();
            Assert.AreEqual("Somewhereville", group.Name);
            Assert.AreEqual(8, group.CategoryId);
        }

        #endregion

        #endregion
    }
}
