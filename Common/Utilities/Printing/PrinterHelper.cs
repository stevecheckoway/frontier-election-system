// -----------------------------------------------------------------------------
// <copyright file="PrinterHelper.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the PrinterHelper class.
// </summary>
// <revision revisor="dev01" date="4/19/2009" version="1.0.11.7">
//     File Created
// </revision>
// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
//     File modified
// </revision>
// -----------------------------------------------------------------------------

namespace Sequoia.Utilities.Printing
{
    #region Using directives

    using System;
    using System.Configuration;

    #endregion

    /// <summary>
    ///     printing method enum
    /// </summary>
    public enum PrintingMethod
    {
        /// <summary>
        ///     direct raw
        /// </summary>
        DIRECT_RAW = 0,

        /// <summary>
        ///     lp raw method
        /// </summary>
        LP_RAW     = 1,

        /// <summary>
        ///     ms api method
        /// </summary>
        MS_API     = 2
    }

    /// <summary>
    ///	    PrinterHelper class
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev01" date="4/19/2009" version="1.0.11.7">
    ///     Class created.
    /// </revision>
    /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
    ///     Formatting changes
    /// </revision>
    public class PrinterHelper
    {
        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="PrinterHelper"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="4/19/2009" version="1.0.11.7">
        ///     Member Created
        /// </revision>	
        public PrinterHelper()
        {
        }

        #endregion

        #region Public Properties

        #endregion

        #region Public Methods

        /// <summary>
        ///     Sends the string to printer.
        /// </summary>
        /// <param name="stringToPrint">The string to print.</param>
        /// <returns>
        ///     <c>true</c> if string was sent to printer; 
        ///     otherwise, <c>false</c>.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="4/19/2009" version="1.0.11.7">
        ///     Member Created
        /// </revision>
        public static bool SendStringToPrinter(
            string stringToPrint)
        {
            int printMethod  = 0;
            try
            {
                printMethod =
                    int.Parse(
                        ConfigurationManager.AppSettings["PrintingMethod"]);
            }
            catch
            {
            }

            bool returnValue = true;

            switch((PrintingMethod)printMethod)
            {
                case PrintingMethod.DIRECT_RAW:
                    RawPrinterHelper.SendStringToPrinter(stringToPrint);
                    break;
                case PrintingMethod.LP_RAW:
                    RawPrinterHelper.CatToLp(stringToPrint);
                    break;
                default:
                    string[] reportLines =
                        stringToPrint.Split(
                        new string[] { Environment.NewLine },
                        StringSplitOptions.None);
                    PrintLibrary pl = new PrintLibrary(reportLines);
                    pl.Print();
                    break;
            }

            return returnValue;
        }

        #endregion

        #region Public Events

        #endregion

        #region Private Methods

        #endregion
    }
}
