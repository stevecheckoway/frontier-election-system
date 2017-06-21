// -----------------------------------------------------------------------------
// <copyright file="MachineOption.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2008 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the MachineOption class.
// </summary>
// <revision revisor="dev16" date="12/22/2008" version="1.0.0.0">
//     File Created
// </revision>  
// <revision revisor="dev06" date="01/09/2009" version="1.0.0.0">
//     File Modified.
// </revision>
// <revision revisor="dev14" date="6/15/2009" version="1.0.13.11">
//     File Modified.
// </revision>
// <revision revisor="dev14" date="6/17/2009" version="1.0.13.13">
//     File Modified
// </revision>   
// -----------------------------------------------------------------------------

namespace Sequoia.DomainObjects
{
    #region Using directives

    using System;
    using System.Xml.Serialization;

    #endregion

    /// <summary>
    ///     Class for Machine Parameters- control machine (tabulator) behavior
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev16" date="12/22/2008" version="1.0.0.0">
    ///     Class created.
    /// </revision>
    /// <revision revisor="dev06" date="1/9/2009" version="1.0.0.0">
    ///     Class modified: added id and description properties. Added new 
    ///     constructor taking id as well.
    /// </revision>
    /// <revision revisor="dev14" date="6/15/2009" version="1.0.13.11">
    ///     Added the [ParameterType]
    /// </revision>
    /// <revision revisor="dev14" date="6/17/2009" version="1.0.13.13">
    ///     Implements <see cref="ISysIxParam" />
    /// </revision> 
    [Serializable]
    [XmlRoot("MachineOption")]
    public class MachineOption : DomainObject, ISysIxParam
    {
        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="MachineOption"/> class.
        /// </summary>
        /// <externalUnit cref="MachineOption(int, string, string)"/>
        /// <revision revisor="dev16" date="12/22/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision> 
        public MachineOption() : this(0, string.Empty, string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MachineOption"/> class.
        /// </summary>
        /// <param name="name">The name of the configuration option.</param>
        /// <param name="value">The value.</param>
        /// <externalUnit cref="MachineOption(int, string, string)"/>
        /// <revision revisor="dev16" date="12/22/2008" version="1.0.0.0">
        /// Member created.
        /// </revision>
        /// <revision revisor="dev16" date="12/22/2008" version="1.0.0.0">
        ///  Chained to new constructor.
        /// </revision>
        public MachineOption(string name, string value) : this(0, name, value)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MachineOption"/> class.
        /// </summary>
        /// <param name="id">The Option id.</param>
        /// <param name="name">The Option name.</param>
        /// <param name="value">The value.</param>
        /// <externalUnit/>
        /// <revision revisor="dev06" date="1/9/2009" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public MachineOption(int id, string name, string value)
        {
            this.Name = name;
            this.Value = value;
            this.Id = id;
            this.Description = string.Empty;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        /// <externalUnit/>
        /// <revision revisor="dev06" date="1/9/2009" version="1.0.0.0">Member Created</revision>
        [XmlIgnore]
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The Option id.</value>
        /// <externalUnit/>
        /// <revision revisor="dev06" date="1/9/2009" version="1.0.0.0">Member Created</revision>
        [XmlIgnore]
        public int Id
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The Option name.</value>
        /// <externalUnit/>
        /// <revision revisor="dev16" date="12/22/2008" version="1.0.?.0">
        /// Member created.
        /// </revision>
        [XmlAttribute("Name")]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        /// <externalUnit/>
        /// <revision revisor="dev16" date="12/22/2008" version="1.0.?.0">
        /// Member created.
        /// </revision>
        [XmlAttribute("Value")]
        public string Value
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the parameter value id.
        /// </summary>
        /// <value>The parameter value id.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="6/15/2009" version="1.0.13.11">
        ///     Member Created
        /// </revision>
        [XmlAttribute("ParameterValueId")]
        public int ParameterValueId
        {
            get; 
            set;
        }

        /// <summary>
        /// Gets or sets the type of the parameter used to determine input 
        /// display and allowable values for the option
        /// </summary>
        /// <value>The Parameter type.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="6/15/2009" version="1.0.13.11">
        ///     Member Created
        /// </revision>
        [XmlIgnore]
        public ParameterTypeValue ParameterValue
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
