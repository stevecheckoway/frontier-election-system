// -----------------------------------------------------------------------------
// <copyright file="EntrySetUpdate.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the EntrySetUpdate class.
// </summary>
// <revision revisor="dev11" date="3/3/2009 2:12:46 PM" version="1.0.0.0">
//     File Created
// </revision>
// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
//     File modified
// </revision> 
// -----------------------------------------------------------------------------

namespace Sequoia.Ems.Data.Custom
{
    #region Using directives

    using System;
    using System.Collections.Generic;
    using System.Data;

    #endregion

    /// <summary>
    ///	    EntrySetUpdate is a class for updating entry sets
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev11" date="3/3/2009 2:12:46 PM" version="1.0.0.0">
    ///     Class created.
    /// </revision>
    /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
    ///     Formatting changes
    /// </revision>
    public class EntrySetUpdate
    {
        #region Constants

        #endregion

        #region Fields

        /// <summary>
        ///     this is the stored procedure that is executed to save the 
        ///     changes to the database
        /// </summary>
        private string sqlStoredProc;

        /// <summary>
        ///     these are the parameters needed by the stored procedure
        ///     See <see cref="sqlStoredProc"/>
        /// </summary>
        private Dictionary<string, SqlDbType> sqlColumns;

        #endregion
        
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="EntrySetUpdate"/> class.
        ///     NOTE: this class works under the assumption that updates to an
        ///     entry set affect only 1 table and that every item in the table
        ///     is updated at the same time, meaning that all columns on that
        ///     table are present in the entry set. For entry sets coming from
        ///     columns from different tables, a different approach has to be
        ///     taken, like using a Dictionary{Enum, EntrySetUpdate}
        ///     where each column has an independent EntrySetUpdate, with the
        ///     caveat that possibly not every column needed to perform the
        ///     update on the corresponding table is available on the entry set
        /// </summary>
        /// <param name="sqlStoredProc">The SQL stored proc.</param>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="3/3/2009" version="1.0.0.0">
        /// Member Created
        /// </revision>
        public EntrySetUpdate(string sqlStoredProc)
        {
            this.sqlStoredProc = sqlStoredProc;
            this.sqlColumns = new Dictionary<string, SqlDbType>();
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the stored procedure that is executed to save the changes
        ///     to the database
        /// </summary>
        /// <value>The SQL update.</value>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="3/3/2009" version="1.0.8.1401">
        ///     Member Created
        /// </revision>
        public string SqlStoredProc
        {
            get
            {
                return this.sqlStoredProc;
            }
        }

        /// <summary>
        ///     Gets the parameters needed by the stored procedure. Each 
        ///     parameter represents a column. See <see cref="sqlStoredProc"/>.
        /// </summary>
        /// <value>The SQL params.</value>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="3/3/2009" version="1.0.8.1401">
        ///     Member Created
        /// </revision>
        public Dictionary<string, SqlDbType> SqlParams
        {
            get
            {
                return this.sqlColumns;
            }
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
