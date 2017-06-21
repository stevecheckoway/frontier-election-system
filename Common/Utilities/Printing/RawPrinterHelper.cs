// -----------------------------------------------------------------------------
// <copyright file="RawPrinterHelper.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the RawPrinterHelper class.
// </summary>
// <revision revisor="dev01" date="1/14/2009" version="1.0.4.15">
//     File Created
// </revision>  
// <revision revisor="dev01" date="1/20/2009" version="1.0.5.1">
//     File modified
// </revision>  
// <revision revisor="dev01" date="1/26/2009" version="1.0.5.7">
//     File modified
// </revision>
// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
//     File modified
// </revision>
// -----------------------------------------------------------------------------

namespace Sequoia.Utilities.Printing
{
    #region Using directives

    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading;

    #endregion

    /// <summary>
    ///     RawPrinterHelper uses winspool.drv to communicate to
    ///     the printer</summary>
    /// <externalUnit/>
    /// <revision revisor="dev01" date="1/14/2009" version="1.0.4.15">
    ///     Class created.
    /// </revision>
    /// <revision revisor="dev01" date="1/26/2009" version="1.0.5.7">
    ///     Changed exposure level and field names
    /// </revision>
    /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
    ///     Formatting changes
    /// </revision>
    public class RawPrinterHelper
    {
        #region Fields

        /// <summary>
        ///     Linefeed Escape Sequence
        /// </summary>
        private const char LineFeed = (char)10;

        /// <summary>
        ///     ESC character
        /// </summary>
        private const char Esc = (char)27;

        /// <summary>
        ///     Escape sequence to select internal font
        /// </summary>
        private const char FontSelector = (char)37;

        /// <summary>
        ///     Inverse video character
        /// </summary>
        private const char InverseVideo = (char)62;

        /// <summary>
        ///     Character one
        /// </summary>
        private const char One = (char)1;

        /// <summary>
        ///     Character zero
        /// </summary>
        private const char Zero = (char)0;

        /// <summary>
        ///     Exclamation character
        /// </summary>
        private const char Exclamation = (char)33;

        /// <summary>
        ///     Quad H Quad W character
        /// </summary>
        private const char QuadHQuadW = (char)6;

        /// <summary>
        ///     Defaults character
        /// </summary>
        private const char Defaults = (char)100;
        
        /// <summary>
        ///     Name of the printer in Windows
        /// </summary>
        private static string printerName = "CP 324 HRS";

        /// <summary>
        ///     Destination in Linux
        /// </summary>
        private static string unixPrintFile = "/dev/usb/lp0";

        #endregion
        
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="RawPrinterHelper"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="1/14/2009" version="1.0.4.15">
        ///     Member Created
        /// </revision>
        public RawPrinterHelper()
        {
        }

        #endregion

        #region Public Properties

        #endregion

        #region Public Methods

        // REVIEW: needs more descriptions of settings and Dll Import... should
        // be listed as an external reference

        /// <summary>
        ///     Write Data to the printer
        /// </summary>
        /// <param name="printerName">Printer Name</param>
        /// <param name="printer">Printer Handle</param>
        /// <param name="pd">Printer Defaults</param>
        /// <returns><c>true</c> if successful</returns>      
        [DllImport(
            "winspool.Drv",
            EntryPoint = "OpenPrinterA",
            SetLastError = true,
            CharSet = CharSet.Ansi,
            ExactSpelling = true,
            CallingConvention = CallingConvention.StdCall)]
        public static extern bool OpenPrinter(
            [MarshalAs(UnmanagedType.LPStr)] string printerName,
            out IntPtr printer,
            IntPtr pd);

        /// <summary>
        ///     Close the connection with the printer
        /// </summary>
        /// <param name="printer">Printer Handle</param>
        /// <returns><c>true</c> if successful</returns>   
        [DllImport(
            "winspool.Drv",
            EntryPoint = "ClosePrinter",
            SetLastError = true,
            ExactSpelling = true,
            CallingConvention = CallingConvention.StdCall)]
        public static extern bool ClosePrinter(IntPtr printer);

        /// <summary>
        ///     Starts the document Printing
        /// </summary>
        /// <param name="printer">Printer Handle</param>
        /// <param name="level">Information Level</param>
        /// <param name="documentInfo">Document Info structure</param>
        /// <returns><c>true</c> if successful</returns>   
        [DllImport(
            "winspool.Drv",
            EntryPoint = "StartDocPrinterA",
            SetLastError = true,
            CharSet = CharSet.Ansi,
            ExactSpelling = true,
            CallingConvention = CallingConvention.StdCall)]
        public static extern bool StartDocPrinter(
            IntPtr printer,
            int level,
            [In, MarshalAs(UnmanagedType.LPStruct)] DOCINFOA documentInfo);

