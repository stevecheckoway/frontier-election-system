// -----------------------------------------------------------------------------
// <copyright file="PaperBallotTarget.cs" company="Sequoia Voting Systems"> 
//    Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//    Distribution of source code is allowable only under the terms of the
//    license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//    This file contains the PaperBallotTarget class.
// </summary>
// <revision revisor="dev11" date="3/23/2009" version="1.0.9.0701">
//    File Created
// </revision>  
// -----------------------------------------------------------------------------

namespace Sequoia.Ballot.Data
{
    #region Using directives

    using System;
    using System.Collections.Generic;

    using com.thentech.pdf.ext;
    using JColor = java.awt.Color;
    using StpParam = BallotEntrySet.StpParam;
    using StpTargetParam = BallotEntrySet.StpTargetParam;
  
    #endregion

    /// <summary>
    ///     Class that represents different target types
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev11" date="3/23/2009" version="1.0.9.0701">
    ///     Class created.
    /// </revision>
    public class PaperBallotTarget
    {
        #region Constants

        /// <summary>
        ///     Constant for maximum value of the line density setting
        /// <see cref="TargetParam.LineDensity"/>
        /// </summary>
        private const float LineDensityMax = 100f;

        /// <summary>
        ///     Constant for first byte mask
        /// </summary>
        private const int FirstByteMask = 0xff;

        /// <summary>
        ///     Constant for check mark x1
        /// </summary>
        private const double CheckMarkX1 = 0.20;

        /// <summary>
        ///     Constant for check mark x2
        /// </summary>
        private const double CheckMarkX2 = 0.36;

        /// <summary>
        ///     Constant for check mark x3
        /// </summary>
        private const double CheckMarkX3 = 0.86;

        /// <summary>
        ///     Constant for check mark y1
        /// </summary>
        private const double CheckMarkY1 = 0.78;

        /// <summary>
        ///     Constant for check mark y2
        /// </summary>
        private const double CheckMarkY2 = 0.98;

        /// <summary>
        ///     Constant for check mark y3
        /// </summary>
        private const double CheckMarkY3 = 0.16;

        /// <summary>
        ///     Constant for x mark x1
        /// </summary>
        private const double XMarkX1 = 0.14;

        /// <summary>
        ///     Constant for x mark x2
        /// </summary>
        private const double XMarkX2 = 0.86;

        /// <summary>
        ///     Constant for x mark y1
        /// </summary>
        private const double XMarkY1 = 0.16;

        /// <summary>
        ///     Constant for x mark y2
        /// </summary>
        private const double XMarkY2 = 0.84;

        #endregion

        #region Fields

        /// <summary>
        ///     an entry set (StpTargetParam)
        /// </summary>
        private BallotEntrySet entrySet;

        /// <summary>
        ///     the color of the target line
        /// </summary>
        private JColor lineDensityColor;

        /// <summary>
        ///     the mark type to mark the target
        /// </summary>
        private TargetMark mark;

