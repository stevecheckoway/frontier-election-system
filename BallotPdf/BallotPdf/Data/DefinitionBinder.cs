// -----------------------------------------------------------------------------
// <copyright file="DefinitionBinder.cs" company="Sequoia Voting Systems"> 
//    Copyright (c) 2008 Sequoia Voting Systems, Inc. All Rights Reserved.
//    Distribution of source code is allowable only under the terms of the
//    license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//    This file contains the DefinitionBinder class.
// </summary>
// <revision revisor="dev11" date="12/16/2008" version="1.0.0.0">
//    File Created
// </revision>  
// <revision revisor="dev16" date="1/11/2009" version="1.0.0.0">
//     File Modified
// </revision>
// <revision revisor="dev14" date="3/13/2009" version="1.0.8.21">
//     File Modified
// </revision>
// -----------------------------------------------------------------------------

namespace Sequoia.Ballot.Data
{
    #region Using directives

    using System;
    using System.Collections.Generic;

    using Sequoia.DomainObjects;
    using Sequoia.Utilities;
    using StpCandList = Sequoia.Ballot.Data.BallotEntrySet.StpCandList;
    using StpContList = Sequoia.Ballot.Data.BallotEntrySet.StpContList;
    using StpElecParam = Sequoia.Ballot.Data.BallotEntrySet.StpElecParam;
    using StpMachOptn = Sequoia.Ballot.Data.BallotEntrySet.StpMachOptn;
    using StpMachParm = Sequoia.Ballot.Data.BallotEntrySet.StpMachParm;

    #endregion

    /// <summary>
    ///     Class for the DefinitionBinder
    /// </summary>
    /// <revision revisor="dev11" date="12/16/2008" version="1.0.0.0">
    ///     Class created.
    /// </revision>
    /// <revision revisor="dev16" date="1/11/2009" version="1.0.0.0">
    ///     Added election Parameters support
    /// </revision>
    /// <revision revisor="dev11" date="03/18/2009" version="1.0.9.0201">
    ///     member renamed
    /// </revision>
    public class DefinitionBinder
    {
        #region Fields

        /// <summary>
        ///     A collection of <see cref="Ballot"/> that generates the 
        ///     Ballots.xml file that goes to the tabulator.
        /// </summary>
        private BallotList ballots;

        /// <summary>
        ///     A collection of <see cref="Card"/> that generates the Cards.xml
        ///     file that goes to the tabulator.
        /// </summary>
        private CardList cards;

        /// <summary>
        ///     A collection of <see cref="Face"/> that generates the Faces.xml
        ///     file that goes to the tabulator.
        /// </summary>
        private FaceList faces;

        /// <summary>
        ///     A collection of <see cref="DomainObjects.Contest"/> that 
        ///     generates the Contests.xml file that goes to the tabulator.
        /// </summary>
        private ContestList contests;

        /// <summary>
        ///     A collection of <see cref="MachineOption"/> that generates the 
        ///     MachineOptions.xml file that goes to the tabulator.
        /// </summary>
        private MachineOptionList machineOptions;

        /// <summary>
        ///     A collection of <see cref="ParameterFile"/>.
        /// </summary>
        private MachineParameters machineParams;

        /// <summary>
        ///     A colleciton of <see cref="ElectionParameter"/>.
        /// </summary>
        private ElectionParameterList electionParameters;

        /// <summary>
        ///     Create a dictionary of faces to keep track of unique faces
        ///     each key represents an MD5 hash of the collection of marks of 
        ///     a face. Any two faces with identical collection of marks are
        ///     considered the same even if their face ids are different. In 
        ///     that case, discard the newer one, and reuse the old one as saved 
        ///     on the dictionary.
        /// </summary>
        private Dictionary<string, Face> faceMap;

