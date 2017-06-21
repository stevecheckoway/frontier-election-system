// -----------------------------------------------------------------------------
// <copyright file="Contest.cs" company="Sequoia Voting Systems"> 
//    Copyright (c) 2008 Sequoia Voting Systems, Inc. All Rights Reserved.
//    Distribution of source code is allowable only under the terms of the
//    license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//    This file contains the Contest class.
// </summary>
// <revision revisor="dev11" date="12/3/2008" version="1.0.0.0">
//    File Created
// </revision>  
// -----------------------------------------------------------------------------

namespace Sequoia.Ballot.Data
{
    #region Using directives

    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Xml;

    using StpBallot = Sequoia.Ballot.Data.BallotEntrySet.StpBallot;
    using StpCont = Sequoia.Ballot.Data.BallotEntrySet.StpCont;

    #endregion

    /// <summary>
    ///     This class represent a contest graphic elements on a paper ballot
    /// </summary>
    /// <revision revisor="dev11" date="12/3/2008" version="1.0.0.0">
    ///     Class created.
    /// </revision>
    public class Contest
    {
        #region Fields

        /// <summary>
        ///     contest's name
        /// </summary>
        private string name;

        /// <summary>
        ///     collection of texts
        /// </summary>
        private List<OfficeText> texts;

        /// <summary>
        ///     contest header bounds
        /// </summary>
        private RectangleF rectangle;

        /// <summary>
        ///     contest header page number
        /// </summary>
        private int page = 0;

        /// <summary>
        ///     collection of candidates
        /// </summary>
        private List<Candidate> candidates;

        /// <summary>
        ///     contest break type (column, page, none)
        /// </summary>
        private BreakType breakType;

        /// <summary>
        ///     contest id
        /// </summary>
        private int id;

        /// <summary>
        ///     number of candidates to vote for
        /// </summary>
        private int voteFor;

