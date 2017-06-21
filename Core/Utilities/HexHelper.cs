//-----------------------------------------------------------------------------
// <copyright file="HexHelper.cs" 
//  company="Sequoia Voting Systems">
//     Copyright (c) 2008 Sequoia Voting Systems. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
//    File created
// </revision>
//-----------------------------------------------------------------------------

namespace Sequoia.EMS.Core.Utilities
{
    #region Using directives

    using System.Collections.Generic;
    using Sequoia.EMS.Core.DesignByContract;

    #endregion

    /// <summary>
    ///     Hexadecimal operations helper class. Static helper class.
    /// </summary>
    /// <revision revisor="dev06" date="10/6/2008" verison="1.0.0.0">
    ///     Class created
    /// </revision>
    public class HexHelper
    {
        #region Constants

        /// <summary>
        ///     add constant for length of a pair
        /// </summary>
        private const int PairLength = 2;

        #endregion

        #region Constructors

        /// <summary>
        ///     Prevents a default instance of the <see cref="HexHelper"/> class from being created.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev06" date="10/6/2008" verison="1.0.0.0">
        ///     Member created
        /// </revision>
        private HexHelper()
        {
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Gets the bytes from public key token hex.
        /// </summary>
        /// <param name="publicKeyTokenHex">The public key token hex.</param>
        /// <returns>
        ///     Returns a <c>byte[]</c> that contains all of the real 
        ///     byte values of the public key token string.
        /// </returns>
        /// <externalUnit cref="Contract"/>
        /// <externalUnit cref="SplitPublicKeyTokenIntoBytePairs"/>
        /// <exception cref="DesignByContractException">
        ///     Failed contracts will cause exception to be thrown.  Handled by 
        ///     caller or framework.
        /// </exception>
        /// <revision revisor="dev06" date="10/6/2008" verison="1.0.0.0">
        ///     Member created
        /// </revision>
        public static byte[] GetBytesFromPublicKeyTokenHex(
            string publicKeyTokenHex)
        {
            Contract.Require(string.IsNullOrEmpty(publicKeyTokenHex) == false);

            // Get the token length.
            int tokenLength = publicKeyTokenHex.Length;

            // Verify tokenLength is an even number.  A byte is two hex chars.
            // Use the mod operator to verify that the number divided by two 
            // leaves no remainders therefore it is an even number.
            Contract.Require(tokenLength % 2 == 0);

            // public key token are only 8 bytes, but gonna use a list here in 
            // case it ever gets larger.
            List<byte> publicKeyTokenBytes = new List<byte>();

            // turn bytes into a string array
            string[] individualPublicKeyTokenBytes =
                SplitPublicKeyTokenIntoBytePairs(publicKeyTokenHex);

            // loop through all the string 'bytes' and parse them 
            // into real bytes
            foreach (string bytePair in individualPublicKeyTokenBytes)
            {
                // parse the byte from the hex string
                byte publicKeyTokenByte =
                    byte.Parse(
                        bytePair, System.Globalization.NumberStyles.HexNumber);

                // add the parsed byte to the list
                publicKeyTokenBytes.Add(publicKeyTokenByte);
            }

            // return the byte list as an array of bytes
            return publicKeyTokenBytes.ToArray();
        }

        /// <summary>
        ///     Splits the public key token into byte pairs.
        /// </summary>
        /// <param name="publicKeyTokenHex">The public key token hex.</param>
        /// <returns>
        ///     A <c>string[]</c> containing the list of hex byte pairs as 
        ///     strings.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev06" date="10/6/2008" verison="1.0.0.0">
        ///     Member created
        /// </revision>
        private static string[] SplitPublicKeyTokenIntoBytePairs(
            string publicKeyTokenHex)
        {
            // create a new list to store the bytes
            List<string> bytePairs = new List<string>();

            // parse the full string hex into char strings
            while (publicKeyTokenHex.Length >= PairLength)
            {
                // get a 'byte' from the string
                string bytePair = publicKeyTokenHex.Substring(0, PairLength);
                
                // add the 'byte' char pair to the list
                bytePairs.Add(bytePair);

                // shorten the full string so we can ge the next pair
                publicKeyTokenHex = publicKeyTokenHex.Remove(0, PairLength);
            }

            // return the list as a string array
            return bytePairs.ToArray();
        }

        #endregion
    }
}
