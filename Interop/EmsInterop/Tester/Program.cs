// -----------------------------------------------------------------------------
// <copyright file="Program.cs" company="Sequoia Voting Systems"> 
//     Copyright (c) 2009 Sequoia Voting Systems, Inc. All Rights Reserved.
//     Distribution of source code is allowable only under the terms of the
//     license agreement found at http://www.sequoiavote.com/license.html
// </copyright>
// <summary>
//     This file contains the Program class.
// </summary>
// <revision revisor="dev13" date="11/19/2009" version="1.1.3.7">
//     Added documentation header
// </revision>
// -----------------------------------------------------------------------------

namespace Tester
{
    #region Using directives

    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;

    using com.thentech.ds.ext;
    using com.thentech.pdf.ext;

    using Sequoia.Ems.Imaging;
    using Sequoia.Ems.Imaging.BarCode;
    using Sequoia.Ems.Interop;
    using Sequoia.Ems.Interop.Java;

    #endregion

    /// <summary>
    ///     The program class
    /// </summary>
    /// <externalUnit/>
    /// <revision revisor="dev13" date="11/19/2009" version="1.1.3.7">
    ///     Added documentation header
    /// </revision>
    public class Program
    {
        /// <summary>
        ///     The main program method.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/19/2009" version="1.1.3.7">
        ///     Added documentation header
        /// </revision>
        public static void Main(string[] args)
        {
            // TestCode128C();
            TestCode128C_b();

            // Test1bppBitmap();
            TestMessageBitmap();
        }

        #region TestCode128C

        /// <summary>
        ///     Tests the code128 C.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/19/2009" version="1.1.3.7">
        ///     Added documentation header
        /// </revision>
        private static void TestCode128C()
        {
            string text = "98723493871103000000";
            Bitmap barcode = Code128.Encode(text, 1, 0.125F, 96, 12, 0);
            PixelChar.DrawChar(barcode, 'M', 0, 2);

            Bitmap pixelchar = new Bitmap(70, 40);
            Graphics g = Graphics.FromImage(pixelchar);
            g.Clear(Color.White);
            g.Dispose();
            pixelchar.SetResolution(96, 96);
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (j * 7 + i < 26)
                    {
                        char ch = (char)(j * 7 + i + 65);
                        PixelChar.DrawChar(pixelchar, ch, 10 * i, 10 * j);
                    }
                }
            }

            MemoryInputStream misStream = new MemoryInputStream(barcode, 100);
            barcode.Dispose();
            barcode.Dispose();
            DSxInput pdfInput = new DSxInput(misStream);

            DSxSystem.setLicenseKey(
                "[Insert your license key here]");
            DSxTask task = new DSxTask();

            PDFxDocument pdfDoc = PDF.createDocument(task);
            PDFxContext context = pdfDoc.getContext();
            PDFxPage pdfPage = pdfDoc.createCustomPage(5, 2);

            PDFxImage bar = PDFxImage.createImageFromJPEG(pdfInput);
            double dpi = 96D,
                k = 72D / dpi;
            pdfPage.drawImage(
                18, 18, k * bar.getWidth(), k * bar.getHeight(), bar);

            bar =
                PDFxImage.createImageFromJPEG(
                    new DSxInput(new MemoryInputStream(pixelchar, 100)));
            pixelchar.Dispose();
            pdfPage.drawImage(
                18, 72, k * bar.getWidth(), k * bar.getHeight(), bar);

            DSxPDFDocument dsDocument = new DSxPDFDocument(pdfDoc);
            dsDocument.save(new DSxOutput(@"bin\Debug\DSxInput2.pdf"));
        }

        /// <summary>
        ///     Tests the code128 C_B.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/19/2009" version="1.1.3.7">
        ///     Added documentation header
        /// </revision>
        private static void TestCode128C_b()
        {
                string text = "982739487";
                Bitmap barcode = Code128.Encode(text, 2, 0.125F, 96);
                barcode.RotateFlip(RotateFlipType.Rotate90FlipY);

                barcode.Save(text + ".bmp", ImageFormat.Bmp);
        }

        #endregion

        #region test 1bpp bitmaps

        /// <summary>
        ///     Test1bpps the bitmap.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/19/2009" version="1.1.3.7">
        ///     Added documentation header
        /// </revision>
        private static void Test1bppBitmap()
        {
            Bitmap bmp = new Bitmap(10, 10, PixelFormat.Format1bppIndexed);
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData data = bmp.LockBits(
                rect, ImageLockMode.WriteOnly, bmp.PixelFormat);

            ClearBitmap(data);
            FillRectangle(data, 6, 7, 3, 2, true);

            bmp.UnlockBits(data);

            bmp.Save("1bpp.bmp", ImageFormat.Bmp);
        }

        /// <summary>
        ///     Clears the bitmap.
        /// </summary>
        /// <param name="data">The bitmap data.</param>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/19/2009" version="1.1.3.7">
        ///     Added documentation header
        /// </revision>
        private static void ClearBitmap(BitmapData data)
        {
            FillRectangle(data, 0, 0, data.Width, data.Height, false);
        }

        /// <summary>
        ///     Fills the rectangle.
        /// </summary>
        /// <param name="data">The bitmap data.</param>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="black">if set to <c>true</c> [black].</param>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/19/2009" version="1.1.3.7">
        ///     Added documentation header
        /// </revision>
        private static void FillRectangle(
            BitmapData data, 
            int x, 
            int y, 
            int width, 
            int height, 
            bool black)
        {
            int x2 = x + width - 1,
                y2 = y + height - 1;
            for (int i = x; i <= x2; i = i + 1)
            {
                for (int j = y; j <= y2; j = j + 1)
                {
                    SetPixel(data, i, j, black);
                }
            }
        }

        /// <summary>
        ///     Sets the pixel.
        /// </summary>
        /// <param name="data">The bitmap data.</param>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <param name="black">if set to <c>true</c> [black].</param>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/19/2009" version="1.1.3.7">
        ///     Added documentation header
        /// </revision>
        private static unsafe void SetPixel(
            BitmapData data, int x, int y, bool black)
        {
            byte* p = (byte*)data.Scan0.ToPointer();
            int index = y * data.Stride + (x >> 3);
            byte mask = (byte)(0x80 >> (x & 0x7));

            if (black)
            {
                p[index] &= (byte)(mask ^ 0xff);
            }
            else
            {
                p[index] |= mask;
            }
        }

        #endregion

        #region Test message bitmap

        /// <summary>
        ///     Tests the message bitmap.
        /// </summary>
        /// <externalUnit/>
        /// <revision revisor="dev13" date="11/19/2009" version="1.1.3.7">
        ///     Added documentation header
        /// </revision>
        private static void TestMessageBitmap()
        {
            Bitmap bitmap = MessageBitmap.Draw(1234567890, 0.125F, 96);
            bitmap.RotateFlip(RotateFlipType.Rotate90FlipY);
            bitmap.Save("Invalid 1234567890.bmp", ImageFormat.Bmp);
        }

        #endregion
    }
}