        #endregion
        
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Contest"/> class.
        /// </summary>
        /// <param name="id">The contest id.</param>
        /// <externalUnit cref="breakType"/>
        /// <externalUnit cref="BreakType"/>
        /// <externalUnit cref="Candidate"/>
        /// <externalUnit cref="Contest"/>
        /// <externalUnit cref="OfficeText"/>
        /// <externalUnit cref="RectangleF"/>
        /// <revision revisor="dev11" date="12/19/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public Contest(int id)
        {
            this.id = id;
            this.texts = new List<OfficeText>();
            this.candidates = new List<Candidate>();
            this.rectangle = new RectangleF();
            this.breakType = BreakType.None;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Contest"/> class.
        /// </summary>
        /// <param name="id">The contest id.</param>
        /// <param name="name">The contest name.</param>
        /// <externalUnit cref="Contest"/>
        /// <externalUnit cref="name"/>
        /// <revision revisor="dev11" date="12/3/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public Contest(int id, string name) : this(id)
        {
            this.name = name;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the name of the contest
        /// </summary>
        /// <value>The contest name.</value>
        /// <externalUnit cref="name"/>
        /// <revision revisor="dev11" date="12/23/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public string Name
        {
            get
            {
                return this.name;
            }
        }

        /// <summary>
        ///     Gets or sets the break.
        /// </summary>
        /// <value>The break.</value>
        /// <externalUnit cref="breakType"/>
        /// <revision revisor="dev11" date="12/23/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public BreakType Break
        {
            get
            {
                return this.breakType;
            }

            set
            {
                this.breakType = value;
            }
        }

        /// <summary>
        ///     Gets the collection of texts
        /// </summary>
        /// <value>The texts.</value>
        /// <externalUnit cref="OfficeText"/>
        /// <externalUnit cref="texts"/>
        /// <revision revisor="dev11" date="12/23/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public List<OfficeText> Texts
        {
            get
            {
                return this.texts;
            }
        }

        /// <summary>
        ///     Gets the collection of candidates.
        /// </summary>
        /// <value>The candidates.</value>
        /// <externalUnit cref="Candidate"/>
        /// <externalUnit cref="candidates"/>
        /// <revision revisor="dev11" date="12/23/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public List<Candidate> Candidates
        {
            get
            {
                return this.candidates;
            }
        }

        /// <summary>
        ///     Gets or sets the bounds of the contest header
        /// </summary>
        /// <value>The bounds.</value>
        /// <externalUnit cref="rectangle"/>
        /// <externalUnit cref="RectangleF"/>
        /// <revision revisor="dev11" date="12/23/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public RectangleF Bounds
        {
            get
            {
                return this.rectangle;
            }

            set
            {
                this.rectangle = value;
            }
        }

        /// <summary>
        ///     Gets or sets the page of the contest header
        /// </summary>
        /// <value>The page number.</value>
        /// <externalUnit cref="page"/>
        /// <revision revisor="dev11" date="12/23/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public int Page
        {
            get
            {
                return this.page;
            }

            set
            {
                this.page = value;
            }
        }

        /// <summary>
        ///     Gets the id of the contest
        /// </summary>
        /// <value>The contest id.</value>
        /// <externalUnit cref="id"/>
        /// <revision revisor="dev11" date="12/23/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public int Id
        {
            get
            {
                return this.id;
            }
        }

        /// <summary>
        ///     Gets or sets the vote for.
        /// </summary>
        /// <value>The vote for.</value>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="3/10/2009" version="1.0.8.2101">
        ///     Member Created
        /// </revision>
        public int VoteFor
        {
            get
            {
                return this.voteFor;
            }

            set
            {
                this.voteFor = value;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Loads the contest from an XML node, including all 
        ///     candidate subnodes.
        /// </summary>
        /// <param name="xmlCont">The XML cont.</param>
        /// <param name="defFont">The def font.</param>
        /// <param name="defSize">Size of the def.</param>
        /// <returns>
        ///     A <see cref="Contest" /> object from the xml.
        /// </returns>
        /// <externalUnit cref="Contest"/>
        /// <externalUnit cref="Keys"/>
        /// <externalUnit cref="XmlNode"/>
        /// <revision revisor="dev11" date="12/23/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public static Contest LoadFromXml(
            XmlNode xmlCont, string defFont, double defSize)
        {
            // read information from the attributes
            string name = xmlCont.Attributes[Keys.ATR_CST_NAME].Value;

            int id = int.Parse(xmlCont.Attributes[Keys.ATR_CST_ID].Value);

            Contest contest = new Contest(id, name);

            try
            {
                XmlAttribute breakAttr = xmlCont.Attributes[Keys.ATR_CST_BREAK];
                contest.Break =
                    (BreakType)Enum.Parse(typeof(BreakType), breakAttr.Value);
            }
            catch
            {
            }

            // get the text lines
            XmlNodeList texts = xmlCont.SelectNodes(Keys.TAG_OFFICE_TXT);

            foreach (XmlNode text in texts)
            {
                contest.Texts.Add(
                    OfficeText.LoadFromXml(text, defFont, defSize));
            }

            // get the candidates
            XmlNodeList cands = xmlCont.SelectNodes(Keys.TAG_CANDIDATE);

            foreach (XmlNode cand in cands)
            {
                contest.Candidates.Add(Candidate.LoadFromXml(cand));
            }

            return contest;
        }

        /// <summary>
        ///     Loads from db.
        /// </summary>
        /// <param name="ballots">The ballots.</param>
        /// <param name="contests">The contests (StpCont).</param>
        /// <param name="candidates">The candidates.</param>
        /// <param name="ballotId">The ballot id.</param>
        /// <param name="contId">The contest id.</param>
        /// <param name="defFont">The def font.</param>
        /// <param name="defSize">Size of the def.</param>
        /// <returns>
        ///     A <see cref="Contest" /> from the database.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="12/19/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev11" date="04/14/2009" version="1.0.11.0401">
        ///     Passing ballot style id to candidates as well since candidate 
        ///     may appear more than once on the ballot entry set.
        /// </revision>
        public static Contest LoadFromDb(
            BallotEntrySet ballots, 
            BallotEntrySet contests, 
            BallotEntrySet candidates, 
            int ballotId,
            int contId, 
            string defFont, 
            double defSize)
        {
            int idxBal = ballots.FindIndex(
                StpBallot.BallotStyleId,
                ballotId,
                StpBallot.ContestId,
                contId),
                idxCst = contests.FindIndex(StpCont.ContestId, contId),
                curCst,
                curCnd = ballots.GetValueInt(idxBal, StpBallot.CandDispFormat),

                // using 0x0003 since this is a 2-bit flag that can take all 4
                // values: 0, 1, 2, 3
                cstFormat = // 0x0003 & 
                    ballots.GetValueInt(idxBal, StpBallot.ContDispFormat);
            var contest = new Contest(contId);

            // read break format settings
            contest.Break =
                (BreakType) Enum.ToObject(typeof(BreakType), cstFormat);
            
            do
            {
                // Load office texts from the contest entry set until a 
                // different contest id is found. Start at the current index 
                // found above.
                contest.Texts.Add(
                    OfficeText.LoadFromDb(contests, idxCst, defFont, defSize));

                // Increment the index by 1 and test if the contest id is 
                // the same
                idxCst = idxCst + 1;
                if (idxCst < contests.Count)
                {
                    curCst = contests.GetValueInt(idxCst, StpCont.ContestId);
                }
                else
                {
                    // idxCst is greater than the number of elements in the
                    // collection, so simply use -1 to break the loop
                    curCst = -1;
                }
            }
            while (curCst == contId);

            do
            {
                // get the current candidate id
                curCnd = ballots.GetValueInt(idxBal, StpBallot.CandidateId);

                // load all candidates for this contest
                contest.Candidates.Add(
                    Candidate.LoadFromDb(
                        ballots,
                        candidates,
                        curCnd,
                        ballotId));

                // increment the index by 1 and test if the contest id is
                // the same
                idxBal = idxBal + 1;

                if (idxBal < ballots.Count)
                {
                    curCst = ballots.GetValueInt(idxBal, StpBallot.ContestId);
                }
                else
                {
                    curCst = -1;
                }
            }
            while (curCst == contId);

            return contest;
        }

        /// <summary>
        ///     Gets the bounds with candidates.
        /// </summary>
        /// <returns>
        ///     A <see cref="RectangleF" /> object with candidates.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="4/8/2009" version="1.0.10.0501">
        ///     Comments added
        /// </revision>
        public RectangleF GetBoundsWithCands()
        {
            RectangleF rect = new RectangleF();
            rect.X = this.rectangle.X;
            rect.Y = this.rectangle.Y;

            rect.Height =
                this.candidates[this.candidates.Count - 1].Bounds.Bottom
                - rect.Y;
            rect.Width = this.rectangle.Width;

            return rect;
        }

        #endregion

        #region Public Events

        #endregion

        #region Private Methods

        #endregion
    }
}
