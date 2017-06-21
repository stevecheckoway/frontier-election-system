// -----------------------------------------------------------------------------
// <copyright file="OfficeTextTests.cs" company="Sequoia Voting Systems"> 
//    Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//    Distribution of source code is allowable only under the terms of the
//    license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//    This file contains the OfficeTextTests test class.
// </summary>
// <revision revisor="dev11" date="2/6/2009" version="1.0.0.0">
//    File Created
// </revision>  
// -----------------------------------------------------------------------------

namespace UnitTests
{
    #region Using directives

    using System;
    using System.Xml;
    using NUnit.Framework;

    using Sequoia.Ballot.Data;

    using StpCont = Sequoia.Ballot.Data.BallotEntrySet.StpCont;

    #endregion

    /// <summary>
    ///     OfficeTextTests is a test fixture for running tests against the 
    ///     OfficeText.
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev11" date="2/6/2009 2:14:09 PM" version="1.0.?.0">
    ///     Class created.
    /// </revision>
    [TestFixture]
    public class OfficeTextTests
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
        ///     Tests <see cref="OfficeText.LoadFromXml"/> method
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="2/6/2009" version="1.0.6.2">
        ///     Member Created
        /// </revision>
        [Test]
        public void LoadFromXmlTest()
        {
            string text = "Some Office Text";
            string strXml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
                "<root><officetext>" + text + "</officetext></root>";
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(strXml);

            XmlNode officeTextNode = xmlDoc.LastChild.FirstChild;
            OfficeText officeText = OfficeText.LoadFromXml(
                officeTextNode, "MyFont", 23);

            Assert.AreEqual(officeText.Text, text);
            Assert.AreEqual(officeText.Font, "MyFont");
            Assert.AreEqual(officeText.FontSize, 23);

            strXml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
                     "<root><officetext font=\"YourFont\" font_size=\"7\">"
                     + text + "</officetext></root>";

            xmlDoc.LoadXml(strXml);
            officeTextNode = xmlDoc.LastChild.FirstChild;
            officeText = OfficeText.LoadFromXml(officeTextNode, "MyFont", 23);

            Assert.AreEqual(officeText.Text, text);
            Assert.AreEqual(officeText.Font, "YourFont");
            Assert.AreEqual(officeText.FontSize, 7);
        }

        /// <summary>
        ///     Tests <see cref="OfficeText.LoadFromDb"/> method
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="2/6/2009" version="1.0.6.2">
        ///     Member Created
        /// </revision>
        [Test]
        public void LoadFromDbTest()
        {
            string text = "Some Office Text";
            BallotEntrySet entrySet = new BallotEntrySet(typeof(StpCont));
            entrySet.Entries.Add(new object[] { 5, text, 1 });

            OfficeText officeText = OfficeText.LoadFromDb(
                entrySet, 0, "MyFont", 23);

            Assert.AreEqual(officeText.Text, text);
            Assert.AreEqual(officeText.Font, "MyFont");
            Assert.AreEqual(officeText.FontSize, 23);
        }

        #endregion
    }
}
