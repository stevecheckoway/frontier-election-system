// -----------------------------------------------------------------------------
// <copyright file="VoterRecordActionList.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the VoterRecordActionList class.
// </summary>
// <revision revisor="dev13" date="9/4/2009" version="1.0.16.14">
//     File Created
// </revision> 
// <revision revisor="dev01" date="11/11/2009" version="1.1.2.20">
//     File Modified
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
    ///     ActionList is a <see cref="List{T}" /> of <see cref="VoterRecordStep" /> objects. 
    /// </summary>
    /// <externalUnit cref="VoterRecordStep"/>
    /// <revision revisor="dev13" date="9/4/2009" version="1.0.16.14">
    ///     Class created.
    /// </revision>
    /// <revision revisor="dev01" date="11/11/2009" version="1.1.2.20">
    ///     Changed reference from VoterRecordAction to VoterRecotdStep
    /// </revision>
    [Serializable]
    [XmlRoot("VoterRecordActions")]
    public class VoterRecordActionList : 
        DomainObjectList<VoterRecordStep>, 
        IPersistible
    {
        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="VoterRecordActionList"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="9/4/2009" version="1.0.16.14">
        ///     Member Created
        /// </revision> 
        public VoterRecordActionList()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="VoterRecordActionList"/> class.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        /// <externalUnit cref="List{T}(int)"/>
        /// <revision revisor="dev13" date="9/4/2009" version="1.0.16.14">
        ///     Member Created
        /// </revision>
        public VoterRecordActionList(int capacity)
            : base(capacity)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="VoterRecordActionList"/> class.
        /// </summary>
        /// <param name="actions">The actions.</param>
        /// <externalUnit cref="VoterRecordStep"/>
        /// <externalUnit cref="List{T}(IEnumerable{T})"/>
        /// <externalUnit cref="IEnumerable{T}"/>
        /// <revision revisor="dev13" date="9/4/2009" version="1.0.16.14">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev01" date="11/11/2009" version="1.1.2.20">
        ///     Changed reference from VoterRecordAction to VoterRecotdStep
        /// </revision>
        public VoterRecordActionList(IEnumerable<VoterRecordStep> actions)
            : base(actions)
        {
        }

        #endregion

        #region Public Properties

        #endregion

        #region Public Methods

        #endregion

        #region Public Events

        #endregion

        #region Private Methods

        #endregion

        #region IPersistible Members

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
    }
}