        /// <summary>
        ///     the color of the mark
        /// </summary>
        private JColor markDensityColor;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="PaperBallotTarget"/> class.
        /// </summary>
        /// <param name="connectionString">
        ///     The connection String.
        /// </param>
        /// <param name="targetType">
        ///     The target Type.
        /// </param>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="3/23/2009" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public PaperBallotTarget(string connectionString, TargetType targetType)
        {
            this.Type = targetType;
            this.entrySet = new BallotEntrySet(typeof(StpTargetParam));
            this.entrySet.Load(connectionString, Convert.ToInt32(targetType));

            float density = this.GetFltParam(TargetParam.LineDensity)
                            / LineDensityMax;

            // density 100 -> black
            // density 0   -> white
            this.lineDensityColor = new JColor(
                1 - density, 1 - density, 1 - density, 1f);
            this.InitializeTargetMark();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="PaperBallotTarget"/> class.
        /// </summary>
        /// <param name="entrySetTargetParams">
        ///     An entry set (StpTargetParam). See <see cref="StpTargetParam"/>.
        /// </param>
        /// <param name="targetType">
        ///     Type of the target. See <see cref="TargetType"/>.
        /// </param>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="3/24/2009" version="1.0.9.0701">
        ///     Member Created
        /// </revision>
        public PaperBallotTarget(
            BallotEntrySet entrySetTargetParams, 
            TargetType targetType)
        {
            this.Type = targetType;
            this.entrySet = entrySetTargetParams;

            float density = 
                this.GetFltParam(TargetParam.LineDensity) / LineDensityMax;

            // density 100 -> black
            // density 0   -> white
            this.lineDensityColor = new JColor(
                1 - density, 1 - density, 1 - density, 1f);
            this.InitializeTargetMark();
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the type.
        /// </summary>
        /// <value>The Target type.</value>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="3/24/2009" version="1.0.9.0701">
        ///     Member Created
        /// </revision>
        public TargetType Type
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets the color of the line density.
        /// </summary>
        /// <value>The color of the line density.</value>
        /// <externalUnit cref="java.awt.Color"/>
        /// <revision revisor="dev11" date="3/24/2009" version="1.0.9.0701">
        ///     Member Created
        /// </revision>
        public JColor LineDensityColor
        {
            get
            {
                return this.lineDensityColor;
            }
        }

        /// <summary>
        ///     Gets or sets the mark.
        /// </summary>
        /// <value>The Target mark.</value>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="5/27/2009" version="1.0.12.14">
        ///     Member Created
        /// </revision>
        public TargetMark Mark
        {
            get
            {
                return this.mark;
            }

            set
            {
                this.mark = value;
            }
        }

        /// <summary>
        ///     Gets the color of the mark density.
        /// </summary>
        /// <value>The color of the mark density.</value>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="5/27/2009" version="1.0.12.14">
        ///     Member Created
        /// </revision>
        public JColor MarkDensityColor
        {
            get
            {
                return this.markDensityColor;
            }
        }

        #endregion

        #region Static Methods

        /// <summary>
        ///     Gets the type of the target.
        /// </summary>
        /// <param name="parms">The parms.</param>
        /// <returns>A targettype for that param</returns>
        /// <externalUnit cref="BallotEntrySet"/>
        /// <externalUnit cref="PdfParam"/>
        /// <externalUnit cref="StpParam"/>
        /// <externalUnit cref="TargetType"/>
        /// <revision revisor="dev11" date="3/24/2009" version="1.0.9.0701">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev11" date="03/24/2009" version="1.0.9.0801">
        ///     updated to work with enum types and ids instead of names when 
        ///     searching on the entry set of pdf parameters
        /// </revision>
        public static TargetType GetTargetType(BallotEntrySet parms)
        {
            TargetType type;

            try
            {
                int idx = parms.FindIndex(
                    StpParam.PDFLayoutParamId, (int) PdfParam.TargetType);
                int val = parms.GetValueInt(idx, StpParam.ParamValue);
                type = (TargetType)Enum.ToObject(typeof(TargetType), val);
            }
            catch (Exception)
            {
                // use arrow as the default
                type = TargetType.Arrow;
            }

            return type;
        }

        /// <summary>
        ///     Gets the available target marks for a given target type
        /// </summary>
        /// <param name="type">The target type.</param>
        /// <returns> a targetMark for the target type</returns>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="4/20/2009" version="1.0.11.0801">
        ///     Member Created
        /// </revision>
        public static TargetMark[] GetTargetMarks(TargetType type)
        {
            // get all marks for all target types
            var allMarks =
                (TargetMark[])Enum.GetValues(typeof(TargetMark));

            // filter the entire set and get only those that apply to the given
            // mark type
            var myMarks = new List<TargetMark>();
            int intType = Convert.ToInt32(type);

            foreach (TargetMark mark in allMarks)
            {
                // if the first byte of the mark is the same value of the given
                // type, then include the mark
                if ((Convert.ToInt32(mark) & FirstByteMask) == intType)
                {
                    myMarks.Add(mark);
                }
            }

            // convert the list to an array and return
            return myMarks.ToArray();
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Gets the STR param.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>the value for that parameter</returns>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="3/24/2009" version="1.0.9.0701">
        ///     Member Created
        /// </revision>
        public string GetStrParam(Enum parameter)
        {
            string val = null;
            int idx = 
                this.entrySet.FindIndex(
                StpTargetParam.TargetParamId, 
                Convert.ToInt32(parameter));

            // if index is greater than or equals to 0 it means that an entry
            // was actually found. Otherwise, return null
            if (idx >= 0)
            {
                // get the actual value from that parameter
                val =
                    this.entrySet.GetValueStr(
                    idx,
                    StpTargetParam.TargetParamValue);
            }

            return val;
        }

        /// <summary>
        ///     Gets the DBL param.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>The double value of the param</returns>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="3/24/2009" version="1.0.9.0701">
        ///     Member Created
        /// </revision>
        public double GetDblParam(Enum parameter)
        {
            string val = this.GetStrParam(parameter);
            return double.Parse(val);
        }

        /// <summary>
        ///     Gets the FLT param.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>The flat value of the param</returns>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="3/24/2009" version="1.0.9.0701">
        ///     Member Created
        /// </revision>
        public float GetFltParam(Enum parameter)
        {
            string val = this.GetStrParam(parameter);
            return float.Parse(val);
        }

        /// <summary>
        ///     Draws the oval mark.
        /// </summary>
        /// <param name="pdfPage">The PDF page.</param>
        /// <param name="pdfContext">The PDF context.</param>
        /// <param name="x">The x (left, in points).</param>
        /// <param name="y">The y (top, in points).</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="markWidth">Width of the line to make the mark.</param>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="4/20/2009" version="1.0.11.0801">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev11" date="04/21/2009" version="1.0.11.0801">
        ///     added mark density color
        /// </revision>
        public void DrawOvalMark(
            PDFxPage pdfPage, 
            PDFxContext pdfContext,
            double x, 
            double y, 
            double width, 
            double height, 
            double markWidth)
        {
            double tempLineWidth = pdfContext.getLineWidth();
            int lineCap = pdfContext.getLineCapStyle(),
                lineJoin = pdfContext.getLineJoinStyle();
            JColor foreGroundcolor = pdfContext.getFgColor(),
                bgColor = pdfContext.getBgColor();

            pdfContext.setFgColor(this.markDensityColor);
            pdfContext.setBgColor(this.markDensityColor);
            switch (this.mark)
            {
                case TargetMark.OvalHalfBottom:
                    pdfPage.drawArc(
                        x, y, width, height, 180, 180, true, false, false);
                    break;
                case TargetMark.OvalHalfLeft:
                    pdfPage.drawArc(
                        x, y, width, height, 90, 180, true, false, false);
                    break;
                case TargetMark.OvalHalfRight:
                    pdfPage.drawArc(
                        x, y, width, height, -90, 180, true, false, false);
                    break;
                case TargetMark.OvalHalfTop:
                    pdfPage.drawArc(
                        x, y, width, height, 0, 180, true, false, false);
                    break;
                case TargetMark.OvalCheckMark:
                    pdfContext.setLineWidth(markWidth);
                    pdfContext.setLineCapStyle(
                        PDFxContext.__Fields.LINE_CAP_ROUND);
                    pdfContext.setLineJoinStyle(
                        PDFxContext.__Fields.LINE_JOIN_ROUND);
                    pdfPage.drawPolyline(
                        new[] 
                        { 
                            x + (width * CheckMarkX1), 
                            x + (width * CheckMarkX2), 
                            x + (width * CheckMarkX3) 
                        },

                        new[]
                        {
                            y + (height * CheckMarkY1), 
                            y + (height * CheckMarkY2), 
                            y + (height * CheckMarkY3)
                        });

                    break;
                case TargetMark.OvalXMark:
                    pdfContext.setLineWidth(markWidth);
                    pdfContext.setLineCapStyle(
                        PDFxContext.__Fields.LINE_CAP_ROUND);
                    pdfContext.setLineJoinStyle(
                        PDFxContext.__Fields.LINE_JOIN_ROUND);
                    pdfPage.drawLine(
                        x + (width * XMarkX1), 
                        y + (height * XMarkY1), 
                        x + (width * XMarkX2), 
                        y + (height * XMarkY2));
                    pdfPage.drawLine(
                        x + (width * XMarkX1), 
                        y + (height * XMarkY2), 
                        x + (width * XMarkX2), 
                        y + (height * XMarkY1));
                    break;
                default: // TargetMark.OvalDefault
                    pdfPage.drawOval(
                        x, 
                        y,
                        width,
                        height,
                        true,
                        false);
                    break;
            }

            pdfContext.setLineWidth(tempLineWidth);
            pdfContext.setLineCapStyle(lineCap);
            pdfContext.setLineJoinStyle(lineJoin);
            pdfContext.setFgColor(foreGroundcolor);
            pdfContext.setBgColor(bgColor);
        }

        /// <summary>
        ///     Sets the mark density.
        /// </summary>
        /// <param name="density">The density.</param>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="5/27/2009" version="1.0.12.14">
        ///     Member Created
        /// </revision>
        public void SetMarkDensity(double density)
        {
            float fdensity = Convert.ToSingle(density / 100D);
            this.markDensityColor = new JColor(
                1 - fdensity, 1 - fdensity, 1 - fdensity, 1f);
        }

        #endregion
    
        #region Public Events

        #endregion

        #region Private Methods

        /// <summary>
        ///     Initializes the target mark. This is actually used by the 
        ///     VoteSim only when creating pre-marked ballots
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="4/20/2009" version="1.0.11.0801">
        ///     Member Created
        /// </revision>
        private void InitializeTargetMark()
        {
            switch (this.Type)
            {
                case TargetType.Arrow:
                    this.mark = TargetMark.ArrowDefault;
                    break;
                default: // TargetType.Oval
                    this.mark = TargetMark.OvalDefault;
                    break;
            }
        }

        #endregion
    }
}
