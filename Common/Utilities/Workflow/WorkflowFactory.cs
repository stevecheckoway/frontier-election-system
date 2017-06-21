// -----------------------------------------------------------------------------
// <copyright file="WorkflowFactory.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the WorkflowFactory class.
// </summary>
// <revision revisor="dev14" date="8/6/2009" version="1.0.14.30">
//     File Created
// </revision>  
// -----------------------------------------------------------------------------

namespace Sequoia.Utilities.Workflow
{
    #region Using directives

    using System.Collections.Specialized;
    using System.Workflow.Runtime;
    using System.Workflow.Runtime.Hosting;

    #endregion

    /// <summary>
    ///     This class creates a singleton instance of the 
    ///     Workflow Runtime engine
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev14" date="8/6/2009" version="1.0.14.30">
    ///     Member Created
    /// </revision>
    public static class WorkflowFactory
    {
        /// <summary>
        ///     singleton instance of the workflow runtime
        /// </summary>
        private static WorkflowRuntime workflowRuntime = null;

        /// <summary>
        ///     lock object
        /// </summary>
        private static object syncRoot = new object();

        /// <summary>
        ///     Gets the workflow runtime.
        /// </summary>
        /// <returns>The runtime</returns>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="8/6/2009" version="1.0.14.30">
        ///     Member Created
        /// </revision>
        public static WorkflowRuntime GetWorkflowRuntime()
        {
            lock (syncRoot)
            {
                // if the instance of the runtime does not exist yet, create it
                if (workflowRuntime == null)
                {
                    workflowRuntime = new WorkflowRuntime();
                    workflowRuntime.StartRuntime();
                }

                // return the singleton instance of the workflow
                return workflowRuntime;
            }
        }

        /// <summary>
        ///     Gets the workflow runtime with an added persistence service
        /// </summary>
        /// <param name="connectionString">
        ///     The connection string to the persistence database.
        /// </param>
        /// <returns>The runtime</returns>
        /// <externalUnit/>
        /// <revision revisor="dev14" date="8/10/2009" version="1.0.15.04">
        ///     Member Created
        /// </revision>
        public static WorkflowRuntime GetWorkflowRuntime(
            string connectionString)
        {
            lock (syncRoot)
            {
                // if the instance of the runtime does not exist yet, create it
                if (workflowRuntime == null)
                {
                    workflowRuntime = new WorkflowRuntime();
                }

                var parms = new NameValueCollection();
                parms.Add("UnloadOnIdle", "true");
                parms.Add("ConnectionString", connectionString);

                workflowRuntime.AddService(
                    new SqlWorkflowPersistenceService(parms));
                workflowRuntime.StartRuntime();

                // return the singleton instance of the workflow
                return workflowRuntime;
            }
        }
    }
}
