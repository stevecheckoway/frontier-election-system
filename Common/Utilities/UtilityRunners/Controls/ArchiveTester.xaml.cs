// -----------------------------------------------------------------------------
// <copyright file="ArchiveTester.xaml.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the RawPrinterHelper class.
// </summary>
// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
//     Added documentation header
// </revision>  
// -----------------------------------------------------------------------------

namespace UtilityRunners.Controls
{
    #region Using directives

    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Forms;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;

    using Sequoia.Utilities.Compression;
    using Sequoia.Utilities.Encryption;
    using Sequoia.Utilities.IO;

    using MessageBox = System.Windows.MessageBox;
    using Path = System.IO.Path;
    using UserControl = System.Windows.Controls.UserControl;

    #endregion

    /// <summary>
    ///     Interaction logic for ArchiveTester.xaml
    /// </summary>
    public partial class ArchiveTester : UserControl
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ArchiveTester"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        public ArchiveTester()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     Handles the Click event of the inSelectFolder control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">
        ///     The <see cref="System.Windows.RoutedEventArgs"/> instance 
        ///     containing the event data.
        /// </param>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        private void inSelectFolder_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            folderBrowser.Description =
                "Select a folder from which to create an encrypted archive.";
            folderBrowser.RootFolder = Environment.SpecialFolder.Desktop;
            folderBrowser.ShowNewFolderButton = false;
            DialogResult result = folderBrowser.ShowDialog();

