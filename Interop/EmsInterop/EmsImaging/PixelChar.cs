// -----------------------------------------------------------------------------
// <copyright file="PixelChar.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the PixelChar class.
// </summary>
// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
//     Added documentation header
// </revision>
// -----------------------------------------------------------------------------

namespace Sequoia.Ems.Imaging
{
    #region Using directives

    using System;
    using System.Drawing;
    using System.Drawing.Imaging;

    using Sequoia.Ems.Imaging.Exception;

    #endregion

    /// <summary>
    ///     Pixel character class
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
    ///     Added documentation header
    /// </revision>
    public class PixelChar : BaseBitmap
    {
        #region Constants

        /// <summary>
        ///     Every character is 7 pixels tall
        /// </summary>
        public static readonly int CharHeight = 7;

        /// <summary>
        ///     First ascii character
        /// </summary>
        private const int FirstAscii = 32;

        #endregion

        /// <summary>
        /// Every number represents a character by means of a bit-wise unsigned
        /// integer.
        /// 
        /// bit 0 : 0 -> character is 6 bits wide by 7 lines
        ///         1 -> character is 7 bits wide by 7 lines
        /// 
        /// bit 1 : upper left corner of character
        /// from there the path is left-to-right then top-to-bottom
        /// 
        /// i.e.
        /// 
        /// char  : 'G'
        /// ascii : 71
        /// idx   : 71 - 48 = 23 (this is 0-based) 
        /// value : 552214628262781 
        /// bits  : 01111101100011110001111100110000011110001101111101
        /// 
        ///         \_____/\_____/\_____/\_____/\_____/\_____/\_____/|
        ///            |      |      |      |      |      |      |    \
        ///            |      |      |      |      |      |      |     \
        ///           /      /      /       |       \      \      \     \
        ///          /      /       |       |        |      \      \     |
        ///         |       |       |       |        |      |       |    |
        ///         v       v       v       v        v      v       v    v
        /// 
        ///                                                            flag
        ///                                                           (bit 0)
        ///                                                              |
        /// line    7       6       5       4       3       2       1    v
        /// 
        ///      0111110 1100011 1100011 1110011 0000011 1100011 0111110 1
        /// 
        ///                                                            ^
        ///                                                            |
        ///                                                         top-left bit
        ///      mirrored    flip       
        ///       bits       horiz.              line
        /// 
        ///      0111110    0111110    .ooooo.    1
        ///      1100011    1100011    oo...oo    2
        ///      0000011    1100000    oo.....    3
        ///      1110011    1100111    oo..ooo    4
        ///      1100011    1100011    oo...oo    5
        ///      1100011    1100011    oo...oo    6
        ///      0111110    0111110    .ooooo.    7 
        /// </summary>
        private static ulong[] Char =
        {
                           0, /*   */
                           0, /* ! */
                           0, /* " */
                           0, /* # */
                           0, /* $ */
                           0, /* % */
                           0, /* & */
                           0, /* ' */
                           0, /* ( */
                           0, /* ) */
                           0, /* * */
                           0, /* + */
                           0, /* , */
                           0, /* - */
                           0, /* . */
                           0, /* / */
             552216745862013, /* 0 */
               8684832917400, /* 1 */
            1117317726495613, /* 2 */
             552212790010749, /* 3 */
             425579389727857, /* 4 */
             552212939899903, /* 5 */
             552214410158973, /* 6 */
             106390839387135, /* 7 */
             552214409110397, /* 8 */
             552213066933117, /* 9 */
                           0, /* : */
                           0, /* ; */
                           0, /* < */
                           0, /* = */
                           0, /* > */
                           0, /* ? */
                           0, /* @ */
             877685038495289, /* A */
             561010506326911, /* B */
             552162618893181, /* C */
             561010657321855, /* D */
            1117311847203839, /* E */
              26596312450047, /* F */
             552214628262781, /* G */
             877670123561927, /* H */
               8684832917118, /* I */
             267413456695545, /* J */
             874332461839303, /* K */
            1117311595545351, /* L */
             878224174998403, /* M */
             877678697113543, /* N */
             552214564299645, /* O */
              26596315620223, /* P */
             971141379384189, /* Q */
             874332665537407, /* R */
             552212795351933, /* S */
               1675446290046, /* T */
             552214564299719, /* U */
             250055025091527, /* V */
             483774293992391, /* W */
             578578384041671, /* X */
               1675456051686, /* Y */
            1117313244606719, /* Z */
        };

