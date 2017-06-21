// -----------------------------------------------------------------------------
// <copyright file="Transmission.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the Transmission class.
// </summary>
// <revision revisor="dev14" date="5/15/2009" version="1.0.12.05">
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
    ///     Transmission is a class that keeps track of transmissions from
    ///     the tabulator
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev14" date="5/15/2009" version="1.0.12.05">
    ///     Class created.
    /// </revision>
    [Serializable]
    [XmlRoot("Transmission")]
    public class Transmission : DomainObject, IPersistible
    {
        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Transmission"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="5/15/2009" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public Transmission()
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the destination.  The IP address where the transmission
        /// was sent to.
        /// </summary>
        /// <value>The destination.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="5/18/2009" version="1.0.12.08">
        ///     Member Created
        /// </revision>
        [XmlAttribute("Destination")]
        public string Destination
        {
            get; 
            set;
        }

        /// <summary>
        /// Gets or sets the machine serial number from which the transmission
        /// was sent
        /// </summary>
        /// <value>The machine serial number.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="5/18/2009" version="1.0.12.08">
        ///     Member Created
        /// </revision>
        [XmlAttribute("MachineSerialNumber")]
        public long MachineSerialNumber
        {
            get; 
            set;
        }

        /// <summary>
        /// Gets or sets the mode id for which the transmission was made.
        /// </summary>
        /// <value>The mode id.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="5/18/2009" version="1.0.12.08">
        ///     Member Created
        /// </revision>
        [XmlAttribute("Mode")]
        public int Mode
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the time the transmission was sent.
        /// </summary>
        /// <value>The time sent.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="5/15/2009" version="1.0.12.05">
        ///     Member Created
        /// </revision>
        [XmlAttribute("TimeSent")]
        public DateTime TimeSent
        {
            get; 
            set;
        }

        #region Implementation of IPersistible

        /// <summary>
        /// Gets the serialized data.
        /// </summary>
        /// <value>The serialized data.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="5/18/2009" version="1.0.12.08">
        ///     Member Created
        /// </revision>
        public string SerializedData
        {
            get
            {
                return Serialize(this);
            }
        }
        
        #endregion

        #endregion

        #region Public Methods

        #region Implementation of IPersistible

        /// <summary>
        /// Sets the persister.
        /// </summary>
        /// <param name="persister">The persister.</param>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="5/18/2009" version="1.0.12.08">
        ///     Member Created
        /// </revision>
        public void SetPersister(IPersister persister)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #endregion

        #region Public Events

        #endregion

        #region Private Methods

        #endregion
    }
}
