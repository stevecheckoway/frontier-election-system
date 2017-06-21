// -----------------------------------------------------------------------------
// <copyright file="WirelessModem.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the WirelessModem class.
// </summary>
// <revision revisor="dev01" date="2/2/2009 1:55:33 PM" version="1.0.5.14">
//     File Created
// </revision>  
// <revision revisor="dev05" date="9/9/2009" version="1.0.17.2">
//     File modified.
// </revision>
// <revision revisor="dev13" date="11/17/2009" version="1.0.17.2">
//     File modified.
// </revision>
// -----------------------------------------------------------------------------

namespace Sequoia.Utilities.Communication
{
    #region Using directives

    using System;
    using System.Configuration;
    using System.Diagnostics;
    using System.IO;
    using System.Threading;

    using Sequoia.DomainObjects;

    #endregion

    /// <summary>
    ///	    WirelessModem helper class
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev01" date="2/2/2009" version="1.0.5.14">
    ///     Class created.
    /// </revision>
    public class WirelessModem : ICommunicationDevice
    {
        #region Fields

        /// <summary>
        ///     Path to the command to handle the connection
        /// </summary>
        private string connectionService = "service";

        /// <summary>
        ///     Path to the command to handle the connection
        /// </summary>
        private string connectionServiceWinDows =
            @"C:\Program Files\AT&T\Communication Manager\ATTCM.exe";

        /// <summary>
        ///     Parameter to start connection
        /// </summary>
        private string connectionServiceStart = "start";

        /// <summary>
        ///     Parameter to stop connection
        /// </summary>
        private string connectionServiceStop = "stop";


        /// <summary>
        ///     How many attemps
        /// </summary>
        private int retryAttemps = 10;

        /// <summary>
        ///     Sleep between Attemps
        /// </summary>
        private int latencyBetweenAttempts = 1000;

        /// <summary>
        ///     Timeout for connection
        /// </summary>
        private int totalTimeout = 45000;

        /// <summary>
        ///     Server to test connection
        /// </summary>
        private string[] serverAddresses = null;

        /// <summary>
        ///     param for whether or not we are connected
        /// </summary>
        private bool connected;

        /// <summary>
        ///     param for whether to process in action
        /// </summary>
        private bool processInAction = false;


        #endregion
        
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="WirelessModem"/> class.
        /// </summary>
        /// <param name="listofServers">The list of servers.</param>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="2/2/2009" version="1.0.5.14">
        ///     Member Created
        /// </revision>
        public WirelessModem(string[] listofServers)
        {
            try
            {
                string value =
                    ConfigurationSettings.AppSettings["ConnectionRetries"];
                if (value != null)
                {
                    this.retryAttemps = int.Parse(value);
                }

                value =
                    ConfigurationSettings.AppSettings["ConnectionTimeOut"];
                if (value != null)
                {
                    this.totalTimeout = int.Parse(value);
                }

                value =
                    ConfigurationSettings.
                    AppSettings["ConnectionRetriesLatency"];
                if (value != null)
                {
                    this.latencyBetweenAttempts = int.Parse(value);
                }

                value =
                    ConfigurationSettings.AppSettings["DefaultTransmissionUrls"];
                this.serverAddresses = listofServers;
            }
            catch
            {
            }

            this.InitForPlatform();
        }

        #endregion

        #region Public Events

        /// <summary>
        ///     Occurs when [got connected].
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="2/2/2009" version="1.0.5.14">
        ///     Member Created
        /// </revision>
        public event ConnectionHandler GotConnected;

