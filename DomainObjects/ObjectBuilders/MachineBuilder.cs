// -----------------------------------------------------------------------------
// <copyright file="MachineBuilder.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the MachineBuilder class.
// </summary>
// <revision revisor="dev14" date="9/29/2009" version="1.0.17.21">
//     File Created
// </revision>
// -----------------------------------------------------------------------------

namespace Sequoia.DomainObjects.ObjectBuilders
{
    #region Using directives

    using System;

    #endregion

    /// <summary>
    ///     MachineBuilder is a class that that creates 
    ///     a <see cref="Machine" /> instance
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev14" date="9/29/2009" version="1.0.17.21">
    ///     Class created.
    /// </revision>
    public class MachineBuilder : ISequoiaBuilder<Machine>
    {
        #region Fields

        /// <summary>
        ///     A reference to a candidate if we are to update one.
        /// </summary>
        private Machine machine;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="MachineBuilder"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="9/29/2009" version="1.0.17.21">
        ///     Member Created
        /// </revision>
        public MachineBuilder() : this(new Machine())
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="MachineBuilder"/> class.
        /// </summary>
        /// <param name="machine">The machine.</param>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="9/29/2009" version="1.0.17.21">
        ///     Member Created
        /// </revision>
        public MachineBuilder(Machine machine)
        {
            // set the builder's reference to the given candidate
            this.machine = machine;
            
            // set the local serial number
            this.SerialNumber = machine.SerialNumber;

            // set the local value for the mac address
            this.MacAddress = machine.MacAddress;

            // set the local location property
            this.WarehouseLocation = machine.WarehouseLocation;
            
            // set the local vote for property
            this.PurchaseDate = machine.PurchaseDate;

            // set the local counter property
            this.ProtectiveCounter = machine.ProtectiveCounter;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the mac address.
        /// </summary>
        /// <value>The mac address.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="9/29/2009" version="1.0.17.21">
        ///     Member Created
        /// </revision>
        public string MacAddress
        {
            get; set;
        }

        /// <summary>
        ///     Gets the protective counter.
        /// </summary>
        /// <value>The protective counter.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="9/29/2009" version="1.0.17.21">
        ///     Member Created
        /// </revision>
        public int ProtectiveCounter
        {
            get; 
            private set;
        }

        /// <summary>
        ///     Gets or sets the purchase date.
        /// </summary>
        /// <value>The purchase date.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="9/29/2009" version="1.0.17.21">
        ///     Member Created
        /// </revision>
        public DateTime PurchaseDate
        {
            get; 
            set;
        }

        /// <summary>
        ///     Gets or sets the serial number.
        /// </summary>
        /// <value>The serial number.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="9/29/2009" version="1.0.17.21">
        ///     Member Created
        /// </revision>
        public int SerialNumber
        {
            get; 
            set;
        }

        /// <summary>
        ///     Gets or sets the warehouse location.
        /// </summary>
        /// <value>The warehouse location.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="9/29/2009" version="1.0.17.21">
        ///     Member Created
        /// </revision>
        public string WarehouseLocation
        {
            get; 
            set;
        }

        #endregion

        #region Public Methods

        #region Implementation of ISequoiaBuilder<T>

        /// <summary>
        ///     Builds an instance of a <see cref="Contest"/>.
        /// </summary>
        /// <returns>The contest instance</returns>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="9/29/2009" version="1.0.17.21">
        ///     Member Created
        /// </revision>
        public Machine Build()
        {
            // set the machine's serial number to the local value
            this.machine.SerialNumber = this.SerialNumber;

            // set the machine's mac address to the local value
            this.machine.MacAddress = this.MacAddress;

            // set the machine's location to the local value
            this.machine.WarehouseLocation = this.WarehouseLocation;

            // set the machine's purchase date to the local value
            this.machine.PurchaseDate = this.PurchaseDate;

            // return the contest
            return this.machine;
        }

        #endregion

        #endregion

        #region Public Events

        #endregion

        #region Private Methods

        #endregion
    }
}
