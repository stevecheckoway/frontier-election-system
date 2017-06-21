// -----------------------------------------------------------------------------
// <copyright file="VoterSession.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2008 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the VoterSession class.
// </summary>
// <revision revisor = "dev05" date="01/18/09" version="1.0.4.19">
//     File Created
// </revision>
// <revision revisor = "dev05" date="01/17/09" version="1.0.4.18">
//     Replaced card list with card status list.
// </revision>
// <revision revisor="dev13" date="11/9/2008" version="1.0.?.0">
//     File modified
// </revision>
// <revision revisor="dev13" date="01/29/2009" version="1.0.5.10">
//     Added cards attribute for the number of cards
// </revision>
// <revision revisor="dev13" date="01/29/2009" version="1.0.5.14">
//     Moved voter session from voter activation solution to domain objects.
// </revision>
// <revision revisor="dev14" date="5/21/2009" version="1.0.12.11">
//     File Modified
// </revision>  
// -----------------------------------------------------------------------------

namespace Sequoia.DomainObjects
{
    #region Using directives

    using System;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;
    using Sequoia.DomainObjects.Persistence;

    #endregion

    /// <summary>
    ///     Serializable voter session object
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev13" date="11/9/2008" version="1.0.0.0">
    ///     Class created.
    /// </revision>
    /// <revision revisor="dev14" date="5/21/2009" version="1.0.12.11">
    ///     Removed private fields that weren't being used since 
    ///     auto Properties are used
    /// </revision>
    [Serializable]
    [XmlRoot("VoterSession")]
    public class VoterSession : IPersistible
    {
        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="VoterSession"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/11/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public VoterSession()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="VoterSession"/> class.
        /// </summary>
        /// <param name="sessionId">The session id.</param>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/9/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public VoterSession(int sessionId)
        {
            // this.Id = this.CreateNewId(sessionId);
            this.Id = sessionId;
        }

        #endregion

        #region Public Properties

        #region IPersistible Members

        /// <summary>
        ///     Gets the serialized data.
        /// </summary>
        /// <value>The serialized data.</value>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="12/15/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public string SerializedData
        {
            get
            {
                return this.ToString();
            }
        }

        #endregion
        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        /// <value>The voter session id.</value>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/9/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        [XmlAttribute("Id")]
        public int Id
        {
            get;
            set;
        }

        /// <summary>
        ///  Gets or sets the status.
        /// </summary>
        /// <value>The session status.</value>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/9/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        [XmlAttribute("Status")]
        public int Status
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the precinct.
        /// </summary>
        /// <value>The precinct id.</value>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/9/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        [XmlAttribute("Precinct")]
        public int Precinct
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the ballot id.
        /// </summary>
        /// <value>The ballot id.</value>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/9/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        [XmlAttribute("BallotId")]
        public int BallotId
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the cards.
        /// </summary>
        /// <value>The number of cards.</value>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="1/29/2009" version="1.0.5.10">
        ///     Member Created
        /// </revision>
        [XmlAttribute("Cards")]
        public int Cards
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the ID's and statuses of the cards the voter will
        ///     be voting.
        /// </summary>
        /// <value>The cards.</value>
        /// <externalUnit/>
        /// <revision revisor = "dev05" date="01/17/09" version="1.0.4.18">
        ///     Member created, replaces CardList.
        /// </revision>
        [XmlArray("CardStatuses")]
        [XmlArrayItem("CardStatus")]
        public CardStatusList CardStatuses
        {
            get;
            set;
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Returns a <see cref="T:System.String"/> that represents the 
        ///     current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.String"/> that represents the current 
        ///     <see cref="T:System.Object"/>.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/10/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public override string ToString()
        {
            // create error message to return if failed
            string failureMessage = "Failed to serialize voter session. ";

            // create return and set to error by default
            string result;

            try
            {
                // serialize this object as an xml formatted string
                // create the xml serializer based on this object
                var serializer =
                    new XmlSerializer(typeof(VoterSession));

                // create a stringbuilder which we will use to send the 
                // serialized output
                var xmlBuilder = new StringBuilder();

                // create the xmlwriter using the stringbuilder
                var writer = new StringWriter(xmlBuilder);

                // serialize this object to the writer
                serializer.Serialize(writer, this);

                // close the xml writer
                writer.Close();

                // set the voter session data
                result = xmlBuilder.ToString();
            }
            catch (Exception exception)
            {
                failureMessage += exception;
                result = failureMessage;
            }

            return result;
        }

        /// <summary>
        ///     Sets the persister.
        /// </summary>
        /// <param name="persister">The persister.</param>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="12/15/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public void SetPersister(IPersister persister)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Public Events

        #endregion
   }
}
