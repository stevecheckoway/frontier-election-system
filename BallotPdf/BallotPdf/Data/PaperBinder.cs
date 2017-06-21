// -----------------------------------------------------------------------------
// <copyright file="PaperBinder.cs" company="Sequoia Voting Systems"> 
//    Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//    Distribution of source code is allowable only under the terms of the
//    license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//    This file contains the PaperBinder class.
// </summary>
// <revision revisor="dev11" date="2/25/2009" version="1.0.8.0801">
//    File Created
// </revision>  
// -----------------------------------------------------------------------------

namespace Sequoia.Ballot.Data
{
    #region Using directives

    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Windows.Controls;
    using System.Windows.Threading;

    using Sequoia.Ems.Interop;
    using Sequoia.Utilities;

    #endregion

    /// <summary>
    ///     This class represents a set of paper ballots. By keeping a set of
    ///     ballots together, commonalities can be identified such as common 
    ///     faces optimizing the generation of PDF files
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev11" date="2/25/2009" version="1.0.8.0801">
    ///     Class created.
    /// </revision>
    public class PaperBinder
    {
        #region Fields

        /// <summary>
        ///     Key: [PaperFace] MD5 key
        ///     Value: [PaperBallot] page filename
        ///     This way, when a paper face is found to be a duplicate, it maps 
        ///     to the corresponding PDF filename
        /// </summary>
        private Dictionary<string, string> dicFaces;

        /// <summary>
        ///     Key: a [PaperBallot]
        ///     Value: a list of PDF filenames for EVERY page
        /// </summary>
        private Dictionary<PaperBallot, string[]> dicBallots;

        /// <summary>
        ///     Filename of the CSV report, relative to a specified output folder
        /// </summary>
        private string csvFilename = "Ballots.csv";

