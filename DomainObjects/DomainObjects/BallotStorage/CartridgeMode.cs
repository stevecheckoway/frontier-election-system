// -----------------------------------------------------------------------------
// <copyright file="CartridgeMode.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2008 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the CartridgeMode class.
// </summary>
// <revision revisor="dev06" date="10/13/2008" version="1.0.0.0">
//     File Created
// </revision> 
// <revision revisor="dev14" date="2/11/2009" version="1.0.6.6">
//     File Modified
// </revision>  
// <revision revisor="dev03" date="3/18/2009" version="1.0.9.2">
//     File Modified
// </revision>
// <revision revisor="dev14" date="5/5/2009" version="1.0.11.21">
//     File Modified
// </revision>  
// <revision revisor="dev06" date="5/11/2009" version="1.0.12.01">
//     File Modified
// </revision>  
// <revision revisor="dev13" date="8/24/2009" version="1.0.16.04">
//     File Modified
// </revision>  
// <revision revisor="dev05" date="09/11/09" version="1.0.17.4">
//     File modified.
// </revision>
// -----------------------------------------------------------------------------

namespace Sequoia.DomainObjects.BallotStorage
{
    #region Using directives

    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    using Sequoia.DomainObjects.Properties;
    
    #endregion

    /// <summary>
    ///     CartridgeMode is an object which represents various "Tally Mode"
    ///     recordsets in the cartridge store. The default modes are supported,
    ///     PreLat, Official, and PostLat, as well as any arbitrary number of
    ///     other modes: such as various recounts etc.
    /// </summary>
    /// <externalUnit cref="VoterRecord"/>
    /// <externalUnit cref="VoterRecordList"/>
    /// <revision revisor="dev06" date="10/13/2008" version="1.0.0.0">
    ///     Class created.
    /// </revision>
    /// <revision revisor="dev06" date="12/12/2008" version="1.0.1.0">
    ///     Updated to use ordered list for voter records instead of generic 
    ///     list
    /// </revision>
    /// <revision revisor="dev03" date="3/18/2009" version="1.0.9.2">
    ///     Added submitted property.
    /// </revision>
    /// <revision revisor="dev06" date="5/11/2009" version="1.0.12.01">
    ///     Removed submitted property - creating transmission log to track this 
    ///     sort of stuff.
    /// </revision>
    [Serializable]
    [XmlRoot("Mode")]
    public class CartridgeMode : ICloneable
    {
        #region Fields

        /// <summary>
        ///     Create var to store id for this mode
        /// </summary>
        private int id = 0;

        /// <summary>
        ///     Create var to store value for when the polls are opened for this 
        ///     mode - TODO: make nullable
        /// </summary>
        private DateTime opened = DateTime.MinValue;

        /// <summary>
        ///     Create var to store value for when the polls are closed for this 
        ///     mode - TODO: make nullable
        /// </summary>
        private DateTime closed = DateTime.MinValue;

        /// <summary>
        ///     Create var to store count of voters
        /// </summary>
        private int voterCount = 0;

        /// <summary>
        ///     Create field to store voter records - order by session id.
        /// </summary>
        private SortedList<string, VoterRecord> orderedVoterRecords =
            new SortedList<string, VoterRecord>();

        /// <summary>
        ///     param for voter record list
        /// </summary>
        private VoterRecordList voterRecords = new VoterRecordList();

