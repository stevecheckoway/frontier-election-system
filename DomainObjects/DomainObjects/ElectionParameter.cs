// -----------------------------------------------------------------------------
// <copyright file="ElectionParameter.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the ElectionParameter class.
// </summary>
// <revision revisor="dev16" date="1/10/2009" version="1.0.0.0">
//     File Created
// </revision>  
// <revision revisor="dev06" date="2/14/2009" version="1.0.7.3">
//     File Modified
// </revision>
// <revision revisor="dev14" date="6/17/2009" version="1.0.13.13">
//     File Modified
// </revision> 
// -----------------------------------------------------------------------------

namespace Sequoia.DomainObjects
{
    #region Using directives

    using System;
    using System.Xml.Serialization;

    #endregion

    /// <summary>
    ///     Election parameter is a domain object for various global election data such
    ///     as election name, date, FIPS codes etc.
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev16" date="1/10/2009" version="1.0.0.0">
    ///     Class created.
    /// </revision>
    /// <revision revisor="dev06" date="2/14/2009" version="1.0.7.3">
    ///     Class modified: added the id and election id properties; also added 
    /// constructors and updated chaining
    /// </revision>
    /// <revision revisor="dev14" date="6/17/2009" version="1.0.13.13">
    ///     Added ParameterValue and ParameterValueId Properties
    /// </revision>
    [Serializable]
    [XmlRoot("ElectionParameter")]
    public class ElectionParameter : DomainObject, ISysIxParam
    {
        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ElectionParameter"/> class.
        /// </summary>
        /// <externalUnit cref="ElectionParameter(string,string)"/>
        /// <revision revisor="dev16" date="1/10/2009" version="1.0.0.0">
        ///     Member Created
        /// </revision> 
        public ElectionParameter() : this(string.Empty, string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ElectionParameter"/> class.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="value">The value.</param>
        /// <externalUnit cref="ElectionParameter(int,int,string,string)"/>
        /// <revision revisor="dev06" date="2/14/2009" version="1.0.6.9">
        ///     Member Created
        /// </revision>
        public ElectionParameter(string name, string value) 
            : this(0, 0, name, value)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ElectionParameter"/> class.
        /// </summary>
        /// <param name="id">The id of the parameter.</param>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="value">The value.</param>
        /// <externalUnit cref="ElectionParameter(int,int,string,string)"/>
        /// <revision revisor="dev06" date="2/14/2009" version="1.0.7.3">
        ///     Member Created
        /// </revision>
        public ElectionParameter(int id, string name, string value) 
            : this(id, 0, name, value)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ElectionParameter"/> class.
        /// </summary>
        /// <param name="id">The id of the parameter.</param>
        /// <param name="electionId">The election id.</param>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="value">The value.</param>
        /// <externalUnit cref="Id"/>
        /// <externalUnit cref="ElectionId"/>
        /// <externalUnit cref="Name"/>
        /// <externalUnit cref="Value"/>
        /// <revision revisor="dev06" date="2/14/2009" version="1.0.7.3">
        ///     Member Created
        /// </revision>
        public ElectionParameter(int id, int electionId, string name, string value)
        {
            this.Id = id;
            this.ElectionId = electionId;
            this.Name = name;
            this.Value = value;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id of the parameter.</value>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="10/7/2009" version="1.1.1.7">
        ///     Member Created
        /// </revision>
        [XmlIgnore]
        public int Id
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the election id.
        /// </summary>
        /// <value>The election id.</value>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="10/7/2009" version="1.1.1.7">
        ///     Member Created
        /// </revision>
        [XmlIgnore]
        public int ElectionId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name of the parameter.</value>
        /// <externalUnit/>
        /// <revision revisor="dev16" date="1/10/2009" version="1.0.0.0">
        /// Member created.
        /// </revision>
        [XmlAttribute("Name")]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        /// <externalUnit/>
        /// <revision revisor="dev16" date="1/10/2009" version="1.0.0.0">
        /// Member created.
        /// </revision>
        [XmlAttribute("Value")]
        public string Value
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the parameter value id.
        /// </summary>
        /// <value>The parameter value id.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="6/17/2009" version="1.0.13.13">
        ///     Member Created
        /// </revision>
        [XmlIgnore]
        public int ParameterValueId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the type of the parameter used to determine input 
        /// display and allowable values for the option
        /// </summary>
        /// <value>The Parameter type.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="6/17/2009" version="1.0.13.13">
        ///     Member Created
        /// </revision>
        [XmlIgnore]
        public ParameterTypeValue ParameterValue
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
    }
}
