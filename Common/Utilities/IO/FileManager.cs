// -----------------------------------------------------------------------------
// <copyright file="FileManager.cs" company="Sequoia Voting Systems">
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the FileManager class.
// </summary>
// <revision revisor="dev01" date="1/22/2009 4:40:10 PM" version="1.0.5.3">
//     File Created
// </revision>
// <revision revisor="dev06" date="2/2/2009" version="1.0.5.14">
//     File Modified
// </revision>
// <revision revisor="dev05" date="03/16/09" version="1.0.8.27">
//     File modified.
// </revision>
// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
//     File modified.
// </revision>
// -----------------------------------------------------------------------------

namespace Sequoia.Utilities.IO
{
    #region Using directives

    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Net.NetworkInformation;

    #endregion

    /// <summary>
    ///	    Class for managing files.
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev01" date="1/22/2009" version="1.0.5.3">
    ///     Class created.
    /// </revision>
    /// <revision revisor="dev06" date="2/2/2009" version="1.0.5.14">
    ///     Class modified:added methods to recursively delete a folder
    ///     structure and recreate a full folder path
    /// </revision>
    /// <revision revisor="dev05" date="03/16/09" version="1.0.8.27">
    ///     Added TouchFile method.
    /// </revision>
    /// <revision revisor="dev01" date="11/17/2009" version="1.1.3.5">
    ///     Added SyncOperatingSystem method
    /// </revision>
    /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
    ///     Formatting changes
    /// </revision>
    public class FileManager
    {
        #region Constructors

        #endregion

        #region Public Properties

        #endregion

        #region Public Methods

        /// <summary>
        ///     Delete files
        /// </summary>
        /// <param name="filesToDelete">files to be deleted</param>
        /// <returns>true if succesful</returns>
        /// <revision revisor="dev01" date="1/22/2009" version="1.0.5.3">
        ///     Method Created
        /// </revision>
        public static bool DeleteFiles(string[] filesToDelete)
        {
            bool returnValue = true;

            try
            {
                foreach (string file in filesToDelete)
                {
                    File.Delete(file);
                }
            }
            catch (Exception)
            {
                returnValue = false;
            }

            return returnValue;
        }

        /// <summary>
        /// Syncs the OS.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="11/17/2009" version="1.1.3.5">
        ///     Member Created
        /// </revision>
        public static void SyncOperatingSystem()
        {
            // flush the file to USB using linux "sync" command
            if (Environment.OSVersion.Platform ==
                    PlatformID.Unix)
            {
                // create a new diagnostic process
                var process = new System.Diagnostics.Process();

                // set the process start info
                process.StartInfo =
                    new ProcessStartInfo("sync");

                // start the process
                process.Start();
            }
        }

        /// <summary>
        /// Delete files in fodler that Match wilcards
        /// </summary>
        /// <param name="path">Path of the Folder to clean</param>
        /// <param name="searchPattern">Wilcards to match</param>
        /// <returns>true if succesful</returns>
        /// <revision revisor="dev01" date="1/22/2009" version="1.0.5.3">
        ///     Method Created
        /// </revision>
        public static bool DeleteAllFilesInFolder(
            string path, string searchPattern)
        {
            bool returnValue = true;
            try
            {
                string[] files = Directory.GetFiles(
                    path,
                    searchPattern)
                    ;

                returnValue =
                    FileManager.DeleteFiles(files);
            }
            catch (Exception)
            {
                returnValue = false;
            }

            return returnValue;
        }

