// -----------------------------------------------------------------------------
// <copyright file="DomainObjectList.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2008 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the DomainObjectList class.
// </summary>
// <revision revisor="dev16" date="12/22/2008" version="1.0.0.0">
//     File Created
// </revision>  
// <revision revisor = "dev05" date="02/05/09" version="1.0.6.1">
//     Added FromXmlFile method.
// </revision>
// <revision revisor="dev11" date="02/20/2009" version="1.0.8.0301">
// Marked as Serializable</revision>
// <revision revisor = "dev14" date="05/21/09" version="1.0.12.11">
//     File Modified
// </revision>
// -----------------------------------------------------------------------------

namespace Sequoia.DomainObjects
{
    #region Using directives

    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    #endregion

    /// <summary>
    /// List of <see cref="DomainObject"/>
    /// </summary>
    /// <typeparam name="D">Type of object in the list</typeparam>
    /// <externalUnit/>
    /// <revision revisor="dev16" date="12/22/2008" version="1.0.?.0">
    /// Class created.
    /// </revision>
    /// <revision revisor="dev11" date="02/20/2009" version="1.0.8.0301">
    /// Marked as Serializable</revision>
    /// <revision revisor="dev14" date="5/21/2009" version="1.0.12.11">
    ///     Changed name of type param to 'D' from 'T' since the underlying
    ///     methods would hide the list type.
    /// </revision>
    [Serializable]
    public class DomainObjectList<D> : List<D>
    {
        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="DomainObjectList{D}"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev16" date="12/22/2008" version="1.0.0.0">
        ///     Member created.
        /// </revision>
        public DomainObjectList()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DomainObjectList{D}"/> class.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        /// <externalUnit/>
        /// <revision revisor="dev16" date="12/22/2008" version="1.0.0.0">
        ///     Member created.
        /// </revision>
        public DomainObjectList(int capacity) : base(capacity)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DomainObjectList{D}"/> class.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <externalUnit/>
        /// <revision revisor="dev16" date="12/22/2008" version="1.0.0.0">
        ///     Member created.
        /// </revision>
        public DomainObjectList(IEnumerable<D> items) : base(items)
        {
        }
        
        #endregion

        #region Public Properties

        #endregion

        #region Public Methods
        /// <summary>
        /// Serializes this item.
        /// </summary>
        /// <typeparam name="T">Type of object to serialize</typeparam>
        /// <param name="item">The item to serialize.</param>
        /// <returns>serialized object as string</returns>
        /// <externalUnit cref="DomainObjectSerializer"/>
        /// <revision revisor="dev16" date="12/22/2008" version="1.0.0.0">
        /// Member created.
        /// </revision>
        public static string Serialize<T>(T item)
        {
            // return xmlBuilder as string
            return DomainObjectSerializer.Serialize(item);
        }

        /// <summary>
        /// Serializes this item.
        /// </summary>
        /// <typeparam name="T">Type of object to serialize</typeparam>
        /// <param name="item">The item to serialize.</param>
        /// <param name="xmlRootOverride">The XML root.</param>
        /// <returns>serialized object as string</returns>
        /// <externalUnit cref="DomainObjectSerializer"/>
        /// <revision revisor="dev16" date="12/22/2008" version="1.0.0.0">
        /// Member created.
        /// </revision>
        public static string Serialize<T>(T item, XmlRootAttribute xmlRootOverride)
        {
            // return xmlBuilder as string
            return DomainObjectSerializer.Serialize(item, xmlRootOverride);
        }

        /// <summary>
        /// Method creates object from xml.
        /// </summary>
        /// <typeparam name="T">The type to deserialize</typeparam>
        /// <param name="xml">The XML string.</param>
        /// <returns>
        /// Based object from xml data- object may be null
        /// </returns>
        /// <externalUnit cref="DomainObjectSerializer"/>
        /// <revision revisor="dev16" date="12/22/2008" version="1.0.0.0">
        /// Member created.
        /// </revision>
        public static T FromXml<T>(string xml)
        {
            return DomainObjectSerializer.FromXml<T>(xml);
        }

        /// <summary>
        /// Deserialize an object from an XML file.
        /// </summary>
        /// <typeparam name="T">Type of object to deserialize</typeparam>
        /// <param name="fileName">Input filename</param>
        /// <returns>Deserialized object of specified type</returns>
        /// <externalUnit/>
        /// <revision revisor="dev05" date="02/05/09" version="1.0.6.1">
        /// Method created.
        /// </revision>
        public static T FromXmlFile<T>(string fileName)
        {
            return DomainObjectSerializer.FromXmlFile<T>(fileName);
        }

        #endregion

        #region Public Events

        #endregion

        #region Private Methods

        #endregion
    }
}
