// -----------------------------------------------------------------------------
// <copyright file="StringComplexity.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the StringComplexity class.
// </summary>
// <revision revisor="dev01" date="1/29/2010" version="1.1.6.1">
//     File Created
// </revision>
// -----------------------------------------------------------------------------

namespace Sequoia.Utilities.Encryption
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Static class that handles the  string complexity
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev01" date="1/29/2010" version="1.1.6.1">
    ///     Member Created
    /// </revision>
    public class StringComplexity
    {
        /// <summary>
        /// Mask Dictionary
        /// </summary>
        public static readonly Dictionary<Complexity, long>
            ComplexityMasks = new Dictionary<Complexity, long>
                                  {
                                      {
                                          Complexity.Simple,
                                          0x08
                                          },
                                      {
                                          Complexity.Medium,
                                          0x020204
                                          },
                                      {
                                          Complexity.Strong, 
                                          0x01020203
                                          }
                                  };

        /// <summary>
        /// Mask Dictionary
        /// </summary>
        private static readonly Dictionary<Categories, long>
            CategoryMasks = new Dictionary<Categories, long>
                                  {
                                      {
                                          Categories.LowerCaseLetters,
                                          0xFF
                                          },
                                      {
                                          Categories.UpperCaseLetters,
                                          0xFF00
                                          },
                                      {
                                          Categories.Numbers, 
                                          0xFF0000
                                          },
                                      {
                                          Categories.Symbols, 
                                          0xFF000000
                                          }
                                  };

        /// <summary>
        /// String Complexity
        /// </summary>
        public enum Complexity
        {
            /// <summary>
            /// Plain LoweCase Letters
            /// </summary>
            Simple,

            /// <summary>
            /// Plain Letters and Numbers
            /// </summary>
            Medium,

            /// <summary>
            /// Lower and UpperCase, Number and Symbols
            /// </summary>
            Strong
        }

        /// <summary>
        /// Character Categories
        /// </summary>
        public enum Categories
        {
            /// <summary>
            /// LowerCase Letters
            /// </summary>
            LowerCaseLetters = 0,

            /// <summary>
            /// UpperCase Letters
            /// </summary>
            UpperCaseLetters = 8,

            /// <summary>
            /// Numbers (0-9)
            /// </summary>
            Numbers = 16,

            /// <summary>
            /// Symbols (_+=-/.,;', etc)
            /// </summary>
            Symbols = 24,
        }

        /// <summary>
        /// Adds the complexity.
        /// </summary>
        /// <param name="complexity">The complexity.</param>
        /// <param name="category">The category.</param>
        /// <param name="repetitions">The repetitions.</param>
        /// <returns>The updated Complexity Mask</returns>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="1/29/2010" version="1.1.6.1">
        ///     Member Created
        /// </revision>
        public static long AddComplexity(
            long complexity,
            Categories category,
            int repetitions)
        {
            long addition = repetitions << (int) category;
            return complexity + addition;
        }

        /// <summary>
        /// Checks the complexity category.
        /// </summary>
        /// <param name="complexity">The complexity.</param>
        /// <param name="category">The category.</param>
        /// <returns>The amount of the complexity category</returns>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="1/29/2010" version="1.1.6.1">
        ///     Member Created
        /// </revision>
        public static int CheckComplexityCategory(
            long complexity, 
            Categories category)
        {
            long tempValue  = complexity & CategoryMasks[category];
            long returnValue = tempValue >> (int)category;
            return (int) returnValue;
        }

        /// <summary>
        /// Processes the complexity.
        /// </summary>
        /// <param name="complexity">The complexity.</param>
        /// <returns>An array with Categories in complexity</returns>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="1/29/2010" version="1.1.6.1">
        ///     Member Created
        /// </revision>
        public static List<Categories> ProcessComplexity(long complexity)
        {
            var returnValue = new List<Categories>();
            foreach (Categories cat in Enum.GetValues(typeof(Categories)))
            {
                for (int i = 0;
                    i
                    < StringComplexity.CheckComplexityCategory(complexity, cat);
                    i++)
                {
                    returnValue.Add(cat);
                }
            }

            return returnValue;
        }
    }
}
