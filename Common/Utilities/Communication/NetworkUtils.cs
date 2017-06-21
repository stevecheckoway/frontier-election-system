// -----------------------------------------------------------------------------
// <copyright file="NetworkUtils.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the NetworkUtils class.
// </summary>
// <revision revisor="dev01" date="2/2/2009 4:59:15 PM" version="1.0.5.14">
//     File Created
// </revision>  
// <revision revisor="dev05" date="09/09/09" version="1.0.17.2">
//     File modified.
// </revision>
// -----------------------------------------------------------------------------

namespace Sequoia.Utilities.Communication
{
    #region Using directives

    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading;

    using Sequoia.DomainObjects;

    #endregion

    /// <summary>
    ///	    Network utilities class
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev01" date="2/2/2009" version="1.0.5.14">
    ///     Class created.
    /// </revision>
    public class NetworkUtils
    {
        #region Fields

        /// <summary>
        ///     time out for TCP connection
        /// </summary>
        internal const int TcpTimeOut = 5000;

        /// <summary>
        ///     Reset event
        /// </summary>
        private static ManualResetEvent connectionDone =
            new ManualResetEvent(false);

        #endregion
        
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="NetworkUtils"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="2/2/2009" version="1.0.5.14">
        ///     Member Created
        /// </revision>	
        public NetworkUtils()
        {
        }

        #endregion

        #region Public Properties

        #endregion

        #region Public Methods

        /// <summary>
        ///     Checks the network.
        /// </summary>
        /// <param name="servers">The servers</param>
        /// <returns>
        ///     An <see cref="OperationResult" /> containing the result
        ///     of the operation.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="2/2/2009" version="1.0.5.14">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev01" date="4/29/2009" version="1.0.11.17">
        ///     Changed return value to operation result
        /// </revision>
        public static OperationResult CheckNetwork(string[] servers)
        {
            // return Value
            var returnValue = new OperationResult(false);

            try
            {
                for (int i = 0; i < servers.Length && 
                    returnValue.Succeeded == false; i++)
                {
                    var parseObject = new Uri(servers[i]);
                    if (servers[i] != null)
                    {
                        returnValue = CheckServer(
                            parseObject.Host,
                            parseObject.Port,
                            TcpTimeOut);
                    }
                }
            }
            catch (Exception exc)
            {
                returnValue = new OperationResult(false,exc.ToString());
            }

            return returnValue;
        }

        /// <summary>
        ///     Checks the server.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="port">The tcpip port.</param>
        /// <param name="connectionTimeout">The connection timeout.</param>
        /// <returns>
        ///     An <see cref="OperationResult" /> containing the result of 
        ///     the operation.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="5/14/2009" version="1.0.13.1">
        ///     Member Created
        /// </revision>
        public static OperationResult CheckServer(
            string server, 
            int port, 
            int connectionTimeout)
        {
            var returnValue = new OperationResult(false);
            connectionDone = new ManualResetEvent(false);
            try
            {
                TcpClient client = new TcpClient();
                client.BeginConnect(
                    server, port, new AsyncCallback(ConnectCallback), client);
                connectionDone.WaitOne(connectionTimeout, false);
                if (client.Connected)
                {
                    returnValue = new OperationResult(true);
                }
                else
                {
                    returnValue = new OperationResult(
                        false, "Server: " + server + " TimeOut");
                }

                client.Close();
            }
            catch(Exception exc)
            {
                returnValue = new OperationResult(false, exc.ToString());
            }

            return returnValue;
        }

        /// <summary>
        ///     Connects the callback.
        /// </summary>
        /// <param name="asyncResult">The Asynchronous Result.</param>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="5/14/2009" version="1.0.13.1">
        ///     Member Created
        /// </revision>
        private static void ConnectCallback(IAsyncResult asyncResult)
        {
            try
            {
                TcpClient client = (TcpClient)asyncResult.AsyncState;

                // Complete the connection.
                client.EndConnect(asyncResult);

                // trigger the connectDone event 
                connectionDone.Set(); 
            }
            catch (Exception)
            {
            }
        }
        #endregion

        #region Public Events

        #endregion

        #region Private Methods

        #endregion
    }
}
