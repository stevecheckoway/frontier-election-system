// -----------------------------------------------------------------------------
// <copyright file="CompressedItem.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the CompressedItem class.
// </summary>
// <revision revisor="dev06" date="1/12/2009" version="1.0.4.13">
//     File Created
// </revision>
// <revision revisor="dev13" date="11/17/2009" version="1.1.3.5">
//     File modified
// </revision> 
// -----------------------------------------------------------------------------

namespace Sequoia.Utilities.Compression
{
    #region Using directives

    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    #endregion

    /// <summary>
    ///     CompressedItem represents a compressed item
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev06" date="1/12/2009" version="1.0.4.13">
    ///     Class created.
    /// </revision>
    [Serializable]
    public class CompressedItem
    {
        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="CompressedItem"/> class.
        /// </summary>
        /// <externalUnit cref="UncompressedSize"/>
        /// <revision revisor="dev06" date="1/12/2009" version="1.0.4.13">
        ///     Member Created
        /// </revision>
        public CompressedItem()
        {
            // set the default uncompressed size
            this.UncompressedSize = 0;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the data.
        /// </summary>
        /// <value>The data to compress.</value>
        /// <externalUnit/>
        /// <revision revisor="dev06" date="1/12/2009" version="1.0.4.13">
        ///     Member Created
        /// </revision>
        public byte[] Data
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets the size of the compressed.
        /// </summary>
        /// <value>The size of the compressed.</value>
        /// <externalUnit cref="Data"/>
        /// <revision revisor="dev06" date="1/12/2009" version="1.0.4.13">
        ///     Member Created
        /// </revision>
        public int CompressedSize
        {
            get
            {
                return this.Data == null ? 0 : this.Data.Length;
            }
        }

        /// <summary>
        ///     Gets or sets the size of the uncompressed.
        /// </summary>
        /// <value>The size of the uncompressed.</value>
        /// <externalUnit/>
        /// <revision revisor="dev06" date="1/12/2009" version="1.0.4.13">
        ///     Member Created
        /// </revision>
        public int UncompressedSize
        {
            get;
            set;
        }

        #endregion

        #region Public Methods

        #region FromXml
        /// <summary>
        ///     Froms the XML.
        /// </summary>
        /// <param name="xml">The XML to serialize</param>
        /// <returns>
        ///     A <see cref="CompressedItem"/> created from the passed-in 
        ///     xml string.
        /// </returns>
        /// <externalUnit cref="StringReader"/>
        /// <externalUnit cref="XmlSerializer"/>
        /// <externalUnit cref="CompressedItem"/>
        /// <revision revisor="dev06" date="1/12/2009" version="1.0.4.13">
        ///     Member Created
        /// </revision>
        public static CompressedItem FromXml(string xml)
        {
            CompressedItem item = null;

            // read the file and deserialize
            using (var reader = new StringReader(xml))
            {
                // create xml serializer to transform sample xml 
                // into cartridge store
                var serializer = new XmlSerializer(typeof(CompressedItem));

                // recreate the cartridge store object from the xml data
                item = (CompressedItem)serializer.Deserialize(reader);
            }

            return item;
        }
        #endregion

        #region ToString
        /// <summary>
        ///     Returns a <see cref="T:System.String"/> that represents the 
        ///     current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.String"/> that represents the 
        ///     current <see cref="T:System.Object"/>.
        /// </returns>
        /// <externalUnit cref="XmlSerializer"/>
        /// <externalUnit cref="StringWriter"/>
        /// <externalUnit cref="StringBuilder"/>
        /// <externalUnit cref="CompressedItem"/>
        /// <revision revisor="dev06" date="1/12/2009" version="1.0.4.13">
        ///     Member Created
        /// </revision>
        public override string ToString()
        {
            // create a serializer for a compressed item
            var serializer = new XmlSerializer(typeof(CompressedItem));

            // create a stringbuilder which we will use to send the 
            // serialized output
            var xmlBuilder = new StringBuilder();

            // create the xmlwriter using the stringbuilder
            using (var writer = new StringWriter(xmlBuilder))
            {
                // serialize this object to the writer
                serializer.Serialize(writer, this);
            }

            // return xmlBuilder as string
            return xmlBuilder.ToString().Replace(
                Environment.NewLine, string.Empty);
        }
        #endregion

        #endregion

        #region Public Events

        #endregion

        #region Private Methods

        #endregion
    }
}