        #endregion
        
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="PaperBinder"/> class.
        /// </summary>
        /// <externalUnit cref="dicBallots"/>
        /// <externalUnit cref="dicFaces"/>
        /// <externalUnit cref="Dictionary{TKey,TValue}"/>
        /// <revision revisor="dev11" date="2/25/2009" version="1.0.8.0801">
        ///     Member Created
        /// </revision>
        public PaperBinder()
        {
            this.dicFaces = new Dictionary<string, string>();
            this.dicBallots = new Dictionary<PaperBallot, string[]>();
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Adds a ballot to the binder. This method identifies common 
        ///     faces, so that they can be reused reducing the final set of PDFs
        /// </summary>
        /// <param name="ballot">The ballot.</param>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="2/25/2009" version="1.0.8.0801">
        ///     Member Created
        /// </revision>
        public void AddBallot(PaperBallot ballot)
        {
            // get the faces of this ballot
            List<PaperFace> faces = PaperBallot.GetFaces(ballot);

            // get the filenames for all the faces of the ballot
            PaperFace face;
            string[] files = Pdf.CreatePdfFilenames(
                PaperBallot.GetPdfFilename(ballot),
                faces.Count);
            string md5, filename;

            // for each face, find it in the lookup dictionary. If it is found,
            // it means that an identical face has already been created, so
            // don't include it when generating the PDF files and replace the
            // filename for the exisiting one
            if (this.dicBallots.ContainsKey(ballot) == false)
            {
                // add the ballot to the dictionary along with its set of 
                // file names
                this.dicBallots.Add(ballot, files);
            }

            for (int i = 0; i < faces.Count; i = i + 1)
            {
                // PaperFace <-->  filename
                // faces[0]  <-->  files[0]
                // faces[1]  <-->  files[1]
                // ...
                face = faces[i];
                filename = files[i];
                md5 = BitConverter.ToString(ObjectMD5.Generate(face));
                if (this.dicFaces.ContainsKey(md5) == false)
                {
                    // this is a new face that up to this point no other ballot
                    // has, so add it to the dictionary for future comparison
                    // to subsequent ballots
                    this.dicFaces.Add(md5, filename);
                }
                else
                {
                    // an identical face is already in use, so to the current
                    // ballot assign that face and replace the duplicate
                    files[i] = this.dicFaces[md5];
                }
            }
        }

        /// <summary>
        ///     Generates all PDF files and saves them to the specified folder.
        ///     Also generates the report that tells how to put together the 
        ///     ballots.
        /// </summary>
        /// <param name="outputFolder">The output folder.</param>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="2/26/2009" version="1.0.8.0901">
        ///     Member Created
        /// </revision>
        public void Generate(string outputFolder)
        {
            this.Generate(outputFolder, null);
        }

        /// <summary>
        ///     Generates all PDF files and saves them to the specified folder.
        ///     Also generates the report that tells how to put together the 
        ///     ballots Optionally, updates a progress bar to let the user know 
        ///     the status of the process.
        /// </summary>
        /// <param name="outputFolder">The output folder.</param>
        /// <param name="bar">The progress bar.</param>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="3/6/2009" version="1.0.8.1601">
        ///     Member Created
        /// </revision>
        public void Generate(string outputFolder, ProgressBar bar)
        {
            // generate PDF files
            this.GeneratePdfFiles(outputFolder, bar);

            // generate CSV report
            this.GenerateCsvMap(outputFolder);
        }

        #endregion

        #region Public Methods

        #endregion

        #region Public Events

        #endregion

        #region Private Methods

        /// <summary>
        ///     Generates the PDF files for all ballots.
        /// </summary>
        /// <param name="outputFolder">The output folder.</param>
        /// <param name="bar">The progress bar.</param>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="2/26/2009" version="1.0.8.0901">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev11" date="03/04/2009" version="1.0.8.1501">
        ///     Added progress bar support
        /// </revision>
        private void GeneratePdfFiles(string outputFolder, ProgressBar bar)
        {
            // a given paper ballot
            PaperBallot ballot;

            // the file name of every page (PDF file name)
            string[] files;

            // the pages to print for the current paper ballot
            List<int> pages = new List<int>();

            // set the progress bar if available
            // step is the progress value
            int step = 0;
            if (bar != null)
            {
                // each key corresponds to a ballot that is generated
                ProgressBar_SetMaximum(bar, this.dicBallots.Keys.Count);
            }

            foreach (KeyValuePair<PaperBallot, string[]> pair in 
                this.dicBallots)
            {
                ballot = pair.Key;
                files = pair.Value;
                pages.Clear();
                for (int i = 0; i < files.Length; i = i + 1)
                {
                    if (!File.Exists(Path.Combine(outputFolder, files[i])))
                    {
                        // do not generate the same PDF twice. Each PDF is for
                        // just 1 page
                        pages.Add(i);
                    }
                }

                ballot.Draw(
                    outputFolder,
                    PaperBallot.GetPdfFilename(ballot),
                    pages.ToArray());

                // update progress bar if available
                if (bar != null)
                {
                    step = step + 1;
                    ProgressBar_SetValue(bar, step);
                }
            }
        }

        /// <summary>
        ///     Generates the CSV map.
        /// </summary>
        /// <param name="outputFolder">The output folder.</param>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="2/26/2009" version="1.0.8.0901">
        ///     Member Created
        /// </revision>
        private void GenerateCsvMap(string outputFolder)
        {
            StreamWriter strWriter =
                File.CreateText(Path.Combine(outputFolder, this.csvFilename));

            // a given paper ballot
            PaperBallot ballot;

            // the file name of every page (PDF file name)
            string[] files;

            // write the column headers
            string line = "Ballot,Files";
            strWriter.WriteLine(line);

            // now for each ballot, add a line to the file
            foreach (KeyValuePair<PaperBallot, string[]> pair in 
                this.dicBallots)
            {
                ballot = pair.Key;
                files = pair.Value;

                line = PaperBallot.GetPdfFilename(ballot);

                foreach (string file in files)
                {
                    line = line + "," + file;
                }

                strWriter.WriteLine(line);
            }

            strWriter.Flush();
            strWriter.Close();
        }

        #endregion

        #region Progress bar

        /// <summary>
        ///     A delegate to handle calls to ProgressBar methods from a thread
        ///     other than the UI's.
        /// </summary>
        /// <param name="bar">The progress bar.</param>
        /// <param name="val">The value.</param>
        /// <revision revisor="dev13" date="11/20/2009" version="1.1.3.8">
        ///     Added documentation header
        /// </revision>
        private delegate void ProgressBar_VoidIntHandler(
            ProgressBar bar, double val);

        /// <summary>
        ///     Safely sets the value of a ProgressBar.
        /// </summary>
        /// <param name="bar">The progress bar.</param>
        /// <param name="val">The value.</param>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="3/6/2009" version="1.0.8.1701">
        ///     Member Created
        /// </revision>
        private static void ProgressBar_SetValue(ProgressBar bar, double val)
        {
            if (bar.CheckAccess() == false)
            {
                // calling from a thread other than UI's
                bar.Dispatcher.Invoke(
                    DispatcherPriority.Send,
                    new ProgressBar_VoidIntHandler(ProgressBar_SetValue),
                    bar,
                    val);
            }
            else
            {
                // executing on the UI thread
                bar.Value = val;
            }
        }

        /// <summary>
        ///     Safely sets the maximum value of a progress bar
        /// </summary>
        /// <param name="bar">The progress bar.</param>
        /// <param name="max">The maximum value.</param>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="3/6/2009" version="1.0.8.1701">
        ///     Member Created
        /// </revision>
        private static void ProgressBar_SetMaximum(ProgressBar bar, double max)
        {
            if (bar.CheckAccess() == false)
            {
                // calling from a thread other than UI's
                bar.Dispatcher.Invoke(
                    DispatcherPriority.Send,
                    new ProgressBar_VoidIntHandler(ProgressBar_SetMaximum),
                    bar,
                    max);
            }
            else
            {
                // executing on the UI thread
                bar.Maximum = max;
            }
        }

        #endregion
    }
}
