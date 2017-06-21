// -----------------------------------------------------------------------------
// <copyright file="MatrixPtyCstPaperBallot.cs" company="Sequoia Voting Systems"> 
//    Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//    Distribution of source code is allowable only under the terms of the
//    license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//    This file contains the MatrixPtyCstPaperBallot class.
// </summary>
// <revision revisor="dev11" date="3/9/2009" version="1.0.8.2001">
//    File Created
// </revision>  
// <revision revisor="dev11" date="03/23/2009" version="1.0.9.0701">
// File modified
// </revision>
// -----------------------------------------------------------------------------

namespace Sequoia.Ballot.Data
{
    #region Using directives

    using System;
    using System.Collections.Generic;
    using System.Drawing;

    using com.thentech.pdf.ext;

    using JColor = java.awt.Color;

    using StpCandList = Sequoia.Ballot.Data.BallotEntrySet.StpCandList;
    using StpContList = Sequoia.Ballot.Data.BallotEntrySet.StpContList;
    using StpParty = Sequoia.Ballot.Data.BallotEntrySet.StpParty;

    #endregion

    /// <summary>
    ///     Class for MatrixPtyCstPaperBallot
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev11" date="3/9/2009" version="1.0.8.2001">
    ///     Class created.
    /// </revision>
    public class MatrixPtyCstPaperBallot : PaperBallot
    {
        #region Constants

        /// <summary>
        ///     defines the left side of the entire matrix, i.e. the left of the
        ///     contest headers (in points)
        /// </summary>
        private const float MatrixX = 0.25F * PointsPerInchF;

        /// <summary>
        ///     defines the top of the entire matrix, i.e. the top of the party
        ///     headers (in points)
        /// </summary>
        private const float MatrixY = 1.25F * PointsPerInchF;

        /// <summary>
        ///     defines the width of the contest headers (in points)
        /// </summary>
        private const float ColWidthContest = 2f * PointsPerInchF;

        /// <summary>
        ///     defines the width of the candidate headers (in points)
        /// </summary>
        private const float ColWidthCandidate = 1F * PointsPerInchF;

        /// <summary>
        ///     defines the height of any row of the matrix (in points), 
        ///     including the top row for parties
        /// </summary>
        private const float RowHeight = 0.5F * PointsPerInchF;

        /// <summary>
        ///     defines the padding of any cell in the matrix (in points)
        /// </summary>
        private const float CellPadding = 0.02F * PointsPerInchF;

        #endregion
        
        #region Fields

        /// <summary>
        ///     number of candidate columns, this doesn't include the column for
        ///     contest headers nor the column for write-ins
        /// </summary>
        private int columnCount;

        /// <summary>
        ///     number of candidate rows, this doesn't include the row for party
        ///     headers
        /// </summary>
        private int rowCount;

        /// <summary>
        ///     maps a party id to a 0-based column index
        /// </summary>
        private Dictionary<int, int> partyColumns;

        /// <summary>
        ///     an entry set listing all parties with names
        /// </summary>
        private BallotEntrySet esParties;

        #endregion
        
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="MatrixPtyCstPaperBallot"/> class.
        /// </summary>
        /// <param name="entrySetBallots">The es ballots.</param>
        /// <param name="entrySetContests">The es conts.</param>
        /// <param name="entrySetCandidates">The es cands.</param>
        /// <param name="entrySetParams">The es params.</param>
        /// <param name="esContList">The es cont list.</param>
        /// <param name="entrySetCandidateList">The es cand list.</param>
        /// <param name="esParties">The es parties.</param>
        /// <param name="target">The target.</param>
        /// <param name="ballotStyleId">The ballot style id.</param>
        /// <param name="currentCardCount">The current card count.</param>
        /// <param name="currentFaceCount">The current face count.</param>
        /// <revision revisor="dev11" date="03/11/2009" version="1.0.8.2101">
        ///     Member created
        /// </revision>
        /// <revision revisor="dev11" date="03/11/2009" version="1.0.8.2201">
        ///     Added party information (StpParty)
        /// </revision>
        public MatrixPtyCstPaperBallot(BallotEntrySet entrySetBallots,
            BallotEntrySet entrySetContests, 
            BallotEntrySet entrySetCandidates, 
            BallotEntrySet entrySetParams, 
            BallotEntrySet esContList,
            BallotEntrySet entrySetCandidateList, 
            BallotEntrySet esParties,
            PaperBallotTarget target,
            int ballotStyleId, 
            int currentCardCount, 
            int currentFaceCount) : base()
        {
            this.target = target;

            // set the card and face counts
            this.currentCardCount = currentCardCount;
            this.currentFaceCount = currentFaceCount;

            this.esParties = esParties;

            Load(
                entrySetBallots,
                entrySetContests,
                entrySetCandidates,
                entrySetParams,
                ballotStyleId);
            this.LoadExtended(esContList, entrySetCandidateList);
            this.Position();
        }

