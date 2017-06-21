// -----------------------------------------------------------------------------
// <copyright file="EnumTextAttribute.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2008 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the EnumTextAttribute class.
// </summary>
// <revision revisor="dev11" date="10/13/2008" version="1.0.0.0">
//     File Created
// </revision>
// <revision revisor="dev13" date="11/18/2009" version="1.0.0.0">
//     File modified
// </revision>
// -----------------------------------------------------------------------------

namespace Sequoia.Utilities
{
    #region Using directives

    using System;
    using System.Collections;
    using System.Reflection;
    
    #endregion

    /// <summary>
    ///     EnumTextAttribute gives access to snumerators
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev11" date="10/13/2008" version="1.0.0.0">
    ///     Class created.
    /// </revision>
    /// <revision revisor="dev11" date="10/13/2008" version="1.0.0.0">
    ///     Formatting changes.
    /// </revision>
    public class EnumTextAttribute : Attribute
    {
        #region Fields

        /// <summary>
        /// Hash labels
        /// </summary>
        private static Hashtable hshTexts = new Hashtable();

        /// <summary>
        /// Hash Values
        /// </summary>
        private static Hashtable hshValues = new Hashtable();

        /// <summary>
        /// Type of enum
        /// </summary>
        private static Type type = typeof(EnumTextAttribute);

        /// <summary>
        /// the attribute text
        /// </summary>
        private string text;

        #endregion
        
        #region Constructors

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the text of the attribute
        /// </summary>
        /// <value>The text that needs to be retrieved.</value>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="10/14/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public string Text
        {
            get { return this.text; }
            set { this.text = value; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Gets the text attribute value of an enum type object
        /// </summary>
        /// <param name="value">The enum type value.</param>
        /// <returns>the text attribute value</returns>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="10/14/2008" version="1.0.?.0">
        ///     Member Created
        /// </revision>
        public static string GetText(Enum value)
        {
            string strValue = null;
            if (hshTexts.ContainsKey(value) == true)
            {
                // not the first time this text is requested, so get it from 
                // the cached hashtable
                strValue = (string) hshTexts[value];
            }
            else
            {
                // first time this text is requested */
                FieldInfo fldValue = value.GetType().GetField(value.ToString());

                // get all EnumTextAttributes this field has (actually it is
                // supposed to have only one, but the method returns an array 
                EnumTextAttribute[] attrs = (EnumTextAttribute[])

                    // type is the typeof EnumTextAttribute
                    fldValue.GetCustomAttributes(type, false);
                if (attrs.Length > 0)
                {
                    // the field has at least 1 text attribute, so get the first
                    // one
                    strValue = attrs[0].Text;

                    // and add the found value to the hashtables in order to
                    // speed up subsequent searches on the same value
                    hshTexts.Add(value, strValue);
                    hshValues.Add(strValue, value);
                }
            }

            return strValue;
        }

        /// <summary>
        ///     Gets the value from a text. Assumes that all values are 
        ///     preloaded by calling GetText first.
        /// </summary>
        /// <param name="text">The text name</param>
        /// <returns>Gets the type of the enumeration</returns>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="10/14/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public static Enum GetValue(string text)
        {
            Enum value = null;

            if (hshValues.ContainsKey(text) == true)
            {
                // get the value from the hashtable
                value = (Enum) hshValues[text];
            }

            return value;
        }

        #endregion

        #region Public Events

        #endregion

        #region Private Methods

        #endregion
    }
}
