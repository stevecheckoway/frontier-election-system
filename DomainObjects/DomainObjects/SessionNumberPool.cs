// -----------------------------------------------------------------------------
// <copyright file="SessionNumberPool.cs" company="Sequoia Voting Systems">
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     SessionNumberPool class implementation.
// </summary>
// <revision revisor="dev05" date="01/12/09" version="1.0.4.13">
//     File Created
// </revision>
// <revision revisor = "dev05" date="01/14/09" version="1.0.4.15">
//     Rewrote, to use a linear congruential generator.
// </revision>
// <revision revisor = "dev05" date="02/17/09" version="1.0.6.13">
//     Fixed bug that occurred when Environment.TickCount rolls over to a
//     negative number.
// </revision>
// -----------------------------------------------------------------------------
namespace Sequoia.DomainObjects
{
    #region Using directives

    using System;
    using System.Xml.Serialization;

    #endregion

    /// <summary>
    ///     A pool that doles out voter session ID numbers from a given range,
    ///     in a random order.
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev05" date="01/12/09" version="1.0.4.13">
    ///     Class created.
    /// </revision>
    /// <revision revisor = "dev05" date="01/14/09" version="1.0.4.15">
    ///     Rewrote, using a linear congruential generator.
    /// </revision>
    [Serializable]
    [XmlRoot("SessionNumberPool")]
    public class SessionNumberPool : DomainObject
    {
        /// <summary>Start of ID range</summary>
        private long start = 1;

        /// <summary>End of ID range (inclusive)</summary>
        private long end = 1;

        /// <summary>Number of ID's already generated</summary>
        private int count = 0;

        /// <summary>Current zero based ID, -1 if none yet generated</summary>
        private int current = -1;

        /// <summary>Have we generated all ID's in the range?</summary>
        private bool full = false;

        /// <summary>Have we obtained the modulus, multiplier, etc.?</summary>
        private bool initialized = false;

        /// <summary>Range length</summary>
        private int length;

        /// <summary>Generator modulus</summary>
        private int modulus;

        /// <summary>Generator multiplier</summary>
        private int multiplier;

        /// <summary>
        ///     Table of moduli and multipliers. The moduli are prime numbers
        ///     just below powers of two, and the corresponding multipliers are
        ///     known to have maximal period, that is, they generate all values
        ///     from 1 to modulus - 1, inclusive. These values come from the
        ///     paper "Tables Of Linear Congruential Generators Of Different
        ///     Sizes And Good Lattice Structure" by Pierre L'Ecuyer; See
        ///     http://www.ams.org/mcom/1999-68-225/S0025-5718-99-00996-5/S0025-5718-99-00996-5.pdf
        /// </summary>
        private int[][] table = new int[][]
        {
            new int[] { 251, 33 },                    // 2 ^ 8 - 5
            new int[] { 509, 160 },                   // 2 ^ 9 - 3
            new int[] { 1021, 65 },                   // 2 ^ 10 - 3
            new int[] { 2039, 603 },                  // 2 ^ 11 - 9
            new int[] { 4093, 209 },                  // 2 ^ 12 - 3
            new int[] { 8191, 6083 },                 // 2 ^ 13 - 1
            new int[] { 16381, 572 },                 // 2 ^ 14 - 3
            new int[] { 32749, 30805 },               // 2 ^ 15 - 19
            new int[] { 65521, 33285 },               // 2 ^ 16 - 15
            new int[] { 131071, 119858 },             // 2 ^ 17 - 1
            new int[] { 262139, 92717 },              // 2 ^ 18 - 5
            new int[] { 524287, 358899 },             // 2 ^ 19 - 1
            new int[] { 1048573, 604211 },            // 2 ^ 20 - 3
            new int[] { 2097143, 1043187 },           // 2 ^ 21 - 9
            new int[] { 4194301, 3279967 },           // 2 ^ 22 - 3
            new int[] { 8388593, 653276 },            // 2 ^ 23 - 15
            new int[] { 16777213, 9726917 },          // 2 ^ 24 - 3
            new int[] { 33554393, 12836191 },         // 2 ^ 25 - 39
            new int[] { 67108859, 19552116 },         // 2 ^ 26 - 5
            new int[] { 134217689, 88641177 },        // 2 ^ 27 - 39
            new int[] { 268435399, 29908911 },        // 2 ^ 28 - 57
            new int[] { 536870909, 520332806 },       // 2 ^ 29 - 3
            new int[] { 1073741789, 1017586987 },     // 2 ^ 30 - 35
            new int[] { 2147483647, 1583458089 },     // 2 ^ 31 - 1
        };

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="SessionNumberPool" /> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev05" date="01/12/09" version="1.0.4.13">
        ///     Method created.
        /// </revision>
        public SessionNumberPool()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="SessionNumberPool" /> class.
        /// </summary>
        /// <param name="start">Start of ID range</param>
        /// <param name="end">End of ID range (inclusive)</param>
        /// <externalUnit/>
        /// <revision revisor="dev05" date="01/12/09" version="1.0.4.13">
        ///     Method created.
        /// </revision>
        /// <revision revisor = "dev05" date="01/14/09" version="1.0.4.15">
        ///     Removed initialization related to old algorithm.
        /// </revision>
        public SessionNumberPool(long start, long end)
        {
            this.start = start;
            this.end = end;
        }