        #endregion

        #region Public Properties

        #endregion

        #region Public Methods

        #endregion

        #region Public Events

        #endregion

        #region Private Methods

        #region -- Load extended data

        /// <summary>
        ///     Loads the extended data. This is the [vote for] number for the 
        ///     contests and the [party id] for the candidates. Also at the same
        ///     time determine the number of columns and rows of the matrix
        /// </summary>
        /// <param name="esContList">The contest list entry set (StpContList)</param>
        /// <param name="entrySetCandidateList">The candidate list (StpCandList)</param>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="3/10/2009" version="1.0.8.2101">
        ///     Member Created
        /// </revision>
        private void LoadExtended(
            BallotEntrySet esContList, BallotEntrySet entrySetCandidateList)
        {
            // a variable for the id (contest or candidate)
            // and a index of the corresponding record of a given id on the 
            // appropriate entry set
            int id, idx;

            // initialize counters
            this.columnCount = 0;
            this.rowCount = 0;

            // get a collection of party ids
            this.partyColumns = new Dictionary<int, int>();

            // for every contest, find it on the contest list and get the 
            // vote for from there
            foreach (Contest contest in contests)
            {
                // get the contest id
                id = contest.Id;

                // find it on the contest list entry set
                idx = esContList.FindIndex(StpContList.ContestId, id);

                // get the vote for from that entry
                contest.VoteFor = esContList.GetValueInt(
                    idx, StpContList.VoteFor);

                // increment the row count by the vote for number
                this.rowCount = this.rowCount + contest.VoteFor;

                // for every candidate of the current contest, find it on the
                // candidate list and get the party id from there
                foreach (Candidate candidate in contest.Candidates)
                {
                    // same here
                    id = candidate.Id;
                    idx =
                        entrySetCandidateList.FindIndex(
                            StpCandList.CandidateId, id);
                    candidate.PartyId = entrySetCandidateList.GetValueInt(
                        idx, StpCandList.PartyId);

                    // add the party id if it's new
                    if (!this.partyColumns.ContainsKey(candidate.PartyId))
                    {
                        // the current number of entries in the dictionary is
                        // the column number
                        this.partyColumns.Add(
                            candidate.PartyId, this.partyColumns.Count);
                    }
                }
            }

            this.columnCount = this.partyColumns.Count;
        }

        #endregion

        #endregion

        #region Protected methods

        #region -- Position

