//-----------------------------------------------------------------------------
// <copyright file="ExceptionManager.cs" 
//  company="Sequoia Voting Systems">
//     Copyright (c) 2008 Sequoia Voting Systems. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
//     File created
// </revision>
// <revision revisor="dev13" date="10/9/2008" version="1.0.0.0">
//     File modified
// </revision>
//-----------------------------------------------------------------------------

namespace Sequoia.EMS.Core.Managers
{
    #region Using directives

    using System;
    using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

    #endregion

    /// <Description>
    ///     <Purpose>
    ///         ExceptionManager provides exception handling and logging 
    ///         services for the framework.
    ///     </Purpose>
    ///     <Design></Design>
    /// </Description>
    /// <summary>
    ///     ExceptionManager provides exception handling and logging services 
    ///     for the framework.
    /// </summary>
    /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
    ///     Class created
    /// </revision>
    public sealed class ExceptionManager
    {
        /// <summary>
        ///     Prevents a default instance of the <see cref="ExceptionManager"/> class from being created.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        private ExceptionManager()
        {
        }

        /// <summary>
        ///     Gets the singleton instance of the ExceptionManager.
        /// </summary>
        /// <value>The instance of the ExceptionManager.</value>
        /// <externalUnit cref="Nested"/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public static ExceptionManager Instance
        {
            get
            {
                // return the nested class used to ensure it is the only 
                // instance
                return Nested.Instance;
            }
        }

        #region Methods

        /// <summary>
        ///     Processes the policy.  Policies are set up in the config file.  
        /// </summary>
        /// <param name="exceptionToHandle">The exception to handle.</param>
        /// <param name="policyName">Name of the policy.</param>
        /// <returns>
        ///     <c>true</c> if the exception is handled properly; 
        ///     otherwise <c>false</c>.</returns>
        /// <externalUnit cref="ExceptionPolicy"/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public bool ProcessPolicy(
            Exception exceptionToHandle, 
            string policyName)
        {
            // Handle the exception with the given policy
            return
                ExceptionPolicy.HandleException(exceptionToHandle, policyName);
        }

        #endregion

        #region Nested Class

        /// <summary>
        ///     A private class used to create the singleton.
        /// </summary>
        private class Nested
        {
            /// <summary>
            ///     Initializes static members of the <see cref="Nested" /> class. Explicit 
            ///     constructor to tell C# compiler not to mark type as 
            ///     beforefieldinit.
            /// </summary>
            /// <externalUnit/>
            /// <revision revisor="dev13" date="10/9/2008" version="1.0.0.0">
            ///     Member Created
            /// </revision>
            static Nested()
            {
            }

            /// <summary>
            ///     ensures this is a singleton, thread safe
            /// </summary>
            internal static readonly ExceptionManager Instance =
                new ExceptionManager();
        }

        #endregion
    }

}
