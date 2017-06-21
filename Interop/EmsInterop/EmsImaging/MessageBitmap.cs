// -----------------------------------------------------------------------------
// <copyright file="MessageBitmap.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the MessageBitmap class.
// </summary>
// <revision revisor="dev11" date="2/11/2009" version="1.0.6.7">
//     File Created
// </revision>
// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
//     File modified
// </revision>
// -----------------------------------------------------------------------------

namespace Sequoia.Ems.Imaging
{
    #region Using directives

    using System;
    using System.Drawing;
    using System.Drawing.Imaging;

    #endregion

    /// <summary>
    ///	    Message Bitmap methods
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev11" date="2/11/2009" version="1.0.6.7">
    ///     Class created.
    /// </revision>
    /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
    ///     Formatting changes.
    /// </revision>
    public class MessageBitmap : BaseBitmap
    {
        #region Constants

        /// <summary>
        ///     This is the text that is printed on the bitmap
        /// </summary>
        private static readonly string Title = "INVALID";

        #endregion
        
        #region Public Methods

        /// <summary>
        ///     Draws the specified number.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <param name="height">The height.</param>
        /// <param name="dpi">The dpi for the image.</param>
        /// <returns>
        ///     A <see cref="Bitmap" /> object.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        public static Bitmap Draw(int number, float height, float dpi)
        {
            string fullText = string.Format(
                "{0} {1}", MessageBitmap.Title, number);
            return Draw(fullText, height, dpi, 0, 0);
        }

        /// <summary>
        ///     Draws the specified text.
        /// </summary>
        /// <param name="text">The text for the image.</param>
        /// <param name="height">The height.</param>
        /// <param name="dpi">The dpi of the image.</param>
        /// <param name="leftMargin">The left margin.</param>
        /// <param name="rightMargin">The right margin.</param>
        /// <returns>
        ///     A <see cref="Bitmap" /> object.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        public static Bitmap Draw(
            string text, 
            float height, 
            float dpi, 
            int leftMargin, 
            int rightMargin)
        {
            leftMargin = leftMargin + 10;
            rightMargin = rightMargin + 10;
            int msgHgt = Convert.ToInt32(Math.Floor(dpi * height)),
                msgWdt = PixelChar.MeasureText(text, 1) + leftMargin + rightMargin;

            Bitmap barcode = DrawMessage(text, msgWdt, msgHgt, dpi, leftMargin);
            return barcode;
        }

        #endregion

        #region Private Methods

        /// <summary>
        ///     Draws the message.
        /// </summary>
        /// <param name="text">The text for the image.</param>
        /// <param name="msgWdt">The MSG WDT.</param>
        /// <param name="msgHgt">The MSG HGT.</param>
        /// <param name="dpi">The dpiof the image.</param>
        /// <param name="leftMargin">The left margin.</param>
        /// <returns>
        ///     A <see cref="Bitmap" /> objects.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        private static Bitmap DrawMessage(
            string text, int msgWdt, int msgHgt, float dpi, int leftMargin)
        {
            // create the bitmap (1 bit per pixel)
            Bitmap bmpMsg = new Bitmap(
                msgWdt, msgHgt, PixelFormat.Format1bppIndexed);

            // set resolution
            bmpMsg.SetResolution(dpi, dpi);

            // get a rectangle for the entire area of the bitmap
            Rectangle rect = new Rectangle(0, 0, msgWdt, msgHgt);

            // and lock it to get a bitmap data object that is the one actually
            // used to handle the bitmap pixels. This is something that needs
            // to be done when dealing with 1-bit-per-pixel bitmaps
            BitmapData g = bmpMsg.LockBits(
                rect, ImageLockMode.WriteOnly, bmpMsg.PixelFormat);

            ClearBitmap(g, true);

            int x = leftMargin,
                y = (msgHgt - PixelChar.CharHeight) / 2,
                move;

            for (int i = 0; i < text.Length; i = i + 1)
            {
                move = PixelChar.DrawChar(g, text[i], x, y, false);
                x = x + move + 1;
            }

            bmpMsg.UnlockBits(g);

            return bmpMsg;
        }

        #endregion
    }
}
