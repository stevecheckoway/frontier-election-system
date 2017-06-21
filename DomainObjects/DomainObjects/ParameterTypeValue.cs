// -----------------------------------------------------------------------------
// <copyright file="ParameterTypeValue.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the ParameterTypeValue class.
// </summary>
// <revision revisor="dev14" date="6/15/2009" version="1.0.13.11">
//     File Created
// </revision>
// -----------------------------------------------------------------------------

namespace Sequoia.DomainObjects
{
    /// <summary>
    ///     ParameterTypeValue is a class that maps a parameter type and its
    ///     possible values to the options and application parameters for
    ///     which values can be set
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev14" date="6/15/2009" version="1.0.13.11">
    ///     Class created.
    /// </revision>
    public class ParameterTypeValue
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        /// <value>The Parameter type id.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="6/15/2009" version="1.0.13.11">
        ///     Member Created
        /// </revision>
        public int Id
        {
            get; set;
        }

        /// <summary>
        ///     Gets or sets the type of the parameter.
        /// </summary>
        /// <value>The type of the parameter.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="6/15/2009" version="1.0.13.11">
        ///     Member Created
        /// </revision>
        public ParameterType ParameterType
        {
            get; 
            set;
        }

        /// <summary>
        ///     Gets or sets the minimum.
        /// </summary>
        /// <value>The minimum.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="6/15/2009" version="1.0.13.11">
        ///     Member Created
        /// </revision>
        public decimal Minimum
        {
            get; 
            set;
        }

        /// <summary>
        ///     Gets or sets the maximum.
        /// </summary>
        /// <value>The maximum.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="6/15/2009" version="1.0.13.11">
        ///     Member Created
        /// </revision>
        public decimal Maximum
        {
            get; 
            set;
        }

        /// <summary>
        ///     Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="6/15/2009" version="1.0.13.11">
        ///     Member Created
        /// </revision>
        public string Value
        {
            get; 
            set;
        }

        /// <summary>
        ///     Gets the values.
        /// </summary>
        /// <value>The values.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="6/15/2009" version="1.0.13.11">
        ///     Member Created
        /// </revision>
        public string[] Values
        {
            get
            {
                var splitChar = new[] { '|' };
                return this.Value.Split(splitChar);
            }
        }

        #endregion
    }
}
