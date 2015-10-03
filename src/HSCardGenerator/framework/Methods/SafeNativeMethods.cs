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
using System.Runtime.InteropServices;
using System.Security;

namespace HSCardGenerator.framework
{
    /// <summary>
    /// Class used to reference or use OS 'native' values and methods, from its system libraries.
    /// This makes it hard to port the program to other OS but... well... shit happens.
    /// </summary>
    [SuppressUnmanagedCodeSecurity]
    internal static class SafeNativeMethods
    {
        /// <summary>
        /// Constants to move windows on drag.
        /// </summary>
        internal const int WM_NCLBUTTONDOWN = 0xA1;
        internal const int HT_CAPTION       = 0x02;

        /// <summary>
        /// Native method to load font from resources.
        /// https://msdn.microsoft.com/en-us/library/windows/desktop/dd183325(v=vs.85).aspx
        /// </summary>
        /// <param name="pbFont">Pointer to Font data from resources</param>
        /// <param name="cbFont">Size of Font data</param>
        /// <param name="pdv">Reserved (Null, zero)</param>
        /// <param name="pcFonts">it returns the number of fonts installed</param>
        /// <returns></returns>
        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pdv, [In] ref uint pcFonts);

        /// <summary>
        /// Used to move windows on drag.
        /// By sending the proper messages, we can do the window draggable and move it by itself
        /// https://msdn.microsoft.com/en-us/library/windows/desktop/ms644950(v=vs.85).aspx
        /// </summary>
        /// <param name="hWnd">Handle of the window to send messages to</param>
        /// <param name="Msg">Message to send</param>
        /// <param name="wParam">Aditional message-specific info (if necessary)</param>
        /// <param name="lParam">Aditional message-specific info (if necessary)</param>
        /// <returns></returns>
        // Used to move windows on drag
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// Releases mouse capture from a window.
        /// We will use it with SendMessage to move windows by dragging the cursor from anywhere :)
        /// https://msdn.microsoft.com/en-us/library/windows/desktop/ms646261(v=vs.85).aspx
        /// </summary>
        /// <returns></returns>
        [DllImportAttribute("user32.dll", CharSet = CharSet.Auto)]
        internal static extern bool ReleaseCapture();
    }
}