// -----------------------------------------------------------------------------
// <copyright file="VoterRecordActionSet.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the VoterRecordActionSet class.
// </summary>
// <revision revisor="dev13" date="9/4/2009" version="1.0.16.14">
//     File Created
// </revision>  
// <revision revisor="dev01" date="11/12/2009" version="1.1.2.21">
//     File Modified
// </revision>
// -----------------------------------------------------------------------------

namespace Sequoia.DomainObjects
{
    #region Using directives

    using System;
    using System.Xml.Serialization;

    #endregion

    /// <summary>
    ///     Class for voter record actions
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev13" date="9/4/2009" version="1.0.16.14">
    ///     Class created.
    /// </revision>
    /// <revision revisor="dev01" date="11/11/2009" version="1.1.2.20">
    ///     Removed Ipersistible inheritance, removed the SetPersister Method
    ///     and removed  the UserId property
    /// </revision>
    [Serializable]
    [XmlRoot("VoterRecordActionSet")]
    public class VoterRecordActionSet : DomainObject
    {
        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="VoterRecordActionSet"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="9/4/2009" version="1.0.16.14">
        ///     Member Created
        /// </revision> 
        public VoterRecordActionSet()
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

        /// <summary>
        /// Gets or sets the contest id.
        /// </summary>
        /// <value>The contest id.</value>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="10/9/2009" version="1.1.1.9">
        ///     Member Created
        /// </revision>
        [XmlAttribute("Contest")]
        public int ContestId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The action type.</value>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="11/11/2009" version="1.1.2.20">
        ///     Member Created
        /// </revision>
        [XmlAttribute("Type")]
        public OutstackConditionId Type
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the voter record actions.
        /// </summary>
        /// <value>The voter record actions.</value>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="9/4/2009" version="1.0.16.14">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev01" date="11/11/2009" version="1.1.2.20">
        ///     Changed XML Tags
        /// </revision>
        [XmlArray("Steps")]
        [XmlArrayItem("VoterRecordStep")]
        public VoterRecordActionList VoterRecordActions
        {
            get;
            set;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <param name="obj">The <see cref="T:System.Object"/> to compare with the current <see cref="T:System.Object"/>.</param>
        /// <returns>
        /// true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
        /// </returns>
        /// <exception cref="T:System.NullReferenceException">
        /// The <paramref name="obj"/> parameter is null.
        /// </exception>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="10/9/2009" version="1.1.1.9">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev01" date="11/12/2009" version="1.1.2.21">
        ///     Modified to include children in comparisson
        /// </revision>
        public override bool Equals(object obj)
        {
            bool same = true;

            var recordActionSet = (VoterRecordActionSet)obj;

            if ((this.ContestId == recordActionSet.ContestId)
                && (this.Type == recordActionSet.Type)
                && (this.VoterRecordActions.Count == recordActionSet.VoterRecordActions.Count))
            {
                foreach (VoterRecordStep step in this.VoterRecordActions)
                {
                    if (recordActionSet.VoterRecordActions.Contains(step) != true)
                    {
                        same = false;
                        break;
                    }
                }
            }
            else
            {
                same = false;
            }

            return same;
        }

        #endregion

        #region Public Events

        #endregion

        #region Private Methods

        #endregion
    }
}
