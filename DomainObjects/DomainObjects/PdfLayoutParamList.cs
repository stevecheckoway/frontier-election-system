// -----------------------------------------------------------------------------
// <copyright file="PdfLayoutParamList.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the PdfLayoutParamList class.
// </summary>
// <revision revisor="dev16" date="1/24/2009" version="1.0.5.5">
//     File Created
// </revision>  
// -----------------------------------------------------------------------------

namespace Sequoia.DomainObjects
{
    #region Using directives

    using System;
    using System.Collections.Generic;

    #endregion

    /// <summary>
    ///     PdfLayoutParamList is a <see cref="List{T}" /> of <see cref="PdfLayoutParam" /> objects. 
    /// </summary>
    /// <externalUnit cref="PdfLayoutParam"/>
    /// <revision revisor="dev16" date="1/24/2009" version="1.0.5.5">
    ///     Class created.
    /// </revision>
    public class PdfLayoutParamList : List<PdfLayoutParam>
    {
        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="PdfLayoutParamList"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev16" date="1/24/2009" version="1.0.5.5">
        ///     Member Created
        /// </revision>
        public PdfLayoutParamList()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="PdfLayoutParamList"/> class.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        /// <externalUnit cref="List{T}(int)"/>
        /// <revision revisor="dev16" date="1/24/2009" version="1.0.5.5">
        ///     Member Created
        /// </revision>
        public PdfLayoutParamList(int capacity)
            : base(capacity)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="PdfLayoutParamList"/> class.
        /// </summary>
        /// <param name="pdfLayoutParams">The PDF layout params.</param>
        /// <externalUnit cref="PdfLayoutParam"/>
        /// <externalUnit cref="List{T}(IEnumerable{T})"/>
        /// <externalUnit cref="IEnumerable{T}"/>
        /// <revision revisor="dev16" date="1/24/2009" version="1.0.5.5">
        ///     Member Created
        /// </revision>
        public PdfLayoutParamList(IEnumerable<PdfLayoutParam> pdfLayoutParams)
            : base(pdfLayoutParams)
        {
        }

        #endregion

        #region Public Properties

        #endregion

        #region Public Methods

        #endregion

        #region Public Events

        #endregion

        #region Private Methods

        #endregion
    }
}
