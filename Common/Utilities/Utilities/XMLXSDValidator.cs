// -----------------------------------------------------------------------------
// <copyright file="XMLXSDValidator.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the XMLXSDValidator class.
// </summary>
// <revision revisor="dev01" date="1/8/2009" version="1.0.4.9">
//     File Created
// </revision>  
// <revision revisor="dev01" date="1/12/2009" version="1.0.4.9">
//     Added extra Constructors
// </revision>  
// <revision revisor="dev01" date="1/21/2009" version="1.0.5.2">
//     File modified
// </revision>
// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
//     File modified
// </revision>
// -----------------------------------------------------------------------------

namespace Sequoia.Utilities
{
    #region Using directives

    using System;
    using System.Collections;
    using System.IO;
    using System.Xml;
    using System.Xml.Schema;

    #endregion

    /// <summary>
    ///     XMLXSDValidator Lets you validate several xml files 
    ///     against several schemas
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev01" date="1/8/2009" version="1.0.4.9">
    ///     Class created.
    /// </revision>
    /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
    ///     Formatting changes.
    /// </revision>
    public class XMLXSDValidator
    {
        #region Fields

        /// <summary>
        ///     Global return Value for Validate method
        /// </summary>
        private bool xmlIsValid = true;

        /// <summary>
        ///     List of Schemas needed to validate the xmls files
        /// </summary>
        private XmlSchemaSet schemaCollection = null;

        /// <summary>
        ///     Files to be validated against Schemas
        /// </summary>
        private XmlReader[] xmlDocsToValidate = null;

        /// <summary>
        ///     List of the paths in case no xmlreader objects present
        /// </summary>
        private string[] xmlPathList = null;

        /// <summary>
        ///     List of the paths in case no XMLSchemaset object present
        /// </summary>
        private string[] xsdPathList = null;

        /// <summary>
        ///     Repository for the Validation Errors
        /// </summary>
        private ArrayList validationErrors = new ArrayList();

        /// <summary>
        ///     Repository for the Validation Warnings
        /// </summary>
        private ArrayList validationWarnings = new ArrayList();

        /// <summary>
        ///     Settings for the validation
        /// </summary>
        private XmlReaderSettings settingsToValidate = null;