        #endregion
        
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="CartridgeMode"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev06" date="10/13/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public CartridgeMode()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="CartridgeMode"/> class.
        /// </summary>
        /// <param name="id">The cartridge mode id.</param>
        /// <externalUnit cref="Id"/>
        /// <revision revisor="dev06" date="10/13/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public CartridgeMode(int id)
        {
            this.Id = id;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        /// <value>The cartridge mode id.</value>
        /// <externalUnit cref="id"/>
        /// <revision revisor="dev06" date="10/13/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        [XmlAttribute("Id")]
        public int Id
        {
            get
            {
                return this.id;
            }

            set
            {
                this.id = value;
            }
        }

        /// <summary>
        ///     Gets or sets the date and time the polls were opened for this 
        ///     mode.
        /// </summary>
        /// <value>The opened.</value>
        /// <externalUnit cref="opened"/>
        /// <revision revisor="dev06" date="10/13/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        [XmlAttribute("Opened")]
        public DateTime Opened
        {
            get
            {
                return this.opened;
            }

            set
            {
                this.opened = value;
            }
        }

        /// <summary>
        ///     Gets or sets the date and time the polls were closed for this 
        ///     mode.
        /// </summary>
        /// <value>The closed.</value>
        /// <externalUnit cref="closed"/>
        /// <revision revisor="dev06" date="10/13/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        [XmlAttribute("Closed")]
        public DateTime Closed
        {
            get
            {
                return this.closed;
            }

            set
            {
                this.closed = value;
            }
        }

        /// <summary>
        ///     Gets or sets the voter count.
        /// </summary>
        /// <value>The voter count.</value>
        /// <externalUnit cref="voterCount"/>
        /// <revision revisor="dev06" date="10/13/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        [XmlAttribute("VoterCount")]
        public int VoterCount
        {
            get
            {
                // get real value from collection - but only if not null
                if (this.VoterRecords != null)
                {
                    this.voterCount = this.VoterRecords.Count;
                }

                // return the voter count
                return this.voterCount;
            }

            set
            {
                this.voterCount = value;
            }
        }

        /// <summary>
        ///     Gets or sets the voter records.
        /// </summary>
        /// <value>The voter records.</value>
        /// <externalUnit cref="voterRecords"/>
        /// <externalUnit cref="VoterRecordList"/>
        /// <exception cref="ArgumentException">
        ///     This will be thrown if code attempts to set the VoterRecord 
        ///     collection to null.
        /// </exception>
        /// <revision revisor="dev06" date="10/13/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        [XmlArray("VoterRecords")]
        [XmlArrayItem("VoterRecord")]
        public VoterRecordList VoterRecords
        {
            get
            {
                return this.voterRecords;
            }

            set
            {
                // make sure incoming value is not null
                if (value == null)
                {
                    throw new ArgumentException(
                        Resources.VoterRecordCannotBeNullMessage);
                }

                this.voterRecords = value;
            }
        }

        #endregion

        #region Public Methods

        #region CanRecordsBeAdded

        /// <summary>
        ///     Determines whether records can be added to this instance.
        /// </summary>
        /// <returns>
        ///  <c>true</c> if records can be added to this instance; 
        ///     otherwise, <c>false</c>.
        /// </returns>
        /// <externalUnit cref="Opened"/>
        /// <externalUnit cref="Closed"/>
        /// <revision revisor="dev06" date="10/13/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public bool CanRecordsBeAdded()
        {
            // we need to check whether or not this mode can accept voter 
            // records make sure the polls have been opened, we are not before 
            // the opening time for some reason, and that the polls are 
            // not closed
            bool arePollsOpened =
                (this.Opened.Equals(DateTime.MinValue) == false
                    && this.Opened <= DateTime.Now);

            // check if the polls have been closed
            bool arePollsClosedYet = !this.Closed.Equals(DateTime.MinValue);

            // return whether or not we can add records still
            return arePollsOpened && !arePollsClosedYet;
        }

        #endregion

        /// <summary>
        ///     Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        ///     A new object that is a copy of this instance.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="2/11/2009" version="1.0.6.6">
        ///     Member Created
        /// </revision>
        public object Clone()
        {
            var clonedMode = (CartridgeMode)MemberwiseClone();
            var clonedVoterRecords = new VoterRecordList();
            foreach (var record in this.VoterRecords)
            {
                clonedVoterRecords.Add(record.Clone() as VoterRecord);
            }

            clonedMode.VoterRecords = clonedVoterRecords;
            return clonedMode;
        }

        /// <summary>
        ///     Gets the clean XML.
        /// </summary>
        /// <returns>
        ///     A string with the clean XML
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="5/5/2009" version="1.0.11.21">
        ///     Member Updated: Made the method public instead of internal
        /// </revision>
        public string GetCleanXml()
        {
            string dirtyXml = this.ToString();

            return
                dirtyXml.Replace(@"\", string.Empty).Replace(
                    "\r\n", string.Empty).Replace(
                    "<?xml version=\"1.0\" encoding=\"utf-16\"?>",
                    string.Empty);
        }

        #region ToString

        /// <summary>
        ///     Returns a <see cref="T:System.String"/> that represents the 
        ///     current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.String"/> that represents the 
        ///     current <see cref="T:System.Object"/>.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="10/16/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public override string ToString()
        {
            // create error message for failed serialization
            const string FailureMessage =
                "Machine cartridge mode failed to serialize.";

            // create return param and set to failure by default
            string serializedCartridge = FailureMessage;

            try
            {
                // serialize this object as an xml formatted string
                // create the xml serializer based on this object
                XmlSerializer serializer =
                    new XmlSerializer(typeof(CartridgeMode));

                // create a stringbuilder which we will use to send the 
                // serialized output
                StringBuilder xmlBuilder = new StringBuilder();

                // create the xmlwriter using the stringbuilder
                StringWriter writer = new StringWriter(xmlBuilder);

                // serialize this object to the writer
                serializer.Serialize(writer, this);

                // close the xml writer
                writer.Close();

                // set the cartridge data
                serializedCartridge = xmlBuilder.ToString();
            }
            catch (Exception)
            {
            }

            // return the report or failure message
            return serializedCartridge;
        }

        #endregion

        #region AddVoterRecord
        /// <summary>
        ///     Adds the voter record to this instance.
        /// </summary>
        /// <param name="voterRecord">The voter record.</param>
        /// <externalUnit cref="VoterRecord"/>
        /// <revision revisor="dev06" date="10/13/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev06" date="12/12/2008" version="1.0.1.0">
        ///     Changed voter reocrd store to ordered list.
        /// </revision>
        public void AddVoterRecord(VoterRecord voterRecord)
        {
            if (this.orderedVoterRecords.ContainsKey(
                voterRecord.Id.ToString() + "_" +
                voterRecord.CardId.ToString()) == false)
            {
                // add the voter record
                this.orderedVoterRecords.Add(
                    voterRecord.Id + "_" + voterRecord.CardId.ToString(),
                    voterRecord);

                int index = this.orderedVoterRecords.IndexOfKey(
                    voterRecord.Id.ToString() + "_" +
                    voterRecord.CardId.ToString());
                this.voterRecords.Insert(index, voterRecord);
            }
        }
        #endregion

        #region GetPollStatusMessage
        /// <summary>
        ///     Gets the poll status message.
        /// </summary>
        /// <returns>
        ///     A string containing the state of the polls and why voter 
        ///     records cannot be addded at the current time.
        /// </returns>
        /// <externalUnit cref="Opened"/>
        /// <externalUnit cref="Closed"/>
        /// <externalUnit cref="Resources"/>
        /// <revision revisor="dev06" date="10/13/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public string GetPollStatusMessage()
        {
            // create return string
            string pollStatusMessage = string.Empty;

            // check to see if the polls are opened
            bool arePollsOpened =
                (this.Opened.Equals(null) == false
                    && this.Opened < DateTime.Now);

            // check if the polls have been closed
            bool arePollsClosedYet = !this.Closed.Equals(null);

            // check the polls state
            if (arePollsOpened == false)
            {
                // set the message as the polls have not been opened yet
                pollStatusMessage =
                    Resources.CartridgeModePollsNotOpenYetMessage;
            }
            else if (string.IsNullOrEmpty(pollStatusMessage) == true
                && arePollsClosedYet == true)
            {
                // set the message as the polls are already closed
                pollStatusMessage = Resources.CartridgeModePollsClosedMessage;
            }

            return pollStatusMessage;
        }
        #endregion

        /// <summary>
        ///     Opens the polls.
        /// </summary>
        /// <param name="openTime">The open time.</param>
        /// <returns>
        ///     <c>true</c> if openeed; otherwise <c>false</c>
        /// </returns>
        /// <externalUnit cref="Opened"/>
        /// <externalUnit cref="Closed"/>
        /// <revision revisor="dev13" date="10/16/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev06" date="12/17/2008" version="1.0.2.0">
        ///     Added param for open time.
        /// </revision>
        public bool OpenPolls(DateTime openTime)
        {
            bool pollsOpened = false;

            if (this.Opened == DateTime.MinValue
                && this.Closed == DateTime.MinValue)
            {
                this.Opened = openTime;
                pollsOpened = true;
            }

            return pollsOpened;
        }

        /// <summary>
        ///     Gets the polls opened message.
        /// </summary>
        /// <returns>a formatted string with the polls opened message.</returns>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="10/16/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public string GetPollsOpenedMessage()
        {
            return string.Format(Resources.PollsOpenedMessage, this.Opened);
        }

        /// <summary>
        ///     Gets the polls operation failed message.
        /// </summary>
        /// <returns>a string with the polls operation failed message.</returns>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="10/16/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public string GetPollsOperationFailedMessage()
        {
            // check to see if the polls have already been opened or if they are 
            // already closed 
            // create return string
            string pollStatusMessage = string.Empty;

            // check to see if the polls are opened
            bool arePollsOpened =
                (this.Opened.Equals(DateTime.MinValue) == false
                    && this.Opened < DateTime.Now);

            // check if the polls have been closed
            bool arePollsClosedYet = !this.Closed.Equals(DateTime.MinValue);

            // check the polls state
            if (arePollsOpened == true && arePollsClosedYet == false)
            {
                // set the message as the polls have not been opened yet
                pollStatusMessage =
                    string.Format(
                        Resources.CartridgeModePollsAlreadyOpenedMessage,
                        this.Id,
                        this.Opened);
            }
            else if (arePollsOpened == true && arePollsClosedYet == true)
            {
                // set the message as the polls have not been opened yet
                pollStatusMessage =
                    string.Format(
                        Resources.CartridgeModePollsAlreadyClosedMessage,
                        this.Id,
                        this.Closed);
            }
            else if (arePollsOpened == false && arePollsClosedYet == false)
            {
                pollStatusMessage = string.Format(
                    Resources.CartridgeModePollsNotOpenYetMessage, this.Id);
            }
            else if (string.IsNullOrEmpty(pollStatusMessage) == true
                && arePollsClosedYet == true)
            {
                // set the message as the polls are already closed
                pollStatusMessage =
                    string.Format(
                        Resources.CartridgeModePollsAlreadyClosedMessage,
                        this.Id,
                        this.Closed);
            }

            return pollStatusMessage;
        }

        /// <summary>
        ///     Closes the polls.
        /// </summary>
        /// <param name="closeTime">The close time.</param>
        /// <returns>
        ///     <c>true</c> if polls close; otherwise <c>false</c>.
        /// </returns>
        /// <externalUnit cref="Opened"/>
        /// <externalUnit cref="Closed"/>
        /// <revision revisor="dev13" date="10/16/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev13" date="12/17/2008" version="1.0.2.0">
        ///     Added close time param
        /// </revision>
        public bool ClosePolls(DateTime closeTime)
        {
            bool pollsClosed = false;

            if (this.Opened != DateTime.MinValue
                && this.Opened < closeTime
                && this.Closed == DateTime.MinValue)
            {
                this.Closed = closeTime;
                pollsClosed = true;
            }

            return pollsClosed;
        }

        /// <summary>
        ///     Gets the polls closed message.
        /// </summary>
        /// <returns>
        ///     string with polls closed message
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="10/16/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public string GetPollsClosedMessage()
        {
            return string.Format(Resources.PollsClosedMessage, this.Closed);
        }

        #endregion

        #region Public Events

        #endregion

        #region Private Methods

        #endregion
    }
}