        /// <summary>
        ///     Positions all elements of the ballot on the page, preparing 
        ///     everything for either exporting that info or for a drawing 
        ///     everything on the ballot and generating a PDF.
        /// </summary>
        /// <externalUnit cref="BreakType"/>
        /// <externalUnit cref="Candidate"/>
        /// <externalUnit cref="Contest"/>
        /// <externalUnit cref="PositionCandidates"/>
        /// <revision revisor="dev11" date="3/10/2009" version="1.0.8.2101">
        ///     Member Created
        /// </revision>
        protected override void Position()
        {
            // start from the upper-left corner of the first contest
            // this doesn't include the party row
            float x,
                  y = MatrixY + RowHeight;

            // colIdx is the column index (value returned by the 
            // parties dictionary)
            int[] samePartyCands;
            int colIdx;
            RectangleF bounds;

            foreach (Contest contest in contests)
            {
                // initialize the candidate of same party counters
                // on every new contest. This array helps keep track of 
                // candidates of the current contest, so that if the current 
                // contest is vote for > 1, the candidates for any given party 
                // are located top to bottom starting at the current y for the 
                // current contest
                samePartyCands = new int[this.columnCount];

                // initialize the x
                x = MatrixX;

                // get the rectangle for the contest header
                bounds = new RectangleF(
                    x, y, ColWidthContest, contest.VoteFor * RowHeight);
                contest.Bounds = bounds;

                // full face ballot, always page 0
                contest.Page = 0;

                // position texts using the current x [left border of header]
                // and current y [top of current contest header]
                this.PositionTexts(contest, x, y);

                // position all candidates for this contest
                foreach (Candidate candidate in contest.Candidates)
                {
                    colIdx = this.partyColumns[candidate.PartyId];

                    // the left of the first candidate column is the left of the
                    // matrix plus the width of the contest column
                    x = MatrixX + ColWidthContest + 

                        // the dictionary maps a party id to a column number
                        // 0-based
                        ColWidthCandidate * colIdx;
                    bounds = new RectangleF(x, y, ColWidthCandidate, RowHeight);

                    // depending on the current number of candidates for the
                    // current column, adjust to the next row so that no 2
                    // candidates are in the same cell
                    bounds.Y = y + samePartyCands[colIdx] * RowHeight;
                    candidate.Bounds = bounds;

                    // full face ballot, always page 0
                    candidate.Page = 0;

                    // increment the counter for this column for this contest
                    samePartyCands[colIdx] = samePartyCands[colIdx] + 1;
                }

                // position the internal elements of each candidate of the 
                // contest
                this.PositionCandidates(contest);

                // increment y to the next row
                y = y + RowHeight * contest.VoteFor;
            }
        }

        /// <summary>
        ///     Positions the arrow of the contest candidates
        /// </summary>
        /// <param name="contest">The contest.</param>
        /// <externalUnit cref="Candidate"/>
        /// <externalUnit cref="Contest"/>
        /// <externalUnit cref="PointF"/>
        /// <externalUnit cref="RectangleF"/>
        /// <externalUnit cref="TargetLayout"/>
        /// <revision revisor="dev11" date="3/10/2009" version="1.0.8.2101">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev11" date="03/23/2009" version="1.0.9.0701">
        ///     added support for oval target
        /// </revision>
        protected override void PositionCandidates(Contest contest)
        {
            PointF point;
            RectangleF bounds;
            foreach (Candidate candidate in contest.Candidates)
            {
                bounds = candidate.Bounds;
                point = new PointF();
                switch (target.Type)
                {
                    case TargetType.Oval:
                        point.X = bounds.X +
                                  (bounds.Width
                                   -
                                   PointsPerInchF
                                   * target.GetFltParam(TargetParam.Width)) / 
                                   2f;
                        point.Y = bounds.Bottom - CellPadding
                                  -
                                  PointsPerInchF
                                  * target.GetFltParam(TargetParam.Height);
                        break;
                    default:
                        // the arrow is positioned horizontally centered, 
                        // vertically aligned to the bottom of the cell
                        point.X = bounds.X
                                  + (bounds.Width - this.arrowGapWidth
                                     -
                                     Convert.ToSingle(
                                         PointsPerInchD * ArrTailX[3]))
                                    / 2f;

                        // y (top of arrow) = [cell bottom] - [cell padding] -
                        // [arrow height]
                        point.Y = bounds.Bottom - CellPadding - 
                            Convert.ToSingle(PointsPerInchF * ArrHeadY[6]);
                        break;
                }

                candidate.XYArrow = point;
            }
        }

