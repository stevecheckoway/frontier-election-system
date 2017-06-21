// -----------------------------------------------------------------------------
// <copyright file="RandomStringGenerator.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the RandomStringGenerator class.
// </summary>
// <revision revisor="dev01" date="1/29/2010" version="1.1.6.1">
//     File Created
// </revision>
// -----------------------------------------------------------------------------

namespace Sequoia.Utilities.Encryption
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Generates Random Strings
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev01" date="1/29/2010" version="1.1.6.1">
    ///     Member Created
    /// </revision>
    public class RandomStringGenerator
    {
        /// <summary>
        /// Mask Dictionary
        /// </summary>
        private readonly Dictionary<StringComplexity.Categories, char[]>
            CategoryArrays = 
            new Dictionary<StringComplexity.Categories, char[]>
                {
                                    {
                                        StringComplexity.Categories.
                                        LowerCaseLetters,
                                        new[]
                                            {
                                                'a',
                                                'b',
                                                'c',
                                                'd',
                                                'e',
                                                'f',
                                                'g',
                                                'h',
                                                'i',
                                                'j',
                                                'k',
                                                'l',
                                                'm',
                                                'n',
                                                'o',
                                                'p',
                                                'q',
                                                'r',
                                                's',
                                                't',
                                                'u',
                                                'v',
                                                'w',
                                                'x',
                                                'y',
                                                'z'
                                            }
                                        },
                                    {
                                        StringComplexity.Categories.
                                        UpperCaseLetters,
                                        new[]
                                            {
                                                'A',
                                                'B',
                                                'C',
                                                'D',
                                                'E',
                                                'F',
                                                'G',
                                                'H',
                                                'I',
                                                'J',
                                                'K',
                                                'L',
                                                'M',
                                                'N',
                                                'O',
                                                'P',
                                                'Q',
                                                'R',
                                                'S',
                                                'T',
                                                'U',
                                                'V',
                                                'W',
                                                'X',
                                                'Y',
                                                'Z'
                                            }
                                        },
                                    {
                                        StringComplexity.Categories.Numbers,
                                        new[]
                                            {
                                                '0',
                                                '1',
                                                '2',
                                                '3',
                                                '4',
                                                '5',
                                                '6',
                                                '7',
                                                '8',
                                                '9'
                                            }
                                        },
                                    {
                                        StringComplexity.Categories.Symbols,
                                        new[]
                                            {
                                                '(',
                                                ')',
                                                '`',
                                                '~',
                                                '!',
                                                '@',
                                                '$',
                                                '%',
                                                '^',
                                                '*',
                                                '-',
                                                '+',
                                                '=',
                                                '|',
                                                '\\',
                                                '{',
                                                '}',
                                                '[',
                                                ']',
                                                ':',
                                                ';',
                                                '"',
                                                ';',
// Probably not permited in SQL password        '\'',
                                                '>',
                                                ',',
                                                '.',
                                                '?',
                                                '/',
                                                '_'
                                            }
                                        }
                                };

        /// <summary>
        /// Singleton instance
        /// </summary>
        private static RandomStringGenerator current = null;

        /// <summary>
        /// Prevents a default instance of the <see cref="RandomStringGenerator"/> class from being created. 
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="1/29/2010" version="1.1.6.1">
        ///     Member Created
        /// </revision>
        private RandomStringGenerator()
        {
        }

        /// <summary>
        /// Gets the current.
        /// </summary>
        /// <value>The current.</value>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="1/29/2010" version="1.1.6.1">
        ///     Member Created
        /// </revision>
        public static RandomStringGenerator Current
        {
            get
            {
                if (current == null)
                {
                    current = new RandomStringGenerator();
                }

                return current;
            }
        }

        /// <summary>
        /// Generates the string.
        /// </summary>
        /// <param name="length">The length.</param>
        /// <param name="complexity">The complexity.</param>
        /// <returns>The random string</returns>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="1/29/2010" version="1.1.6.1">
        ///     Member Created
        /// </revision>
        public string GenerateString(
            int length,
            long complexity)
        {
            StringBuilder tempString = new StringBuilder(length);

            var categoryList = StringComplexity.ProcessComplexity(complexity);
            var restOfCategories = StringComplexity.ProcessComplexity(complexity);
            StringComplexity.Categories category;
            byte[] bytesHolder = SequoiaRandom.RandomBytes(length);
            Array listOfCategories =
                Enum.GetValues(typeof(StringComplexity.Categories));

            foreach (byte letter in bytesHolder)
            {
                int index = 0;
                if (categoryList.Count > 0)
                {
                    index = SequoiaRandom.RandomNumber(
                        0, categoryList.Count - 1);
                    category = categoryList[index];
                    categoryList.RemoveAt(index);
                }
                else
                {
                    index = SequoiaRandom.RandomNumber(
                        0,
                        restOfCategories.Count - 1);
                    category = restOfCategories[index];
                }

                char character = this.GetCharacter(letter, category);
                tempString.Append(character);
            }
            
            return tempString.ToString();
        }

        /// <summary>
        /// Generates the string.
        /// </summary>
        /// <param name="length">The length.</param>
        /// <param name="complexity">The complexity.</param>
        /// <returns>The random string</returns>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="1/29/2010" version="1.1.6.1">
        ///     Member Created
        /// </revision>
        public string GenerateString(
            int length,
            StringComplexity.Complexity complexity)
               {
                   return this.GenerateString(
                       length,
                       StringComplexity.ComplexityMasks[complexity]);
               }

        /// <summary>
        /// Gets the character.
        /// </summary>
        /// <param name="letter">The random number.</param>
        /// <param name="category">The category.</param>
        /// <returns>The character in the projected position within the
        /// category array.</returns>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="1/29/2010" version="1.1.6.1">
        /// Member Created
        /// </revision>
        private char GetCharacter(
            byte letter, 
            StringComplexity.Categories category)
        {
            double placement = 
                letter * (this.CategoryArrays[category].Length - 1) / 256d;
            var charIndex = (int)Math.Round(placement);
            return this.CategoryArrays[category][charIndex];
        }

        /// <summary>
        /// Generates the string.
        /// </summary>
        /// <param name="minLength">Length of the min.</param>
        /// <param name="maxLength">Length of the max.</param>
        /// <param name="complexity">The complexity.</param>
        /// <returns>The random string</returns>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="1/29/2010" version="1.1.6.1">
        /// Member Created
        /// </revision>
        public string GenerateString(
            int minLength, 
            int maxLength,
            StringComplexity.Complexity complexity)
        {
            return
                this.GenerateString(
                    SequoiaRandom.RandomNumber(minLength, maxLength), 
                    complexity);
        }
    }
}
