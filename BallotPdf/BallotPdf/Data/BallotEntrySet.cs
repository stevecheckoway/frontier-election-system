// -----------------------------------------------------------------------------
// <copyright file="BallotEntrySet.cs" company="Sequoia Voting Systems">
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//   This file contains the BallotEntrySet class.
// </summary>
// <revision revisor="dev11" date="12/18/2008" version="1.0.0.0">
//    File Created
// </revision>
// <revision revisor="dev16" date="1/11/2009" version="1.0.0.0">
//    File Modified
// </revision>
// <revision revisor="dev14" date="3/13/2009" version="1.0.0.2301">
//    File Modified
// </revision>
// <revision revisor="dev11" date="03/23/2009" version="1.0.9.0701">
//    File modified
// </revision>
// <revision revisor="dev05" date="10/19/09" version="1.1.1.19">
//     File modified.
// </revision>
// -----------------------------------------------------------------------------

namespace Sequoia.Ballot.Data
{
    #region Using directives

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;

    using Sequoia.Ems.Data.Custom;

    #endregion

    /// <summary>
    ///     Represents a Ballot Entry Set
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev11" date="12/18/2008" version="1.0.0.0">
    ///     Class created.
    /// </revision>
    public class BallotEntrySet : BaseEntrySet
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="BallotEntrySet"/> class.
        /// </summary>
        /// <param name="type">
        ///     The Ballot entry type.
        /// </param>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="12/18/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public BallotEntrySet(Type type) : base(type)
        {
        }

        #endregion

        #region Enums

        /// <summary>
        ///     This enum type handles the upCandidateDisplayTextList stored 
        ///     proc on the database.
        /// </summary>
        /// <revision revisor="dev11" date="04/15/2009" version="1.0.11.0501">
        ///     members renamed to match stored proc columns
        /// </revision>
        public enum StpCand
        {
            /// <summary>
            ///     candidate id
            /// </summary>
            CandidateId, 

            /// <summary>
            ///     candidate display name
            /// </summary>
            CandidateDisplayName, 

            /// <summary>
            ///     candidate type
            ///     0 - standard
            ///     3 - write-in
            ///     4 - response
            ///     See <see cref="DomainObjects.CandidateType"/>
            /// </summary>
            CandidateType
        }

        /// <summary>
        ///     This enum type handles the upContestDisplayTestList stored proc
        ///     on the database. Lists contests for ballot creation.
        /// </summary>
        /// <revision revisor="dev11" date="04/15/2009" version="1.0.11.0501">
        ///     members renamed to match stored proc names
        /// </revision>
        public enum StpCont
        {
            /// <summary>
            ///     contest id
            /// </summary>
            ContestId, 

            /// <summary>
            ///     contest display text
            /// </summary>
            DisplayText, 

            /// <summary>
            ///     contest text order (top to bottom)
            /// </summary>
            TextOrder
        }

        /// <summary>
        ///     This enum type handles the upBallotDefinitionGet stored proc on 
        ///     the database. Lists ballots, contests and candidates for ballot 
        ///     creation.
        /// </summary>
        /// <revision revisor="dev11" date="04/15/2009" version="1.0.11.0501">
        ///     members renames to match stored proc columns
        /// </revision>
        public enum StpBallot
        {
            /// <summary>
            ///     ballot style id
            /// </summary>
            BallotStyleId, 

            /// <summary>
            ///     contest id present on the ballot
            /// </summary>
            ContestId, 

            /// <summary>
            ///     contest list order
            /// </summary>
            ContestLO, 

            /// <summary>
            ///     contest display format (bit-wise integer value)
            /// </summary>
            ContDispFormat, 

            /// <summary>
            ///     candidate id
            /// </summary>
            CandidateId, 

            /// <summary>
            ///     candidate list order
            /// </summary>
            CandLO, 

            /// <summary>
            ///     candidate display format (bit-wise integer value)
            /// </summary>
            CandDispFormat
        }

