// -----------------------------------------------------------------------------
// <copyright file="BallotPdfTestObject.cs" company="Sequoia Voting Systems">
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//   This file contains the BallotPdfTestObject class.
// </summary>
// <revision revisor="dev11" date="2/27/2009" version="1.0.0.0">
//    File Created
// </revision>
// -----------------------------------------------------------------------------

namespace UnitTests
{
    #region Using directives

    using System;
    using System.IO;
    using System.Xml.Serialization;

    using Sequoia.Ballot.Data;

    using PaperSide = Sequoia.Ballot.PaperSide;
    using PdfParam = Sequoia.Ballot.PdfParam;
    using StpBallot = Sequoia.Ballot.Data.BallotEntrySet.StpBallot;
    using StpCand = Sequoia.Ballot.Data.BallotEntrySet.StpCand;
    using StpCandList = Sequoia.Ballot.Data.BallotEntrySet.StpCandList;
    using StpCont = Sequoia.Ballot.Data.BallotEntrySet.StpCont;
    using StpContList = Sequoia.Ballot.Data.BallotEntrySet.StpContList;
    using StpParam = Sequoia.Ballot.Data.BallotEntrySet.StpParam;
    using StpParty = Sequoia.Ballot.Data.BallotEntrySet.StpParty;
    using StpTargetParam = Sequoia.Ballot.Data.BallotEntrySet.StpTargetParam;
    using TargetParam = Sequoia.Ballot.TargetParam;
    using TargetType = Sequoia.Ballot.TargetType;

    #endregion

    /// <summary>
    ///     Class for BallotPdfTestObject
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev11" date="2/27/2009" version="1.0.0.0">
    ///     Class created.
    /// </revision>
    public class BallotPdfTestObject
    {
        #region Constants

        /// <summary>
        ///     The connection string.
        /// </summary>
        public const string ConnectionString = "Data Source=[your data source];"
                                                + "Initial Catalog=[your db];"
                                                + "User Id=[your user];"
                                                + "Password=[your password]";
        #endregion
        
        #region Fields

        /// <summary>
        ///     The ballots.
        /// </summary>
        public BallotEntrySet ballots;

        /// <summary>
        /// The contests.
        /// </summary>
        public BallotEntrySet contests;

        /// <summary>
        /// The candidates.
        /// </summary>
        public BallotEntrySet candidates;

        /// <summary>
        /// The parameters.
        /// </summary>
        public BallotEntrySet parameters;

        /// <summary>
        /// The candlist.
        /// </summary>
        private BallotEntrySet candlist;

        /// <summary>
        /// The contlist.
        /// </summary>
        private BallotEntrySet contlist;

        /// <summary>
        /// The parties.
        /// </summary>
        private BallotEntrySet parties;

        /// <summary>
        /// The target.
        /// </summary>
        private PaperBallotTarget target;

