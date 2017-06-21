// -----------------------------------------------------------------------------
// <copyright file="PaperBallotIdentifier.cs" company="Sequoia Voting Systems"> 
//    Copyright (c) 2008 Sequoia Voting Systems, Inc. All Rights Reserved.
//    Distribution of source code is allowable only under the terms of the
//    license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//    This file contains the PaperBallotIdentifier class.
// </summary>
// <revision revisor="dev11" date="12/23/2008" version="1.0.0.0">
//    File Created
// </revision>
// <revision revisor="dev06" date="1/10/2009" version="1.0.0.0">
//    File Modified
// </revision>  
// <revision revisor="dev11" date="02/23/2009" version="1.0.8.0601">
// class now inherits from BallotShape</revision>
// -----------------------------------------------------------------------------

namespace Sequoia.Ballot.Data
{
    #region Using directives

    using System;
    using System.Collections.Generic;

    using Sequoia.Ballot;
    using Sequoia.Utilities;

    using StpParam = BallotEntrySet.StpParam;

    #endregion

    /// <summary>
    ///     Class for PaperBallotIdentifier
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev11" date="12/23/2008" version="1.0.0.0">
    ///     Class created.
    /// </revision>
    /// <revision revisor="dev11" date="02/23/2009" version="1.0.8.0601">
    ///     Class now inherits from <see cref="BallotShape"/>
    /// </revision>
    /// <revision revisor="dev11" date="02/26/2009" version="1.0.8.0901">
    ///     Class marked as Serializable
    /// </revision>
    [Serializable]
    public class PaperBallotIdentifier : BallotShape
    {
        #region Constants

        /// <summary>
        ///     this character separates a key-value pair from another pair
        /// </summary>
        public const char SepMajor = '|';

        /// <summary>
        ///     this character separates a key from the value
        /// </summary>
        public const char SepMinor = ':';

        /// <summary>
        ///     all fields selected
        /// </summary>
        public const int NoMask = 0xFFFF;

        #endregion
        
        #region Fields

        /// <summary>
        ///     param for the field map
        /// </summary>
        private Dictionary<IdField, object> fieldMap;

        /// <summary>
        ///     param for the mask
        /// </summary>
        private int mask;

