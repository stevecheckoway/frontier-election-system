//-----------------------------------------------------------------------------
// <copyright file="BaseDataService.cs" 
//  company="Sequoia Voting Systems">
//     Copyright (c) 2008 Sequoia Voting Systems. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <revision revisor="dev13" date="10/6/2008" version="1.0.0.0">
//     File created
// </revision>
// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
//     File created
// </revision>
// <revision revisor="dev14" date="4/20/2009" version="1.0.11.08">
//     File Modified
// </revision>
// <revision revisor="dev14" date="5/21/2009" version="1.0.12.11">
//     File Modified
// </revision>
// <revision revisor="dev14" date="9/09/2009" version="1.0.17.02">
//     File Modified
// </revision>
// <revision revisor="dev22" date="03/08/2010" version="1.1.7.15">
//     File Modified
// </revision>
//-----------------------------------------------------------------------------

namespace Sequoia.EMS.Core.DataServices
{
    #region Using Directives

    using System;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;

    using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
    using Sequoia.EMS.Core.DataServices.Extensions;
    using Sequoia.EMS.Core.Exceptions;
    #endregion

    /// <summary>
    ///     BaseDataService is a base class for implementing data services.
    /// </summary>
    /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
    ///     Class created
    /// </revision>
    /// <revision revisor="dev14" date="4/20/2009" version="1.0.11.08">
    ///     Updated to accept connection string to connect to election db
    /// </revision>
    /// <revision revisor="dev14" date="5/21/2009" version="1.0.12.11">
    ///     Cleaned up some build warnings
    /// </revision>
    /// <revision revisor="dev22" date="03/08/2010" version="1.1.7.15">
    /// Removed DB access methods that took a boolean parameter that would
    /// hide exceptions and changed remaining methods to use new method
    /// <see cref="Execute{Result}"/> for common error handling.
    /// </revision>
    public class BaseDataService : IDataService
    {
        #region Fields

        /// <summary>
        ///     set up param for sql database reference
        /// </summary>
        protected SqlDatabase db = null;

        /// <summary>
        ///     create var to hold a current command if command will allow 
        ///     cancelling.
        /// </summary>
        protected DbCommand currentSqlCommand = null;

        /// <summary>
        ///     param for data service definition used for current connection
        /// </summary>
        private DataServiceDefinition dataServiceDefinition = null;

        /// <summary>
        ///     set default timeout to 30 seconds
        /// </summary>
        private int defaultTimeout = 30;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="BaseDataService"/> class.
        /// </summary>
        /// <externalUnit />
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public BaseDataService()
        {
        }

        #endregion

        #region Events

        /// <summary>
        /// Occurs when the service is attempting to reconnect to a data source.
        /// </summary>
        public event EventHandler<AuthorizationEventArgs> AuthorizationRequired;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the data service definition.
        /// </summary>
        /// <value>The data service definition.</value>
        /// <externalUnit cref="dataServiceDefinition"/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public DataServiceDefinition DataServiceDefinition
        {
            get { return this.dataServiceDefinition; }
            set { this.dataServiceDefinition = value; }
        }

        /// <summary>
        ///     Gets or sets the default timeout in seconds. 
        ///     Unlimited = 0 [not recommended].
        /// </summary>
        /// <value>The default timeout [in seconds].</value>
        /// <externalUnit cref="defaultTimeout"/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public virtual int DefaultTimeout
        {
            get
            {
                return this.defaultTimeout;
            }

            set
            {
                // check to see if value is allowable first
                // 0 means unlimited timeout, so include it for now.
                if (value >= 0 && value <= Int32.MaxValue)
                {
                    this.defaultTimeout = value;
                }
            }
        }

        #endregion

        #region Methods

        #region CreateDatabase
        
        /// <summary>
        ///     Creates the database.
        /// </summary>
        /// <param name="serverName">Name of the server.</param>
        /// <param name="databaseName">Name of the database.</param>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <externalUnit cref="db"/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public void CreateDatabase(
            string serverName, 
            string databaseName, 
            string username, 
            string password)
        {
            // create the connection string from the input paramters and load it.
            string connectionString =
                string.Format(
                    "Data Source={0};Initial Catalog={1};"
                    + "User ID={2};Password={3}",
                    serverName,
                    databaseName,
                    username,
                    password);

            // set the db param
            this.db = new SqlDatabase(connectionString);
        }

        /// <summary>
        ///     Creates the database.
        /// </summary>
        /// <param name="dataDefinition">The data service definition.</param>
        /// <externalUnit cref="db"/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        public void CreateDatabase(DataServiceDefinition dataDefinition)
        {
            // create the database
            this.db = new SqlDatabase(dataDefinition.ConnectionString);
        }

