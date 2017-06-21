// -----------------------------------------------------------------------------
// <copyright file="BaseEntrySet.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2008 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the BaseEntrySet class.
// </summary>
// <revision revisor="dev11" date="10/20/2008" version="1.0.0.0">
//     File Created
// </revision>
// <revision revisor="dev13" date="11/17/2009" version="1.1.3.5">
//     File modified
// </revision>
// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
//     File modified
// </revision>
// -----------------------------------------------------------------------------

namespace Sequoia.Ems.Data.Custom
{
    #region Using directives

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    using Sequoia.EMS.Core.DataServices;

    using SqlDatabase = Microsoft.Practices.EnterpriseLibrary.Data.Sql;

    #endregion

    /// <summary>
    ///	    An entry set abstract read operations to the DB from the application 
    ///     side This implementation provides an easy-to-use and light object to 
    ///     handle DB read operations.
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev11" date="10/20/2008" version="1.0.0.0">
    ///     Class created.
    /// </revision>
    /// <revision revisor="dev13" date="11/17/2009" version="1.1.3.5">
    ///     Formatting changes
    /// </revision>
    /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
    ///     Formatting changes
    /// </revision>
    public abstract class BaseEntrySet : BaseDataService
    {
        #region Constants

        /// <summary>
        ///     error code to use when loading from the database fails
        /// </summary>
        public const int ErrorLoadingRset = 1; // 1

        /// <summary>
        ///     error code to use when writing to the database fails
        /// </summary>
        public const int ErrorSavingChanges = 1 << 1; // 2

        /// <summary>
        ///     error code to use when closing an open connection fails
        /// </summary>
        public const int ErrorClosingConnection = 1 << 2; // 4
        
        #endregion

        #region Fields

        /// <summary>
        ///     this hashtable contains the corresponding SQL statement 
        ///     (hashtable object, string) used to update a column 
        ///     (hashtable key, enum type) value, for all columns that are 
        ///     updated for a specific recordset type.
        /// </summary>
        protected Hashtable hstSql;

        /// <summary>
        ///     contains pre-built SQL statements that need to be execute to 
        ///     save changes to the recordset.
        /// </summary>
        protected List<int> modifiedEntries;

        /// <summary>
        ///     this object contains the information on how to save changes 
        ///     to the database.
        /// </summary>
        protected EntrySetUpdate esuInfo;

        /// <summary>
        ///     SQL query executed to populate the record set.
        /// </summary>
        protected string sqlLoad;

        /// <summary>
        ///     database connector string.
        /// </summary>
        protected string strConnection;

        /// <summary>
        ///     Fields for properties param for type 
        ///     (this is one of the Qry* enum types).
        /// </summary>
        protected Type type;

        /// <summary>
        ///     param for number of columns.
        /// </summary>
        protected int columns;

        /// <summary>
        ///     param for the recordset entries. Each entry contains an object[]
        ///     with the values of a resulting entry from running the SQL 
        ///     statement that populates this recordset (sqlLoad). Each of these 
        ///     object[]contains primitive types according to the corresponding 
        ///     column data type.
        /// </summary>
        protected List<object[]> entries;

        /// <summary>
        ///     This is the list of parameters on the stored procedure. 
        ///     They are mapped in the order as they appear parameters[0] -> 
        ///     args[0] parameters[1] -> args[1]...
        /// </summary>
        protected string[] parameters;

        /// <summary>
        ///     this dictionary maps parameter positions to SQL types. This is 
        ///     used when assigning paratemers to the SqlCommand args[0] -> 0 -> 
        ///     typeMap[0]args[1] -> 1 -> typeMap[1]...
        /// </summary>
        protected SqlDbType[] typeMap;

        /// <summary>
        ///     in order to reduce complexity, a dictionary is used by the 
        ///     initializers to call the init method corresponding to the given 
        ///     entry set type.
        /// </summary>
        protected Dictionary<Type, InitHandler> initMethods;

