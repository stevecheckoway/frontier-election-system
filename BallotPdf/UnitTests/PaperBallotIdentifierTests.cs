// -----------------------------------------------------------------------------
// <copyright file="PaperBallotIdentifierTests.cs" 
//    company="Sequoia Voting Systems"> 
//    Copyright (c) 2008 Sequoia Voting Systems, Inc. All Rights Reserved.
//    Distribution of source code is allowable only under the terms of the
//    license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//    This file contains the PaperBallotIdentifierTests test class.
// </summary>
// <revision revisor="dev11" date="12/24/2008" version="1.0.0.0">
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

    using NUnit.Framework;

    using Sequoia.Ballot;
    using Sequoia.Ballot.Data;
    using Sequoia.Utilities;

    #endregion

    /// <summary>
    ///     PaperBallotIdentifierTests is a test fixture for running tests 
    ///     against the paper ballot identifier
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev11" date="12/24/2008" version="1.0.0.0">
    ///     Class created.
    /// </revision>
    /// <revision revisor="dev13" date="11/20/2009" version="1.1.3.8">
    ///     Formatting changes
    /// </revision>
    [TestFixture]
    public class PaperBallotIdentifierTests
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
            IdField[] fields = (IdField[]) Enum.GetValues(typeof(IdField));
            foreach (IdField field in fields)
            {
                EnumTextAttribute.GetText(field);
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
        ///     Builds a string with all possible fields, then parses it 
        ///     creating a new PaperBallotIdentifier instance, then gets every 
        ///     field and compares the value. Also, sets a new value for each 
        ///     field and then gets it to verify it was set properly.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="12/29/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        [Test]
        public void ParseOk_Get_SetGet()
        {
            IdField[] fields = (IdField[]) Enum.GetValues(typeof(IdField));
            string barcode = string.Empty;

            // builds a barcode string with all fields
            for (int i = 0; i < fields.Length; i++)
            {
                if (i > 0)
                {
                    barcode += PaperBallotIdentifier.SepMajor;
                }

                barcode += EnumTextAttribute.GetText(fields[i])
                           + PaperBallotIdentifier.SepMinor + i;
            }

            // parse the barcode string and build a new paper ballot identifier
            // instance
            PaperBallotIdentifier objId = PaperBallotIdentifier.Parse(barcode);

            // get each field and verify it was properly parsed
            for (int i = 0; i < fields.Length; i++)
            {
                Assert.AreEqual(objId.Get(fields[i]), i.ToString());
            }

            // set a new value to each field and verify again
            for (int i = 0, val; i < fields.Length; i++)
            {
                val = fields.Length + i;
                objId.Set(fields[i], val);
                Assert.AreEqual(objId.Get(fields[i]), val);
            }
        }

        /// <summary>
        ///     Tests <see cref="PaperBallotIdentifier.Parse"/> with bad strings
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="2/6/2009" version="1.0.6.2">
        ///     Member Created
        /// </revision>
        [Test]
        public void ParseCatchesException()
        {
            string barcode = "badidentifier" + PaperBallotIdentifier.SepMinor
                             + "something";

            PaperBallotIdentifier identifier =
                PaperBallotIdentifier.Parse(barcode);

            // no fields
            Assert.IsFalse(identifier.HasFields);

            // 1 field ok, 1 not ok (discarded)
            barcode = string.Format(
                barcode + "{0}{1}{2}{3}",
                PaperBallotIdentifier.SepMajor,
                EnumTextAttribute.GetText(IdField.Precinct),
                PaperBallotIdentifier.SepMinor,
                1);
            identifier = PaperBallotIdentifier.Parse(barcode);
            Assert.IsTrue(identifier.HasFields);

            // no fields either
            barcode = "sometext";
            identifier = PaperBallotIdentifier.Parse(barcode);
            Assert.IsFalse(identifier.HasFields);
        }

        /// <summary>
        ///     Tests <see cref="PaperBallotIdentifier.ToString"/>
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="2/6/2009" version="1.0.6.2">
        ///     Member Created
        /// </revision>
        [Test]
        public void ToStringTest()
        {
            string barcode = string.Format(
                "{0}{1}{2}{3}{4}{1}{5}",
                EnumTextAttribute.GetText(IdField.Language),
                PaperBallotIdentifier.SepMinor,
                1,
                PaperBallotIdentifier.SepMajor,
                EnumTextAttribute.GetText(IdField.Code),
                2);

            PaperBallotIdentifier identifier =
                PaperBallotIdentifier.Parse(barcode);

            string toString = identifier.ToString(),
                   field1 = string.Format(
                       "{0}{1}{2}",
                       EnumTextAttribute.GetText(IdField.Language),
                       PaperBallotIdentifier.SepMinor,
                       1),
                   field2 = string.Format(
                       "{0}{1}{2}",
                       EnumTextAttribute.GetText(IdField.Code),
                       PaperBallotIdentifier.SepMinor,
                       2);

            Assert.IsTrue(toString.IndexOf(field1) > -1);
            Assert.IsTrue(toString.IndexOf(field2) > -1);

            identifier = new PaperBallotIdentifier();
            Assert.AreEqual(string.Empty, identifier.ToString());
        }

        /// <summary>
        ///     Tests <see cref="PaperBallotIdentifier.ToString"/>
        ///     Note: as a current test requirement, only Language and Code 
        ///     fields are being used on the ToString method. This will not be 
        ///     the final behavior.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="2/6/2009" version="1.0.6.2">
        ///     Member Created
        /// </revision>
        [Test]
        public void ToStringFailsWithoutCodeAndLanguageFieldsTest()
        {
            string barcode = string.Format(
                "{0}{1}{2}{3}{4}{1}{5}",
                EnumTextAttribute.GetText(IdField.BallotTypeId),
                PaperBallotIdentifier.SepMinor,
                1,
                PaperBallotIdentifier.SepMajor,
                EnumTextAttribute.GetText(IdField.Precinct),
                2);

            PaperBallotIdentifier identifier =
                PaperBallotIdentifier.Parse(barcode);

            string toString = identifier.ToString(),
                   field1 = string.Format(
                       "{0}{1}{2}",
                       EnumTextAttribute.GetText(IdField.BallotTypeId),
                       PaperBallotIdentifier.SepMinor,
                       1),
                   field2 = string.Format(
                       "{0}{1}{2}",
                       EnumTextAttribute.GetText(IdField.Precinct),
                       PaperBallotIdentifier.SepMinor,
                       2);

            Assert.IsTrue(toString.IndexOf(field1) > -1);
            Assert.IsTrue(toString.IndexOf(field2) > -1);
        }

        /// <summary>
        ///     Tests <see cref="PaperBallotIdentifier.ToString"/> keeps
        ///     the enum type field order.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/20/2009" version="1.1.3.8">
        ///     Added documentation header
        /// </revision>
        [Test]
        public void ToStringKeepsEnumTypeFieldOrderTest()
        {
            string barcode = "CRD:4|BLT:2|LNG:EN|PCT:4|BLS:9|CDE:10";
            PaperBallotIdentifier identifier =
                PaperBallotIdentifier.Parse(barcode);

            string toString = identifier.ToString();

            Assert.AreNotEqual(barcode, toString);
        }
        #endregion
    }
}
