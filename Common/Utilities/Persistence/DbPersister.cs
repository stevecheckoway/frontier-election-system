// -----------------------------------------------------------------------------
// <copyright file="DbPersister.cs" company="Sequoia Voting Systems" >
//     Copyright (c) 2008 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the DbPersister class.
// </summary>
// <revision revisor="dev06" date="12/12/2008 3:11:32 PM" version="1.0.?.0">
//     File Created
// </revision>
// <revision revisor="dev05" date="03/19/09" version="1.0.9.3">
//     File modified.
// </revision>
// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
//     File modified.
// </revision>
// -----------------------------------------------------------------------------

namespace Sequoia.Utilities.Persistence
{
    #region Using directives

    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml.Serialization;

    using Sequoia.DomainObjects;
    using Sequoia.DomainObjects.Persistence;
    using Sequoia.Utiltities.Persistence;
    
    #endregion

    /// <summary>
    ///	    Database persistence helper class
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev06" date="12/12/2008" version="1.0.0.0">
    ///     Class created.
    /// </revision>
    /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
    ///     Formatting changes
    /// </revision>
    public class DbPersister : IPersister
    {
        #region Fields

        /// <summary>
        ///     the persistible item
        /// </summary>
        private IPersistible persistibleItem = null;

        /// <summary>
        ///     the data access object
        /// </summary>
        private IDataAccessObject<IPersistible> dao = null;

        #endregion
        
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="DbPersister"/> class.
        /// </summary>
        /// <param name="dao">The data access object.</param>
        /// <externalUnit/>
        /// <revision revisor="dev06" date="12/12/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public DbPersister(IDataAccessObject<IPersistible> dao)
        {
            //this.persistibleItem = persistibleItem;
            this.dao = dao;
        }

        #endregion

        #region Public Properties

        #endregion

        #region Public Methods

        /// <summary>
        ///     Loads the item to be recreated.
        /// </summary>
        /// <param name="item">The persistible item.</param>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        public void LoadItemToBeRecreated(IPersistible item)
        {
            this.persistibleItem = item;
        }

        #endregion

        #region Public Events

        #endregion

        #region Private Methods

        #endregion

        #region IPersister Members

        /// <summary>
        ///     Persists the specified item.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="item">The item to persist.</param>
        /// <returns>
        ///     An <see cref="OperationResult" /> containing the results
        ///     of the operation.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        OperationResult IPersister.Persist<T>(T item)
        {
            // get result from db save operation
            var result = this.dao.Save(item);

            return result;
        }

        /// <summary>
        ///     Recreates this instance.
        /// </summary>
        /// <typeparam name="T">The type of the item to recreate.</typeparam>
        /// <returns>
        ///     The item typed based on the passed-in type.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        T IPersister.Recreate<T>()
        {
            // create an item we are deserializing and set to its default value
            // - could be null
            T item = default(T);

            // make sure the data is not empty
            if (!string.IsNullOrEmpty(this.persistibleItem.SerializedData))
            {
                // read the serialized data
                using (var reader =
                    new StringReader(this.persistibleItem.SerializedData))
                {
                    // create xml serializer to transform sample xml into 
                    // cartridge store
                    var serializer = new XmlSerializer(typeof(T));

                    // recreate the cartridge store object from the xml data
                    item = (T) serializer.Deserialize(reader);
                }
            }

            // make sure the item is not null before setting the persister
            if(item != null)
            {
                item.SetPersister(this);
            }

            return item;
        }

        #endregion
    }
}
