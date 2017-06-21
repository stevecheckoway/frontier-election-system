// -----------------------------------------------------------------------------
// <copyright file="EnumTextAttributeTests.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2008 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the Main class.
// </summary>
// <revision revisor="dev11" date="10/13/2008" version="1.0.0.0">
//     File Created
// </revision>  
// -----------------------------------------------------------------------------

namespace UnitTests
{
    #region Using directives

    using System;
    using NUnit.Framework;

    using Sequoia.Utilities;

    #endregion

    /// <summary>
    ///	    enum text attribute tests
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev11" date="10/13/2008" version="1.0.0.0">
    ///     Class created.
    /// </revision>
    [TestFixture]
    public class EnumTextAttributeTests
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="EnumTextAttributeTests"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="10/13/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>	
        public EnumTextAttributeTests()
        {
        }

        #endregion

        #region Public Properties

        #endregion

        #region Public Methods

        /// <summary>
        ///     Sets up the enum tests.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        [SetUp]
        public void SetupEnumTests()
        {
            /* by calling GetText first, the hashtable is populated for the 
             * tests */
            EnumTest myEnum = EnumTest.EnumTypeWithTextAttribute;
            string myText = EnumTextAttribute.GetText(myEnum);
        }

        /// <summary>
        ///     Tests EnumTextAttribute.GetText method
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="10/14/2008" version="1.0.?.0">
        ///     Added documentation header
        /// </revision>
        [Test]
        public void GetEnumText()
        {
            EnumTest myEnum = EnumTest.EnumTypeWithTextAttribute;
            string myText = EnumTextAttribute.GetText(myEnum);
            Assert.AreEqual("Testing Enum Text Attributes", myText);

            myEnum = EnumTest.EnumTypeWithNoTextAttribute;
            myText = EnumTextAttribute.GetText(myEnum);
            Assert.IsNull(myText);
        }

        /// <summary>
        ///     Tests EnumTextAttribute.GetValue method
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="10/14/2008" version="1.0.?.0">
        ///     Member Created
        /// </revision>
        [Test]
        public void GetEnumValue()
        {
            string myText = "Testing Enum Text Attributes";
            Enum myEnum = EnumTextAttribute.GetValue(myText);
            Assert.AreEqual(myEnum, EnumTest.EnumTypeWithTextAttribute);

            myEnum = EnumTextAttribute.GetValue("some text here");
            Assert.IsNull(myEnum);

            myEnum = EnumTextAttribute.GetValue("Another text here");
            Assert.IsNull(myEnum);

            EnumTextAttribute.GetText(EnumTest.EnumTypeWithAnotherText);
            myEnum = EnumTextAttribute.GetValue("Another text here");
            Assert.IsNotNull(myEnum);
            Assert.AreNotEqual(myEnum, EnumTest.EnumTypeWithTextAttribute);
            Assert.AreNotEqual(myEnum, EnumTest.EnumTypeWithNoTextAttribute);
            Assert.AreEqual(myEnum, EnumTest.EnumTypeWithAnotherText);
        }

        #endregion

        #region Public Events

        #endregion

        #region Fields

        #endregion

        #region Private Methods

        #endregion
    }
}
