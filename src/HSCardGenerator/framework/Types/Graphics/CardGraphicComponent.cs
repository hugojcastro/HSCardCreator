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

namespace HSCardGenerator.framework.Types.Graphics
{
    /// <summary>
    /// Class used to store card element properties and their values
    /// As it uses a Font component, it must be IDisposable and free the component somewhere
    /// </summary>
    public class CardGraphicComponent : IDisposable
    {
        /// <summary>
        /// Definition of properties for CardGraphicComponent
        /// </summary>
        #region Properties
        // CardGraphic position of the element: top,left and dimensions
        public int Top    { get; set; } = 0;
        public int Left   { get; set; } = 0;
        public int Width  { get; set; } = 0;
        public int Height { get; set; } = 0;
        // Graphic Font and Outline colors
        public Brush textColor   { get; set; } = Brushes.White;
        public Brush borderColor { get; set; } = Brushes.Black;
        // Text outline size for element (if necessary)
        public float Outline { get; set; } = 0f;
        // Default font to use
        public Font font { get; set; } = null;
        #endregion

        /// <summary>
        /// Constructor. Just assign a default font to... font :)
        /// </summary>
        public CardGraphicComponent()
        {
            // Initialize to Belwe, Hearthstone standard :D
            font = new Font(Fonts.BelweFamily, 8.0f, FontStyle.Regular);
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
                font.Dispose();
            }
        }

        /// <summary>
        /// Disposable types usually implement a finalizer.
        /// </summary>
        ~CardGraphicComponent()
        {
            Dispose(false);
        }
    }
}