        /// <summary>
        ///     Draws the char on the given bitmap at the specified location.
        ///     The char is drawn in a 7x7 or 6x7 pixel grid.
        ///     Notice that the bitmap background is assumed to be white and the
        ///     character is drawn in black
        /// </summary>
        /// <param name="bitmap">The bitmap.</param>
        /// <param name="ch">
        ///     The character. Only 0-9,A-Z (upper case) are supported
        /// </param>
        /// <param name="left">
        ///     The x coordinate to the upper-left corner of the
        ///     character grid as measured in pixels from the upper-left corner 
        ///     of the bitmap
        /// </param>
        /// <param name="top">
        ///     The y coordinate to the upper-left corner of the
        ///     character grid as measured in pixels from the upper-left corner 
        ///     of the bitmap
        /// </param>
        /// <returns>
        ///     The line width.
        /// </returns>
        public static int DrawChar(Bitmap bitmap, char ch, int left, int top)
        {
            ulong definition = Char[Convert.ToInt32(ch) - FirstAscii],
                pixel;

            /* get the line width */
            int width = 6 + (int)(definition & 1), 
                /* all characters have 7 rows */
                height = CharHeight, 
                /* start from bit 1 since bit 0 is the width flag */
                bit = 1;
            
            /* for each line, draw every pixel in it */
            for (int j = 0; j < height; j = j + 1)
            {
                for (int i = 0; i < width; i = i + 1)
                {
                    /* get the mask for current pixel */
                    pixel = ((ulong) 1) << bit;
                    /* test the pixel bit using the mask */
                    if ((definition & pixel) == pixel)
                    {
                        // the pixel bit is set, so set the pixel color as
                        // black 
                        bitmap.SetPixel(left + i, top + j, Color.Black);
                    }

                    // whether the pixel was set to black or skipped, move to
                    // the next bit
                    bit = bit + 1;
                }
            }

            return width;
        }

        /// <summary>
        ///     Draws the char.
        /// </summary>
        /// <param name="bitmap">The bitmap.</param>
        /// <param name="ch">The character.</param>
        /// <param name="left">The left coordinate.</param>
        /// <param name="top">The top coordinate.</param>
        /// <returns>
        ///     The line width.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        public static int DrawChar(
            BitmapData bitmap, char ch, int left, int top)
        {
            return DrawChar(bitmap, ch, left, top, true);
        }

        /// <summary>
        ///     Draws the char.
        /// </summary>
        /// <param name="bitmap">The bitmap.</param>
        /// <param name="ch">The character.</param>
        /// <param name="left">The left coordinate.</param>
        /// <param name="top">The top coordinate.</param>
        /// <param name="black">if set to <c>true</c> [black].</param>
        /// <returns>
        ///     The line width.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        public static int DrawChar(
            BitmapData bitmap, char ch, int left, int top, bool black)
        {
            ulong definition = Char[Convert.ToInt32(ch) - FirstAscii],
                pixel;

            /* get the line width */
            int width = 6 + (int)(definition & 1),
                /* all characters have 7 rows */
                height = CharHeight,
                /* start from bit 1 since bit 0 is the width flag */
                bit = 1;

            /* for each line, draw every pixel in it */
            for (int j = 0; j < height; j = j + 1)
            {
                for (int i = 0; i < width; i = i + 1)
                {
                    /* get the mask for current pixel */
                    pixel = ((ulong) 1) << bit;
                    /* test the pixel bit using the mask */
                    if ((definition & pixel) == pixel)
                    {
                        // the pixel bit is set, so set the pixel color as
                        // black
                        SetPixel(bitmap, left + i, top + j, black);
                    }

                    // whether the pixel was set to black or skipped, move to
                    // the next bit
                    bit = bit + 1;
                }
            }

            return width;
        }

        /// <summary>
        ///     Measures the text.
        /// </summary>
        /// <param name="text">The text to measure.</param>
        /// <param name="charSpacing">The char spacing.</param>
        /// <returns>
        ///     The width.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        public static int MeasureText(string text, int charSpacing)
        {
            int width = (text.Length - 1) * charSpacing,
                charWidth;

            try
            {
                for (int i = 0; i < text.Length; i = i + 1)
                {
                    ulong definition =
                        Char[Convert.ToInt32(text[i]) - FirstAscii];
                    charWidth = 6 + (int)(definition & 1);
                    width = width + charWidth;
                }
            } 
            catch (System.Exception ex)
            {
                throw new CharacterNotSupportedException(string.Empty, ex);
            }

            return width;
        }
    }
}
