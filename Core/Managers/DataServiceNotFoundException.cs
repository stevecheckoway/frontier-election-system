//-----------------------------------------------------------------------------
// <copyright file="DataServiceNotFoundException.cs" 
//  company="Sequoia Voting Systems">
//     Copyright (c) 2008 Sequoia Voting Systems. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
//     File created
// </revision>
//-----------------------------------------------------------------------------

namespace Sequoia.EMS.Core.Managers
{
    #region Using directives
    
    using System;

    #endregion

    /// <summary>
    ///     DataServiceNotFoundException is a custom exception thrown when the 
    ///     data service cannot be found.
    /// </summary>
    /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
    ///     Class created
    /// </revision>
    public class DataServiceNotFoundException : Exception
    {
        #region Fields

        /// <summary>
        ///     param for the data service name
        /// </summary>
        private string dataServiceName = string.Empty;
        
        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="DataServiceNotFoundException"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public DataServiceNotFoundException()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DataServiceNotFoundException"/> class.
        /// </summary>
        /// <param name="dataServiceName">Name of the data service.</param>
        /// <externalUnit cref="DataServiceName"/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public DataServiceNotFoundException(string dataServiceName)
        {
            this.DataServiceName = dataServiceName;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the name of the data service.
        /// </summary>
        /// <value>The name of the data service.</value>
        /// <externalUnit cref="dataServiceName"/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public string DataServiceName
        {
            get { return this.dataServiceName; }
            set { this.dataServiceName = value; }
        }

        #endregion

        #region Methods

        #endregion
    }
}