        #endregion

        /// <summary>
        ///     Gets or sets the start of ID range.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev05" date="01/12/09" version="1.0.4.13">
        ///     Property created.
        /// </revision>
        public long Start
        {
            get
            {
                return this.start;
            }

            set
            {
                this.start = value;
            }
        }

        /// <summary>
        ///     Gets or sets the end of ID range (inclusive)
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev05" date="01/12/09" version="1.0.4.13">
        ///     Property created.
        /// </revision>
        public long End
        {
            get
            {
                return this.end;
            }

            set
            {
                this.end = value;
            }
        }

        /// <summary>
        ///     Gets or sets the number of ID's already generated.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev05" date="01/14/09" version="1.0.4.15">
        ///     Property created.
        /// </revision>
        public int Count
        {
            get
            {
                return this.count;
            }

            set
            {
                this.count = value;
            }
        }

        /// <summary>
        ///     Gets or sets the current zero based ID, -1 if none 
        ///     yet generated.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev05" date="01/14/09" version="1.0.4.15">
        ///     Property created.
        /// </revision>
        public int Current
        {
            get
            {
                return this.current;
            }

            set
            {
                this.current = value;
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether or not we have 
        ///     generated all ID's in the range?
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev05" date="01/14/09" version="1.0.4.15">
        ///     Property created.
        /// </revision>
        public bool Full
        {
            get
            {
                return this.full;
            }

            set
            {
                this.full = value;
            }
        }

        /// <summary>
        ///     Return the next random ID in the range.
        /// </summary>
        /// <returns>Next random ID in the range</returns>
        /// <externalUnit/>
        /// <revision revisor="dev05" date="01/12/09" version="1.0.4.13">
        ///     Method created.
        /// </revision>
        /// <revision revisor = "dev05" date="01/14/09" version="1.0.4.15">
        ///     Rewrote for new algorithm.
        /// </revision>
        /// <revision revisor = "dev05" date="02/17/09" version="1.0.6.13">
        ///     Make sure the initial random value is positive!
        /// </revision>
        public long Next()
        {
            // Get modulus, multiplier, etc., if we haven't already.
            if (!this.initialized)
            {
                this.Initialize();
            }

            // First time, get a random value between 1 and length - 1;
            // otherwise run the generator to get the next value.
            if (this.current == -1)
            {
                this.current = (Environment.TickCount & Int32.MaxValue) %
                    (this.length - 1) + 1;
            }
            else
            {
                this.GetNextValue();
            }

            if (++this.count == this.length)
            {
                this.full = true;
            }

            // Return start of range plus zero based value.
            return this.start + this.current;
        }

        /// <summary>
        ///     Initialize the generator. We find the first modulus in the table
        ///     that's big enough for the range.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev05" date="01/14/09" version="1.0.4.15">
        ///     Method created.
        /// </revision>
        private void Initialize()
        {
            // Length of range.
            long length = this.end - this.start + 1;

            if (length < 1 || length > (Int32.MaxValue - 3))
            {
                throw new ApplicationException(
                    "Invalid session number pool range");
            }

            this.length = (int) length;

            // Find a modulus and multiplier suitable for the range.
            foreach (int[] entry in this.table)
            {
                if (entry[0] > this.length)
                {
                    this.modulus = entry[0];
                    this.multiplier = entry[1];
                    break;
                }
            }
        }

        /// <summary>
        ///     Get the next zero based ID, i.e. relative to the start of the
        ///     range.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev05" date="01/14/09" version="1.0.4.15">
        ///     Method created.
        /// </revision>
        private void GetNextValue()
        {
            // Value in 1 ... modulus - 1 range.
            int value = this.current + 1;

            // Run the generator. Since the modulus is generally bigger than the
            // size of the range, we have to throw out values that are too high.
            do
            {
                // Long value, so we don't overflow.
                long longValue = value;

                value = (int)(longValue * this.multiplier % this.modulus);
            } while (value > this.length);

            // Save zero based value (in 0 ... length - 1 range).
            this.current = value - 1;
        }
    }
}