        /// <summary>
        ///     Delete files in fodler that Match wilcards
        /// </summary>
        /// <param name="sourcePath">Source Folder</param>
        /// <param name="destPath">Destination Folder</param>
        /// <param name="searchPattern">Wilcards to match</param>
        /// <returns>true if succesful</returns>
        /// <revision revisor="dev01" date="4/13/2009" version="1.0.11.3">
        ///     Method Created
        /// </revision>
        public static bool CopyFiles(
            string sourcePath,
            string destPath, 
            string searchPattern)
        {
            bool returnValue = true;
            if (Directory.Exists(sourcePath) &&
                Directory.Exists(destPath))
            {
                try
                {
                    // get the files in the folder that match the sear pattern
                    string[] files = Directory.GetFiles(
                        sourcePath,
                        searchPattern);
                        
                    // copy each file
                    foreach (string file in files)
                    {
                        File.Copy(
                            file,
                            Path.Combine(
                               destPath,
                               Path.GetFileName(file)));
                    }
                }
                catch (Exception)
                {
                    returnValue = false;
                }
            }
            else
            {
                returnValue = false;
            }

            return returnValue;
        }

        /// <summary>
        ///     Delete all files in Folder
        /// </summary>
        /// <param name="path">Path of the Folder to clean</param>
        /// <returns>true if succesful</returns>
        /// <revision revisor="dev01" date="1/22/2009" version="1.0.5.3">
        ///     Method Created
        /// </revision>
        public static bool DeleteAllFilesInFolder(string path)
        {
            return FileManager.DeleteAllFilesInFolder(path, "*.*");
        }

        /// <summary>
        ///     Deletes all folders including and beneath the top level folder,
        ///     then recreates the tree to the desired child level.
        /// </summary>
        /// <param name="topLevelPath">The top level path.</param>
        /// <param name="lowestLevelPath">The lowest level path.</param>
        /// <returns>
        ///     <c>true</c> if the folder structure is deleted and
        ///     recreated successfully; otherwise <c>false</c>.
        /// </returns>
        /// <externalUnit cref="RecursiveDelete"/>
        /// <externalUnit cref="FullFolderRestore"/>
        /// <revision revisor="dev06" date="2/2/2009" version="1.0.5.14">
        ///     Member Created
        /// </revision>
        public static bool RecreateFolders(
            string topLevelPath,
            string lowestLevelPath)
        {
            // recursively clean the folders
            bool operationSucceeded = RecursiveDelete(topLevelPath);

            if (operationSucceeded)
            {
                // recreate all folders spec'd in path
                operationSucceeded = FullFolderRestore(lowestLevelPath);
            }

            return operationSucceeded;
        }

        /// <summary>
        ///     Deletes recursively
        /// </summary>
        /// <param name="topLevelFolder">The top level folder.</param>
        /// <returns>
        ///     <c>true</c> if the folders are deleted successfully;
        ///     otherwise <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     This exception gets thrown if folder path is not supplied.
        /// </exception>
        /// <exception cref="FileManagerException">
        ///     This exception gets thrown if any disk exception or access 
        ///     exception is thrown by the .NET subsystem.
        /// </exception>
        /// <externalUnit cref="ArgumentNullException"/>
        /// <externalUnit cref="Directory"/>
        /// <externalUnit cref="IOException"/>
        /// <externalUnit cref="FileManagerException"/>
        /// <externalUnit cref="NotSupportedException"/>
        /// <externalUnit cref="UnauthorizedAccessException"/>
        /// <revision revisor="dev06" date="2/2/2009" version="1.0.5.14">
        ///     Member Created
        /// </revision>
        public static bool RecursiveDelete(string topLevelFolder)
        {
            if (string.IsNullOrEmpty(topLevelFolder))
            {
                // REVIEW: hard coded strings....
                throw new ArgumentNullException(
                    topLevelFolder,
                    "No path was supplied for recursive folder deletion.");
            }

            // create var for return
            bool operationSucceeded = false;

            try
            {
                // make sure folder exists first
                if (Directory.Exists(topLevelFolder))
                {
                    // recursively clean the folders
                    Directory.Delete(topLevelFolder, true);
                }

                // set op to succeeded
                operationSucceeded = true;
            }
            catch(IOException ioException)
            {
                // TODO: use error ids instead of putting message here.
                throw new FileManagerException("IO Failure", ioException);
            }
            catch(UnauthorizedAccessException accessException)
            {
                throw new FileManagerException(
                    "Access failed. Please check your disk/resource " + 
                    "access permissions.",
                    accessException);
            }
            catch(NotSupportedException notSupportedException)
            {
                throw new FileManagerException(
                    "The disk operation was not supported.",
                    notSupportedException);
            }

            return operationSucceeded;
        }

