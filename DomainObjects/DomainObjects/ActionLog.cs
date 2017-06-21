// -----------------------------------------------------------------------------
// <copyright file="ActionLog.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the Action class.
// </summary>
// <revision revisor="dev01" date="11/11/2009" version="1.1.2.20">
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
    ///     Root class for voter record actions
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev13" date="9/4/2009" version="1.0.16.14">
    ///     Class created.
    /// </revision>
    /// <revision revisor="dev01" date="11/11/2009" version="1.1.2.20">
    ///     Changed Class name from VoterRecordActions to ActionLog, changed 
    ///     XML tags, Removed Ipersistable interface and SetPersister Method.
    /// </revision>
    [Serializable]
    [XmlRoot("ActionLog")]
    public class ActionLog : DomainObject
    {
        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ActionLog"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="9/4/2009" version="1.0.16.14">
        ///     Member Created
        /// </revision> 
        public ActionLog()
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
        /// <revision revisor="dev01" date="11/11/2009" version="1.1.2.20">
        ///     XML tags
        /// </revision>
        [XmlArray("Sessions")]
        [XmlArrayItem("ActionSession")]
        public DomainObjectList<ActionSession> VoterRecordSessions
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the type of the action.
        /// </summary>
        /// <value>The type of the action.</value>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="9/4/2009" version="1.0.16.14">
        ///     Member Created
        /// </revision>
        [XmlAttribute("VoterRecordId")]
        public long VoterRecordId
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the card id.
        /// </summary>
        /// <value>The card id.</value>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="9/4/2009" version="1.0.16.14">
        ///     Member Created
        /// </revision>
        [XmlAttribute("CardId")]
        public int CardId
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
        /// <param name="userId">the user Id</param>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="10/9/2009" version="1.1.1.9">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev01" date="11/11/2009" version="1.1.2.20">
        ///     Changed logic to pull the correct session and then add the 
        ///     action to it
        /// </revision>
        /// <revision revisor="dev01" date="11/12/2009" version="1.1.2.21">
        ///     Modified search Paradigm
        /// </revision>
        public void AddAction(
            int contestIdParameter,
            VoterRecordStep action,
            OutstackConditionId actionType,
            Guid userId)
        {
            var actionSession = new ActionSession
                                    {
                                        UserId = userId,
                                        VoterRecordActionSets =
                                            new VoterRecordActionSetList()
                                    };
            if (this.VoterRecordSessions.Exists(d => d.UserId == userId))
            {
                actionSession =
                    this.VoterRecordSessions.Find(d => d.UserId == userId);
            }
            else
            {
                this.VoterRecordSessions.Add(actionSession);
            }

            actionSession.AddAction(contestIdParameter, action, actionType);
        }

        /// <summary>
        ///     Removes the action.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="11/12/2009" version="1.1.2.21">
        ///     Member Created
        /// </revision>
        public void RemoveAction(VoterRecordStep action)
        {
            bool found = false;
            for (int i = 0; i < this.VoterRecordSessions.Count && 
                found != true; i++)
            {
                ActionSession session = this.VoterRecordSessions[i];
                for (int j = 0; j < session.VoterRecordActionSets.Count && 
                    found != true; j++)
                {
                    VoterRecordActionSet actionSet =
                        session.VoterRecordActionSets[j];

                    if (actionSet.VoterRecordActions.Remove(action))
                    {
                        found = true;
                    }
                }
            }

            if (found)
            {
                this.Clean();
            }
        }

        /// <summary>
        ///     Removes the action set.
        /// </summary>
        /// <param name="set">The ActionSet.</param>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="11/12/2009" version="1.1.2.21">
        ///     Member Created
        /// </revision>
        public void RemoveActionSet(VoterRecordActionSet set)
        {
            bool found = false;
            for (int i = 0; i < this.VoterRecordSessions.Count && 
                found != true; i++)
            {
                ActionSession session = this.VoterRecordSessions[i];
                if (session.VoterRecordActionSets.Remove(set))
                {
                    found = true;
                }
            }

            if (found)
            {
                this.Clean();
            }
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
            var list = new DomainObjectList<ActionSession>();
            foreach (ActionSession session in this.VoterRecordSessions)
            {
                if (session.VoterRecordActionSets.Count == 0)
                {
                    list.Add(session);
                }
                else
                {
                    session.Clean();
                }
            }

            foreach (ActionSession removedSession in list)
            {
                this.VoterRecordSessions.Remove(removedSession);
            }
        }

        #endregion

        #region Public Events

        #endregion

        #region Private Methods

        #endregion
    }
}