        /// <summary>
        ///     This enum type refers to the parameter collection neede to 
        ///     generate the ballot PDFs. Handles the upPdfParamList stored proc 
        ///     on the database.
        /// </summary>
        public enum StpParam
        {
            /// <summary>
            ///     parameter id
            /// </summary>
            PDFLayoutParamId, 

            /// <summary>
            ///     parameter name
            /// </summary>
            ParamName, 

            /// <summary>
            ///     parameter description
            /// </summary>
            ParamDescription, 

            /// <summary>
            ///     parameter value (as string)
            /// </summary>
            ParamValue
        }

        /// <summary>
        ///     Lists candidates
        /// </summary>
        /// <revision revisor="dev11" date="04/15/2009" version="1.0.11.0501">
        ///     members renamed to match stored proc columns
        /// </revision>
        public enum StpCandList
        {
            /// <summary>
            ///     candidate id
            /// </summary>
            CandidateId, 

            /// <summary>
            ///     candidate list order
            /// </summary>
            CandidateLO, 

            /// <summary>
            ///     contest id for this candidate
            /// </summary>
            ContestId, 

            /// <summary>
            ///     candidate display name
            /// </summary>
            CandidateDisplayName, 

            /// <summary>
            ///     candidate type
            /// </summary>
            CandidateType, 

            /// <summary>
            ///     candidate party id
            /// </summary>
            PartyId, 

            /// <summary>
            ///     candidate ballot status id
            /// </summary>
            BallotStatusId
        }

        /// <summary>
        ///     Lists contests
        /// </summary>
        /// <revision revisor="dev11" date="04/15/2009" version="1.0.11.0501">
        ///     members renamed to match stored proc columns
        /// </revision>
        public enum StpContList
        {
            /// <summary>
            ///     contest id
            /// </summary>
            ContestId, 

            /// <summary>
            ///     contest vote for
            /// </summary>
            VoteFor, 

            /// <summary>
            ///     contest name
            /// </summary>
            ContestName, 

            /// <summary>
            ///     contest type
            /// </summary>
            ContestType, 

            /// <summary>
            ///     contest party id
            /// </summary>
            PartyId, 

            /// <summary>
            ///     contest ballot status id
            /// </summary>
            BallotStatusId, 

            /// <summary>
            ///     contest list order
            /// </summary>
            ListOrder
        }

        /// <summary>
        ///     Lists machine options
        /// </summary>
        /// <revision revisor="dev11" date="04/15/2009" version="1.0.11.0501">
        ///     members renamed to match stored proc columns
        /// </revision>
        public enum StpMachOptn
        {
            /// <summary>
            ///     machine option id
            /// </summary>
            MachineOptionId, 

            /// <summary>
            ///     machine option name
            /// </summary>
            OptionName, 

            /// <summary>
            ///     machine option description
            /// </summary>
            OptionDescription, 

            /// <summary>
            ///     machine option value
            /// </summary>
            OptionValue
        }

        /// <summary>
        ///     Lists XML files used for machine initialization
        /// </summary>
        /// <revision revisor="dev11" date="04/15/2009" version="1.0.11.0501">
        ///     members renamed to match stored proc columns
        /// </revision>
        public enum StpMachParm
        {
            /// <summary>
            ///     machine parameter name (XML file name)
            /// </summary>
            ParamName
        }

        /// <summary>
        ///     Lists election wide parameters
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev16" date="1/11/2009" version="1.0.0.0">
        ///     Enum Created
        /// </revision>
        public enum StpElecParam
        {
            /// <summary>
            ///     election parameter name
            /// </summary>
            ParamName, 

            /// <summary>
            ///     election parameter value
            /// </summary>
            ParamValue
        }

        /// <summary>
        ///     Lists a mapping between ballot style ids and precinct ids
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="2/28/2009" version="1.0.8.1101">
        ///     Enum Created
        /// </revision>
        public enum StpBaltPrec
        {
            /// <summary>
            ///     the ballot style id
            /// </summary>
            BallotStyleId, 

            /// <summary>
            ///     The precinct id
            /// </summary>
            PrecinctId, 