        /// <summary>
        ///     Positions the texts of a contest using a supplied coordinates 
        ///     for the top left corner of the contest header.
        /// </summary>
        /// <param name="contest">The contest.</param>
        /// <param name="x">
        ///     The left side of the contest header (in points)
        /// </param>
        /// <param name="y">
        ///     The top side of the contest header (in points)
        /// </param>
        /// <externalUnit cref="OfficeText"/>
        /// <externalUnit cref="PDFxFont"/>
        /// <externalUnit cref="PointF"/>
        /// <externalUnit cref="RectangleF"/>
        /// <revision revisor="dev11" date="3/10/2009" version="1.0.">
        ///     Member Created
        /// </revision>
        protected override void PositionTexts(Contest contest, float x, float y)
        {
            List<OfficeText> texts = contest.Texts;

            float curY = y + CellPadding,
                  cx = x + ColWidthContest / 2f;

            for (int i = 0; i < texts.Count; i = i + 1)
            {
                OfficeText text = texts[i];
                PDFxFont font = GetFont(text.Font);
                double yMax = font.getYMax(text.FontSize),
                       yMin = font.getYMin(text.FontSize); // negative value

                PointF point = new PointF();
                point.X = cx; // relative to the page
                point.Y = Convert.ToSingle(curY + yMax); // relative to the page
                text.XYText = point;

                curY = curY + Convert.ToSingle(yMax - yMin);
            }
        }

        #endregion

        #region -- Draw

        /// <summary>
        ///     Draws the candidate on a PDF page. Includes box border, 
        ///     candidate name, arrow. Finally it updates [bottomY] and [rightX] 
        ///     that are internal variables to determine the extent of the 
        ///     candidates' area.
        /// </summary>
        /// <param name="cand">The candidate.</param>
        /// <externalUnit cref="Candidate"/>
        /// <externalUnit cref="PDFxFont"/>
        /// <externalUnit cref="RectangleF"/>
        /// <externalUnit cref="TargetLayout"/>
        /// <revision revisor="dev11" date="4/8/2009" version="1.0.10.0501">
        ///     Comments added
        /// </revision>
        protected override void DrawCandidate(Candidate cand)
        {
            RectangleF bounds = cand.Bounds;

            this.pdfPage.drawRect(
                bounds.Left,
                bounds.Top,
                bounds.Width,
                bounds.Height,
                false,
                true);

            PDFxFont font = this.GetFont(this.fntCand);
            this.pdfContext.setFont(font, this.fntCandSize);

            double cx = bounds.Left + bounds.Width / 2f,
                   cy = bounds.Top + CellPadding
                        + font.getYMax(this.fntCandSize);

            this.pdfPage.drawText(
                cx, cy, cand.Name, PDFxPanel.__Fields.ALIGN_CENTER);

            DrawTarget(cand);
        }

        /// <summary>
        ///     Draws the border around all candidates on the current page. This
        ///     method should be called after ALL candidates and contest headers 
        ///     have been drawn on the current page since [bottomY] needs to be 
        ///     updated for that first.
        /// </summary>
        /// <revision revisor="dev11" date="3/10/2009" version="1.0.8.2101">
        ///     Member Created
        /// </revision>
        protected override void DrawFrame()
        {
            // the upper left corner of the frame is defined by constants
            // MatrixX and MatrixY
            double left = MatrixX,
                   top = MatrixY,

                   // the width of the frame is the column width of the contests
                   // plus the width of all candidate columns
                   width = ColWidthContest
                           + this.columnCount * ColWidthCandidate,

                   // the height of the frame is the height of all rows as they
                   // all have the same height
                   height = (1 + this.rowCount) * RowHeight,

                   // get the center of party cells to place the party name 
                   // horizontally centered
                   cx = ColWidthCandidate / 2D,
                   cy = RowHeight / 2D;

            // this is the index of a given party in the esParties entry set
            int idx;

            pdfPage.drawRect(left, top, width, height, false, true);

            left = MatrixX + ColWidthContest;
            pdfContext.setBgColor(JColor.LIGHT_GRAY);
            foreach (KeyValuePair<int, int> pair in this.partyColumns)
            {
                // get the record index of the current party
                idx = this.esParties.FindIndex(StpParty.PartyId, pair.Key);

                // draw the party rectangle
                pdfPage.drawRect(
                    left, top, ColWidthCandidate, RowHeight, true, true);

                // draw the party name
                pdfPage.drawText(

                    // use the center of the bounding box
                    left + cx,
                    top + cy,

                    // get the party name from the entry set using the 
                    // found index
                    this.esParties.GetValueStr(idx, StpParty.PartyName),

                    // center the name horizontally
                    PDFxPanel.__Fields.ALIGN_CENTER);
                left = left + ColWidthCandidate;
            }
        }

        #endregion

        #endregion
    }
}
