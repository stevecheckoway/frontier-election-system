// -----------------------------------------------------------------------------
// <copyright file="PaperBinderTests.cs" company="Sequoia Voting Systems"> 
//    Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//    This file contains the PaperBinderTests test class.
// </summary>
// <revision revisor="dev11" date="2/26/2009" version="1.0.0.0">
//    File Created
// </revision>  
// -----------------------------------------------------------------------------

namespace UnitTests
{
    #region Using directives

    using System;
    using System.IO;

    using NUnit.Framework;

    using Sequoia.Ballot;
    using Sequoia.Ballot.Data;

    #endregion

    /// <summary>
    ///     PaperBinderTests is a test fixture for running tests against the
    ///     paper binder
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev11" date="2/26/2009" version="1.0.0.0">
    ///     Class created.
    /// </revision>
    /// <revision revisor="dev11" date="03/18/2009" version="1.0.9.0201">
    ///     comments added, field name changed
    /// </revision>
    [TestFixture]
    public class PaperBinderTests
    {
        #region Constants

        /// <summary>
        ///     output folder constant
        /// </summary>
        private const string OutputFolder = @"..\..\TestData\PaperBinder";

        #endregion

        #region Fields

        /// <summary>
        ///     this object handles the creation of mock data for the tests
        /// </summary>
        private BallotPdfTestObject mockData;

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
            try
            {
                string[] files =
                    Directory.GetFiles(OutputFolder, "*.pdf");
                foreach (string file in files)
                {
                    File.Delete(file);
                }
            }
            catch (Exception)
            {
                // some exception thrown, so alert the test may not yield 
                // reliable results
                Assert.IsTrue(false);
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
        ///     No reduction should happen here since all faces are different.
        ///     The front face is identical except for the barcode
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="2/26/2009" version="1.0.8.">
        ///     Member Created
        /// </revision>
        [Test]
        public void FaceReductionTest_AllDifferent()
        {
            // add a ballot style with 2 contests, each contest has 2 candidates
            // ballot style 1
            // contest 1
            // candidate 1
            // candidate 2
            // contest 2
            // candidate 3
            // candidate 4
            this.mockData.AddStpBallotContest(1, 1, 1, 0, 2, 1);

            // add a page break on this contest to place it entirely on another
            // face. (contest display format == 1 means page break)
            this.mockData.AddStpBallotContest(1, 2, 2, 1, 2, 3);

            // add a second ballot style
            // ballot style 2
            // contest 1
            // candidate 1
            // candidate 2
            // contest 3
            // candidate 5
            // candidate 6
            this.mockData.AddStpBallotContest(2, 1, 1, 0, 2, 1);

            // add a page break on this contest to place it entirely on another
            // face. At this point, front faces are identical for both ballots
            // back faces are not
            this.mockData.AddStpBallotContest(2, 3, 3, 1, 2, 5);

            // now add header texts for all contests
            // 3 contests, 1 text per contest, first id is 1
            this.mockData.AddStpContBallot(new int[] { 1, 1, 1 }, 1);

            // add candidate texts for all candidates
            // 6 candidates total, first candidate id is 1
            this.mockData.AddStpCandBallot(6, 1);

            // load parameters from DB
            this.mockData.LoadStpParams();
            this.mockData.SetTarget(0.12, 0.08, 0, 0, 0.0035, 100);

            // make sure the barcode location is on the front of the paper
            // take the parameters entry set and set the IdentifierLocation
            // parameter to Front
            BallotPdfTestObject.SetIdentifierFace(
                this.mockData.parameters, PaperSide.Front);

            // make sure that at least 1 field is included on the barcode
            BallotPdfTestObject.SetIdentifierMask(this.mockData.parameters, 1);

            // create paper ballot objects for both ballot styles
            PaperBallot ballot1 = new PaperBallot(
                this.mockData.ballots,
                this.mockData.contests,
                this.mockData.candidates,
                this.mockData.parameters,
                this.mockData.target,
                1,
                0,
                0),
                        
                        // for the second ballot, we already have 1 card and 
                        // 2 faces
                        ballot2 = new PaperBallot(
                            this.mockData.ballots,
                            this.mockData.contests,
                            this.mockData.candidates,
                            this.mockData.parameters,
                            this.mockData.target,
                            2,
                            1,
                            2);

            // create a paper binder
            PaperBinder binder = new PaperBinder();
            binder.AddBallot(ballot1);
            binder.AddBallot(ballot2);

            binder.Generate(OutputFolder);

            // wait a few seconds to let the PDF library flush all docs
            System.Threading.Thread.Sleep(4000);

            string[] files = Directory.GetFiles(OutputFolder, "*.pdf");
            Assert.AreEqual(4, files.Length);
        }

        /// <summary>
        ///     Tests that the front is the same in face reduction
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/20/2009" version="1.1.3.8">
        ///     Added documentation header
        /// </revision>
        [Test]
        public void FaceReductionTest_FrontIsSame()
        {
            // add a ballot style with 2 contests, each contest has 2 candidates
            // ballot style 3
            // contest 1
            // candidate 1
            // candidate 2
            // contest 2
            // candidate 3
            // candidate 4
            this.mockData.AddStpBallotContest(3, 1, 1, 0, 2, 1);

            // add a page break on this contest to place it entirely on another
            // face. (contest display format == 1 means page break)
            this.mockData.AddStpBallotContest(3, 2, 2, 1, 2, 3);

            // add a second ballot style
            // ballot style 4
            // contest 1
            // candidate 1
            // candidate 2
            // contest 3
            // candidate 5
            // candidate 6
            this.mockData.AddStpBallotContest(4, 1, 1, 0, 2, 1);

            // add a page break on this contest to place it entirely on another
            // face. At this point, front faces are identical for both ballots
            // back faces are not
            this.mockData.AddStpBallotContest(4, 3, 3, 1, 2, 5);

            // now add header texts for all contests
            // 4 contests, 1 text per contest, first id is 1
            this.mockData.AddStpContBallot(new int[] { 1, 1, 1 }, 1);

            // add candidate texts for all candidates
            // 16 candidates total, first candidate id is 1
            this.mockData.AddStpCandBallot(6, 1);

            // load parameters from DB
            this.mockData.LoadStpParams();

            // make sure the barcode location is on the back of the paper
            BallotPdfTestObject.SetIdentifierFace(
                this.mockData.parameters, PaperSide.Back);

            // make sure that at least 1 field is included on the barcode
            BallotPdfTestObject.SetIdentifierMask(this.mockData.parameters, 1);
            this.mockData.SetTarget(0.12, 0.08, 0, 0, 0.0035, 100);

            int cardCount = 0, faceCount = 0;

            // create paper ballot objects for both ballot styles
            PaperBallot ballot1 = new PaperBallot(
                this.mockData.ballots,
                this.mockData.contests,
                this.mockData.candidates,
                this.mockData.parameters,
                this.mockData.target,
                3,
                cardCount,
                faceCount);

            // for the second ballot, we already have 1 card and 2 faces
            cardCount += ballot1.CardCount;
            faceCount += ballot1.FaceCount;
            PaperBallot ballot2 = new PaperBallot(
                this.mockData.ballots,
                this.mockData.contests,
                this.mockData.candidates,
                this.mockData.parameters,
                this.mockData.target,
                4,
                cardCount,
                faceCount);

            // create a paper binder
            PaperBinder binder = new PaperBinder();
            binder.AddBallot(ballot1);
            binder.AddBallot(ballot2);

            binder.Generate(OutputFolder);

            // wait a few seconds to let the PDF library flush all docs
            System.Threading.Thread.Sleep(4000);

            string[] files = Directory.GetFiles(OutputFolder, "*.pdf");

            // 1 face is reused, so only 3 files should be found
            Assert.AreEqual(3, files.Length);
        }

        /// <summary>
        ///     Tests face reduction to make sure that odd faces on the back
        ///     contain a barcode.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/20/2009" version="1.1.3.8">
        ///     Added documentation header.
        /// </revision>
        [Test]
        public void FaceReductionTest_OddFacesWithBarcodeInTheBack()
        {
            // add a ballot style with 2 contests, each contest has 2 candidates
            // ballot style 3
            // contest 1
            // candidate 1
            // candidate 2
            // contest 2
            // candidate 3
            // candidate 4
            this.mockData.AddStpBallotContest(3, 1, 1, 0, 2, 1);

            // add a page break on this contest to place it entirely on another
            // face. (contest display format == 1 means page break)
            this.mockData.AddStpBallotContest(3, 2, 2, 1, 2, 3);

            // add a second ballot style
            // ballot style 4
            // contest 1
            // candidate 1
            // candidate 2
            // contest 3
            // candidate 5
            // candidate 6
            this.mockData.AddStpBallotContest(4, 1, 1, 0, 2, 1);

            // add a page break on this contest to place it entirely on another
            // face. At this point, front faces are identical for both ballots
            // back faces are not
            this.mockData.AddStpBallotContest(4, 3, 3, 1, 2, 5);

            // add a third face to test that a 4th empty face is added just for
            // the barcode
            this.mockData.AddStpBallotContest(4, 4, 4, 1, 10, 7);

            // now add header texts for all contests
            // 4 contests, 1 text per contest, first id is 1
            this.mockData.AddStpContBallot(new int[] { 1, 1, 1, 1 }, 1);

            // add candidate texts for all candidates
            // 16 candidates total, first candidate id is 1
            this.mockData.AddStpCandBallot(16, 1);

            // load parameters from DB
            this.mockData.LoadStpParams();
            this.mockData.SetTarget(0.12, 0.08, 0, 0, 0.0035, 100);

            // make sure the barcode location is on the back of the paper
            BallotPdfTestObject.SetIdentifierFace(
                this.mockData.parameters, PaperSide.Back);

            // make sure that at least 1 field is included on the barcode
            BallotPdfTestObject.SetIdentifierMask(this.mockData.parameters, 1);

            int cardCount = 0, faceCount = 0;

            // create paper ballot objects for both ballot styles
            PaperBallot ballot1 = new PaperBallot(
                this.mockData.ballots,
                this.mockData.contests,
                this.mockData.candidates,
                this.mockData.parameters,
                this.mockData.target,
                3,
                cardCount,
                faceCount);

            // for the second ballot, we already have 1 card and 2 faces
            cardCount += ballot1.CardCount;
            faceCount += ballot1.FaceCount;
            PaperBallot ballot2 = new PaperBallot(
                this.mockData.ballots,
                this.mockData.contests,
                this.mockData.candidates,
                this.mockData.parameters,
                this.mockData.target,
                4,
                cardCount,
                faceCount);

            // create a paper binder
            PaperBinder binder = new PaperBinder();
            binder.AddBallot(ballot1);
            binder.AddBallot(ballot2);

            binder.Generate(OutputFolder);

            // wait a few seconds to let the PDF library flush all docs
            System.Threading.Thread.Sleep(4000);

            string[] files = Directory.GetFiles(OutputFolder, "*.pdf");

            // 1 face is reused, and 1 blank face is added so 5 files 
            // should be found
            Assert.AreEqual(5, files.Length);
        }

        #endregion
    }
}
