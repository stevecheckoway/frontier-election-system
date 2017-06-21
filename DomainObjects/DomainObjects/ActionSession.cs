// -----------------------------------------------------------------------------
// <copyright file="ActionSession.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the Action class.
// </summary>
// <revision revisor="dev13" date="9/4/2009" version="1.0.16.14">
//     File Created
// </revision>  
// <revision revisor="dev01" date="11/11/2009" version="1.1.2.20">
//     Changed file name from VoterrecordActions to ActionSession
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
    ///     Root class for voter record actions
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev13" date="9/4/2009" version="1.0.16.14">
    ///     Class created.
    /// </revision>
    /// <revision revisor="dev01" date="11/11/2009" version="1.1.2.20">
    ///     Changed name of the class from VoterRecordActions to ActionSession,
    ///     removed reference to Ipersistible interface and SetPersister method,
    ///     added UserId property. Also added Equals method.
    /// </revision>
    [Serializable]
    [XmlRoot("ActionSession")]
    public class ActionSession : DomainObject
    {
        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ActionSession"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="9/4/2009" version="1.0.16.14">
        ///     Member Created
        /// </revision> 
        public ActionSession()
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
        ///     Gets or sets the voter record action sets.
        /// </summary>
        /// <value>The voter record action sets.</value>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="9/4/2009" version="1.0.16.14">
        ///     Member Created
        /// </revision>
        [XmlArray("VoterRecordActionSets")]
        [XmlArrayItem("VoterRecordActionSet")]
        public VoterRecordActionSetList VoterRecordActionSets
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the user id.
        /// </summary>
        /// <value>The user id.</value>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="11/11/2009" version="1.1.2.20">
        ///     Member Created
        /// </revision>
        [XmlAttribute("UserId")]
        public Guid UserId
        {
            get;
            set;
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Adds the action.
        /// </summary>
        /// <param name="contestIdParameter">The contest id parameter.</param>
        /// <param name="action">The action.</param>
        /// <param name="actionType">Type action</param>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="10/9/2009" version="1.1.1.9">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev01" date="11/12/2009" version="1.1.2.21">
        ///     Modified search Paradigm
        /// </revision>
        public void AddAction(
            int contestIdParameter, 
            VoterRecordStep action,
            OutstackConditionId actionType)
        {
            var actionSet = new VoterRecordActionSet
                                {
                                    Type = actionType,
                                    ContestId = contestIdParameter,
                                    VoterRecordActions =
                                        new VoterRecordActionList(),
                                };

            if (this.VoterRecordActionSets.Exists(d => d.Type == actionType 
                && d.ContestId == contestIdParameter))
            {
                actionSet =
                    this.VoterRecordActionSets.Find(
                        d =>
                        d.Type == actionType
                        && d.ContestId == contestIdParameter);
            }
            else
            {
                this.VoterRecordActionSets.Add(actionSet);
            }

            actionSet.VoterRecordActions.Add(action);
        }
        
        /// <summary>
        ///     Determines whether the specified <see cref="T:System.Object"/> 
        ///     is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <param name="obj">
        ///     The <see cref="T:System.Object"/> to compare with the current 
        ///     <see cref="T:System.Object"/>.
        /// </param>
        /// <returns>
        ///     <c>true</c> if the specified <see cref="T:System.Object"/> is 
        ///     equal to the current <see cref="T:System.Object"/>; 
        ///     otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="T:System.NullReferenceException">
        ///     The <paramref name="obj"/> parameter is null.
        /// </exception>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="11/11/2009" version="1.1.2.20">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev01" date="11/12/2009" version="1.1.2.21">
        ///     Modified to include children in comparisson
        /// </revision>
        public override bool Equals(object obj)
        {
            bool same = true;

            var session = (ActionSession)obj;

            if ((this.UserId == session.UserId)
                && (session.VoterRecordActionSets.Count == 
                this.VoterRecordActionSets.Count))
            {
                foreach (VoterRecordActionSet set in this.VoterRecordActionSets)
                {
                    if (session.VoterRecordActionSets.Contains(set) != true)
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

        /// <summary>
        ///     Cleans this instance.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="11/12/2009" version="1.1.2.21">
        ///     Member Created
        /// </revision>
        public void Clean()
        {
            var list = new DomainObjectList<VoterRecordActionSet>();
            foreach (VoterRecordActionSet set in this.VoterRecordActionSets)
            {
                if (set.VoterRecordActions.Count == 0)
                {
                    list.Add(set);
                }
            }

            foreach (VoterRecordActionSet removedSet in list)
            {
                this.VoterRecordActionSets.Remove(removedSet);
            }
        }

        #endregion

        #region Public Events

        #endregion

        #region Private Methods

        #endregion
    }
}

