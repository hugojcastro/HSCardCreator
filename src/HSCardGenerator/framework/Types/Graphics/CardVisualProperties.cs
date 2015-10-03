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
    /// Class used to store common values for card elements (the 'base' class)
    /// </summary>
    public class CardVisualProperties : IDisposable
    {
        /// <summary>
        /// Fixed properties (they should not change value)
        /// </summary>
        #region Definitions of common properties
        // Card "face", card picture
        public virtual CardGraphicComponent Face            { get; } = new CardGraphicComponent { Top = 0,   Left = 0,   Width = 380, Height = 550 };
        // Banner for minion race. As it's only for minions, we can define it as "constant", "fixed".
        public virtual CardGraphicComponent RaceBracket     { get; } = new CardGraphicComponent { Top = 474, Left = 108, Width = 176, Height = 43 };
        // Text for minion race. As it's only for minions, we can define it as "constant", "fixed".
        public virtual CardGraphicComponent RaceBracketText { get; } = new CardGraphicComponent { Top = 488, Left = 206, Width = 160, Height = 36, font = new Font(Fonts.BelweFamily, 18.0f, FontStyle.Regular), Outline = 4.0f };
        // Card name: font is the same for all, only change the curve. Position is stored in curve.
        public virtual CardGraphicComponent Name            { get; } = new CardGraphicComponent { font = new Font(Fonts.BelweFamily, 26.0f, FontStyle.Regular), Outline = 4.75f };
        /// <summary>
        /// Custom properties (each card should have a custom value for all of these and their properties)
        /// </summary>
        // Card cost (all should have one, but located in diferent places)
        public virtual CardGraphicComponent Cost        { get; } = null;
        // Card attack (minions) / damage(weapons)
        public virtual CardGraphicComponent Attack      { get; } = null;
        // Card health (minions) / durability (weapons)
        public virtual CardGraphicComponent Health      { get; } = null;
        // Card description. Although font is the same, position and size are diferent for each card type.
        public virtual CardGraphicComponent Description { get; } = null;
        // Background for card (portrait, better said).
        public virtual CardGraphicComponent Background  { get; } = null;
        // Watermark for card sets.
        public virtual CardGraphicComponent Watermark   { get; } = null;
        // This is used to get stuff from resources. Part of the path to resource.
        public virtual string typePath                  { get; } = "";
        // For pathcurves, used to draw card name. All should have one :D
        public virtual PointF[] namePoints              { get; } = null;
        #endregion

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
                Face.Dispose();
                RaceBracket.Dispose();
                RaceBracketText.Dispose();
                Name.Dispose();
                // Free custom properties if necessary
                if (Cost != null)        Cost.Dispose();
                if (Attack != null)      Attack.Dispose();
                if (Health != null)      Health.Dispose();
                if (Description != null) Description.Dispose();
                if (Background != null)  Background.Dispose();
                if (Watermark != null)   Watermark.Dispose();
            }
        }

        /// <summary>
        /// Disposable types usually implement a finalizer.
        /// </summary>
        ~CardVisualProperties()
        {
            Dispose(false);
        }
    }
}
