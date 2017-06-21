// -----------------------------------------------------------------------------
// <copyright file="Pdf.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the Pdf class.
// </summary>
// <revision revisor="dev13" date="11/19/2009" version="1.1.3.7">
//     Added documentation header
// </revision>
// -----------------------------------------------------------------------------

namespace Sequoia.Ems.Interop
{
    #region Using directives

    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;

    using com.thentech.ds.ext;
    using com.thentech.pdf.ext;

    using java.io;

    using Sequoia.Ems.Interop.Exception;

    using File = System.IO.File;

    #endregion

    /// <summary>
    ///     Class for Aqua Pdf processing
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev13" date="11/19/2009" version="1.1.3.7">
    ///     Added documentation header
    /// </revision>
    public class Pdf
    {
        /// <summary>
        ///     License key as provided by the AquaPDF vendor. This key 
        ///     activates the library after being loaded in memory by the class 
        ///     loader.
        /// </summary>
        private const string LicenseKey = 
            "Insert your reference key here";

        /// <summary>
        ///     Encrypts a PDF document using [RC4 128 bit] algorithm and grants
        ///     permission for printing.
        /// </summary>
        /// <param name="strPdfFullPath">The full path for the PDF.</param>
        /// <exception cref="PdfIOException">
        ///     Unable to read/write to the specified file path.
        /// </exception>
        /// <revision revisor="dev13" date="11/19/2009" version="1.1.3.7">
        ///     Added documentation header
        /// </revision>
        public static void Secure(string strPdfFullPath)
        {
            // set the license key before doing anything with the library
            Activate();
            DSxTask task = new DSxTask();
            DSxPDFDocument dsDocument = null;
            try
            {
                // load the existing PDF doc from the file system
                dsDocument = new DSxPDFDocument(
                    task, new DSxInput(strPdfFullPath));

                DSxPDFSecurity dsSec = new DSxPDFSecurity();
                dsSec.setSecurityLevel(DSxPDFSecurity.RC4_128BIT);
                dsSec.setPermission(DSxPDFSecurity.PRINT);
                dsDocument.setSecurity(dsSec);

                File.Delete(strPdfFullPath); // delete unprotected pdf
                dsDocument.save(new DSxOutput(strPdfFullPath));
            }
            catch (DSxRuntimeException)
            {
                // something went wrong while trying to read/write to the 
                // specified path
                throw new PdfIOException();
            }
            finally
            {
                // close and release resources
                if (dsDocument != null)
                {
                    dsDocument.close();
                }

                task.close();
            }
        }

        /// <summary>
        ///     Extracts the pages as separate files.
        /// </summary>
        /// <param name="pdfDoc">The PDF doc.</param>
        /// <param name="strDestinationFolder">
        ///     The STR destination folder.
        /// </param>
        /// <param name="strFilename">The STR filename.</param>
        /// <exception cref="PdfIOException">
        ///     Unable to write to a specified PDF path/filename.
        /// </exception>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="12/22/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public static void ExtractPagesAsSeparateFiles(
            PDFxDocument pdfDoc, 
            string strDestinationFolder, 
            string strFilename)
        {
            int[] pages = new int[pdfDoc.getPageCount()];
            for (int i = 0; i < pages.Length; i = i + 1)
            {
                pages[i] = i;
            }

            ExtractPagesAsSeparateFiles(
                pdfDoc, strDestinationFolder, strFilename, pages);
        }

