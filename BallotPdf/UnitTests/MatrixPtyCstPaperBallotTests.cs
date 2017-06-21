// -----------------------------------------------------------------------------
// <copyright file="MatrixPtyCstPaperBallotTests.cs" company="Sequoia Voting Systems"> 
//    Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//    Distribution of source code is allowable only under the terms of the
//    license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//    This file contains the MatrixPtyCstPaperBallotTests test class.
// </summary>
// <revision revisor="dev11" date="3/10/2009" version="1.0.0.0">
//    File Created
// </revision>  
// -----------------------------------------------------------------------------

namespace UnitTests
{
    #region Using directives

    using System;
    using System.Collections.Generic;
    using System.IO;
    using NUnit.Framework;

    using Sequoia.Ballot;
    using Sequoia.Ballot.Data;
    using Sequoia.DomainObjects;

    #endregion

    /// <summary>
    ///     MatrixPtyCstPaperBallotTests is a test fixture for running tests 
    ///     against MatrixPtyCstPaperBallotTests
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev11" date="3/10/2009" version="1.0.0.0">
    ///     Class created.
    /// </revision>
    [TestFixture]
    public class MatrixPtyCstPaperBallotTests
    {
        #region Fields

        /// <summary>
        ///     ballot test object
        /// </summary>
        private BallotPdfTestObject objTest;

        /// <summary>
        ///     file name param
        /// </summary>
        private string fileName;

        /// <summary>
        ///     full path param
        /// </summary>
        private string fullPath;

        #endregion
        