        /// <summary>
        ///     Finalizes the printed document
        /// </summary>
        /// <param name="printer">Printer Handle</param>
        /// <returns><c>true</c> if successful</returns>   
        [DllImport(
            "winspool.Drv",
            EntryPoint = "EndDocPrinter",
            SetLastError = true,
            ExactSpelling = true,
            CallingConvention = CallingConvention.StdCall)]
        public static extern bool EndDocPrinter(IntPtr printer);

        /// <summary>
        ///     Starts Printing
        /// </summary>
        /// <param name="printer">Printer Handle</param>
        /// <returns><c>true</c> if successful</returns>    
        [DllImport(
            "winspool.Drv",
            EntryPoint = "StartPagePrinter",
            SetLastError = true,
            ExactSpelling = true,
            CallingConvention = CallingConvention.StdCall)]
        public static extern bool StartPagePrinter(IntPtr printer);

        /// <summary>
        ///     Finalizes the page printing
        /// </summary>
        /// <param name="printer">Printer Handle</param>
        /// <returns><c>true</c> if successful</returns>     
        [DllImport(
            "winspool.Drv",
            EntryPoint = "EndPagePrinter",
            SetLastError = true,
            ExactSpelling = true,
            CallingConvention = CallingConvention.StdCall)]
        public static extern bool EndPagePrinter(IntPtr printer);

        /// <summary>
        ///     Write Data to the printer
        /// </summary>
        /// <param name="printer">Printer Handle</param>
        /// <param name="bytes">Bytes to Print</param>
        /// <param name="count">Length of the structure</param>
        /// <param name="written">Amount written</param>
        /// <returns><c>true</c> if successful</returns>
        [DllImport(
            "winspool.Drv",
            EntryPoint = "WritePrinter",
            SetLastError = true,
            ExactSpelling = true,
            CallingConvention = CallingConvention.StdCall)]
        public static extern bool WritePrinter(
            IntPtr printer,
            IntPtr bytes,
            int count,
            out int written);

        /// <summary>
        ///     Sends Bytes to printer
        /// </summary>
        /// <param name="printerName">Printer Name</param>
        /// <param name="bytes"> Bytes being sent</param>
        /// <param name="count">Length of the byte array</param>
        /// <returns><c>true</c> if succesful</returns>
        /// <revision revisor="dev01" date="1/14/2009" version="1.0.4.15">
        ///     Member Created
        /// </revision>
        public static bool SendBytesToPrinter(
            string printerName, 
            IntPtr bytes, 
            int count)
        {
            // Error Code
            int error = 0;

            // How many bytes were written
            int written = 0;
            IntPtr printer = new IntPtr(0);
            DOCINFOA di = new DOCINFOA();

            // Assume failure unless you specifically succeed.
            bool success = false;

            di.pDocName = "System IX Reports";
            di.pDataType = "RAW";

            // Open the printer.
            if (OpenPrinter(printerName.Normalize(), out printer, IntPtr.Zero))
            {
                // Start a document.
                if (StartDocPrinter(printer, 1, di))
                {
                    // Start a page.
                    if (StartPagePrinter(printer))
                    {
                        // Write your bytes.
                        success = WritePrinter(
                            printer,
                            bytes,
                            count, 
                            out written);
                        EndPagePrinter(printer);
                    }

                    EndDocPrinter(printer);
                }

                ClosePrinter(printer);
            }

            // If you did not succeed, GetLastError may give more information
            // about why not.
            if (success == false)
            {
                error = Marshal.GetLastWin32Error();
            }

            return success;
        }