        /// <summary>
        ///     Restores the full supplied folder path.
        /// </summary>
        /// <param name="path">The restore path.</param>
        /// <returns>
        ///     <c>true</c> if the folders are restored, otherwise <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     This exception gets thrown if either folder path 
        ///     is not supplied.
        /// </exception>
        /// <exception cref="FileManagerException">
        ///     This exception gets thrown if any disk exception or access 
        ///     exception is thrown by the .NET subsystem.
        /// </exception>
        /// <externalUnit cref="ArgumentNullException"/>
        /// <externalUnit cref="Directory"/>
        /// <externalUnit cref="IOException"/>
        /// <externalUnit cref="FileManagerException"/>
        /// <externalUnit cref="NotSupportedException"/>
        /// <externalUnit cref="UnauthorizedAccessException"/>
        /// <revision revisor="dev06" date="2/2/2009" version="1.0.5.14">
        ///     Member Created
        /// </revision>
        public static bool FullFolderRestore(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                // REVIEW: hard coded strings....
                throw new ArgumentNullException(
                    path,
                    "No path was supplied for recursive folder creation.");
            }

            // create var for return
            bool operationSucceeded = false;

            try
            {
                // recreate all folders spec'd in path
                Directory.CreateDirectory(path);

                // set op succeeded to true
                operationSucceeded = true;
            }
            catch (IOException ioException)
            {
                // TODO: use error ids instead of putting message here.
                throw new FileManagerException("IO Failure", ioException);
            }
            catch (UnauthorizedAccessException accessException)
            {
                throw new FileManagerException(
                    "Access failed. Please check your disk/resource " + 
                    "access permissions.",
                    accessException);
            }
            catch (NotSupportedException notSupportedException)
            {
                throw new FileManagerException(
                    "The disk operation was not supported.",
                    notSupportedException);
            }

            return operationSucceeded;
        }

        /// <summary>
        ///     Give a file a new time stamp.
        /// </summary>
        /// <param name="pathName">Full pathname of file</param>
        /// <param name="timeStamp">The desired timestamp</param>
        /// <returns>
        ///     <c>true</c> if creation time was updated; 
        ///     otherwise, <c>false</c>.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev05" date="03/16/09" version="1.0.8.27">
        ///     Method created.
        /// </revision>
        /// <revision revisor="dev06" date="04/23/09" version="1.0.11.11">
        ///     Method updated to return a boolean.
        /// </revision>
        public static bool TouchFile(string pathName, DateTime timeStamp)
        {
            FileInfo fi = new FileInfo(pathName);
            
            fi.CreationTime = fi.LastWriteTime = fi.LastAccessTime = timeStamp;

            return fi.CreationTime == timeStamp
                    && fi.LastWriteTime == timeStamp
                    && fi.LastAccessTime == timeStamp;
        }

        /// <summary>
        ///     Copy files
        /// </summary>
        /// <param name="fileName">Path to the source file</param>
        /// <param name="destPath">Path to the destination folder</param>
        /// <param name="share">Network Share to copy the file from</param>
        /// <param name="login">Secure Login</param>
        /// <param name="password">Secured password</param>
        /// <returns><c>true</c> if succesful</returns>
        /// <revision revisor="dev01" date="1/22/2009" version="1.0.5.3">
        ///     Method Created
        /// </revision>
        public static bool CopyRemoteFiles(
            string fileName,
            string destPath,
            string share,
            string login,
            string password)
                {
                    // Return Value
                    bool returnValue = true;
                    switch (Environment.OSVersion.Platform)
                    {
                        case PlatformID.Unix:
                            returnValue = FileManager.CopyRemoteFilesLinux(
                                fileName,
                                destPath,
                                share,
                                login,
                                password);
                            break;
                        default:
                            returnValue = FileManager.CopyRemoteFilesWin32NT(
                                fileName,
                                destPath,
                                share,
                                login,
                                password);
                            break;
                    }

                    return returnValue;
                }