        /// <summary>
        /// Creates the database using an existing connection string
        /// </summary>
        /// <param name="connectionString">
        ///     The connection string.
        ///     <example>
        ///         "Data Source=myDataSource;Initial Catalog=myDatabase;"
        ///         "User ID=sa;Password=myPassword"
        ///     </example>
        /// </param>
        /// <externalUnit cref="db" />
        /// <externalUnit cref="SqlDatabase" />
        /// <revision revisor="dev14" date="4/20/2009" version="1.0.11.06">
        ///     Member Created
        /// </revision>
        public void CreateDatabase(string connectionString)
        {
            this.db = new SqlDatabase(connectionString);
        }

        #endregion

        #region IAuthorizationRequestor Members

        /// <summary>
        /// Addds the given object to this object's authorization request.
        /// </summary>
        /// <param name="requestor">The object to attach to this object.</param>
        /// <revision revisor="dev22" date="03/10/2010" version="1.1.7.17">
        /// Member added.
        /// </revision>
        public void Attach(IAuthorizationRequestor requestor)
        {
            requestor.AuthorizationRequired += (sender, e) =>
                                          this.AuthorizationRequired(sender, e);
        }

        #endregion

        #region SetTargetDatabaseAsElection
        /// <summary>
        ///     Checks the current database connection, and if it is set to the 
        ///     profile db, changes the connection to use the election db.
        /// </summary>
        /// <externalUnit cref="DataServiceDefinition"/>
        /// <externalUnit cref="db"/>
        /// <externalUnit cref="CreateDatabase(DataServices.DataServiceDefinition)"/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        protected void SetTargetDatabaseAsElection()
        {
            if (this.dataServiceDefinition == null)
            {
                throw new InvalidOperationException("The data service " +
                                          "definition cannot be null.");
            }

            // explicitly set the election database
            if (DataServiceDefinition.IsUsingProfileDatabase == true 
                || (DataServiceDefinition.IsUsingElectionDatabase == true))
            {
                // reset to use the election db, not the profile
                this.db = null;
                DataServiceDefinition.UseElectionDatabase();

                // create the database connection
                this.CreateDatabase(DataServiceDefinition);
            }

            if (this.db == null)
            {
                throw new InvalidOperationException("The db instance cannot " +
                                                    "be null.");
            }
        }

        /// <summary>
        ///     Sets the target database to the election database using a
        ///     provided connectionString
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <externalUnit cref="db"/>
        /// <externalUnit cref="CreateDatabase(string)"/>
        /// <revision revisor="dev14" date="4/20/2009" version="1.0.11.08">
        ///     Member Created
        /// </revision>
        protected void SetTargetDatabaseAsElection(string connectionString)
        {
            if (String.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException("connectionString");
            }

            // set the db using the connectionString
            this.CreateDatabase(connectionString);

            if (this.db == null)
            {
                throw new InvalidOperationException("The db instance cannot " +
                                                    "be null.");
            }
        }

        #endregion

        #region SetTargetDatabaseAsProfile
        /// <summary>
        ///     Checks the current database connection, and if it is set to the 
        ///     election db, changes the connection to use the profile db.
        /// </summary>
        /// <externalUnit cref="DataServiceDefinition"/>
        /// <externalUnit cref="db"/>
        /// <externalUnit cref="CreateDatabase(DataServices.DataServiceDefinition)"/>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        protected void SetTargetDatabaseAsProfile()
        {
            // check db contract
            if (this.dataServiceDefinition == null)
            {
                throw new InvalidOperationException("The data service " +
                                          "definition cannot be null.");
            }

            // explicitly set the election database
            if (DataServiceDefinition.IsUsingElectionDatabase == true 
                || (DataServiceDefinition.IsUsingProfileDatabase == true))  
            {
                // reset to use the election db, not the profile
                this.db = null;
                DataServiceDefinition.UseProfileDatabase();

                // creat ethe database connection
                this.CreateDatabase(DataServiceDefinition);
            }

            // make sure we have a valid db
            if (this.db == null)
            {
                throw new InvalidOperationException("The db instance cannot " +
                                                    "be null.");
            }
        }
        #endregion

