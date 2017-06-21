// -----------------------------------------------------------------------------
// <copyright file="TabulatorUser.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the TabulatorUser class.
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
    ///     Class containing a tabulator user
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev13" date="4/17/2009" version="1.0.11.07">
    ///     Class created.
    /// </revision>
    [Serializable]
    [XmlRoot("TabulatorUser")]
    public class TabulatorUser : IPersistible
    {
        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="TabulatorUser"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="4/17/2009" version="1.0.11.07">
        ///     Member Created
        /// </revision> 
        public TabulatorUser()
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the user id.
        /// </summary>
        /// <value>The user id.</value>
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
        ///     Gets or sets the pin.
        /// </summary>
        /// <value>The pin number.</value>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="4/17/2009" version="1.0.11.07">
        ///     Member Created
        /// </revision>
        [XmlAttribute("pin")]
        public string Pin
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

        public string SerializedData
        {
            get { throw new NotImplementedException(); }
        }

        public void SetPersister(IPersister persister)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
