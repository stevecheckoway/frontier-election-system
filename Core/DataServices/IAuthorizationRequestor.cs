//-----------------------------------------------------------------------------
// <copyright file="IAuthorizationRequestor.cs" 
//  company="Sequoia Voting Systems">
//     Copyright (c) 2008 Sequoia Voting Systems. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <revision revisor="dev22" date="03/09/2010" version="1.1.7.16">
//     File created
// </revision>
//-----------------------------------------------------------------------------

namespace Sequoia.EMS.Core.DataServices
{
    #region Using Directives

    using System;

    #endregion

    /// <summary>
    /// Defines members common to objects that request authorization (for
    /// themselves or on others' behalf).
    /// </summary>
    /// <remarks>This interface is separate from the <see cref="IDataService"/>
    /// interface because in this way it can be implemented by objects that
    /// contain one or more dataservices, but are not dataservices themselves.
    /// Such objects need to act as "request relayers" for the services they
    /// contain, but in doing so they become authorization requestors, which
    /// is what this interface lets them express.</remarks>
    /// <revision revisor="dev22" date="03/09/2010" version="1.1.7.16">
    /// Interface added.
    /// </revision>
    public interface IAuthorizationRequestor
    {
        /// <summary>
        /// Occurs when the object requires authorization to connect to a
        /// data source.
        /// </summary>
        /// <revision revisor="dev22" date="03/08/2010" version="1.1.7.16">
        /// Member added.
        /// </revision>
        event EventHandler<AuthorizationEventArgs> AuthorizationRequired;

        /// <summary>
        /// Attaches the given object's authorization request to this object's
        /// request, essentially making this object the other's request handler.
        /// </summary>
        /// <param name="requestor">The object to attach to this object.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown by implementors when <paramref name="requestor"/> is null or
        /// the same instance as this object.
        /// </exception>
        /// <remarks>
        /// <para>This method allows the current object to become a
        /// "request relayer" for another object (as long as this object's
        /// request event has a handler attached, of course). This is useful
        /// when objects that require authorization are created inside other
        /// objects that cannot authorize a request themselves.
        /// </para>
        /// <para>The main idea behind this method is that of daisy-chained
        /// requestors, all passing along a request until an end handler is
        /// reached. Note that it is the programmer's responsibility to
        /// a) ensure that an end handler is connected and
        /// b) avoid creating a circular chain.
        /// </para>
        /// <para>To implement this method, simply attach a handler to the
        /// given object's <see cref="AuthorizationRequired"/> event that does
        /// nothing but raise this object's own event with the original event
        /// arguments.
        /// </para></remarks>
        void Attach(IAuthorizationRequestor requestor);
    }
}