        #region Fixture Setup
        /// <summary>
        /// This method runs once for the entire test fixture. Place any logic 
        /// that needs to happen before this test fixture is run in this method.
        /// </summary>
        [TestFixtureSetUp]
        public void FixtureSetup()
        {
            this.objTest = new BallotPdfTestObject();
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
            this.objTest.Clear();
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
        ///     Tests the constructor
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="2/6/2009" version="1.0.6.2">
        ///     Member Created
        /// </revision>
        [Test]
        public void ConstructorTest()
        {
            this.objTest.AddStpBallotBallot(1, new int[] { 5, 6, 7 }, 0, 0);
            this.objTest.AddStpBallotBallot(2, new int[] { 100 }, 3, 5 + 6 + 7);

            this.objTest.AddStpContBallot(new int[] { 2, 1, 2 }, 0);
            this.objTest.AddStpContBallot(new[] { 3 }, 3);

            this.objTest.AddStpCandBallot(5 + 6 + 7 + 100);

            this.objTest.AddStpParty(7);

            this.objTest.LoadStpParams();
            this.objTest.SetTarget(0.12, 0.08, 0, 0, 0.0035, 100);

            MatrixPtyCstPaperBallot ballot = new MatrixPtyCstPaperBallot(
                this.objTest.ballots,
                this.objTest.contests,
                this.objTest.candidates,
                this.objTest.parameters,
                this.objTest.contlist,
                this.objTest.candlist,
                this.objTest.parties,
                this.objTest.target,
                1,
                1,
                1);

            Assert.IsTrue(true);
        }

        /// <summary>
        ///     Tests the pdf generation (
        ///     (<see cref="PaperBallot.Draw(string,string)"/>,
        ///     <see cref="PaperBallot.Draw(string)"/>)
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="2/6/2009" version="1.0.6.2">
        ///     Member Created
        /// </revision>
        [Test]
        public void DrawTest()
        {
            this.objTest.AddStpBallotBallot(
                1, new int[] { 5, 10, 7, 5, 8, 6 }, 0, 0);

            this.objTest.AddStpBallotBallot(
                2, new int[] { 100 }, 6, 5 + 10 + 7 + 5 + 8 + 6);

            this.objTest.AddStpContBallot(new int[] { 2, 1, 2, 1, 1, 1 }, 0);
            this.objTest.AddStpContBallot(new[] { 3 }, 6);

            // count is the sum of all candidates
            this.objTest.AddStpCandBallot(5 + 10 + 7 + 5 + 8 + 6 + 100);

            // count is the maximum number of candidates in a contest
            this.objTest.AddStpParty(10);

            this.objTest.LoadStpParams();
            this.objTest.SetParameter("BallotPageWidth", 14.0D);
            this.objTest.SetParameter("BallotPageHeight", 8.5D);
            this.objTest.SetParameter("ContestFont", "HelveticaBold");
            this.objTest.SetParameter("ContestFontSize", 10.0D);
            this.objTest.SetParameter("IdentifierLocation", "Front");
            this.objTest.SetParameter("TargetType", 1);
            this.objTest.SetTarget(0.12, 0.08, 0, 0, 0.0035, 100);

            MatrixPtyCstPaperBallot ballot = new MatrixPtyCstPaperBallot(
                this.objTest.ballots,
                this.objTest.contests,
                this.objTest.candidates,
                this.objTest.parameters,
                this.objTest.contlist,
                this.objTest.candlist,
                this.objTest.parties,
                this.objTest.target,
                1,
                1,
                1);

            this.fileName = "myballot.pdf";
            if (File.Exists(this.fileName) == true)
            {
                File.Delete(this.fileName);
            }

            ballot.Draw(this.fileName);
            Assert.IsTrue(File.Exists(this.fileName));

            string path = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                @"..\..\TestData\"),
                   otherFileName = "myOtherBallot";
            this.fullPath = Path.Combine(path, otherFileName + "_01.pdf");

            if (File.Exists(this.fullPath))
            {
                File.Delete(this.fullPath);
            }

            ballot.Draw(path, otherFileName + ".pdf");
            Assert.IsTrue(File.Exists(this.fullPath));
        }

        /// <summary>
        ///     Tests <see cref="PaperBallot.ExportPositions"/>
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="2/6/2009" version="1.0.6.2">
        ///     Member Created
        /// </revision>
        [Test]
        public void ExportPositionsTest()
        {
            this.objTest.AddStpBallotBallot(1, new int[] { 5, 6, 7 }, 0, 0);
            this.objTest.AddStpBallotBallot(2, new int[] { 100 }, 3, 5 + 6 + 7);

            this.objTest.AddStpContBallot(new int[] { 2, 1, 2 }, 0);
            this.objTest.AddStpContBallot(new[] { 3 }, 3);

            this.objTest.AddStpCandBallot(5 + 6 + 7 + 100);

            // count is the maximum number of candidates in a contest
            this.objTest.AddStpParty(7);

            this.objTest.LoadStpParams();
            BallotPdfTestObject.SetIdentifierFace(
                this.objTest.parameters, PaperSide.Front);
            this.objTest.SetTarget(0.12, 0.08, 0, 0, 0.0035, 100);

            int selectedId = 1;
            MatrixPtyCstPaperBallot ballot = new MatrixPtyCstPaperBallot(
                this.objTest.ballots,
                this.objTest.contests,
                this.objTest.candidates,
                this.objTest.parameters,
                this.objTest.contlist,
                this.objTest.candlist,
                this.objTest.parties,
                this.objTest.target,
                selectedId,
                1,
                1);

            Ballot doBallot = ballot.ExportPositions();
            Assert.IsNotNull(doBallot);
            Assert.IsNotEmpty(doBallot.Cards);
            Assert.AreEqual(doBallot.BallotStyleId, selectedId);
            Assert.IsNotEmpty(doBallot.Cards[0].Faces);
            Assert.AreEqual(doBallot.Cards[0].Faces[0].Marks.Count, 5 + 6 + 7);

            List<int> markIds = new List<int>();
            bool uniqueIds = true;

            foreach (Card card in doBallot.Cards)
            {
                foreach (Face face in card.Faces)
                {
                    markIds.Clear();
                    uniqueIds = true;
                    foreach (Mark mark in face.Marks)
                    {
                        uniqueIds = uniqueIds
                                    && (markIds.Contains(mark.Id) == false);
                        markIds.Add(mark.Id);
                    }

                    Assert.IsTrue(uniqueIds);
                }
            }
        }

        #endregion
    }
}
