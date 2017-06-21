// -----------------------------------------------------------------------------
// <copyright file="UsbHelper.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2008 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the UsbHelper class.
// </summary>
// <revision revisor="dev13" date="11/9/2008" version="1.0.0.0">
//     File Created
// </revision>  
// -----------------------------------------------------------------------------

namespace Sequoia.Utilities.IO
{
    #region Using directives

    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Management;

    #endregion

    /// <summary>
    ///     Delegate for when a USB stick (voter key) is inserted
    /// </summary>
    /// <param name="sender">the sending object</param>
    /// <param name="args">
    ///     a <see cref="UsbKeyLoadedEventArgs" /> object.
    /// </param>
    public delegate void UsbKeyFoundEventHandler(
        object sender, UsbKeyLoadedEventArgs args);

    /// <summary>Helper class for USB operations</summary>
    /// <externalUnit/>
    /// <revision revisor="dev06" date="10/20/2008" version="1.0.0.0">
    ///     Class created.
    /// </revision>
    public class UsbHelper : IDisposable
    {
        #region Fields

        /// <summary>
        /// Create var for storing reference to the management watcher
        /// </summary>
        private static ManagementEventWatcher usbRemovalWatcher = null;

        /// <summary>
        /// Create var for storing reference to the management watcher
        /// </summary>
        private static ManagementEventWatcher usbInsertionWatcher = null;

        /// <summary>
        /// If true will delete everything on the drive at initialization
        /// </summary>
        private bool clearOnLoad = false;

