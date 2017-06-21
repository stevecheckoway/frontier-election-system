// -----------------------------------------------------------------------------
// <copyright file="Codec.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the Codec class.
// </summary>
// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
//     Added documentation header
// </revision>
// -----------------------------------------------------------------------------

namespace Sequoia.Ems.Imaging
{
    #region Using directives

    using System;
    using System.Drawing.Imaging;

    #endregion

    /// <summary>
    ///     Image codec class
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
    ///     Added documentation header
    /// </revision>
    public class Codec
    {
        /// <summary>
        ///     Gets the image encoder.
        /// </summary>
        /// <param name="mimeType">Type of the MIME.</param>
        /// <returns>
        ///     An <see cref="ImageCodecInfo" /> object containing the 
        ///     image encoder.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        public static ImageCodecInfo GetImageEncoder(string mimeType)
        {
            return GetImageEncoder(mimeType, ImageCodecInfo.GetImageEncoders());
        }

        /// <summary>
        ///     Gets the image encoder.
        /// </summary>
        /// <param name="mimeType">Type of the MIME.</param>
        /// <param name="codecs">The codecs.</param>
        /// <returns>
        ///     An <see cref="ImageCodecInfo" /> object containing the 
        ///     image encoder.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        public static ImageCodecInfo GetImageEncoder(
            string mimeType, 
            ImageCodecInfo[] codecs)
        {
            ImageCodecInfo encoder = null;
            int i = 0;

            while ((i < codecs.Length) && (encoder == null))
            {
                if (codecs[i].MimeType == mimeType)
                {
                    encoder = codecs[i];
                }
                else
                {
                    i = i + 1;
                }
            }

            return encoder;
        }
    }
}
