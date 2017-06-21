//-----------------------------------------------------------------------------
// <copyright file="Contract.cs" 
//  company="Sequoia Voting Systems">
//     Copyright (c) 2008 Sequoia Voting Systems. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
//     File created
// </revision>
//-----------------------------------------------------------------------------

namespace Sequoia.EMS.Core.DesignByContract
{
    #region Using directives

    using System;

    #endregion

    /// <summary>
    ///     <Purpose>
    ///         <para>
    ///             Each method generates an exception if the contract is 
    ///             broken.  The basic premise is that for a given piece of 
    ///             functionality, there are certain conditions that need to be 
    ///             met in order for it to function properly. This class 
    ///             provides a mechanism for an explicit 'contract' to be 
    ///             defined for methods to ensure that these conditionas are 
    ///             met.  Each method in this class is used to determine a 
    ///             specified assertion is true, otherwise, it raises an 
    ///             exception.
    ///         </para>
    ///         <para>
    ///             Yes, the purpose of this class is to make sure contracts 
    ///             are fulfilled, and it they are not, then we throw an 
    ///             exception.  There are three 'layers' of exception catching 
    ///             for any thrown exception in our framework.  The first layer 
    ///             encompasses the handling at any level where we need to take 
    ///             action if necessary to reset data or notify the user.  
    ///             The second layer is in the SnapInShell.  The SnapInShell 
    ///             explicitly catches any unhandled exceptions thrown in a 
    ///             snap-in. Depending on the type of exception, the SnapInShell 
    ///             may allow operation to continue, or will force the snap-in 
    ///             to close so the user can restart the snap in and try what 
    ///             they we re doing again.  The user is always notified of what 
    ///             is happening and prompted for action.  The last level is 
    ///             there in the event that something slips by.  The main 
    ///             application start routine wires up a default exception not 
    ///             handled handler that tells the user the system nust be 
    ///             shutdown and restarted.
    ///         </para>
    ///     </Purpose>
    ///     <Design>
    ///         <para>
    ///             Provides support for Design By Contract
    ///             as described by Bertrand Meyer in his seminal book,
    ///             Object-Oriented Software Construction (2nd Ed) Prentice Hall 
    ///             1997 (See chapters 11 and 12).
    ///         </para>
    ///         <para>
    ///             See also Building Bug-free O-O Software: An Introduction to 
    ///             Design by Contract 
    ///             http://www.eiffel.com/doc/manuals/technology/contract/
    ///         </para>
    ///         <para>
    ///             Additional Notes:The goal I had when using this class stems 
    ///             from Eiffel's "Design by Contract" usage.  The "Contract" is 
    ///             used visually and conceptually by developers, but is a 
    ///             contract between a method and the method's caller.  The 
    ///             concept helps aid in design and development of a piece of 
    ///             functionality/method, in that it explicitly states what the 
    ///             method needs to complete its operation successfully.  
    ///             "i.e. I (the method) require argument 1 to not be null and 
    ///             be greater then 0 if I am to perform my function without 
    ///             failing, otherwise, there is nothing I can do."
    ///         </para>
    ///         <para>
    ///             This is the reason why I am using it.  This contract takes 
    ///             the place of constructs like ArgumentNullException and the 
    ///             like, while helping to understand what the method needs in a 
    ///             quick manner when looking at the code.  If the contracts are 
    ///             ever broken, it means that the code is definitely not in a 
    ///             functioning state and that an unexpected error most 
    ///             certainly has occurred.
    ///         </para>
    ///         <para>
    ///             Many times, the applied implementation not only uses 
    ///             precondition checking, but also post-condition checking 
    ///             before the method leaves and calls the next method as well.  
    ///             Most of the time, the preconditions are left in the code, 
    ///             and the post-conditions are in a pre-processor directive so 
    ///             that the code is only present in debug mode, and not in 
    ///             release mode. However, I have chosen not to use 
    ///             pre-processor directives so that the production code is the 
    ///             same as the tested code, regardless of its compilation mode.
    ///         </para>
    ///         <para>
    ///             If the usage needs to be removed, I can do that and just 
    ///             check for my conditions using if statements around all 
    ///             parameter checks.  I think the Contract usage results in 
    ///             simpler, cleaner, more easily understood code.  A breaking 
    ///             of the contract is truly exceptional, and hence is handled 
    ///             like other exceptions, and is caught at the shell for the 
    ///             loaded snap-in.
    ///         </para>
    ///         <para>
    ///             If this tool is used anywhere where database operations are 
    ///             being performed, all operationes will be used in 
    ///             transactions, and they will be rolled back in the event an 
    ///             exception is thrown due to a broken Contract.
    ///         </para>
    ///     </Design>
    /// </summary>
    /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
    ///     Class created
    /// </revision>
    public sealed class Contract
    {
		#region Constructors

        /// <summary>
        ///     Prevents a default instance of the <see cref="Contract" /> class from being created.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
		private Contract()
		{
		}

		#endregion

        #region Methods

        /// <summary>
        ///     Precondition check.
        /// </summary>
        /// <param name="assertion">if set to <c>true</c> [assertion].</param>
        /// <param name="message">The message.</param>
        /// <externalUnit cref="PreconditionException"/>
        /// <exception cref="PreconditionException">
        ///     The exception is handled by snap-in components.  See Contract 
        ///     purpose notes or Exception Handling section in Framework notes.
        /// </exception>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public static void Require(bool assertion, string message)
        {
            // check if the assertion is false
            if (assertion == false)
            {
                // create an error specifiying the contract was not fulfilled
                throw new PreconditionException(message);
            }
        }

