// -----------------------------------------------------------------------------
// <copyright file="PaperBallotTests.cs" company="Sequoia Voting Systems"> 
//    Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//    Distribution of source code is allowable only under the terms of the
//    license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//    This file contains the PaperBallotTests test class.
// </summary>
// <revision revisor="dev11" date="1/13/2009" version="1.0.0.0">
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
    ///     PaperBallotTests is a test fixture for running tests against the 
    ///     paper ballot
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev11" date="1/13/2009" version="1.0.0.0">
    ///     Class created.
    /// </revision>
    /// <revision revisor="dev11" date="03/18/2009" version="1.0.9.0201">
    ///     comments added, field name changed
    /// </revision>
    [TestFixture]
    public class PaperBallotTests
    {
        #region Fields

        /// <summary>
        ///     this object handles the creation of mock data for the tests
        /// </summary>
        private BallotPdfTestObject mockData;

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
        ///     This method runs once for the entire test fixture. Place any 
        ///     logic that needs to happen before this test fixture is run in 
        ///     this method.
        /// </summary>
        [TestFixtureSetUp]
        public void FixtureSetup()
        {
            this.mockData = new BallotPdfTestObject();
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
            this.mockData.Clear();
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
        ///     Tests the constructor
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="2/6/2009" version="1.0.6.2">
        ///     Member Created
        /// </revision>
        [Test]
        public void ConstructorTest()
        {
            this.mockData.AddStpBallotBallot(1, new int[] { 5, 6, 7 }, 0, 0);
            this.mockData.AddStpBallotBallot(
                2, new int[] { 100 }, 3, 5 + 6 + 7);

            this.mockData.AddStpContBallot(new int[] { 2, 1, 2 }, 0);
            this.mockData.AddStpContBallot(new[] { 3 }, 3);

            this.mockData.AddStpCandBallot(5 + 6 + 7 + 100);

            this.mockData.LoadStpParams();
            this.mockData.SetTarget(0.12, 0.08, 0, 0, 0.0035, 100);

            PaperBallot ballot = new PaperBallot(
                this.mockData.ballots,
                this.mockData.contests,
                this.mockData.candidates,
                this.mockData.parameters,
                this.mockData.target,
                1);

            Assert.IsTrue(true);
        }

        /// <summary>
        ///     Tests the pdf generation 
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
            this.mockData.AddStpBallotBallot(1, new int[] { 5, 6, 7 }, 0, 0);
            this.mockData.AddStpBallotBallot(
                2, new int[] { 100 }, 3, 5 + 6 + 7);

            this.mockData.AddStpContBallot(new int[] { 2, 1, 2 }, 0);
            this.mockData.AddStpContBallot(new[] { 3 }, 3);

            this.mockData.AddStpCandBallot(5 + 6 + 7 + 100);

            this.mockData.LoadStpParams();
            mockData.SetParameter("TargetType", 2);
            mockData.SetParameter("TargetLayout", "Right");
            this.mockData.SetTarget(0.12, 0.08, 0, 0, 0.003, 100);

            PaperBallot ballot = new PaperBallot(
                this.mockData.ballots,
                this.mockData.contests,
                this.mockData.candidates,
                this.mockData.parameters,
                this.mockData.target,
                2);

            ballot.ElectionInfo =
                new PaperBallotElectionInfo(
                    BallotPdfTestObject.ConnectionString);

            fileName = "myballot.pdf";
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            ballot.Draw(fileName);
            Assert.IsTrue(File.Exists(fileName));

            string path = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                @"..\..\TestData\"),
                   otherFileName = "myOtherBallot";
            fullPath = Path.Combine(path, otherFileName + "_01.pdf");

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }

            ballot.Draw(path, otherFileName + ".pdf");
            Assert.IsTrue(File.Exists(fullPath));

            otherFileName = "myOtherTxmlBallot";
            fullPath = Path.Combine(path, otherFileName + "_01.pdf");
            PaperBallotRichTextTemplate template =
                new PaperBallotRichTextTemplate(
                    @"..\..\..\BallotPdf\BallotTemplates.xml");
            ballot.Template = template;
            ballot.BallotStyleName = "MyMunicipality";
            ballot.PrecinctName = "MyPrecinct";

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }

            ballot.Draw(path, otherFileName + ".pdf");
            Assert.IsTrue(File.Exists(fullPath));

            ballot.IsAudioBallot = true;
            ballot.Draw(path, "audioballot.pdf");
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
            this.mockData.AddStpBallotBallot(1, new int[] { 5, 6, 7 }, 0, 0);
            this.mockData.AddStpBallotBallot(
                2, new int[] { 100 }, 3, 5 + 6 + 7);

            this.mockData.AddStpContBallot(new int[] { 2, 1, 2 }, 0);
            this.mockData.AddStpContBallot(new[] { 3 }, 3);

            this.mockData.AddStpCandBallot(5 + 6 + 7 + 100);

            this.mockData.LoadStpParams();
            BallotPdfTestObject.SetIdentifierFace(
                this.mockData.parameters, PaperSide.Front);

            this.mockData.SetTarget(0.12, 0.08, 0, 0, 0.0035, 100);

            int selectedId = 1;
            PaperBallot ballot = new PaperBallot(
                this.mockData.ballots,
                this.mockData.contests,
                this.mockData.candidates,
                this.mockData.parameters,
                this.mockData.target,
                selectedId);

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

        #region Private Methods

        #endregion
    }
}