        /// <summary>
        ///     Fails the validation if warnings are encountered
        /// </summary>
        private bool failValidationOnWarning = false;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="XMLXSDValidator"/> class.
        /// </summary>
        /// <param name="pathToXML">Path to the XML to validate</param>
        /// <param name="pathToXSD">
        ///     Path to the Schema to validate the XML
        /// </param>
        /// <param name="breakOnWarning">
        ///     Sets the object to fail the validation on warnings
        /// </param>
        /// <revision revisor="dev01" date="1/8/2009" version="1.0.4.9">
        ///     Member Created
        /// </revision>
        public XMLXSDValidator(
            string pathToXML,
            string pathToXSD,
            bool breakOnWarning)
            : this(
            new[] { pathToXML },
            new[] { pathToXSD },
            breakOnWarning)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="XMLXSDValidator"/> class.
        /// </summary>
        /// <param name="pathsToXML">List of paths to XMLs to validate</param>
        /// <param name="pathsToXSD">
        ///     List of paths to Schemas to validate the XMLs
        /// </param>
        /// <param name="breakOnWarning">
        ///     Sets the object to fail the validation on warnings
        /// </param>
        /// <revision revisor="dev01" date="1/8/2009" version="1.0.4.9">
        ///     Member Created
        /// </revision>
        public XMLXSDValidator(
            string[] pathsToXML,
            string[] pathsToXSD,
            bool breakOnWarning)
            : this(breakOnWarning)
        {
            this.xmlPathList = pathsToXML;
            this.xsdPathList = pathsToXSD;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="XMLXSDValidator"/> class.
        /// </summary>
        /// <param name="xml">XML document to validate</param>
        /// <param name="pathsToXSD">
        ///     List of paths to Schemas to validate the XMLs
        /// </param>
        /// <param name="breakOnWarning">Sets the object to fail the validation 
        /// on warnings</param>
        /// <revision revisor="dev01" date="1/8/2009" version="1.0.4.9">
        ///     Member Created
        /// </revision>
        public XMLXSDValidator(
            XmlReader xml,
            string[] pathsToXSD,
            bool breakOnWarning)
            : this(breakOnWarning)
        {
            this.xmlDocsToValidate = new XmlReader[] { xml };
            this.xsdPathList = pathsToXSD;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="XMLXSDValidator"/> class.
        /// </summary>
         /// <param name="xmls">List of documents to validate</param>
        /// <param name="schemas">List of Schemas to validate against</param>
        /// <param name="breakOnWarning">
        ///     Sets the object to fail the validation on warnings
        /// </param>
        /// <revision revisor="dev01" date="1/8/2009" version="1.0.4.9">
        ///     Member Created
        /// </revision>
        public XMLXSDValidator(
            XmlReader[] xmls,
            XmlSchemaSet schemas,
            bool breakOnWarning)
            : this(breakOnWarning)
        {
            this.xmlDocsToValidate = xmls;
            this.schemaCollection = schemas;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="XMLXSDValidator"/> class.
        /// </summary>
        /// <param name="xml">XML document to validate</param>
        /// <param name="schemas">List of Schemas to validate against</param>
        /// <param name="breakOnWarning">
        ///     Sets the object to fail the validation on warnings
        /// </param>
        /// <revision revisor="dev01" date="1/8/2009" version="1.0.4.9">
        ///     Member Created
        /// </revision>
        public XMLXSDValidator(
            XmlReader xml,
            XmlSchemaSet schemas,
            bool breakOnWarning)
            : this(
            new[] { xml },
            schemas,
            breakOnWarning)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="XMLXSDValidator"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <param name="breakOnWarning">
        ///     Sets the object to fail the validation on warnings
        /// </param>
        /// <revision revisor="dev01" date="1/8/2009" version="1.0.4.9">
        ///     Member Created
        /// </revision>
        private XMLXSDValidator(bool breakOnWarning)
        {
            this.failValidationOnWarning = breakOnWarning;
            this.settingsToValidate = new XmlReaderSettings();
            this.settingsToValidate.ValidationType = ValidationType.Schema;
            this.settingsToValidate.ConformanceLevel = ConformanceLevel.Auto;
            this.settingsToValidate.ValidationEventHandler +=
                this.Settings_ValidationEventHandler;
            this.settingsToValidate.ProhibitDtd = false;
            this.settingsToValidate.CloseInput = true;
            this.settingsToValidate.ValidationFlags =
                XmlSchemaValidationFlags.ReportValidationWarnings |
                XmlSchemaValidationFlags.ProcessIdentityConstraints |
                XmlSchemaValidationFlags.ProcessInlineSchema |
                XmlSchemaValidationFlags.ProcessSchemaLocation;
            this.schemaCollection = new XmlSchemaSet();
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets Warnings in the Validation.
        /// </summary>
        public ArrayList ValidationWarnings
        {
            get
            {
                return this.validationWarnings;
            }
        }

        /// <summary>
        ///     Gets the Errors in the Validation.
        /// </summary>
        public ArrayList ValidationErrors
        {
            get
            {
                return this.validationErrors;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Validate XM against XSDs
        /// </summary>
        /// <returns><c>true</c> if valid</returns>
        /// <revision revisor="dev01" date="1/8/2009" version="1.0.4.9">
        ///     Member Created
        /// </revision>
        public bool Validate()
        {
            XmlReader validator;

            // Load the Schemas
            this.LoadXSDSchemas();
            this.LoadXMLDocs();

            // Loop thru all the documents to validate against the 
            // collection of schemas
            foreach (XmlReader xmlRe in this.xmlDocsToValidate)
            {
                try
                {
                    // Create an object to navigate the xml doc
                    validator = XmlReader.Create(
                        xmlRe, this.settingsToValidate);

                    // Review: What is this while loop doing???

                    // Scan all the nodes of the XML doc
                    while (validator.Read())
                    {
                    }
                }
                catch (Exception exc)
                {
                    this.xmlIsValid = false;
                    this.validationErrors.Add(exc.ToString());
                }
            }

            if (this.validationErrors.Count > 0)
            {
                this.xmlIsValid = false;
            }

            return this.xmlIsValid;
        }

        #endregion

        #region Public Events

        #endregion

        #region Private Methods

        /// <summary>
        ///     Load Schemas from the Paths
        /// </summary>
        /// <returns>
        ///     <c>true</c> if all the schemas are loaded correctly, 
        ///     <c>false</c> if there's an error
        /// </returns>
        private bool LoadXSDSchemas()
        {
            // Review: Needs comments
            bool returnValue = true;
            try
            {
                if (this.schemaCollection == null || 
                    this.schemaCollection.Count == 0)
                {
                    foreach (string schemaPath in this.xsdPathList)
                    {
                        // Create an streamReader to read the schema
                        StreamReader strReader = new StreamReader(schemaPath);

                        // Parse and Load Schema
                        XmlSchema schema = XmlSchema.Read(
                            strReader, 
                            this.Settings_ValidationEventHandler);

                        if (this.schemaCollection == null)
                        {
                            this.schemaCollection = new XmlSchemaSet();
                        }

                        // Add Schema to local collection
                        this.schemaCollection.Add(schema);
                        strReader.Close();
                        strReader.Dispose();
                        strReader = null;
                    }
                }

                this.settingsToValidate.Schemas = this.schemaCollection;
            }
            catch (Exception exc)
            {
                this.validationErrors.Add(exc.ToString());
                returnValue = false;
            }

            return returnValue;
        }

        /// <summary>
        ///     Load documents from the Paths
        /// </summary>
        /// <returns>
        ///     <c>true</c> if all the docs are loaded correctly, 
        ///     <c>false</c> if there's an error
        /// </returns>
        private bool LoadXMLDocs()
        {
            // Review: needs comments
            bool returnValue = true;
            int counter = 0;
            try
            {
                if (this.xmlDocsToValidate == null)
                {
                    this.xmlDocsToValidate =
                        new XmlReader[this.xmlPathList.Length];
                    foreach (string xmlPath in this.xmlPathList)
                    {
                        // Create an streamReader to read the Doc
                        StreamReader strReader = new StreamReader(xmlPath);

                        // Parse and Load Doc
                        XmlReader xmlDoc = XmlReader.Create(
                            strReader, this.settingsToValidate);

                        // Add XML to local collection
                        this.xmlDocsToValidate[counter] = xmlDoc;
                        counter = counter + 1;
                    }
                }
            }
            catch (Exception exc)
            {
                this.validationErrors.Add(exc.ToString());
                returnValue = false;
            }

            return returnValue;
        }

        /// <summary>
        ///     Event triggered by errors in the validation
        /// </summary>
        /// <param name="sender">Object who triggers the Event</param>
        /// <param name="eventArgs">Event Arguments</param>
        private void Settings_ValidationEventHandler(
            object sender, 
            ValidationEventArgs eventArgs)
        {
            switch (eventArgs.Severity)
            {
                case XmlSeverityType.Warning:
                    this.validationWarnings.Add(eventArgs.Message);
                    if (this.failValidationOnWarning == true)
                    {
                        this.xmlIsValid = false;
                    }

                    break;
                default:
                    this.validationErrors.Add(eventArgs.Message);
                    this.xmlIsValid = false;
                    break;
            }
        }

        #endregion
    }
}
