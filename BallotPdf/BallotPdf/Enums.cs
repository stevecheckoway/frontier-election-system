// -----------------------------------------------------------------------------
// <copyright file="Enums.cs" company="Sequoia Voting Systems"> 
//    Copyright (c) 2008 Sequoia Voting Systems, Inc. All Rights Reserved.
//    Distribution of source code is allowable only under the terms of the
//    license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//    This file contains the Enums class.
// </summary>
// <revision revisor="dev11" date="12/9/2008" version="1.0.0.0">
//    File Created
// </revision>  
// <revision revisor="dev16" date="1/24/2009" version="1.0.5.5">
//    File Modified
// </revision>
// <revision revisor="dev11" date="02/24/2009" version="1.0.8.0701">
// File modified</revision>
// <revision revisor="dev11" date="03/31/2009" version="1.0.9.1501">
// File modified
// </revision>
// <revision revisor="dev05" date="08/27/09" version="1.0.16.6">
//    File modified.
// </revision>
// -----------------------------------------------------------------------------

namespace Sequoia.Ballot
{
    #region Using directives

    using Utilities;

    #endregion

    /// <summary>
    ///     Possible values used on the "break" attribute of contests and 
    ///     candidates.
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev11" date="12/9/2008" version="1.0.0.0">
    ///     Class created.
    /// </revision>
    public enum BreakType
    {
        /// <summary>
        ///     No break specified
        /// </summary>
        None = 0,

        /// <summary>
        ///     Page break. Move the element to the next page
        /// </summary>
        Page = 1,

        /// <summary>
        ///     Column break. Move the element to the next column
        /// </summary>
        Column = 2,

        /// <summary>
        ///     No break with double width
        /// </summary>
        NoneDouble = 3,

        /// <summary>
        ///     Page break with double width
        /// </summary>
        PageDouble = 4,

        /// <summary>
        ///     Column brake with double width
        /// </summary>
        ColumnDouble = 5
    }

    /// <summary>
    ///     Card Ids is a concatenated string made of key-value pairs, as 
    ///     follows:
    ///     key1:value1|key2:value2|...
    ///     This enum provides a list of all possible fields. Notice that the 
    ///     actual name used in the compound Id is the enum's text attribute.
    /// </summary>
    /// <revision revisor="dev11" date="02/25/2009" version="1.0.8.0801">
    ///     Ballot style field added
    /// </revision>
    /// <revision revisor="dev11" date="03/03/2009" version="1.0.8.1401">
    ///     Added binary values to each enum value so that they can be selected 
    ///     using a bitwise integer
    /// </revision>
    /// <revision revisor="dev11" date="03/12/2009" version="1.0.8.2301">
    ///     language moved to first, all values refreshed
    /// </revision>
    /// <revision revisor="dev11" date="11/02/2009" version="1.1.2.1101">
    ///     Added <see cref="ControlBallot"/>
    /// </revision>
    public enum IdField
    {
        /// <summary>
        ///     Language Flag
        /// </summary>
        [EnumText(Text = "LNG")]
        Language = 1,

        /// <summary>
        ///     Ballot style ID
        /// </summary>
        [EnumText(Text = "BLS")]
        BallotStyleId = 2,

        /// <summary>
        ///     Ballot type
        /// </summary>
        [EnumText(Text = "BLT")]
        BallotTypeId = 4,

        /// <summary>
        ///     The Card ID
        /// </summary>
        [EnumText(Text = "CRD")]
        Card = 8,

        /// <summary>
        ///     The ballot Code ID
        /// </summary>
        [EnumText(Text = "CDE")]
        Code = 16,

        /// <summary>
        ///     Precinct ID
        /// </summary>
        [EnumText(Text = "PCT")]
        Precinct = 32,

        /// <summary>
        ///     Audio control ballot
        /// </summary>
        [EnumText(Text = "CTL")]
        ControlBallot = 64
    }

