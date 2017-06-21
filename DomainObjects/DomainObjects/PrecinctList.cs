// -----------------------------------------------------------------------------
// <copyright file="PrecinctList.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the PrecinctList class.
// </summary>
// <revision revisor="dev14" date="11/10/2009" version="1.1.2.19">
//     File Created
// </revision>  
// -----------------------------------------------------------------------------

namespace Sequoia.DomainObjects
{
    #region Using directives

    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;
    using Persistence;

    #endregion

    /// <summary>
    ///	    PrecinctList is a <see cref="DomainObjectList{T}" /> of 
    ///     <see cref="Precinct" /> objects. 
    /// </summary>
    /// <externalUnit cref="Precinct"/>
    /// <externalUnit cref="DomainObjectList{T}"/>
    /// <revision revisor="dev14" date="11/10/2009" version="1.1.2.19">
    ///     Class created.
    /// </revision>
    [Serializable]
    [XmlRoot("Precincts")]
    public class PrecinctList : DomainObjectList<Precinct>, IPersistible
    {
        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="PrecinctList"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="11/10/2009" version="1.1.2.19">
        ///     Member Created
        /// </revision>	
        public PrecinctList()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PrecinctList"/> class.
        /// </summary>
        /// <param name="capacity">The list capacity.</param>
        /// <externalUnit cref="DomainObjectList{T}"/>
        /// <revision revisor="dev14" date="11/10/2009" version="1.1.2.19">
        /// Member Created
        /// </revision>
        public PrecinctList(int capacity)
            : base(capacity)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="PrecinctList"/> class.
        /// </summary>
        /// <param name="precincts">The precincts.</param>
        /// <externalUnit cref="Precinct"/>
        /// <externalUnit cref="DomainObjectList{T}"/>
        /// <revision revisor="dev14" date="11/10/2009" version="1.1.2.19">
        ///     Member Created
        /// </revision>
        public PrecinctList(IEnumerable<Precinct> precincts)
            : base(precincts)
        {
        }

        #endregion

        #region Public Properties

        #region Implementation of IPersistible

        /// <summary>
        ///     Gets the serialized data.
        /// </summary>
        /// <value>The serialized data.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="11/10/2009" version="1.1.2.19">
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

        #endregion

        #region Public Events

        #endregion

        #region Private Methods

        #endregion

        #region Implementation of IPersistible

        /// <summary>
        /// Sets the persister.
        /// </summary>
        /// <param name="persister">The persister.</param>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="11/10/2009" version="1.1.2.19">
        ///     Member Created
        /// </revision>
        public void SetPersister(IPersister persister)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
