// -----------------------------------------------------------------------------
// <copyright file="Machine.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the Machine class domain object.  Object defines
//     the characteristics of a Voting machine
// </summary>
// <revision revisor="dev16" date="1/28/2009" version="1.0.5.9">
//     File Created
// </revision>
// <revision revisor="dev06" date="2/2/2009" version="1.0.5.14">
//     File Modified: Line length and comments.
// </revision>
// <revision revisor="dev14" date="3/09/2009" version="1.0.8.19">
//     File Modified.
// </revision>
// <revision revisor="dev14" date="7/7/2009" version="1.0.13.33">
//     File Modified.
// </revision>  
// -----------------------------------------------------------------------------

namespace Sequoia.DomainObjects
{
    #region Using directives

    using System;
    using System.Xml.Serialization;

    #endregion

    /// <summary>
    ///     Machine class domain object- defines attributes of a voting machine
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev16" date="1/28/2009" version="1.0.5.9">
    ///     Class created.
    /// </revision>
    /// <revision revisor="dev14" date="7/7/2009" version="1.0.13.33">
    ///     Added Validation attributes to properties
    /// </revision>
    public class Machine : DomainObject
    {
        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Machine"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev16" date="1/28/2009" version="1.0.5.9">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev16" date="1/28/2009" version="1.0.5.9">
        ///     added a null MachineAssignment in the call to constructor
        /// </revision>
        public Machine() 
            : this(0, 0, string.Empty, 0, DateTime.Now, 0, string.Empty, null)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Machine"/> class.
        /// </summary>
        /// <param name="id">The Machine id.</param>
        /// <param name="serialNumber">The serial number.</param>
        /// <param name="macAddress">The mac address.</param>
        /// <param name="typeId">The type id.</param>
        /// <param name="purchaseDate">The purchase date.</param>
        /// <param name="protectiveCounter">The protective counter.</param>
        /// <param name="warehouseLocation">The warehouse location.</param>
        /// <param name="assignment">The machine assignment.</param>
        /// <externalUnit cref="Id"/>
        /// <externalUnit cref="SerialNumber"/>
        /// <externalUnit cref="MacAddress"/>
        /// <externalUnit cref="PurchaseDate"/>
        /// <externalUnit cref="ProtectiveCounter"/>
        /// <externalUnit cref="TypeId"/>
        /// <externalUnit cref="WarehouseLocation"/>
        /// <externalUnit cref="MachineAssignment"/>
        /// <revision revisor="dev16" date="1/28/2009" version="1.0.5.9">
        ///     Member created.
        /// </revision>
        /// <revision revisor="dev14" date="3/09/2009" version="1.0.8.19">
        ///     Added MachineAssignment
        /// </revision>
        public Machine(
            int id, 
            int serialNumber, 
            string macAddress, 
            int typeId, 
            DateTime purchaseDate, 
            int protectiveCounter, 
            string warehouseLocation,
            MachineAssignment assignment)
        {
            // set the properties
            this.Id = id;
            this.SerialNumber = serialNumber;
            this.MacAddress = macAddress;
            this.PurchaseDate = purchaseDate;
            this.ProtectiveCounter = protectiveCounter;
            this.TypeId = typeId;
            this.WarehouseLocation = warehouseLocation;
            this.MachineAssignment = assignment;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the machine id.
        /// </summary>
        /// <value>The Machine id.</value>
        /// <externalUnit/>
        /// <revision revisor="dev16" date="1/28/2009" version="1.0.5.9">
        ///     Member created.
        /// </revision>
        public int Id
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the serial number.
        /// </summary>
        /// <value>The serial number.</value>
        /// <externalUnit/>
        /// <revision revisor="dev16" date="1/28/2009" version="1.0.5.9">
        ///     Member created.
        /// </revision>
        public int SerialNumber
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the mac address.
        /// </summary>
        /// <value>The mac address.</value>
        /// <externalUnit/>
        /// <revision revisor="dev16" date="1/28/2009" version="1.0.5.9">
        ///     Member created.
        /// </revision>
        public string MacAddress
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the type id.
        /// </summary>
        /// <value>The type id.</value>
        /// <externalUnit/>
        /// <revision revisor="dev16" date="1/28/2009" version="1.0.5.9">
        ///     Member created.
        /// </revision>
        public int TypeId
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the purchase date.
        /// </summary>
        /// <value>The purchase date.</value>
        /// <externalUnit/>
        /// <revision revisor="dev16" date="1/28/2009" version="1.0.5.9">
        ///     Member created.
        /// </revision>
        public DateTime PurchaseDate
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the protective counter.
        /// </summary>
        /// <value>The protective counter.</value>
        /// <externalUnit/>
        /// <revision revisor="dev16" date="1/28/2009" version="1.0.5.9">
        ///     Member created.
        /// </revision>
        public int ProtectiveCounter
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the warehouse location.
        /// </summary>
        /// <value>The warehouse location.</value>
        /// <externalUnit/>
        /// <revision revisor="dev16" date="1/28/2009" version="1.0.5.9">
        ///     Member created.
        /// </revision>
        public string WarehouseLocation
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or the name.
        /// </summary>
        /// <value>The Machine name.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="2/19/2009" version="1.0.8.02">
        ///     Member created.
        /// </revision>
        [XmlAttribute("Name")]
        public string Name
        {
            get
            {
                return String.Format("Machine: {0}", this.SerialNumber);
            }
        }

        /// <summary>
        ///     Gets or sets the machine assignment.
        /// </summary>
        /// <value>The machine assignment.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="3/9/2009" version="1.0.8.19">
        ///     Member Created
        /// </revision>
        public MachineAssignment MachineAssignment
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
