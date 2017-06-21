// -----------------------------------------------------------------------------
// <copyright file="EncryptedItem.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the EncryptedItem class.
// </summary>
// <revision revisor="dev01" date="1/28/2009" version="1.0.5.9">
//     File Created
// </revision>
// <revision revisor="dev13" date="11/17/2009" version="1.1.3.5">
//     File modified
// </revision>  
// -----------------------------------------------------------------------------

namespace Sequoia.Utilities.Encryption
{
    #region Using directives

    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    #endregion

    /// <summary>
    ///   EncryptedItem represent an encrypted item
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev01" date="1/28/2009" version="1.0.5.9">
    ///     Class created.
    /// </revision>
    /// <revision revisor="dev13" date="11/17/2009" version="1.1.3.5">
    ///     Formatting changes
    /// </revision>
    [Serializable]
    public class EncryptedItem
    {
        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="EncryptedItem"/> class.
        /// </summary>
        /// <revision revisor="dev01" date="1/28/2009" version="1.0.5.9">
        /// Member Created
        /// </revision>
        public EncryptedItem()
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the data.
        /// </summary>
        /// <value>The data to compress.</value>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="1/28/2009" version="1.0.5.9">
        ///     Member Created
        /// </revision>
        public byte[] Data
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets the size of the encrypted.
        /// </summary>
        /// <value>The size of the encrypted.</value>
        /// <externalUnit cref="Data"/>
        /// <revision revisor="dev01" date="1/28/2009" version="1.0.5.9">
        ///     Member Created
        /// </revision>
        public int EncryptedSize
        {
            get
            {
                return this.Data == null ? 0 : this.Data.Length;
            }
        }

        #endregion

        #region Public Methods

        #region FromXml
        /// <summary>
        ///     Froms the XML.
        /// </summary>
        /// <param name="xml">The XML to serialize</param>
        /// <returns>
        ///     A <see cref="EncryptedItem"/> created from the passed in 
        ///     xml string.
        /// </returns>
        /// <externalUnit cref="StringReader"/>
        /// <externalUnit cref="XmlSerializer"/>
        /// <externalUnit cref="EncryptedItem"/>
        /// <revision revisor="dev01" date="1/28/2009" version="1.0.5.9">
        ///     Member Created
        /// </revision>
        public static EncryptedItem FromXml(string xml)
        {
            EncryptedItem item = null;

            // read the file and deserialize
            using (var reader = new StringReader(xml))
            {
                // create xml serializer to transform 
                // sample xml into cartridge store
                var serializer = new XmlSerializer(typeof(EncryptedItem));

                // recreate the cartridge store object from the xml data
                item = (EncryptedItem)serializer.Deserialize(reader);
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
        ///     A <see cref="T:System.String"/> that represents 
        ///     the current <see cref="T:System.Object"/>.
        /// </returns>
        /// <externalUnit cref="XmlSerializer"/>
        /// <externalUnit cref="StringWriter"/>
        /// <externalUnit cref="StringBuilder"/>
        /// <externalUnit cref="EncryptedItem"/>
        /// <revision revisor="dev01" date="1/28/2009" version="1.0.5.9">
        ///     Member Created
        /// </revision>
        public override string ToString()
        {
            // create a serializer for an encrypted item
            var serializer = new XmlSerializer(typeof(EncryptedItem));

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
