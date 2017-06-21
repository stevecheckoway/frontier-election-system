// -----------------------------------------------------------------------------
// <copyright file="ICommunicationDevice.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the ICommunicationDevice interface.
// </summary>
// <revision revisor="dev01" date="2/2/2009 1:41:48 PM" version="1.0.5.14">
//     File Created
// </revision>  
// <revision revisor="dev05" date="9/9/2009" version="1.0.17.2">
//     File modified.
// </revision>
// <revision revisor="dev13" date="11/17/2009" version="1.0.17.2">
//     File modified.
// </revision>
// -----------------------------------------------------------------------------

namespace Sequoia.Utilities.Communication
{
    #region Using directives

    using Sequoia.DomainObjects;

    #endregion

    /// <summary>
    ///     Delegate for the connection handler
    /// </summary>
    /// <param name="connected"><c>true</c> if connected.</param>
    /// <revision revisor="dev01" date="2/2/2009" version="1.0.5.14">
    ///     Member Created
    /// </revision>
    public delegate void ConnectionHandler(bool connected);
    
    /// <summary>
    ///	    ICommunciationDevice is the interface for a communication device
    /// </summary>
    /// <revision revisor="dev01" date="2/2/2009" version="1.0.14.0">
    ///     Interface created.
    /// </revision>
    public interface ICommunicationDevice
    {
        #region Events

        /// <summary>
        ///     Occurs when [got connected].
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="2/2/2009" version="1.0.5.14">
        ///     Member Created
        /// </revision>
        event ConnectionHandler GotConnected;

        /// <summary>
        ///     Occurs when [got disconnected].
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="2/2/2009" version="1.0.5.14">
        ///     Member Created
        /// </revision>
        event ConnectionHandler GotDisconnectedConnected;

        #endregion
        
        #region Properties

        /// <summary>
        ///     Gets a value indicating whether this 
        ///     <see cref="ICommunicationDevice"/> is connected.
        /// </summary>
        /// <value><c>true</c> if connected; otherwise, <c>false</c>.</value>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="2/2/2009" version="1.0.5.14">
        ///     Member Created
        /// </revision>
        bool Connected { get;}

        #endregion

        #region Methods

        /// <summary>
        ///     Connects this instance.
        /// </summary>
        /// <returns>
        ///     An <see cref="OperationResult" /> containing the results
        ///     of the operation.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="2/2/2009" version="1.0.5.14">
        ///     Member Created
        /// </revision>
        OperationResult Connect();

        /// <summary>
        ///     Disconnects this instance.
        /// </summary>
        /// <returns>
        ///     An <see cref="OperationResult" /> containing the results
        ///     of the operation.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="2/2/2009" version="1.0.5.14">
        ///     Member Created
        /// </revision>
        OperationResult Disconnect();

        #endregion
    }
}