        /// <summary>
        /// File name to Search
        /// </summary>
        private string desiredFileName = string.Empty;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="UsbHelper"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <param name="clearRootOnUsbKeyLoad">Tells if USB should be cleares</param>
        /// <revision revisor="dev06" date="10/20/2008 1:00:35 PM" version="1.0.?.0">
        /// Member Created
        /// </revision>
        public UsbHelper(bool clearRootOnUsbKeyLoad)
        {
            this.InitializeHelper();

            // wire up deletion - root clkear on insert cartridge key
            this.clearOnLoad = clearRootOnUsbKeyLoad;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="UsbHelper"/> class.
        /// </summary>
        /// <param name="desiredFileName">File to search for</param>
        /// <externalUnit/>
        /// <revision revisor="dev06" date="10/20/2008 1:00:35 PM" version="1.0.?.0">
        ///     Member Created
        /// </revision>
        public UsbHelper(string desiredFileName)
        {
            this.InitializeHelper();
            this.desiredFileName = desiredFileName;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="UsbHelper"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev06" date="10/20/2008 1:00:35 PM" version="1.0.?.0">
        ///    Member Created
        /// </revision>
        public UsbHelper()
            : this(false)
        {
        }

        #endregion

        #region Public events

        /// <summary>
        ///     Occurs when [voter key found].
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/9/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public event UsbKeyFoundEventHandler UsbKeyFound;

        /// <summary>
        /// Occurs when [usb key removed].
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev06" date="11/12/2008" version="1.0.0.0">Member Created</revision>
        public event EventHandler UsbKeyRemoved;

        #endregion

        #region Public Properties

        #endregion

        #region Public Methods

        #endregion

        #region IDisposable Members

        /// <summary>
        /// Overloads the Dispose Method
        /// </summary>
        /// <revision revisor="dev06" date="10/21/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public void Dispose()
        {
            if (usbInsertionWatcher != null)
            {
                usbInsertionWatcher.Stop();
                usbInsertionWatcher.Dispose();
            }

            if (usbRemovalWatcher != null)
            {
                // Just freeing up resources here, since I should be done 
                // watching the usb event now.
                usbRemovalWatcher.Stop();
                usbRemovalWatcher.Dispose();
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        ///     Logs the exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <externalUnit cref="EventLog"/>
        /// <externalUnit cref="Environment"/>
        /// <externalUnit cref="EventLogEntryType"/>
        /// <revision revisor="dev06" date="10/21/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        private static void LogException(Exception exception)
        {
            EventLog log = new EventLog(
                "Application",
                Environment.MachineName,
                "Sequoia EMS: IO Utilities - USB Helper");

            log.WriteEntry(exception.ToString(), EventLogEntryType.Error);
        }

        /// <summary>
        ///     Adds the remove usb handler.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/9/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        private void AddRemoveUsbHandler()
        {
            WqlEventQuery wqlEventQuery = null;
            var scope = new ManagementScope("root\\CIMV2");
            scope.Options.EnablePrivileges = true;

            try
            {
                wqlEventQuery = new WqlEventQuery
                {
                    EventClassName = "__InstanceDeletionEvent",
                    WithinInterval = new TimeSpan(0, 0, 3),
                    Condition = @"TargetInstance ISA 'Win32_USBControllerdevice'"
                };
                usbRemovalWatcher = new ManagementEventWatcher(scope, wqlEventQuery);
                usbRemovalWatcher.EventArrived += this.UsbRemoved;
                usbRemovalWatcher.Start();
            }
            catch (Exception e)
            {
                LogException(e);

                if (usbRemovalWatcher != null)
                {
                    usbRemovalWatcher.Stop();
                }
            }
        }

        /// <summary>
        ///     Adds the insert usb handler.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/9/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        private void AddInsertUsbHandler()
        {
            WqlEventQuery wqlEventQuery = null;
            var scope = new ManagementScope("root\\CIMV2") { Options = { EnablePrivileges = true } };

            try
            {
                wqlEventQuery = new WqlEventQuery()
                {
                    EventClassName = "__InstanceCreationEvent",
                    WithinInterval = new TimeSpan(0, 0, 3),
                    Condition = @"TargetInstance ISA 'Win32_USBControllerdevice'",
                    GroupByPropertyList = { "TargetInstance.SourceName", "TargetInstance.ID" }
                };
                usbInsertionWatcher = new ManagementEventWatcher(scope, wqlEventQuery);
                usbInsertionWatcher.EventArrived += this.UsbAdded;
                usbInsertionWatcher.Start();
            }
            catch (ApplicationException e)
            {
                LogException(e);
                if (usbInsertionWatcher != null)
                {
                    usbInsertionWatcher.Stop();
                }
            }
        }

        /// <summary>
        ///     Usbs the added.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">
        ///     The <see cref="System.EventArgs"/> instance containing the 
        ///     event data.
        /// </param>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/9/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        private void UsbAdded(object sender, EventArgs e)
        {
            // create var for desired file path
            string pathToFile = string.Empty;

            DriveInfo[] drives = DriveInfo.GetDrives();

            foreach (DriveInfo driveInfo in drives)
            {
                if (driveInfo.DriveType.ToString() == "Removable")
                {
                    if (this.clearOnLoad)
                    {
                        this.ClearDrive(driveInfo);
                    }
                    else
                    {
                        pathToFile = this.LocateDesiredFile(driveInfo, this.desiredFileName);

                        if (string.IsNullOrEmpty(pathToFile) == false)
                        {
                            // stop searching
                            break;
                        }
                    }
                }
            }

            if (string.IsNullOrEmpty(pathToFile) == false)
            {
                this.OnUsbKeyFound(
                    new UsbKeyLoadedEventArgs(pathToFile));
            }
        }

        /// <summary>
        ///     Locates a file in a drive.
        /// </summary>
        /// <param name="driveInfo">The drive to search</param>
        /// <param name="fileName">Name of file to search</param>
        /// <externalUnit/>
        /// <returns>Path to the founded file</returns>
        /// <revision revisor="dev13" date="11/9/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        private string LocateDesiredFile(DriveInfo driveInfo, string fileName)
        {
            // look for the file and return its path if found
            DirectoryInfo rootDirectory = driveInfo.RootDirectory;

            string pathToFile = string.Empty;

            if (string.IsNullOrEmpty(fileName))
            {
                pathToFile = rootDirectory.FullName;
            }
            else
            {
                FileInfo[] files = rootDirectory.GetFiles();

                // Delete all files found at root
                foreach (FileInfo file in files)
                {
                    if (file.Name.Equals(
                        fileName, StringComparison.CurrentCultureIgnoreCase))
                    {
                        // set our param
                        pathToFile = rootDirectory.FullName;

                        // found the file, so stop searching
                        break;
                    }
                }
            }

            return pathToFile;
        }

        /// <summary>
        ///     Deletes all the files in the Drive.
        /// </summary>
        /// <param name="driveInfo">The drive to clear</param>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/9/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        private void ClearDrive(DriveInfo driveInfo)
        {
            // check to see if the drive contains any files at
            // its root. If it does, delete them.
            DirectoryInfo rootDirectory = driveInfo.RootDirectory;

            string rootPath = rootDirectory.ToString();

            FileInfo[] files = rootDirectory.GetFiles();

            // Delete all files found at root
            foreach (FileInfo file in files)
            {
                // Delete the file
                file.Delete();
            }

            // find all sub-directories
            DirectoryInfo[] directories = rootDirectory.GetDirectories();

            // delete all sub-directories
            foreach (DirectoryInfo directory in directories)
            {
                // Delete the directory
                directory.Delete();
            }

            // fire event with clear drive path
            this.OnUsbKeyFound(new UsbKeyLoadedEventArgs(rootPath));
        }

        /// <summary>
        ///     Usbs the removed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">
        ///     The <see cref="System.EventArgs"/> instance containing the 
        ///     event data.
        /// </param>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/9/2008" version="1.0.?.0">Member Created</revision>
        private void UsbRemoved(object sender, EventArgs e)
        {
            // fire event
            this.OnUsbKeyRemoved();
        }

        /// <summary>
        ///     Raises the <see cref="UsbKeyFound"/> event.
        /// </summary>
        /// <param name="args">
        ///     The <see cref="UsbKeyLoadedEventArgs"/> 
        ///     instance containing the event data.
        /// </param>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/9/2008" version="1.0.?.0">Member Created</revision>
        private void OnUsbKeyFound(UsbKeyLoadedEventArgs args)
        {
            if (this.UsbKeyFound != null)
            {
                this.UsbKeyFound(this, args);
            }
        }

        /// <summary>
        ///     Raises the <see cref="UsbKeyFound"/> event.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/9/2008" version="1.0.?.0">Member Created</revision>
        private void OnUsbKeyRemoved()
        {
            if (this.UsbKeyRemoved != null)
            {
                this.UsbKeyRemoved(this, EventArgs.Empty);
            }
        }

        /// <summary>
        ///     Initializes the class.
        /// </summary>
        /// <revision revisor="dev13" date="11/9/2008" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        private void InitializeHelper()
        {
            // make this happen whenever the usb helper is instantiated.
            this.AddInsertUsbHandler();
            this.AddRemoveUsbHandler();
        }

        #endregion
    }
}