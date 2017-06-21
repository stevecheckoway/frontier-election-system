// -----------------------------------------------------------------------------
// <copyright file="ParameterFileList.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2008 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the ParameterFileList class.
// </summary>
// <revision revisor="dev16" date="12/22/2008" version="1.0.0.0">
//     File Created
// </revision>  
// -----------------------------------------------------------------------------

namespace Sequoia.DomainObjects
{
    #region Using directives

    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    #endregion

    /// <summary>
    ///     List of files with parameters
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev16" date="12/22/2008" version="1.0.0.0">
    ///     Class created.
    /// </revision>
    [Serializable]
    [XmlRoot("ParameterFiles")]
    public class ParameterFileList : DomainObjectList<ParameterFile>
    {
        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ParameterFileList"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev16" date="12/22/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision> 
        public ParameterFileList()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ParameterFileList"/> class.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="10/7/2009" version="1.1.1.7">
        ///     Member Created
        /// </revision>
        public ParameterFileList(int capacity) : base(capacity)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ParameterFileList"/> class.
        /// </summary>
        /// <param name="parameterFiles">The parameter files.</param>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="10/7/2009" version="1.1.1.7">
        ///     Member Created
        /// </revision>
        public ParameterFileList(IEnumerable<ParameterFile> parameterFiles)
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
