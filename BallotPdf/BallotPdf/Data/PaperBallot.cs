// -----------------------------------------------------------------------------
// <copyright file="PaperBallot.cs" company="Sequoia Voting Systems"> 
//    Copyright (c) 2008 Sequoia Voting Systems, Inc. All Rights Reserved.
//    Distribution of source code is allowable only under the terms of the
//    license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//   This file contains the PaperBallot class.
// </summary>
// <revision revisor="dev11" date="12/3/2008" version="1.0.0.0">
//    File Created
// </revision>  
// <revision revisor="dev16" date="12/30/2008" version="1.0.0.0">
//    File Modified
// </revision>
// <revision revisor="dev06" date="1/10/2009" version="1.0.0.0">
//    File Modified
// </revision>
// <revision revisor="dev11" date="1/20/2009" version="1.0.5.1">
//    File Modified
// </revision>
// <revision revisor="dev11" date="2/10/2009" version="1.0.6.6">
//    File Modified
// </revision>
// <revision revisor="dev11" date="02/23/2009" version="1.0.8.0601">
//    static methods added
// </revision>
// <revision revisor="dev11" date="02/24/2009" version="1.0.8.0701">
//    File modified for code compliance</revision>
// <revision revisor="dev11" date="02/25/2009" version="1.0.8.0801">
//    File modified to support face reduction at PDF level and ability
//    to specify which pages to print</revision>
// <revision revisor="dev11" date="03/12/2009" version="1.0.8.2301">
//    File modified</revision>
// -----------------------------------------------------------------------------

namespace Sequoia.Ballot.Data
{
    #region Using directives

    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Text;

    using com.thentech.ds.ext;
    using com.thentech.pdf.ext;

    using Sequoia.DomainObjects;
    using Sequoia.Ems.Interop;
    using Sequoia.Ems.Interop.Exception;
    using Sequoia.Utilities;

    using JColor = java.awt.Color;
    using StpBallot = BallotEntrySet.StpBallot;
    using StpParam = BallotEntrySet.StpParam;

    #endregion

    /// <summary>
    ///     This class represents a paper ballot. It is useful for PDF 
    ///     generation, and machine initialization using positioning information
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev11" date="12/3/2008 1:54:13 PM" version="1.0.?.0">
    ///     Class created.
    /// </revision>
    /// <revision revisor="dev06" date="1/10/2009" version="1.0.0.0">
    ///     Modified the Draw method to return the number of pages/faces that 
    ///     were created (as pdfs).
    /// </revision>
    /// <revision revisor="dev11" date="01/12/2009" version="1.0.0.0">
    ///     Now properly creating front faces of all cards by drawing the 
    ///     corresponding barcode and all static artifacts (registration marks, 
    ///     instructions, etc)
    /// </revision>
    /// <revision revisor="dev11" date="01/13/2009" version="1.0.0.0">
    ///     Added support for a border around all candidates on every page when
    ///     drawing the PDF files</revision>
    /// <revision revisor="dev11" date="02/10/2009" version="1.0.6.6">
    ///     solved code review issues
    /// </revision>
    /// <revision revisor="dev11" date="03/31/2009" version="1.0.9.1501">
    ///     added a property to mark the ballot as a test ballot
    /// </revision>
    public class PaperBallot
    {
        #region Constants

        /// <summary>
        ///     X-coordinates for the head of the arrow
        /// </summary>
        public static readonly double[] ArrHeadX = new[]
                                                       {
                                                           0, 0.08, 0.08, 0.14,
                                                           0.14, 0.08, 0.08
                                                       };

        /// <summary>
        ///     Y-coordinates for the head of the arrow
        /// </summary>
        public static readonly double[] ArrHeadY = new[]
                                                       {
                                                           0.08, 0, 0.04, 0.04,
                                                           0.12, 0.12, 0.16
                                                       };

        /// <summary>
        ///     X-coordinates for the tail of the arrow
        /// </summary>
        /// <revision revisor="dev11" date="01/19/2009" version="1.0.4.20">
        ///     The fixed gap of 0.15" was removed in order to be configurable 
        ///     on the database
        /// </revision>
        public static readonly double[] ArrTailX = new[]
                                                       {
                                                         0.14, 0.19, 0.21, 0.25,
                                                         0.25, 0.21, 0.19, 0.14
                                                       };

        /// <summary>
        ///     Y-coordinates for the tail of the arrow
        /// </summary>
        public static readonly double[] ArrTailY = new[]
                                                       {
                                                         0.12, 0.12, 0.14, 0.14,
                                                         0.02, 0.02, 0.04, 0.04
                                                       };

        /// <summary>
        ///     X-coordinates for the mark of the arrow joining head and tail
        /// </summary>
        public static readonly double ArrMarkX = 0.14;

        /// <summary>
        ///     Y-coordinates for the mark of the arrow joining head and tail
        /// </summary>
        public static readonly double ArrMarkY = 0.08;

        /// <summary>
        ///     defines the arrow length (only head and tail, no gap within)
        /// </summary>
        public static readonly double ArrowLength = ArrTailX[3];

        /// <summary>
        ///     initial value for both BallotGenerationPlugin and 
        ///     MachineInitializationPlugin
        /// </summary>
        public const int CardCountStartsFrom = 1;

        /// <summary>
        ///     initial value for both BallotGenerationPlugin and 
        ///     MachineInitializationPlugin
        /// </summary>
        public const int FaceCountStartsFrom = 1;

        /// <summary>
        ///     Use this factor to convert from Inches to Points and viceversa
        ///     (to simplify code, a constant is provided as a double)
        /// </summary>
        protected const double PointsPerInchD = 72D;

        /// <summary>
        ///     Use this factor to convert from Inches to Points and viceversa
        ///     (to simplify code, a constant is provided as a float)
        /// </summary>
        protected const float PointsPerInchF = 72F;

        /// <summary>
        ///     back page header height (does not include page margin; in 
        ///     points) 0.5" -> convert to points: multiply by 72D
        /// </summary>
        protected const double BackHeaderHeight = 0.5 * PointsPerInchD;

        /// <summary>
        ///     registration marks are squares of 1/4" on each side (in points)
        /// </summary>
        protected const double RegistrationMarkSide = 0.25 * PointsPerInchD;

        /// <summary>
        ///     registration marks are located on the corners of the page at 
        ///     1/4" from the respective borders (in points)
        /// </summary>
        protected const double RegistrationMarkMargin = 0.25 * PointsPerInchD;

        /// <summary>
        ///     the left side of the barcode is at this distance from the right
        ///     border of the page. When the barcode is vertical, this distance 
        ///     is from the top of the page
        /// </summary>
        protected const double BarcodeRightOffset = 3 * PointsPerInchD;

        /// <summary>
        ///     defines the font size for the test deck index
        /// </summary>
        protected const double TestDeckIndexFontSize = 12;

        /// <summary>
        ///     distance of ballot id (barcode) left side from the right border 
        ///     of the page (in points)
        /// </summary>
        protected const double BallotIdLeftFromPageRight = 3D * PointsPerInchD;

        /// <summary>
        ///     the only reason to use this multiplier is because depending on 
        ///     the decimal places of a column width occasionally produced an 
        ///     additional column that extends beyond the right side of the 
        ///     paper
        /// </summary>
        protected const double ColumnWidthMultiplier = 1.5;

        /// <summary>
        ///     instructions text bottom margin
        /// </summary>
        protected const double InstructionsBottomMargin = 0.125 * 
            PointsPerInchD;

        /// <summary>
        ///     Pattern to be applied on test decks
        /// </summary>
        protected const string TestDeckNamePattern = "{0}{1}{2:000#}";

        #endregion

        #region Fields

        /// <summary>
        ///     the context of the PDF document (handles current formatting)
        /// </summary>
        protected PDFxContext pdfContext;

        /// <summary>
        ///     the current PDF page used to draw elements on it
        /// </summary>
        protected PDFxPage pdfPage;

        /// <summary>
        ///     candidate font name
        /// </summary>
        protected string fntCand;

        /// <summary>
        ///     candidate font point size
        /// </summary>
        protected double fntCandSize;

        /// <summary>
        ///     contest font name
        /// </summary>
        protected string fntCont;

        /// <summary>
        ///     page size (in points)
        /// </summary>
        protected SizeF pageSize;

        /// <summary>
        ///     ballot page margin (in points)
        /// </summary>
        protected double pageMargin;

        /// <summary>
        ///     Ballot card identifier that is encoded in the bar code. this 
        ///     code is the same for all card of a ballot except for the CDE 
        ///     field which corresponds to the card index.
        /// </summary>
        protected PaperBallotIdentifier identifier;

        /// <summary>
        ///     collection of contests for the ballot
        /// </summary>
        protected List<Contest> contests;

        /// <summary>
        ///     Param for tracking global card count accross all ballots
        /// </summary>
        protected int currentCardCount = 0;

        /// <summary>
        ///     Param for tracking global face count accross all ballots 
        ///     and cards
        /// </summary>
        protected int currentFaceCount = 0;

        /// <summary>
        ///     defines the gap width between head and tail of the arrow 
        ///     (in points)
        /// </summary>
        protected float arrowGapWidth;

        /// <summary>
        ///     handles the voting target
        /// </summary>
        protected PaperBallotTarget target;

        /// <summary>
        ///     an AquaPDF task to handle PDF generation
        /// </summary>
        private DSxTask dsTask;

        /// <summary>
        ///     page footer height (front and back; does not include page 
        ///     margin; in points)
        ///     0.3" -> convert to points: multiply by 72D
        /// </summary>
        /// <revision revisor="dev11" date="02/24/2009" version="1.0.8.0701">
        ///     Changed to 0.5" since at 0.3" candidates were too close to the
        ///     registration marks and the tabulator was having problems to 
        ///     detect the registration marks
        /// </revision>
        private const double FooterHeight = 0.25 * PointsPerInchD;

        /// <summary>
        ///     The watermark font size
        /// </summary>
        private const double WatermarkFontSize = 48.0;

        /// <summary>
        ///     the PDF document for this ballot
        /// </summary>
        private PDFxDocument pdfDoc;

        /// <summary>
        ///     contest font point size
        /// </summary>
        private double fntContSize;

        /// <summary>
        ///     the collection of all avalable fonts for the document generation
        /// </summary>
        private Dictionary<string, PDFxFont> fonts;

        /// <summary>
        ///     number of columns on the page
        /// </summary>
        private int columns;

        /// <summary>
        ///     candidate box height (in points)
        /// </summary>
        private double candHeight;

        /// <summary>
        ///     column width (in points)
        /// </summary>
        private float columnWidth;

        /// <summary>
        ///     Its a padding value (in inches) to use inside candidate and 
        ///     contest boxes
        /// </summary>
        /// <revision revisor="dev11" date="03/18/2009" version="1.0.9.0201">
        ///     changed to [float]
        /// </revision>
        private float boxPadding;

        /// <summary>
        ///     front page header height (does not include page margin; in points)
        /// </summary>
        private double headerHeight;

        /// <summary>
        ///     page size (in inches)
        /// </summary>
        private SizeF pageSizeInches;

        /// <summary>
        ///     If there is a double width column
        /// </summary>
        private bool doubleSpan = false;

        /// <summary>
        ///     specifies that for any given contest header, the height must be
        ///     multiple of candidate box height, producing a perfect grid on 
        ///     the ballot page
        /// </summary>
        private bool ballotGrid;

        /// <summary>
        ///     font folder
        /// </summary>
        private string fontFolder;

        /// <summary>
        ///     this is the bottom Y coordinate of the candidate(s) at the 
        ///     bottom of the current page, and this coordinate is needed to 
        ///     draw the frame around all candidate boxes
        /// </summary>
        private double bottomY;

        /// <summary>
        ///     this is the right X coordinate of the right-most candidate(s) on 
        ///     the current page, and this coordinate is needed to draw the 
        ///     column frames
        /// </summary>
        private double rightX;

        /// <summary>
        ///     For voted candidates, this value is the thickness of the mark on
        ///     the voting target for those candidates (in points)
        /// </summary>
        private double markWidth;

        /// <summary>
        ///     This enum type is used to place the voting target to the left or 
        ///     right side of the candidate box on a ballot
        /// </summary>
        private TargetLayout arrowLayout;

        /// <summary>
        ///     Defines whether the identifier (barcode) should be put on the 
        ///     front or back of the paper
        /// </summary>
        private PaperSide identifierFace;

        /// <summary>
        ///     the ballot style id of this paper ballot
        /// </summary>
        private int ballotStyelId;

        /// <summary>
        ///     registration mark quiet zone (in points)
        /// </summary>
        private double regMarkQuietZone;

        /// <summary>
        ///     it's a sequential number assigned externally that defines 
        ///     whether a ballot is part of a test deck and therefore includes 
        ///     all test artifacts. When this variable is not null and greater 
        ///     than zero the ballot is treated as a test ballot. Otherwise, it 
        ///     is a regular ballot
        /// </summary>
        private int? testDeckIndex;

        /// <summary>
        ///     name of the test deck. This name appear at the bottom of the 
        ///     ballot along with the test deck index (shifted to be 1-based). 
        ///     Notice that this value appears only the test deck index 
        ///     is not null
        /// </summary>
        private string testDeckName;

        /// <summary>
        ///     Folder to save files
        /// </summary>
        private string outputFolder = string.Empty;