        /// <summary>
        ///     Copy files from windows share using samba to linux
        /// </summary>
        /// <param name="fileName">Path to the source file</param>
        /// <param name="destPath">Path to the destination folder</param>
        /// <param name="share">Network Share to copy the file from</param>
        /// <param name="login">Secure Login</param>
        /// <param name="password">Secured password</param>
        /// <returns>true if succesful</returns>
        /// <revision revisor="dev01" date="1/22/2009" version="1.0.5.3">
        ///     Method Created
        /// </revision>
        public static bool CopyRemoteFilesLinux(
            string fileName,
            string destPath,
            string share,
            string login,
            string password)
        {
            // Return Value
            bool returnValue = true;

            // Process to handle the smbclient copy process
            var processInfo = new ProcessStartInfo();

            // Create parameters for smbclient
            processInfo.FileName = "smbclient";
            processInfo.RedirectStandardOutput = false;
            processInfo.UseShellExecute = false;
            processInfo.Arguments =
                String.Format(
                    "{0} {1} -U {2} -c \"get {3} {4}\"",
                    share.Replace('\\', '/'),
                    password,
                    login,
                    fileName,
                    Path.Combine(destPath, fileName));
            Process processHandle = null;
            try
            {
                processHandle = Process.Start(processInfo);
            }
            catch (Exception)
            {
                returnValue = false;
            }

            if (processHandle != null)
            {
                processHandle.WaitForExit();
                if (processHandle.ExitCode != 0)
                {
                    returnValue = false;
                }

                processHandle.Close();
            }

            return returnValue;
        }

        /// <summary>
        ///     Gets the local Mac Address.
        /// </summary>
        /// <returns>the macaddress string</returns>
        /// <revision revisor="dev01" date="1/22/2009" version="1.0.5.3">
        ///     Method Created
        /// </revision>
        public static string GetMacAddress()
        {
            // Empty placeholder for mac address
            string macAddress = string.Empty;

            // Get al lthe network Interfaces
            NetworkInterface[] nics =
                NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface nic in nics)
            {
                if (nic.OperationalStatus == OperationalStatus.Up &&
                    nic.NetworkInterfaceType != NetworkInterfaceType.Loopback &&
                    nic.NetworkInterfaceType != NetworkInterfaceType.Tunnel)
                {
                    macAddress = nic.GetPhysicalAddress().ToString();
                }
            }

            return macAddress;
        }

        /// <summary>
        ///     Copy files from windows share to loca windows.
        /// </summary>
        /// <param name="fileName">Path to the source file</param>
        /// <param name="destPath">Path to the destination folder</param>
        /// <param name="share">Network Share to copy the file from</param>
        /// <param name="login">Secure Login</param>
        /// <param name="password">Secured password</param>
        /// <returns>true if succesful</returns>
        /// <revision revisor="dev01" date="1/22/2009" version="1.0.5.3">
        ///     Method Created
        /// </revision>
        private static bool CopyRemoteFilesWin32NT(
            string fileName,
            string destPath,
            string share,
            string login,
            string password)
        {
            bool returnValue = true;
            try
            {
                File.Copy(
                    Path.Combine(share, fileName),
                    Path.Combine(destPath, fileName));
            }
            catch (Exception)
            {
                returnValue = false;
            }

            return returnValue;
        }
        #endregion
    }
}
