// -----------------------------------------------------------------------------
// <copyright file="TextToAudio.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the TextToMP3 class.
// </summary>
// <revision revisor="dev01" date="8/5/2009" version="1.0.14.2801">
//     File Created
// </revision>
// <revision revisor="dev13" date="11/17/2009" version="1.1.3.5">
//     File modified
// </revision>
// -----------------------------------------------------------------------------

namespace Sequoia.Utilities.Audio
{
    #region Using directives

    using System;
    using System.IO;
    using System.Threading;

    #endregion

    /// <summary>
    ///     Text to audio conversion class
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev01" date="8/5/2009" version="1.0.14.2801">
    ///     Member Created
    /// </revision>
    /// <revision revisor="dev13" date="11/17/2009" version="1.1.3.5">
    ///     Formatting changes
    /// </revision>
    public class TextToAudio
    {
        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="TextToAudio"/> class.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/17/2009" version="1.1.3.5">
        ///     Added documentation header
        /// </revision>
        public TextToAudio()
        {
            tmpWavFile = Path.Combine(
                Environment.GetEnvironmentVariable("temp"), "temp.wav");
            tmpMP3File = Path.Combine(
                Environment.GetEnvironmentVariable("temp"), "temp.mp3");
            this.textToAudio2 = new NCTTEXTTOAUDIO2Lib.TextToAudio2Class();
            this.textToAudio2.Register(
                "[your company name]", "[your license key]");
            this.textToAudio2.EndConvert +=
                new NCTTEXTTOAUDIO2Lib.
                    _ITextToAudio2Events_EndConvertEventHandler(
                    SaveAudioToMp3File);
            audioFile = new NCTAUDIOFILE2Lib.AudioFile2Class();
            audioFile.Register("[your company name]", "[your license key]");
            this.textToAudio2.VoiceNum = 0;
            this.textToAudio2.Rate = 0;
            this.textToAudio2.Volume = 100;
            this.textToAudio2.AsyncMode = true;
            frequency = NCTTEXTTOAUDIO2Lib.FrequencyConstants.FQ_22_KHZ;
            channels = NCTTEXTTOAUDIO2Lib.ChannelsConstants.MONO;
            this.textToAudio2.SetFormatArray(frequency, channels);
        }

        #endregion

        #region Fields

        /// <summary>
        ///     param for mp3 finished
        /// </summary>
        private ManualResetEvent mp3Finished = new ManualResetEvent(false);

        /// <summary>
        ///     param for temporary wav file
        /// </summary>
        private string tmpWavFile;
        
        /// <summary>
        ///     param for mp3 file
        /// </summary>
        private string tmpMP3File;

        /// <summary>
        ///     param for text to audio
        /// </summary>
        private NCTTEXTTOAUDIO2Lib.TextToAudio2Class textToAudio2;

        /// <summary>
        ///     param for audio file
        /// </summary>
        private NCTAUDIOFILE2Lib.AudioFile2Class audioFile;

        /// <summary>
        ///     param for frequency
        /// </summary>
        private NCTTEXTTOAUDIO2Lib.FrequencyConstants frequency;

        /// <summary>
        ///     param for channels
        /// </summary>
        private NCTTEXTTOAUDIO2Lib.ChannelsConstants channels;

        #endregion

        #region Public Methods

        /// <summary>
        ///     Generates the MP3.
        /// </summary>
        /// <param name="textToTransform">The text to transform.</param>
        /// <returns>
        ///     The audio bytes.
        /// </returns>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="7/29/2009" version="1.0.14.15">
        ///     Member Created
        /// </revision>
        /// <revision revisor="dev11" date="11/02/2009" version="1.1.2.1101">
        ///     File deletion surrounded by try/catch clauses
        /// </revision>
        public byte[] GenerateMp3(string textToTransform)
        {
            try
            {
                File.Delete(this.tmpWavFile);
            }
            catch
            {
            }

            try
            {
                File.Delete(this.tmpMP3File);
            }
            catch (Exception)
            {
            }

            byte[] result = null;

            this.textToAudio2.ConvertStringToWav(
                textToTransform,
                this.tmpWavFile,
                NCTTEXTTOAUDIO2Lib.BitsPerSampleConstants.BPS_16_BIT);
            this.mp3Finished.WaitOne();
            result = File.ReadAllBytes(this.tmpMP3File);
            File.Delete(this.tmpWavFile);
            File.Delete(this.tmpMP3File);
            this.mp3Finished.Reset();

            return result;
        }

        /// <summary>
        ///     Generates the MP3.
        /// </summary>
        /// <param name="text">The file text.</param>
        /// <param name="path">The file path.</param>
        /// <param name="filename">The filename.</param>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/17/2009" version="1.1.3.5">
        ///     Added documentation header
        /// </revision>
        public void GenerateMp3(string text, string path, string filename)
        {
            byte[] audio = this.GenerateMp3(text);
            if (Path.HasExtension(filename) == false)
            {
                filename = string.Format("{0}.mp3", filename);
            }

            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }

            File.WriteAllBytes(Path.Combine(path, filename), audio);
        }

        #endregion

        #region Private Methods

        /// <summary>
        ///     Saves the audio to MP3 file.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev01" date="7/29/2009" version="1.0.14.15">
        ///     Member Created
        /// </revision>
        private void SaveAudioToMp3File()
        {
            Array audioArray;
            this.audioFile.OpenFile(
                this.tmpWavFile,
                NCTAUDIOFILE2Lib.FormatTypeConstants.DEFAULT,
                NCTAUDIOFILE2Lib.FlagOpen.READ);

            audioArray = this.audioFile.ReadFile(this.audioFile.nSamples);
            this.audioFile.CloseFile();
            this.audioFile.SetFormatMP3(
                NCTAUDIOFILE2Lib.FrequencyConstants.FQ_22_KHZ,
                NCTAUDIOFILE2Lib.BitrateConstants.BR_32_KBPS,
                this.audioFile.ArrayChannels);
            this.audioFile.CreateFile(
                this.tmpMP3File, NCTAUDIOFILE2Lib.FormatTypeConstants.MP3);
            this.audioFile.WriteFile(ref audioArray);
            this.audioFile.CloseFile();
            this.mp3Finished.Set();
        }
        
        #endregion

        #region Constants
        
        #endregion
    }

}
