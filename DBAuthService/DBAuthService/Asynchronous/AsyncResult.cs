// -----------------------------------------------------------------------------
// <copyright file="AsyncResult.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2010 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the AsyncResult class.
// </summary>
// <revision revisor="dev14" date="2/16/2010" version="1.1.6.19">
//     File Created
// </revision>  
// -----------------------------------------------------------------------------

namespace Sequoia.DBAuthService.Asynchronous
{
    #region Using directives

    using System;
    using System.Threading;

    #endregion

    /// <summary>
    ///	    Contains methods and events for handling asynchronous operations
    ///     and reporting back its state and results
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev14" date="2/16/2010" version="1.1.6.19">
    ///     Class created.
    /// </revision>
    public class AsyncResult : IAsyncResult, IDisposable
    {
        /// <summary>
        ///     Callback delegate
        /// </summary>
        private AsyncCallback callback;

        /// <summary>
        ///     State of the asynchronous operation
        /// </summary>
        private object state;

        /// <summary>
        ///     Event to signal to awaiting threads that controlling thread 
        ///     has completed
        /// </summary>
        private ManualResetEvent manualResentEvent;

        /// <summary>
        /// Initializes a new instance of the <see cref="AsyncResult"/> class.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <param name="state">The state.</param>
        /// <externalUnit cref="AsyncCallback" />
        /// <revision revisor="dev14" date="2/16/2010" version="1.1.6.19">
        ///     Member Created
        /// </revision>
        public AsyncResult(AsyncCallback callback, object state)
        {
            // set local reference to the callback delegate
            this.callback = callback;

            // set local reference to the operation state
            this.state = state;

            // set local reference to the reset event, starts as not set.
            this.manualResentEvent = new ManualResetEvent(false);
        }

        /// <summary>
        ///     Gets a user-defined object that qualifies or contains 
        ///     information about the asynchronous operation.
        /// </summary>
        /// <value></value>
        /// <returns>
        ///     A user-defined object that qualifies or contains information 
        ///     about an asynchronous operation.
        /// </returns>
        /// <externalUnit cref="IAsyncResult" />
        /// <revision revisor="dev14" date="2/16/2010" version="1.1.6.19">
        ///     Member Created
        /// </revision>
        object IAsyncResult.AsyncState
        {
            get
            {
                return this.state;
            }
        }

        /// <summary>
        ///     Gets the reset event.
        /// </summary>
        /// <value>The reset event.</value>
        /// <externalUnit cref="ManualResetEvent" />
        /// <revision revisor="dev14" date="2/16/2010" version="1.1.6.19">
        ///     Member Created
        /// </revision>
        public ManualResetEvent AsyncWait
        {
            get
            {
                return this.manualResentEvent;
            }
        }

        /// <summary>
        ///     Gets a <see cref="T:System.Threading.WaitHandle"/> that is used 
        ///     to wait for an asynchronous operation to complete.
        /// </summary>
        /// <value></value>
        /// <returns>
        ///     A <see cref="T:System.Threading.WaitHandle"/>.
        /// </returns>
        /// <externalUnit cref="IAsyncResult" />
        /// <revision revisor="dev14" date="2/16/2010" version="1.1.6.19">
        ///     Member Created
        /// </revision>
        WaitHandle IAsyncResult.AsyncWaitHandle
        {
            get
            {
                return this.AsyncWait;
            }
        }

        /// <summary>
        ///     Gets a value indicating whether the asynchronous operation 
        ///     completed synchronously.
        /// </summary>
        /// <value></value>
        /// <returns>
        ///     [true] if the asynchronous operation completed synchronously; 
        ///     otherwise, [false].
        /// </returns>
        /// <externalUnit cref="IAsyncResult" />
        /// <revision revisor="dev14" date="2/16/2010" version="1.1.6.19">
        ///     Member Created
        /// </revision>
        bool IAsyncResult.CompletedSynchronously
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        ///     Gets a value indicating whether the asynchronous operation 
        ///     has completed.
        /// </summary>
        /// <value></value>
        /// <returns>
        ///     [true] if the operation is complete; otherwise, [false].
        /// </returns>
        /// <externalUnit cref="IAsyncResult" />
        /// <revision revisor="dev14" date="2/16/2010" version="1.1.6.19">
        ///     Member Created
        /// </revision>
        bool IAsyncResult.IsCompleted
        {
            get
            {
                return this.manualResentEvent.WaitOne(0, false);
            }
        }

        /// <summary>
        ///     Handles the completion of the operation
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="2/16/2010" version="1.1.6.19">
        ///     Member Created
        /// </revision>
        public void Complete()
        {
            // Set the reset event so that other threads are aware of completion
            this.manualResentEvent.Set();

            // Call the callback delegate
            if (this.callback != null)
            {
                this.callback(this);
            }
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, 
        ///     releasing, or resetting unmanaged resources.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="2/16/2010" version="1.1.6.19">
        ///     Member Created
        /// </revision>
        public void Dispose()
        {
            // close the reset event and dispose of it
            this.manualResentEvent.Close();
            this.manualResentEvent = null;

            // dispose of the state object
            this.state = null;

            // dispose of the callback delegate
            this.callback = null;
        }
    }
}