        #endregion
        
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="BallotPdfTestObject"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="2/27/2009" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public BallotPdfTestObject()
        {
            this.ballots = new BallotEntrySet(typeof(StpBallot));
            this.contests = new BallotEntrySet(typeof(StpCont));
            this.candidates = new BallotEntrySet(typeof(StpCand));
            this.parameters = new BallotEntrySet(typeof(StpParam));

            this.candlist = new BallotEntrySet(typeof(StpCandList));
            this.contlist = new BallotEntrySet(typeof(StpContList));
            this.parties = new BallotEntrySet(typeof(StpParty));
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Clears this instance.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/19/2009" version="1.1.3.7">
        ///     Added documentation header
        /// </revision>
        public void Clear()
        {
            this.ballots.Entries.Clear();
            this.contests.Entries.Clear();
            this.candidates.Entries.Clear();
            this.parameters.Entries.Clear();

            this.candlist.Entries.Clear();
            this.contlist.Entries.Clear();
            this.parties.Entries.Clear();

            this.target = null;
        }

        /// <summary>
        ///     Sets the parameter.
        /// </summary>
        /// <param name="name">The parmater name.</param>
        /// <param name="value">The parameter value.</param>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/19/2009" version="1.1.3.7">
        ///     Added documentation header
        /// </revision>
        public void SetParameter(string name, object value)
        {
            int idx = this.parameters.FindIndex(StpParam.ParamName, name);
            this.parameters.SetValue(idx, StpParam.ParamValue, value);
        }

        /// <summary>
        ///     Sets the target.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="offsetX">The offset X.</param>
        /// <param name="offsetY">The offset Y.</param>
        /// <param name="lineWidth">Width of the line.</param>
        /// <param name="lineDensity">The line density.</param>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/19/2009" version="1.1.3.7">
        ///     Added documentation header
        /// </revision>
        public void SetTarget(
            double width, 
            double height, 
            double offsetX, 
            double offsetY, 
            double lineWidth, 
            double lineDensity)
        {
            BallotEntrySet targetParams =
                new BallotEntrySet(typeof(StpTargetParam));

            targetParams.Entries.Add(
                new object[]
                    {
                        1,
                        TargetParam.Width.ToString(), string.Empty,
                        width.ToString()
                    });

            targetParams.Entries.Add(
                new object[]
                    {
                        2,
                        TargetParam.Height.ToString(), string.Empty,
                        height.ToString()
                    });

            targetParams.Entries.Add(
                new object[]
                    {
                        3,
                        TargetParam.OffsetX.ToString(), string.Empty,
                        offsetX.ToString()
                    });

            targetParams.Entries.Add(
                new object[]
                    {
                        4,
                        TargetParam.OffsetY.ToString(), string.Empty,
                        offsetY.ToString()
                    });

            targetParams.Entries.Add(
                new object[]
                    {
                        5,
                        TargetParam.LineWidth.ToString(), string.Empty,
                        lineWidth.ToString()
                    });

            targetParams.Entries.Add(
                new object[]
                    {
                        6,
                        TargetParam.LineDensity.ToString(), string.Empty,
                        lineDensity.ToString()
                    });

            TargetType type;

            int idx = this.parameters.FindIndex(
                StpParam.PDFLayoutParamId, (int) PdfParam.TargetType);
            int val = this.parameters.GetValueInt(idx, StpParam.ParamValue);

            type = (TargetType) Enum.ToObject(typeof(TargetType), val);

            this.target = new PaperBallotTarget(targetParams, type);
        }

        #endregion

        #region Static Methods

        /// <summary>
        ///     Sets the identifier face.
        /// </summary>
        /// <param name="parameters">
        ///     The parameters.
        /// </param>
        /// <param name="side">
        ///     The paper side.
        /// </param>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="3/24/2009" version="1.0.9.0801">
        ///     Member Created
        /// </revision>
        public static void SetIdentifierFace(
            BallotEntrySet parameters, PaperSide side)
        {
            int idx = parameters.FindIndex(
                StpParam.PDFLayoutParamId, 
                (int) PdfParam.IdentifierLocation);
            if (idx < 0)
            {
                parameters.Entries.Add(
                    new object[]
                        {
                            1000, 
                            PdfParam.IdentifierLocation.ToString(), 
                            null, 
                            side.ToString()
                        });
            }
            else
            {
                parameters.Entries[idx][Convert.ToInt32(StpParam.ParamValue)] =
                    side.ToString();
            }
        }

        /// <summary>
        ///     Sets the identifier mask.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="mask">The indentifer mask.</param>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/19/2009" version="1.1.3.7">
        ///     Added documentation header
        /// </revision>
        public static void SetIdentifierMask(
            BallotEntrySet parameters, int mask)
        {
            int idx = parameters.FindIndex(
                StpParam.PDFLayoutParamId, 
                (int) PdfParam.IdentifierMask);
            if (idx < 0)
            {
                parameters.Entries.Add(
                    new object[]
                        {
                            1000, 
                            PdfParam.IdentifierMask.ToString(), 
                            null, 
                            mask
                        });
            }
            else
            {
                parameters.Entries[idx][Convert.ToInt32(StpParam.ParamValue)] =
                    mask;
            }
        }

        #endregion

        #region Private Methods

        #region Print

        /// <summary>
        ///     Prints the ballot entry set. To view this output open [Unit Test 
        ///     Sessions] window
        /// </summary>
        /// <param name="entrySet">
        ///     The entry set.
        /// </param>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="1/14/2009" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public static void PrintBallotEntrySet(BallotEntrySet entrySet)
        {
            for (int i = 0; i < entrySet.Count; i = i + 1)
            {
                object[] entry = entrySet.Entries[i];
                foreach (object obj in entry)
                {
                    Console.Write(obj.ToString().PadLeft(4, ' '));
                }

                Console.WriteLine();
            }
        }

        #endregion

        #region StpBallot

        /// <summary>
        ///     Adds a ballot to the ballot entry set with specific contests and
        ///     candidates for each contest
        /// </summary>
        /// <param name="ballotId">
        ///     The ballot id.
        /// </param>
        /// <param name="candCount">
        ///     The candidate count for each contest.
        /// </param>
        /// <param name="contOffset">
        ///     The contest id offset.
        /// </param>
        /// <param name="candOffset">
        ///     The candidate id offset.
        /// </param>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="2/6/2009" version="1.0.6.2">
        ///     Member Created
        /// </revision>
        public void AddStpBallotBallot(
            int ballotId, 
            int[] candCount,
            int contOffset,
            int candOffset)
        {
            for (int i = 0; i < candCount.Length; i = i + 1)
            {
                this.AddStpBallotContest(
                    ballotId,
                    contOffset + i,
                    1 + i,
                    0,
                    candCount[i],
                    candOffset);
                this.AddStpContList(contOffset + i);
                this.AddStpCandList(contOffset + i, candCount[i], candOffset);
                candOffset = candOffset + candCount[i];
            }
        }

        /// <summary>
        ///     Adds the STP ballot contest.
        /// </summary>
        /// <param name="ballotId">The ballot id.</param>
        /// <param name="contId">The cont id.</param>
        /// <param name="contOrd">The cont ord.</param>
        /// <param name="contFormat">The cont format.</param>
        /// <param name="candCount">The cand count.</param>
        /// <param name="candOffset">The cand offset.</param>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/19/2009" version="1.1.3.7">
        ///     Added documentation header
        /// </revision>
        public void AddStpBallotContest(
            int ballotId, 
            int contId, 
            int contOrd, 
            int contFormat, 
            int candCount, 
            int candOffset)
        {
            BallotEntrySet entrySet = this.ballots;
            for (int i = 0; i < candCount; i = i + 1)
            {
                this.AddStpBallotEntry(
                    entrySet,
                    ballotId,
                    contId,
                    contOrd,
                    contFormat,
                    candOffset + i,
                    1 + i,
                    0);
            }
        }

        /// <summary>
        ///     Adds the STP ballot entry.
        /// </summary>
        /// <param name="entrySet">The entry set.</param>
        /// <param name="ballotId">The ballot id.</param>
        /// <param name="contId">The cont id.</param>
        /// <param name="contOrd">The cont ord.</param>
        /// <param name="contFormat">The cont format.</param>
        /// <param name="candId">The cand id.</param>
        /// <param name="candOrd">The cand ord.</param>
        /// <param name="candFormat">The cand format.</param>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/19/2009" version="1.1.3.7">
        ///     Added documentation header
        /// </revision>
        public void AddStpBallotEntry(BallotEntrySet entrySet, 
            int ballotId, 
            int contId, 
            int contOrd, 
            int contFormat, 
            int candId, 
            int candOrd, 
            int candFormat)
        {
            entrySet.Entries.Add(
                new object[]
                    {
                        ballotId, 
                        contId, 
                        contOrd, 
                        contFormat, 
                        candId, 
                        candOrd,
                        candFormat
                    });
        }

        #endregion

        #region StpCont

        /// <summary>
        ///     Adds entries to a ballot entry set with office texts
        /// </summary>
        /// <param name="textCount">
        ///     The text count for each contest.
        /// </param>
        /// <param name="contOffset">
        ///     The contest id offset.
        /// </param>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="2/6/2009" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public void AddStpContBallot(int[] textCount, int contOffset)
        {
            BallotEntrySet entrySet = this.contests;
            for (int i = 0; i < textCount.Length; i = i + 1)
            {
                this.AddStpContContest(entrySet, contOffset + i, textCount[i]);
            }
        }

        /// <summary>
        ///     Adds the STP cont contest.
        /// </summary>
        /// <param name="entrySet">The entry set.</param>
        /// <param name="contId">The cont id.</param>
        /// <param name="textCount">The text count.</param>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/19/2009" version="1.1.3.7">
        ///     Added documentation header
        /// </revision>
        public void AddStpContContest(
            BallotEntrySet entrySet, int contId, int textCount)
        {
            for (int i = 0; i < textCount; i = i + 1)
            {
                this.AddStpContEntry(
                    entrySet, 
                    contId, 
                    string.Format("Text.{0}.{1}", contId, 1 + i), 
                    1 + i);
            }
        }

        /// <summary>
        ///     Adds the STP cont entry.
        /// </summary>
        /// <param name="entrySet">The entry set.</param>
        /// <param name="contId">The cont id.</param>
        /// <param name="text">The entry set text.</param>
        /// <param name="textOrd">The text ord.</param>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/19/2009" version="1.1.3.7">
        ///     Added documentation header
        /// </revision>
        public void AddStpContEntry(
            BallotEntrySet entrySet, 
            int contId, 
            string text, 
            int textOrd)
        {
            entrySet.Entries.Add(new object[] { contId, text, textOrd });
        }

        #endregion

        #region StpCand

        /// <summary>
        ///     Adds the STP cand ballot.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/19/2009" version="1.1.3.7">
        ///     Added documentation header
        /// </revision>
        public void AddStpCandBallot(int count)
        {
            this.AddStpCandBallot(count, 0);
        }

        /// <summary>
        ///     Adds the STP cand ballot.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <param name="offset">The offset.</param>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/19/2009" version="1.1.3.7">
        ///     Added documentation header
        /// </revision>
        public void AddStpCandBallot(int count, int offset)
        {
            BallotEntrySet entrySet = this.candidates;
            for (int i = 0; i < count; i = i + 1)
            {
                entrySet.Entries.Add(
                    new object[] { offset + i, "Candidate." + i, 0 });
            }
        }

        #endregion

        #region StpParams

        /// <summary>
        ///     Loads the STP params.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/19/2009" version="1.1.3.7">
        ///     Added documentation header
        /// </revision>
        public void LoadStpParams()
        {
            BallotEntrySet entrySet = this.parameters;
            string strConnection = ConnectionString;

            entrySet.Load(strConnection);
        }

        #endregion

        #region StpCandList

        /// <summary>
        ///     Adds the STP cand list.
        /// </summary>
        /// <param name="contId">The cont id.</param>
        /// <param name="candCount">The cand count.</param>
        /// <param name="candOffset">The cand offset.</param>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/19/2009" version="1.1.3.7">
        ///     Added documentation header
        /// </revision>
        public void AddStpCandList(int contId, int candCount, int candOffset)
        {
            BallotEntrySet entrySet = this.candlist;
            for (int i = 0; i < candCount; i = i + 1)
            {
                entrySet.Entries.Add(
                    new object[]
                        {
                            candOffset + i, 1 + i, contId, "Candidate." + i, 1,
                            1 + i, 1
                        });
            }
        }

        #endregion

        #region StpContList

        /// <summary>
        ///     Adds the STP cont list.
        /// </summary>
        /// <param name="contId">The cont id.</param>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/19/2009" version="1.1.3.7">
        ///     Added documentation header
        /// </revision>
        public void AddStpContList(int contId)
        {
            BallotEntrySet entrySet = this.contlist;
            entrySet.Entries.Add(
                new object[]
                    {
                        contId, 1, "Contest." + contId, 1, 0, 1, contId
                    });
        }

        #endregion

        #region StpParty

        /// <summary>
        ///     Adds the STP party.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/19/2009" version="1.1.3.7">
        ///     Added documentation header
        /// </revision>
        public void AddStpParty(int count)
        {
            BallotEntrySet entrySet = this.parties;
            entrySet.Entries.Add(new object[] { 0, "Non Partisan", "Non", 1 });
            for (int i = 1; i <= count; i = i + 1)
            {
                entrySet.Entries.Add(
                    new object[] { i, "Party " + i, "P" + i, i + 1 });
            }
        }

        #endregion

        #region XML Serialization

        /// <summary>
        ///     Serializes the specified path.
        /// </summary>
        /// <param name="path">The file path.</param>
        /// <param name="list">The object list.</param>
        /// <param name="type">The object type.</param>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/19/2009" version="1.1.3.7">
        ///     Added documentation header
        /// </revision>
        public void Serialize(string path, object list, Type type)
        {
            XmlSerializer serializer = new XmlSerializer(type);
            TextWriter writer = new StreamWriter(path);
            serializer.Serialize(writer, list);
            writer.Flush();
            writer.Close();
        }

        #endregion

        #endregion
    }
}
