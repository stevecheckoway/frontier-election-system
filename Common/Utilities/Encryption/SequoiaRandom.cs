// -----------------------------------------------------------------------------
// <copyright file="SequoiaRandom.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the SequoiaRandom class.
// </summary>
// <revision revisor="dev01" date="1/29/2010" version="1.1.6.1">
//     File Created
// </revision>
// -----------------------------------------------------------------------------

namespace Sequoia.Utilities.Encryption
{
    using System;
    using System.Security.Cryptography;

    /// <summary>
    /// Class dedicated to generate Cryptographically safe 
    /// random numbers
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev01" date="1/29/2010" version="1.1.6.1">
    ///     Member Created
    /// </revision>
    public class SequoiaRandom
    {
        /// <summary>
        /// Sequoias the number.
        /// </summary>
        /// <param name="length">
        /// The length.
        /// </param>
        /// <returns>
        /// A random byte Array
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="1/29/2010" version="1.1.6.1">
        ///     Member Created
        /// </revision>
        public static byte[] RandomBytes(int length)
        {
            var returnValue = new byte[length];

            var generator = new RNGCryptoServiceProvider();

            generator.GetBytes(returnValue);

            return returnValue;
        }

        /// <summary>
        /// Randoms the number.
        /// </summary>
        /// <param name="minValue">
        /// The min value.
        /// </param>
        /// <param name="maxValue">
        /// The max Value.
        /// </param>
        /// <returns>
        /// An integer between those values
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="1/29/2010" version="1.1.6.1">
        ///     Member Created
        /// </revision>
        public static int RandomNumber(int minValue, int maxValue)
        {
            int returnValue = minValue;
            if (minValue != maxValue)
            {
                int difference = Math.Abs((maxValue - minValue));
                var randomNumber =
                    (double)BitConverter.ToInt32(SequoiaRandom.RandomBytes(4), 0);

                randomNumber =
                    randomNumber > 0
                        ?
                            randomNumber + 2d
                        : randomNumber + 1d;

                var extraDouble = (randomNumber + int.MaxValue) * difference /
                                  ((1d + int.MaxValue) * 2);

                var extraNumber = (int)Math.Round(extraDouble);
                returnValue += extraNumber;
            }

            return returnValue;
        }

        /// <summary>
        /// Randoms the number.
        /// </summary>
        /// <returns>A random Positive Integer</returns>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="1/29/2010" version="1.1.6.1">
        ///     Member Created
        /// </revision>
        public static int RandomNumber()
        {
            return BitConverter.ToInt32(SequoiaRandom.RandomBytes(4), 0);
        }
    }
}
