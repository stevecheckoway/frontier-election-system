// -----------------------------------------------------------------------------
// <copyright file="OfficeText.cs" company="Sequoia Voting Systems"> 
//    Copyright (c) 2008 Sequoia Voting Systems, Inc. All Rights Reserved.
//    Distribution of source code is allowable only under the terms of the
//    license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//    This file contains the OfficeText class.
// </summary>
// <revision revisor="dev11" date="12/3/2008" version="1.0.0.0">
//    File Created
// </revision>  
// -----------------------------------------------------------------------------

namespace Sequoia.Ballot.Data
{
    #region Using directives

    using System.Drawing;
    using System.Xml;

    using StpCont = Sequoia.Ballot.Data.BallotEntrySet.StpCont;

    #endregion

    /// <summary>
    ///     Class for OfficeText
    /// </summary>
    /// <revision revisor="dev11" date="12/3/2008" version="1.0.0.0">
    ///     Class created.
    /// </revision>
    public class OfficeText
    {
        #region Fields

        /// <summary>
        ///     actual text that is printed on the ballot
        /// </summary>
        private string text;

        /// <summary>
        ///     Font name (either a PDF built-in font, or the name of a TTF 
        ///     font. Do not include the path as this is a separate parameter)
        /// </summary>
        private string fontName;

        /// <summary>
        ///     font point size
        /// </summary>
        private double fontPointSize;

        /// <summary>
        ///     coordinates (in points) of the text
        /// </summary>
        private PointF xyText;

        #endregion
        
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="OfficeText"/> class.
        /// </summary>
        /// <param name="text">The office text.</param>
        /// <param name="font">The text font.</param>
        /// <param name="fontSize">Size of the font.</param>
        /// <externalUnit cref="fontName"/>
        /// <externalUnit cref="fontPointSize"/>
        /// <externalUnit cref="text"/>
        /// <revision revisor="dev11" date="12/3/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public OfficeText(string text, string font, double fontSize)
        {
            this.text = text;
            this.fontName = font;
            this.fontPointSize = fontSize;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the text.
        /// </summary>
        /// <value>The office text.</value>
        /// <externalUnit cref="text"/>
        /// <revision revisor="dev11" date="12/23/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public string Text
        {
            get
            {
                return this.text;
            }
        }

        /// <summary>
        ///     Gets the font.
        /// </summary>
        /// <value>The text font.</value>
        /// <externalUnit cref="fontName"/>
        /// <revision revisor="dev11" date="12/23/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public string Font
        {
            get
            {
                return this.fontName;
            }
        }

        /// <summary>
        ///     Gets the size of the font.
        /// </summary>
        /// <value>The size of the font.</value>
        /// <externalUnit cref="fontPointSize"/>
        /// <revision revisor="dev11" date="12/23/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public double FontSize
        {
            get
            {
                return this.fontPointSize;
            }
        }

        /// <summary>
        ///     Gets or sets the coordinates (in points) of the text.
        /// </summary>
        /// <value>The XY text.</value>
        /// <externalUnit cref="xyText"/>
        /// <revision revisor="dev11" date="12/23/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public PointF XYText
        {
            get
            {
                return this.xyText;
            }

            set
            {
                this.xyText = value;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Loads from XML.
        /// </summary>
        /// <param name="xmlText">The XML text.</param>
        /// <param name="defFont">The def font.</param>
        /// <param name="defSize">Size of the def.</param>
        /// <returns>
        ///     An <see cref="OfficeText" /> from xml.
        /// </returns>
        /// <externalUnit cref="Keys"/>
        /// <externalUnit cref="OfficeText"/>
        /// <externalUnit cref="XmlAttribute"/>
        /// <externalUnit cref="XmlNode"/>
        /// <revision revisor="dev11" date="12/23/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public static OfficeText LoadFromXml(
            XmlNode xmlText, string defFont, double defSize)
        {
            XmlAttribute xmlFont = xmlText.Attributes[Keys.ATR_OFT_FONT],
                         xmlSize = xmlText.Attributes[Keys.ATR_OFT_SIZE];
            string name = xmlText.InnerText,
                   font = (xmlFont != null) ? xmlFont.Value : defFont;
            double size = (xmlSize != null)
                              ? double.Parse(xmlSize.Value)
                              : defSize;

            return new OfficeText(name, font, size);
        }

        /// <summary>
        ///     Loads from db.
        /// </summary>
        /// <param name="contests">The contests.</param>
        /// <param name="index">The index.</param>
        /// <param name="defFont">The def font.</param>
        /// <param name="defSize">Size of the def.</param>
        /// <returns>
        ///     An <see cref="OfficeText" /> from the database.
        /// </returns>
        /// <externalUnit cref="BallotEntrySet"/>
        /// <externalUnit cref="OfficeText"/>
        /// <externalUnit cref="StpCont"/>
        /// <revision revisor="dev11" date="12/23/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public static OfficeText LoadFromDb(
            BallotEntrySet contests, int index, string defFont, double defSize)
        {
            string text = contests.GetValueStr(index, StpCont.DisplayText);
            return new OfficeText(text, defFont, defSize);
        }

        #endregion

        #region Public Events

        #endregion

        #region Private Methods

        #endregion
    }
}