        #endregion
        
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="PaperBallotIdentifier"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="12/23/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public PaperBallotIdentifier()
        {
            this.fieldMap = new Dictionary<IdField, object>();
            this.mask = NoMask;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets a value indicating whether this instance has fields.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance has fields; 
        ///     otherwise, <c>false</c>.
        /// </value>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="2/6/2009" version="1.0.6.2">
        /// Member Created</revision>
        public bool HasFields
        {
            get
            {
                return this.fieldMap.Count > 0;
            }
        }

        /// <summary>
        ///     Gets or sets the mask.
        /// </summary>
        /// <value>The mask.</value>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="4/8/2009" version="1.0.10.0501">
        ///     Comments added
        /// </revision>
        public int Mask
        {
            get
            {
                return this.mask;
            }

            set
            {
                this.mask = value;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Sets the specified field.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <param name="val">The value.</param>
        /// <returns>
        ///     returns [true] if the field was set. Typically this is 
        ///     constrained by the mask
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="12/24/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev11" date="03/11/2009" version="1.0.8.2201">
        ///     method now sets a field only if the mask allows it
        /// </revision>
        /// <revision revisor="dev11" date="11/02/2009" version="1.1.2.1101">
        ///     <see cref="IdField.ControlBallot"/> is not required to be 
        ///     selected to be added to the identifier.
        /// </revision>
        public bool Set(IdField field, object val)
        {
            bool success = false;

            if (this.IsFieldSelected(field) || (IdField.ControlBallot == field))
            {
                if (this.fieldMap.ContainsKey(field) == true)
                {
                    this.fieldMap.Remove(field);
                }

                this.fieldMap.Add(field, val);
                success = true;
            }

            return success;
        }

        /// <summary>
        ///     Gets the specified field.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <returns>
        ///     The specified field.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="4/8/2009" version="1.0.10.0501">
        ///     Comments added
        /// </revision>
        public object Get(IdField field)
        {
            object val = null;
            if (this.fieldMap.ContainsKey(field) == true)
            {
                val = this.fieldMap[field];
            }

            return val;
        }

        /// <summary>
        ///     Removes the specified field.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="11/2/2009" version="1.1.2.1101">
        ///     Member Created
        /// </revision>
        public void Remove(IdField field)
        {
            if (this.fieldMap.ContainsKey(field) == true)
            {
                this.fieldMap.Remove(field);
            }
        }

        /// <summary>
        ///     Parses the specified identifier.
        ///     the string format is as follows:
        ///     key1:value1|key2:value2|key3:value3|...
        ///     IMPORTANT: this method depends on EnumTextAttribute class and 
        ///     hence all IdFields texts have to be used first.
        /// </summary>
        /// <param name="identifier">The identifier.</param>
        /// <returns>
        ///     A <see cref="PaperBallotIdentifier" /> containing the specified
        ///     identifier.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="12/24/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public static PaperBallotIdentifier Parse(string identifier)
        {
            PaperBallotIdentifier pbiId = new PaperBallotIdentifier();

            string[] pairs = identifier.Split(SepMajor),
                     pair;
            IdField key;

            foreach (string strPair in pairs)
            {
                pair = strPair.Split(SepMinor);
                if (pair.Length != 2)
                {
                    continue;
                }

                try
                {
                    key = (IdField) EnumTextAttribute.GetValue(pair[0]);
                    pbiId.Set(key, pair[1]);
                }
                catch (Exception)
                {
                    // simply ignore the exception and drop the current field
                    // since the field is not supported or is mispelled
                }
            }

            return pbiId;
        }

        /// <summary>
        ///     Determines whether a given identifier field is selected
        /// </summary>
        /// <param name="entrySetParams">
        ///     The parameters. See <see cref="StpParam"/>
        /// </param>
        /// <param name="idField">
        ///     The id field. See <see cref="IdField"/>
        /// </param>
        /// <returns>
        ///     <c>true</c> if the identifier field is selected; 
        ///     otherwise, <c>false</c>.
        /// </returns>
        /// <externalUnit cref="BallotEntrySet"/>
        /// <externalUnit cref="Enum"/>
        /// <externalUnit cref="PdfParam"/>
        /// <externalUnit cref="StpParam"/>
        /// <revision revisor="dev11" date="3/4/2009" version="1.0.">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev11" date="03/24/2009" version="1.0.9.0801">
        ///     Updated to work with enum types and ids instead of names when 
        ///     searching on the entry set of pdf parameters.
        /// </revision>
        public static bool IsFieldSelected(BallotEntrySet entrySetParams, Enum idField)
        {
            int idx = entrySetParams.FindIndex(
                StpParam.PDFLayoutParamId, (int) PdfParam.IdentifierMask),
                field = Convert.ToInt32(idField),
                mask;
            bool selected = false;

            if (idx >= 0)
            {
                mask = entrySetParams.GetValueInt(idx, StpParam.ParamValue);
                selected = (mask & field) == field;
            }

            return selected;
        }

        /// <summary>
        ///     Determines whether [is field selected] [the specified id field].
        /// </summary>
        /// <param name="idField">The id field.</param>
        /// <returns>
        ///     <c>true</c> if [is field selected] [the specified id field]; 
        ///     otherwise, <c>false</c>.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="3/4/2009" version="1.0.9.0801">
        ///     Member Created
        /// </revision>
        public bool IsFieldSelected(Enum idField)
        {
            int field = Convert.ToInt32(idField);
            bool selected = (this.mask & field) == field;
            return selected;
        }

        /// <summary>
        ///     Sets the mask.
        /// </summary>
        /// <param name="entrySetParams">The es params.</param>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="3/4/2009" version="1.0.">
        ///     Member Created
        /// </revision>
        public void SetMask(BallotEntrySet entrySetParams)
        {
            int idx = entrySetParams.FindIndex(
                StpParam.PDFLayoutParamId, (int) PdfParam.IdentifierMask);
            if (idx >= 0)
            {
                this.mask = 
                    entrySetParams.GetValueInt(idx, StpParam.ParamValue);
            }
        }

        /// <summary>
        ///     Determines whether the specified field is present on the 
        ///     identifier
        /// </summary>
        /// <param name="field">The field.</param>
        /// <returns>
        /// 	<c>true</c> if the specified field is present; 
        ///     otherwise, <c>false</c>.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="11/2/2009" version="1.1.2.1101">
        ///     Member Created
        /// </revision>
        public bool HasField(IdField field)
        {
            return this.fieldMap.ContainsKey(field);
        }

        /// <summary>
        ///     Clones this instance.
        /// </summary>
        /// <returns>
        ///     A clone of the <see cref="PaperBallotIdentifier'" />.
        /// </returns>
        /// <externalUnit cref="fieldMap"/>
        /// <externalUnit cref="KeyValuePair{TKey,TValue}"/>
        /// <externalUnit cref="PaperBallotIdentifier"/>
        /// <revision revisor="dev11" date="2/25/2009" version="1.0.8.0801">
        ///     Member Created
        /// </revision>
        public PaperBallotIdentifier Clone()
        {
            PaperBallotIdentifier identifier = new PaperBallotIdentifier();
            foreach (KeyValuePair<IdField, object> pair in this.fieldMap)
            {
                identifier.Set(pair.Key, pair.Value);
            }

            return identifier;
        }

        #endregion

        #region Public Events

        #endregion

        #region Private Methods

        #endregion

        #region Overriden Methods

        /// <summary>
        ///     Returns a <see cref="T:System.String"/> that represents the 
        ///     current <see cref="PaperBallotIdentifier"/>.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.String"/> that represents the current 
        ///     <see cref="T:System.Object"/>.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="12/24/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev06" date="1/10/2009" version="1.0.0.0">
        ///     Updated so that the card identifer only shows the language and 
        ///     the code - this will need to be revised when we figure out how 
        ///     the mask will work
        /// </revision>
        /// <revision revisor="dev11" date="02/27/2009" version="1.0.8.1001">
        ///     the restriction for including only [language] and [code] has 
        ///     been removed
        /// </revision>
        public override string ToString()
        {
            string identifier = string.Empty, field;
            IdField[] fields = Enum.GetValues(typeof(IdField)) as IdField[];
            for (int i = 0; i < fields.Length; i = i + 1)
            {
                if (this.fieldMap.ContainsKey(fields[i]) == true)
                {
                    if (identifier.Length > 0)
                    {
                        identifier = identifier + SepMajor;
                    }

                    if (fields[i] == IdField.ControlBallot)
                    {
                        field =
                            Convert.ToInt32(
                            this.fieldMap[fields[i]]).ToString();
                    }
                    else
                    {
                        field = this.fieldMap[fields[i]].ToString();
                    }

                    identifier = identifier
                                 + EnumTextAttribute.GetText(fields[i])
                                 + SepMinor + field;
                }
            }

            return identifier;
        }

        #endregion
    }
}
