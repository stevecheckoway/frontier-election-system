// -----------------------------------------------------------------------------
// <copyright file="ParameterFile.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2008 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the ParameterFile class.
// </summary>
// <revision revisor="dev16" date="12/22/2008 8:14:37 PM" version="1.0.?.0">
//     File Created
// </revision>  
// -----------------------------------------------------------------------------

namespace Sequoia.DomainObjects
{
    #region Using directives

    using System;
    using System.Xml.Serialization;

    #endregion

    /// <summary>
    ///     Structure to point to parameter file
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev16" date="12/22/2008" version="1.0.0.0">
    ///     Class created.
    /// </revision>
    [Serializable]
    [XmlRoot("ParameterFile")]
    public class ParameterFile : DomainObject
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ParameterFile"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev16" date="12/22/2008" version="1.0.0.0">
        /// Member Created
        /// </revision>
        public ParameterFile() : this(string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ParameterFile"/> class.
        /// </summary>
        /// <param name="name">The name of the file.</param>
        /// <externalUnit/>
        /// <revision revisor="dev16" date="12/22/2008" version="1.0.0.0">
        /// Member created.
        /// </revision>
        public ParameterFile(string name)
        {
            this.Name = name;
        }

        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name of the parameter file.</value>
        /// <externalUnit/>
        /// <revision revisor="dev16" date="12/23/2008" version="1.0.?.0">
        /// Member created.
        /// </revision>
        [XmlAttribute("Name")]
        public string Name
        {
            get;
            set;
        }
        #endregion
    }
}