            /// <summary>
            ///     ballot style name
            /// </summary>
            BallotStyleName, 

            /// <summary>
            ///     precinct name
            /// </summary>
            PrecinctName
        }

        /// <summary>
        ///     Lists all parties
        /// </summary>
        /// <revision revisor="dev11" date="04/15/2009" version="1.0.11.0501">
        ///     members renamed to match stored proc columns
        /// </revision>
        public enum StpParty
        {
            /// <summary>
            ///     The party id
            /// </summary>
            PartyId, 

            /// <summary>
            ///     party full name
            /// </summary>
            PartyName, 

            /// <summary>
            ///     party short name
            /// </summary>
            Abbreviation, 

            /// <summary>
            ///     party list order
            /// </summary>
            ListOrder
        }

        /// <summary>
        ///     Lists all parameters that customize the appearance and settings 
        ///     of voting target.
        /// </summary>
        /// <revision revisor="dev11" date="04/15/2009" version="1.0.11.0501">
        ///     members renamed to match stored proc columns
        /// </revision>
        public enum StpTargetParam
        {
            /// <summary>
            ///     The target param Id
            /// </summary>
            TargetParamId, 

            /// <summary>
            ///     The param name
            /// </summary>
            ParamName, 

            /// <summary>
            ///     the param description
            /// </summary>
            ParamDescription, 

            /// <summary>
            ///     The target param value
            /// </summary>
            TargetParamValue
        }

        #endregion

        #region Public Methods

        #endregion

        #region Public Events

        #endregion

        #region Fields

        #endregion

        #region Overrides of BaseEntrySet

