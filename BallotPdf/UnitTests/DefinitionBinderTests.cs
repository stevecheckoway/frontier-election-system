// -----------------------------------------------------------------------------
// <copyright file="DefinitionBinderTests.cs" company="Sequoia Voting Systems"> 
//    Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//    Distribution of source code is allowable only under the terms of the
//    license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//    This file contains the DefinitionBinderTests test class.
// </summary>
// <revision revisor="dev11" date="2/9/2009" version="1.0.6.5">
//    File Created
// </revision>  
// -----------------------------------------------------------------------------

namespace UnitTests
{
    #region Using directives

    using System;

    using NUnit.Framework;

    using Sequoia.Ballot;
    using Sequoia.Ballot.Data;
    using Sequoia.DomainObjects;

    #endregion

    /// <summary>
    ///     DefinitionBinderTests is a test fixture for running tests against 
    ///     the definition binder.
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev11" date="2/9/2009" version="1.0.6.5">
    ///     Class created.
    /// </revision>
    [TestFixture]
    public class DefinitionBinderTests
    {
        private BallotPdfTestObject objTest;

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
        ///     Tests <see cref="DefinitionBinder.AddBallot"/>
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="2/9/2009" version="1.0.6.5">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev11" date="02/19/2009" version="1.0.8.0201">
        ///     added comments
        /// </revision>
        [Test]
        public void AddBallotTest()
        {
            // add a ballot style with 3 contests (5 cands, 6 cands and 7 cands
            // respectively)
            objTest.AddStpBallotBallot(1, new int[] { 5, 6, 7 }, 0, 0);

            // add a second ballot style with 1 contest (100 candidates)
            // for contest ids, start from 3 since, there are already 3 contests
            // for candidate ids, start from (5+6+7) since that's the number of
            // candidates already in the collection
            objTest.AddStpBallotBallot(2, new int[] { 100 }, 3, 5 + 6 + 7);

            // add header texts for the contests of the first ballot style
            // (2 texts, 1 text and 2 texts respectively)
            objTest.AddStpContBallot(new int[] { 2, 1, 2 }, 0);
            objTest.AddStpContBallot(new[] { 3 }, 3);

            // add candidate texts for all candidates
            objTest.AddStpCandBallot(5 + 6 + 7 + 100);

            // load parameters from DB
            objTest.LoadStpParams();
            this.objTest.SetTarget(0.12, 0.08, 0, 0, 0.0035, 100);

            int selectedId = 1;
            PaperBallot ballot = new PaperBallot(
                objTest.ballots, objTest.contests, objTest.candidates, 
                objTest.parameters, objTest.target, selectedId);

            Ballot doBallot = ballot.ExportPositions();

            Assert.IsNotEmpty(doBallot.Cards[0].Faces);

            DefinitionBinder binder = new DefinitionBinder();
            binder.AddBallot(doBallot);

            // assert that the first ballot has at least 1 card
            // this proves that the binder properly created a ballot 
            // (domain object) for the specified paper ballot and added a 
            // card to it
            Assert.IsNotNull(binder.Ballots[0].Cards[0]);

            // but that card has no faces even though the exported ballot 
            // (doBallot) has faces there
            Assert.IsNull(binder.Ballots[0].Cards[0].Faces);

            // assert that the cards collection is not empty
            // This proves the binder extracted the cards from the given ballot
            // and populated the card collection
            Assert.IsNotEmpty(binder.Cards);

            // assert that the faces collection is not empty
            // This proves the binder properly extracted the faces from the 
            // given ballot and 
            Assert.IsNotEmpty(binder.Faces);
        }

