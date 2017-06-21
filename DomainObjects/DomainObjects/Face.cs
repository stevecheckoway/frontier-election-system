// -----------------------------------------------------------------------------
// <copyright file="Face.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2008 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the Face class.
// </summary>
// <revision revisor="dev13" date="12/12/2008" version="1.0.0.0">
//     File Created
// </revision>  
// <revision revisor="dev16" date="12/22/2008" version="1.0.0.0">
//      File modified
// </revision>
// -----------------------------------------------------------------------------

namespace Sequoia.DomainObjects
{
    #region Using directives

    using System;
    using System.Xml.Serialization;

    using Persistence;

    #endregion

    /// <summary>
    ///     An object representing a face style definition
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev13" date="12/12/2008" version="1.0.0.0">
    ///     Class created.
    /// </revision>
    /// <revision revisor="dev16" date="12/22/2008" version="1.0.0.0">
    ///     Updated to inhereit from base domain object.  
    ///     Removed tostring override. Removed unused Fields
    /// </revision>
    [Serializable]
    [XmlRoot("Face")]
    public class Face : DomainObject
    {
        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Face"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="12/15/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public Face()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Face"/> class.
        /// </summary>
        /// <param name="id">The face id.</param>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="12/12/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public Face(int id)
        {
            this.Id = id;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the face id.
        /// </summary>
        /// <value>The face id.</value>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="12/12/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        [XmlAttribute("Id")]
        public int Id
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the marks.
        /// </summary>
        /// <value>The marks.</value>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="12/12/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        [XmlArray("Marks")]
        [XmlArrayItem("Mark")]
        public MarkList Marks
        {
            get;
            set;
        }

        #endregion

        #region Public Methods
        #endregion

        #region Public Events

        #endregion

        #region Private Methods

        #endregion

        #region IPersistible Members

        /// <summary>
        ///     Gets the serialized data.
        /// </summary>
        /// <value>The serialized data.</value>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="12/15/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev16" date="12/22/2008" version="1.0.0.0">
        ///     Modified to use DomainObject.Serialize method
        /// </revision>
        public string SerializedData
        {
            get
            {
                return Serialize(this);
            }
        }

        /// <summary>
        ///     Sets the persister.
        /// </summary>
        /// <param name="persister">The persister.</param>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="12/15/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public void SetPersister(IPersister persister)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
