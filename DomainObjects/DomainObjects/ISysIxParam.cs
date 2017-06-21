// -----------------------------------------------------------------------------
// <copyright file="ISysIxParam.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the ISysIxParam class.
// </summary>
// <revision revisor="dev14" date="6/17/2009" version="1.0.13.13">
//     File Created
// </revision>
// -----------------------------------------------------------------------------

namespace Sequoia.DomainObjects
{
    /// <summary>
    ///     ISysIxParam is an interface which serves as a contract between
    ///     data that serve as properties in the system and the display of that
    ///     data based on the type of parameter it represents.
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev14" date="6/17/2009" version="1.0.13.13">
    ///     Interface created.
    /// </revision>
    public interface ISysIxParam
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the value of the parameter.  The value is always 
        ///     saved as a string in the database, but can be cast to 
        ///     the appropriate type
        /// </summary>
        /// <value>The value.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="6/17/2009" version="1.0.13.13">
        ///     Member Created
        /// </revision>
        string Value { get; set; }

        /// <summary>
        ///     Gets or sets the parameter value id.
        /// </summary>
        /// <value>The parameter value id.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="6/17/2009" version="1.0.13.13">
        ///     Member Created
        /// </revision>
        int ParameterValueId { get; set; }

        /// <summary>
        ///     Gets or sets the <see cref="ParameterTypeValue"/> object, which
        ///     holds the <see cref="ParameterType"/> the allowable values for 
        ///     the parameter type.
        /// </summary>
        /// <value>The parameter value.</value>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="6/17/2009" version="1.0.13.01">
        ///     Member Created
        /// </revision>
        ParameterTypeValue ParameterValue { get; set; }

        #endregion
    }
}
