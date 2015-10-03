/*
 * This file is part of HSCardGenerator.
 *
 * Licensed under the MIT license. See LICENSE file in the project root for full license information.
 *
 * HSCardGenerator is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
 * warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 *
 * Hearthstone®: Heroes of Warcraft
 * ©2014 Blizzard Entertainment, Inc. All rights reserved. Heroes of Warcraft is a trademark, and Hearthstone is a registered trademark of Blizzard Entertainment, Inc. in the U.S. and/or other countries.
 *
 * HTMLRenderer is a library originally created by Jose Menendez Póo and maintained by ArthurHub. More information and license in: https://github.com/ArthurHub/HTML-Renderer
 *
 * Newtonsoft.Json is open source licensed to James Newton-King under the MIT license. More information and license in: https://github.com/JamesNK/Newtonsoft.Json
 */

using System;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.InteropServices;

namespace HSCardGenerator.framework
{
    /// <summary>
    /// Class to load fonts from resources and use them in the program
    /// Made it IDisposable to ensure the release of resources
    /// </summary>
    public class Fonts : IDisposable
    {
        /// <summary>
        /// Font Families to be used
        /// </summary>
        #region FontFamily static vars
        public static FontFamily BelweFamily          = null;
        public static FontFamily FranklinGothicFamily = null;
        public static FontFamily EasternCharsFamily   = null;
        #endregion

        /// <summary>
        /// Load font family from resource.
        /// </summary>
        /// <param name="fontArray">Data with font from resource</param>
        /// <returns></returns>
        private static FontFamily loadFont(byte[] fontArray)
        {
            // Get font byte array length
            int dataLength = fontArray.Length;

            // Assign memory and copy font to it
            IntPtr ptrData = Marshal.AllocCoTaskMem(dataLength);
            Marshal.Copy(fontArray, 0, ptrData, dataLength);

            // Load font resource
            uint cFonts = 0;
            SafeNativeMethods.AddFontMemResourceEx(ptrData, (uint)fontArray.Length, IntPtr.Zero, ref cFonts);

            // Pass it to the PrivateFontCollection object
            using (var pfc = new PrivateFontCollection())
            {
                pfc.AddMemoryFont(ptrData, dataLength);

                // Free not managed memory
                Marshal.FreeCoTaskMem(ptrData);

                // Return first (and only) font in family
                return pfc.Families[0];
            }
        }

        /// <summary>
        /// Main method to load font families (Belwe, FranklinGothic and eastern ones)
        /// Only Belwe and FranklinG are used :D
        /// </summary>
        public static void loadFonts()
        {
            BelweFamily          = loadFont(Properties.Resources.FontBelwe);
            FranklinGothicFamily = loadFont(Properties.Resources.FontFranklinGothic);
            EasternCharsFamily   = loadFont(Properties.Resources.FontEasternChars);
        }

        /// <summary>
        /// Public implementation of Dispose pattern callable by consumers.
        /// </summary>
        public void Dispose()
        {
            // Do the job
            Dispose(true);
            // And interact with Garbage Collector
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Protected implementation of Dispose pattern.
        /// </summary>
        /// <param name="disposing">To grant only one use</param>
        protected virtual void Dispose(bool disposing)
        {
            // Free stuff
            if (disposing)
            {
                // Free managed objects
                if (BelweFamily != null)          BelweFamily.Dispose();
                if (FranklinGothicFamily != null) FranklinGothicFamily.Dispose();
                if (EasternCharsFamily != null)   EasternCharsFamily.Dispose();
            }
        }

        /// <summary>
        ///  Disposable types usually implement a finalizer.
        /// </summary>
        ~Fonts()
        {
            Dispose(false);
        }
    }
}
