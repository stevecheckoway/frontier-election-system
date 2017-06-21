// -----------------------------------------------------------------------------
// <copyright file="Keys.cs" company="Sequoia Voting Systems"> 
//    Copyright (c) 2008 Sequoia Voting Systems, Inc. All Rights Reserved.
//    Distribution of source code is allowable only under the terms of the
//    license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//    This is a sealed with common constants for the Sequoia.Ballot
//    namespace. No other functionality is provided here.
// </summary>
// <revision revisor="dev11" date="12/4/2008" version="1.0.0.0">
//    File Created
// </revision>  
// -----------------------------------------------------------------------------

namespace Sequoia.Ballot
{
    #region Using directives

    using System;

    #endregion

    /// <summary>
    ///     This is a sealed with common constants for the Sequoia.Ballot
    ///     namespace. No other functionality is provided here.
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev11" date="12/4/2008" version="1.0.0.0">
    ///     Class created.
    /// </revision>
    /// <revision revisor="dev11" date="1/18/09" version="1.0.4.19">
    ///     Added parameters for controlling the arrow appearance
    /// </revision>
    /// <revision revisor="dev11" date="02/26/2009" version="1.0.8.0901">
    ///     added comments and the identifer location parameters
    /// </revision>
    /// <revision revisor="dev11" date="03/24/2009" version="1.0.9.0801">
    ///     all constants used for pdf layout parameters removed (commented out)
    /// </revision>
    public sealed class Keys
    {
        #region Constructors

        #endregion

        #region Public Properties

        #endregion

        #region Public Methods

        #endregion

        #region Public Events

        #endregion

        #region Constants (XML)

        /// <summary>
        ///     ballot id (full string that is encoded into the PDF417 bar code)
        /// </summary>
        public const string PDF_BAL_ID = "pdf_bal_id";

        /// <summary>
        ///     padding for candidates and contest boxes (in inches, double)
        /// </summary>
        public const string PDF_BOX_PAD = "pdf_box_pad";

        /// <summary>
        ///     font file name (full path, string)
        /// </summary>
        public const string PDF_CND_FONT = "pdf_cnd_font";

        /// <summary>
        ///     candidate font point size (double)
        /// </summary>
        public const string PDF_CND_SIZE = "pdf_cnd_size";

        /// <summary>
        ///     height (in inches, double) of a candidate header
        /// </summary>
        public const string PDF_CND_HGT = "pdf_cnd_hgt";

        /// <summary>
        ///     # of columns (all columns will have the same width, integer)
        /// </summary>
        public const string PDF_COLS = "pdf_cols";

        /// <summary>
        ///     font file name (full path, string)
        /// </summary>
        public const string PDF_CST_FONT = "pdf_cst_font";

        /// <summary>
        ///     contest font point size (double)
        /// </summary>
        public const string PDF_CST_SIZE = "pdf_cst_size";

        /// <summary>
        ///     header height not including top margin (in inches, double)
        /// </summary>
        public const string PDF_HED_HGT = "pdf_hed_hgt";

        /// <summary>
        ///     for voted candidates, this value (in inches) represents the mark
        ///     thickness joining both ends of the arrow (double)
        /// </summary>
        public const string PDF_MRK_WDT = "pdf_mrk_wdt";

        /// <summary>
        ///     forces contest headers height to be multiple of candidate height
        ///     producing a perfect grid on the ballot. Use true or false
        /// </summary>
        public const string PDF_PGE_GRID = "pdf_pge_grid";
        
        /// <summary>
        ///     page height (in inches, double)
        /// </summary>
        public const string PDF_PGE_HGT = "pdf_pge_hgt";

        /// <summary>
        ///     page margin (in inches, double), same for all 4 margins
        /// </summary>
        public const string PDF_PGE_MRG = "pdf_pge_mrg";

        /// <summary>
        ///     page width (in inches, double)
        /// </summary>
        public const string PDF_PGE_WDT = "pdf_pge_wdt";

        /// <summary>
        ///     full path to the pdf resources folder
        /// </summary>
        public const string PDF_RESOURCES_DIR = "pdf_res";

        /// <summary>
        ///     Column break
        /// </summary>
        public const string ATR_CND_BREAK = "column_break";

        /// <summary>
        ///     ID
        /// </summary>
        public const string ATR_CND_ID = "id";

        /// <summary>
        ///     Voted flag
        /// </summary>
        public const string ATR_CND_VOTED = "voted";

        /// <summary>
        ///     Break
        /// </summary>
        public const string ATR_CST_BREAK = "break";

        /// <summary>
        ///     ID
        /// </summary>
        public const string ATR_CST_ID = "id";

        /// <summary>
        ///     Name tag
        /// </summary>
        public const string ATR_CST_NAME = "name";

        /// <summary>
        ///     Font tag
        /// </summary>
        public const string ATR_OFT_FONT = "font";

        /// <summary>
        ///     font Size tag
        /// </summary>
        public const string ATR_OFT_SIZE = "font_size";

        /// <summary>
        ///     Candidate Tag
        /// </summary>
        public const string TAG_CANDIDATE = "candidate";

        /// <summary>
        ///     contest Tag
        /// </summary>
        public const string TAG_CONTEST = "contest";

        /// <summary>
        ///     Office Text Tag
        /// </summary>
        public const string TAG_OFFICE_TXT = "office_text";

        /// <summary>
        ///     Path to contest
        /// </summary>
        public const string XPT_CONTEST = "ballot/contests/" + TAG_CONTEST;

        #endregion

        #region Private Methods

        #endregion
    }
}
