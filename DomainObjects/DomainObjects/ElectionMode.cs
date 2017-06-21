// -----------------------------------------------------------------------------
// <copyright file="ElectionMode.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the ElectionMode class.
// </summary>
// <revision revisor="dev06" date="4/19/2009" version="1.0.11.7">
//     File Created
// </revision>
// -----------------------------------------------------------------------------

namespace Sequoia.DomainObjects
{
    /// <summary>
    ///     ElectionMode is a class that represents an election mode - like 
    ///     pre-lat or official.
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev06" date="4/19/2009" version="1.0.11.7">
    ///     Class created.
    /// </revision>
    public class ElectionMode
    {
        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ElectionMode"/> class.
        /// </summary>
        /// <externalUnit cref="ElectionMode(int, string, bool)"/>
        /// <revision revisor="dev06" date="4/19/2009" version="1.0.11.7">
        ///     Member Created
        /// </revision>
        public ElectionMode()
            : this(0, string.Empty, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ElectionMode"/> class.
        /// </summary>
        /// <param name="id">The id of the election mode.</param>
        /// <param name="name">The name of the election mode.</param>
        /// <param name="isDefault">if set to <c>true</c> [is default].</param>
        /// <externalUnit cref="Id"/>
        /// <externalUnit cref="Name"/>
        /// <revision revisor="dev06" date="4/19/2009" version="1.0.11.7">
        /// Member Created
        /// </revision>
        public ElectionMode(int id, string name, bool isDefault)
        {
            // set values
            this.Id = id;
            this.Name = name;
            this.IsDefault = isDefault;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id of the mode as stored in the db.</value>
        /// <externalUnit/>
        /// <revision revisor="dev06" date="4/19/2009" version="1.0.11.7">
        ///     Member Created
        /// </revision>
        public int Id
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name/label value of the mode.</value>
        /// <externalUnit/>
        /// <revision revisor="dev06" date="4/19/2009" version="1.0.11.7">
        ///     Member Created
        /// </revision>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is default.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is default; otherwise, <c>false</c>.
        /// </value>
        /// <externalUnit/>
        /// <revision revisor="dev06" date="4/19/2009" version="1.0.11.7">
        ///     Member Created
        /// </revision>
        public bool IsDefault
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
