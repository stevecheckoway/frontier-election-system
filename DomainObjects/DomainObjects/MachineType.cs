// -----------------------------------------------------------------------------
// <copyright file="MachineType.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the MachineType class.
// </summary>
// <revision revisor="dev06" date="4/19/2009" version="1.0.11.7">
//     File Created
// </revision>
// -----------------------------------------------------------------------------

namespace Sequoia.DomainObjects
{
    /// <summary>
    ///     MachineType is a class that represents a type of machine - like 
    ///     the Frontier.
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev06" date="4/19/2009" version="1.0.11.7">
    ///     Class created.
    /// </revision>
    public class MachineType
    {
        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="MachineType"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev06" date="4/19/2009" version="1.0.11.7">
        ///     Member Created
        /// </revision>
        public MachineType()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="MachineType"/> class.
        /// </summary>
        /// <param name="machineTypeId">
        ///     The machine Type Id.
        /// </param>
        /// <param name="machineName">
        ///     The machine Name.
        /// </param>
        /// <externalUnit/>
        /// <revision revisor="dev06" date="4/19/2009" version="1.0.11.7">
        ///     Member Created
        /// </revision>
        public MachineType(int machineTypeId, string machineName)
        {
            // set the properties
            this.Id = machineTypeId;
            this.Name = machineName;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The Machine Type id.</value>
        /// <externalUnit/>
        /// <revision revisor="dev06" date="4/19/2009" version="1.0.11.7">
        ///     Member Created
        /// </revision>
        public int Id
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The Machine type name.</value>
        /// <externalUnit/>
        /// <revision revisor="dev06" date="4/19/2009" version="1.0.11.7">
        ///     Member Created
        /// </revision>
        public string Name
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
