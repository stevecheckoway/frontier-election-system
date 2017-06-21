// -----------------------------------------------------------------------------
// <copyright file="Code128.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the Code128 class.
// </summary>
// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
//     Added documentation header
// </revision>
// -----------------------------------------------------------------------------

namespace Sequoia.Ems.Imaging.BarCode
{
    #region Using directives

    using System;
    using System.Drawing;
    using System.Drawing.Imaging;

    #endregion

    /// <summary>
    ///     Functionality for code 128 barcode.
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
    ///     Added documentation header
    /// </revision>
    public class Code128 : BaseBitmap
    {
        #region Definitions

        /// <summary>
        /// This array contains the encoding for all codewords
        /// Each codeword is encoded as an interlaced sequence of 3 bars and
        /// 3 spaces that are represented as bits in these numbers, except for
        /// STOP codeword (106)
        /// 
        /// i.e.
        ///                                     B S B S B S
        /// value   encoding      bits          bars/spaces
        /// -----   --------   -----------      -----------
        ///  00      1740      11011001100      2 1 2 2 2 2
        ///  01      1644      11001101100      2 2 2 1 2 2
        ///  ...
        ///  101     1886      11101011110      3 1 1 1 4 1
        ///  ...
        ///  105     1692      11010011100      2 1 1 2 3 2
        ///  106     6379      1100011101011    2 3 3 1 1 1 2
        /// </summary>
        private static readonly int[] Encoding = 
            {
                1740, 1644, 1638, 1176, 1164, 1100, 1224, 1220, 1124, 
                1608, 1604, 1572, 1436, 1244, 1230, 1484, 1260, 1254, 
                1650, 1628, 1614, 1764, 1652, 1902, 1868, 1836, 1830, 
                1892, 1844, 1842, 1752, 1734, 1590, 1304, 1112, 1094, 
                1416, 1128, 1122, 1672, 1576, 1570, 1464, 1422, 1134, 
                1496, 1478, 1142, 1910, 1678, 1582, 1768, 1762, 1774, 
                1880, 1862, 1814, 1896, 1890, 1818, 1914, 1602, 1930, 
                1328, 1292, 1200, 1158, 1068, 1062, 1424, 1412, 1232, 
                1218, 1076, 1074, 1554, 1616, 1978, 1556, 1146, 1340, 
                1212, 1182, 1508, 1268, 1266, 1956, 1940, 1938, 1758, 
                1782, 1974, 1400, 1310, 1118, 1512, 1506, 1960, 1954, 
                1502, 1518, 1886, 1966, 1668, 1680, 1692, 6379
            };

        /// <summary>
        ///     Code A code
        /// </summary>
        private const int CodeA = 101;

        /// <summary>
        ///     Start C code
        /// </summary>
        private const int StartC = 105;

        /// <summary>
        ///     Stop code
        /// </summary>
        private const int Stop = 106;

        #endregion

        #region Private methods

        /// <summary>
        ///     Computes the bar width in pixels
        /// </summary>
        /// <param name="codewords">the data codewords</param>
        /// <param name="xDim">x-dimension, in pixels</param>
        /// <returns>width in pixels</returns>
        /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        private static int GetBarWidth(int[] codewords, int xDim)
        {
            // [start codeword] + 
            // [codewords] +
            // [checksum] +
            // [stop codeword] 
            int width = 1 + codewords.Length + 1 + 1;

            // [each codeword has 11 modules] +
            // [termination bar of 11 (2 modules)]
            width = 11 * width + 2;

            // multiply #modules by x-dimension
            width = xDim * width;
            return width;
        }

        /// <summary>
        ///     Computes the checksum digit of the barcode
        /// </summary>
        /// <param name="codewords">
        ///     an array of integer where each slot contains a double-digit 
        ///     number. For numbers lesser than 10, it is assumed to have 
        ///     a heading 0
        /// </param>
        /// <returns>checksum value</returns>
        /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        private static int GetChecksum(int[] codewords)
        {
            // initialize with 105 which is the START codeword for Code 128C
            // and will be the first codeword of the bar code
            long sum = StartC;

            for (int i = 0; i < codewords.Length; i = i + 1)
            {
                sum = sum + (1 + i) * codewords[i];
            }

            // as defined by the spec, the checksum is the remainder of dividing
            // the sum by 103
            int checksum = (int) sum % 103;
            return checksum;
        }

        /// <summary>
        /// E   ncodes a digit-only string into codewords
        /// </summary>
        /// <param name="text">the text to encode (digits only)</param>
        /// <returns>
        ///     all data codewords (start, checksum and stop codewords are
        ///     not included
        /// </returns>
        /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        private static int[] GetCodewords(string text)
        {
            int[] codewords;
            int length = text.Length, count;
            bool odd = (length % 2 == 1) ? true : false;
            if (odd == true)
            {
                // [encode an even number of digits as code-c] +
                // [code-a] +
                // [the last digit]
                count = ((length - 1) / 2) + 1 + 1;
                codewords = new int[count];

                // code-a
                codewords[count - 2] = CodeA;

                // get ascii of last character and subtract 32 to
                // get the value in code-a
                codewords[count - 1] = ((int) text[length - 1]) - 32;
                text = text.Substring(0, length - 1);
            }
            else
            {
                count = length / 2;
                codewords = new int[count];
            }

            int i = 0, j = 0;
            while (i < text.Length)
            {
                codewords[j] = int.Parse(text.Substring(i, 2));
                j = j + 1;
                i = i + 2;
            }

            return codewords;
        }

