//-----------------------------------------------------------------------------
// <copyright file="EventLogItem.cs" 
//  company="Sequoia Voting Systems">
//     Copyright (c) 2008 Sequoia Voting Systems. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
//     File created
// </revision>
//-----------------------------------------------------------------------------

namespace Sequoia.EMS.Core.Logging
{
    #region Using directives

    using System;
    using System.Data.SqlTypes;

    #endregion

    /// <summary>
    ///     EventLogItem is a representation of the Event Log table in the 
    ///     databases for profiles and elections.
    /// </summary>
    /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
    ///     Class created
    /// </revision>
    public class EventLogItem
    {
        #region Fields

        /// <summary>
        ///     param for event log item id
        /// </summary>
        private int id = 0;

        /// <summary>
        ///     param for componenet id
        /// </summary>
        private int componentId = 0;

        /// <summary>
        ///     param for when the event occurred
        /// </summary>
        private DateTime eventDate = (DateTime)SqlDateTime.Null;

        /// <summary>
        ///     param for the user logged in when the event happened
        /// </summary>
        private int userId = 0;

        /// <summary>
        ///     param for the workstation on which the event happened
        /// </summary>
        private string workstationName = string.Empty;

        /// <summary>
        ///     param for a description of the event
        /// </summary>
        private string description = string.Empty;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="EventLogItem"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public EventLogItem()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="EventLogItem"/> class.
        /// </summary>
        /// <param name="componentId">The component id.</param>
        /// <param name="description">The description.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="workstationName">Name of the workstation.</param>
        /// <externalUnit cref="ComponentId"/>
        /// <externalUnit cref="Description"/>
        /// <externalUnit cref="UserId"/>
        /// <externalUnit cref="WorkstationName"/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public EventLogItem(
            int componentId, 
            string description, 
            int userId, 
            string workstationName)
        {
            // set properties
            this.ComponentId = componentId;
            this.Description = description;
            this.UserId = userId;
            this.WorkstationName = workstationName;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        /// <value>The event log item id.</value>
        /// <externalUnit cref="id"/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
        }

        /// <summary>
        ///     Gets or sets the component id.
        /// </summary>
        /// <value>The component id.</value>
        /// <externalUnit cref="componentId"/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public int ComponentId
        {
            get { return this.componentId; }
            set { this.componentId = value; }
        }

        /// <summary>
        ///     Gets or sets the event date.
        /// </summary>
        /// <value>The event date.</value>
        /// <externalUnit cref="eventDate"/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public DateTime EventDate
        {
            get { return this.eventDate; }
            set { this.eventDate = value; }
        }

        /// <summary>
        ///     Gets or sets the user id.
        /// </summary>
        /// <value>The user id.</value>
        /// <externalUnit cref="userId"/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public int UserId
        {
            get { return this.userId; }
            set { this.userId = value; }
        }

        /// <summary>
        ///     Gets or sets the name of the workstation.
        /// </summary>
        /// <value>The name of the workstation.</value>
        /// <externalUnit cref="workstationName"/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public string WorkstationName
        {
            get { return this.workstationName; }
            set { this.workstationName = value; }
        }

        /// <summary>
        ///     Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        /// <externalUnit cref="description"/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }

        #endregion

        #region Methods

        #endregion
    }
}