            if (result == DialogResult.OK)
            {
                this.inFolderPath.Text = folderBrowser.SelectedPath;
            }
        }

        /// <summary>
        ///     Handles the Click event of the inCreateEncryptedArchive control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">
        ///     The <see cref="System.Windows.RoutedEventArgs"/> instance 
        ///     containing the event data.
        /// </param>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        private void inCreateEncryptedArchive_Click(
            object sender, RoutedEventArgs e)
        {
            string selectedFolder = inFolderPath.Text;

            if (!string.IsNullOrEmpty(selectedFolder) && 
                Directory.Exists(selectedFolder))
            {
                this.CreateEncryptedArchive(selectedFolder, inArchiveName.Text);
            }
            else
            {
                MessageBox.Show(
                    "Please select a valid folder.",
                    "No folder selected",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);

                this.inSelectFolder.Focus();
            }
        }

        /// <summary>
        ///     Decrypts the archive to folder.
        /// </summary>
        /// <param name="selectedArchive">The selected archive.</param>
        /// <param name="privateKey">The private key.</param>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        private void DecryptArchiveToFolder(
            string selectedArchive, string privateKey)
        {
            try
            {
                string folderPath = Path.GetDirectoryName(selectedArchive);
                
                string filename = Path.GetFileName(selectedArchive);

                string publicKey = string.Empty;

                FileManifestItem item = new FileManifestItem(
                    folderPath, filename);
                var validationResult  = item.IsValid(folderPath);

                if (validationResult.Succeeded == false)
                {
                    MessageBox.Show("Validation of encrypted package failed.");
                }
                else
                {
                    MessageBox.Show(
                        "Validation of encrypted package succeeded.");
                }

                var result = EncryptedArchive.ExtractArchive(
                    folderPath, selectedArchive, new SequoiaCryptoProvider());

                if (result.Succeeded)
                {
                    MessageBox.Show(
                        "The selected archive has been extracted.",
                        "Archive extraction succeeded",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show(
                        result.Details,
                        "Archive extraction failed",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(
                    string.Format(
                    "Error creating dencrypted archive folder: {0}",
                    exception.Message),
                    "Archive Decryption Failure",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        /// <summary>
        ///     Creates the encrypted archive.
        /// </summary>
        /// <param name="selectedFolder">The selected folder.</param>
        /// <param name="archiveName">Name of the archive.</param>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        private void CreateEncryptedArchive(
            string selectedFolder, string archiveName)
        {
            try
            {
                string publicKey = string.Empty;

                string archiveFileName =
                string.IsNullOrEmpty(archiveName)
                    ? "TestEncryptedArchive"
                    : archiveName;
                string fullArchiveFilename = archiveFileName + ".zip";
                using (var encryptedArchive = new EncryptedArchive(
                    selectedFolder,
                    fullArchiveFilename,
                    new SequoiaCryptoProvider()))
                {
                    encryptedArchive.OpenWrite(DateTime.Now, true);
                    foreach (
                        string pathName in Directory.GetFiles(selectedFolder))
                    {
                        string fileName = System.IO.Path.GetFileName(pathName);

                        if (fileName == fullArchiveFilename)
                        {
                            continue;
                        }

                        // encryptedArchive.AddFile(fileName);
                        byte[] fileData = File.ReadAllBytes(pathName);

                        encryptedArchive.AddFile(fileData, fileName);
                    }
                }

                var sequoiaProvider = new SequoiaCryptoProvider();
                sequoiaProvider.CreateSignature(
                    Path.Combine(
                        selectedFolder,
                        Path.ChangeExtension(fullArchiveFilename, "enc")));
            }
            catch (Exception exception)
            {
                MessageBox.Show(
                    string.Format(
                    "Error creating encrypted archive: {0}", 
                    exception.Message),
                    "Encrypted Archive Creation Failure",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        /// <summary>
        ///     Handles the Click event of the inSelectArchive control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">
        ///     The <see cref="System.Windows.RoutedEventArgs"/> instance 
        ///     containing the event data.
        /// </param>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        private void inSelectArchive_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.CheckFileExists = true;
            fileDialog.DefaultExt = "enc";
            fileDialog.DereferenceLinks = true;
            fileDialog.Filter = "Encrypted Files|*.enc|All Files|*.*";
            fileDialog.FilterIndex = 1;
            DialogResult result = fileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                this.inArchivePath.Text = fileDialog.FileName;
            }
        }

        /// <summary>
        ///     Handles the Click event of the inDecryptArchive control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">
        ///     The <see cref="System.Windows.RoutedEventArgs"/> instance 
        ///     containing the event data.
        /// </param>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        private void inDecryptArchive_Click(object sender, RoutedEventArgs e)
        {
            string selectedArchivePath = this.inArchivePath.Text;

            if (!string.IsNullOrEmpty(selectedArchivePath) && 
                File.Exists(selectedArchivePath))
            {
                string selectedKeyPath = this.inAlternatePrivateKey.Text;

                string privateKey = string.Empty;
                if (File.Exists(selectedKeyPath))
                {
                    privateKey = File.ReadAllText(selectedKeyPath);
                }

                this.DecryptArchiveToFolder(selectedArchivePath, privateKey);
            }
            else
            {
                MessageBox.Show(
                    "Please select a valid folder.",
                    "No folder selected",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);

                this.inSelectFolder.Focus();
            }
        }

        /// <summary>
        ///     Handles the Click event of the inPickFileForVerification control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">
        ///     The <see cref="System.Windows.RoutedEventArgs"/> instance 
        ///     containing the event data.
        /// </param>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        private void inPickFileForVerification_Click(
            object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.CheckFileExists = true;
            fileDialog.DefaultExt = "enc";
            fileDialog.DereferenceLinks = true;
            fileDialog.Filter = "Encrypted Files|*.enc|All Files|*.*";
            fileDialog.FilterIndex = 1;
            DialogResult result = fileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                this.inFileForVerification.Text = fileDialog.FileName;
            }
        }

        /// <summary>
        ///     Handles the Click event of the inVerifySignature control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">
        ///     The <see cref="System.Windows.RoutedEventArgs"/> instance 
        ///     containing the event data.
        /// </param>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        private void inVerifySignature_Click(object sender, RoutedEventArgs e)
        {
             string selectedArchivePath = this.inFileForVerification.Text;

            if (!string.IsNullOrEmpty(selectedArchivePath) && 
                File.Exists(selectedArchivePath))
            {
                this.VerifyArchiveSignature(selectedArchivePath);
            }
            else
            {
                MessageBox.Show(
                    "Please select a valid file for signature verification.",
                    "No file selected",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);

                this.inPickFileForVerification.Focus();
            }
        }

        /// <summary>
        ///     Verifies the archive signature.
        /// </summary>
        /// <param name="selectedArchivePath">The selected archive path.</param>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        private void VerifyArchiveSignature(string selectedArchivePath)
        {
            string pathToSigFile = selectedArchivePath + ".sig";

            if (!File.Exists(pathToSigFile))
            {
                MessageBox.Show(
                    "Please select a valid file which has a signature file " + 
                    "of the same name, plus '.sig.'",
                    "No signature file present",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            else
            {
                var sequoiaCryptoProvider = new SequoiaCryptoProvider();

                bool verified =
                    sequoiaCryptoProvider.VerifySignature(
                        pathToSigFile, selectedArchivePath);

                if (verified)
                {
                    MessageBox.Show(
                    "The file matches the signature!",
                    "File Signature Test",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show(
                    "the file does NOT match the signature.",
                    "File Signature Test",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                }
            }
        }

        /// <summary>
        ///     Handles the Click event of the inPickNewPrivateKey control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">
        ///     The <see cref="System.Windows.RoutedEventArgs"/> instance 
        ///     containing the event data.
        /// </param>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
        ///     Added documentation header
        /// </revision>
        private void inPickNewPrivateKey_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.CheckFileExists = true;
            fileDialog.DefaultExt = "enc";
            fileDialog.DereferenceLinks = true;
            fileDialog.Filter = "XML Files|*.xml|All Files|*.*";
            fileDialog.FilterIndex = 1;
            DialogResult result = fileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                this.inAlternatePrivateKey.Text = fileDialog.FileName;
            }
        }
    }
}
