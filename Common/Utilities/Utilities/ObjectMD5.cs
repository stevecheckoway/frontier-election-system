// -----------------------------------------------------------------------------
// <copyright file="ObjectMD5.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     this class provides an easy way to generate MD5 hashes off of objects in
// memory, for verification and comparison
// </summary>
// <revision revisor="dev11" date="2/19/2009" version="1.0.8.0201">
//     File Created
// </revision>
// <revision revisor="dev13" date="11/18/2009" version="1.1.3.6">
//     File modified
// </revision>
// -----------------------------------------------------------------------------

namespace Sequoia.Utilities
{
    #region Using directives

    using System;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Security.Cryptography;

    #endregion

    /// <summary>
    ///	    ObjectMD5 class
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev11" date="2/19/2009" version="1.0.8.0201">
    ///     Class created.
    /// </revision>
    public class ObjectMD5
    {
        #region Fields

        /// <summary>
        ///     MD5 provider
        /// </summary>
        private static readonly MD5 md5Provider;

        /// <summary>
        ///     a dummy object to perform a lock statement on a critical block 
        ///     of code. This object is used to implement thread-safe calls on 
        ///     these methods without running the risk of deadlocks
        /// </summary>
        private static readonly object dummy;

        #endregion
        
        #region Constructors

        /// <summary>
        ///     Initializes static members of the <see cref="ObjectMD5"/> class.
        /// </summary>
        /// <externalUnit cref="MD5CryptoServiceProvider"/>
        /// <revision revisor="dev11" date="2/19/2009" version="1.0.8.0201">
        ///     Member Created
        /// </revision>	
        static ObjectMD5()
        {
            // initialize static members
            md5Provider = new MD5CryptoServiceProvider();
            dummy = new object();
        }

        #endregion

        #region Public Properties

        #endregion

        #region Public Methods

        /// <summary>
        ///     Generates the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>
        ///     The MD5 hash.
        /// </returns>
        /// <externalUnit cref="md5Provider"/>
        /// <externalUnit cref="SerializableToByteArray"/>
        /// <revision revisor="dev11" date="2/19/2009" version="1.0.8.0201">
        ///     Member Created
        /// </revision>
        public static byte[] Generate(object source)
        {
            // serialize object into array of bytes
            byte[] bytes = SerializableToByteArray(source);

            // generate MD5 hash from array of bytes
            byte[] md5Key = md5Provider.ComputeHash(bytes);

            // return hash
            return md5Key;
        }

        /// <summary>
        ///     Compares the specified objects using an MD5 hash of each object
        /// </summary>
        /// <param name="object1">The object1.</param>
        /// <param name="object2">The object2.</param>
        /// <returns>Returns true if both hashes are the same</returns>
        /// <externalUnit cref="BitConverter"/>
        /// <externalUnit cref="Generate"/>
        /// <revision revisor="dev11" date="2/19/2009" version="1.0.8.0201">
        ///     Member Created
        /// </revision>
        public static bool Compare(object object1, object object2)
        {
            // generate MD5 hashes
            byte[] md5Obj1 = Generate(object1),
                   md5Obj2 = Generate(object2);

            // convert hashes to strings
            string hash1 = BitConverter.ToString(md5Obj1),
                   hash2 = BitConverter.ToString(md5Obj2);

            // compare strings
            return (hash1 == hash2);
        }

        #endregion

        #region Public Events

        #endregion

        #region Private Methods

        /// <summary>
        ///     Serializes an object into a byte array
        /// </summary>
        /// <param name="source">A serializable object.</param>
        /// <returns>
        ///     A byte array containing the serialized object.
        /// </returns>
        /// <externalUnit cref="BinaryFormatter"/>
        /// <externalUnit cref="dummy"/>
        /// <externalUnit cref="MemoryStream" />
        /// <revision revisor="dev11" date="2/19/2009" version="1.0.8.0201">
        ///     Member Created
        /// </revision>
        private static byte[] SerializableToByteArray(object source)
        {
            // create a memory stream to receive the array of bytes from
            // serialization of object [source]
            MemoryStream stream = new MemoryStream();

            // create a binary formatter to perform the serialization
            BinaryFormatter formatter = new BinaryFormatter();

            // an array of bytes to receive from the memory stream
            byte[] serial = null;

            try
            {
                // in order to make this thread-safe, lock the object during
                // serialization
                lock (dummy)
                {
                    // serialize the object and save the output into a 
                    // memory stream
                    formatter.Serialize(stream, source);
                }

                // get the array of bytes from the memory stream
                serial = stream.ToArray();
            }
            finally
            {
                // something went wrong, make sure to close the stream to
                // free resources. This usually happens when an object non-
                // serializable is used on this method
                stream.Close();
            }

            return serial;
        }

        #endregion
    }
}