        #endregion
   
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="PaperBallot"/> class.
        /// </summary>
        /// <externalUnit cref="contests"/>
        /// <externalUnit cref="Dictionary{TKey,TValue}"/>
        /// <externalUnit cref="fonts"/>
        /// <externalUnit cref="List{T}"/>
        /// <externalUnit cref="PDFxFont"/>
        /// <revision revisor="dev11" date="3/10/2009" version="1.0.8.2101">
        ///     Member Created
        /// </revision>
        public PaperBallot()
        {
            // initialize all collections
            this.contests = new List<Contest>();
            this.fonts = new Dictionary<string, PDFxFont>();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="PaperBallot"/> class
        ///     using DB information via entry sets
        /// </summary>
        /// <param name="entrySetBallots">
        ///     The es ballots.
        /// </param>
        /// <param name="entrySetContests">
        ///     The es conts.
        /// </param>
        /// <param name="entrySetCandidates">
        ///     The es cands.
        /// </param>
        /// <param name="entrySetParams">
        ///     The es params.
        /// </param>
        /// <param name="target">
        ///     The target.
        /// </param>
        /// <param name="ballotStyleId">
        ///     The ballot style id.
        /// </param>
        /// <externalUnit cref="BallotEntrySet"/>
        /// <externalUnit cref="PaperBallot(BallotEntrySet,BallotEntrySet,BallotEntrySet,BallotEntrySet,PaperBallotTarget,int,int,int)"/>
        /// <revision revisor="dev11" date="12/23/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev06" date="1/10/2009" version="1.0.0.0">
        ///     Added new constructor for card and face coutns, so chained
        /// </revision>
        public PaperBallot(
            BallotEntrySet entrySetBallots, 
            BallotEntrySet entrySetContests, 
            BallotEntrySet entrySetCandidates, 
            BallotEntrySet entrySetParams, 
            PaperBallotTarget target, 
            int ballotStyleId)
            : this(
            entrySetBallots, 
            entrySetContests, 
            entrySetCandidates, 
            entrySetParams, 
            target, 
            ballotStyleId, 
            0, 
            0)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="PaperBallot"/> class.
        /// </summary>
        /// <param name="entrySetBallots">
        ///     The es ballots.
        /// </param>
        /// <param name="entrySetContests">
        ///     The es conts.
        /// </param>
        /// <param name="entrySetCandidates">
        ///     The es cands.
        /// </param>
        /// <param name="entrySetParams">
        ///     The es params.
        /// </param>
        /// <param name="target">
        ///     The target.
        /// </param>
        /// <param name="ballotStyleId">
        ///     The ballot style id.
        /// </param>
        /// <param name="currentCardCount">
        ///     The current card count. This parameter
        ///     is used on the XML file generation as an offset for the card id
        /// </param>
        /// <param name="currentFaceCount">
        ///     The current face count. This parameter
        ///     is used on the XML file generation as an offset for the face id
        /// </param>
        /// <externalUnit cref="BallotEntrySet"/>
        /// <externalUnit cref="currentCardCount"/>
        /// <externalUnit cref="currentFaceCount"/>
        /// <externalUnit cref="contests"/>
        /// <externalUnit cref="fonts"/>
        /// <externalUnit cref="Load(BallotEntrySet,BallotEntrySet,BallotEntrySet,BallotEntrySet,int)"/>
        /// <externalUnit cref="Position"/>
        /// <revision revisor="dev06" date="1/10/2009" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public PaperBallot(
            BallotEntrySet entrySetBallots, 
            BallotEntrySet entrySetContests, 
            BallotEntrySet entrySetCandidates, 
            BallotEntrySet entrySetParams, 
            PaperBallotTarget target, 
            int ballotStyleId, 
            int currentCardCount, 
            int currentFaceCount) : this()
        {
            this.target = target;

            // set the card and face counts
            this.currentCardCount = currentCardCount;
            this.currentFaceCount = currentFaceCount;
            this.Load(
                entrySetBallots,
                entrySetContests,
                entrySetCandidates,
                entrySetParams,
                ballotStyleId);
            this.Position();
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the ballot style id that was used to create this instance 
        ///     as specified on the constructor
        /// </summary>
        /// <value>The ballot style id.</value>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="2/25/2009" version="1.0.8.0801">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev11" date="03/11/2009" version="1.0.8.2201">
        ///     value is no longer stored on the barcode but on a private field
        /// </revision>
        public int BallotStyleId
        {
            get
            {
                return this.ballotStyelId;
            }
        }

        /// <summary>
        ///     Gets or sets the precinct id
        /// </summary>
        /// <value>The precinct.</value>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="3/2/2009" version="1.0.8.1301">
        ///     Member Created
        /// </revision>
        public object Precinct
        {
            get
            {
                return this.identifier.Get(IdField.Precinct);
            }

            set
            {
                this.identifier.Set(IdField.Precinct, value);
            }
        }

        /// <summary>
        ///     Gets the face count.
        /// </summary>
        /// <value>The face count.</value>
        /// <externalUnit cref="Candidate"/>
        /// <externalUnit cref="Contest"/>
        /// <revision revisor="dev11" date="3/2/2009" version="1.0.8.1301">
        ///     Member Created
        /// </revision>
        public int FaceCount
        {
            get
            {
                // get the last contest
                Contest lastCont = this.contests[this.contests.Count - 1];

                // get the last candidate of that contest
                Candidate lastCand = 
                    lastCont.Candidates[lastCont.Candidates.Count - 1];

                // since page is 0-based, add 1 to get the total count of faces
                // the page (face) of the candidate is the last page of the 
                // ballot
                return lastCand.Page + 1;
            }
        }

        /// <summary>
        ///     Gets the card count.
        /// </summary>
        /// <value>The card count.</value>
        /// <externalUnit cref="FaceCount"/>
        /// <revision revisor="dev11" date="3/2/2009" version="1.0.8.1301">
        ///     Member Created
        /// </revision>
        public int CardCount
        {
            get
            {
                return Convert.ToInt32(
                    Math.Ceiling(
                        Convert.ToDouble(this.FaceCount) / 2D));
            }
        }

        /// <summary>
        ///     Gets the identifier face (either Front or Back)
        /// </summary>
        /// <value>The identifier face.</value>
        /// <externalUnit cref="PaperSide"/>
        /// <revision revisor="dev11" date="3/2/2009" version="1.0.8.1301">
        ///     Member Created
        /// </revision>
        public PaperSide IdentifierFace
        {
            get
            {
                return this.identifierFace;
            }
        }

        /// <summary>
        ///     Gets or sets the index of the ballot within a test deck.
        ///     If the value is null or lesser than 1, the ballot is a regular 
        ///     ballot. Otherwise it is considered as part of a test deck
        /// </summary>
        /// <value>The index of the test deck.</value>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="3/31/2009" version="1.0.9.1501">
        ///     Member Created
        /// </revision>
        public int? TestDeckIndex
        {
            get
            {
                return this.testDeckIndex;
            }

            set
            {
                this.testDeckIndex = value;
            }
        }

        /// <summary>
        ///     Gets or sets the name of the test deck that this ballot 
        ///     belongs to.
        /// </summary>
        /// <value>The name of the test deck.</value>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="4/20/2009" version="1.0.11.0801">
        ///     Member Created
        /// </revision>
        public string TestDeckName
        {
            get
            {
                return this.testDeckName;
            }

            set
            {
                this.testDeckName = value;
            }
        }

        /// <summary>
        ///     Gets or sets the election info.
        /// </summary>
        /// <value>The election info.</value>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="4/2/2009" version="1.0.9.1701">
        ///     Member Created
        /// </revision>
        public PaperBallotElectionInfo ElectionInfo
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the TXML template with all rich texts
        /// </summary>
        /// <value>The templates.</value>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="4/10/2009" version="1.0.11.0101">
        ///     Member Created
        /// </revision>
        public PaperBallotRichTextTemplate Template
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the name of the ballot style.
        /// </summary>
        /// <value>The name of the ballot style.</value>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="4/15/2009" version="1.0.11.0501">
        ///     Member Created
        /// </revision>
        public string BallotStyleName
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the name of the precinct.
        /// </summary>
        /// <value>The name of the precinct.</value>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="4/15/2009" version="1.0.11.0501">
        ///     Member Created
        /// </revision>
        public string PrecinctName
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is 
        ///     audio control.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is audio control; 
        ///     otherwise, <c>false</c>.
        /// </value>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="11/2/2009" version="1.1.2.1101">
        ///     Member Created
        /// </revision>
        public bool IsAudioBallot
        {
            get
            {
                // this is a audio control ballot when the field for that is
                // part of the identifier and it has a true value. Otherwise
                // it's not
                return this.identifier.HasField(IdField.ControlBallot)
                        &&
                        ((ControlBallotType)
                         this.identifier.Get(IdField.ControlBallot)
                         == ControlBallotType.AudioBallot);
            }

            set
            {
                if (value)
                {
                    // set the value to that field
                    this.identifier.Set(
                        IdField.ControlBallot,
                        ControlBallotType.AudioBallot);
                }
                else
                {
                    // remove the field from the identifier
                    this.identifier.Remove(IdField.ControlBallot);
                }
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Generates the ballot PDF and saves it to the specified path
        /// </summary>
        /// <param name="pathPdf">
        ///     The path PDF.
        /// </param>
        /// <externalUnit cref="Pdf"/>
        /// <externalUnit cref="dsTask"/>
        /// <externalUnit cref="DSxTask"/>
        /// <externalUnit cref="Draw()"/>
        /// <externalUnit cref="DSxPDFDocument"/>
        /// <externalUnit cref="pdfDoc"/>
        /// <externalUnit cref="DSxOutput"/>
        /// <externalUnit cref="DSxRuntimeException"/>
        /// <externalUnit cref="PdfIOException"/>
        /// <revision revisor="dev11" date="12/16/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev11" date="04/10/2009" version="1.0.11.0101">
        ///     added temp repository as the font folder
        /// </revision>
        public void Draw(string pathPdf)
        {
            // before doing anything with the AquaPDF library, the license key
            // has to be set first 
            Pdf.Activate();

            // a task has to be created for everything and cannot be closed 
            // before finishing the entire drawing process 
            this.dsTask = new DSxTask();

            this.dsTask.setTempRepository(new DSxRepository(this.fontFolder));

            // create the pdf document 
            this.Draw();

            // in order to save the file, a document wrapper is needed 
            DSxPDFDocument dsDocument = new DSxPDFDocument(this.pdfDoc);
            try
            {
                // save the file to the specified path 
                dsDocument.save(new DSxOutput(pathPdf));
            }
            catch (DSxRuntimeException)
            {
                // the actual exception catched is [com.thentech.ds.t111.t158],
                // which happens to be a subclass of DSxRuntimeException, but
                // doesn't have any info, so simply disregard it and rethrow it
                // as a [PdfIOException] exception that will be handled by the
                // caller method 
                throw new PdfIOException();
            }
            finally
            {
                dsDocument.close();

                // release all resources 
                this.dsTask.close();
            }
        }

        /// <summary>
        ///     Generates the ballot PDFs (each page is saved to a separate 
        ///     file) and saves them to the specified path using the specified 
        ///     name and the page index. This method will create the output 
        ///     folder in case it doesn't exist (Pdf.ExtractPagesAsSeparateFiles 
        ///     will do it)
        /// </summary>
        /// <param name="outputFolder">
        ///     The output folder.
        /// </param>
        /// <param name="filename">
        ///     The filename.
        /// </param>
        /// <externalUnit cref="DrawSubCall"/>
        /// <exception cref="PdfIOException">
        ///     Unable to write at least 1 of the PDF files
        /// </exception>
        /// <revision revisor="dev11" date="12/29/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev06" date="1/10/2009" version="1.0.0.0">
        ///     Updated to return the number of pages/faces that are drawn
        /// </revision>
        /// <revision revisor="dev11" date="02/25/2009" version="1.0.8.0801">
        ///     Refactored to call <see cref="DrawSubCall"/>
        /// </revision>
        /// <returns>
        ///     The drawing.
        /// </returns>
        public int Draw(string outputFolder, string filename)
        {
            return this.DrawSubCall(outputFolder, filename, null);
        }

        /// <summary>
        ///     Generates the ballot PDFs (each page is saved to a separate 
        ///     file)and saves them to the specified path using the specified 
        ///     name and the page index. This method will create the output 
        ///     folder in case it doesn't exist (Pdf.ExtractPagesAsSeparateFiles 
        ///     will do it)
        /// </summary>
        /// <param name="outputFolder">
        ///     The output folder.
        /// </param>
        /// <param name="filename">
        ///     The filename.
        /// </param>
        /// <param name="faces">
        ///     The face numbers.
        /// </param>
        /// <returns>
        ///     The drawing.
        /// </returns>
        /// <externalUnit cref="DrawSubCall"/>
        /// <exception cref="PdfIOException">
        ///     Unable to write at least 1 of the PDF files
        /// </exception>
        /// <revision revisor="dev11" date="12/29/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev06" date="1/10/2009" version="1.0.0.0">
        ///     Updated to return the number of pages/faces that are drawn
        /// </revision>
        /// <revision revisor="dev11" date="02/25/2009" version="1.0.8.0801">
        ///     Refactored to call <see cref="DrawSubCall"/>
        /// </revision>
        public int Draw(string outputFolder, string filename, int[] faces)
        {
            return this.DrawSubCall(outputFolder, filename, faces);
        }

        /// <summary>
        ///     Exports the positions and relevant properties to a Ballot object
        ///     Notice that corelation between mark ids for face i and candidate 
        ///     ids on page i is kept only when the barcode is on the front. 
        ///     When the barcode is on the back, the marks from front and back 
        ///     are swaped
        /// <see cref="Ballot"/>
        /// </summary>
        /// <returns>
        /// </returns>
        /// <externalUnit cref="Ballot"/>
        /// <externalUnit cref="Candidate"/>
        /// <externalUnit cref="Card"/>
        /// <externalUnit cref="CardList"/>
        /// <externalUnit cref="Contest"/>
        /// <externalUnit cref="contests"/>
        /// <externalUnit cref="currentCardCount"/>
        /// <externalUnit cref="currentFaceCount"/>
        /// <externalUnit cref="Face"/>
        /// <externalUnit cref="FaceList"/>
        /// <externalUnit cref="identifier"/>
        /// <externalUnit cref="IdField"/>
        /// <externalUnit cref="Mark"/>
        /// <externalUnit cref="PointF"/>
        /// <revision revisor="dev11" date="12/16/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev06" date="01/10/2008" version="1.0.0.0">
        ///     Added tracking of global cards and faces
        /// </revision>
        /// <revision revisor="dev11" date="01/12/2008" version="1.0.0.0">
        ///     Moved offset values inside the rounding argument. Even if any of 
        ///     the offset values is a number with 3 decimals, the resulting 
        ///     number as serialized in the resulting XMLs is not. By rounding 
        ///     the resulting number the precision is guaranteed
        /// </revision>
        /// <revision revisor="dev11" date="01/13/2008" version="1.0.4.13">
        ///     Now the barcode is updated with the current card id using the
        ///     [currentCardCount]. Notice that the card id is being used as
        ///     the [CDE] field of the barcode
        /// </revision>
        /// <revision revisor="dev11" date="01/20/2009" version="1.0.5.1">
        ///     Added support for the [TargetLayout] parameter as the target
        ///     position is different for each of the two values
        /// </revision>
        /// <revision revisor="dev11" date="02/27/2009" version="1.0.8.0901">
        ///     Previously the [IdField.BallotTypeId] was being used, now
        ///     [BallotStyleId] is used
        /// </revision>
        /// <revision revisor="dev11" date="03/02/2009" version="1.0.8.1301">
        /// </revision>
        /// <revision revisor="dev11" date="03/06/2009" version="1.0.8.1701">
        ///     changes added to support newly added ballot style id and 
        ///     precinct id attribute in <see cref="Ballot"/>
        /// </revision>
        /// <revision revisor="dev11" date="03/07/2009" version="1.0.8.1801">
        ///     when the barcode is on the back, the face marks of every card 
        ///     are swapped so that the tabulator thinks the front is the face 
        ///     with the barcode
        /// </revision>
        /// <revision revisor="dev11" date="03/11/2009" version="1.0.8.2201">
        ///     barcode is now optional
        /// </revision>
        /// <revision revisor="dev11" date="03/16/2009" version="1.0.8.2701">
        ///     added comments, vars renamed
        /// </revision>
        /// <revision revisor="dev11" date="03/23/2009" version="1.0.9.0701">
        ///     parameters removed. Offset values are now taken from the target.
        ///     See <see cref="PaperBallotTarget"/>, <see cref="target"/>
        /// </revision>
        public Ballot ExportPositions()
        {
            // this is the return object of this method
            Ballot ballot = new Ballot();

            // variable for cards
            Card card = null;

            // face is used at all times, faceB is used when the barcode is on
            // the back and the faces have to be swapped
            Face face = null, faceB;

            // variables for marks and mark lists
            Mark mark;
            MarkList markList;

            // the current page is initialized as -1 since page is 0-based and
            // no page is selected at the beginning. Also, this guarantees that
            // the first page found is treated as a new page
            // i.e. [current page] != [new page]
            // -1 != [page of the first candidate, 0-based]
            int curPage = -1;

            // card and face ids are assigned sequentially using the initializer
            // fields. This way ids are sequential across multiple ballots
            int cardId = this.currentCardCount;
            int faceId = this.currentFaceCount;

            // this is the upper-left corner of the gap between head and tail
            // of the target arrow for a given candidate
            PointF xyTarget;

            // since the precinct is optional on the barcode, use an object-type
            // variable to get the value from the barcode
            object objPrecId;

            // initialize the collection of cards on the ballot (domain object)
            ballot.Cards = new CardList();

            // and the ballot style id
            ballot.BallotStyleId = this.ballotStyelId;

            // get the precinct id from the identifier
            objPrecId = this.identifier.Get(IdField.Precinct);
            if (objPrecId != null)
            {
                // if it was found, set it to the ballot object
                ballot.PrecinctId = (int)objPrecId;

                // in case it wasn't found, precinct id attribute on the
                // ballot object will retain its default value (zero)
            }

            // group candidate by page: all candidates in the same page are
            // a face. Every even page number is a front face and hence a new
            // card
            foreach (Contest cont in this.contests)
            {
                foreach (Candidate cand in cont.Candidates)
                {
                    if (curPage != cand.Page)
                    {
                        // even-odd checking. This if statement only allows
                        // even page numbers, since those are front pages
                        if (cand.Page % 2 == 0)
                        {
                            // current page number is even, meaning a front and
                            // a new card
                            card = new Card();

                            // the barcode on the card has the card index (code)
                            // so assign the current card id to the barcode and
                            // then convert it to string and assign it to the
                            // card
                            this.identifier.Set(IdField.Code, cardId);
                            card.Barcode = this.identifier.ToString();

                            // initialize the face list and the card id
                            card.Faces = new FaceList();
                            card.Id = cardId;

                            // add the card to the collection of cards of the 
                            // ballot
                            ballot.Cards.Add(card);

                            // increment the card index
                            cardId = cardId + 1;
                        }

                        // a new page was found, meaning that a new face is 
                        // needed
                        face = new Face();

                        // initialize the mark list and face id
                        face.Marks = new MarkList();
                        face.Id = faceId;

                        // add the face to the current card
                        card.Faces.Add(face);

                        // save the current page number and increment the 
                        // face index
                        curPage = cand.Page;
                        faceId = faceId + 1;
                    }

                    // every candidate represents a mark, that's why the mark id
                    // is actually the face id
                    mark = new Mark(cand.Id);

                    // get the actual location of the target arrow for the 
                    // current candidate
                    xyTarget = cand.GetXYTarget(
                        this.arrowLayout, this.target.Type);

                    // now, this method receives 2 parameters: offset X and Y
                    // these are used to move help improve accuracy on the 
                    // tabulator readings by reducing the chance of picking up 
                    // part of the head or tail of the arrow inside the scanning 
                    // area. Additionally, round to the specified number of 
                    // decimal digits to reduce complexity on the XML files
                    mark.LeftX = Math.Round(
                        this.target.GetDblParam(TargetParam.OffsetX)
                        + Convert.ToDouble(xyTarget.X),
                        Candidate.Precision);
                    mark.TopY = Math.Round(
                        this.target.GetDblParam(TargetParam.OffsetY)
                        + Convert.ToDouble(xyTarget.Y),
                        Candidate.Precision);

                    // add the mark to the current face
                    face.Marks.Add(mark);
                }
            }

            if (this.identifierFace == PaperSide.Back)
            {
                if ((card.Faces.Count == 1) && 
                    (this.identifier.HasFields == true))
                {
                    // at this point [card] corresponds to the last card of the 
                    // ballot and by definition a card (or a piece of paper for
                    // that matter) has always 2 faces (paper sides). The thing
                    // is that if all candidates and the barcode are on the 
                    // front then the back face is blank. However, if the 
                    // candidates are on the front but the barcode is on the 
                    // back, then an additional face definition has to be added 
                    // just for the barcode. That's why this [IF] clause says 
                    // [if faces == 1] meaning that the card only is using 
                    // the front
                    face = new Face();
                    face.Marks = new MarkList();
                    face.Id = faceId;
                    card.Faces.Add(face);
                }

                // now that all faces have been added to all cards, the content
                // of the faces has to be swaped since the tabulator considers
                // the face with the barcode the front, even that physically 
                // it's the back (or at least, in the context of this method)
                for (int i = 0; i < ballot.Cards.Count; i = i + 1)
                {
                    // get the card
                    card = ballot.Cards[i];

                    // since the barcode is optional, even when the barcode is
                    // on the back, if all candidates are on the front, the
                    // card has only the front face and nothing needs 
                    // to be swapped
                    if (card.Faces.Count == 2)
                    {
                        // get the front face
                        face = card.Faces[0];

                        // get the back face
                        faceB = card.Faces[1];

                        // swap marks
                        markList = face.Marks;
                        face.Marks = faceB.Marks;
                        faceB.Marks = markList;
                    }
                }
            }

            return ballot;
        }

        #endregion

        #region Static Methods

        /// <summary>
        ///     Creates a collection of paper faces from the specified ballot
        /// </summary>
        /// <param name="ballot">
        ///     The ballot.
        /// </param>
        /// <returns>
        ///     a list of faces of the given ballot
        /// </returns>
        /// <externalUnit cref="Candidate"/>
        /// <externalUnit cref="Contest"/>
        /// <externalUnit cref="HasIdentifier"/>
        /// <externalUnit cref="IdField"/>
        /// <externalUnit cref="List{T}"/>
        /// <externalUnit cref="PaperBallot"/>
        /// <externalUnit cref="PaperBallotIdentifier"/>
        /// <externalUnit cref="PaperFace"/>
        /// <externalUnit cref="PaperSide"/>
        /// <revision revisor="dev11" date="2/23/2009" version="1.0.8.0601">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev11" date="03/02/2009" version="1.0.8.1301">
        ///     When the barcode is on the back, but the ballot has an odd 
        ///     number of faces, an additional blank face has to be added just 
        ///     for the barcode. Added support for that.
        /// </revision>
        /// <revision revisor="dev11" date="03/18/2009" version="1.0.9.0201">
        ///     comments added/extended
        /// </revision>
        public static List<PaperFace> GetFaces(PaperBallot ballot)
        {
            List<Contest> contests = ballot.contests;
            PaperBallotIdentifier id = ballot.identifier;
            PaperSide idInThisSide = ballot.identifierFace;
            int curCardCount = ballot.currentCardCount;

            // create a new collection
            List<PaperFace> faces = new List<PaperFace>();

            // the current paper face
            PaperFace face = null;
            PaperBallotIdentifier barcode;

            // the current page index
            int curPage = -1, curCard;

            if (ballot.IsAudioBallot)
            {
                face = new PaperFace();
                faces.Add(face);
                barcode = id.Clone();
                face.Artifacts.Add(barcode);
            }
            else
            {
                // for every contest, get every candidate
                foreach (Contest contest in contests)
                {
                    // for every candidate get the candidate id and the 
                    // page number
                    foreach (Candidate candidate in contest.Candidates)
                    {
                        // if the page number is different than the current or 
                        // the face is null, create a new face and add it to
                        // the collection before adding candidate ids to it
                        if ((curPage != candidate.Page) || (face == null))
                        {
                            face = new PaperFace();
                            faces.Add(face);
                            curPage = candidate.Page;
                        }

                        // add the candidate id
                        face.Candidates.Add(candidate.Id);

                        // check for the barcode on the current page. 
                        // Notice that in this case, the page number is 0-based. 
                        // This is because AquaPDF handles pages as 1-based
                        if (HasIdentifier(curPage, true, idInThisSide) == true)
                        {
                            curCard = (curPage / 2) + curCardCount;
                            barcode = id.Clone();
                            barcode.Set(IdField.Code, curCard);
                            face.Artifacts.Add(barcode);
                        }
                    }
                }

                if ((ballot.identifier.HasFields == true)
                    && (ballot.IdentifierFace == PaperSide.Back)
                    && (faces.Count % 2 == 1))
                {
                    // When the barcode is on the back and the ballot has an odd 
                    // number of faces, an additional blank face has to be added 
                    // just for the barcode
                    face = new PaperFace();
                    faces.Add(face);

                    // since no candidates are in this page, increment the 
                    // page number manually
                    curPage = curPage + 1;

                    // now get the card number
                    curCard = (curPage / 2) + curCardCount;

                    // get a duplicate of the barcode so that the code field can 
                    // be modified without modifying other face's barcode
                    barcode = id.Clone();

                    // and set the card number
                    barcode.Set(IdField.Code, curCard);

                    // add the barcode to the blank face
                    face.Artifacts.Add(barcode);
                }
            }

            return faces;
        }

        /// <summary>
        ///     Builds the PDF base file name for the given ballot. It is a base 
        ///     name since the generation saves each page to a separate file 
        ///     using this name as the base and adding a two-digit 1-based 
        ///     number for the page before the file extension.
        /// </summary>
        /// <param name="ballot">
        ///     The ballot.
        /// </param>
        /// <returns>
        ///     the base name of the pdf file for the given ballot.
        ///     With precinct (the ballot has the precinct field on 
        ///     the barcode):
        ///     BLS###_PCT###.pdf
        ///     Without the precinct (the precinct field is not present on 
        ///     the barcode)
        ///     BLS###.pdf
        ///     ### is the 0-padded id where applicable
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="2/26/2009" version="1.0.8.0901">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev11" date="2/28/2009" version="1.0.8.1101">
        ///     Added support for precinct-specific ballots. Also, each part of 
        ///     the filename uses the corresponding barcode field name
        /// </revision>
        /// <revision revisor="dev11" date="03/18/2009" version="1.0.9.0201">
        ///     comments added / extended
        /// </revision>
        /// <revision revisor="dev11" date="11/02/2009" version="1.1.2.1101">
        ///     Added support for audio control ballots
        /// </revision>
        public static string GetPdfFilename(PaperBallot ballot)
        {
            string filename, control = string.Empty;
            object precinct = ballot.Precinct;

            if (ballot.IsAudioBallot)
            {
                control = string.Format(
                    "{0}{1}_",
                    EnumTextAttribute.GetText(IdField.ControlBallot),
                    Convert.ToInt32(ControlBallotType.AudioBallot));
            }

            if (precinct != null)
            {
                filename = // {0} first parameter
                    // {1:00#} second parameter, 0-padded 3-digit integer
                    // {2} third parameter
                    // {3:00#} fourth parameter, 0-padded 3-digit integer
                    string.Format(
                        "{4}{0}{1:00#}_{2}{3:00#}.pdf",

                        // insert the ballot style id field marker (BLS)
                        EnumTextAttribute.GetText(IdField.BallotStyleId),

                        // insert the actual value for the ballot syle
                        // id, that is formatted as a 0-padded 3-digit
                        // integer
                        ballot.BallotStyleId,

                        // insert the precinct id field marker (PCT)
                        EnumTextAttribute.GetText(IdField.Precinct),

                        // insert the actual value for the precinct
                        // id, that is formatted as a 0-padded 3-digit
                        // integer
                        precinct,
                        control);
            }
            else
            {
                filename =

                    // {0} first parameter
                    // {1:00#} second parameter, 0-padded 3-digit integer
                    string.Format(
                            "{2}{0}{1:00#}.pdf", 

                            // insert the ballot style id field marker (BLS)
                            EnumTextAttribute.GetText(IdField.BallotStyleId), 

                            // insert the actual value for the ballot syle
                            // id, that is formatted as a 0-padded 3-digit
                            // integer
                            ballot.BallotStyleId,
                            control);
            }

            return filename;
        }

        /// <summary>
        ///     Gets the PDF test filename.
        /// </summary>
        /// <param name="ballot">
        ///     The ballot.
        /// </param>
        /// <param name="ballotNumber">
        ///     The ballot number.
        /// </param>
        /// <returns>
        ///     The get pdf test filename.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="3/30/2009" version="1.0.9.1401">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev11" date="04/14/2009" version="1.0.11.0401">
        ///     added support for precincts.
        ///     ballot number is now 1-based (filename and printed on ballot 
        ///     only) the parameter for this methods is still 0-based.
        /// </revision>
        public static string GetPdfTestFilename(
            PaperBallot ballot, int ballotNumber)
        {
            string filename;
            object precinct = ballot.Precinct;
            if (precinct != null)
            {
                filename = string.Format(
                        "TestDeck_{0}{1:00#}_{2}{3:00#}_N{4:000#}.pdf",
 
                        // insert the ballot style id field marker (BLS)
                        EnumTextAttribute.GetText(IdField.BallotStyleId), 

                        // insert the actual value for the ballot syle
                        // id, that is formatted as a 0-padded 3-digit
                        // integer
                        ballot.BallotStyleId, 

                        // insert the precinct id field marker (PCT)
                        EnumTextAttribute.GetText(IdField.Precinct), 

                        // insert the actual value for the precinct
                        // id, that is formatted as a 0-padded 3-digit
                        // integer
                        precinct, 

                        // insert the ballot number
                        1 + ballotNumber);
            }
            else
            {
                filename = string.Format(
                        "TestDeck_{0}{1:00#}_N{2:000#}.pdf", 

                        // insert the ballot style id field marker (BLS)
                        EnumTextAttribute.GetText(IdField.BallotStyleId), 

                        // insert the actual value for the ballot syle
                        // id, that is formatted as a 0-padded 3-digit
                        // integer
                        ballot.BallotStyleId, 

                        // insert the ballot number
                        1 + ballotNumber);
            }

            return filename;
        }

        /// <summary>
        ///     Determines whether the specified page number has identifier.
        /// </summary>
        /// <param name="pageNumber">
        ///     The page number.
        /// </param>
        /// <param name="numberIsZeroBased">
        ///     if set to <c>true</c> [number is zero based].
        /// </param>
        /// <param name="printIdentifierOnThisSide">
        ///     The print identifier on this side.
        /// </param>
        /// <returns>
        ///     <c>true</c> if the specified page number has identifier; 
        ///     otherwise, <c>false</c>.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="2/25/2009" version="1.0.8.0801">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev11" date="03/18/2009" version="1.0.9.0201">
        ///     comments added / extended
        /// </revision>
        private static bool HasIdentifier(
            int pageNumber, 
            bool numberIsZeroBased, 
            PaperSide printIdentifierOnThisSide)
        {
            bool hasIdentifier = false, 

                 // a number is even when the remainder is 0 after dividing by 2
                 numberIsEven = (pageNumber % 2) == 0, 

                 // conversely, a number is odd when it is not even
                 numberIsOdd = numberIsEven == false, 
                 printOnFront = printIdentifierOnThisSide == PaperSide.Front, 
                 printOnBack = printOnFront == false;

            if (numberIsZeroBased)
            {
                // page number is zero based:
                // page 0 -> front
                // page 1 -> back
                // page 2 -> front
                // ... and so on
                // when the page number is even, it refers to the front of the
                // paper, so if the setting says to print on the front, then
                // that page has an identifier (barcode) on it.
                // when the page number is odd, it refers to the back of the
                // paper, so if the setting says to print on the back, then
                // that page has an identifier (barcode) on it
                hasIdentifier = (numberIsEven == true)
                                    ? printOnFront
                                    : printOnBack;
            }
            else
            {
                // page number is not zero based, so it is 1 based:
                // page 1 -> front
                // page 2 -> back
                // page 3 -> front
                // ... and so on
                // when the page number is odd, it refers to the front of the
                // paper, so if the setting says to print on the front, then
                // that page has an identifier (barcode) on it.
                // when the page number is even, it refers to the back of the
                // paper, so if the setting says to print on the back, then
                // that page has an identifier (barcode) on it
                hasIdentifier = (numberIsOdd == true)
                                    ? printOnFront
                                    : printOnBack;
            }

            return hasIdentifier;
        }

        /// <summary>
        ///     Gets the type of the paper ballot.
        /// </summary>
        /// <param name="entrySetParams">
        ///     an entry set with all pdf parameters as stored on the database.
        /// </param>
        /// <returns>
        /// </returns>
        /// <externalUnit cref="BallotEntrySet"/>
        /// <externalUnit cref="Enum"/>
        /// <externalUnit cref="PaperBallotType"/>
        /// <externalUnit cref="PdfParam"/>
        /// <externalUnit cref="StpParam"/>
        /// <revision revisor="dev11" date="3/10/2009" version="1.0.8.2101">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev11" date="03/24/2009" version="1.0.9.0801">
        ///     updated to work with enum types and ids instead of names when 
        ///     searching on the entry set of pdf parameters
        /// </revision>
        public static PaperBallotType GetPaperBallotType(
            BallotEntrySet entrySetParams)
        {
            PaperBallotType type;

            // get the index of the paper ballot type parameter
            int idx = entrySetParams.FindIndex(
                StpParam.PDFLayoutParamId, (int) PdfParam.PaperBallotType);
            if (idx < 0)
            {
                // if not found, then use the default type
                type = PaperBallotType.PaperBallot;
            }
            else
            {
                try
                {
                    // otherwise, get the corresponding value
                    type =
                        (PaperBallotType)
                        Enum.ToObject(
                            typeof (PaperBallotType),
                            entrySetParams.GetValueInt(
                            idx, StpParam.ParamValue));
                }
                catch (Exception)
                {
                    // if not found, then use the default type
                    type = PaperBallotType.PaperBallot;
                }
            }

            return type;
        }

        /// <summary>
        ///     Creates a new instance of a paper ballot using the appropriate
        ///     PaperBallot class. See <see cref="PaperBallotType"/>
        /// </summary>
        /// <param name="entrySetBallots">
        ///     The es ballots.
        /// </param>
        /// <param name="entrySetContests">
        ///     The es conts.
        /// </param>
        /// <param name="entrySetCandidates">
        ///     The es cands.
        /// </param>
        /// <param name="entrySetParams">
        ///     The es params.
        /// </param>
        /// <param name="esContList">
        ///     The es cont list.
        /// </param>
        /// <param name="entrySetCandidateList">
        ///     The es cand list.
        /// </param>
        /// <param name="esParties">
        ///     The es parties.
        /// </param>
        /// <param name="target">
        ///     The target.
        /// </param>
        /// <param name="ballotStyleId">
        ///     The ballot style id.
        /// </param>
        /// <param name="cardCount">
        ///     The card count.
        /// </param>
        /// <param name="faceCount">
        ///     The face count.
        /// </param>
        /// <param name="paperBallotType">
        ///     Type of the paper ballot.
        /// </param>
        /// <returns>
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="3/10/2009" version="1.0.8.2101">
        ///     Member Created
        /// </revision>
        public static PaperBallot NewPaperBallot(
            BallotEntrySet entrySetBallots, 
            BallotEntrySet entrySetContests, 
            BallotEntrySet entrySetCandidates, 
            BallotEntrySet entrySetParams, 
            BallotEntrySet esContList, 
            BallotEntrySet entrySetCandidateList, 
            BallotEntrySet esParties, 
            PaperBallotTarget target, 
            int ballotStyleId, 
            int cardCount, 
            int faceCount, 
            PaperBallotType paperBallotType)
        {
            PaperBallot ballot;
            switch (paperBallotType)
            {
                    // depending on the paper ballot type use the appropriate
                    // constructor. Each class overrides specific methods to 
                    // provide a different layout
                case PaperBallotType.MatrixPtyCstPaperBallot:
                    ballot = new MatrixPtyCstPaperBallot(
                            entrySetBallots, 
                            entrySetContests, 
                            entrySetCandidates, 
                            entrySetParams, 
                            esContList, 
                            entrySetCandidateList, 
                            esParties, 
                            target, 
                            ballotStyleId, 
                            cardCount, 
                            faceCount);
                    break;
                default: // PaperBallotType.PaperBallot
                    ballot = new PaperBallot(
                            entrySetBallots, 
                            entrySetContests, 
                            entrySetCandidates, 
                            entrySetParams, 
                            target, 
                            ballotStyleId, 
                            cardCount, 
                            faceCount);
                    break;
            }

            return ballot;
        }

        #endregion
       
        #region Private Methods

        #region Load (Entry sets)

        /// <summary>
        ///     Loads the specified es ballots.
        /// </summary>
        /// <param name="entrySetBallots">
        ///     The es ballots.
        /// </param>
        /// <param name="entrySetContests">
        ///     The es conts.
        /// </param>
        /// <param name="entrySetCandidates">
        ///     The es cands.
        /// </param>
        /// <param name="entrySetParams">
        ///     The es params.
        /// </param>
        /// <param name="ballotStyleId">
        ///     The ballot style id.
        /// </param>
        /// <externalUnit cref="BallotEntrySet"/>
        /// <externalUnit cref="Contest"/>
        /// <externalUnit cref="contests"/>
        /// <externalUnit cref="LoadDefs(BallotEntrySet,int)"/>
        /// <externalUnit cref="StpBallot"/>
        /// <revision revisor="dev11" date="12/23/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        protected void Load(
            BallotEntrySet entrySetBallots, 
            BallotEntrySet entrySetContests, 
            BallotEntrySet entrySetCandidates, 
            BallotEntrySet entrySetParams, 
            int ballotStyleId)
        {
            this.LoadDefs(entrySetParams, ballotStyleId);

            // current entry index in esBallot entry set 
            int idxBal = entrySetBallots.FindIndex(
                StpBallot.BallotStyleId, ballotStyleId),

                // current ballot id, current contest id 
                curBal,
                curCst;

            // contest ids for contests already loaded 
            var contestIds = new HashSet<int>();

            do
            {
                // get the contest id 
                curCst = entrySetBallots.GetValueInt(
                    idxBal, StpBallot.ContestId);
                if (contestIds.Contains(curCst) == false)
                {
                    // if the contest has not been loaded, load it. Since the 
                    // driving collection is [entrySetBallots] every contest 
                    // appears once for every candidate
                    this.contests.Add(
                        Contest.LoadFromDb(
                            entrySetBallots, 
                            entrySetContests, 
                            entrySetCandidates, 
                            ballotStyleId, 
                            curCst, 
                            this.fntCont, 
                            this.fntContSize));
                    contestIds.Add(curCst);
                }

                // increment current index 
                idxBal = idxBal + 1;
                if (idxBal < entrySetBallots.Count)
                {
                    // make sure the ballot index is still the same, otherwise
                    // break the loop and exit 
                    curBal = entrySetBallots.GetValueInt(
                        idxBal, StpBallot.BallotStyleId);
                }
                else
                {
                    // use a negative value to break to do-while loop since
                    // by definition all Ids are greater or equal than 0
                    curBal = -1;
                }
            }
            while (curBal == ballotStyleId);
        }

        /// <summary>
        ///     Loads the parameter definitions of the ballot
        /// </summary>
        /// <param name="entrySetParams">
        ///     The es params.
        /// </param>
        /// <param name="ballotStyleId">
        ///     The ballot style id.
        /// </param>
        /// <externalUnit cref="arrowGapWidth"/>
        /// <externalUnit cref="arrowLayout"/>
        /// <externalUnit cref="BallotEntrySet"/>
        /// <externalUnit cref="ballotGrid"/>
        /// <externalUnit cref="boxPadding"/>
        /// <externalUnit cref="candHeight"/>
        /// <externalUnit cref="columns"/>
        /// <externalUnit cref="columnWidth"/>
        /// <externalUnit cref="currentCardCount"/>
        /// <externalUnit cref="fntCand"/>
        /// <externalUnit cref="fntCandSize"/>
        /// <externalUnit cref="fntCont"/>
        /// <externalUnit cref="fntContSize"/>
        /// <externalUnit cref="fontFolder"/>
        /// <externalUnit cref="fonts"/>
        /// <externalUnit cref="GetBoolParam"/>
        /// <externalUnit cref="GetDblParam"/>
        /// <externalUnit cref="GetFltParam"/>
        /// <externalUnit cref="GetIntParam"/>
        /// <externalUnit cref="GetStrParam"/>
        /// <externalUnit cref="identifier"/>
        /// <externalUnit cref="identifierFace"/>
        /// <externalUnit cref="IdField"/>
        /// <externalUnit cref="Keys"/>
        /// <externalUnit cref="markWidth"/>
        /// <externalUnit cref="pageMargin"/>
        /// <externalUnit cref="PaperSide"/>
        /// <externalUnit cref="pageSize"/>
        /// <externalUnit cref="pageSizeInches"/>
        /// <externalUnit cref="PaperBallotIdentifier"/>
        /// <externalUnit cref="PDFxFont"/>
        /// <externalUnit cref="PointsPerInchD"/>
        /// <externalUnit cref="PointsPerInchF"/>
        /// <externalUnit cref="SizeF"/>
        /// <externalUnit cref="TargetLayout"/>
        /// <revision revisor="dev11" date="12/23/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev16" date="12/30/2008" version="1.0.0.0">
        ///     Modified barcode to include only the language and card 
        ///     identifier
        /// </revision>
        /// <revision revisor="dev06" date="1/10/2009" version="1.0.0.0">
        ///     Added ballot type id back to the identifier and setting the code 
        ///     to the current card count - will need to be revisted when 
        ///     working with the mask
        /// </revision>
        /// <revision revisor="dev11" date="1/18/09" version="1.0.4.19">
        ///     Added arrow gap handling
        /// </revision>
        /// <revision revisor="dev11" date="02/25/2009" version="1.0.8.0801">
        ///     using ballot style id field on the identifier (this replaces the
        ///     ballot type id)
        /// </revision>
        /// <revision revisor="dev11" date="02/27/2009" version="1.0.8.1001">
        ///     Removed check on trailing backslash of font folder
        /// </revision>
        /// <revision revisor="dev11" date="03/11/2009" version="1.0.8.2201">
        ///     ballot style id is now optional on the barcode
        /// </revision>
        /// <revision revisor="dev11" date="03/12/2009" version="1.0.8.2301">
        ///     added support for registration mark quiet zone
        /// </revision>
        /// <revision revisor="dev11" date="03/18/2009" version="1.0.9.0201">
        ///     changed [boxPadding] to [float]
        /// </revision>
        /// <revision revisor="dev11" date="03/21/2009" version="1.0.9.0501">
        ///     added target type support
        /// </revision>
        /// <revision revisor="dev11" date="03/31/2009" version="1.0.9.1501">
        ///     removed preloading of standard fonts
        /// </revision>
        private void LoadDefs(BallotEntrySet entrySetParams, int ballotStyleId)
        {
            this.ballotStyelId = ballotStyleId;
            this.pageSizeInches = new SizeF(
                    this.GetFltParam(entrySetParams, PdfParam.BallotPageWidth), 
                    this.GetFltParam(
                    entrySetParams, PdfParam.BallotPageHeight));
            this.pageSize = new SizeF(
                PointsPerInchF * this.pageSizeInches.Width,
                PointsPerInchF * this.pageSizeInches.Height);

            this.pageMargin = PointsPerInchD
                              *
                              this.GetDblParam(
                                  entrySetParams, PdfParam.BallotPageMargin);
            this.headerHeight = PointsPerInchD
                                *
                                this.GetDblParam(
                                    entrySetParams, PdfParam.CardHeaderHeight);
            this.regMarkQuietZone = PointsPerInchD
                                    *
                                    this.GetDblParam(
                                        entrySetParams,
                                        PdfParam.RegMarkQuietZone);

            this.columns = this.GetIntParam(
                entrySetParams, PdfParam.ColumnCount);

            this.identifier = new PaperBallotIdentifier();

            // set the mask using the entry set. This method will extract 
            // the mask from there
            this.identifier.SetMask(entrySetParams);
            if (this.identifier.IsFieldSelected(IdField.BallotStyleId))
            {
                this.identifier.Set(IdField.BallotStyleId, ballotStyleId);
            }

            if (this.identifier.IsFieldSelected(IdField.Code))
            {
                this.identifier.Set(IdField.Code, this.currentCardCount);
            }

            if (this.identifier.IsFieldSelected(IdField.Language))
            {
                this.identifier.Set(IdField.Language, "EN");
            }

            try
            {
                // since we are parsing a string to get an enum value, if the
                // string is mispelled, no value will be found and an exception
                // thrown. For that reason enclose the statement in a try/catch
                // and assign a value by default in case this doesn't work
                this.identifierFace =
                    (PaperSide) Enum.Parse(
                                    typeof (PaperSide),
                                    this.GetStrParam(
                                        entrySetParams,
                                        PdfParam.IdentifierLocation));
            }
            catch
            {
                // assign a value by default
                this.identifierFace = PaperSide.Front;
            }

            this.fntCand = this.GetStrParam(
                entrySetParams, PdfParam.CandidateFont);
            this.fntCandSize = this.GetDblParam(
                entrySetParams, PdfParam.CandidateFontSize);

            this.fntCont = this.GetStrParam(
                entrySetParams, PdfParam.ContestFont);
            this.fntContSize = this.GetDblParam(
                entrySetParams, PdfParam.ContestFontSize);

            this.candHeight = PointsPerInchD
                              *
                              this.GetDblParam(
                                  entrySetParams, PdfParam.CandidateHeight);
            this.boxPadding = this.GetFltParam(
                entrySetParams, PdfParam.BoxPadding);

            // columnWidth = ([page width] - [page left margin] - [page right 
            // margin]) / [number of columns]
            this.columnWidth =
                (float)
                (this.pageSize.Width - this.pageMargin - this.pageMargin)
                / this.columns;

            this.ballotGrid = this.GetBoolParam(
                entrySetParams, PdfParam.BallotPageGrid);

            this.fontFolder = this.GetStrParam(
                entrySetParams, PdfParam.FontFolder);

            this.arrowGapWidth = PointsPerInchF
                                 *
                                 this.GetFltParam(
                                     entrySetParams, PdfParam.ArrowGap);
            this.markWidth = PointsPerInchD
                             *
                             this.GetDblParam(
                                 entrySetParams, PdfParam.MarkThickness);
            try
            {
                // since we are parsing a string to get an enum value, if the
                // string is mispelled, no value will be found and an exception
                // thrown. For that reason enclose the statement in a try/catch
                // and assign a value by default in case this doesn't work
                this.arrowLayout = (TargetLayout) Enum.Parse(
                                                    typeof(TargetLayout),
                                                    this.GetStrParam(
                                                        entrySetParams,
                                                        PdfParam.TargetLayout));
            }
            catch
            {
                // assign a value by default
                this.arrowLayout = TargetLayout.Right;
            }
        }

        /// <summary>
        ///     Gets a parameter as a string
        /// </summary>
        /// <param name="entrySetParams">
        ///     The entry set of parameters.
        /// </param>
        /// <param name="name">
        ///     The name of the parameter to look for.
        /// </param>
        /// <returns>
        ///     The get str param.
        /// </returns>
        /// <externalUnit cref="BallotEntrySet"/>
        /// <externalUnit cref="StpParam"/>
        /// <revision revisor="dev11" date="12/22/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev11" date="03/24/2009" version="1.0.9.0801">
        ///     modified to handle enum types and ids instead of parameter names
        /// </revision>
        private string GetStrParam(BallotEntrySet entrySetParams, PdfParam name)
        {
            // REVIEW: No magic numbers
            string val = null;
            int idx = entrySetParams.FindIndex(
                StpParam.PDFLayoutParamId, Convert.ToInt32(name));

            // if index is greater than or equals to 0 it means that an entry
            // was actually found. Otherwise, return null
            if (idx >= 0)
            {
                // get the actual value from that parameter
                val = entrySetParams.GetValueStr(idx, StpParam.ParamValue);
            }

            return val;
        }

        /// <summary>
        ///     Gets a parameter value as an integer
        /// </summary>
        /// <param name="entrySetParams">
        ///     The es params.
        /// </param>
        /// <param name="name">
        ///     The param name.
        /// </param>
        /// <returns>
        ///     The get int param.
        /// </returns>
        /// <externalUnit cref="GetStrParam(BallotEntrySet,PdfParam)"/>
        /// <revision revisor="dev11" date="12/23/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        private int GetIntParam(BallotEntrySet entrySetParams, PdfParam name)
        {
            string val = this.GetStrParam(entrySetParams, name);
            return decimal.ToInt32(Convert.ToDecimal(val));
        }

        /// <summary>
        ///     Gets a parameter value as a double
        /// </summary>
        /// <param name="entrySetParams">
        ///     The es params.
        /// </param>
        /// <param name="name">
        ///     The param name.
        /// </param>
        /// <returns>
        ///     The get dbl param.
        /// </returns>
        /// <externalUnit cref="GetStrParam(BallotEntrySet,PdfParam)"/>
        /// <revision revisor="dev11" date="12/23/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        private double GetDblParam(BallotEntrySet entrySetParams, PdfParam name)
        {
            string val = this.GetStrParam(entrySetParams, name);
            return double.Parse(val);
        }

        /// <summary>
        ///     Gets a parameter value as a float
        /// </summary>
        /// <param name="entrySetParams">
        ///     The es params.
        /// </param>
        /// <param name="name">
        ///     The param name.
        /// </param>
        /// <returns>
        ///     The get flt param.
        /// </returns>
        /// <externalUnit cref="GetStrParam(BallotEntrySet,PdfParam)"/>
        /// <revision revisor="dev11" date="12/23/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        private float GetFltParam(BallotEntrySet entrySetParams, PdfParam name)
        {
            string val = this.GetStrParam(entrySetParams, name);
            return float.Parse(val);
        }

        /// <summary>
        ///     Gets a parameter value as a boolean
        /// </summary>
        /// <param name="entrySetParams">
        ///     The es params.
        /// </param>
        /// <param name="name">
        ///     The param name.
        /// </param>
        /// <returns>
        ///     The get bool param.
        /// </returns>
        /// <externalUnit cref="GetStrParam(BallotEntrySet,PdfParam)"/>
        /// <revision revisor="dev11" date="12/23/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        private bool GetBoolParam(BallotEntrySet entrySetParams, PdfParam name)
        {
            string val = this.GetStrParam(entrySetParams, name);
            return bool.Parse(val);
        }

        #endregion

        #region Load (XML)

        /// <summary>
        ///     Gets a font from the fonts dictionary. If the font is not there 
        ///     yet it loads it and adds it to the dictionary
        /// </summary>
        /// <param name="key">
        ///     Either "Helvetica" or a TTF filename located in
        ///     the font folder. The font folder is defined by the [FontFolder]
        ///     parameter on the [PDFLayoutParameter] table on the database
        /// </param>
        /// <returns>
        ///     The font from the fonts dictionary. 
        /// </returns>
        /// <externalUnit cref="DSxInput"/>
        /// <externalUnit cref="fontFolder"/>
        /// <externalUnit cref="fonts"/>
        /// <externalUnit cref="Path"/>
        /// <externalUnit cref="PdfIOException"/>
        /// <externalUnit cref="PDFxFont"/>
        /// <exception cref="PdfIOException">
        ///     Unable to find the specified *.ttf
        ///     file using the path configured in the database 
        ///     ([PDFLayoutParameters] table, [FontFolder] field)
        /// </exception>
        /// <revision revisor="dev11" date="12/5/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev11" date="02/27/2009" version="1.0.8.1001">
        ///     Now to create the full path to the font file, we use 
        ///     <see cref="Path.Combine"/>
        /// </revision>
        /// <revision revisor="dev11" date="03/31/2009" version="1.0.9.1501">
        ///     added dynamic creation of standard fonts and comments
        /// </revision>
        protected PDFxFont GetFont(string key)
        {
            // the font object
            PDFxFont font;

            if (this.fonts.ContainsKey(key) == true)
            {
                // the font has already been used and added to the dictionary
                // so get it from there
                font = this.fonts[key];
            }
            else
            {
                // this is a new font
                try
                {
                    // try first using a standard font
                    // the [key] should be the name of one of the members of
                    // [PdfStandardFont] enum type
                    PdfStandardFont standardFont =
                        (PdfStandardFont)
                        Enum.Parse(typeof (PdfStandardFont), key);

                    // convert the enum to its integer value and use that to
                    // create the standard font
                    font =
                        PDFxFont.createStandardFont(
                            Convert.ToInt32(standardFont));

                    // add it to the dictionary for future use
                    this.fonts.Add(key, font);
                }
                catch
                {
                    // if the standard font didn't work, try creating a true
                    // type font with the given [key]
                    try
                    {
                        // create the font object
                        font =
                            PDFxFont.createTrueTypeFont(
                                new DSxInput(
                                    Path.Combine(this.fontFolder, key)),
                                    true);

                        // add it to the dictionary
                        this.fonts.Add(key, font);
                    }
                    catch (Exception ex)
                    {
                        // nothing worked, so throw an exception to alert the 
                        // user that the font name needs to be corrected
                        throw new PdfIOException(
                            string.Format(
                                "Cannot find "
                                + "specified font file:\n{0}\\{1}",
                                this.fontFolder,
                                key),
                            ex);
                    }
                }
            }

            return font;
        }

        #endregion

        #region Position

        /// <summary>
        ///     Positions all elements of the ballot on the page, preparing 
        ///     everything for either exporting that info or for a drawing 
        ///     everything on the ballot and generating a PDF
        /// </summary>
        /// <externalUnit cref="BackHeaderHeight"/>
        /// <externalUnit cref="BreakType"/>
        /// <externalUnit cref="Candidate"/>
        /// <externalUnit cref="columnWidth"/>
        /// <externalUnit cref="Contest"/>
        /// <externalUnit cref="contests"/>
        /// <externalUnit cref="headerHeight"/>
        /// <externalUnit cref="pageMargin"/>
        /// <externalUnit cref="pageSize"/>
        /// <externalUnit cref="PositionCandidates"/>
        /// <externalUnit cref="PositionTexts"/>
        /// <revision revisor="dev11" date="12/23/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev11" date="01/12/2009" version="1.0.0.0">
        ///     Now sets header margin according to either front or back of a 
        ///     card, not just for the first card
        /// </revision>
        /// <revision revisor="dev11" date="03/09/2009" version="1.0.8.2001">
        ///     method modifiers changed to allow overriding in derived classes
        /// </revision>
        /// <revision revisor="dev11" date="04/11/2009" version="1.0.11.0201">
        ///     Fixed bug that depending on the decimal places of a column width
        ///     occasionally produced an additional column that extends beyond 
        ///     the right side of the paper
        /// </revision>
        protected virtual void Position()
        {
            float x = (float) this.pageMargin, 
                  y = x + (float) this.headerHeight, 
                  gridTop = y;

            // page is 0-based since it is for internal use. Notice that AquaPDF
            // uses 1-based indices for pages
            int page = 0;
            float tempColumnWidth = this.columnWidth;

            foreach (Contest cont in this.contests)
            {
                if (cont.Break == BreakType.Page ||
                    cont.Break == BreakType.PageDouble)
                {
                    // if the contest has a page break, increment the page 
                    // number by 1 and reset x and y coords to the upper 
                    // left corner 
                    page = page + 1;
                    x = (float) this.pageMargin;
                    if ((page % 2) == 0)
                    {
                        // the front of a new card 
                        y = x + (float) this.headerHeight;
                    }
                    else
                    {
                        // [top margin] + [reg mark 0.25"] + [some arbitrary 
                        // margin 0.25"] 
                        y = x + (float) BackHeaderHeight;
                    }

                    gridTop = y;
                }
                else if (cont.Break == BreakType.Column || 
                    cont.Break == BreakType.ColumnDouble)
                {
                    // if the contest has a column break, then determine if it 
                    // is a new column on the same page or the first column of 
                    // an additional page
                    double temp1 = x + ColumnWidthMultiplier * this.columnWidth, 
                        temp2 = this.pageSize.Width - this.pageMargin;
                    if (temp1 >= temp2)
                    {
                        page = page + 1;
                        x = (float) this.pageMargin;
                    }
                    else
                    {
                        x = x + this.columnWidth;
                    }

                    y = Convert.ToSingle(
                        this.pageMargin
                        +
                        ((page % 2 == 0)

                         // the new page is a front so use the front header 
                         // height 
                             ? this.headerHeight

                         // the page now is on the back of a card 
                             : BackHeaderHeight));
                }

                if (cont.Break == BreakType.ColumnDouble ||
                    cont.Break == BreakType.PageDouble ||
                    cont.Break == BreakType.NoneDouble)
                {
                    this.doubleSpan = true;
                    this.columnWidth = 2 * this.columnWidth;
                }
 
                cont.Page = page;
                this.PositionTexts(cont, x, y);
                this.PositionCandidates(cont);

                Candidate lastCand = cont.Candidates[cont.Candidates.Count - 1];
                y = lastCand.Bounds.Bottom;
                x = lastCand.Bounds.Left;
                page = lastCand.Page;
                this.columnWidth = tempColumnWidth;
            }
        }

        /// <summary>
        ///     Positions the texts of a contest using a supplied coordinates 
        ///     for the top left corner of the contest header
        /// </summary>
        /// <param name="contest">
        ///     The contest.
        /// </param>
        /// <param name="x">
        ///     The left side of the contest header
        /// </param>
        /// <param name="y">
        ///     The top side of the contest header
        /// </param>
        /// <externalUnit cref="BackHeaderHeight"/>
        /// <externalUnit cref="ballotGrid"/>
        /// <externalUnit cref="boxPadding"/>
        /// <externalUnit cref="candHeight"/>
        /// <externalUnit cref="columnWidth"/>
        /// <externalUnit cref="FooterHeight"/>
        /// <externalUnit cref="GetFont"/>
        /// <externalUnit cref="headerHeight"/>
        /// <externalUnit cref="MoveContestTo"/>
        /// <externalUnit cref="OfficeText"/>
        /// <externalUnit cref="pageMargin"/>
        /// <externalUnit cref="pageSize"/>
        /// <externalUnit cref="PDFxFont"/>
        /// <externalUnit cref="PointF"/>
        /// <externalUnit cref="RectangleF"/>
        /// <revision revisor="dev11" date="12/23/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev11" date="01/18/2009" version="1.0.4.19">
        ///     Added detection of contests outside the alloed are of the page,
        ///     now contests are moved to either a new column or a new page
        /// </revision>
        /// <revision revisor="dev11" date="01/20/2009" version="1.0.5.1">
        ///     Full support for [TargetLayout] parameter
        /// </revision>
        /// <revision revisor="dev11" date="03/09/2009" version="1.0.8.2001">
        ///     method modifiers changed to allow overriding in derived classes
        /// </revision>
        /// <revision revisor="dev11" date="03/18/2009" version="1.0.9.0201">
        ///         now using [boxPadding]
        /// </revision>
        protected virtual void PositionTexts(Contest contest, float x, float y)
        {
            List<OfficeText> texts = contest.Texts;

            float padding = this.boxPadding,
 
                  // inches
                  curY = y + padding, 
                  cx = x + this.columnWidth / 2F, 
                  height, 
                  offset;

            double newX, newY;
            int newPage = contest.Page;

            for (int i = 0; i < texts.Count; i = i + 1)
            {
                OfficeText text = texts[i];
                PDFxFont font = this.GetFont(text.Font);
                double yMax = font.getYMax(text.FontSize), 
                       yMin = font.getYMin(text.FontSize); // negative value

                PointF point = new PointF();
                point.X = cx; // relative to the page
                point.Y = (float)(curY + yMax); // relative to the page
                text.XYText = point;

                curY = curY + (float)(yMax - yMin);
            }

            curY = curY + padding;
            RectangleF rect = new RectangleF();
            rect.X = x;
            rect.Y = y;
            rect.Width = this.columnWidth;

            // make the height a multiple of candidate height in order to have
            // a perfect grid on the page 
            height = curY - y;
            if (this.ballotGrid == true)
            {
                height =
                    (float)
                    (Math.Ceiling(height / this.candHeight) * this.candHeight);

                // center texts vertically by offsetting them half the 
                // additional space added to the header height 
                offset = (height - (curY - y)) / 2F;
                for (int i = 0; i < texts.Count; i = i + 1)
                {
                    OfficeText text = texts[i];
                    PointF point = text.XYText;
                    point.Y = point.Y + offset;
                    text.XYText = point;
                }
            }

            rect.Height = height;
            contest.Bounds = rect;

            // make sure the contest is within the target area on the page and
            // if not, move it to the next column or if needed, to the next page 
            if (rect.Bottom > (this.pageSize.Height - this.pageMargin 
                               - RegistrationMarkSide - this.regMarkQuietZone))
            {
                // the contest needs to be moved to the next column 
                if (rect.Right >= this.pageSize.Width - this.pageMargin)
                {
                    // since there is no space for a next column, move the 
                    // contest to the next page
                    newPage = contest.Page + 1;
                    newX = Convert.ToSingle(this.pageMargin);
                }
                else
                {
                    // move the contest to the next column 
                    newX = rect.Right;
                }

                newY = Convert.ToSingle(
                    this.pageMargin
                    +
                    ((newPage % 2 == 0)

                     // the new page is a front so use the front header height 
                         ? this.headerHeight

                     // the page now is on the back of a card 
                         : BackHeaderHeight));

                MoveContestTo(contest, newX, newY, newPage);
            }
        }

        /// <summary>
        ///     Positions the candidates of a given contest on the ballot
        /// </summary>
        /// <param name="contest">
        ///     The contest.
        /// </param>
        /// <externalUnit cref="arrowGapWidth"/>
        /// <externalUnit cref="arrowLayout"/>
        /// <externalUnit cref="BackHeaderHeight"/>
        /// <externalUnit cref="boxPadding"/>
        /// <externalUnit cref="Candidate"/>
        /// <externalUnit cref="columnWidth"/>
        /// <externalUnit cref="Contest"/>
        /// <externalUnit cref="contests"/>
        /// <externalUnit cref="FooterHeight"/>
        /// <externalUnit cref="headerHeight"/>
        /// <externalUnit cref="pageMargin"/>
        /// <externalUnit cref="pageSize"/>
        /// <externalUnit cref="PointF"/>
        /// <externalUnit cref="RectangleF"/>
        /// <externalUnit cref="TargetLayout"/>
        /// <revision revisor="dev11" date="12/24/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev11" date="01/12/2009" version="1.0.0.0">
        ///     Now sets top of column according to either front or back of a 
        ///     card, not just for the first card
        /// </revision>
        /// <revision revisor="dev11" date="03/09/2009" version="1.0.8.2001">
        ///     method modifiers changed to allow overriding in derived classes
        /// </revision>
        /// <revision revisor="dev11" date="03/18/2009" version="1.0.9.0201">
        ///     now uses [boxPadding]
        /// </revision>
        protected virtual void PositionCandidates(Contest contest)
        {
            // REVIEW: ditto
            float x = contest.Bounds.Left, 
                  y = contest.Bounds.Bottom;
            List<Candidate> candidates = contest.Candidates;
            int page = contest.Page;
            for (int i = 0; i < candidates.Count; i = i + 1)
            {
                Candidate cand = candidates[i];

                RectangleF rect = new RectangleF();
                rect.X = x;
                rect.Y = y;
                rect.Width = this.columnWidth;
                rect.Height = (float) this.candHeight;

                if (rect.Bottom > (this.pageSize.Height - this.pageMargin 
                               - RegistrationMarkSide - this.regMarkQuietZone))
                {
                    // the candidate box goes beyond the allowed area and it
                    // needs to be moved to either a new column or a new page 
                    if (rect.Right >= this.pageSize.Width - this.pageMargin)
                    {
                        // a new column does not fit either so move the 
                        // candidate to a new page 
                        page = page + 1;
                        x = Convert.ToSingle(this.pageMargin);
                        y = Convert.ToSingle(this.pageMargin +
                                             ((page % 2 == 0)
                                                  ? this.headerHeight
                                                  : BackHeaderHeight));
                    }
                    else
                    {
                        x = rect.X + this.columnWidth;
                        if ((page % 2) == 0)
                        {
                            // the first contest already considers the front
                            // page header, so it's easier to get the Y from
                            // there 
                            y = this.contests[0].Bounds.Top;
                        }
                        else
                        {
                            // the back page only has the registration marks 
                            y = 18 + (float) BackHeaderHeight;
                        }
                    }

                    rect.X = x;
                    rect.Y = y;
                }

                y = y + (float) this.candHeight;

                cand.Bounds = rect;
                cand.Page = page;

                // a padding for the arrow 
                float padding = this.boxPadding,

                      // dx = right - ([padding] + [arrow length], all in 
                      // points) 
                      dx,

                      // dy = top + ([box height] - [arrow height in 
                      // points]) / 2
                      dy;
                switch (this.target.Type)
                {
                    case TargetType.Oval:
                        dx = rect.Right - PointsPerInchF * (padding
                                                            +
                                                            this.target.
                                                                GetFltParam(
                                                                TargetParam.
                                                                    Width));
                        dy = rect.Top
                             + (rect.Height - PointsPerInchF *
                                              this.target.GetFltParam(
                                                  TargetParam.Height)) / 2f;
                        break;
                    default:
                        dx = rect.Right - PointsPerInchF *
                                          (padding
                                           + Convert.ToSingle(ArrowLength))
                             - this.arrowGapWidth;
                        dy = rect.Top
                             + (rect.Height - PointsPerInchF * 0.16f) / 2f;
                        break;
                }

                if (this.arrowLayout == TargetLayout.Left)
                {
                    dx = rect.Left + PointsPerInchF * padding;
                }

                dx = Convert.ToSingle(Math.Round(dx, Candidate.Precision));
                dy = Convert.ToSingle(Math.Round(dy, Candidate.Precision));
                cand.XYArrow = new PointF(dx, dy);
            }
        }

        /// <summary>
        ///     Moves the contest to a specified coordinates and page
        /// </summary>
        /// <param name="contest">
        ///     The contest.
        /// </param>
        /// <param name="x">
        ///     The x coordinate, relative to the left border of the
        ///     page, in points.
        /// </param>
        /// <param name="y">
        ///     The y coordinate, relative to the top border of the
        ///     page, in points.
        /// </param>
        /// <param name="page">
        /// T   he page number.
        /// </param>
        /// <externalUnit cref="Contest"/>
        /// <externalUnit cref="OfficeText"/>
        /// <externalUnit cref="PointF"/>
        /// <externalUnit cref="RectangleF"/>
        /// <revision revisor="dev11" date="1/18/2009" version="1.0.4.19">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev11" date="03/09/2009" version="1.0.8.2001">
        ///     method modifers changed
        /// </revision>
        private static void MoveContestTo(
            Contest contest, double x, double y, int page)
        {
            RectangleF bounds = contest.Bounds;
            float deltaX = Convert.ToSingle(x) - bounds.X, 
                  deltaY = Convert.ToSingle(y) - bounds.Y;

            foreach (OfficeText text in contest.Texts)
            {
                PointF xy = text.XYText;
                xy.X = xy.X + deltaX;
                xy.Y = xy.Y + deltaY;
                text.XYText = xy;
            }

            bounds.X = bounds.X + deltaX;
            bounds.Y = bounds.Y + deltaY;

            contest.Bounds = bounds;
            contest.Page = page;
        }

        #endregion

        #region Draw

        /// <summary>
        ///     Draws the contest header on a PDF page
        /// </summary>
        /// <param name="cont">
        ///     The contest.
        /// </param>
        /// <externalUnit cref="bottomY"/>
        /// <externalUnit cref="GetFont"/>
        /// <externalUnit cref="java.awt.Color"/>
        /// <externalUnit cref="OfficeText"/>
        /// <externalUnit cref="pdfContext"/>
        /// <externalUnit cref="pdfPage"/>
        /// <externalUnit cref="PDFxPanel"/>
        /// <externalUnit cref="RectangleF"/>
        /// <externalUnit cref="rightX"/>
        /// <externalUnit cref="SetCurrentPage(Contest)"/>
        /// <revision revisor="dev11" date="12/24/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev11" date="01/13/2009" version="1.0.0.0">
        ///     Now updates [bottomY] after drawing the contest border
        /// </revision>
        /// <revision revisor="dev11" date="01/14/2009" version="1.0.4.14">
        ///     Now updates [rightX] after drawing the contest border
        /// </revision>
        /// <revision revisor="dev11" date="03/09/2009" version="1.0.8.2001">
        ///     method modifiers changed to allow overriding in derived classes
        /// </revision>
        protected virtual void DrawContest(Contest cont)
        {
            this.SetCurrentPage(cont);
            RectangleF rect = cont.Bounds;

            this.pdfContext.setBgColor(JColor.LIGHT_GRAY);
            this.pdfPage.drawRect(
                rect.Left, rect.Top, rect.Width, rect.Height, true, true);

            string allcontest = string.Empty;
            foreach (OfficeText text in cont.Texts)
            {
                allcontest += " " + text.Text;
                this.pdfContext.setFont(this.GetFont(text.Font), text.FontSize);
                this.pdfPage.drawText(
                    text.XYText.X, 
                    text.XYText.Y, 
                    text.Text, 
                    PDFxPanel.__Fields.ALIGN_CENTER);
            }

            // update bottomY 
            this.bottomY = Math.Max(this.bottomY, rect.Bottom);
            this.rightX = Math.Max(this.rightX, rect.Right);
        }

        /// <summary>
        ///     Draws the candidate on a PDF page. Includes box border, 
        ///     candidate name, arrow. Finally it updates [bottomY] and [rightX] 
        ///     that are internal variables to determine the extent of the 
        ///     candidates' area
        /// </summary>
        /// <param name="cand">
        ///     The candidate.
        /// </param>
        /// <externalUnit cref="arrowGapWidth"/>
        /// <externalUnit cref="arrowLayout"/>
        /// <externalUnit cref="bottomY"/>
        /// <externalUnit cref="boxPadding"/>
        /// <externalUnit cref="Candidate"/>
        /// <externalUnit cref="DrawArrow"/>
        /// <externalUnit cref="fntCand"/>
        /// <externalUnit cref="fntCandSize"/>
        /// <externalUnit cref="GetFont"/>
        /// <externalUnit cref="pdfContext"/>
        /// <externalUnit cref="pdfPage"/>
        /// <externalUnit cref="PDFxFont"/>
        /// <externalUnit cref="RectangleF"/>
        /// <externalUnit cref="rightX"/>
        /// <externalUnit cref="SetCurrentPage(Candidate)"/>
        /// <externalUnit cref="TargetLayout"/>
        /// <revision revisor="dev11" date="12/24/2008" version="1.0.0.0">
        ///     Member Created</revision>
        /// <revision revisor="dev11" date="01/13/2009" version="1.0.0.0">
        ///     Now updates [bottomY] after drawing the candidate border
        /// </revision>
        /// <revision revisor="dev11" date="01/14/2009" version="1.0.4.14">
        ///     Now updates [rightX] after drawing the candidate border
        /// </revision>
        /// <revision revisor="dev11" date="01/19/2009" version="1.0.4.20">
        ///     Since the gap width is no longer fixed, the candidate box is now
        ///     drawn based on that value
        /// </revision>
        /// <revision revisor="dev11" date="01/20/2009" version="1.0.5.1">
        ///     Full support for [TargetLayout] parameter
        /// </revision>
        /// <revision revisor="dev11" date="03/09/2009" version="1.0.8.2001">
        ///     method modifiers changed to allow overriding in derived classes
        /// </revision>
        /// <revision revisor="dev11" date="03/18/2009" version="1.0.9.0201">
        ///     now using [boxPadding] and constants
        /// </revision>
        /// <revision revisor="dev11" date="08/20/2009" version="1.0.15.1401">
        ///     No candidate name is printed for write-in candidates
        /// </revision>
        protected virtual void DrawCandidate(Candidate cand)
        {
            SetCurrentPage(cand);
            RectangleF rect = cand.Bounds;

            // right = [right side of the entire candidate box] - [0.25" 
            // which is the length of the arrow + 0.05" which is some margin] - 
            // [the arrow gap width]
            double arrowBox = PointsPerInchD * 0.3 + this.arrowGapWidth, right;
            if (this.target.Type == TargetType.Oval)
            {
                // get instead the space needed for the oval
                arrowBox = PointsPerInchD
                           * (0.05 + this.target.GetDblParam(
                           TargetParam.Width));
            }

            // get the right side of the lines
            right = rect.Right - arrowBox - this.boxPadding * PointsPerInchD;
            double[] x = new[] {right, rect.Left, rect.Left, right},
                     y = new double[]
                             {rect.Top, rect.Top, rect.Bottom, rect.Bottom};
            if (this.arrowLayout == TargetLayout.Left)
            {
                x[0] = rect.Left + arrowBox;
                x[1] = rect.Right;
                x[2] = rect.Right;
                x[3] = x[0];
            }

            this.pdfPage.drawPolyline(x, y);

            PDFxFont font = this.GetFont(this.fntCand);
            this.pdfContext.setFont(font, this.fntCandSize);

            // the line height is a fixed value a font has that is defined by 
            // the font designer at design time that is supposed to be ok for
            // all glyphs in the font, including some that very unlikely may be
            // used. For that reason, this value is usually inappropriate (again
            // it is different for every font, but somewhat similar for fonts
            // used by this program, i.e. for voting ballots) and a lower value
            // is more suitable. Around 3 quarters of that value yields a much
            // better result when trying to vertically center texts
            double lineHgt = font.getHeight(this.fntCandSize) * 0.7, 

                   // the padding is simply the remaining space divided by 2 
                   vertPad = (rect.Height - lineHgt) / 2D, 
                   leftPad = PointsPerInchD * this.boxPadding,

                   // and that's how the baseline of the text is obtained 
                   lineY = rect.Bottom - vertPad;
            if (this.arrowLayout == TargetLayout.Left)
            {
                switch (this.target.Type)
                {
                    case TargetType.Oval:
                        leftPad = leftPad
                                  +
                                  PointsPerInchD
                                  * this.target.GetDblParam(TargetParam.Width)
                                  + PointsPerInchD * this.boxPadding;
                        break;
                    default:

                        // add the arrow space as well as it is on the left side of the 
                        // box 
                        leftPad = leftPad + PointsPerInchD * ArrowLength
                                  + this.arrowGapWidth
                                  + PointsPerInchD * this.boxPadding;
                        break;
                }
            }

            if (cand.Type != CandidateType.WriteIn)
            {
                this.pdfPage.drawText(rect.Left + leftPad, lineY, cand.Name);
            }

            this.DrawTarget(cand);

            // update bottomY 
            this.bottomY = Math.Max(this.bottomY, rect.Bottom);
            this.rightX = Math.Max(this.rightX, rect.Right);
        }

        /// <summary>
        ///     Draws the target.
        /// </summary>
        /// <param name="candidate">
        ///     The candidate.
        /// </param>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="4/8/2009" version="1.0.10.0501">
        ///     Comments added
        /// </revision>
        protected virtual void DrawTarget(Candidate candidate)
        {
            switch (this.target.Type)
            {
                case TargetType.Oval:
                    this.DrawOval(candidate);
                    break;
                default: // case TargetType.Arrow
                    this.DrawArrow(candidate);
                    break;
            }
        }

        /// <summary>
        ///     Draws the arrow.
        /// </summary>
        /// <param name="candidate">
        ///     The candidate.
        /// </param>
        /// <externalUnit cref="ArrHeadX"/>
        /// <externalUnit cref="ArrHeadY"/>
        /// <externalUnit cref="ArrMarkY"/>
        /// <externalUnit cref="ArrTailX"/>
        /// <externalUnit cref="ArrTailY"/>
        /// <externalUnit cref="arrowGapWidth"/>
        /// <externalUnit cref="arrowLayout"/>
        /// <externalUnit cref="Candidate"/>
        /// <externalUnit cref="java.awt.Color"/>
        /// <externalUnit cref="markWidth"/>
        /// <externalUnit cref="pdfContext"/>
        /// <externalUnit cref="pdfPage"/>
        /// <externalUnit cref="RectangleF"/>
        /// <externalUnit cref="TargetLayout"/>
        /// <externalUnit cref="ToPoints"/>
        /// <revision revisor="dev11" date="12/4/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev11" date="1/19/09" version="1.0.4.20">
        ///     Mark adjusted to support [MarkWidth] and [ArrowGap] parameters
        /// </revision>
        /// <revision revisor="dev11" date="1/20/2009" version="1.0.5.1">
        ///     Fixed premarked arrows to properly support [Targetlayout]
        /// </revision>
        /// <revision revisor="dev11" date="03/09/2009" version="1.0.8.2001">
        ///     method modifiers changed to allow overriding in derived classes
        /// </revision>
        /// <revision revisor="dev11" date="07/13/2009" version="1.0.14.0601">
        ///     fixed mark density color on arrow targets
        /// </revision>
        protected virtual void DrawArrow(Candidate candidate)
        {
            this.pdfContext.setBgColor(JColor.BLACK);
            JColor fgcolor = this.pdfContext.getFgColor();

            RectangleF bounds = candidate.Bounds;
            bool flip = this.arrowLayout == TargetLayout.Left;
            double dx = candidate.XYArrow.X
                        + ((flip == true) ? this.arrowGapWidth : 0),
                   dx2 = candidate.XYArrow.X
                         + ((flip == true) ? 0 : this.arrowGapWidth),
                   dy = candidate.XYArrow.Y,
                   lineWidth = this.pdfContext.getLineWidth();

            this.pdfPage.drawPolygon(
                this.ToPoints(ArrHeadX, dx, flip), 
                this.ToPoints(ArrHeadY, dy, false), 
                true, 
                false);
            this.pdfPage.drawPolygon(
                this.ToPoints(ArrTailX, dx2, flip), 
                this.ToPoints(ArrTailY, dy, false), 
                true, 
                false);

            if (candidate.Voted == true)
            {
                this.pdfContext.setFgColor(this.target.MarkDensityColor);

                // set the line width to the specified value 
                this.pdfContext.setLineWidth(this.markWidth);

                // x0 = [the right side of the head, in points] + [the
                // offset set by the candidate position] 
                // y (it's the same for both ends of the mark since the arrow is
                // horizontal) = [the middle of the arrow in points] + [the
                // offset set by the candidate position]
                // x1 = [x0] + [arrow gap width]
                double x0 = 72D
                            *
                            candidate.GetXYTarget(
                                this.arrowLayout, this.target.Type).X,
                       x1 = x0 + this.arrowGapWidth,
                       y = (72D * ArrMarkY) + dy;
                this.pdfPage.drawLine(x0, y, x1, y);

                // restore the line width as it was 
                this.pdfContext.setLineWidth(lineWidth);
                this.pdfContext.setFgColor(fgcolor);
            }
        }

        /// <summary>
        ///     Draws the oval.
        /// </summary>
        /// <param name="candidate">
        ///     The candidate.
        /// </param>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="3/21/2009" version="1.0.9.0501">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev11" date="04/01/2009" version="1.0.9.1601">
        ///     now the entire oval's line is within the specified width and 
        ///     height. Previously, half the line was inside and half was 
        ///     outside
        /// </revision>
        protected virtual void DrawOval(Candidate candidate)
        {
            JColor fgColor = this.pdfContext.getFgColor();
            double lineWidth = this.pdfContext.getLineWidth();

            // set oval values
            this.pdfContext.setFgColor(this.target.LineDensityColor);
            this.pdfContext.setBgColor(JColor.BLACK);
            this.pdfContext.setLineWidth(
                PointsPerInchD *
                this.target.GetDblParam(TargetParam.LineWidth));

            double x = candidate.XYArrow.X, 
                   y = candidate.XYArrow.Y, 
                   width = PointsPerInchD
                           *
                           (this.target.GetDblParam(TargetParam.Width)
                            - this.target.GetDblParam(TargetParam.LineWidth)), 
                   height = PointsPerInchD
                           *
                           (this.target.GetDblParam(TargetParam.Height)
                            - this.target.GetDblParam(TargetParam.LineWidth));

            // draw the oval
            this.pdfPage.drawOval(
                x, y, 
                width, 
                height, 
                false, true);
            if (candidate.Voted)
            {
                // use the target to make a mark. the target object knows what
                // target type and target mark is selected
                this.target.DrawOvalMark(
                    this.pdfPage,
                    this.pdfContext,
                    x,
                    y,
                    width,
                    height,
                    this.markWidth);
            }

            // restore values
            this.pdfContext.setFgColor(fgColor);
            this.pdfContext.setLineWidth(lineWidth);
        }

        /// <summary>
        ///     Draws the ballot id as a PDF417 barcode on the current (front) 
        ///     page of a ballot card
        /// </summary>
        /// <param name="cardNumber">
        /// The card number.
        /// </param>
        /// <externalUnit cref="ASCIIEncoding"/>
        /// <externalUnit cref="GetFont"/>
        /// <externalUnit cref="identifier"/>
        /// <externalUnit cref="IdField"/>
        /// <externalUnit cref="pageMargin"/>
        /// <externalUnit cref="pageSize"/>
        /// <externalUnit cref="pdfContext"/>
        /// <externalUnit cref="pdfPage"/>
        /// <externalUnit cref="PDFxDimension"/>
        /// <externalUnit cref="PDFxFont"/>
        /// <externalUnit cref="PDFxPanel"/>
        /// <revision revisor="dev11" date="12/24/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev11" date="01/20/2009" version="1.0.5.1">
        ///     Updated to print the encoded data as well. The barcode has now 4
        ///     data columns
        /// </revision>
        /// <revision revisor="dev11" date="03/09/2009" version="1.0.8.2001">
        ///     method modifiers changed to allow overriding in derived classes
        /// </revision>
        /// <revision revisor="dev11" date="04/02/2009" version="1.0.9.1701">
        ///     distance from right border of the page changed to a constant
        /// </revision>
        protected virtual void DrawBallotId(int cardNumber)
        {
            // REVIEW: magic numbers and comments; split declarations

            // arbitrarily put the left side of the barcode 3" from the right 
            // side of the page 
            double x = this.pageSize.Width - BallotIdLeftFromPageRight, 
                   y = this.pageMargin;

            this.identifier.Set(IdField.Code, cardNumber);
            string data = this.identifier.ToString();
            byte[] bytes =
                ASCIIEncoding.ASCII.GetBytes(data);

            // get the size of the barcode 
            PDFxDimension size = this.pdfPage.measureBarCodePDF417(
                bytes, 4, 3, 3);
            PDFxFont oldFont = this.pdfContext.getFont(),
                     font = this.GetFont(this.fntCont);
            double oldSize = this.pdfContext.getFontPointSize(), 
                   fntSize = 7, 
                   textWidth, 
                   barWidth = size.getWidth();

            // print the barcode 
            this.pdfPage.drawBarCodePDF417(

                    // x,y = upper-left ocrner of the bar code (in points)
                    // bytes = bytes to encode
                    // data columns, bar height (in points), error level
                    x, y, bytes, 4, 3, 3);

            this.pdfContext.setFont(font, fntSize);
            textWidth = this.pdfPage.getTextWidth(data);
            if (textWidth > barWidth)
            {
                fntSize = fntSize * barWidth / textWidth;
                this.pdfContext.setFontPointSize(fntSize);
            }

            this.pdfPage.drawText(
                x + barWidth / 2D, 
                y + size.getHeight() + font.getYMax(fntSize), 
                data, 
                PDFxPanel.__Fields.ALIGN_CENTER);
            this.pdfContext.setFont(oldFont, oldSize);
        }

        /// <summary>
        ///     Draws the instructions on the current (front) page of a ballot 
        ///     card
        /// </summary>
        /// <externalUnit cref="fntCont"/>
        /// <externalUnit cref="fntContSize"/>
        /// <externalUnit cref="GetFont"/>
        /// <externalUnit cref="headerHeight"/>
        /// <externalUnit cref="pageMargin"/>
        /// <externalUnit cref="pageSize"/>
        /// <externalUnit cref="pdfContext"/>
        /// <externalUnit cref="pdfPage"/>
        /// <externalUnit cref="PDFxFont"/>
        /// <revision revisor="dev11" date="12/24/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev11" date="01/12/2009" version="1.0.0.0">
        ///     Setting the proper font
        /// </revision>
        /// <revision revisor="dev11" date="01/20/2009" version="1.0.5.1">
        ///     Setting the proper font size
        /// </revision>
        /// <revision revisor="dev11" date="03/09/2009" version="1.0.8.2001">
        ///     method modifiers changed to allow overriding in derived classes
        /// </revision>
        /// <revision revisor="dev11" date="04/10/2009" version="1.0.11.0101">
        ///     added support for templates
        /// </revision>
        /// <revision revisor="dev11" date="04/15/2009" version="1.0.11.0501">
        ///     added support for parameters on templates
        /// </revision>
        /// <revision revisor="dev11" date="04/16/2009" version="1.0.11.0601">
        ///     fixed vertical positioning of text
        /// </revision>
        protected virtual void DrawInstructions()
        {
            double x1 = 0.75 * 72D, x2 = this.pageSize.Width - x1;
            if ((this.Template != null) && (this.Template.IsValidXml == true))
            {
                string templateName = string.Format(
                    "Instructions{0}",
                    this.IsAudioBallot
                        ? "ControlBallot"
                        : this.target.Type.ToString());
                PDFxFont font = this.GetFont(this.fntCont);
                this.pdfContext.setFont(font, this.fntContSize);
                TXMLxEngine txmlEngine = this.pdfDoc.getTXMLEngine();
                TXMLxAttributes txmlAttributes =
                    this.GetTXMLAttributes(txmlEngine);
                TXMLxData txmlData = txmlEngine.createTXMLData(
                        this.Template.Txml, txmlAttributes);

                // measure the text vertically
                TXMLxTextAreaFeedback txmlFeedback =
                    this.pdfPage.measureTXMLTextArea(
                        x1,
                        x2,
                        0,
                        txmlData,
                        templateName,
                        0,
                        this.pageSize.Height);

                // y2 is the distance between the base of the first line and
                // the base of the last line. So the height of the first line is
                // actually not included, but it's not a problem since the base
                // of the first line is the reference point to print the text
                double y2 = txmlFeedback.getY2(),
                       y = this.pageMargin + this.headerHeight - y2

                           // ymin is a negative value, so use a '+' to actually
                           // subtract as well
                           + font.getYMin(this.fntContSize)
                           - InstructionsBottomMargin;

                this.pdfPage.drawTXMLTextArea(
                    x1, x2, y, txmlData, templateName);
            }
        }

        /// <summary>
        ///     Draws the border around all candidates on the current page. This
        ///     method should be called after ALL candidates and contest headers 
        ///     have been drawn on the current page since [bottomY] needs to be 
        ///     updated for that first.
        /// </summary>
        /// <externalUnit cref="BackHeaderHeight"/>
        /// <externalUnit cref="bottomY"/>
        /// <externalUnit cref="columnWidth"/>
        /// <externalUnit cref="headerHeight"/>
        /// <externalUnit cref="pageMargin"/>
        /// <externalUnit cref="pageSize"/>
        /// <externalUnit cref="pdfPage"/>
        /// <externalUnit cref="rightX"/>
        /// <revision revisor="dev11" date="1/13/2009" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev11" date="1/14/2009" version="1.0.4.14">
        ///     Replaced entire frame by column frames
        /// </revision>
        /// <revision revisor="dev11" date="03/24/2009" version="1.0.9.0801">
        ///     bug fixed that depending on the decimal places of a column width
        ///     occasionally produced an additional column that extends beyond 
        ///     the right side of the paper
        /// </revision>
        protected virtual void DrawFrame()
        {
            // getPageNumber() method returns a 1-based page number
            // by checking for even-odd numbers, we can easily determine if
            // the page number refers to a front or a back
            // i.e. since the number returned is 1-based, diving by 2 and 
            // getting a remainder of 1 means that the number is odd and 
            // therefore it is a front page
            bool front = (this.pdfPage.getPageNumber() % 2) == 1;
            double left =
                this.pageMargin,
                   top,
                   width = Convert.ToDouble(this.columnWidth),
                   height,
                   rightMax = Convert.ToDouble(this.pageSize.Width)
                              - this.pageMargin;

            if (front == true)
            {
                top = this.pageMargin + this.headerHeight;
            }
            else
            {
                top = this.pageMargin + BackHeaderHeight;
            }

            // height of the rectangle 
            height = this.bottomY - top;
            while ((left < this.rightX) && (left + width <= rightMax))
            {
                if ((this.doubleSpan) && (!front && left > this.pageMargin))
                {
                    width = width * 2;
                }

                // draw a rectangle (no fill, stroke only) 
                this.pdfPage.drawRect(left, top, width, height, false, true);
                
                left = left + width;
            }

            if (left < rightMax)
            {
                // if not all columns have candidates, draw a border for the 
                // remaining area, even if it's larger than a single column
                this.pdfPage.drawRect(
                    left, top, rightMax - left, height, false, true);
            }
        }

        /// <summary>
        ///     Draws the test artifacts.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="3/31/2009" version="1.0.9.1501">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev11" date="04/14/2009" version="1.0.11.0401">
        ///     number printed on ballot is now 1-based. Internally, the test 
        ///     deck index is still 0-based
        /// </revision>
        /// <revision revisor="dev11" date="04/20/2009" version="1.0.11.0801">
        ///     added test deck name
        /// </revision>
        /// <revision revisor="dev11" date="04/27/2009" version="1.0.11.1501">
        ///     added a limit to the test deck name. This limit depends on page 
        ///     width, reg mark quiet zone, etc.
        /// </revision>
        protected virtual void DrawTestArtifacts()
        {
            // find the middle point of the page by diving the width by 2
            double x = this.pageSize.Width / 2D,

                   // use the bottom of the page, between the registration marks
                   // of the bottom, so move away from the page border using the
                   // page margin
                   y = this.pageSize.Height - this.pageMargin,
                   availWidth = this.pageSize.Width - 2 * RegistrationMarkMargin
                                - 2 * RegistrationMarkSide
                                - 2 * this.regMarkQuietZone,
                   actualWidth;
            int number = 1 + this.testDeckIndex.Value, 
                length;
            string text =

                // convert the test deck index to a 4-digit 0-left-padded string
                string.Format(
                    TestDeckNamePattern,
                    string.IsNullOrEmpty(this.testDeckName)
                        ? string.Empty
                        : this.testDeckName,
                    string.IsNullOrEmpty(this.testDeckName) ? 
                    string.Empty : " ",
                    number);

            // get the font object for Courier Bold
            PDFxFont font = 
                this.GetFont(PdfStandardFont.CourierBold.ToString());

            // set the font and font size
            this.pdfContext.setFont(font, PaperBallot.TestDeckIndexFontSize);

            // get the actual width of the text using the current font settings
            actualWidth = this.pdfPage.getTextWidth(text);
            if (actualWidth > availWidth)
            {
                // since this text is printed using Courier font, which is a 
                // monospaced font, each character has the same width, and 
                // therefore it's easy to know where to cut off the test deck 
                // name. the pattern concatenates the test deck name and 5 more 
                // characters so subtract 5 to the length, and 3 more for the 
                // ellipsis dots
                length =
                    Convert.ToInt32(
                        Math.Floor((availWidth / actualWidth) * text.Length))
                    - 5 - 3;
                this.testDeckName = this.testDeckName.Substring(
                    0, length) + "...";
                text = string.Format(
                    TestDeckNamePattern, this.testDeckName, " ", number);
            }

            // draw the test deck index
            this.pdfPage.drawText(

                // coordinates of the center of the baseline of the text
                x,
                y,
                text,

                // center text horizontally
                PDFxPanel.__Fields.ALIGN_CENTER);
        }

        /// <summary>
        ///     Draws the control artifacts.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="11/2/2009" version="1.1.2.1101">
        ///     Member Created
        /// </revision>
        protected virtual void DrawControlArtifacts()
        {
            // set foreground color
            PDFxFont font =
                this.GetFont(PdfStandardFont.HelveticaBold.ToString());

            this.SetCurrentPage(0);
 
            double x = this.pdfPage.getWidth() / 2.0,
                   y = this.pageMargin + this.headerHeight
                       +
                       (this.pdfPage.getHeight() - this.headerHeight
                        - 2 * this.pageMargin) / 2;

            this.pdfContext.setFont(font, WatermarkFontSize);
            this.pdfContext.setFgColor(JColor.BLACK);
            this.pdfPage.drawText(
                x,
                y,
                Properties.Resources.AudioControlBallotText,
                PDFxPanel.__Fields.ALIGN_CENTER);
        }

        /// <summary>
        ///     Converts all numbers in array to points.
        /// </summary>
        /// <param name="array">
        ///     The array of coordinates (in inches).
        /// </param>
        /// <param name="dxPts">
        ///     an offset that is applied to all values (in points)
        /// </param>
        /// <param name="flip">
        ///     if set to <c>true</c> assumes the array of
        ///     coordinates is flipped horizontally.
        /// </param>
        /// <returns>
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="12/4/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev11" date="1/19/2009" version="1.0.4.20">
        ///     Added parameter to flip the arrow horizontally
        /// </revision>
        /// <revision revisor="dev11" date="03/09/2009" version="1.0.8.2001">
        ///     method modifiers changed to allow usage on derived classes
        /// </revision>
        protected double[] ToPoints(double[] array, double dxPts, bool flip)
        {
            // REVIEW: magic numbers and comments;
            double[] ptArr = new double[array.Length];
            if (flip == true)
            {
                for (int i = 0; i < array.Length; i = i + 1)
                {
                    ptArr[i] = (72D * (0.25 - array[i])) + dxPts;
                }
            }
            else
            {
                for (int i = 0; i < array.Length; i = i + 1)
                {
                    ptArr[i] = (72D * array[i]) + dxPts;
                }
            }

            return ptArr;
        }

        /// <summary>
        ///     Draws the candidates
        /// </summary>
        /// <param name="cont">
        ///     The contest.
        /// </param>
        /// <externalUnit cref="Candidate"/>
        /// <externalUnit cref="Contest"/>
        /// <externalUnit cref="DrawCandidate"/>
        /// <revision revisor="dev11" date="12/24/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        private void DrawCandidates(Contest cont)
        {
            foreach (Candidate candidate in cont.Candidates)
            {
                this.DrawCandidate(candidate);
            }
        }

        /// <summary>
        ///     Draws the title of the ballot
        /// </summary>
        /// <externalUnit cref="fntCont"/>
        /// <externalUnit cref="fntContSize"/>
        /// <externalUnit cref="GetFont"/>
        /// <externalUnit cref="pageMargin"/>
        /// <externalUnit cref="pdfPage"/>
        /// <externalUnit cref="PDFxFont"/>
        /// <revision revisor="dev11" date="12/24/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev11" date="01/12/2009" version="1.0.0.0">
        ///     Setting the proper font
        /// </revision>
        /// <revision revisor="dev11" date="01/20/2009" version="1.0.5.1">
        ///     Setting the proper font size
        /// </revision>
        /// <revision revisor="dev11" date="04/02/2009" version="1.0.9.1701">
        ///     added support for election info
        /// </revision>
        /// <revision revisor="dev11" date="04/11/2009" version="1.0.11.0201">
        ///     added support for templates
        /// </revision>
        /// <revision revisor="dev11" date="04/15/2009" version="1.0.11.0501">
        ///     added support for parameters on templates
        /// </revision>
        /// <revision revisor="dev11" date="04/16/2009" version="1.0.11.0601">
        ///     fixed vertical alignment of text to properly top-align with page
        ///     margin
        /// </revision>
        private void DrawTitle()
        {
            string title = "BALLOT";
            PDFxFont font = this.GetFont(this.fntCont);
            this.pdfContext.setFont(font, this.fntContSize);
            bool titleFromTemplate = false;
            double x1 = 0.75 * 72D, 
                   x2 = this.pageSize.Width - BallotIdLeftFromPageRight
                        - this.regMarkQuietZone, 
                y = this.pageMargin;

            if ((this.Template != null) && (this.Template.IsValidXml == true))
            {
                string templateName = "BallotTitle";
                TXMLxEngine txmlEngine = this.pdfDoc.getTXMLEngine();
                TXMLxAttributes txmlAttributes =
                    this.GetTXMLAttributes(txmlEngine);
                TXMLxData txmlData = txmlEngine.createTXMLData(
                    this.Template.Txml, txmlAttributes);
                try
                {
                    this.pdfPage.drawTXMLTextArea(
                        x1, x2, y, txmlData, templateName);
                    titleFromTemplate = true;
                }
                catch (Exception)
                {
                    // do nothing and let the next if statement try the title
                    // using the ElectionInfo and that does not work, then the
                    // default title
                }
            }

            if (titleFromTemplate == false)
            {
                if (this.ElectionInfo != null)
                {
                    if (string.IsNullOrEmpty(this.ElectionInfo.ElectionName)
                        == false)
                    {
                        title = this.ElectionInfo.ElectionName;
                    }

                    if (this.ElectionInfo.ElectionDate != null)
                    {
                        title = title + "\n"
                                +
                                this.ElectionInfo.ElectionDate.Value.
                                    ToLongDateString();
                    }
                }

                this.pdfPage.drawTextArea(x1, x2, y, title);
            }
        }

        /// <summary>
        ///     Gets the TXML attributes.
        /// </summary>
        /// <param name="engine">
        ///     The engine.
        /// </param>
        /// <returns>
        ///     the TXML attributes
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="4/15/2009" version="1.0.11.0501">
        ///     Member Created
        /// </revision>
        private TXMLxAttributes GetTXMLAttributes(TXMLxEngine engine)
        {
            TXMLxAttributes attributes = engine.createXMLAttributes();
            string electionName = string.Empty, 
                   electionDate = string.Empty, 
                   ballotStyleName = string.Empty, 
                   precinctName = string.Empty;

            if (this.ElectionInfo != null)
            {
                if (!string.IsNullOrEmpty(this.ElectionInfo.ElectionName))
                {
                    electionName = this.ElectionInfo.ElectionName;
                }

                if (this.ElectionInfo.ElectionDate != null)
                {
                    electionDate =
                        this.ElectionInfo.ElectionDate.Value.ToLongDateString();
                }
            }

            if (string.IsNullOrEmpty(this.BallotStyleName) == false)
            {
                ballotStyleName = this.BallotStyleName;
            }

            if (string.IsNullOrEmpty(this.PrecinctName) == false)
            {
                precinctName = this.PrecinctName;
            }

            attributes.add(
                TxmlPlaceholder.ElectionName.ToString(), electionName);
            attributes.add(
                TxmlPlaceholder.ElectionDate.ToString(), electionDate);
            attributes.add(
                TxmlPlaceholder.BallotStyleName.ToString(), ballotStyleName);
            attributes.add(
                TxmlPlaceholder.PrecinctName.ToString(), precinctName);

            return attributes;
        }

        /// <summary>
        ///     Sets the current page during the PDF generation according to a 
        ///     given contest. 
        /// </summary>
        /// <param name="cont">
        ///     The contest.
        /// </param>
        /// <externalUnit cref="bottomY"/>
        /// <externalUnit cref="Contest"/>
        /// <externalUnit cref="DrawBallotArtifacts"/>
        /// <externalUnit cref="DrawFrame"/>
        /// <externalUnit cref="pageSizeInches"/>
        /// <externalUnit cref="pdfDoc"/>
        /// <externalUnit cref="pdfPage"/>
        /// <externalUnit cref="rightX"/>
        /// <revision revisor="dev11" date="12/24/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev11" date="01/13/2008" version="1.0.0.0">
        ///     Added frame drawing on the current page BEFORE creating a 
        ///     new page
        /// </revision>
        /// <revision revisor="dev11" date="01/14/2009" version="1.0.4.14">
        ///     Modified frame drawing to include column borders
        /// </revision>
        private void SetCurrentPage(Contest cont)
        {
            this.SetCurrentPage(cont.Page);
        }

        /// <summary>
        ///     Sets the current page for a given candidate
        /// </summary>
        /// <param name="cand">
        ///     The candidate
        /// </param>
        /// <externalUnit cref="Candidate"/>
        /// <externalUnit cref="pdfDoc"/>
        /// <externalUnit cref="pdfPage"/>
        /// <externalUnit cref="DrawFrame"/>
        /// <externalUnit cref="DrawBallotArtifacts"/>
        /// <externalUnit cref="bottomY"/>
        /// <externalUnit cref="rightX"/>
        /// <revision revisor="dev11" date="12/24/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev11" date="01/13/2008" version="1.0.0.0">
        ///     Added frame drawing on the current page BEFORE creating a 
        ///     new page
        /// </revision>
        /// <revision revisor="dev11" date="01/14/2009" version="1.0.4.14">
        ///     Modified frame drawing to include column borders
        /// </revision>
        private void SetCurrentPage(Candidate cand)
        {
            this.SetCurrentPage(cand.Page);
        }

        /// <summary>
        ///     Sets the current page for a given candidate
        /// </summary>
        /// <param name="zeroBasedPageNumber">
        ///     The Page Number
        /// </param>
        /// <externalUnit cref="Candidate"/>
        /// <externalUnit cref="pdfDoc"/>
        /// <externalUnit cref="pdfPage"/>
        /// <externalUnit cref="DrawFrame"/>
        /// <externalUnit cref="DrawBallotArtifacts"/>
        /// <externalUnit cref="bottomY"/>
        /// <externalUnit cref="rightX"/>
        /// <revision revisor="dev11" date="12/24/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev11" date="01/13/2008" version="1.0.0.0">
        ///     Added frame drawing on the current page BEFORE creating a 
        ///     new page
        /// </revision>
        /// <revision revisor="dev11" date="01/14/2009" version="1.0.4.14">
        ///     Modified frame drawing to include column borders
        /// </revision>
        private void SetCurrentPage(int zeroBasedPageNumber)
        {
            int pageCount = this.pdfDoc.getPageCount();
            if (pageCount > zeroBasedPageNumber)
            {
                // add 1 since object page index is 0-based, but AquaPdf is 
                // 1-based 
                this.pdfPage = this.pdfDoc.getPage(zeroBasedPageNumber + 1);
            }
            else
            {
                if (pageCount > 0)
                {
                    this.DrawFrame();
                }

                // add a new page to the document and draw all artifacts (notice
                // artifacts differ from front to back) 
                this.pdfPage = this.pdfDoc.createCustomPage(
                    this.pageSizeInches.Width, this.pageSizeInches.Height);
                this.DrawBallotArtifacts();

                // reset [bottomY] as a new page is created 
                this.bottomY = 0;
                this.rightX = 0;
            }
        }

        /// <summary>
        ///     Draws the ballot artifacts (registration marks, barcode, 
        ///     instructions and title)
        /// </summary>
        /// <externalUnit cref="currentCardCount"/>
        /// <externalUnit cref="DrawBallotId"/>
        /// <externalUnit cref="DrawInstructions"/>
        /// <externalUnit cref="DrawTitle"/>
        /// <externalUnit cref="HasIdentifier"/>
        /// <externalUnit cref="identifierFace"/>
        /// <externalUnit cref="java.awt.Color"/>
        /// <externalUnit cref="PaperSide"/>
        /// <externalUnit cref="pageSize"/>
        /// <externalUnit cref="pdfContext"/>
        /// <externalUnit cref="pdfPage"/>
        /// <externalUnit cref="RegistrationMarkMargin"/>
        /// <externalUnit cref="RegistrationMarkSide"/>
        /// <revision revisor="dev11" date="12/24/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev06" date="1/10/2009" version="1.0.0.0">
        ///     Added the current card number addition to current card count so 
        ///     that card number is global to all ballots
        /// </revision>
        /// <revision revisor="dev11" date="01/12/2009" version="1.0.0.0">
        ///     Now detects the front of every card and not just the first card
        /// </revision>
        /// <revision revisor="dev11" date="02/11/2009" version="1.0.6.7">
        ///     Lower-right registration mark now appears on back pages as well
        ///     (making all 4 registration marks apear on boths sides of the 
        ///     ballot
        /// </revision>
        /// <revision revisor="dev11" date="02/24/2009" version="1.0.8.0701">
        ///     Added support for having the barcode on the front or the back
        ///     of the paper
        /// </revision>
        /// <revision revisor="dev11" date="02/25/2009" version="1.0.8.0801">
        ///     add usage of <see cref="HasIdentifier"/>
        /// </revision>
        /// <revision revisor="dev11" date="03/07/2009" version="1.0.8.1801">
        ///     [cardNumber] now fixed to be 0-based
        /// </revision>
        /// <revision revisor="dev11" date="03/31/2009" version="1.0.9.1501">
        ///     added test artifacts
        /// </revision>
        /// <revision revisor="dev11" date="04/08/2009" version="1.0.10.0501">
        ///     test artifacts printed on every page, not just the front
        /// </revision>
        private void DrawBallotArtifacts()
        {
            // in order to know whether a page is a front or a back use a simple
            // even-odd test, keeping in mind that page numbers are 1-based
            bool front = (this.pdfPage.getPageNumber() % 2) == 1;

            // make the card number 0-based too and continuous using the
            // current card count
            int cardNumber = ((this.pdfPage.getPageNumber() - 1) / 2)
                             + this.currentCardCount;

            // By definition registration marks are 0.25"x0.25" and 0.25" away 
            // from the borders of the paper 
            double side = PaperBallot.RegistrationMarkSide, 
                   margin = PaperBallot.RegistrationMarkMargin;

            this.pdfContext.setBgColor(JColor.BLACK);

            // upper-left registration mark 
            this.pdfPage.drawRect(margin, margin, side, side, true, false);

            // lower-left registration mark 
            this.pdfPage.drawRect(
                margin, 
                this.pageSize.Height - margin - side, 
                side, 
                side, 
                true, 
                false);

            // upper-right registration mark 
            this.pdfPage.drawRect(
                this.pageSize.Width - margin - side, 
                margin, 
                side, 
                side, 
               true, 
               false);

            // lower-right registration mark 
            this.pdfPage.drawRect(
               this.pageSize.Width - margin - side, 
               this.pageSize.Height - margin - side, 
               side, 
               side, 
               true, 
               false);

            if ((HasIdentifier(
                this.pdfPage.getPageNumber(), false, this.identifierFace)
                 && this.identifier.HasFields))
            {
                // the current page has the identifier (barcode)
                this.DrawBallotId(cardNumber);
            }

            if ((this.testDeckIndex != null) && (this.testDeckIndex >= 0))
            {
                this.DrawTestArtifacts();
            }

            if (front == true)
            {
                this.DrawInstructions();
                this.DrawTitle();
            } 
        }

        /// <summary>
        ///     Creates a new PDF document, initializes some context settings 
        ///     and draws all contests and candidates
        /// </summary>
        /// <externalUnit cref="Contest"/>
        /// <externalUnit cref="contests"/>
        /// <externalUnit cref="DrawCandidates"/>
        /// <externalUnit cref="DrawContest"/>
        /// <externalUnit cref="DrawFrame"/>
        /// <externalUnit cref="dsTask"/>
        /// <externalUnit cref="PDF"/>
        /// <externalUnit cref="pdfContext"/>
        /// <externalUnit cref="pdfDoc"/>
        /// <revision revisor="dev11" date="1/12/2009" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev11" date="01/13/2008" version="1.0.0.0">
        ///     Added fram drawing of the current page after drawing all the 
        ///     candidates
        /// </revision>
        /// <revision revisor="dev11" date="03/02/2009" version="1.0.8.1301">
        ///     Added a blank page for the barcode when the number of pages is 
        ///     an odd number and the barcode is suppose to be on the back
        /// </revision>
        private void Draw()
        {
            int pageCount;

            // create a new document instance using the current task 
            this.pdfDoc = PDF.createDocument(this.dsTask);

            // get the context object for that document 
            this.pdfContext = this.pdfDoc.getContext();

            // set a line width thinner than the one by default 
            this.pdfContext.setLineWidth(0.25); // <--------- line width

            if (this.IsAudioBallot)
            {
                this.DrawControlArtifacts();
            }
            else
            {
                // for every contest, draw the contest header and then all of 
                // its candidates
                foreach (Contest cont in this.contests)
                {
                    this.DrawContest(cont);
                    this.DrawCandidates(cont);
                }

                // draw the frame of the last page. This is needed here since 
                // the regular trigger for calling that method is at the time of 
                // needing a new page
                this.DrawFrame();
            }

            // get the number of pages and add a blank page for the barcode of
            // the last card if necessary
            pageCount = this.pdfDoc.getPageCount();
            if ((this.identifier.HasFields == true)
                && (this.identifierFace == PaperSide.Back)
                && (pageCount % 2 == 1))
            {
                this.SetCurrentPage(pageCount + 1);
            }
        }

        /// <summary>
        ///     Generates the ballot PDFs (each page is saved to a separate 
        ///     file) and saves them to the specified path using the specified 
        ///     name and the page index. This method will create the output 
        ///     folder in case it doesn't exist (Pdf.ExtractPagesAsSeparateFiles 
        ///     will do it)
        /// </summary>
        /// <param name="outputFolder">
        ///     The output folder.
        /// </param>
        /// <param name="filename">
        ///     The filename.
        /// </param>
        /// <param name="faces">
        ///     The face numbers to print.
        /// </param>
        /// <returns>
        ///     The draw sub call.
        /// </returns>
        /// <externalUnit cref="Pdf"/>
        /// <externalUnit cref="dsTask"/>
        /// <externalUnit cref="DSxTask"/>
        /// <externalUnit cref="Draw()"/>
        /// <externalUnit cref="pdfDoc"/>
        /// <exception cref="PdfIOException">
        ///     Unable to write at least 1 of the PDF files
        /// </exception>
        /// <revision revisor="dev11" date="12/29/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev06" date="1/10/2009" version="1.0.0.0">
        ///     Updated to return the number of pages/faces that are drawn
        /// </revision>
        /// <revision revisor="dev11" date="02/25/2009" version="1.0.8.0801">
        ///     Added support for specific pages
        /// </revision>
        /// <revision revisor="dev11" date="03/18/2009" version="1.0.9.0201">
        ///     removed underscore from method name
        /// </revision>
        /// <revision revisor="dev11" date="04/10/2009" version="1.0.11.0101">
        ///     added temp repository as the font folder
        /// </revision>
        private int DrawSubCall(
            string outputFolder, string filename, int[] faces)
        {
            // create return param
            int pageCount = 0;

            // before doing anything with the AquaPDF library, the license key
            // has to be set first 
            Pdf.Activate();

            // a task has to be created for everything and cannot be closed 
            // before finishing the entire drawing process 
            this.dsTask = new DSxTask();
            this.dsTask.setTempRepository(new DSxRepository(this.fontFolder));

            // create the pdf document 
            this.Draw();
            try
            {
                // set return value to pages drawn
                pageCount = this.pdfDoc.getPageCount();

                if ((faces == null) || (faces.Length == 0))
                {
                    // save each page as a separate file 
                    Pdf.ExtractPagesAsSeparateFiles(
                       this.pdfDoc, outputFolder, filename);
                }
                else
                {
                    // save each page as a separate file 
                    Pdf.ExtractPagesAsSeparateFiles(
                        this.pdfDoc, outputFolder, filename, faces);
                }
            }
            finally
            {
                // no catch block is needed since [ExtractPagesAsSeparateFiles]
                // already throws [PdfIOException] exceptions that are handled 
                // by the caller of this method release all resources 
                this.dsTask.close();
            }

            return pageCount;
        }

        #endregion
        #endregion
    }
}