        /// <summary>
        ///     Sends String to printer, it decides what method to call 
        ///     depending the  platform
        /// </summary>
        /// <param name="stringToPrint">String to Print</param>
        /// <returns>true if it was successful</returns>
        /// <revision revisor="dev01" date="1/14/2009" version="1.0.4.15">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev01" date="1/17/2009" version="1.0.4.18">
        ///     Added multiple platform support
        /// </revision>
        /// <revision revisor="dev01" date="1/22/2009" version="1.0.5.3">
        ///     Added Footer support for samson prototype
        /// </revision>
        /// <revision revisor="dev01" date="1/26/2009" version="1.0.5.7">
        ///     Changed the reference and calls with printername
        /// </revision> 
        public static bool SendStringToPrinter(
            string stringToPrint)
        {
            // Return Value
            bool returnValue = true;

            // Add about an inch at the end
            string stringWithFooter =
                new string(Esc, 1) +
                new string(FontSelector, 1) +
                new string(One, 1) +
                new string(LineFeed, 1) +
                stringToPrint +
                new string(LineFeed, 10);

            try
            {
                // Decide which platform are we running on
                switch (Environment.OSVersion.Platform)
                {
                    case PlatformID.Unix:
                        returnValue = UnixSendStringtoPrinter(
                            RawPrinterHelper.unixPrintFile,
                            stringWithFooter);
                        break;
                    default:
                        returnValue = Win32NTSendStringToPrinter(
                            RawPrinterHelper.printerName,
                            stringWithFooter);
                        break;
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }

            return returnValue;
        }

        /// <summary>
        ///     Sends string to printer
        /// </summary>
        /// <param name="printerName">Printer name</param>
        /// <param name="stringToPrint">Strin to prinrt</param>
        /// <returns>true if it was successful</returns>
        /// <revision revisor="dev01" date="1/17/2009" version="1.0.4.18">
        ///     Member created (moved code from cref="SendStringToPrinter")
        /// </revision>
        /// <revision revisor="dev01" date="1/26/2009" version="1.0.5.7">
        ///     Changed the printerName var used
        /// </revision> 
        public static bool Win32NTSendStringToPrinter(
            string printerName, string stringToPrint)
        {
            // Container for the byte representation of the string to print
            IntPtr bytes;

            // Lenght of the string to print
            int count;
            
            // Return Value
            bool returnValue = true;

            try
            {
                // How many characters are in the string?
                count = stringToPrint.Length;

                // Assume that the printer is expecting ANSI text, and 
                // then convert the string to ANSI text.
                bytes = Marshal.StringToCoTaskMemAnsi(stringToPrint);

                // Send the converted ANSI string to the printer.
                SendBytesToPrinter(printerName, bytes, count);
                Marshal.FreeCoTaskMem(bytes);
            }
            catch (Exception)
            {
                // TODO: exception needs to be logged
                returnValue = false;
            }

            return returnValue;
        }

        /// <summary>
        ///     Cats to lp.
        /// </summary>
        /// <param name="whatToPrint">The what to print.</param>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="4/21/2009" version="1.0.11.9">
        ///     Member Created
        /// </revision>
        public static void CatToLp(string whatToPrint)
        {
            var filename = Path.GetTempFileName();

            using (var file = File.CreateText(filename))
            {
                file.Write(whatToPrint);
            }

            using (var p = new Process())
            {
                p.StartInfo = new ProcessStartInfo("lp", filename);
                p.Start();
                p.WaitForExit();
                File.Delete(filename);
            }
        }

        /// <summary>
        ///     Sends string to printer file in Linux
        /// </summary>
        /// <param name="printerName">Target printer</param>
        /// <param name="stringToPrint">String to Print</param>
        /// <returns>true if it was successful</returns>
        /// <revision revisor="dev01" date="1/17/2009" version="1.0.4.18">
        ///     Member created
        /// </revision>
        /// <revision revisor="dev01" date="1/20/2009" version="1.0.5.1">
        ///     Added extra inch of printing, and changed writing method
        /// </revision> 
        /// <revision revisor="dev01" date="1/22/2009" version="1.0.5.3">
        ///     Move extra inch of papaer to calling method and added exception
        /// handling
        /// </revision> 
        /// <revision revisor="dev01" date="1/26/2009" version="1.0.5.7">
        ///     Added printer name param
        /// </revision> 
        /// <revision revisor="dev01" date="2/12/2009" version="1.0.6.1">
        ///     Changed implementation for redirecting file
        /// </revision> 
        public static bool UnixSendStringtoPrinter(
            string printerName,
            string stringToPrint)
        {
            // Return Value;
            bool returnValue = true;

            try
            {
                // Add about an inch at the end
                byte[] content = new ASCIIEncoding().GetBytes(
                    stringToPrint);

                // file Stream
                FileStream stream = new FileStream(
                    printerName,
                    FileMode.Truncate,
                    FileAccess.Write,
                    FileShare.Write,
                    content.Length,
                    FileOptions.WriteThrough);

                for (int i = 0; i < content.Length; i++)
                {
                    stream.WriteByte(content[i]);
                }

                // Flush all the data    
                stream.Flush();

                // Close the Stream
                stream.Close();

                // dispose Stream
                stream.Dispose();

                // Pause in case more jobs are in queue
                Thread.Sleep(500);
            }
            catch (Exception exc)
            {
                throw exc;
            }

            return returnValue;
        }
        #endregion

        #region Public Events

        #endregion

        #region Private Methods

        #endregion
     
        #region Structure and API declarions
        /// <summary>
        ///     Imported Marshalled Class from winSpool.drv
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public class DOCINFOA
        {
            /// <summary>
            ///     Document Name
            /// </summary>
            [MarshalAs(UnmanagedType.LPStr)]
            public string pDocName;

            /// <summary>
            ///     OutPut file
            /// </summary>
            [MarshalAs(UnmanagedType.LPStr)]
            public string pOutputFile;

            /// <summary>
            ///     Type of Data (RAW)
            /// </summary>
            [MarshalAs(UnmanagedType.LPStr)]
            public string pDataType;
        }
        #endregion
    }
}
