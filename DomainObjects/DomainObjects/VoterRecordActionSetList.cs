// -----------------------------------------------------------------------------
// <copyright file="VoterRecordActionSetList.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the VoterRecordActionSetList class.
// </summary>
// <revision revisor="dev13" date="9/4/2009" version="1.0.16.14">
//     File Created
// </revision>  
// -----------------------------------------------------------------------------

namespace Sequoia.DomainObjects
{
    #region Using directives

    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    using Sequoia.DomainObjects.Persistence;

    #endregion

    /// <summary>
    ///     ActionList is a <see cref="List{T}" /> of <see cref="VoterRecordActionSet" /> objects. 
    /// </summary>
    /// <externalUnit cref="VoterRecordActionSet"/>
    /// <revision revisor="dev13" date="9/4/2009" version="1.0.16.14">
    ///     Class created.
    /// </revision>
    [Serializable]
    [XmlRoot("VoterRecordActionSets")]
    public class VoterRecordActionSetList : 
        DomainObjectList<VoterRecordActionSet>, 
        IPersistible
    {
        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="VoterRecordActionSetList"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="9/4/2009" version="1.0.16.14">
        ///     Member Created
        /// </revision> 
        public VoterRecordActionSetList()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="VoterRecordActionSetList"/> class.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        /// <externalUnit cref="List{T}(int)"/>
        /// <revision revisor="dev13" date="9/4/2009" version="1.0.16.14">
        ///     Member Created
        /// </revision>
        public VoterRecordActionSetList(int capacity)
            : base(capacity)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="VoterRecordActionSetList"/> class.
        /// </summary>
        /// <param name="actionSets">The voter record action sets.</param>
        /// <externalUnit cref="VoterRecordActionSet"/>
        /// <externalUnit cref="List{T}(IEnumerable{T})"/>
        /// <externalUnit cref="IEnumerable{T}"/>
        /// <revision revisor="dev13" date="9/4/2009" version="1.0.16.14">
        ///     Member Created
        /// </revision>
        public VoterRecordActionSetList(
            IEnumerable<VoterRecordActionSet> actionSets) : base(actionSets)
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the serialized data.
        /// </summary>
        /// <value>The serialized data.</value>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="9/4/2009" version="1.0.16.14">
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

        #region Public Methods

        /// <summary>
        ///     Sets the persister.
        /// </summary>
        /// <param name="persister">The persister.</param>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="9/4/2009" version="1.0.16.14">
        ///     Member Created
        /// </revision>
        public void SetPersister(IPersister persister)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Public Events

        #endregion

        #region Private Methods

        #endregion
    }
}

