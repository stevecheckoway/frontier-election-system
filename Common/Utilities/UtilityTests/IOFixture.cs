// -----------------------------------------------------------------------------
// <copyright file="IOFixture.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2008 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the IOFixture test class.
// </summary>
// <revision revisor="dev06" date="11/12/2008" version="1.0.0.0">
//     File Created
// </revision>  
// -----------------------------------------------------------------------------

namespace UtilityTests
{
    #region Using directives

    using System;
    
    using NUnit.Framework;

    using Sequoia.Utilities.IO;

    #endregion

    /// <summary>
    ///	    IOFixture is a test fixture for running tests against IO
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev06" date="11/12/2008" version="1.0.0.0">
    ///     Class created.
    /// </revision>
    [TestFixture]
    public class IOFixture
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
        ///     Handles the UsbKeyRemoved event of the helper control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">
        ///     The <see cref="System.EventArgs"/> instance containing 
        ///     the event data.
        /// </param>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        private void helper_UsbKeyRemoved(object sender, EventArgs e)
        {
            Console.WriteLine("USB Key removed.");
        }

        /// <summary>
        ///     Handles the UsbKeyFound event of the helper control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">
        ///     The <see cref="Sequoia.Utilities.IO.UsbKeyLoadedEventArgs"/> 
        ///     instance containing the event data.
        /// </param>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        private void helper_UsbKeyFound(
            object sender, UsbKeyLoadedEventArgs args)
        {
            Console.WriteLine(
                string.Format(
                    "USB Key loaded. Path is '{0}'", args.PathToUsbKey));
        }

        #endregion
    }
}