    /// <summary>
    ///     This enum type is used to place the voting target to the left or 
    ///     right side of the candidate box on a ballot.
    /// </summary>
    /// <revision revisor="dev11" date="1/19/09" version="1.0.4.20">
    ///     Member created
    /// </revision>
    /// <revision revisor="dev16" date="1/24/2009" verision="1.0.5.5">
    ///     Changed Leading and Trailing to Left and Right
    /// </revision>
    public enum TargetLayout
    {
        /// <summary>
        ///     The target is located to the left of the candidate box
        /// </summary>
        Left,

        /// <summary>
        ///     The target is located to the right of the candidate box
        /// </summary>
        Right
    }

    /// <summary>
    ///     Certain artifacts in the paper ballot are located on the front or 
    ///     the back of a single paper. For instance, an election official might 
    ///     design the ballots so that barcodes are placed in the [Back] but the 
    ///     title and instructions in the [Front].
    /// </summary>
    /// <revision revisor="dev11" date="02/24/2009" version="1.0.8.0701">
    ///     Member created
    /// </revision>
    public enum PaperSide
    {
        /// <summary>
        ///     Refers to the front side of a paper ballot
        ///     For 0-based page numbers, the front refers to even page numbers
        /// </summary>
        Front,

        /// <summary>
        ///     Refers to the back side of a paper ballot
        ///     For 0-based page numbers, the back refers to odd page numbers
        /// </summary>
        Back
    }

    /// <summary>
    ///     This enum type specifies the paper ballot type to use
    ///     The database stores the integer representation of each enum value
    /// </summary>
    public enum PaperBallotType
    {
        /// <summary>
        ///     Basic layout using columns, top to bottom, then left to
        ///     right. This type handles multiple faces and cards. Barcode is on
        ///     the back or the front of every card. Portrait paper orientation
        ///     Case study: The Philippines
        /// </summary>
        PaperBallot = 1,

        /// <summary>
        ///     This layout is a full face ballot in a matrix configuration. 
        ///     Rows represent contests, columns represent parties. Every ballot 
        ///     has only 1 (ONE) face which includes the barcode. Column break 
        ///     and page break formatting attributes are ignored. Landscape 
        ///     paper orientation. Case study: New Jersey
        /// </summary>
        MatrixPtyCstPaperBallot = 2
    }

    /// <summary>
    ///     Defines the target type that is displayed on the ballot so that the 
    ///     voter marks his/her selection.
    /// </summary>
    public enum TargetType
    {
        /// <summary>
        ///     Head and tail, the voter marks by joining head and tail with a
        ///     thick line.
        /// </summary>
        Arrow = 1,

        /// <summary>
        ///     An oval, the voter marks by filling the oval with black.
        /// </summary>
        Oval = 2
    }

    /// <summary>
    ///     Voting target parameters. The values are the ids on the database.
    /// </summary>
    public enum TargetParam
    {
        /// <summary>
        ///     Width of the voting target
        /// </summary>
        Width = 1,

        /// <summary>
        ///     Height of the voting target
        /// </summary>
        Height = 2,

        /// <summary>
        ///     Horizontal Offset
        /// </summary>
        OffsetX = 3,

        /// <summary>
        ///     Vertical Offset
        /// </summary>
        OffsetY = 4,

        /// <summary>
        ///     This is the line width of the target, not the mark.
        ///     In the case of an oval, this is the line width of the oval.
        /// </summary>
        LineWidth = 5,

        /// <summary>
        ///     This is the line density of the target (not to be confused with 
        ///     the density of the mark which is not stored on the database and 
        ///     is set from the GUI only).
        /// </summary>
        LineDensity = 6
    }

    /// <summary>
    ///     Defines the different ways to mark a target. This enum lists all 
    ///     supported marks, for all supported target types.
    ///     The least significant byte stores the target type.
    /// </summary>
    public enum TargetMark
    {
        // --- Arrow ----

        /// <summary>
        ///     (1 left shift 8) | TargetType.Arrow = 0001 0000 0001 = 257
        /// </summary>
        [EnumText(Text = "Default")]
        ArrowDefault = (1 << 8) | TargetType.Arrow,

        // --- Oval ----

        /// <summary>
        ///     (1 left shift  8) | TargetType.Oval = 0001 0000 0010 = 258
        /// </summary>
        [EnumText(Text = "Full shading")]
        OvalDefault = (1 << 8) | TargetType.Oval,

