// -----------------------------------------------------------------------------
// <copyright file="BallotStyle.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the BallotStyle class.
// </summary>
// <revision revisor="dev14" date="11/12/2009" version="1.1.2.21">
//     File Created
// </revision>  
// -----------------------------------------------------------------------------

namespace Sequoia.DomainObjects
{
    #region Using directives

    using System;
    using System.Xml.Serialization;
    using Persistence;

    #endregion

    /// <summary>
    ///	    Represents a ballot style, which can be shared by different precincts
    ///     and portions. Different than the <see cref="Ballot" /> class that
    ///     represents a physical ballot.
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev14" date="11/12/2009" version="1.1.2.21">
    ///     Class created.
    /// </revision>
    public class BallotStyle : DomainObject
    {
        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="BallotStyle"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="11/12/2009" version="1.1.2.21">
        ///     Member Created
        /// </revision>	
        public BallotStyle()
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the ballot style id.
        /// </summary>
        /// <value>The ballot style id.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="11/12/2009" version="1.1.2.21">
        ///     Member Created
        /// </revision>
        public int Id
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the name of the style.
        /// </summary>
        /// <value>The name of the ballot style.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="11/12/2009" version="1.1.2.21">
        ///     Member Created
        /// </revision>
        public string Name
        {
            get; 
            set;
        }

        /// <summary>
        ///     Gets or sets the code associated with the style.
        /// </summary>
        /// <value>The code associated with the style.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="11/12/2009" version="1.1.2.21">
        ///     Member Created
        /// </revision>
        public string Code
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the order in which the style should appear in a list.
        /// </summary>
        /// <value>The list order.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="11/12/2009" version="1.1.2.21">
        ///     Member Created
        /// </revision>
        public int ListOrder
        {
            get;
            set;
        }

        #endregion

        #region Public Methods

        #endregion

        #region Public Events

        #endregion

        #region Private Methods

        #endregion
    }
}