        /// <summary>
        ///     Occurs when [got disconnected].
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="2/2/2009" version="1.0.5.14">
        ///     Member Created
        /// </revision>
        public event ConnectionHandler GotDisconnectedConnected;

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets a value indicating whether this 
        ///     <see cref="ICommunicationDevice"/> is connected.
        /// </summary>
        /// <value><c>true</c> if connected; otherwise, <c>false</c>.</value>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="2/2/2009" version="1.0.5.14">
        ///     Member Created
        /// </revision>
        public bool Connected
        {
            get { return this.connected; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Connects this instance.
        /// </summary>
        /// <returns>
        ///     An <see cref="OperationResult" /> containing the results
        ///     of the operation.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="2/2/2009" version="1.0.5.14">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev01" date="4/29/2009" version="1.0.11.17">
        ///     Changed return value to operation result
        /// </revision>
        public OperationResult Connect()
        {
            // Return Value
            var returnValue = new OperationResult(true);
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.Unix:
                    returnValue = this.ConnectLinux();
                    break;
                default:
                    returnValue = this.ConnectWin32NT();
                    break;
            }

            this.connected = returnValue.Succeeded;

            if (this.connected == false)
            {
                this.Disconnect();
            }

            return returnValue;
        }

        /// <summary>
        ///     Disconnects this instance.
        /// </summary>
        /// <returns>
        ///     An <see cref="OperationResult" /> containing the results
        ///     of the operation.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="2/2/2009" version="1.0.5.14">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev01" date="4/29/2009" version="1.0.11.17">
        ///     Changed return value to operation result
        /// </revision>
        public OperationResult Disconnect()
        {
            // Return Value
            OperationResult returnValue = new OperationResult(true);
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.Unix:
                    returnValue = this.DisconnectLinux();
                    break;
                default:
                    returnValue = this.DisconnectWin32NT();
                    break;
            }

            this.connected = !returnValue.Succeeded;
            return returnValue;
        }

        /// <summary>
        ///     Connects this instance.
        /// </summary>
        /// <returns>
        ///     An <see cref="OperationResult" /> containing the results
        ///     of the operation.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="2/2/2009" version="1.0.5.14">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev01" date="4/29/2009" version="1.0.11.17">
        ///     Changed return value to operation result
        /// </revision>
        public OperationResult ConnectLinux()
        {
            // Return Value
            var returnValue = NetworkUtils.CheckNetwork(this.serverAddresses);
            if (returnValue.Succeeded == false)
            {
                // currentattemps
                int currentattemps = 0;

                // Process placeholder for 
                var processInfo = new ProcessStartInfo();
                processInfo.FileName = this.connectionService;
                processInfo.RedirectStandardOutput = false;
                processInfo.UseShellExecute = false;
                processInfo.Arguments =
                    String.Format("{0} {1}", "ppp", this.connectionServiceStart);
                Process processHandle = null;
                try
                {
                    processHandle = Process.Start(processInfo);
                    if (processHandle != null)
                    {
                        processHandle.WaitForExit();
 
                        processHandle.Close();
                        while (
                            (returnValue.Succeeded == false) &&
                            (currentattemps < this.retryAttemps) &&
                            currentattemps *
                            (this.latencyBetweenAttempts +
                            (NetworkUtils.TcpTimeOut * this.serverAddresses.Length))
                            < this.totalTimeout)
                        {
                            returnValue =
                                NetworkUtils.CheckNetwork(this.serverAddresses);
                            Thread.Sleep(this.latencyBetweenAttempts);
                            currentattemps++;
                        }
                    }
                }
                catch (Exception exc)
                {
                    returnValue = new OperationResult(false, exc.ToString());
                }
            }

            return returnValue;
        }

        /// <summary>
        ///     Disconnects this instance.
        /// </summary>
        /// <returns>
        ///     An <see cref="OperationResult" /> containing the results
        ///     of the operation.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="2/2/2009" version="1.0.5.14">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev01" date="4/29/2009" version="1.0.11.17">
        ///     Changed return value to operation result
        /// </revision>
        public OperationResult DisconnectLinux()
        {
            var returnValue = new OperationResult(true);

            // currentattemps
            int currentattemps = 0;

            // Process placeholder for 
            var processInfo = new ProcessStartInfo();
            processInfo.FileName = this.connectionService;
            processInfo.RedirectStandardOutput = false;
            processInfo.UseShellExecute = false;
            processInfo.Arguments =
                String.Format("{0} {1}", "ppp", this.connectionServiceStop);
            Process processHandle = null;
            try
            {
                processHandle = Process.Start(processInfo);
                if (processHandle != null)
                {
                    processHandle.WaitForExit();
                    processHandle.Close();

                    while (
                        (returnValue.Succeeded == false) &&
                        (currentattemps < this.retryAttemps) &&
                        currentattemps * this.latencyBetweenAttempts < 
                        this.totalTimeout)
                    {
                        if (NetworkUtils.CheckNetwork(
                            this.serverAddresses).Succeeded == false)
                        {
                            returnValue = new OperationResult(true);
                        }

                        Thread.Sleep(this.latencyBetweenAttempts);
                        currentattemps++;
                    }
                }
                else
                {
                    returnValue =
                        new OperationResult(
                            false,
                            "Unable to disconnect Service");
                }
            }
            catch (Exception exc)
            {
                returnValue = new OperationResult(false, exc.ToString());
            }

            return returnValue;
        }