        /// <summary>
        ///     column names
        /// </summary>
        private string[] columnNames;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="BaseEntrySet"/> class.
        /// </summary>
        /// <param name="type">The type to initialize.</param>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="10/20/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev11" date="04/15/2009" version="1.0.11.0501">
        ///     added column names
        /// </revision>
        public BaseEntrySet(Type type)
        {
            this.type = type;
            this.columns = Enum.GetValues(type).Length;

            this.entries = new List<object[]>();
            this.modifiedEntries = new List<int>();
            this.initMethods = new Dictionary<Type, InitHandler>();

            this.InitializeType();
            /* call the corresponding initializer. By putting the call here
             * the implementation is forcing to have InitializeType method
             * implemented on the child class and making sure all initializers
             * are added to the dictionary */
            this.initMethods[type]();

            this.columnNames = Enum.GetNames(type);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="BaseEntrySet"/> class.
        /// </summary>
        /// <param name="type">The type to initialize.</param>
        /// <param name="connection">The connection.</param>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="10/20/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public BaseEntrySet(Type type, string connection) : this(type)
        {
            this.strConnection = connection;
        }

        #endregion

        #region Delegates

        /// <summary>
        ///     this delegate is used by the subclases to populate the 
        ///     initMethod dictionary <see cref="BaseEntrySet.initMethods"/>
        /// </summary>
        protected delegate void InitHandler();

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the columns.
        /// </summary>
        /// <value>The columns.</value>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="10/20/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public int Columns
        {
            get
            {
                return this.columns;
            }
        }

        /// <summary>
        ///     Gets the entries.
        /// </summary>
        /// <value>The entries.</value>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="10/20/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public List<object[]> Entries
        {
            get
            {
                return this.entries;
            }
        }

        /// <summary>
        ///     Gets the count.
        /// </summary>
        /// <value>The count.</value>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="10/20/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public int Count
        {
            get
            {
                return this.entries.Count;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     For each column (field) in the sqlReader data set add the values
        ///     to an entry array and then append the entry array (row of data) 
        ///     to the entries array.
        /// </summary>
        /// <param name="sqlReader">
        ///     Contains a row of data from the database.
        /// </param>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="10/20/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev11" date="04/03/2009" version="1.0.10.0101">
        ///     changed to prevent the addition of columns to stored procs from
        ///     breaking this method.
        /// </revision>
        /// <revision revisor="dev11" date="04/15/2009" version="1.0.11.0501">
        ///     columns are referred by name instead of by index. The name of 
        ///     the column is the name of the enum member.
        /// </revision>
        /// <revision revisor="dev11" date="09/11/2009" version="1.0.17.0401">
        ///     Parameter changed from [SqlDataReader] to [IDataReader].
        /// </revision>
        public void AddEntry(IDataReader sqlReader)
        {
            try
            {
                // declare an object array of size columns to 
                // hold record set values
                object[] entry = new object[this.columns];

                // count of columns in the record set
                int countOfColumnsInReader = sqlReader.FieldCount,
                    totalColumns = Math.Min(
                        this.columns, countOfColumnsInReader);

                for (int i = 0; i < totalColumns; i++)
                {
                    entry[i] = sqlReader[this.columnNames[i]];
                }

                // add the data columns to the entries array
                this.entries.Add(entry);
            }
            catch (Exception)
            {
                this.ShowFriendlyError(EntrySetErrorCode.UnexpectedError);
            }
        }

        /// <summary>
        ///     Reads a record from the current EntrySet, passed in on sqlReader
        ///     and calls AddEntry to have the columns added to the 
        ///     entries array.
        /// </summary>
        /// <param name="sqlReader">data access record reader object</param>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="10/20/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev11" date="09/11/2009" version="1.0.17.0401">
        ///     Parameter changed from [SqlDataReader] to [IDataReader]
        /// </revision>
        public void AddEntries(IDataReader sqlReader)
        {
            while (sqlReader.Read())
            {
                this.AddEntry(sqlReader);
            }
        }

        /// <summary>
        ///     Returns the first found entry index where [property] is [val],
        ///     or -1 if none is found
        /// </summary>
        /// <param name="property">
        ///     corresponds to one of the columns of the entry set.
        /// </param>
        /// <param name="val">Either a Contest Id or a Style Id</param>
        /// <returns>
        ///     Returns the index of the property in the entries array.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="10/20/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public int FindIndex(Enum property, int val)
        {
            // start from -1 as the first statement inside the loop 
            // will increment it by 1 and this is just a indexer that is 
            // incremented sequentially.
            int idx = -1,
                intProp = Convert.ToInt32(property);

            try
            {
                int obj;    // working 32 bit integer variable
                do
                {
                    // first increment, then test. This ensures that when the 
                    // right entry is found, the index used will not change
                    idx = idx + 1;
                    obj =
                        Convert.ToInt32(((object[]) this.entries[idx])[
                        intProp]);
                } 
                while (obj != val);
            }
            catch
            {
                idx = -1;
            }

            return idx;
        }

        /// <summary>
        ///     Returns the first found entry index where [property n] is 
        ///     [val n], or -1 if none is found. This way not just 1 value is 
        ///     compared but as many pairs as passed are compared to find the 
        ///     desired entry.
        /// </summary>
        /// <param name="enumObjectPairs">
        ///     The enum object pairs. Each pair is a [enum property] 
        ///     and a [value].
        /// </param>
        /// <returns>
        ///     The first found entry index.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="10/20/2008" version="1.0.0.0">
        /// Member Created</revision>
        public int FindIndex(params object[] enumObjectPairs)
        {
            // start from -1 as the first statement inside the loop 
            // will increment it by 1 and this is just a indexer that is 
            // incremented sequentially
            int idx = -1,
                count = this.entries.Count,
                pairsCount = enumObjectPairs.Length,
                column;

            bool isMatch = false;

            object entryObj, paramObj;

            try
            {
                // while not found and still more entries available on the collection
                // test the current entry for the given criteria
                while ((isMatch == false) && (idx < count))
                {
                    // increment the index by 1
                    idx = idx + 1;

                    // for each entry reset [isMatch] to true
                    isMatch = true;

                    // since each pair represents [property] and [value], 
                    // increment [j] by 2 to go to the next pair
                    for (int j = 0; j < pairsCount; j = j + 2)
                    {
                        // get the column index from the enum property value
                        column = Convert.ToInt32(enumObjectPairs[j]);

                        // get the found object in that position
                        entryObj = this.entries[idx][column];

                        // get the expected value for this column
                        paramObj = enumObjectPairs[j + 1];

                        // compare found and expected
                        isMatch = isMatch
                                  && entryObj.Equals(paramObj);
                    }
                }
            }
            catch
            {
                idx = -1;
            }

            // make sure the search ended because a match was found
            if (isMatch == false)
            {
                idx = -1;
            }

            return idx;
        }

        /// <summary>
        ///     Checks to see if the 'property' at 'idx' in array entries is set
        /// </summary>
        /// <param name="idx">Index value in entries to examine</param>
        /// <param name="property">
        ///     corresponds to one of the columns of the entry set.
        /// </param>
        /// <returns>
        ///     Returns an Int32 property value otherwise it returns -1.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="10/20/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public int GetValueInt(int idx, Enum property)
        {
            // create param for return value
            int valueAsInt;

            try
            {
                // get the desired object value from out entry list
                object obj =
                    ((object[]) this.entries[idx])[Convert.ToInt32(property)];

                // check the value in case it is null
                if (DBNull.Value.Equals(obj))
                {
                    // the value was null; set retur value to smallest possible 
                    // int value so it is init'd
                    valueAsInt = int.MinValue;
                }
                else
                {
                    // convert the value to an int
                    valueAsInt = Convert.ToInt32(obj);
                }
            }
            catch (Exception)
            {
                this.ShowFriendlyError(EntrySetErrorCode.UnexpectedError);
                valueAsInt = int.MinValue;
            }

            // return the value
            return valueAsInt;
        }

        /// <summary>
        ///     Checks to see if the 'property' at 'idx' in array entries is set
        /// </summary>
        /// <param name="idx">Index value in entries to examine</param>
        /// <param name="property">
        ///     corresponds to one of the columns of the entry set.
        /// </param>
        /// <returns>
        ///     Returns a Double property value otherwise it returns -1.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="10/20/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public double GetValueDouble(int idx, Enum property)
        {
            // create param for return value
            double valueAsDouble;

            // get the desired object value from out entry list
            object obj =
                ((object[]) this.entries[idx])[Convert.ToInt32(property)];

            // check the value in case it is null
            if (DBNull.Value.Equals(obj))
            {
                // the value was null; set return value to smallest possible 
                // double value so it is init'd
                valueAsDouble = double.NaN;
            }
            else
            {
                // convert the value to a double
                valueAsDouble = Convert.ToDouble(obj);
            }

            // return the value
            return valueAsDouble;
        }

        /// <summary>
        ///     Gets the value as date.
        /// </summary>
        /// <param name="idx">The idx value.</param>
        /// <param name="property">The property.</param>
        /// <returns>
        ///     The value date.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="4/2/2009" version="1.0.9.1701">
        ///     Member Created
        /// </revision>
        public DateTime? GetValueDate(int idx, Enum property)
        {
            // create param for return value
            DateTime? valueAsDate;

            object obj =
                ((object[]) this.entries[idx])[Convert.ToInt32(property)];

            // check the value in case it is null
            if (DBNull.Value.Equals(obj))
            {
                // the value is null
                valueAsDate = null;
            }
            else
            {
                valueAsDate = Convert.ToDateTime(obj);
            }

            return valueAsDate;
        }

        /// <summary>
        ///     Returns the value in array entries at index 'idx' for property
        ///     'property'
        /// </summary>
        /// <param name="idx">Index value in entries to examine</param>
        /// <param name="property">
        ///     corresponds to one of the columns of the entry set.
        /// </param>
        /// <returns>
        ///     Returns a String property regardless if the property
        ///     value has been set
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="10/20/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public string GetValueStr(int idx, Enum property)
        {
            return ((object[])
                this.entries[idx])[Convert.ToInt32(property)].ToString();
        }

        /// <summary>
        ///     Sets the value and adds the entry index to the pool of modified
        ///     entries whose changes are saved at a later time
        /// </summary>
        /// <param name="idx">The idx value.</param>
        /// <param name="property">The property.</param>
        /// <param name="val">The value.</param>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="4/8/2009" version="1.0.10.0501">
        ///     comments added
        /// </revision>
        public void SetValue(int idx, Enum property, object val)
        {
            // set the value on the entries array
            ((object[]) this.entries[idx])[Convert.ToInt32(property)] = val;

            // mark this entry as modified
            // Note: this approach assumes that the entire entry is updated to
            // the database
            // See EntrySetUpdate
            if (this.modifiedEntries.Contains(idx) == false)
            {
                this.modifiedEntries.Add(idx);
            }
        }

        /// <summary>
        ///     Loads the specified STR connection.
        /// </summary>
        /// <param name="strConnection">The STR connection.</param>
        /// <param name="args">The arguments.</param>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="10/20/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev11" date="09/11/2009" version="1.0.17.0401">
        ///     Refactored to use members provided 
        ///     by <see cref="BaseDataService"/>
        /// </revision>
        /// <revision revisor="dev11" date="09/17/2009" version="1.0.17.1001">
        ///     Added error service usage
        /// </revision>
        /// <revision revisor="dev11" date="09/22/2009" version="1.0.17.1501">
        ///     Added missing optional paramaters as explicit NULLs. Without 
        ///     this <see cref="BaseDataService.GetReader(string,object[])"/> 
        ///     throws an exception when not all parameters are present even 
        ///     if they are optional.
        /// </revision>
        public void Load(string strConnection, params object[] args)
        {
            try
            {
                this.strConnection = strConnection;
                if (this.db == null)
                {
                    this.CreateDatabase(strConnection);
                }

                args = this.SetOptionalArgsToNull(args);
                IDataReader reader = this.GetReader(this.sqlLoad, args);

                // empty the entries array
                this.entries.Clear();

                // populate the entry set
                this.AddEntries(reader);
            }
            catch(Exception ex)
            {
                this.ShowFriendlyError(EntrySetErrorCode.LoadError);
            }
        }

        /// <summary>
        ///     Saves the changes.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="4/8/2009" version="1.0.10.0501">
        ///     Comments added
        /// </revision>
        /// <revision revisor="dev11" date="09/17/2009" version="1.0.17.1001">
        ///     Added error service usage
        /// </revision>
        public void SaveChanges()
        {
            if (this.modifiedEntries.Count > 0)
            {
                SqlConnection sqlConnection =
                    new SqlConnection(this.strConnection);
                SqlTransaction sqlTransaction = null;

                try
                {
                    sqlConnection.Open();
                    sqlTransaction = sqlConnection.BeginTransaction();

                    SqlCommand sqlCommand =
                        new SqlCommand(
                            this.esuInfo.SqlStoredProc,
                            sqlConnection,
                            sqlTransaction);
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    foreach (int idx in this.modifiedEntries)
                    {
                        this.InitializeCommand(
                            sqlCommand, this.esuInfo, this.Entries[idx]);
                        string sql = sqlCommand.CommandText;
                        sqlCommand.ExecuteScalar();
                    }

                    sqlTransaction.Commit();
                    this.modifiedEntries.Clear();
                    sqlConnection.Close();
                }
                catch (Exception ex)
                {
                    try
                    {
                        // at least 1 command did not execute succesfully, so
                        // rollback transaction and close connection
                        sqlTransaction.Rollback();
                        sqlConnection.Close();
                    }
                    finally
                    {
                        this.ShowFriendlyError(
                            EntrySetErrorCode.SaveChangesError);
                    }
                }
            }
        }

        #endregion

        #region Public Events

        #endregion

        #region Private Methods

        /// <summary>
        ///     Each subclass has to implement this method which is where all 
        ///     properties are set for each enum type defined in the subclass
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="10/20/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        protected abstract void InitializeType();

        /// <summary>
        ///     Initializes the SQL.
        /// </summary>
        /// <param name="sqlCommand">Command to execute</param>
        /// <param name="args">The arguments.</param>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="10/20/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        private void InitializeCommand(
            SqlCommand sqlCommand,
            params object[] args)
        {
            SqlParameterCollection sqlParams = sqlCommand.Parameters;
            for (int i = 0; i < this.parameters.Length; i = i + 1)
            {
                sqlParams.Add(this.parameters[i], this.typeMap[i]);
                sqlParams[this.parameters[i]].Value = args[i];
            }
        }

        /// <summary>
        ///     Initializes the command.
        /// </summary>
        /// <param name="sqlCommand">The SQL command.</param>
        /// <param name="esuInfo">The esu info.</param>
        /// <param name="args">The arguments.</param>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="4/8/2009" version="1.0.10.0501">
        /// Comments added</revision>
        private void InitializeCommand(
            SqlCommand sqlCommand, 
            EntrySetUpdate esuInfo, 
            params object[] args)
        {
            SqlParameterCollection sqlParams = sqlCommand.Parameters;
            int i = 0;
            foreach (KeyValuePair<string, SqlDbType> parm in esuInfo.SqlParams)
            {
                sqlParams.Add(parm.Key, parm.Value);
                sqlParams[parm.Key].Value = args[i];
                i = i + 1;
            }
        }

        /// <summary>
        ///     Shows the friendly error.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        private void ShowFriendlyError(EntrySetErrorCode errorCode)
        {
            //if (this.ErrorService != null)
            //{
            //    ErrorInfoItem errorInfo = this.ErrorService.GetError(
            //        errorCode, CultureInfo.CurrentUICulture.Name);
            //    MessageHelper.ShowFriendlyError(errorInfo.Convert());
            //}
        }

        /// <summary>
        ///     Sets the optional args to null. Without this
        ///     <see cref="BaseDataService.GetReader(string,object[])"/> 
        ///     throws an exception when not all parameters are present even if 
        ///     they are optional
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>
        ///     The new arguments
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev11" date="9/22/2009" version="1.0.17.1501">
        ///     Member Created
        /// </revision>
        private object[] SetOptionalArgsToNull(params object[] args)
        {
            object[] newArgs;
            int i = 0;
            if ((this.parameters != null)
                && (args != null)
                && (this.parameters.Length > args.Length))
            {
                newArgs = new object[this.parameters.Length];
                for (i = 0; i < args.Length; i = i + 1)
                {
                    newArgs[i] = args[i];
                }
            }
            else
            {
                newArgs = args;
            }

            return newArgs;
        }

        #endregion
    }
}