        /// <summary>
        ///     Tests the face reduction capabilities of 
        ///     <see cref="DefinitionBinder.AddBallot"/>
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="2/20/2009" version="1.0.8.0301">
        ///     Member Created
        /// </revision>
        [Test]
        public void FaceReductionTest()
        {
            // add a ballot style with 2 contests, each contest has 2 candidates
            // ballot style 1
            // contest 1
            // candidate 1
            // candidate 2
            // contest 2
            // candidate 3
            // candidate 4
            this.objTest.AddStpBallotContest(1, 1, 1, 0, 2, 1);

            // add a page break on this contest to place it entirely on another
            // face. (contest display format == 1 means page break)
            this.objTest.AddStpBallotContest(1, 2, 2, 1, 2, 3);

            // add a second ballot style
            // ballot style 2
            // contest 1
            // candidate 1
            // candidate 2
            // contest 3
            // candidate 5
            // candidate 6
            this.objTest.AddStpBallotContest(2, 1, 1, 0, 2, 1);

            // add a page break on this contest to place it entirely on another
            // face. At this point, front faces are identical for both ballots
            // back faces are not
            this.objTest.AddStpBallotContest(2, 3, 3, 1, 2, 5);

            // now add header texts for all contests
            // 3 contests, 1 text per contest, first id is 1
            this.objTest.AddStpContBallot(new int[] { 1, 1, 1 }, 1);

            // add candidate texts for all candidates
            // 6 candidates total, first candidate id is 1
            this.objTest.AddStpCandBallot(6, 1);

            // load parameters from DB
            this.objTest.LoadStpParams();
            this.objTest.SetTarget(0.12, 0.08, 0, 0, 0.0035, 100);

            // make sure the barcode location is on the front of the paper
            BallotPdfTestObject.SetIdentifierFace(
                this.objTest.parameters, PaperSide.Front);

            // create paper ballot objects for both ballot styles
            PaperBallot ballot1 = new PaperBallot(
                this.objTest.ballots,
                this.objTest.contests,
                this.objTest.candidates,
                this.objTest.parameters,
                this.objTest.target,
                1,
                0,
                0),
                        
                        // for the second ballot, we already have 1 card and 
                        // 2 faces
                        ballot2 = new PaperBallot(
                            this.objTest.ballots,
                            this.objTest.contests,
                            this.objTest.candidates,
                            this.objTest.parameters,
                            this.objTest.target,
                            2,
                            1,
                            2);

            // create a definition binder and add both ballots
            // the definition binder should be smart enough to notice that the
            // first face of both ballot cards are identical, resulting in only
            // 3 faces for 2 cards, 1 card per ballot
            DefinitionBinder binder = new DefinitionBinder();
            binder.AddBallot(ballot1.ExportPositions());
            binder.AddBallot(ballot2.ExportPositions());

            // save files for further inspection
            this.objTest.Serialize(
                @"..\..\TestData\Ballots.xml",
                binder.Ballots,
                typeof(BallotList));
            this.objTest.Serialize(
                @"..\..\TestData\Cards.xml", binder.Cards, typeof(CardList));
            this.objTest.Serialize(
                @"..\..\TestData\Faces.xml", binder.Faces, typeof(FaceList));

            // check the results
            // 3 faces expected, not 4 even though there are 2 cards, 2 \
            // faces each
            Assert.AreEqual(3, binder.Faces.Count);

            // 2 cards, 1 for each ballot style
            Assert.AreEqual(2, binder.Cards.Count);

            // the first face of both cards are the same
            Assert.AreEqual(
                binder.Cards[0].Faces[0].Id,
                binder.Cards[1].Faces[0].Id);

            // verify face ids
            Assert.AreEqual(0, binder.Cards[0].Faces[0].Id);
            Assert.AreEqual(1, binder.Cards[0].Faces[1].Id);
            Assert.AreEqual(0, binder.Cards[1].Faces[0].Id);
            Assert.AreEqual(3, binder.Cards[1].Faces[1].Id);

            // verify card ids
            Assert.AreEqual(0, binder.Cards[0].Id);
            Assert.AreEqual(1, binder.Cards[1].Id);

            // verify ballot ids
            Assert.AreEqual(1, binder.Ballots[0].Id);
            Assert.AreEqual(2, binder.Ballots[1].Id);

            // verify ballot style ids
            Assert.AreEqual(1, binder.Ballots[0].BallotStyleId);
            Assert.AreEqual(2, binder.Ballots[1].BallotStyleId);
        }

