// -----------------------------------------------------------------------------
// <copyright file="MemoryInputStream.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the MemoryInputStream class.
// </summary>
// <revision revisor="dev13" date="11/19/2009" version="1.1.3.7">
//     Added documentation header
// </revision>
// -----------------------------------------------------------------------------

namespace Sequoia.Ems.Interop.Java
{
    #region Using directives

    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;

    using java.io;

    using Sequoia.Ems.Imaging;

    #endregion

    /// <summary>
    ///     Class for the memory input stream
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev13" date="11/19/2009" version="1.1.3.7">
    ///     Added documentation header
    /// </revision>
    public class MemoryInputStream : InputStream
    {
        #region Fields

        /// <summary>
        ///     param for the stream
        /// </summary>
        private MemoryStream stream;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="MemoryInputStream"/> class.
        /// </summary>
        /// <param name="memoryStream">The memory stream.</param>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/19/2009" version="1.1.3.7">
        ///     Added documentation header
        /// </revision>
        public MemoryInputStream(MemoryStream memoryStream)
        {
            this.stream = memoryStream;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="MemoryInputStream"/> class.
        /// </summary>
        /// <param name="bitmap">The bitmap.</param>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/19/2009" version="1.1.3.7">
        ///     Added documentation header
        /// </revision>
        public MemoryInputStream(Bitmap bitmap)
        {
            this.stream = new MemoryStream();
            bitmap.Save(this.stream, ImageFormat.Jpeg);
            this.stream.Position = 0;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="MemoryInputStream"/> class.
        /// </summary>
        /// <param name="bitmap">The bitmap.</param>
        /// <param name="quality">The quality.</param>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/19/2009" version="1.1.3.7">
        ///     Added documentation header
        /// </revision>
        public MemoryInputStream(Bitmap bitmap, int quality)
        {
            this.stream = new MemoryStream();
            ImageCodecInfo encoder = Codec.GetImageEncoder("image/jpeg");
            EncoderParameters parameters = new EncoderParameters(1);
            parameters.Param[0] = 
                new EncoderParameter(Encoder.Quality, quality);
            bitmap.Save(this.stream, encoder, parameters);
            this.stream.Position = 0;
        }

        #endregion

        #region Public methods

        /// <summary>
        ///     Reads the byte.
        /// </summary>
        /// <returns>
        ///     The number of bytes written to the buffer.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/19/2009" version="1.1.3.7">
        ///     Added documentation header
        /// </revision>
        public override int read()
        {
            return this.stream.ReadByte();
        }

        /// <summary>
        ///     Reads the specified buffer.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        /// <returns>
        ///     The number of bytes written to the buffer.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/19/2009" version="1.1.3.7">
        ///     Added documentation header
        /// </revision>
        public new int read(byte[] buffer)
        {
            return this.stream.Read(buffer, 0, buffer.Length);
        }

        /// <summary>
        ///     Reads the specified buffer.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        /// <param name="off">The offset.</param>
        /// <param name="len">The length.</param>
        /// <returns>
        ///     The number of bytes written to the buffer.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/19/2009" version="1.1.3.7">
        ///     Added documentation header
        /// </revision>
        public new int read(byte[] buffer, int off, int len)
        {
            return this.stream.Read(buffer, off, len);
        }

        /// <summary>
        ///     Resets this instance.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/19/2009" version="1.1.3.7">
        ///     Added documentation header
        /// </revision>
        public void Reset()
        {
            this.stream.Position = 0;
        }

        #endregion
    }
}