        /// <summary>
        ///     Draws the codeword
        /// </summary>
        /// <param name="g"></param>
        /// <param name="codeword">a 2-digit number</param>
        /// <param name="x">x-coordinate to draw the codeword</param>
        /// <param name="xDim">x-dimension</param>
        /// <param name="height">bitmap height</param>
        /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        private static void DrawCodeword(
            BitmapData g, int codeword, int x, int xDim, int height)
        {
            // bar pattern as a bit-wise integer. Each bit represents a module
            // that when 1 it is a bar, and when 0 it is a space 
            int encoding = Encoding[codeword],

                // either 1 or 0
                bit,

                // number of bits of the encoding value
                bits = 0, aux = encoding;

            // count the number of bits on this codeword. All codewords are
            // supposed to be 11-bit long except for the STOP codeword who is
            // 13-bit long
            while (aux > 0)
            {
                bits = bits + 1;
                aux = aux >> 1;
            }

            x = x + bits * xDim;
            for (int i = 0; i < bits; i = i + 1)
            {
                bit = 1 & (encoding >> i);
                x = x - xDim;
                if (bit == 1)
                {
                    FillRectangle(g, x, 0, xDim, height, true);
                }
            }
        }

        /// <summary>
        ///     Draws the barcode.
        /// </summary>
        /// <param name="digits">The digits.</param>
        /// <param name="barWdt">The bar WDT.</param>
        /// <param name="barHgt">The bar HGT.</param>
        /// <param name="xDim">The x dim.</param>
        /// <param name="dpi">The dpi for the image.</param>
        /// <param name="leftMargin">The left margin.</param>
        /// <returns>
        ///     The barcode <see cref="Bitmap" />
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="10/24/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev11" date="02/11/2009" version="1.0.6.7">
        ///     Before returning the bitmap, now the bits are unlocked
        /// </revision>
        private static Bitmap DrawBarcode(
            int[] digits, 
            int barWdt, 
            int barHgt, 
            int xDim, 
            float dpi, 
            int leftMargin)
        {
            int x = leftMargin,
                cwWdt = 11 * xDim;
            Bitmap bmpBar = new Bitmap(
                barWdt, barHgt, PixelFormat.Format1bppIndexed);
            bmpBar.SetResolution(dpi, dpi);
            Rectangle rect = new Rectangle(0, 0, barWdt, barHgt);
            BitmapData g = bmpBar.LockBits(
                rect, ImageLockMode.WriteOnly, bmpBar.PixelFormat);
            ClearBitmap(g);

            int checksum = GetChecksum(digits);

            DrawCodeword(g, StartC, x, xDim, barHgt);
            x = x + cwWdt;
            for (int i = 0; i < digits.Length; i = i + 1)
            {
                DrawCodeword(g, digits[i], x, xDim, barHgt);
                x = x + cwWdt;
            }

            DrawCodeword(g, checksum, x, xDim, barHgt);
            x = x + cwWdt;
            DrawCodeword(g, Stop, x, xDim, barHgt);

            bmpBar.UnlockBits(g);

            return bmpBar;
        }

        #endregion

        #region Public methods

        /// <summary>
        ///     Encodes the text (digits only) using Code 128 C and creates a 
        ///     bitmap according to specified parameters.
        /// </summary>
        /// <param name="text">the text to encode (digits only)</param>
        /// <param name="xDim">
        ///     x-dimension in pixels. By using pixels here, the user is forced 
        ///     to provide a measure that produces grid-fit bitmaps only.
        /// </param>
        /// <param name="height">bar code height in inches</param>
        /// <param name="dpi">bitmap dpi (dots per inch)</param>
        /// <returns>
        ///     A bitmap with the barcode in it. No border whatsoever is added, 
        ///     the bar code fills the entire bitmap. The bar code is drawn
        ///     horizontally
        /// </returns>
        /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        public static Bitmap Encode(
            string text, int xDim, float height, float dpi)
        {
            return Encode(text, xDim, height, dpi, 0, 0);
        }

        /// <summary>
        ///     Encodes the text (digits only) using Code 128 C and creates a 
        ///     bitmap according to specified parameters.
        /// </summary>
        /// <param name="text">the text to encode (digits only)</param>
        /// <param name="xDim">
        ///     x-dimension in pixels. By using pixels here, the user is forced 
        ///     to provide a measure that produces grid-fit bitmaps only.
        /// </param>
        /// <param name="height">bar code height in inches</param>
        /// <param name="dpi">bitmap dpi (dots per inch)</param>
        /// <param name="leftMargin">
        ///     The left margin (in pixels, unused space)
        /// </param>
        /// <param name="rightMargin">
        ///     The right margin (in pixels, unused space)
        /// </param>
        /// <returns>
        ///     A bitmap with the barcode in it. No border whatsoever is
        ///     added, the bar code fills the entire bitmap. The bar code is 
        ///     drawn horizontally.
        /// </returns>
        /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        public static Bitmap Encode(
            string text, 
            int xDim, 
            float height, 
            float dpi, 
            int leftMargin, 
            int rightMargin)
        {
            int[] digits = GetCodewords(text);
            int barHgt = Convert.ToInt32(Math.Floor(dpi * height)),
                barWdt = GetBarWidth(digits, xDim) + leftMargin + rightMargin;

            Bitmap barcode = DrawBarcode(
                digits, barWdt, barHgt, xDim, dpi, leftMargin);
            return barcode;
        }

        #endregion
    }
}