        /// <summary>
        ///     (2 left shift  8) | TargetType.Oval = 0010 0000 0010 = 514
        /// </summary>
        [EnumText(Text = "Half right")]
        OvalHalfRight = (2 << 8) | TargetType.Oval,

        /// <summary>
        ///     (3 left shift  8) | TargetType.Oval = 0011 0000 0010 = 770
        /// </summary>
        [EnumText(Text = "Half left")]
        OvalHalfLeft = (3 << 8) | TargetType.Oval,

        /// <summary>
        ///     (4 left shift  8) | TargetType.Oval = 0100 0000 0010 = 1026
        /// </summary>
        [EnumText(Text = "Half top")]
        OvalHalfTop = (4 << 8) | TargetType.Oval,

        /// <summary>
        ///     (5 left shift  8) | TargetType.Oval = 0101 0000 0010 = 1282
        /// </summary>
        [EnumText(Text = "Half bottom")]
        OvalHalfBottom = (5 << 8) | TargetType.Oval,

        /// <summary>
        ///     (6 left shift  8) | TargetType.Oval = 0110 0000 0010 = 1538
        /// </summary>
        [EnumText(Text = "X mark")]
        OvalXMark = (6 << 8) | TargetType.Oval,

        /// <summary>
        ///     (7 left shift  8) | TargetType.Oval = 0111 0000 0010 = 1792
        /// </summary>
        [EnumText(Text = "Check mark")]
        OvalCheckMark = (7 << 8) | TargetType.Oval
    }

    /// <summary>
    ///     This is the collection of parameters that are stored on the 
    ///     database, (PDFLayoutParameter table). The integer value of each enum 
    ///     member is the id on the database.
    /// </summary>
    /// <revision revisor="dev11" date="04/11/2009" version="1.0.11.0201">
    ///     Added data folder
    /// </revision>
    public enum PdfParam
    {
        /// <summary>
        ///     Gap width between head and tail of the arrow (in inches)
        /// </summary>
        ArrowGap = 18,

        /// <summary>
        ///     [true] forces the contest box height to be a multiple of 
        ///     candidate box height for the standard layout. See 
        ///     <see cref="PaperBallotType"/>. [false] fits contest box 
        ///     height around contest text independent of candidate height.
        /// </summary>
        BallotPageGrid = 13,

        /// <summary>
        ///     Height of the ballot paper (in inches)
        /// </summary>
        BallotPageHeight = 7,

        /// <summary>
        ///     Margin around the ballot paper (in inches). This margin applies 
        ///     to registration marks, header and content of the ballot.
        /// </summary>
        BallotPageMargin = 9,

        /// <summary>
        ///     Width of the ballot paper (in inches).
        /// </summary>
        BallotPageWidth = 8,

        /// <summary>
        ///     Its a padding value (in inches) to use inside candidate and 
        ///     contest boxes.
        /// </summary>
        BoxPadding = 1,

        /// <summary>
        ///     Font name of the candidate texts. This is either the name of a 
        ///     TrueType font file, [Helvetica] or [HelveticaBold].
        /// </summary>
        CandidateFont = 10,

        /// <summary>
        ///     Candidate font size (in points)
        /// </summary>
        CandidateFontSize = 3,

        /// <summary>
        ///     Candidate box height (in inches)
        /// </summary>
        CandidateHeight = 2,

        /// <summary>
        ///     Card header height (in inches). This value applies to the front
        ///     face only.
        /// </summary>
        CardHeaderHeight = 12,

        /// <summary>
        ///     Number of columns on the face. This value applies when the paper
        ///     ballot type is <see cref="PaperBallotType"/>
        /// </summary>
        ColumnCount = 4,

        /// <summary>
        ///     Font name of the contest texts. This is either the name of a 
        ///     TrueType font file, [Helvetica] or [HelveticaBold]
        /// </summary>
        ContestFont = 11,

        /// <summary>
        ///     Contest font size (in points)
        /// </summary>
        ContestFontSize = 5,

        /// <summary>
        ///     Full path of the EMS font folder. Typically this is a folder 
        ///     inside the EMS installation folder
        /// </summary>
        FontFolder = 16,

