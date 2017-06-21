// -----------------------------------------------------------------------------
// <copyright file="PaperBallotRichTextTemplate.cs" company="Sequoia Voting Systems"> 
//    Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//    Distribution of source code is allowable only under the terms of the
//    license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//    This file contains the PaperBallotRichTextTemplate class.
// </summary>
// <revision revisor="dev11" date="4/10/2009" version="1.0.11.0101">
//    File Created
// </revision>  
// -----------------------------------------------------------------------------

namespace Sequoia.Ballot.Data
{
    #region Using directives

    using System;
    using System.IO;
    using System.Xml;

    using StpParam = Sequoia.Ballot.Data.BallotEntrySet.StpParam;

    #endregion

    /// <summary>
    ///     Class for PaperBallotRichTextTemplate
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev11" date="4/10/2009" version="1.0.11.0101">
    ///     Class created.
    /// </revision>
    public class PaperBallotRichTextTemplate
    {
        #region Constants

        /// <summary>
        ///     Constant for the file name
        /// </summary>
        private const string FileName = "BallotTemplates.xml";

        #endregion
        
        #region Fields

        /// <summary>
        ///     full string of the xml
        /// </summary>
        private string txml;

        /// <summary>
        ///     True if the template xml is well-formed
        /// </summary>
        private bool isValidXml;

        #endregion
        
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="PaperBallotRichTextTemplate"/> class.
        /// </summary>
        /// <param name="txmlFullPath">The TXML full path.</param>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="4/10/2009" version="1.0.11.0101">
        ///     Member Created
        /// </revision>
        public PaperBallotRichTextTemplate(string txmlFullPath)
        {
            try
            {
                // load the xml (TXML) from the file as plain text since this
                // is what AquaPDF's engine uses 
                StreamReader reader = new StreamReader(txmlFullPath);
                this.txml = reader.ReadToEnd();
                reader.Close();

                // parse the file simply to verify it is well-formed XML. No
                // TXML-specific tests are done here on the XML
                // X mlDocument xmlDocument = new XmlDocument();

                // x mlDocument.LoadXml(txml);

                // no exception thrown means that the XML is valid and loaded
                this.isValidXml = true;
            }
            catch (Exception)
            {
                // drop any loaded text
                this.txml = null;

                // set the flag to false
                this.isValidXml = false;
            }
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="PaperBallotRichTextTemplate"/> class.
        /// </summary>
        /// <param name="parameters">The pdf layout parameters.</param>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="4/11/2009" version="1.0.11.0201">
        ///     Member Created
        /// </revision>
        public PaperBallotRichTextTemplate(BallotEntrySet parameters)
        {
            try
            {
                int idx = parameters.FindIndex(
                    StpParam.PDFLayoutParamId,
                    Convert.ToInt32(PdfParam.DataFolder));
                string txmlFullPath = Path.Combine(
                    parameters.GetValueStr(idx, StpParam.ParamValue),
                    FileName);

                // load the xml (TXML) from the file as plain text since this
                // is what AquaPDF's engine uses 
                StreamReader reader = new StreamReader(txmlFullPath);
                this.txml = reader.ReadToEnd();
                reader.Close();

                // parse the file simply to verify it is well-formed XML. No
                // TXML-specific tests are done here on the XML
                // X mlDocument xmlDocument = new XmlDocument();

                // x mlDocument.LoadXml(txml);

                // no exception thrown means that the XML is valid and loaded
                this.isValidXml = true;
            }
            catch
            {
                // drop any loaded text
                this.txml = null;

                // set the flag to false
                this.isValidXml = false;
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets a value indicating whether this instance is valid XML.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is valid XML; 
        ///     otherwise, <c>false</c>.
        /// </value>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="4/10/2009" version="1.0.11.0101">
        ///     Member Created
        /// </revision>
        public bool IsValidXml
        {
            get
            {
                return this.isValidXml;
            }
        }

        /// <summary>
        ///     Gets the TXML.
        /// </summary>
        /// <value>The TXML.</value>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="4/10/2009" version="1.0.11.0101">
        ///     Member Created
        /// </revision>
        public string Txml
        {
            get
            {
                return this.txml;
            }
        }

        #endregion

        #region Public Methods

        #endregion

        #region Public Events

        #endregion

        #region Private Methods

        #endregion
    }
}
