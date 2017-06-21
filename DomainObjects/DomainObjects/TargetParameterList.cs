// -----------------------------------------------------------------------------
// <copyright file="TargetParameterList.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the TargetParameterList class.
// </summary>
// <revision revisor="dev14" date="4/5/2009" version="1.0.10.02">
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
    ///     TargetParameterList is a class that represents a collection of
    ///     <see cref="TargetParameter" />
    /// </summary>
    /// <externalUnit cref="DomainObjectList{T}" />
    /// <externalUnit cref="TargetParameter" />
    /// <revision revisor="dev14" date="4/5/2009" version="1.0.10.02">
    ///     Class created.
    /// </revision>
    [Serializable]
    [XmlRoot("TargetParameters")]
    public class TargetParameterList : DomainObjectList<TargetParameter>, 
        IPersistible
    {
        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="TargetParameterList"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="4/5/2009" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public TargetParameterList()
        {
        }

        #endregion

        #region Public Properties

        #endregion

        #region Public Methods

        #region Implementation of IPersistible

        /// <summary>
        ///     Gets the serialized data.
        /// </summary>
        /// <value>The serialized data.</value>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/25/2009" version="1.1.3.12">
        ///     Added documentation header
        /// </revision>
        public string SerializedData
        {
            get { throw new System.NotImplementedException(); }
        }
        
        /// <summary>
        ///     Sets the persister.
        /// </summary>
        /// <param name="persister">The persister.</param>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/25/2009" version="1.1.3.12">
        ///     Added documentation header
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