        #region GetReader
        /// <summary>
        ///     Gets the reader.
        /// </summary>
        /// <param name="procName">Name of the proc.</param>
        /// <param name="paramValues">The param values.</param>
        /// <returns>
        ///     An <see cref="IDataReader"/> with the operation results.
        /// </returns>
        /// <externalUnit cref="db"/>
        /// <externalUnit cref="DatabaseException"/>
        /// <exception cref="DatabaseException">
        ///     DatabaseException is a custom exception that wraps all our other 
        ///     sql exceptions.  It is handled by the default exception handler 
        ///     in the ui that offers different solutions for an unhandled db 
        ///     exception. For instance, the user can opt to log out and try the
        ///     operation again.
        /// </exception>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        /// <revision revisor="dev22" date="03/08/2010" version="1.1.7.15">
        /// Changed to use new method <see cref="Execute{Result}"/> to leverage
        /// its error handling functionality and cleaned up unneeded code.
        /// </revision>
        protected IDataReader GetReader(
            string procName, 
            params object[] paramValues)
        {
            return Execute(() =>
            {
                // updated 1/10/2008
                // create sql command from input, this will automatically 
                // have the return value
                // parameter included.
                if (paramValues == null || paramValues.Length == 0)
                {
                    this.currentSqlCommand = 
                        this.db.GetStoredProcCommand(procName);
                }
                else
                {
                    this.currentSqlCommand =
                        this.db.GetStoredProcCommand(procName, paramValues);
                }

                // set time out value
                if (this.currentSqlCommand != null)
                {
                    this.currentSqlCommand.CommandTimeout = 
                        this.DefaultTimeout;
                }

                // run the proc 
                return db.ExecuteReader(currentSqlCommand);
            });
        }
        #endregion

        #region RunScalarProcedure
        /// <summary>
        ///     Runs the scalar procedure.
        /// </summary>
        /// <param name="procName">Name of the proc.</param>
        /// <param name="paramValues">The param values.</param>
        /// <returns>The result of the operation.</returns>
        /// <externalUnit cref="db"/>
        /// <externalUnit cref="DatabaseException"/>
        /// <exception cref="DatabaseException">
        ///     DatabaseException is a custom exception 
        ///     that wraps all our other sql exceptions.  It is handled by the 
        ///     default exception handler in the ui that offers different 
        ///     solutions for an unhandled db exception. For instance, the user 
        ///     can opt to log out and try the operation again.
        /// </exception>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        /// <revision revisor="dev14" date="9/9/2009" version="1.0.17.02">
        ///     Removed retry logic
        /// </revision>
        protected object RunScalarProcedure(
            string procName, 
            params object[] paramValues)
        {
            return Execute(() =>
            {
                // create sql command from input, this will automatically have 
                // the return value parameter included.
                DbCommand sqlCommand =
                    this.db.GetStoredProcCommand(procName, paramValues);

                // set time out value
                if (sqlCommand != null)
                {
                    sqlCommand.CommandTimeout = this.DefaultTimeout;
                }

                // run the proc to get the result
                return db.ExecuteScalar(sqlCommand);
            });
        }
        #endregion

        #region RunNonQueryProcedure
        /// <summary>
        ///     Runs the non query procedure.
        /// </summary>
        /// <param name="procName">Name of the proc.</param>
        /// <param name="paramValues">The param values.</param>
        /// <returns>The result of the db operation. 0 = success.</returns>
        /// <externalUnit cref="db"/>
        /// <externalUnit cref="DatabaseException"/>
        /// <exception cref="DatabaseException">
        ///     DatabaseException is a custom exception that wraps all our other 
        ///     sql exceptions.  It is handled by the default exception handler 
        ///     in the ui that offers different solutions for an unhandled db 
        ///     exception. For instance, the user can opt to log out and try the 
        ///     operation again.
        /// </exception>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        /// <revision revisor="dev14" date="9/9/2009" version="1.0.17.02">
        ///     Removed retry logic
        /// </revision>
        protected int RunNonQueryProcedure(
            string procName, 
            params object[] paramValues)
        {
            return this.RunNonQueryProcedure(null, procName, paramValues);
        }

