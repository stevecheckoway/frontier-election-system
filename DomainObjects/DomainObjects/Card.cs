// -----------------------------------------------------------------------------
// <copyright file="Card.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2008 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the Card class.
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
    ///     An object represnting a card style definition
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev13" date="12/12/2008" version="1.0.0.0">
    ///     Class created.
    /// </revision>
    /// <revision revisor="dev16" date="12/22/2008 10:28 AM" version="1.0.0.0">
    ///      Updated to inhereit from base domain object.  removed tostring override
    /// </revision>
    [Serializable]
    [XmlRoot("Card")]
    public class Card : DomainObject
    {
        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Card"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="12/15/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public Card()
        {
        }
        
        /// <summary>
        ///     Initializes a new instance of the <see cref="Card"/> class.
        /// </summary>
        /// <param name="id">The face id.</param>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="12/12/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public Card(int id)
        {
            this.Id = id;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the card id.
        /// </summary>
        /// <value>The card id.</value>
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
        ///     Gets or sets the barcode.
        /// </summary>
        /// <value>The barcode.</value>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="12/17/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        [XmlAttribute("Barcode")]
        public string Barcode
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the faces.
        /// </summary>
        /// <value>The faces.</value>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="12/12/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        [XmlArray("Faces")]
        [XmlArrayItem("Face")]
        public FaceList Faces
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