        /// <summary>
        ///     Tests the face reduction at XML level, when there is an odd 
        ///     number of faces in 1 ballot, but the barcode has been configured 
        ///     to appear on the back of the card.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="3/16/2009" version="1.0.">
        ///     Member Created
        /// </revision>
        [Test]
        public void FaceReductionTest_OddFacesWithBarcodeInTheBack()
        {
            // add a ballot style with 2 contests, each contest has 2 candidates
            // ballot style 1
            // contest 1
            // candidate 1
            // candidate 2
            // contest 2
            // candidate 3
            // candidate 4
            this.objTest.AddStpBallotContest(1, 1, 1, 0, 2, 1);

            // add a page break on this contest to place it entirely on another
            // face. (contest display format == 1 means page break)
            this.objTest.AddStpBallotContest(1, 2, 2, 1, 2, 3);

            // add a second ballot style
            // ballot style 2
            // contest 1
            // candidate 1
            // candidate 2
            // contest 3
            // candidate 5
            // candidate 6
            this.objTest.AddStpBallotContest(2, 1, 1, 0, 2, 1);

            // add a page break on this contest to place it entirely on another
            // face. At this point, front faces are identical for both ballots
            // back faces are not
            this.objTest.AddStpBallotContest(2, 3, 3, 1, 2, 5);

            // add a third face to test that a 4th empty face is added just for
            // the barcode
            this.objTest.AddStpBallotContest(2, 4, 4, 1, 10, 7);

            // now add header texts for all contests
            // 4 contests, 1 text per contest, first id is 1
            this.objTest.AddStpContBallot(new int[] { 1, 1, 1, 1 }, 1);

            // add candidate texts for all candidates
            // 16 candidates total, first candidate id is 1
            this.objTest.AddStpCandBallot(16, 1);

            // load parameters from DB
            this.objTest.LoadStpParams();
            this.objTest.SetTarget(0.12, 0.08, 0, 0, 0.0035, 100);

            // make sure the barcode location is on the back of the paper
            BallotPdfTestObject.SetIdentifierFace(
                this.objTest.parameters, PaperSide.Back);

            // make sure that at least 1 field is included on the barcode
            BallotPdfTestObject.SetIdentifierMask(this.objTest.parameters, 1);

            // create paper ballot objects for both ballot styles
            PaperBallot ballot1 = new PaperBallot(
                this.objTest.ballots,
                this.objTest.contests,
                this.objTest.candidates,
                this.objTest.parameters,
                this.objTest.target,
                1,
                1,
                1),
                        
                        // for the second ballot, we already have 1 card and 
                        // 2 faces
                        ballot2 = new PaperBallot(
                            this.objTest.ballots,
                            this.objTest.contests,
                            this.objTest.candidates,
                            this.objTest.parameters,
                            this.objTest.target,
                            2,
                            2,
                            3);
            ballot1.Precinct = 1;
            ballot2.Precinct = 2;

            // create a definition binder and add both ballots
            // the definition binder should be smart enough to notice that the
            // first face of both ballot cards are identical, resulting in only
            // 3 faces for 2 cards, 1 card per ballot
            DefinitionBinder binder = new DefinitionBinder();
            binder.AddBallot(ballot1.ExportPositions());
            binder.AddBallot(ballot2.ExportPositions());

            // generated PDFs to manually compare barcodes
            PaperBinder binder2 = new PaperBinder();
            binder2.AddBallot(ballot1);
            binder2.AddBallot(ballot2);
            binder2.Generate(@"..\..\TestData");

            // save files for further inspection
            this.objTest.Serialize(
                @"..\..\TestData\Ballots.B.xml",
                binder.Ballots,
                typeof(BallotList));
            this.objTest.Serialize(
                @"..\..\TestData\Cards.B.xml", binder.Cards, typeof(CardList));
            this.objTest.Serialize(
                @"..\..\TestData\Faces.B.xml", binder.Faces, typeof(FaceList));

            // check the results
            // 3 faces expected, not 4 even though there are 2 cards, 
            // 2 faces each
            Assert.AreEqual(5, binder.Faces.Count);

            // 3 cards, 1 + 2
            Assert.AreEqual(3, binder.Cards.Count);

            // the second face of both cards are the same
            Assert.AreEqual(binder.Cards[0].Faces[1].Id,
                binder.Cards[1].Faces[1].Id);

            // verify face ids
            Assert.AreEqual(1, binder.Cards[0].Faces[0].Id);
            Assert.AreEqual(2, binder.Cards[0].Faces[1].Id);

                // the second card reuses the first face
            Assert.AreEqual(3, binder.Cards[1].Faces[0].Id);
            Assert.AreEqual(2, binder.Cards[1].Faces[1].Id);
            Assert.AreEqual(5, binder.Cards[2].Faces[0].Id);

                // an additional blank face just for the barcode
            Assert.AreEqual(6, binder.Cards[2].Faces[1].Id);

            // verify card ids
            Assert.AreEqual(1, binder.Cards[0].Id);
            Assert.AreEqual(2, binder.Cards[1].Id);
            Assert.AreEqual(3, binder.Cards[2].Id);

            // verify ballot ids
            Assert.AreEqual(1, binder.Ballots[0].Id);
            Assert.AreEqual(2, binder.Ballots[1].Id);
        }

        #endregion
    }
}