        /// <summary>
        ///     Precondition check.
        /// </summary>
        /// <param name="assertion">if set to <c>true</c> [assertion].</param>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        /// <externalUnit cref="PreconditionException"/>
        /// <exception cref="PreconditionException">
        ///     The exception is handled by snap-in components.  See Contract 
        ///     purpose notes or Exception Handling section in Framework notes.
        /// </exception>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public static void Require(
            bool assertion,
            string message,
            Exception innerException)
        {
            // check if the assertion is false
            if (assertion == false)
            {
                // create an error specifiying the contract was not fulfilled
                throw new PreconditionException(message, innerException);
            }
        }

        /// <summary>
        ///     Condition check with overriding exception. i.e. Do not use a 
        ///     precondition exception. This is to verify conditions throughout 
        ///     program execution. 
        /// </summary>
        /// <param name="assertion">if set to <c>true</c> [assertion].</param>
        /// <param name="overrideException">The override exception.</param>
        /// <externalUnit/>
        /// <exception cref="Exception">
        ///     The exception is handled by snap-in components.  See Contract 
        ///     purpose notes or Exception Handling section in Framework notes.
        /// </exception>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public static void Require(bool assertion, Exception overrideException)
        {
            // check if the assertion is false
            if (assertion == false)
            {
                // don't create a new error, instead use the exception provided
                throw overrideException;
            }
        }

        /// <summary>
        ///     Precondition check.
        /// </summary>
        /// <param name="assertion">if set to <c>true</c> [assertion].</param>
        /// <externalUnit cref="PreconditionException"/>
        /// <exception cref="PreconditionException">
        ///     The exception is handled by snap-in components.  See Contract 
        ///     purpose notes or Exception Handling section in Framework notes.
        /// </exception>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public static void Require(bool assertion)
        {
            // check if the assertion is false
            if (assertion == false)
            {
                // create an error specifiying the contract was not fulfilled
                throw new PreconditionException("Precondition failed.");
            }
        }

        /// <summary>
        ///     Postcondition check.
        /// </summary>
        /// <param name="assertion">if set to <c>true</c> [assertion].</param>
        /// <param name="message">The message.</param>
        /// <externalUnit cref="PostconditionException"/>
        /// <exception cref="PostconditionException">
        ///     The exception is handled by snap-in components.  See Contract 
        ///     purpose notes or Exception Handling section in Framework notes.
        /// </exception>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public static void Ensure(bool assertion, string message)
        {
            // check if the assertion is false
            if (assertion == false)
            {
                // create an error specifiying the contract was not fulfilled
                throw new PostconditionException(message);
            }
        }

        /// <summary>
        ///     Postcondition check.
        /// </summary>
        /// <param name="assertion">if set to <c>true</c> [assertion].</param>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        /// <externalUnit cref="PostconditionException"/>
        /// <exception cref="PostconditionException">
        ///     The exception is handled by snap-in components.  See Contract 
        ///     purpose notes or Exception Handling section in Framework notes.
        /// </exception>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public static void Ensure(
            bool assertion,
            string message,
            Exception innerException)
        {
            // check if the assertion is false
            if (assertion == false)
            {
                // create an error specifiying the contract was not fulfilled
                throw new PostconditionException(message, innerException);
            }
        }

        /// <summary>
        ///     Postcondition check.
        /// </summary>
        /// <param name="assertion">if set to <c>true</c> [assertion].</param>
        /// <externalUnit cref="PostconditionException"/>
        /// <exception cref="PostconditionException">
        ///     The exception is handled by snap-in components.  See Contract 
        ///     purpose notes or Exception Handling section in Framework notes.
        /// </exception>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public static void Ensure(bool assertion)
        {
            // check if the assertion is false
            if (assertion == false)
            {
                // create an error specifiying the contract was not fulfilled
                throw new PostconditionException("Postcondition failed.");
            }
        }

        /// <summary>
        ///     Invariant check.
        /// </summary>
        /// <param name="assertion">if set to <c>true</c> [assertion].</param>
        /// <param name="message">The message.</param>
        /// <externalUnit cref="InvariantException"/>
        /// <exception cref="InvariantException">
        ///     The exception is handled by snap-in components.  See Contract 
        ///     purpose notes or Exception Handling section in Framework notes.
        /// </exception>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public static void Invariant(bool assertion, string message)
        {
            // check if the assertion is false
            if (assertion == false)
            {
                // create an error specifiying the contract was not fulfilled
                throw new InvariantException(message);
            }
        }

        /// <summary>
        ///     Invariant check.
        /// </summary>
        /// <param name="assertion">if set to <c>true</c> [assertion].</param>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        /// <externalUnit cref="InvariantException">
        ///     The exception is handled by snap-in components.  See Contract 
        ///     purpose notes or Exception Handling section in Framework notes.
        /// </externalUnit>
        /// <exception cref="InvariantException"/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public static void Invariant(
            bool assertion,
            string message,
            Exception innerException)
        {
            // check if the assertion is false
            if (assertion == false)
            {
                // create an error specifiying the contract was not fulfilled
                throw new InvariantException(message, innerException);
            }
        }

        /// <summary>
        ///     Invariant check.
        /// </summary>
        /// <param name="assertion">if set to <c>true</c> [assertion].</param>
        /// <externalUnit cref="InvariantException"/>
        /// <exception cref="InvariantException">
        ///     The exception is handled by snap-in components.  See Contract 
        ///     purpose notes or Exception Handling section in Framework notes.
        /// </exception>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public static void Invariant(bool assertion)
        {
            // check if the assertion is false
            if (assertion == false)
            {
                // create an error specifiying the contract was not fulfilled
                throw new InvariantException("Invariant failed.");
            }
        }

        #endregion 
    }
}