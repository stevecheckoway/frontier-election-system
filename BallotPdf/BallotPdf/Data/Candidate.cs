// -----------------------------------------------------------------------------
// <copyright file="Candidate.cs" company="Sequoia Voting Systems"> 
//    Copyright (c) 2008 Sequoia Voting Systems, Inc. All Rights Reserved.
//    Distribution of source code is allowable only under the terms of the
//    license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//    This file contains the Candidate class.
// </summary>
// <revision revisor="dev11" date="12/3/2008" version="1.0.0.0">
//    File Created
// </revision>  
// <revision revisor="dev11" date="1/20/2009" version="1.0.5.1">
//    File modified
// </revision>
// -----------------------------------------------------------------------------

namespace Sequoia.Ballot.Data
{
    #region Using directives

    using System;
    using System.Drawing;
    using System.Xml;

    using Sequoia.DomainObjects;

    using StpBallot = Sequoia.Ballot.Data.BallotEntrySet.StpBallot;
    using StpCand = Sequoia.Ballot.Data.BallotEntrySet.StpCand;

    #endregion

    /// <summary>
    ///     This class represents the graphic elements of a candidate on a 
    ///     paper ballot.
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev11" date="12/3/2008" version="1.0.0.0">
    ///     Class created.
    /// </revision>
    public class Candidate
    {
        #region Constants

        /// <summary>
        ///     value is bit-wise integer corresponding to the [flag] field
        /// </summary>
        public const int VotedFlag = 1;

        /// <summary>
        ///     value is bit-wise integer corresponding to the [flag] field
        /// </summary>
        private const int ColumnBreakFlag = 2;

        /// <summary>
        ///     decimal places used to round coordinates
        /// </summary>
        private const int XYPrecision = 3;

        /// <summary>
        ///     PDF units are expressed in points but everything else is 
        ///     expressed in inches. For conversion purposes use this constant. 
        ///     There are 72 points per inch.
        /// </summary>
        private const float PointsPerInchF = 72f;

        /// <summary>
        ///     Used to mask only the first two bytes of an integer
        /// </summary>
        private const int MaskTwoBytes = 0xFFFF;

        #endregion
        
        #region Fields

        /// <summary>
        ///     candidate name
        /// </summary>
        private string name;

        /// <summary>
        ///     candidate formatting flags
        /// </summary>
        private int flags = 0;

        /// <summary>
        ///     candidate box bounds
        /// </summary>
        private RectangleF rectangle = new RectangleF();

        /// <summary>
        ///     candidate page number
        /// </summary>
        private int page = 0;

        /// <summary>
        ///     coordinates for the text of the candidate's name
        /// </summary>
        private PointF xyName = new PointF();

        /// <summary>
        ///     coordinates for the arrow of the candidate's voting target
        /// </summary>
        private PointF xyArrow = new PointF();

        /// <summary>
        ///     candidate's id
        /// </summary>
        private int id;

        /// <summary>
        ///     party identification
        /// </summary>
        private int partyId;

