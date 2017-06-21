// -----------------------------------------------------------------------------
// <copyright file="DomainObjectsTests.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2008 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the DomainObjectsTests test class.
// </summary>
// <revision revisor="dev13" date="12/12/2008" version="1.0.0.0">
//     File Created
// </revision>
// <revision revisor = "dev05" date="01/14/09" version="1.0.4.15">
//     Rewrote and extended session number pool test.
// </revision>
// -----------------------------------------------------------------------------

namespace DomainObjectsTests
{
    #region Using directives

    using System;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Xml.Serialization;
    using NUnit.Framework;

    using Sequoia.DomainObjects;
    using Sequoia.DomainObjects.Persistence;

    #endregion

    /// <summary>
    ///     DomainObjectsTests is a test fixture for running tests against 
    ///     the DomainObjects project.
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev13" date="12/12/2008" version="1.0.0.0">
    ///     Class created.
    /// </revision>
    [TestFixture]
    public class DomainObjectsTests
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
        ///     Test that we can create a ballot with an id of 1
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="12/12/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        [Test]
        public void CreateBallotTest()
        {
            Ballot ballot = new Ballot(1);

            Assert.AreEqual(1, ballot.Id);
        }

        /// <summary>
        ///     Test that we can create a card with an id of 1
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="12/12/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        [Test]
        public void CreateCardTest()
        {
            Card card = new Card(1);

            Assert.AreEqual(1, card.Id);
        }

        /// <summary>
        ///     Test that we can create a face
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="12/12/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        [Test]
        public void CreateFaceTest()
        {
            Face face = new Face(1);

            Assert.AreEqual(1, face.Id);
        }

        /// <summary>
        ///     Test that we can create a new mark
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="12/12/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        [Test]
        public void CreateMarkTest()
        {
            Mark mark = new Mark(1);

            mark.LeftX = 50;
            mark.TopY = 50;

            Assert.AreEqual(1, mark.Id);
            Assert.AreEqual(50, mark.LeftX);
            Assert.AreEqual(50, mark.TopY);
        }

        /// <summary>
        ///     Test that we can create a new candidate
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev16" date="12/22/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        [Test]
        public void CreateCandidateTest()
        {
            var candidate = new Candidate(1, "Test Candidate");

            Assert.AreEqual(1, candidate.Id);
            Assert.AreEqual("Test Candidate", candidate.Name);
        }

        /// <summary>
        ///     Test that we can create a new contest
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev16" date="12/22/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        [Test]
        public void CreateContestTest()
        {
            var contest = new Contest(1, 1, "Test Contest");

            Assert.AreEqual(1, contest.Id);
            Assert.AreEqual("Test Contest", contest.Name);
            Assert.AreEqual(1, contest.VoteFor);
        }

        /// <summary>
        ///     Test for contest serialization
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev16" date="12/22/2008" version="1.0.?.0">
        ///     Member created.
        /// </revision>
        [Test]
        public void DeSerializeContestTest()
        {
            var contestList =
                ContestList.FromXml<ContestList>(this.GetContestText());

            Assert.AreEqual(1, contestList.Count);
            Assert.AreEqual(4, contestList[0].Candidates.Count);
            Assert.AreEqual("Straight Party", contestList[0].Name);
            Assert.AreEqual(29, contestList[0].Id);
            Assert.AreEqual(1, contestList[0].VoteFor);
            Assert.AreEqual(98, contestList[0].Candidates[3].Id);
            Assert.AreEqual("New York", contestList[0].Candidates[3].Name);
        }

        /// <summary>
        ///     Test for contest de-serialization
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev16" date="12/22/2008" version="1.0.?.0">
        ///     Member created.
        /// </revision>
        [Test]
        public void SerializeContestTest()
        {
            var contestList =
                ContestList.FromXml<ContestList>(this.GetContestText());
            var xmlRootOverride = new XmlRootAttribute("Contests");

            string contestXml = ContestList.Serialize(
                contestList, xmlRootOverride);

            Assert.AreEqual(this.GetContestText(), contestXml, contestXml);

            var contest = new Contest();
            contest.Id = 1;
            contest.Name = "King";
            contest.VoteFor = 2;

            var newList = new ContestList();
            newList.Add(contest);
            contestXml = ContestList.Serialize(newList, xmlRootOverride);
            Assert.IsNotNull(contestXml);
        }

        /// <summary>
        ///     Test for machine option serialization
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev16" date="12/22/2008" version="1.0.0.0">
        ///     Member created.
        /// </revision>
        [Test]
        public void DeSerializeMachineOptionTest()
        {
            var machineOptionList =
                MachineOptionList.FromXml<MachineOptionList>(
                    this.GetMachineOptionText());

            Assert.AreEqual(5, machineOptionList.Count);
            Assert.AreEqual("OmrThreshold", machineOptionList[0].Name);
            Assert.AreEqual("20", machineOptionList[0].Value);
        }

