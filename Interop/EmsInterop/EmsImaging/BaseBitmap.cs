// -----------------------------------------------------------------------------
// <copyright file="BaseBitmap.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the BaseBitmap class.
// </summary>
// <revision revisor="dev11" date="2/11/2009" version="1.0.0.0">
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
    using System.Drawing.Imaging;

    #endregion

    /// <summary>
    ///	    Base bitmap methods
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev11" date="2/11/2009" version="1.0.0.0">
    ///     Class created.
    /// </revision>
    /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
    ///     Formatting changes.
    /// </revision>
    public class BaseBitmap
    {
        #region Protected Methods

        /// <summary>
        ///     Clears the bitmap.
        /// </summary>
        /// <param name="data">The bitmap data.</param>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="10/24/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        protected static void ClearBitmap(BitmapData data)
        {
            FillRectangle(data, 0, 0, data.Width, data.Height, false);
        }

        /// <summary>
        ///     Clears the bitmap.
        /// </summary>
        /// <param name="data">The bitmap data.</param>
        /// <param name="black">if set to <c>true</c> [black].</param>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="2/13/2009" version="1.0.6.9">
        /// Member Created</revision>
        protected static void ClearBitmap(BitmapData data, bool black)
        {
            FillRectangle(data, 0, 0, data.Width, data.Height, black);
        }

        /// <summary>
        ///     Fills the rectangle.
        /// </summary>
        /// <param name="data">The bitmap data.</param>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="black">if set to <c>true</c> [black].</param>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="10/24/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        protected static void FillRectangle(
            BitmapData data, 
            int x, 
            int y,
            int width, 
            int height, 
            bool black)
        {
            int x2 = x + width - 1,
                y2 = y + height - 1;
            for (int i = x; i <= x2; i = i + 1)
            {
                for (int j = y; j <= y2; j = j + 1)
                {
                    SetPixel(data, i, j, black);
                }
            }
        }

        /// <summary>
        ///     Sets the pixel.
        /// </summary>
        /// <param name="data">The bitmap data.</param>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <param name="black">if set to <c>true</c> [black].</param>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="10/24/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        protected unsafe static void SetPixel(
            BitmapData data, int x, int y, bool black)
        {
            byte* p = (byte*) data.Scan0.ToPointer();
            int index = y * data.Stride + (x >> 3);
            byte mask = (byte) (0x80 >> (x & 0x7));
            if (black)
            {
                p[index] &= (byte)(mask ^ 0xff);
            }
            else
            {
                p[index] |= mask;
            }
        }

        #endregion
    }
}