        #endregion
        
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="DefinitionBinder"/> class.
        /// </summary>
        /// <externalUnit cref="BallotList"/>
        /// <externalUnit cref="ballots"/>
        /// <externalUnit cref="CardList"/>
        /// <externalUnit cref="cards"/>
        /// <externalUnit cref="ContestList"/>
        /// <externalUnit cref="contests"/>
        /// <externalUnit cref="ElectionParameterList"/>
        /// <externalUnit cref="electionParameters"/>
        /// <externalUnit cref="MachineOptionList"/>
        /// <externalUnit cref="machineOptions"/>
        /// <externalUnit cref="MachineParameters"/>
        /// <externalUnit cref="machineParams"/>
        /// <revision revisor="dev11" date="12/16/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public DefinitionBinder()
        {
            this.ballots = new BallotList();
            this.cards = new CardList();
            this.faces = new FaceList();
            this.contests = new ContestList();
            this.machineOptions = new MachineOptionList();
            this.machineParams = new MachineParameters();
            this.machineParams.ParameterFiles = new ParameterFileList();
            this.electionParameters = new ElectionParameterList();

            this.faceMap = new Dictionary<string, Face>();
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets a collection of <see cref="Ballot"/> that generates the 
        ///     Ballots.xml file that goes to the tabulator.
        /// </summary>
        /// <value>The ballots.</value>
        /// <externalUnit cref="BallotList"/>
        /// <externalUnit cref="ballots"/>
        /// <revision revisor="dev11" date="12/16/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public BallotList Ballots
        {
            get
            {
                return this.ballots;
            }
        }

        /// <summary>
        ///     Gets a collection of <see cref="Card"/> that generates the 
        ///     Cards.xml file that goes to the tabulator.
        /// </summary>
        /// <value>The cards.</value>
        /// <externalUnit cref="CardList"/>
        /// <externalUnit cref="cards"/>
        /// <revision revisor="dev11" date="12/16/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public CardList Cards
        {
            get
            {
                return this.cards;
            }
        }

        /// <summary>
        ///     Gets collection of <see cref="Face"/> that generates the 
        ///     Faces.xml file that goes to the tabulator.
        /// </summary>
        /// <value>The faces.</value>
        /// <externalUnit cref="FaceList"/>
        /// <externalUnit cref="faces"/>
        /// <revision revisor="dev11" date="12/16/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public FaceList Faces
        {
            get
            {
                return this.faces;
            }
        }

        /// <summary>
        ///     Gets a collection of <see cref="DomainObjects.Contest"/> as 
        ///     loaded from the DB.
        /// </summary>
        /// <value>The contests.</value>
        /// <externalUnit cref="ContestList"/>
        /// <externalUnit cref="contests"/>
        /// <revision revisor="dev11" date="12/23/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public ContestList Contests
        {
            get
            {
                return this.contests;
            }
        }

        /// <summary>
        ///     Gets a collection of <see cref="MachineOption"/> as loaded
        ///     from the DB.
        /// </summary>
        /// <value>The machine options.</value>
        /// <externalUnit cref="MachineOptionList"/>
        /// <externalUnit cref="machineOptions"/>
        /// <revision revisor="dev11" date="12/23/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public MachineOptionList MachineOptions
        {
            get
            {
                return this.machineOptions;
            }
        }

        /// <summary>
        ///     Gets the machine params.
        /// </summary>
        /// <value>The machine params.</value>
        /// <externalUnit cref="MachineParameters"/>
        /// <externalUnit cref="machineParams"/>
        /// <revision revisor="dev11" date="12/23/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public MachineParameters MachineParams
        {
            get
            {
                return this.machineParams;
            }
        }

        /// <summary>
        ///     Gets the election params.
        /// </summary>
        /// <value>
        ///     The election params.
        /// </value>
        /// <externalUnit cref="ElectionParameterList"/>
        /// <externalUnit cref="electionParameters"/>
        /// <revision revisor="dev16" date="1/11/2009" version="1.0.0.0">
        ///     Member created.
        /// </revision>
        public ElectionParameterList ElectionParams 
        {
            get 
            {
                return this.electionParameters;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Extracts the cards and faces from the ballot and creates 
        ///     duplicates that are added to the corresponding collections, plus 
        ///     adding a duplicate of the ballot to the collection. Notice that 
        ///     cards and ballot duplicates have the inner collections empty, so 
        ///     that the serialization do not create those collections as part 
        ///     of the resulting XML.
        /// </summary>
        /// <param name="ballot">The ballot.</param>
        /// <externalUnit cref="Ballot"/>
        /// <externalUnit cref="Card"/>
        /// <externalUnit cref="CardList"/>
        /// <externalUnit cref="Face"/>
        /// <externalUnit cref="FaceList"/>
        /// <revision revisor="dev11" date="12/16/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev11" date="02/20/2009" version="1.0.8.0301">
        ///     Added face reuse and removal of duplicates
        /// </revision>
        /// <revision revisor="dev11" date="03/04/2009" version="1.0.8.1501">
        ///     since a ballot style can be used for multiple precincts, and the
        ///     precinct id is now part of the barcode, the ballot style id is 
        ///     no longer enough to uniquely identify a ballot on the 
        ///     Ballots.xml file. So now, the id is a sequential number.
        /// </revision>
        /// <revision revisor="dev11" date="03/06/2009" version="1.0.8.1701">
        ///     added support for new ballot attributes (ballot style id and 
        ///     precinct id).
        /// </revision>
        /// <revision revisor="dev11" date="03/16/2009" version="1.0.8.2701">
        ///     changed local variables name
        /// </revision>
        public void AddBallot(Ballot ballot)
        {
            // create a dictionary of faces to keep track of unique faces
            // each key represents an MD5 hash of the collection of marks of 
            // a face. Any two faces with identical collection of marks are
            // considered the same even if their face ids are different. In that
            // case, discard the newer one, and reuse the old one as saved on
            // the dictionary
            string md5;
            Face uniqueFace;

            // create a duplicate of the ballot so that the cards collection is
            // populated with custom duplicates
            Ballot ballotToCard = new Ballot();

            // make it 1-based identifier
            ballotToCard.Id = 1 + this.ballots.Count;
            ballotToCard.BallotStyleId = ballot.BallotStyleId;
            ballotToCard.PrecinctId = ballot.PrecinctId;
            ballotToCard.Cards = new CardList();

            // add the ballot duplicate to the ballots collection
            this.ballots.Add(ballotToCard);

            foreach (Card card in ballot.Cards)
            {
                // create a duplicate of the card so that the faces collection
                // is populated with custom duplicates
                Card cardToFace = new Card();
                cardToFace.Barcode = card.Barcode;
                cardToFace.Id = card.Id;
                cardToFace.Faces = new FaceList();

                // Add the duplicate card to the cards collection. 
                // This duplicate links a card to a face.
                this.cards.Add(cardToFace);
                foreach (Face face in card.Faces)
                {
                    // get the MD5 hash of the collection of marks of the 
                    // current face
                    md5 = BitConverter.ToString(ObjectMD5.Generate(face.Marks));

                    // check if an identical collection is already in use
                    if (this.faceMap.ContainsKey(md5) == true)
                    {
                        // a face with an identical collection of marks is 
                        // already in use so get it from the dictionary
                        uniqueFace = this.faceMap[md5];
                    }
                    else
                    {
                        // no existing face up to this point has the same 
                        // collection of marks, so add the face to the 
                        // dictionary using the MD5 hash
                        this.faceMap.Add(md5, face);

                        // add the new face since all marks need to be included
                        // as well on the [Faces.xml] file
                        this.faces.Add(face);

                        // use this face as the [uniqueFace]
                        uniqueFace = face;
                    }

                    // create the face entry that goes on the current card
                    // This is the face instance that appears on the [Cards.xml]
                    // file and doesn't contain any marks in it. It is simply
                    // a pointer to the corresponding face entry on the 
                    // [Faces.xml]where all marks are defined
                    Face faceToMark = new Face();

                    // map to the uniqueFace using the [id]
                    faceToMark.Id = uniqueFace.Id;

                    // add the duplicate of the face to the current card 
                    // collection that generates the [Cards.xml] file
                    // this duplicate do not contain the marks
                    cardToFace.Faces.Add(faceToMark);
                }

                // create another card duplicate to link the ballot to the card
                // and add it to the collection of cards of the ballot duplicate
                Card cardFromBallot = new Card();
                cardFromBallot.Barcode = card.Barcode;
                cardFromBallot.Id = card.Id;
                ballotToCard.Cards.Add(cardFromBallot);
            }
        }

        /// <summary>
        ///     Overloaded 
        ///     <see cref="Load(string,int,BallotEntrySet,BallotEntrySet)"/> to 
        ///     allow for no assignment id passed in.
        /// </summary>
        /// <param name="strConnection">The STR connection.</param>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="3/13/2009" version="1.0.8.21">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev11" date="04/14/2009" version="1.0.11.0401">
        ///     passing null to other entry sets
        /// </revision>
        public void Load(string strConnection)
        {
            this.Load(strConnection, 0, null, null);
        }

        /// <summary>
        ///     Loads all other collections from the DB using the supplied 
        ///     connection string.
        /// </summary>
        /// <param name="strConnection">The connection string.</param>
        /// <param name="machineAssignmentId">The machine assignment id.</param>
        /// <param name="contList">List of Contests</param>
        /// <param name="candList">List of Candidates</param>
        /// <externalUnit cref="contests"/>
        /// <externalUnit cref="electionParameters"/>
        /// <externalUnit cref="LoadContList"/>
        /// <externalUnit cref="LoadElecParams"/>
        /// <externalUnit cref="LoadMachOptn"/>
        /// <externalUnit cref="LoadMachParams"/>
        /// <externalUnit cref="MachineOptions"/>
        /// <externalUnit cref="MachineParams"/>
        /// <revision revisor="dev11" date="12/23/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev14" date="3/13/2009" version="1.0.8.2301">
        ///     Added machine assignmentId to pass to get election parameters
        /// </revision>
        /// <revision revisor="dev11" date="04/14/2009" version="1.0.11.0401">
        ///     added entry sets for contest list and candidate list
        /// </revision>
        public void Load(
            string strConnection, 
            int machineAssignmentId,
            BallotEntrySet contList, 
            BallotEntrySet candList)
        {
            this.contests.Clear();
            this.LoadContList(contList, candList);

            this.machineOptions.Clear();
            this.LoadMachOptn(strConnection);

            this.machineParams.ParameterFiles.Clear();
            this.LoadMachParams(strConnection);

            // Load up the election paramters based on the assignment of the
            // machine being initialized
            this.electionParameters.Clear();
            this.LoadElecParams(strConnection, machineAssignmentId);
        }

        #endregion

        #region Public Events

        #endregion

        #region Private Methods

        /// <summary>
        ///     Creates a new instance of Contest from the entry set of contests
        ///     using a supplied contest id.
        /// </summary>
        /// <param name="contList">The contest entry set.</param>
        /// <param name="contId">The contest id.</param>
        /// <returns>The Contest</returns>
        /// <externalUnit cref="BallotEntrySet"/>
        /// <externalUnit cref="DomainObjects.Contest"/>
        /// <externalUnit cref="StpContList"/>
        /// <revision revisor="dev11" date="12/23/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev01" date="11/16/2009" version="1.1.3.4">
        ///     Changed party assigment
        /// </revision>
        private DomainObjects.Contest GetContest(
            BallotEntrySet contList, int contId)
        {
            int idx = contList.FindIndex(StpContList.ContestId, contId);
            DomainObjects.Contest contest = new DomainObjects.Contest();
            contest.Id = contId;
            contest.Name = contList.GetValueStr(idx, StpContList.ContestName);
            contest.VoteFor = contList.GetValueInt(idx, StpContList.VoteFor);
            contest.BallotStatusId = contList.GetValueInt(
                idx, StpContList.BallotStatusId);
            contest.ContestType = contList.GetValueInt(
                idx, StpContList.ContestType);
            contest.ListOrder = 
                contList.GetValueInt(idx, StpContList.ListOrder);
            contest.Party = new Party(
                contList.GetValueInt(idx, StpContList.PartyId),
                string.Empty,
                string.Empty,
                0,
                false);
            return contest;
        }

        /// <summary>
        ///     Loads the contest list.
        /// </summary>
        /// <param name="contList">Contest List</param>
        /// <param name="candList">Candidate List</param>
        /// <externalUnit cref="BallotEntrySet"/>
        /// <externalUnit cref="contests"/>
        /// <externalUnit cref="DomainObjects.Contest"/>
        /// <externalUnit cref="GetContest"/>
        /// <externalUnit cref="StpCandList"/>
        /// <externalUnit cref="StpContList"/>
        /// <revision revisor="dev11" date="12/23/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev11" date="04/14/2009" version="1.0.11.0401">
        ///     each candidate is added now only once. This assumes obviously 
        ///     that each contest appearance has the exact same candidates
        /// </revision>
        private void LoadContList(
            BallotEntrySet contList, BallotEntrySet candList)
        {
            // B allotEntrySet candList = new BallotEntrySet(typeof (StpCandList)),
            // contList = new BallotEntrySet(typeof (StpContList));
            Dictionary<int, DomainObjects.Contest> contMap =
                new Dictionary<int, DomainObjects.Contest>();
            List<int> candidateIds = new List<int>();
            DomainObjects.Contest cont;
            int contId, candId;

            // for every candidate, find the corresponding contest
            for (int i = 0; i < candList.Count; i = i + 1)
            {
                contId = candList.GetValueInt(i, StpCandList.ContestId);
                if (contMap.ContainsKey(contId) == false)
                {
                    // this contest has not been created yet, so create an
                    // instance for it, add it to the map for easy access and
                    // to the contest collection
                    cont = this.GetContest(contList, contId);
                    contMap.Add(contId, cont);
                    this.contests.Add(cont);
                }
                else
                {
                    // get the contest from the map
                    cont = contMap[contId];
                }
                
                candId = candList.GetValueInt(i, StpCandList.CandidateId);
                if (candidateIds.Contains(candId) == false)
                {
                    DomainObjects.Candidate cand = 
                        new DomainObjects.Candidate();
                    cand.Id = candId;
                    cand.Name = candList.GetValueStr(
                        i, StpCandList.CandidateDisplayName);
                    cand.BallotStatusId = candList.GetValueInt(
                        i, StpCandList.BallotStatusId);
                    cand.CandidateType = candList.GetValueInt(
                        i, StpCandList.CandidateType);
                    cand.PartyId = candList.GetValueInt(i, StpCandList.PartyId);
                    cont.Candidates.Add(cand);
                    candidateIds.Add(candId);
                }
            }
        }

        /// <summary>
        ///     Loads the machine options
        /// </summary>
        /// <param name="strConnection">The STR connection.</param>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="12/23/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        private void LoadMachOptn(string strConnection)
        {
            BallotEntrySet machOptn = new BallotEntrySet(typeof(StpMachOptn));

            machOptn.Load(strConnection);

            for (int i = 0; i < machOptn.Count; i = i + 1)
            {
                MachineOption option = new MachineOption();
                option.Name = machOptn.GetValueStr(i, StpMachOptn.OptionName);
                option.Value = machOptn.GetValueStr(i, StpMachOptn.OptionValue);
                this.machineOptions.Add(option);
            }
        }

        /// <summary>
        ///     Loads the machine parameters
        /// </summary>
        /// <param name="strConnection">The STR connection.</param>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="12/23/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        private void LoadMachParams(string strConnection)
        {
            BallotEntrySet machParm = new BallotEntrySet(typeof(StpMachParm));

            machParm.Load(strConnection);

            for (int i = 0; i < machParm.Count; i = i + 1)
            {
                ParameterFile file = new ParameterFile();
                file.Name = machParm.GetValueStr(i, StpMachParm.ParamName);
                this.machineParams.ParameterFiles.Add(file);
            }
        }

        /// <summary>
        ///     Loads the election parameters
        /// </summary>
        /// <param name="strConnection">The STR connection.</param>
        /// <param name="machineAssignmentId">The machine assignment id.</param>
        /// <externalUnit/>
        /// <revision revisor="dev16" date="1/11/2009" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        private void LoadElecParams(
            string strConnection, 
            int machineAssignmentId)
        {
            BallotEntrySet elecParam = new BallotEntrySet(typeof(StpElecParam));

            // pass in the machineAssignmentId to the proc if we got one
            if (machineAssignmentId != 0)
            {
                elecParam.Load(strConnection, machineAssignmentId);
            }
            else
            {
                elecParam.Load(strConnection);
            }

            for (int i = 0; i < elecParam.Count; i = i + 1)
            {
                ElectionParameter parameter = new ElectionParameter();
                parameter.Name = elecParam.GetValueStr(
                    i, StpElecParam.ParamName);
                parameter.Value = elecParam.GetValueStr(
                    i, StpElecParam.ParamValue);
                this.electionParameters.Add(parameter);
            }
        }
        #endregion
    }
}