        #endregion
        
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Candidate"/> class.
        /// </summary>
        /// <param name="id">The candidate id.</param>
        /// <param name="name">The candidate name.</param>
        /// <externalUnit cref="id"/>
        /// <externalUnit cref="name"/>
        /// <revision revisor="dev11" date="12/3/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public Candidate(int id, string name)
        {
            this.id = id;
            this.name = name;
            this.Type = CandidateType.Standard;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Candidate"/> class.
        /// </summary>
        /// <param name="id">The candidateid.</param>
        /// <param name="name">The candidate name.</param>
        /// <param name="voted">if set to <c>true</c> [voted].</param>
        /// <param name="columnBreak">
        ///     if set to <c>true</c> [column break].
        /// </param>
        /// <externalUnit cref="ColumnBreakFlag"/>
        /// <externalUnit cref="SetFlag"/>
        /// <externalUnit cref="VotedFlag"/>
        /// <revision revisor="dev11" date="12/23/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public Candidate(int id, string name, bool voted, bool columnBreak)
            : this(id, name)
        {
            this.SetFlag(VotedFlag, voted);
            this.SetFlag(ColumnBreakFlag, columnBreak);
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the name of the candidate
        /// </summary>
        /// <value>The candidate name.</value>
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
        ///     Gets a value indicating whether this <see cref="Candidate"/> 
        ///     is voted.
        /// </summary>
        /// <value><c>true</c> if voted; otherwise, <c>false</c>.</value>
        /// <externalUnit cref="flags"/>
        /// <externalUnit cref="VotedFlag"/>
        /// <revision revisor="dev11" date="12/23/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public bool Voted
        {
            get
            {
                return (this.flags & VotedFlag) == VotedFlag;
            }
        }

        /// <summary>
        ///     Gets a value indicating whether this candidate has 
        ///     a [column break].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [start new column]; otherwise, <c>false</c>.
        /// </value>
        /// <externalUnit cref="ColumnBreakFlag"/>
        /// <externalUnit cref="flags"/>
        /// <revision revisor="dev11" date="12/23/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public bool StartNewColumn
        {
            get
            {
                return (this.flags & ColumnBreakFlag) == ColumnBreakFlag;
            }
        }

        /// <summary>
        ///     Gets or sets the bounds of the candidate entire box
        /// </summary>
        /// <value>The bounds.</value>
        /// <externalUnit cref="rectangle"/>
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
        ///     Gets or sets the coordinates of the text for the name. These
        ///     coordinates refer to the point used by AquaPDF to draw text.
        /// </summary>
        /// <value>The name of the XY.</value>
        /// <externalUnit cref="xyName"/>
        /// <revision revisor="dev11" date="12/23/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public PointF XYName
        {
            get
            {
                return this.xyName;
            }

            set
            {
                this.xyName = value;
            }
        }

        /// <summary>
        ///     Gets or sets the XY for the arrow (in points). Upper left corner 
        ///     of the arrow bounds.
        /// </summary>
        /// <value>The XY arrow.</value>
        /// <externalUnit cref="xyArrow"/>
        /// <revision revisor="dev11" date="12/15/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public PointF XYArrow
        {
            get
            {
                return this.xyArrow;
            }

            set
            {
                this.xyArrow = value;
            }
        }

        /// <summary>
        ///     Gets the id.
        /// </summary>
        /// <value>The candidate id.</value>
        /// <externalUnit cref="id"/>
        /// <revision revisor="dev11" date="12/15/2008" version="1.0.0.0">
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
        ///     Gets or sets the page number
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
        ///     Gets or sets the party id.
        /// </summary>
        /// <value>The party id.</value>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="3/10/2009" version="1.0.8.2101">
        ///     Member Created
        /// </revision>
        public int PartyId
        {
            get
            {
                return this.partyId;
            }

            set
            {
                this.partyId = value;
            }
        }

        /// <summary>
        ///     Gets the precision used to round coordinate values
        /// </summary>
        /// <value>The number of decimal places.</value>
        /// <externalUnit cref="XYPrecision"/>
        /// <revision revisor="dev11" date="2/9/2009" version="1.0.6.5">
        ///     Member Created
        /// </revision>
        public static int Precision
        {
            get
            {
                // return the constant
                return Candidate.XYPrecision;
            }
        }

        /// <summary>
        ///     Gets or sets the candidate type.
        /// </summary>
        /// <value>The candidate type.</value>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="8/20/2009" version="1.0.15.1401">
        ///     Member Created
        /// </revision>
        public CandidateType Type
        {
            get;
            set;
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Loads a candidate from an XML node
        /// </summary>
        /// <param name="xmlCand">The XML cand.</param>
        /// <returns>
        ///     A <see cref="Candidate" /> from the xml.
        /// </returns>
        /// <externalUnit cref="Candidate"/>
        /// <externalUnit cref="Keys"/>
        /// <externalUnit cref="XmlAttribute"/>
        /// <externalUnit cref="XmlNode"/>
        /// <revision revisor="dev11" date="12/23/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public static Candidate LoadFromXml(XmlNode xmlCand)
        {
            string name = xmlCand.InnerText;
            int id = int.Parse(xmlCand.Attributes[Keys.ATR_CND_ID].Value);

            XmlAttribute xmlBreak = xmlCand.Attributes[Keys.ATR_CND_BREAK],
                         xmlVoted = xmlCand.Attributes[Keys.ATR_CND_VOTED];
            
            bool columnBreak = (xmlBreak != null)
                               && (bool.Parse(xmlBreak.Value) == true),
                 voted = (xmlVoted != null)
                         && (bool.Parse(xmlVoted.Value) == true);
            
            return new Candidate(id, name, voted, columnBreak);
        }

        /// <summary>
        ///     Loads a candidate from the DB using supplied entry sets
        /// </summary>
        /// <param name="ballots">The ballots.</param>
        /// <param name="candidates">The candidates.</param>
        /// <param name="id">The candidate id.</param>
        /// <param name="ballotId">The ballot id.</param>
        /// <returns>
        ///     A <see cref="Candidate" /> from the database.
        /// </returns>
        /// <externalUnit cref="BallotEntrySet"/>
        /// <externalUnit cref="BreakType"/>
        /// <externalUnit cref="Candidate"/>
        /// <externalUnit cref="StpBallot"/>
        /// <externalUnit cref="StpCand"/>
        /// <revision revisor="dev11" date="12/23/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev11" date="04/14/2009" version="1.0.11.0401">
        ///     added ballot style id
        /// </revision>
        public static Candidate LoadFromDb(
            BallotEntrySet ballots, 
            BallotEntrySet candidates, 
            int id, 
            int ballotId)
        {
            int idxBal = ballots.FindIndex(
                StpBallot.BallotStyleId,
                ballotId,
                StpBallot.CandidateId,
                id),
                idxCnd = candidates.FindIndex(StpCand.CandidateId, id),
                maskCnd = ballots.GetValueInt(idxBal, StpBallot.CandDispFormat),
                intBreak = (int) BreakType.Column;
            string name = candidates.GetValueStr(
                idxCnd, StpCand.CandidateDisplayName);
            Candidate cand = new Candidate(
                id,
                name,
                (maskCnd & VotedFlag) == VotedFlag,
                (maskCnd & intBreak) == intBreak);

            cand.Type = (CandidateType) Enum.ToObject(
                                            typeof(CandidateType),
                                            candidates.GetValueInt(
                                                idxCnd, StpCand.CandidateType));

            return cand;
        }

        /// <summary>
        ///     Gets the XY for the target (in inches)
        /// </summary>
        /// <value>The XY target.</value>
        /// <externalUnit cref="PaperBallot"/>
        /// <externalUnit cref="PointF"/>
        /// <externalUnit cref="PointsPerInchF"/>
        /// <externalUnit cref="Precision"/>
        /// <externalUnit cref="TargetLayout"/>
        /// <externalUnit cref="xyArrow"/>
        /// <revision revisor="dev11" date="1/20/2009" version="1.0.5.1">
        ///     Member Created
        /// </revision>
        public PointF GetXYTarget(
            TargetLayout arrowLayout, TargetType targetType)
        {
            PointF point = new PointF(

                // [absolute arrow x, in inches] + [relative x coordinate]
                xyArrow.X / PointsPerInchF + (float) PaperBallot.ArrHeadX[3],

                // [absolute arrow y, in inches] + [relative y coordinate]
                xyArrow.Y / PointsPerInchF + (float) PaperBallot.ArrHeadY[3]);

            if (targetType == TargetType.Oval)
            {
                point.X = xyArrow.X / PointsPerInchF;
                point.Y = xyArrow.Y / PointsPerInchF;
            }

            if (arrowLayout == TargetLayout.Left)
            {
                point.X = xyArrow.X / PointsPerInchF
                          + Convert.ToSingle(0.25F - PaperBallot.ArrTailX[0]);
            }

            // round the coordinates to a specified precision in order to
            // keep the exported XML (which will be used by the tabulator
            // to create the slip file) not too complicated
            point.X = Convert.ToSingle(Math.Round(point.X, Precision));
            point.Y = Convert.ToSingle(Math.Round(point.Y, Precision));

            return point;
        }
        #endregion

        #region Public Events

        #endregion

        #region Private Methods

        /// <summary>
        ///     Sets the flag (voted, column break).
        ///     Voted: the candidate appears as voted when generating a PDF 
        ///     ballot. This is useful for pre-voted ballot generation on vote 
        ///     simulations ColumnBreak: a column break is added right before 
        ///     the candidate forcing it to appear at the beginning of the next 
        ///     column (even if it requires a new page).
        /// </summary>
        /// <param name="bit">
        ///     Either one of two values: <see cref="VotedFlag"/>, 
        ///     <see cref="ColumnBreakFlag"/>.
        /// </param>
        /// <param name="val">if set to <c>true</c> [val].</param>
        /// <externalUnit cref="flags"/>
        /// <externalUnit cref="MaskTwoBytes"/>
        /// <revision revisor="dev11" date="12/23/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        private void SetFlag(int bit, bool val)
        {
            if (val)
            {
                // by using a bit-OR operation, that bit is set to 1
                //
                // i.e.:                            bit
                // |
                // v
                // flags        = ???? ???? ???? ????
                // bit          = 0000 0000 0000 0010
                // OR result    = ???? ???? ???? ??1?
                this.flags = this.flags | bit;
            }
            else
            {
                // reverse the bit by XOR with the appropriate mask
                //
                // i.e.:                            bit
                // |
                // v
                // bit          = 0000 0000 0000 0010
                // MaskTwoBytes = 1111 1111 1111 1111
                // XOR result   = 1111 1111 1111 1101
                bit = bit ^ MaskTwoBytes;

                // by using a bit-AND operation, the flag bit is set to 0
                // flags        = ???? ???? ???? ????
                // bit          = 1111 1111 1111 1101
                // AND result   = ???? ???? ???? ??0?
                this.flags = this.flags & bit;
            }
        }

        #endregion
    }
}
