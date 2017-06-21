// -----------------------------------------------------------------------------
// <copyright file="DomainObjectSerializer.cs" company="Sequoia Voting Systems">
//     Copyright (c) 2008 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the DomainObjectSerializer class.
// </summary>
// <revision revisor="dev16" date="12/22/2008" version="1.0.?.0">
//     File Created
// </revision>
// <revision revisor = "dev05" date="02/05/09" version="1.0.6.1">
//     Added FromXmlFile method.
// </revision>
// -----------------------------------------------------------------------------

namespace Sequoia.DomainObjects
{
    #region Using directives

    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    #endregion

    /// <summary>
    /// Serialize and deserialize domainobjects
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev16" date="12/22/2008" version="1.0.0.0">
    ///     Class created.
    /// </revision>
    internal sealed class DomainObjectSerializer
    {
        #region Fields

        #endregion

        #region Public Methods

        /// <summary>
        /// Serializes this item.
        /// </summary>
        /// <param name="item">
        /// The item to serialize.
        /// </param>
        /// <typeparam name="T">
        /// Item to serialize
        /// </typeparam>
        /// <returns>
        /// serialized object as string
        /// </returns>
        /// <externalUnit cref="XmlSerializer"/>
        /// <externalUnit cref="StringWriter"/>
        /// <revision revisor="dev16" date="12/22/2008" version="1.0.0.0">
        /// Member created.
        /// </revision>
        internal static string Serialize<T>(T item)
        {
            return Serialize(item, null);
        }

        /// <summary>
        /// Serializes this item.
        /// </summary>
        /// <param name="item">
        /// The item to serialize.
        /// </param>
        /// <param name="xmlAttribute">
        /// The xml Attribute.
        /// </param>
        /// <typeparam name="T">
        /// Item to serialize
        /// </typeparam>
        /// <returns>
        /// serialized object as string
        /// </returns>
        /// <externalUnit cref="XmlSerializer"/>
        /// <externalUnit cref="StringWriter"/>
        /// <revision revisor="dev16" date="12/22/2008" version="1.0.0.0">
        /// Member created.
        /// </revision>
        internal static string Serialize<T>(T item, XmlRootAttribute xmlAttribute)
        {
            // serialize this object as an xml formatted string
            // create the xml serializer based on this object
             XmlSerializer serializer = 
                xmlAttribute == null ? 
                new XmlSerializer(typeof(T)) : 
                new XmlSerializer(typeof(T), xmlAttribute);

            // create a stringbuilder which we will use to send the
            // serialized output
            var xmlBuilder = new StringBuilder();

            // create the xmlwriter using the stringbuilder
            using (var writer = new StringWriter(xmlBuilder))
            {
                // serialize this object to the writer
                serializer.Serialize(writer, item);
            }

            // return xmlBuilder as string
            return xmlBuilder.ToString();
        }

        /// <summary>
        /// Method creates object from xml.
        /// </summary>
        /// <typeparam name="T">Type to be deserialized into</typeparam>
        /// <param name="xml">The XML to desirialize.</param>
        /// <returns>
        /// Based object from xml data- object may be null
        /// </returns>
        /// <externalUnit cref="StringReader"/>
        /// <externalUnit cref="XmlSerializer"/>
        /// <revision revisor="dev16" date="12/22/2008" version="1.0.0.0">
        /// Member created.
        /// </revision>
        internal static T FromXml<T>(string xml)
        {
            // create an item we are deserializing and set to its default value
            // - could be null
            T item;

            // load the string
            using (var reader = new StringReader(xml))
            {
                // create xml serializer to transform sample xml into item
                var serializer = new XmlSerializer(typeof(T));

                // recreate the object from the xml data
                item = (T)serializer.Deserialize(reader);
            }

            return item;
        }

        /// <summary>
        /// Deserialize an object from an XML file.
        /// </summary>
        /// <typeparam name="T">
        /// Type of object to deserialize
        /// </typeparam>
        /// <param name="fileName">
        /// Input filename
        /// </param>
        /// <externalUnit/>
        /// <revision revisor="dev05" date="02/05/09" version="1.0.6.1">
        ///     Method created.
        /// </revision>
        /// <returns>
        /// The from xml file.
        /// </returns>
        internal static T FromXmlFile<T>(string fileName)
        {
            T result;      // Returned object

            try
            {
                result = FromXml<T>(File.ReadAllText(fileName));
            }
            catch (InvalidOperationException)
            {
                // If the deserialization fails, the exception message is
                // something like, "There is an error in XML document (2, 2)".
                // It's more helpful to know which file it was.
                throw new InvalidOperationException(
                    "Error reading XML file " + fileName);
            }

            return result;
        }

        #endregion

        #region Public Events

        #endregion

        #region Private Methods

        #endregion
    }
}