        /// <summary>
        ///     Test for machine option de-serialization
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev16" date="12/22/2008" version="1.0.0.0">
        ///     Member created.
        /// </revision>
        [Test]
        public void SerializeMachineOptionTest()
        {
            var machineParamList =
                MachineOptionList.FromXml<MachineOptionList>(
                    this.GetMachineOptionText());
            var xmlRootOverride = new XmlRootAttribute("MachineOptions");

            string machineParamXml =
                MachineOptionList.Serialize(machineParamList, xmlRootOverride);

            Assert.AreEqual(
                this.GetMachineOptionText(), machineParamXml, machineParamXml);
        }

        /// <summary>
        ///     Test for machine option de-serialization
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev16" date="12/22/2008" version="1.0.0.0">
        ///     Member created.
        /// </revision>
        [Test]
        public void SerializeMachineParametersTest()
        {
            var machineParameters =
                MachineParameters.FromXml<MachineParameters>(
                    this.GetMachineParametersText());

            Assert.IsNotNull(machineParameters);
            Assert.AreEqual(
                5,
                machineParameters.ParameterFiles.Count,
                "Wrong number of parameter files found.");

            string machineParamXml =
                MachineParameters.Serialize(machineParameters);

            Assert.AreEqual(
                this.GetMachineParametersText(),
                machineParamXml,
                machineParamXml);
        }

        /// <summary>
        ///     For face reduction and optimization, [BallotPdf] project 
        ///     requires [MarkList] as binary serializable. But for that to 
        ///     happen, all super classes have to be marked as well, namely 
        ///     [DomainObjectList] and [DomainObject]. This test ensures 
        ///     that requirement is met.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="2/20/2009" version="1.0.8.0301">
        ///     Member Created
        /// </revision>
        [Test]
        public void MarkListBinarySerializationTest()
        {
            var stream = new MemoryStream();
            var formatter = new BinaryFormatter();

            // create a mark list collection, and populate it with 10 marks
            var marks = new MarkList();
            for (int i = 1; i <= 10; i = i + 1)
            {
                marks.Add(new Mark(i));
            }

            try
            {
                formatter.Serialize(stream, marks);

                // no exception thrown, so pass the test
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                // an exception was thrown, meaning that the serialization 
                // failed so fail the test
                Assert.IsTrue(false);
            }
            finally
            {
                stream.Close();
            }
        }

        /// <summary>
        ///     Gets the parameter value types.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/25/2009" version="1.1.3.12">
        ///     Added documentation header
        /// </revision>
        [Test]
        public void ParameterValueTypeValues()
        {
            var typeValue = new ParameterTypeValue();
            typeValue.Value = "1|2|3";
            string[] values = typeValue.Values;
            Assert.AreEqual(3, values.Length);
        }

        /// <summary>
        ///     Test for election parameter de-serialization
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev16" date="1/10/2009" version="1.0.0.0">
        ///     Member created.
        /// </revision>
        [Test]
        public void DeSerializeElectionParameterTest()
        {
            var electionParameterList =
                ElectionParameterList.FromXml<ElectionParameterList>(
                    this.GetElectionParametersText());

            Assert.AreEqual(6, electionParameterList.Count);
            Assert.AreEqual("ElectionName", electionParameterList[0].Name);
            Assert.AreEqual("Philippines Demo", electionParameterList[0].Value);
        }

        /// <summary>
        ///     Test for election parameter serialization
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev16" date="1/10/2009" version="1.0.0.0">
        ///     Member created.
        /// </revision>
        [Test]
        public void SerializeElectionParameterTest()
        {
            var electionParamList =
                ElectionParameterList.FromXml<ElectionParameterList>(
                    this.GetElectionParametersText());
            var xmlRootOverride = new XmlRootAttribute("ElectionParameters");

            string electionParamXml =
                ElectionParameterList.Serialize(
                    electionParamList, xmlRootOverride);

            Assert.AreEqual(
                this.GetElectionParametersText(),
                electionParamXml,
                electionParamXml);
        }

        /// <summary>
        ///     Test SessionNumberPool, to see if the period equals the length
        ///     of the range.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor = "dev05" date="01/14/09" version="1.0.4.15">
        ///     Use a helper method, and test multiple ranges.
        /// </revision>
        /// <revision revisor="dev05" date="01/12/09" version="1.0.4.13">
        ///     Method created.
        /// </revision>
        [Test]
        public void SessionNumberPoolTest()
        {
            this.TestPoolPeriod(23, 234);
            this.TestPoolPeriod(1, 365);
            this.TestPoolPeriod(1, 508);
            this.TestPoolPeriod(1, 509);
            this.TestPoolPeriod(1, 510);
            this.TestPoolPeriod(1234, 4321);
            this.TestPoolPeriod(10001, 20000);

            // Test power of 2 range lengths, to exercise different moduli in
            // the LCG. We can go to 31 in the loop, but that takes a long
            // time...
            for (int i = 11; i < 26; i++)
            {
                this.TestPoolPeriod(1, 1 << i);
            }
        }

        #endregion

        #region Private helper methods