        /// <summary>
        ///     Binds types to initializers
        /// </summary>
        /// <externalUnit cref="BaseEntrySet.initMethods"/>
        /// <externalUnit cref="InitStpBallotSql"/>
        /// <externalUnit cref="InitStpBaltPrecSql"/>
        /// <externalUnit cref="InitStpCandListSql"/>
        /// <externalUnit cref="InitStpCandSql"/>
        /// <externalUnit cref="InitStpContListSql"/>
        /// <externalUnit cref="InitStpContSql"/>
        /// <externalUnit cref="InitStpElecParamSql"/>
        /// <externalUnit cref="InitStpMachOptnSql"/>
        /// <externalUnit cref="InitStpMachParmSql"/>
        /// <externalUnit cref="InitStpParamSql"/>
        /// <externalUnit cref="InitStpPartySql"/>
        /// <externalUnit cref="InitStpTargetParamSql"/>
        /// <externalUnit cref="StpBallot"/>
        /// <externalUnit cref="StpBaltPrec"/>
        /// <externalUnit cref="StpCand"/>
        /// <externalUnit cref="StpCandList"/>
        /// <externalUnit cref="StpCont"/>
        /// <externalUnit cref="StpContList"/>
        /// <externalUnit cref="StpElecParam"/>
        /// <externalUnit cref="StpMachOptn"/>
        /// <externalUnit cref="StpMachParm"/>
        /// <externalUnit cref="StpParam"/>
        /// <externalUnit cref="StpParty"/>
        /// <externalUnit cref="StpTargetParam"/>
        /// <revision revisor="dev11" date="12/18/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev16" date="1/11/2009" version="1.0.0.0">
        ///     Added Election Parameter file StpElecParam
        /// </revision>
        /// <revision revisor="dev11" date="2/28/2009" version="1.0.8.1101">
        ///     Added ballot-precinct initialization (StpBaltPrec)
        /// </revision>
        /// <revision revisor="dev11" date="03/11/2009" version="1.0.8.2201">
        ///     Added party initialization (StpParty)</revision>
        /// <revision revisor="dev11" date="03/23/2009" version="1.0.9.0701">
        ///     added target parameter initialization. 
        ///     See <see cref="StpTargetParam"/>
        /// </revision>
        protected override void InitializeType()
        {
            this.initMethods.Add(typeof(StpCand), this.InitStpCandSql);
            this.initMethods.Add(typeof(StpCont), this.InitStpContSql);
            this.initMethods.Add(typeof(StpBallot), this.InitStpBallotSql);
            this.initMethods.Add(typeof(StpParam), this.InitStpParamSql);
            this.initMethods.Add(typeof(StpCandList), this.InitStpCandListSql);
            this.initMethods.Add(typeof(StpContList), this.InitStpContListSql);
            this.initMethods.Add(typeof(StpMachOptn), this.InitStpMachOptnSql);
            this.initMethods.Add(typeof(StpMachParm), this.InitStpMachParmSql);
            this.initMethods.Add(
                typeof(StpElecParam), this.InitStpElecParamSql);
            this.initMethods.Add(typeof(StpBaltPrec), this.InitStpBaltPrecSql);
            this.initMethods.Add(typeof(StpParty), this.InitStpPartySql);
            this.initMethods.Add(
                typeof(StpTargetParam), this.InitStpTargetParamSql);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Initializes the entry set for reading the candidate info from the
        /// database
        /// </summary>
        /// <externalUnit cref="BaseEntrySet.hstSql"/>
        /// <externalUnit cref="BaseEntrySet.sqlLoad"/>
        /// <revision revisor="dev11" date="12/18/2008" version="1.0.0.0">
        /// Member Created</revision>
        private void InitStpCandSql()
        {
            // no modifications will be performed to this entry set,
            // so the hashtable is empty
            this.hstSql = new Hashtable();

            // name of the stored procedure
            this.sqlLoad = "upCandidateDisplayTextList";

            // since no parameters are required by the stored procedure
            // no need to initialize (parameters) and (typeMap)
        }

        /// <summary>
        /// Initializes the entry set for reading the contest info from the
        /// database
        /// </summary>
        /// <externalUnit cref="BaseEntrySet.hstSql"/>
        /// <externalUnit cref="BaseEntrySet.sqlLoad"/>
        /// <revision revisor="dev11" date="12/18/2008" version="1.0.0.0">
        /// Member Created</revision>
        private void InitStpContSql()
        {
            // no modifications will be performed to this entry set,
            // so the hashtable is empty
            this.hstSql = new Hashtable();

            // name of the stored procedure
            this.sqlLoad = "upContestDisplayTextList";

            // since no parameters are required by the stored procedure
            // no need to initialize (parameters) and (typeMap)
        }

        /// <summary>
        /// Initializes the entry set for reading the ballot info from the
        /// database
        /// </summary>
        /// <externalUnit cref="BaseEntrySet.hstSql"/>
        /// <externalUnit cref="BaseEntrySet.sqlLoad"/>
        /// <externalUnit dbref="upBallotDefinitionGet"/>
        /// <revision revisor="dev11" date="12/18/2008" version="1.0.0.0">
        /// Member Created</revision>
        /// <revision revisor="dev14" date="3/13/2009" version="1.0.8.2301">
        /// Added ReportGroupId to initialization of proc
        /// </revision>
        private void InitStpBallotSql()
        {
            // no modifications will be performed to this entry set,
            // so the hashtable is empty
            this.hstSql = new Hashtable();

            // name of the stored procedure
            this.sqlLoad = "upBallotDefinitionGet";

            // parameters on the stored procedure 
            this.parameters = new[] { "@ReportGroupId " };

            // data types for the parameters on the store procedure 
            this.typeMap = new[] { SqlDbType.Int };
        }

        /// <summary>
        /// Initializes the entry set for reading the parameters from the
        /// database for the PDF generation
        /// </summary>
        /// <externalUnit cref="BaseEntrySet.hstSql"/>
        /// <externalUnit cref="BaseEntrySet.sqlLoad"/>
        /// <revision revisor="dev11" date="12/22/2008" version="1.0.0.0">
        /// Member Created</revision>
        /// <revision revisor="dev11" date="03/04/2009" version="1.0.8.1501">
        /// Added initialization for writing operations</revision>
        private void InitStpParamSql()
        {
            // no modifications will be performed to this entry set,
            // so the hashtable is empty
            this.hstSql = new Hashtable();

            // name of the stored procedure
            this.sqlLoad = "upPdfParamList";

            // since no parameters are required by the stored procedure
            // no need to initialize (parameters) and (typeMap)
            this.esuInfo = new EntrySetUpdate("upPdfParamUpdate");
            Dictionary<string, SqlDbType> parms = this.esuInfo.SqlParams;
            parms.Add("@PdfParamId", SqlDbType.Int);
            parms.Add("@ParamName", SqlDbType.Text);
            parms.Add("@ParamDescription", SqlDbType.Text);
            parms.Add("@ParamValue", SqlDbType.Int);
        }
        
        /// <summary>
        /// Initializes the entry set for loading the entire list of candidates
        /// with properties. See <see cref="StpCandList"/>
        /// </summary>
        /// <externalUnit cref="BaseEntrySet.hstSql"/>
        /// <externalUnit cref="BaseEntrySet.sqlLoad"/>
        /// <revision revisor="dev11" date="12/23/2008" version="1.0.0.0">
        /// Member Created</revision>
        /// <revision revisor="dev11" date="04/14/2009" version="1.0.11.0401">
        /// added report group id as parameter
        /// </revision>
        private void InitStpCandListSql()
        {
            // no modifications will be performed to this entry set,
            // so the hashtable is empty
            this.hstSql = new Hashtable();

            // name of the stored procedure
            this.sqlLoad = "upCandidateList";

            // parameters on the stored procedure 
            this.parameters = new[] { "@ReportGroupId " };

            // data types for the parameters on the store procedure 
            this.typeMap = new[] { SqlDbType.Int };
        }

        /// <summary>
        /// Initializes the entry set for loading the entire list of contests
        /// with properties. See <see cref="StpContList"/>
        /// </summary>
        /// <externalUnit cref="BaseEntrySet.hstSql"/>
        /// <externalUnit cref="BaseEntrySet.sqlLoad"/>
        /// <revision revisor="dev11" date="12/23/2008" version="1.0.0.0">
        /// Member Created</revision>
        /// <revision revisor="dev11" date="04/14/2009" version="1.0.11.0401">
        /// added report group id as parameter
        /// </revision>
        private void InitStpContListSql()
        {
            // no modifications will be performed to this entry set,
            // so the hashtable is empty
            this.hstSql = new Hashtable();

            // name of the stored procedure
            this.sqlLoad = "upContestList";

            // parameters on the stored procedure 
            this.parameters = new[] { "@ReportGroupId " };

            // data types for the parameters on the store procedure 
            this.typeMap = new[] { SqlDbType.Int };
        }

        /// <summary>
        /// Initializes the entry set for loading the machine options list
        /// See <see cref="StpMachOptn"/>
        /// </summary>
        /// <externalUnit cref="BaseEntrySet.hstSql"/>
        /// <externalUnit cref="BaseEntrySet.sqlLoad"/>
        /// <revision revisor="dev11" date="12/23/2008" version="1.0.0.0">
        /// Member Created</revision>
        private void InitStpMachOptnSql()
        {
            // no modifications will be performed to this entry set,
            // so the hashtable is empty
            this.hstSql = new Hashtable();

            // name of the stored procedure
            this.sqlLoad = "upMachineOptionList";

            // since no parameters are required by the stored procedure
            // no need to initialize (parameters) and (typeMap)
        }

        /// <summary>
        /// Initializes the entry set for loading the machine  parameters list
        /// See <see cref="StpMachParm"/>
        /// </summary>
        /// <externalUnit cref="BaseEntrySet.hstSql"/>
        /// <externalUnit cref="BaseEntrySet.sqlLoad"/>
        /// <revision revisor="dev11" date="1/12/2009" version="1.0.0.0">
        /// Member Created</revision>
        private void InitStpMachParmSql()
        {
            // no modifications will be performed to this entry set,
            // so the hashtable is empty
            this.hstSql = new Hashtable();

            // name of the stored procedure
            this.sqlLoad = "upMachineParamList";

            // since no parameters are required by the stored procedure
            // no need to initialize (parameters) and (typeMap)
        }

        /// <summary>
        /// Initializes the entry set for loading the election parameters list
        /// See <see cref="StpElecParam"/>
        /// </summary>
        /// <externalUnit cref="BaseEntrySet.hstSql"/>
        /// <externalUnit cref="BaseEntrySet.sqlLoad"/>
        /// <revision revisor="dev16" date="1/11/2009" version="1.0.0.0">
        /// Member Created
        /// </revision>
        /// <revision revisor="dev14" date="3/13/2009" version="1.0.8.23">
        /// Passing in machine assignment id to the proc
        /// </revision>
        /// <revision revisor="dev11" date="10/02/2009" version="1.0.18.0201">
        /// Added election id as 2nd paramenter
        /// </revision>
        /// <revision revisor="dev05" date="10/19/09" version="1.1.1.19">
        ///     Stored proc no longer takes election id parameter.
        /// </revision>
        private void InitStpElecParamSql()
        {
            // no modifications will be performed to this entry set,
            // so the hashtable is empty
            this.hstSql = new Hashtable();

            // name of the stored procedure
            this.sqlLoad = "upElectionParamList";

            // parameters on the stored procedure 
            this.parameters = new[] { "@MachineAssignmentId" };

            // data types for the parameters on the store procedure 
            this.typeMap = new[] { SqlDbType.Int };
        }

        /// <summary>
        /// Initializes the entry set for reading the ballot-precinct combinations
        /// from the database
        /// See <see cref="StpBaltPrec"/>
        /// </summary>
        /// <externalUnit cref="BaseEntrySet.hstSql"/>
        /// <externalUnit cref="BaseEntrySet.sqlLoad"/>
        /// <revision revisor="dev11" date="2/28/2009" version="1.0.8.1101">
        /// Member Created</revision>
        /// <revision revisor="dev14" date="3/13/2009" version="1.0.8.2301">
        /// Added ReportGroupId param to the initialization of the proc
        /// </revision>
        private void InitStpBaltPrecSql()
        {
            // no modifications will be performed to this entry set,
            // so the hashtable is empty
            this.hstSql = new Hashtable();

            // name of the stored procedure
            this.sqlLoad = "upBallotPrecinctList";

            // parameters on the stored procedure 
            this.parameters = new[] { "@ReportGroupId " };

            // data types for the parameters on the store procedure 
            this.typeMap = new[] { SqlDbType.Int };
        }

        /// <summary>
        /// Initializes the entry set for loading the list of parties from the
        /// database. See <see cref="StpParty"/>
        /// </summary>
        /// <externalUnit cref="BaseEntrySet.hstSql"/>
        /// <externalUnit cref="BaseEntrySet.sqlLoad"/>
        /// <revision revisor="dev11" date="3/11/2009" version="1.0.8.2201">
        /// Member Created</revision>
        private void InitStpPartySql()
        {
            // no modifications will be performed to this entry set,
            // so the hashtable is empty
            this.hstSql = new Hashtable();

            // name of the stored procedure
            this.sqlLoad = "upPartyList";

            // since no parameters are required by the stored procedure
            // no need to initialize (parameters) and (typeMap)
        }

        /// <summary>
        /// Initializes the entry set for loading the list of targets from the
        /// database. See <see cref="StpTargetParam"/>
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="3/23/2009" version="1.0.9.0701">
        /// Member Created</revision>
        private void InitStpTargetParamSql()
        {
            // no modifications will be performed to this entry set,
            // so the hashtable is empty
            this.hstSql = new Hashtable();

            // name of the stored procedure
            this.sqlLoad = "upTargetParameterList";

            // since no parameters are required by the stored procedure
            // no need to initialize (parameters) and (typeMap)
            this.parameters = new[] { "@TargetTypeId" };
            this.typeMap = new[] { SqlDbType.Int };
        }

        #endregion
    }
}
