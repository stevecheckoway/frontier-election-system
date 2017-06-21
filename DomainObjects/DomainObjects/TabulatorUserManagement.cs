// -----------------------------------------------------------------------------
// <copyright file="TabulatorUserManagement.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the TabulatorUserManagement class.
// </summary>
// <revision revisor="dev13" date="4/17/2009" version="1.0.11.07">
//     File Created
// </revision>  
// -----------------------------------------------------------------------------

namespace Sequoia.DomainObjects
{
    #region Using directives

    using System;
    using System.Xml.Serialization;

    using Sequoia.DomainObjects.Persistence;

    #endregion

    /// <summary>
    ///     Class for tabulator user management.
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev13" date="4/17/2009" version="1.0.11.07">
    ///     Class created.
    /// </revision>
    [Serializable]
    [XmlRoot("TabulatorUserManagement")]
    public class TabulatorUserManagement : DomainObject, IPersistible
    {
        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="TabulatorUserManagement"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="4/17/2009" version="1.0.11.07">
        ///     Member Created
        /// </revision> 
        public TabulatorUserManagement()
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the tabulator roles.
        /// </summary>
        /// <value>The tabulator roles.</value>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="4/17/2009" version="1.0.11.07">
        ///     Member Created
        /// </revision>
        [XmlArray("TabulatorRoles")]
        [XmlArrayItem("TabulatorRole")]
        public TabulatorRoleList TabulatorRoles
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
        /// <revision revisor="dev13" date="4/17/2009" version="1.0.11.07">
        ///     Member Created
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
        /// <revision revisor="dev13" date="4/17/2009" version="1.0.11.07">
        ///     Member Created
        /// </revision>
        public void SetPersister(IPersister persister)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
