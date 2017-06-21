//-----------------------------------------------------------------------------
// <copyright file="MessageHelper.cs" 
//  company="Sequoia Voting Systems">
//     Copyright (c) 2008 Sequoia Voting Systems. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
//     File created
// </revision>
//-----------------------------------------------------------------------------

namespace Sequoia.EMS.Core.Utilities
{
    #region Using directives

    using System.Windows.Forms;

    #endregion

    /// <summary>
    ///     MessageHelper is a utility class which centralizes creation of 
    ///     message boxes and notices to the user.
    /// </summary>
    /// <revision revisor="dev06" date="10/6/2008" verison="1.0.0.0">
    ///     Member created
    /// </revision>
    public class MessageHelper
    {
        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        ///     Prevents a default instance of the <see cref="MessageHelper"/> class from being created.  
        ///     This is private, as all methods are static.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        private MessageHelper()
        {
        }

        #endregion

        #region Properties

        #endregion

        #region Methods

        /// <summary>
        ///     Shows the information to the user.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="caption">The caption.</param>
        /// <returns>
        ///     The dialog result from the user interaction. This will an 'OK' 
        ///     result.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public static DialogResult ShowInfo(string message, string caption)
        {
            return MessageBox.Show(
                FilterNewLine(message),
                FilterNewLine(caption),
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        /// <summary>
        ///     Shows the fatal error to the user.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="caption">The caption.</param>
        /// <returns>
        ///     The dialog result from the user interaction. This will an 'OK' 
        ///     result.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public static DialogResult ShowFatal(string message, string caption)
        {
           return
                MessageBox.Show(
                    FilterNewLine(message),
                    FilterNewLine(caption),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Stop);
        }

        /// <summary>
        ///     Shows the warning to the user.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="caption">The caption.</param>
        /// <returns>
        ///     The dialog result from the user interaction. This will an 'OK' 
        ///     result.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public static DialogResult ShowWarning(string message, string caption)
        {
            return
                MessageBox.Show(
                    FilterNewLine(message),
                    FilterNewLine(caption),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
        }

        /// <summary>
        ///     Shows a question to the user.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="caption">The caption.</param>
        /// <returns>
        ///     The dialog result from the user interaction. This will either be 
        ///     a 'Yes' or 'No' result.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public static DialogResult ShowQuestion(string message, string caption)
        {
            return
                MessageBox.Show(
                    FilterNewLine(message),
                    FilterNewLine(caption),
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
        }

        /// <summary>
        ///     Filters the new line.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A <c>string</c> containing the filtered value.</returns>
        /// <externalUnit/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        private static string FilterNewLine(string value)
        {
            // create param for new line value  stored in resource records
            string delimitedNewLine = "\\r\\n";

            // create param for the real new line value.
            string newLine = "\r\n";

            // check to see if there are any delimited new liens in value
            if (value.Contains(delimitedNewLine))
            {
                // if so, replace with the real value
                value = value.Replace(delimitedNewLine, newLine);
            }

            // return the filtered string
            return value;
        }

        #endregion
    }
}
