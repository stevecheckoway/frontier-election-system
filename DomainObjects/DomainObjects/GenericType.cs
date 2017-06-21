// -----------------------------------------------------------------------------
// <copyright file="genericType.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the genericType class.
// </summary>
// <revision revisor="dev01" date="8/17/2009" version="1.0.15.9">
//     File Created
// </revision>
// -----------------------------------------------------------------------------

namespace Sequoia.DomainObjects
{
    #region Using directives

    using System;
    using System.Xml.Serialization;
    
    #endregion

    /// <summary>
    ///     GenericType represents a GenericType in an election definition    
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev01" date="8/17/2009" version="1.0.15.9">
    ///     Class created.
    /// </revision>
    [Serializable]
    [XmlRoot("GenericType")]
    public class GenericType : DomainObject
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="GenericType"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="8/17/2009" version="1.0.15.9">
        ///     Member Created
        /// </revision>
        public GenericType()
            : this(0, String.Empty)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="GenericType"/> class.
        /// </summary>
        /// <param name="id">
        ///     The Type id.
        /// </param>
        /// <param name="name">
        ///     The Type name.
        /// </param>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="8/17/2009 7:49:51 AM" version="1.0.15.9">
        ///     Member Created
        /// </revision>
        public GenericType(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        #endregion

        #region Public Properties
        /// <summary>
        ///     Gets or sets the genericType id.
        /// </summary>
        /// <value>The genericType id.</value>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="8/17/2009" version="1.0.15.9">
        ///     Member Created
        /// </revision>
        [XmlAttribute("Id")]
        public int Id
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the genericType name.
        /// </summary>
        /// <value>The genericType name.</value>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="8/17/2009" version="1.0.15.9">
        ///     Member Created
        /// </revision>
        [XmlAttribute("Name")]
        public string Name
        {
            get;
            set;
        }
        #endregion
    }
}
