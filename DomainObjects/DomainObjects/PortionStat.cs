// -----------------------------------------------------------------------------
// <copyright file="PortionStat.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the PortionStat class.
// </summary>
// <revision revisor="dev13" date="10/21/2009" version="1.1.1.21">
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
    ///	    PortionStat represents a precinct portion statistic object. This is
    ///     used to handle voter registration info related to parties 
    ///     and precinct portions.
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev13" date="10/21/2009" version="1.1.1.21">
    ///     Class created.
    /// </revision>
    [Serializable]
    [XmlRoot("PortionStat")]
    public class PortionStat : DomainObject
    {
        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="PortionStat"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="10/21/2009" version="1.1.1.21">
        ///     Member Created
        /// </revision>	
        public PortionStat()
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the party id.
        /// </summary>
        /// <value>The party id.</value>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="10/21/2009" version="1.1.1.21">
        ///     Member Created
        /// </revision>
        [XmlAttribute("PartyId")]
        public int PartyId
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the external reference key party id.
        /// </summary>
        /// <value>The external reference key party id.</value>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="10/22/2009" version="1.1.1.22">
        ///     Member Created
        /// </revision>
        [XmlAttribute("ExtRefKeyPartyId")]
        public string ExtRefKeyPartyId
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the portion id.
        /// </summary>
        /// <value>The portion id.</value>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="10/21/2009" version="1.1.1.21">
        ///     Member Created
        /// </revision>
        [XmlAttribute("PortionId")]
        public int PortionId
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the external reference key portion id.
        /// </summary>
        /// <value>The external reference key portion id.</value>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="10/22/2009" version="1.1.1.22">
        ///     Member Created
        /// </revision>
        [XmlAttribute("ExtRefKeyPortionId")]
        public string ExtRefKeyPortionId
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the registration.
        /// </summary>
        /// <value>The registration.</value>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="10/21/2009" version="1.1.1.21">
        ///     Member Created
        /// </revision>
        [XmlAttribute("Registration")]
        public long Registration
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
