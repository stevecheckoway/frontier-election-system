// -----------------------------------------------------------------------------
// <copyright file="CartridgeModeList.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2008 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the CartridgeModeList class.
// </summary>
// <revision revisor="dev06" date="10/13/2008" version="1.0.0.0">
//     File Created
// </revision>  
// <revision revisor="dev13" date="8/24/2009" version="1.0.16.04">
//     File Modified
// </revision>  
// <revision revisor="dev05" date="09/11/09" version="1.0.17.4">
//     File modified.
// </revision>
// -----------------------------------------------------------------------------

namespace Sequoia.DomainObjects.BallotStorage
{
    #region Using directives

    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    #endregion

    /// <summary>
    ///     CartridgeModeList is a <see cref="List{T}" /> of 
    ///     <see cref="CartridgeMode" /> objects. 
    /// </summary>
    /// <externalUnit cref="CartridgeMode"/>
    /// <revision revisor="dev06" date="10/13/2008" version="1.0.0.0">
    ///     Class created.
    /// </revision>
    [Serializable]
    public class CartridgeModeList : List<CartridgeMode>
    {
        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="CartridgeModeList"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev06" date="10/13/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision> 
        public CartridgeModeList()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="CartridgeModeList"/> class.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        /// <externalUnit cref="List{T}(int)"/>
        /// <revision revisor="dev06" date="10/13/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public CartridgeModeList(int capacity)
            : base(capacity)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="CartridgeModeList"/> class.
        /// </summary>
        /// <param name="cartridgeModes">The cartridge modes.</param>
        /// <externalUnit cref="CartridgeMode"/>
        /// <externalUnit cref="List{T}(IEnumerable{T})"/>
        /// <externalUnit cref="IEnumerable{T}"/>
        /// <revision revisor="dev06" date="10/13/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public CartridgeModeList(IEnumerable<CartridgeMode> cartridgeModes)
            : base(cartridgeModes)
        {
        }

        #endregion

        #region Public Properties

        #endregion

        #region Public Methods

        #region IsModeIdValid
        /// <summary>
        ///     Determines whether the specified mode id is valid - i.e. is 
        ///     in the list. This method just checks the entire collection for 
        ///     the id.  If performance becomes an issue, we could always 
        ///     track this using another method such as a hashtable or change 
        ///     this collection to be a dictionary.  Should be very few modes 
        ///     though, so don't believe this will cause any issues.
        /// </summary>
        /// <param name="modeId">The mode id.</param>
        /// <returns>
        ///     <c>true</c> if the specified mode id is in the list; 
        ///     otherwise, <c>false</c>.
        /// </returns>
        /// <externalUnit cref="CartridgeMode"/>
        /// <revision revisor="dev06" date="10/13/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public bool IsModeIdValid(int modeId)
        {
            // create var to return and track value
            bool doIContainTheSpecifiedId = false;

            // check all the cartridge modes in the collection
            foreach (CartridgeMode mode in this)
            {
                // check each id to see if it is in the list
                if (mode.Id.Equals(modeId))
                {
                    // set the return value as we found it
                    doIContainTheSpecifiedId = true;

                    // found it so stop looking
                    break;
                }
            }

            return doIContainTheSpecifiedId;
        }
        #endregion

        #region GetCartridgeMode
        /// <summary>
        ///     Gets the cartridge mode.
        /// </summary>
        /// <param name="modeId">The mode id.</param>
        /// <returns>
        ///     The <see cref="CartridgeMode"/> identified by the supplied 
        ///     mode id.  Returns <c>null</c> if the item is not in the list.
        /// </returns>
        /// <externalUnit cref="CartridgeMode"/>
        /// <revision revisor="dev06" date="10/13/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public CartridgeMode GetCartridgeMode(int modeId)
        {
            // create var to store th desired mode
            CartridgeMode desiredMode = null;

            // check all the cartridge modes in the collection
            foreach (CartridgeMode mode in this)
            {
                // check each id to see if it is in the list
                if (mode.Id.Equals(modeId))
                {
                    // set the return value as we found it
                    desiredMode = mode;

                    // found it so stop looking
                    break;
                }
            }

            return desiredMode;
        }
        #endregion

        /// <summary>
        ///     Gets the next cartridge mode.
        /// </summary>
        /// <param name="currentModeId">The current mode id.</param>
        /// <returns>
        ///     A <see cref="CartridgeMode" /> representing the next valid mode. 
        ///     Returns <c>null</c> if next mode is not valid.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="12/22/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public CartridgeMode GetNextCartridgeMode(int currentModeId)
        {
            // create var to store the new mode
            CartridgeMode mode = null;

            // check that current mode is less than the max (3).
            if (currentModeId < 3)
            {
                mode = new CartridgeMode(currentModeId + 1);
            }
            else if (currentModeId == 3)
            {
                mode = this.GetCartridgeMode(currentModeId);
                
                if (mode.Closed != DateTime.MinValue)
                {
                    // the next cartridge mode is not valid
                    mode = null;
                }
            }

            return mode;
        }

        #endregion

        #region Public Events

        #endregion

        #region Private Methods

        #endregion
    }
}
