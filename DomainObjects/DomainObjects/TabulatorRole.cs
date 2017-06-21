// -----------------------------------------------------------------------------
// <copyright file="TabulatorRole.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the TabulatorRole class.
// </summary>
// <revision revisor="dev13" date="4/17/2009" version="1.0.11.07">
//     File Created
// </revision>  
// <revision revisor="dev13" date="4/22/2009" version="1.0.11.10">
//     File modified
// </revision> 
// -----------------------------------------------------------------------------

namespace Sequoia.DomainObjects
{
    #region Using directives

    using System;
    using System.Xml.Serialization;

    #endregion

    /// <summary>
    ///     Class for a tabulator role
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev13" date="4/17/2009" version="1.0.11.07">
    ///     Class created.
    /// </revision>
    [Serializable]
    [XmlRoot("TabulatorRole")]
    public class TabulatorRole : DomainObject
    {
        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="TabulatorRole"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="4/17/2009" version="1.0.11.07">
        ///     Member Created
        /// </revision> 
        public TabulatorRole()
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the role id.
        /// </summary>
        /// <value>The role id.</value>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="4/17/2009" version="1.0.11.07">
        ///     Member Created
        /// </revision>
        [XmlAttribute("id")]
        public int Id
        {
            get;
            set;
        }
        
        /// <summary>
        ///     Gets or sets the role name.
        /// </summary>
        /// <value>The role name.</value>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="4/17/2009" version="1.0.11.07">
        ///     Member Created
        /// </revision>
        [XmlAttribute("name")]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the tabulator users.
        /// </summary>
        /// <value>The tabulator users.</value>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="4/17/2009" version="1.0.11.07">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev13" date="4/22/2009" version="1.0.11.10">
        ///     Modified xml array attribute
        /// </revision>
        [XmlArray("TabulatorUsers")]
        [XmlArrayItem("TabulatorUser")]
        public TabulatorUserList TabulatorUsers
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
