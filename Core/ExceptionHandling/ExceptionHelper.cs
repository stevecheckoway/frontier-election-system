//-----------------------------------------------------------------------------
// <copyright file="ExceptionHelper.cs" 
//  company="Sequoia Voting Systems">
//     Copyright (c) 2008 Sequoia Voting Systems. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
//     File created
// </revision>
//----------------------------------------------------------------------------- 

namespace Sequoia.EMS.Core.ExceptionHandling
{
    #region Using directives

    using System;
    using System.Text;

    #endregion

    /// <summary>
    ///     ExceptionHelper is a utility class that is use dto help manage and 
    ///     manipulate exceptions.
    /// </summary>
    /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
    ///     Class created
    /// </revision>
    public class ExceptionHelper
    {
        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ExceptionHelper"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public ExceptionHelper()
        {
        }

        #endregion

        #region Properties

        #endregion

        #region Methods

        #region GetFullExceptionMessage
        /// <summary>
        ///     Gets the full exception message for the exception stack.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <returns>
        ///     The messages of all exceptions/inner exceptions for the given 
        ///     exception stack/tree.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public static string GetFullExceptionMessage(Exception exception)
        {
            // create string builder for creating the error message
            StringBuilder fullErrorMessage = new StringBuilder();

            // if the exception exists
            if (exception != null)
            {
                // add the info to the string builder
                fullErrorMessage.AppendLine(
                    string.Format(
                        "Message: {0};\r\n Full: {1}\r\n{2}\r\n\r\n",
                        exception.Message,
                        exception.Source,
                        exception.StackTrace));

                // loop through all of the exception and the inner exception
                // to get all the data possible
                Exception currentException = exception;
                while (currentException.InnerException != null)
                {
                    // set the current exception to its inner exception
                    currentException = currentException.InnerException;

                    // add the info to the string builder
                    fullErrorMessage.AppendLine(
                        string.Format(
                            "Message: {0};\r\n Full: {1}\r\n{2}\r\n\r\n",
                            currentException.Message,
                            currentException.Source,
                            currentException.StackTrace));
                }
            }

            // return the error message as a string
            return fullErrorMessage.ToString();
        }
        #endregion

        #endregion
    }
}
