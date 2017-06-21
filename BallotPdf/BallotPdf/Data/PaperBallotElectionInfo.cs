// -----------------------------------------------------------------------------
// <copyright file="PaperBallotElectionInfo.cs" company="Sequoia Voting Systems"> 
//    Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//    Distribution of source code is allowable only under the terms of the
//    license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//    This file contains the PaperBallotElectionInfo class.
// </summary>
// <revision revisor="dev11" date="4/2/2009" version="1.0.9.1701">
//    File Created
// </revision>  
// -----------------------------------------------------------------------------

namespace Sequoia.Ballot.Data
{
    #region Using directives

    using System;

    using StpElecParam = BallotEntrySet.StpElecParam;

    #endregion

    /// <summary>
    ///     Class for PaperBallotElectionInfo
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev11" date="4/2/2009" version="1.0.9.1701">
    ///     Class created.
    /// </revision>
    public class PaperBallotElectionInfo
    {
        #region Constants

        /// <summary>
        ///     Default machine id
        /// </summary>
        public const int DefaultMachineId = 4;
        
        /// <summary>
        ///     name of parameter on the database
        /// </summary>
        private const string ParamElectionName = "ElectionName";

        /// <summary>
        ///     name of parameter on the database
        /// </summary>
        private const string ParamElectionDate = "ElectionDate";

        #endregion
        
        #region Fields

        /// <summary>
        ///     election name
        /// </summary>
        private string electionName;

        /// <summary>
        ///     election date
        /// </summary>
        private DateTime? electionDate;

        #endregion
        
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="PaperBallotElectionInfo"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="4/2/2009" version="1.0.9.1701">
        ///     Member Created
        /// </revision>
        public PaperBallotElectionInfo(string strConnection)
        {
            BallotEntrySet entrySet = new BallotEntrySet(typeof(StpElecParam));
            entrySet.Load(strConnection, DefaultMachineId);

            int idx = entrySet.FindIndex(
                StpElecParam.ParamName, ParamElectionName);
            if (idx >= 0)
            {
                this.electionName = entrySet.GetValueStr(
                    idx, StpElecParam.ParamValue);
            }
            else
            {
                // no election name found, use empty string
                this.electionName = string.Empty;
            }

            idx = entrySet.FindIndex(StpElecParam.ParamName, ParamElectionDate);
            try
            {
                this.electionDate = entrySet.GetValueDate(
                    idx, StpElecParam.ParamValue);
            }
            catch
            {
                // do nothing and let election date be null
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the name of the election.
        /// </summary>
        /// <value>The name of the election.</value>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="4/2/2009" version="1.0.9.1701">
        ///     Member Created
        /// </revision>
        public string ElectionName
        {
            get
            {
                return this.electionName;
            }
        }

        /// <summary>
        ///     Gets the election date.
        /// </summary>
        /// <value>The election date.</value>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="4/2/2009" version="1.0.9.1701">
        ///     Member Created
        /// </revision>
        public DateTime? ElectionDate
        {
            get
            {
                return this.electionDate;
            }
        }

        #endregion

        #region Public Methods

        #endregion

        #region Public Events

        #endregion

        #region Private Methods

        #endregion
    }
}
