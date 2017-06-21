// -----------------------------------------------------------------------------
// <copyright file="PrintLibrary.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the PrintLibrary class.
// </summary>
// <revision revisor="dev01" date="4/19/2009" version="1.0.11.9">
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
    using System.Drawing;
    using System.Drawing.Printing;

    #endregion

    /// <summary>
    ///	    PrintLibrary helper class
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev01" date="4/19/2009" version="1.0.11.9">
    ///     Class created.
    /// </revision>
    /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
    ///     Formatting changes.
    /// </revision>
    public class PrintLibrary
    {
        #region Fields

        /// <summary>
        ///     Max characters per line
        /// </summary>
        private const int MaxCharactersPerLine = 40;

        /// <summary>
        ///     Size in inches in report
        /// </summary>
        private const int MaxSizeInInches = 11;

        /// <summary>
        ///     Paper width
        /// </summary>
        private const int PaperWidthInInches = 3;

        /// <summary>
        ///     Lines at the bottom
        /// </summary>
        private const int BottomExtraLines = 10;
        
        /// <summary>
        ///     Index to the line array
        /// </summary>
        private int lineIndex = 0;

        /// <summary>
        ///     Lines to print
        /// </summary>
        private string[] linesToPrint = null;

        /// <summary>
        ///     Font to print reports
        /// </summary>
        private Font tabulatorPrinterFont = new Font("Courier New", 8);

        /// <summary>
        ///     Document that controls printing
        /// </summary>
        private PrintDocument printingDocument = null;

        #endregion
        
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="PrintLibrary"/> class.
        /// </summary>
        /// <param name="lines">The lines to print.</param>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="4/19/2009" version="1.0.11.9">
        ///     Member Created
        /// </revision>
        public PrintLibrary(string[] lines)
        {
            this.linesToPrint = lines;
            this.printingDocument = new PrintDocument();
            this.printingDocument.DefaultPageSettings.Margins.Bottom = 0;
            this.printingDocument.DefaultPageSettings.Margins.Top = 0;
            this.printingDocument.DefaultPageSettings.Margins.Left = 10;
            this.printingDocument.DefaultPageSettings.Margins.Right = 0;
            this.printingDocument.DefaultPageSettings.PaperSize = new PaperSize(
                "Thermal",
                PaperWidthInInches * 100,
                MaxSizeInInches * 100);
            this.printingDocument.PrintController =
                  new StandardPrintController();
            
            this.printingDocument.OriginAtMargins = true;

            this.printingDocument.PrintPage += 
                new PrintPageEventHandler(this.printingDocument_PrintPage);
        }

        #endregion

        #region Public Properties

        #endregion

        #region Public Methods

        /// <summary>
        /// Prints this instance.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="4/21/2009" version="1.0.11.9">
        ///     Member Created
        /// </revision>
        public void Print()
        {
            this.printingDocument.Print();
        }

        #endregion

        #region Public Events

        #endregion

        #region Private Methods

        /// <summary>
        ///     Handles the PrintPage event of the printingDocument control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="arguments">
        ///     The <see cref="System.Drawing.Printing.PrintPageEventArgs"/> 
        ///     instance containing the event data.
        /// </param>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="4/19/2009" version="1.0.11.7">
        ///     Member Created
        /// </revision>
        void printingDocument_PrintPage(
            object sender, 
            PrintPageEventArgs arguments)
        {
            float linesPerPage = 0;
            float yPos = 0;
            int count = 0;
            float leftMargin = arguments.MarginBounds.Left;
            float topMargin = arguments.MarginBounds.Top;

            // Calculate the number of lines per page.
            linesPerPage = this.LinesPerPage(
                this.printingDocument,
                arguments,
                arguments.Graphics,
                this.tabulatorPrinterFont);

            if (lineIndex <= 0)
            {
                arguments.Graphics.DrawLine(
                    new Pen(Brushes.Black, 3f), leftMargin, yPos, 250, yPos);
                count += 3;
            }

            // Print each line of the file.
            while ((count < linesPerPage) &&
               (this.lineIndex < this.linesToPrint.Length))
            {
                string[] paragraph = null;
                yPos = topMargin + (count *
                   this.tabulatorPrinterFont.GetHeight(arguments.Graphics));
                paragraph = BuildParagraphFromLine(linesToPrint[lineIndex]);
                foreach (var lineToPrint in paragraph)
                {
                    arguments.Graphics.DrawString(
                        lineToPrint,
                        this.tabulatorPrinterFont,
                        Brushes.Black,
                        leftMargin,
                        yPos,
                        new StringFormat());
                    count++;
                    yPos = topMargin + (count *
                      this.tabulatorPrinterFont.GetHeight(arguments.Graphics));
                }

                this.lineIndex++;
            }

            // If more lines exist, print another page.
            if (lineIndex < linesToPrint.Length)
            {
                arguments.HasMorePages = true;
            }
            else
            {
                arguments.HasMorePages = false;
                count += BottomExtraLines;
                yPos = topMargin + (count *
                  this.tabulatorPrinterFont.GetHeight(arguments.Graphics));
                arguments.Graphics.DrawLine(
               new Pen(Brushes.Black, 3f), leftMargin, yPos, 250, yPos);
            }
        }

        /// <summary>
        ///     Builds the paragraph from line.
        /// </summary>
        /// <param name="fromLine">From line.</param>
        /// <returns>
        ///     The string containing the paragraph.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="4/20/2009" version="1.0.11.8">
        ///     Member Created
        /// </revision>
        private string[] BuildParagraphFromLine(string fromLine)
        {
            string[] result = null;

            var lines = (int)Math.Ceiling(
               (double)fromLine.Length / MaxCharactersPerLine);

            if (lines <= 1)
            {
                result = new string[1] { fromLine };
            }
            else
            {
                result = new string[lines];
                for (int i = 0; i < lines; i++)
                {
                    int lengthRemaining = MaxCharactersPerLine;
                    if (i == lines - 1)
                    {
                        lengthRemaining =
                            fromLine.Length - (MaxCharactersPerLine * i);
                    }

                    result[i] =
                        fromLine.Substring(
                            i * MaxCharactersPerLine,
                            lengthRemaining);
                }
            }

            return result;
        }

        /// <summary>
        ///     Lines the per page.
        /// </summary>
        /// <param name="printDoc">The print doc.</param>
        /// <param name="arguments">
        ///     The <see cref="System.Drawing.Printing.PrintPageEventArgs"/> 
        ///     instance containing the event data.
        /// </param>
        /// <param name="graphics">The on graphics.</param>
        /// <param name="usingFont">The using font.</param>
        /// <returns>
        ///     The number of lines per page.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="4/19/2009" version="1.0.11.7">
        ///     Member Created
        /// </revision>
        private int LinesPerPage(
            PrintDocument printDoc,
            PrintPageEventArgs arguments,
            Graphics graphics,
            Font usingFont)
        {
            int result = 0;
            if (usingFont != null)
            {
                // Font height in pixels
                int fontHeight = usingFont.Height;

                if (printDoc != null)
                {
                    PrinterResolution printerResolution = null;
                    foreach (PrinterResolution res in
                        printDoc.PrinterSettings.PrinterResolutions)
                    {
                        if ((res.X > 65) &&
                            (res.Y > 65))
                        {
                            printerResolution = res;
                            break;
                        }
                    }

                    if (printerResolution != null)
                    {
                        result = (int)(
                                           MaxSizeInInches
                                           * (double)printerResolution.Y /
                                           usingFont.GetHeight(
                                               printerResolution.Y)
                                       );
                    }
                }

                if ((result == 0) && (arguments != null))
                {
                    if (graphics == null)
                    {
                        result =
                            (int)
                            ((double)arguments.PageBounds.Height
                             / (double)fontHeight);
                    }
                    else
                    {
                        result = (int)((double)arguments.PageBounds.Height /
                            usingFont.GetHeight(graphics));
                    }
                }
            }

            return result;
        }

        #endregion
    }
}
