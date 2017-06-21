// -----------------------------------------------------------------------------
// <copyright file="MessageBitmapTests.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the MessageBitmapTests test class.
// </summary>
// <revision revisor="dev11" date="2/11/2009" version="1.0.6.7">
//     File Created
// </revision>
// <revision revisor="dev13" date="11/19/2009" version="1.1.3.7">
//     File modified
// </revision>  
// -----------------------------------------------------------------------------

namespace Tester
{
    #region Using directives

    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using NUnit.Framework;

    using Sequoia.Ems.Imaging;
    using Sequoia.Ems.Imaging.Exception;

    #endregion

    /// <summary>
    ///	    MessageBitmapTests is a test fixture for running tests 
    ///     against the message bitmap
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev11" date="2/11/2009" version="1.0.6.7">
    ///     Class created.
    /// </revision>
    /// <revision revisor="dev13" date="11/19/2009" version="1.1.3.7">
    ///     Formatting changes.
    /// </revision>
    [TestFixture]
    public class MessageBitmapTests
    {
        #region Fixture Setup
        /// <summary>
        ///     his method runs once for the entire test fixture. Place any 
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
        ///     Tests drawing.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/19/2009" version="1.1.3.7">
        ///     Added documentation header
        /// </revision>
        [Test]
        public void TestDraw()
        {
            // test all digits
            Bitmap bitmap = MessageBitmap.Draw(1234567890, 0.125F, 96);
            bitmap.Save("Invalid 1234567890.bmp", ImageFormat.Bmp);
            Assert.IsTrue(true);

            // test negative numbers
            bitmap = MessageBitmap.Draw(-1, 0.125F, 96);
            bitmap.Save("Invalid -1.bmp", ImageFormat.Bmp);
            Assert.IsTrue(true);

            // test special characters
            try
            {
                bitmap = MessageBitmap.Draw(@"ית", 0.125F, 96, 10, 10);
                bitmap.Save("Invalid special chars.bmp", ImageFormat.Bmp);

                // an exception has to be thrown, otherwise, the test failed
                // therefore a true assertion on false
                Assert.IsTrue(false);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is CharacterNotSupportedException);
            }

            // test margins
            bitmap = MessageBitmap.Draw("123", 0.125F, 96, 7, 5);
            bitmap.Save("Invalid margins.bmp", ImageFormat.Bmp);
            Assert.IsTrue(true);
        }

        #endregion
    }
}