        /// <summary>
        ///     Gets the ballot text.
        /// </summary>
        /// <returns>a string containing a mock ballots.xml file.</returns>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="12/12/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        private string GetBallotText()
        {
            string ballotText = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>"
                + "\r\n<Ballots "
                + "xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" "
                + "xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" count=\"1\">"
                + "\r\n  <Ballot Id=\"1\">"
                + "\r\n    <Cards count=\"1\">"
                + "\r\n      <Card Id=\"1\" Order=\"1\" />" 
                + "\r\n    </Cards>"
                + "\r\n  </Ballot>"
                + "\r\n</Ballots>";

            return ballotText;
        }

        /// <summary>
        ///     Gets the card text.
        /// </summary>
        /// <returns>a string with a mock cards.xml file</returns>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="12/12/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        private string GetCardText()
        {
            string cardText = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>"
                + "\r\n<Cards "
                + "xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" "
                + "xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" count=\"2\">"
                + "\r\n  <Card Id=\"1\">"
                + "\r\n    <Faces count=\"2\">"
                + "\r\n      <Face Id=\"1\" Order=\"1\" />"
                + "\r\n      <Face Id=\"2\" Order=\"2\" />" 
                + "\r\n    </Faces>"
                + "\r\n  </Card>"
                + "\r\n</Cards>";

            return cardText;
        }

        /// <summary>
        ///     Gets the face text.
        /// </summary>
        /// <returns>a string with a mock faces.xml file</returns>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="12/12/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        private string GetFaceText()
        {
            string faceText = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>"
                + "<Faces "
                + "xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" "
                + "xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" count=\"2\">"
                + "<Face Id=\"1\">"
                + "<Marks count=\"2\">"
                + "<Mark Id=\"1\" LeftX=\"50\" TopY=\"50\" />"
                + "<Mark Id=\"2\" LeftX=\"50\" TopY=\"100\" />"
                + "</Marks>"
                + "</Face>"
                + "</Faces>";

            return faceText;
        }

        /// <summary>
        ///     Test a given range for the session number pool.
        /// </summary>
        /// <param name="start">The starting number.</param>
        /// <param name="end">The ending number.</param>
        /// <externalUnit/>
        /// <revision revisor="dev05" date="01/14/09" version="1.0.4.15">
        ///     Method created.
        /// </revision>
        private void TestPoolPeriod(long start, long end)
        {
            int length = (int)(end - start + 1);   // Range length
            int i = 0;                              // Loop index
            long number = 0;                        // Current generated number
            long firstNumber = 0;                   // First generated number
            var snp = new SessionNumberPool(start, end);    // Pool object

            // Generate numbers for the whole range. The first number should
            // reappear the very last time through the loop.
            for (i = 0; i <= length; i++)
            {
                number = snp.Next();
                if (i == 0)
                {
                    firstNumber = number;
                }
                else if (number == firstNumber)
                {
                    break;
                }
            }

            Assert.AreEqual(length, i);
        }

        /// <summary>
        ///     Gets the contest text.
        /// </summary>
        /// <returns>a string containing a mock contest.xml file.</returns>
        /// <externalUnit/>
        /// <revision revisor="dev16" date="12/22/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        private string GetContestText()
        {
            string contestText;
            using (var reader = new StreamReader(
                    Path.Combine(
                        Environment.CurrentDirectory, 
                        "TestFiles\\Contest.xml")))
            {
                contestText = reader.ReadToEnd();
            }

            return contestText;
        }

        /// <summary>
        ///     Gets the machine option text.
        /// </summary>
        /// <returns>
        ///     A string containing a mock machine option .xml file
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev16" date="12/22/2008" version="1.0.0.0">
        ///     Member created.
        /// </revision>
        private string GetMachineOptionText()
        {
            string machineOptionText;
            using (var reader = new StreamReader(
                Path.Combine(
                    Environment.CurrentDirectory, 
                    "TestFiles\\MachineOptionTest.xml")))
            {
                machineOptionText = reader.ReadToEnd();
            }

            return machineOptionText;
        }

        /// <summary>
        ///     Gets the machine parameters text.
        /// </summary>
        /// <returns>
        ///     A string containing a mock machine parameters file.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev16" date="12/22/2008" version="1.0.0.0">
        ///     Member created.
        /// </revision>
        private string GetMachineParametersText()
        {
            string machineParametersText;
            using (var reader = new StreamReader(
                Path.Combine(
                    Environment.CurrentDirectory, 
                    "TestFiles\\MachineParametersTest.xml")))
            {
                machineParametersText = reader.ReadToEnd();
            }

            return machineParametersText;
        }
        #endregion

        /// <summary>
        ///     Gets the Election Parameter text.
        /// </summary>
        /// <returns>
        ///     A string containing a mock election parameters file.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev16" date="1/10/2009" version="1.0.0.0">
        ///     Member created.
        /// </revision>
        private string GetElectionParametersText()
        {
            string electionParameterText;
            using (var reader = new StreamReader(
                Path.Combine(
                    Environment.CurrentDirectory, 
                    "TestFiles\\ElectionParametersTest.xml")))
            {
                electionParameterText = reader.ReadToEnd();
            }

            return electionParameterText;
        }
    }
}
