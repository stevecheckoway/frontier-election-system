// -----------------------------------------------------------------------------
// <copyright file="MachineParameters.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2008 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the MachineParameters class.
// </summary>
// <revision revisor="dev16" date="12/23/2008" version="1.0.?.0">
//     File Created
// </revision>  
// <revision revisor="dev16" date="1/27/2009" version="1.0.5.7">
//     File Modified
// </revision>
// -----------------------------------------------------------------------------

namespace Sequoia.DomainObjects
{
    #region Using directives
    
    using System;
    using System.Xml.Serialization;
    using Persistence;
 
    #endregion

    /// <summary>
    ///     Machine Parameters 
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev16" date="12/23/2008" version="1.0.0.0">
    ///     Class created.
    /// </revision>
    /// <revision revisor="dev16" date="1/26/2009" version="1.0.5.7">
    /// Added implementation of IPersistable
    /// </revision>
    [Serializable]
    [XmlRoot("MachineParameters")]
    public class MachineParameters : DomainObject, IPersistible
    {
        #region Fields

        #endregion

        #region Public Properties
        /// <summary>
        ///     Gets or sets the ParameterFiles collection.
        /// </summary>
        /// <value>The ParameterFiles.</value>
        /// <externalUnit/>
        /// <revision revisor="dev16" date="12/23/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        [XmlArray("ParameterFiles")]
        [XmlArrayItem("ParameterFile")]
        public ParameterFileList ParameterFiles
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets the serialized data.
        /// </summary>
        /// <value>The serialized data.</value>
        /// <externalUnit/>
        /// <revision revisor="dev16" date="1/26/2009" version="1.0.5.7">
        ///     Member created.
        /// </revision>
        public string SerializedData
        {
            get
            {
                return Serialize(this);
            }
        }

        #endregion

        #region Public Methods

        #endregion

        #region Public Events

        #endregion

        #region Private Methods

        #endregion

        /// <summary>
        ///     Sets the persister.
        /// </summary>
        /// <param name="persister">The persister.</param>
        /// <externalUnit/>
        /// <revision revisor="dev16" date="1/26/2009" version="1.0.5.7">
        ///     Member created.
        /// </revision>
        public void SetPersister(IPersister persister)
        {
            throw new NotImplementedException();
        }
    }
}
