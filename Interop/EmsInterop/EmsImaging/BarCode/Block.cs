// -----------------------------------------------------------------------------
// <copyright file="Block.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the Block class.
// </summary>
// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
//     Added documentation header
// </revision>
// -----------------------------------------------------------------------------

namespace Sequoia.Ems.Imaging.BarCode
{
    #region Using directives

    using System;
    using System.Collections.Generic;
    using System.Text;

    #endregion

    /// <summary>
    ///     this struct gives more meaningful names to a couple of integer 
    ///     properties that are the result of checking the length of the 
    ///     bar code data. 
    /// </summary>
    public struct Pad
    {
        /// <summary>
        ///     This is the amount of CWs that are appended to the data CWs in 
        ///     order to fill all columns and rows of the bar code.
        /// </summary>
        public int Padding;

        /// <summary>
        ///     # of rows of the bar code.
        /// </summary>
        public int Rows;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Pad"/> struct.
        /// </summary>
        /// <param name="_padding"># of padding CWs</param>
        /// <param name="_rows"># of rows of the bar code</param>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        public Pad(int _padding, int _rows)
        {
            this.Padding = _padding;
            this.Rows = _rows;
        }
    }
    
    /// <summary>
    ///     Data is encoded into a PDF417 bar code by blocks in order to 
    ///     optimize the size of the bar code. Each block is encoded using one 
    ///     of 3 different algorithms (text, numeric and binary) in order to 
    ///     reduce the size of the bar code.
    /// </summary>
    class Block
    {
        /// <summary>
        ///     Represents the number of characters in this block. It is public 
        ///     since blocks are merged and data is updated externally
        /// </summary>
        public int Count = 1;

        /// <summary>
        ///      Either one of:
        ///     900 (text mode)
        ///     901 (binary mode)
        ///     902 (numeric mode)
        ///     913 (binary mode only for next CW)
        ///     It is public since blocks are merged and data is 
        ///     updated externally
        /// </summary>
        public int Mode = 900;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Block"/> class.
        /// </summary>
        /// <param name="_mode">The _mode.</param>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        public Block(int _mode)
        {
            // every block is created with at least 1 character
            this.Count = 1;
            this.Mode = _mode;
        }
    }
}