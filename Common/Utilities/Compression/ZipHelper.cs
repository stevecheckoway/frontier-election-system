// -----------------------------------------------------------------------------
// <copyright file="ZipHelper.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the ZipHelper class.
// </summary>
// <revision revisor="dev06" date="1/11/2009" version="1.0.0.0">
//     File Created
// </revision>  
// <revision revisor="dev01" date="1/23/2009" version="1.0.5.3">
//     File modified
// </revision>
// <revision revisor="dev13" date="11/17/2009" version="1.1.3.5">
//     file modified
// </revision> 
// -----------------------------------------------------------------------------

namespace Sequoia.Utilities.Compression
{
    #region Using directives

    using System;
    using System.IO;
    using System.Text;
    using ICSharpCode.SharpZipLib.Zip;
    using ICSharpCode.SharpZipLib.Zip.Compression.Streams;

    #endregion

    /// <summary>
    ///     ZipHelper is a utility which compresses and decompresses data.
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev06" date="1/11/2009 10:54:43 PM" version="1.0.0.0">
    ///     Class created.
    /// </revision>
    public class ZipHelper
    {
        #region Fields

        #endregion

        #region Public Properties

        #endregion

        #region Public Methods

        #region Compress
        /// <summary>
        ///     Compresses the specified string to compress.
        /// </summary>
        /// <param name="stringToCompress">The string to compress.</param>
        /// <returns>
        ///     A <see cref="CompressedItem"/> containing details about 
        ///     the compressed string.
        /// </returns>
        /// <externalUnit cref="CompressedItem"/>
        /// <externalUnit cref="MemoryStream"/>
        /// <externalUnit cref="Encoding"/>
        /// <externalUnit cref="DeflaterOutputStream"/>
        /// <revision revisor="dev06" date="1/11/2009" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public static CompressedItem Compress(string stringToCompress)
        {
            // create parm for return
            var compressedItem = new CompressedItem();

            // get the bytes of the string
            byte[] uncompressedData =
                Encoding.Unicode.GetBytes(stringToCompress);

            // save the uncompressed size of the byte array to 
            // use when decompressing
            compressedItem.UncompressedSize = uncompressedData.Length;

            // create a stream for storing the compressed data
            using (var compressedStream = new MemoryStream())
            {
                // create stream for compressing the data
                using (var zipStream =
                    new DeflaterOutputStream(compressedStream))
                {
                    // write the data to the stream - which will compress it 
                    // and put it it the compressed stream object
                    zipStream.Write(
                        uncompressedData, 0, uncompressedData.Length);
                }

                // set the compressed data on the return item
                compressedItem.Data = compressedStream.ToArray();
            }

            return compressedItem;
        }
        #endregion

        /// <summary>
        ///     Compresses the images.
        /// </summary>
        /// <param name="dir">The directory.</param>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/17/2009" version="1.1.3.5">
        ///     Added documentation header
        /// </revision>
        public static void CompressImages(string dir)
        {
            ZipFile z = ZipFile.Create(dir + "\\test.zip");

            z.BeginUpdate();
            foreach (string file in Directory.GetFiles(dir))
            {
                z.Add(Path.GetFileName(file));
            }   

            z.CommitUpdate();
            z.Close();
        }

        /// <summary>
        ///     Compresses the directory.
        /// </summary>
        /// <param name="dir">The directory.</param>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/17/2009" version="1.1.3.5">
        ///     Added documentation header
        /// </revision>
        public static void CompressDir(string dir)
        {
            try
            {
                string[] filenames = Directory.GetFiles(dir);

                // zip up the files
                using (ZipOutputStream s = 
                    new ZipOutputStream(File.Create(dir + "\\test.zip")))
                {
                    s.SetLevel(9); // 0-9, 9 being the highest compression
                    byte[] buffer = new byte[4096];

                    foreach (string file in filenames)
                    {
                        ZipEntry entry = new ZipEntry(Path.GetFileName(file));
                        entry.DateTime = DateTime.Now;
                        entry.Size = (new FileInfo(file)).Length;
                        s.PutNextEntry(entry);

                        using (FileStream fs = File.OpenRead(file))
                        {
                            int sourceBytes;
                            do
                            {
                                sourceBytes = fs.Read(buffer, 0, buffer.Length);
                                s.Write(buffer, 0, sourceBytes);
                            } 
                            while (sourceBytes > 0);
                        }
                    }

                    s.Finish();
                    s.Close();
                }
            }
            catch (Exception)
            {
            }
        }

        #region CompressFiles
        /// <summary>
        ///     Compresses the specified folder into a file.
        /// </summary>
        /// <param name="sourceFolder">
        ///     Folder where all the Files are stored.
        /// </param>
        /// <param name="destinationFile">File to compress</param>
        /// <param name="recursive">Is it recursive?</param>
        /// <returns>True if succesful</returns>
        /// <revision revisor="dev01" date="1/23/2009" version="1.0.5.3">
        ///     Member Created
        /// </revision>
        public static bool CompressFiles(
            string sourceFolder,
            string destinationFile,
            bool recursive)
        {
            bool returnValue = false;
            var zipper = new FastZip();
            try
            {
                // Creating the zip file with all the files in the sourcefolder 
                zipper.CreateZip(
                    destinationFile,
                    sourceFolder,
                    recursive, 
                    string.Empty);
                
                returnValue = true;
            }
            catch (Exception)
            {
                throw;
            }
            
            return returnValue;
        }
        #endregion

        #region Decompress

        /// <summary>
        ///     Decompresses the specified compressed item.
        /// </summary>
        /// <param name="compressedItem">The compressed item.</param>
        /// <returns>The uncompressed value of the compressed data.</returns>
        /// <externalUnit cref="CompressedItem"/>
        /// <revision revisor="dev06" date="1/11/2009" version="1.0.0.0">
        ///     Member Created
        /// </revision>
        public static string Decompress(CompressedItem compressedItem)
        {
            // Review: should this be in Try/catch block? 

            // create return param
            string results = string.Empty;

            // create a memory stream which stores the compressed data
            using (var compressedStream = new MemoryStream(compressedItem.Data))
            {
                // create stream which will unzip data
                using (var zipStream =
                    new InflaterInputStream(compressedStream))
                {
                    // create a byte array to store the uncompressed data
                    byte[] decompressedData =
                        new byte[compressedItem.UncompressedSize];

                    // read the unzipped data to the byte array
                    zipStream.Read(
                        decompressedData, 0, decompressedData.Length);

                    // convert the byte array back into a string
                    results = Encoding.Unicode.GetString(decompressedData);
                }
            }

            return results;
        }
        #endregion

        /// <summary>
        ///     Compresses the specified folder into a file.
        /// </summary>
        /// <param name="sourceFile">File to compress</param>
        /// <param name="destinationFolder">
        ///     Folder where all the Files are stored
        /// </param>
        /// <returns>True if succesful</returns>
        /// <revision revisor="dev01" date="1/23/2009" version="1.0.5.3">
        ///     Member Created
        /// </revision>
        public static bool DeCompressFiles(
            string sourceFile,
            string destinationFolder)
        {
            // Return Value
            bool returnValue = true;

            // FastZip Object to unzip files
            var zipper = new FastZip();

            try
            {
                // Extracting all the files in the zip file to the destination 
                // folder
                zipper.ExtractZip(
                    sourceFile,
                    destinationFolder,
                    string.Empty);
            }
            catch (Exception)
            {
                // Review: Exception should be logged
                returnValue = false;
            }

            return returnValue;
        }

        #endregion

        #region Public Events

        #endregion

        #region Private Methods

        #endregion
    }
}