        /// <summary>
        ///     Runs the non query procedure.
        /// </summary>
        /// <param name="transaction">The transaction.</param>
        /// <param name="procName">Name of the proc.</param>
        /// <param name="paramValues">The param values.</param>
        /// <returns>
        ///     The result of the db operation. 0 = success.
        /// </returns>
        /// <externalUnit cref="db"/>
        /// <externalUnit cref="DatabaseException"/>
        /// <exception cref="DatabaseException">
        ///     DatabaseException is a custom exception that wraps all our other
        ///     sql exceptions.  It is handled by the default exception handler
        ///     in the ui that offers different solutions for an unhandled db
        ///     exception. For instance, the user can opt to log out and try the
        ///     operation again.
        /// </exception>
        /// <revision revisor="dev06" date="10/6/2008" version="1.0.0.0">
        ///     Member created
        /// </revision>
        /// <revision revisor="dev14" date="9/9/2009" version="1.0.17.02">
        ///     Removed retry logic
        /// </revision>
        protected int RunNonQueryProcedure(
            DbTransaction transaction,
            string procName,
            params object[] paramValues)
        {
            // create param for proc result
            // default to -1 as result will be 0+ for records affected.
            int result = -1;

            return Execute(() =>
            {
                // create sql command from input, this will automatically 
                // have the return value parameter included.
                DbCommand sqlCommand =
                    this.db.GetStoredProcCommand(procName, paramValues);

                // set time out value
                if (sqlCommand != null)
                {
                    sqlCommand.CommandTimeout = this.DefaultTimeout;
                }

                // run the proc 
                db.ExecuteNonQuery(sqlCommand);

                // get the return code value
                object returnCode =
                    this.db.GetParameterValue(sqlCommand, "@RETURN_VALUE");

                // get the real return code value as int
                if (returnCode != null
                    && Int32.TryParse(
                        returnCode.ToString(),
                        out result) == false)
                {
                    // failed, set code to -1
                    result = -1;
                }

                return result;
            });
        }
        #endregion

        #region Execute

        /// <summary>
        /// Executes a database operation and provides uniform error handling
        /// for specific errors.
        /// </summary>
        /// <typeparam name="Result">The type of result returned by the
        /// operation to be executed.
        /// </typeparam>
        /// <param name="dbOperation">The operation to execute (presumably a
        /// DB operation).
        /// </param>
        /// <exception cref="DatabaseException">Thrown if a database error
        /// occurs while executing the operation.
        /// </exception>
        /// <returns>The result of executing <paramref name="dbOperation"/>.
        /// </returns>
        /// <remarks>This method is designed to take in any database operation.
        /// In doing so it allows logic, such as error handling, to be applied
        /// to operations in a consistent way.
        /// The method has retry logic built in, but any error handler may
        /// bypass it by simply re-throwing the error.
        /// </remarks>
        /// <revision revisor="dev22" date="03/08/2010" version="1.1.7.11">
        /// Member added (work for US577).
        /// </revision>
        /// <revision revisor="dev22" date="03/08/2010" version="1.1.7.15">
        /// Refactored code into <see cref="Reauthorize"/> method and changed
        /// to wrap other exceptions in a <see cref="DatabaseException"/>.
        /// </revision>
        private Result Execute<Result>(Func<Result> dbOperation)
        {
            Result result = default(Result);
            const byte MaxRetries = 3;
            byte attempts = 0;
            bool failure = true;

            Func<Boolean> retry = () => failure && attempts <= MaxRetries;

            while (retry())
            {
                attempts++;

                try
                {
                    result = dbOperation();
                    failure = false;
                }
                catch (SqlException ex)
                {
                    if (retry() && ex.IsLoginError())
                    {
                        this.Reauthorize(ex);
                    }
                    else
                    {
                        throw ex.ToDBError();
                    }
                }
            }

            return result;
        }

        #endregion

        #region Reauthorize

        /// <summary>
        /// Raises an event requesting database authorization. If authorization
        /// succeeds, the database connection is recreated so the original
        /// operation can be retried.
        /// </summary>
        /// <param name="error">The original error that caused the authorization
        /// request.
        /// </param>
        /// <exception cref="DatabaseException">Thrown if the reauthorization
        /// attempt fails.
        /// </exception>
        /// <revision revisor="dev22" date="03/08/2010" version="1.1.7.15">
        /// Member added. (work for US577).
        /// </revision>
        private void Reauthorize(SqlException error)
        {
            var eventData = 
                new AuthorizationEventArgs(this.dataServiceDefinition);

            if (this.AuthorizationRequired != null)
            {
                try
                {
                    this.AuthorizationRequired(this, eventData);
                }
                catch (Exception ex)
                {
                    throw new DatabaseException(
                        String.Format(
                            "{0} {1}",
                            Properties.Resources.ErrorStartMessage,
                            Properties.Resources.ReauthorizationErrorMessage),
                        ex);
                }
            }

            if (eventData.New == null)
            {
                throw error.ToDBError(
                    String.Format(
                        "{0} {1}",
                        Properties.Resources.ErrorStartMessage,
                        Properties.Resources.MissingAuthorizationHandlerMessage));
            }

            this.dataServiceDefinition.Username = eventData.New.Username;
            this.dataServiceDefinition.SecureUsername = eventData.New.Username;

            this.dataServiceDefinition.Password = eventData.New.Password;
            this.dataServiceDefinition.SecurePassword = eventData.New.Password;

            this.CreateDatabase(this.dataServiceDefinition);
        }

        #endregion

        #endregion
    }
}