        /// <summary>
        ///     Identifier location: [Front, Back]. See <see cref="PaperSide"/>
        /// </summary>
        IdentifierLocation = 19,

        /// <summary>
        ///     Identifier mask: a bitwise integer
        ///     See <see cref="IdField"/>
        /// </summary>
        IdentifierMask = 22,

        /// <summary>
        ///     Represents the thickness of the line segment joining the head 
        ///     and tail of the arrow on premarked candidates. These are used 
        ///     for test deck generation
        /// </summary>
        MarkThickness = 6,

        /// <summary>
        ///     Defines the appearance of the ballot. Use one of the values 
        ///     provided by the enum type in integer form. 
        ///     See <see cref="PaperBallotType"/>
        /// </summary>
        PaperBallotType = 23,

        /// <summary>
        ///     Represents the margin around registration marks that must be 
        ///     clear of ANY other content, so that the scanner is able to 
        ///     properly identify the registration marks. (in inches)
        /// </summary>
        RegMarkQuietZone = 24,

        /// <summary>
        ///     Whether the arrow is on the left or the right of the candidate 
        ///     box Use the name (string) of the enum type values 
        ///     See <see cref="TargetLayout"/>
        /// </summary>
        TargetLayout = 17,

        /// <summary>
        ///     Defines the target type (arrow, oval, etc)
        ///     For valid values see <see cref="TargetType"/>. Use integer
        ///     representation of the enum values
        /// </summary>
        TargetType = 25,

        /// <summary>
        ///     Full path to the EMS data folder containing the PDF TXML 
        ///     templates file
        /// </summary>
        DataFolder = 26
    }

    /// <summary>
    ///     Enumerates all PDF standard fonts. Adobe Acrobat provides built-in
    ///     support for these and no TrueType font file is needed. The values 
    ///     correspond to those defined by AquaPDF:com.thentech.pdf.ext.PDFxFont
    /// </summary>
    public enum PdfStandardFont
    {
        /// <summary>
        ///     Courier font
        /// </summary>
        Courier = 8,

        /// <summary>
        ///     CourierBold font
        /// </summary>
        CourierBold = 9,

        /// <summary>
        ///     CourierBoldItalic font
        /// </summary>
        CourierBoldItalic = 11,

        /// <summary>
        ///     CourierItalic font
        /// </summary>
        CourierItalic = 10,

        /// <summary>
        ///     Helvetica font
        /// </summary>
        Helvetica = 0,

        /// <summary>
        ///     HelveticaBold font
        /// </summary>
        HelveticaBold = 1,

        /// <summary>
        ///     HelveticaBoldItalic font
        /// </summary>
        HelveticaBoldItalic = 3,

        /// <summary>
        ///     HelveticaItalic font
        /// </summary>
        HelveticaItalic = 2,

        /// <summary>
        ///     Symbol font
        /// </summary>
        Symbol = 12,

        /// <summary>
        ///     TimesBold font
        /// </summary>
        TimesBold = 5,

        /// <summary>
        ///     TimesBoldItalic font
        /// </summary>
        TimesBoldItalic = 7,

        /// <summary>
        ///     TimesItalic font
        /// </summary>
        TimesItalic = 6,

        /// <summary>
        ///     Times font
        /// </summary>
        Times = 4,

        /// <summary>
        ///     ZapfDingbats font
        /// </summary>
        ZapfDingbats = 13
    }

    /// <summary>
    ///     Enumerates all supported placeholders in TXML templates
    /// </summary>
    public enum TxmlPlaceholder
    {
        /// <summary>
        ///     Name of the election
        /// </summary>
        ElectionName,

        /// <summary>
        ///     Date of the Election
        /// </summary>
        ElectionDate,

        /// <summary>
        ///     Name of the ballot styl
        /// </summary>
        BallotStyleName,

        /// <summary>
        ///     Name of the precinct
        /// </summary>
        PrecinctName
    }

    /// <summary>
    ///     Enum for controll ballot type
    /// </summary>
    public enum ControlBallotType
    {
        /// <summary>
        ///     Audio ballot
        /// </summary>
        AudioBallot = 1
    }
}