        /// <summary>
        ///     Extracts the specified pages as separate files.
        /// </summary>
        /// <param name="pdfDoc">The PDF doc.</param>
        /// <param name="strDestinationFolder">
        ///     The STR destination folder.
        /// </param>
        /// <param name="strFilename">The STR filename.</param>
        /// <param name="pages">The pages (0-based).</param>
        /// <exception cref="PdfIOException">
        ///     Unable to write to a specified PDF path/filename.
        /// </exception>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="2/25/2009" version="1.0.8.0801">
        ///     Member Created
        /// </revision>
        public static void ExtractPagesAsSeparateFiles(
            PDFxDocument pdfDoc, 
            string strDestinationFolder, 
            string strFilename, 
            int[] pages)
        {
            if (Directory.Exists(strDestinationFolder) == false)
            {
                // if folder doesn't exist, create it
                Directory.CreateDirectory(strDestinationFolder);
            }

            if (strDestinationFolder.EndsWith(@"\") == false)
            {
                strDestinationFolder = strDestinationFolder + @"\";
            }

            // create a task for this procedure
            DSxTask task = new DSxTask();

            // in order to save the file, a document wrapper is needed
            DSxPDFDocument dsDocument = new DSxPDFDocument(pdfDoc);

            // save the file to a memory stream
            ByteArrayOutputStream os = new ByteArrayOutputStream();
            DSxTDFDocument.save(dsDocument, new DSxOutput(os));
            dsDocument.close();

            try
            {
                string[] filenames = CreatePdfFilenames(

                    // the number of pages is the page number of the last page
                    // plus 1 since page indices are 0-based
                    strFilename, pages[pages.Length - 1] + 1);

                for (int i = 0; i < pages.Length; i++)
                {
                    dsDocument = DSxTDFDocument.getPDF(
                        task,
                        new DSxInput(
                            new ByteArrayInputStream(os.toByteArray())));
                    dsDocument.extractPages(new int[] { pages[i] });
                    dsDocument.save(
                        new DSxOutput(
                            Path.Combine(

                            // i.e.:
                            // pages = {0, 2, 5}
                            //
                            // the page number of the last page is 5, so 
                            // filenames for pages 0-5 have to be generated 
                            // (6 pages total)
                            //
                            // filenames = { [filename 0], [filename 1],
                            //   [filename 2], [filename 3], [filename 4],
                            //   [filename 5] }
                                strDestinationFolder, filenames[pages[i]])));

                    // close the document
                    dsDocument.close();
                }
            }
            catch (DSxRuntimeException)
            {
                throw new PdfIOException();
            }
            finally
            {
                // close the document 
                dsDocument.close();

                // close the task 
                task.close();
            }
        }

        /// <summary>
        ///     Creates the PDF filenames. Each filename is for a different page 
        ///     of a PDF document whose pages are saved to separate files.
        /// </summary>
        /// <param name="strFilename">The STR filename.</param>
        /// <param name="pageCount">The page count.</param>
        /// <returns>
        ///     A string array containing the file names.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="2/25/2009" version="1.0.8.0801">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev11" date="2/28/2009" version="1.0.8.1101">
        ///     name pattern modified
        /// </revision>
        public static string[] CreatePdfFilenames(
            string strFilename, int pageCount)
        {
            string[] names = new string[pageCount];

            if (strFilename.EndsWith(".pdf"))
            {
                // remove file extension if necessary
                strFilename = strFilename.Substring(0, strFilename.Length - 4);
            }

            for (int i = 0; i < pageCount; i = i + 1)
            {
                names[i] = string.Format(
                    "{0}_{1}.pdf",
                    strFilename,

                    // page number (1-based) left padded with a 0 if necessary
                    // i.e.:
                    //   page 0 -> 0 + 1 -> 1 -> 01
                    //   page 1 -> 1 + 1 -> 2 -> 02 
                    //   page 10 -> 10 + 1 -> 11 -> 11
                    (i + 1).ToString().PadLeft(2, '0'));
            }

            return names;
        }

        /// <summary>
        ///     Activates AquaPDF
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="12/22/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public static void Activate()
        {
            DSxSystem.setLicenseKey(LicenseKey);
        }

        /// <summary>
        ///     Gets the PDF417 dims.
        /// </summary>
        /// <param name="maxW">The max Width (in points).</param>
        /// <param name="maxH">The max Height (in points).</param>
        /// <param name="barHeight">Height of the bar (in points).</param>
        /// <returns>
        ///     Maximum number of rows and columns to fit in the specified
        ///     area using the given font size.Width = # of data columns
        ///     Size.Height = # of rows.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="12/31/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public static Size GetPdf417Dims(
            double maxW, double maxH, double barHeight)
        {
            Size dims = new Size();
            double

                // using the font size, get the module width. A module refers to
                // the smallest block of data in a bar code 
                // (it represents a bit)
                // pdf417.02.ttf
                modW = barHeight / 3D,

                // get the maximum number of modules
                modules = Math.Floor(maxW / modW),

                // translate that to data columns. A bar code has a start and 
                // stop CWs, plus 2 row indicators, plus 3 to 30 data columns. 
                // Each column, except for the stop CW, has 17 modules. We need 
                // to get the data columns.
                cols = Math.Floor(((modules - 1.0) / 17.0) - 4),

                // get the module height for a given font size
                // pdf417.02.ttf
                modH = barHeight,

                // get the number of available rows
                rows = Math.Floor(maxH / modH);

            // set columns and rows
            dims.Width = Convert.ToInt32(cols);
            dims.Height = Convert.ToInt32(rows);

            // return those values
            return dims;
        }
    }
}