        #endregion

        #region Private Methods

        /// <summary>
        ///     Inits for platform.
        /// </summary>
        /// <returns>
        ///     <c>true</c> if initialized; otherwise, <c>false</c>.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="5/19/2009" version="1.0.13.1">
        ///     Member Created
        /// </revision>
        private bool InitForPlatform()
        {
            bool returnValue = false;

            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.Unix:
                    returnValue = this.InitForLinux();
                    break;
                default:
                    returnValue = this.InitForWin32();
                    break;
            }

            return returnValue;
        }

        /// <summary>
        ///     Inits for linux.
        /// </summary>
        /// <returns>
        ///     <c>true</c> if initialized; otherwise, <c>false</c>.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="5/19/2009" version="1.0.13.1">
        ///     Member Created
        /// </revision>
        private bool InitForLinux()
        {
            // TODO: create init for linux or remove completely if not needed on linux...
            return false;
        }

        /// <summary>
        ///     Inits for win32.
        /// </summary>
        /// <returns>
        ///     <c>true</c> if initialized; otherwise, <c>false</c>.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="5/19/2009" version="1.0.13.1">
        ///     Member Created
        /// </revision>
        private bool InitForWin32()
        {
            bool returnValue = false;

            if (File.Exists(this.connectionServiceWinDows))
            {
                // Process placeholder for 
                var processInfo = new ProcessStartInfo();
                processInfo.FileName = this.connectionServiceWinDows;
                processInfo.RedirectStandardOutput = false;
                processInfo.WindowStyle = ProcessWindowStyle.Minimized;
                processInfo.UseShellExecute = false;
                processInfo.Arguments =
                    String.Format("{0}", "-d");
                Process processHandle = null;
                try
                {
                    processHandle = Process.Start(processInfo);
                    processHandle.Exited +=
                        new EventHandler(this.processHandle_Exited);

                    this.processInAction = !processHandle.HasExited;
                }
                catch (Exception)
                {
                    //TODO: Log errors
                    returnValue = false;
                }
            }

            return returnValue;
        }

        /// <summary>
        ///     Handles the Exited event of the processHandle control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">
        ///     The <see cref="System.EventArgs"/> instance containing 
        ///     the event data.
        /// </param>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/17/2009" version="1.1.3.5">
        ///     Added documentation header and access modifier
        /// </revision>
        private void processHandle_Exited(object sender, EventArgs e)
        {
            this.processInAction = false;
        }

        /// <summary>
        ///     Connects the win32 NT.
        /// </summary>
        /// <returns>
        ///     An <see cref="OperationResult" /> containing the results 
        ///     of the operation.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="2/2/2009" version="1.0.5.14">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev01" date="4/29/2009" version="1.0.11.17">
        ///     Changed return value to operation result
        /// </revision>
        private OperationResult ConnectWin32NT()
        {
            var returnValue = NetworkUtils.CheckNetwork(this.serverAddresses);
            if (returnValue.Succeeded == false)
            {
                // currentattemps
                int currentattemps = 0;

                try
                {
                    while (
                        (returnValue.Succeeded == false) &&
                        (currentattemps < this.retryAttemps) &&
                        currentattemps * this.latencyBetweenAttempts < 
                        this.totalTimeout)
                    {
                        returnValue =
                            NetworkUtils.CheckNetwork(this.serverAddresses);
                        Thread.Sleep(this.latencyBetweenAttempts);
                        currentattemps++;
                    }
                }
                catch (Exception exc)
                {
                    returnValue = new OperationResult(false, exc.ToString());
                }
            }

            return returnValue;
        }

        /// <summary>
        ///     Disconnects the win32 NT.
        /// </summary>
        /// <returns>
        ///     An <see cref="OperationResult" /> containing the results
        ///     of the operation.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="2/2/2009" version="1.0.5.14">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev01" date="4/29/2009" version="1.0.11.17">
        ///     Changed return value to operation result
        /// </revision>
        private OperationResult DisconnectWin32NT()
        {
            return new OperationResult(true);
        }

        #endregion
    }
}
