// -----------------------------------------------------------------------------
// <copyright file="Pdf417Result.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the Pdf417Result class.
// </summary>
// <revision revisor="dev13" date="11/19/2009" version="1.1.3.7">
//     Added documentation header
// </revision>
// -----------------------------------------------------------------------------

namespace Sequoia.Ems.Imaging.BarCode
{
    /// <summary>
    ///     This class encapsulates the result of a PDF417 bar code providing 
    ///     additional data about the encoding process. This is useful when 
    ///     trying to produce a bar code with physical characteristics not part 
    ///     of the encoding process.
    /// </summary>
    /// <revision revisor="dev13" date="11/19/2009" version="1.1.3.7">
    ///     Added documentation header
    /// </revision>
    public class Pdf417Result
    {
        #region Fields

        /// <summary>
        ///     This is the encoded string. It is assumed that a special font is 
        ///     used for rendering the actual bar code.
        /// </summary>
        private string encoded;

        /// <summary>
        ///     # of rows of the bar code
        /// </summary>
        private int rows;

        /// <summary>
        ///     # of data columns of the bar code. Row indicator columns and 
        ///     start/stop columns are not accounted for here.
        /// </summary>
        private int cols;

        /// <summary>
        ///     # of padding CW. This is the number of dummy data that needs to 
        ///     be added to the actual bar code data in order to fill the entire 
        ///     area of the bar code. This happens since PDF417 is not a linear 
        ///     bar code but a 2-D bar code, requiring complete 
        ///     rows and columns.
        /// </summary>
        private int cwPadding;

        /// <summary>
        ///     This is the suggested number of column to minimize the addtional
        ///     padding CWs that need to be appended to the data of the bar code
        /// </summary>
        private int suggCols;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Pdf417Result"/> class.
        /// </summary>
        /// <param name="encoded">
        ///     This is the encoded text that simply requires the special font 
        ///     [PDF417] in order to render the bar code.
        /// </param>
        /// <param name="cols"># of data columns.</param>
        /// <param name="rows"># of rows.</param>
        /// <param name="padding"># of padding CWs.</param>
        /// <revision revisor="dev13" date="11/19/2009" version="1.1.3.7">
        ///     Added documentation header
        /// </revision>
        public Pdf417Result(string encoded, int cols, int rows, int padding)
        {
            this.encoded = encoded;
            this.cols = cols;
            this.rows = rows;
            this.cwPadding = padding;

            this.suggCols = cols - (this.cwPadding / rows);
        }

        #endregion

        #region Public properties

        /// <summary>
        ///     Gets the encoded.
        /// </summary>
        /// <value>The encoded.</value>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/19/2009" version="1.1.3.7">
        ///     Added documentation header
        /// </revision>
        public string Encoded
        {
            get
            {
                return this.encoded;
            }
        }

        /// <summary>
        ///     Gets the rows.
        /// </summary>
        /// <value>The rows.</value>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/19/2009" version="1.1.3.7">
        ///     Added documentation header
        /// </revision>
        public int Rows
        {
            get
            {
                return this.rows;
            }
        }

        /// <summary>
        ///     Gets the columns.
        /// </summary>
        /// <value>The columns.</value>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/19/2009" version="1.1.3.7">
        ///     Added documentation header
        /// </revision>
        public int Columns
        {
            get 
            {
                return this.cols;
            }
        }

        /// <summary>
        ///     Gets the cw padding.
        /// </summary>
        /// <value>The cw padding.</value>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/19/2009" version="1.1.3.7">
        ///     Added documentation header
        /// </revision>
        public int CwPadding
        {
            get
            {
                return this.cwPadding;
            }
        }

        /// <summary>
        ///     Gets the suggested cols.
        /// </summary>
        /// <value>The suggested cols.</value>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/19/2009" version="1.1.3.7">
        ///     Added documentation header
        /// </revision>
        public int SuggestedCols
        {
            get
            {
                return this.suggCols;
            }
        }

        #endregion
    }
}