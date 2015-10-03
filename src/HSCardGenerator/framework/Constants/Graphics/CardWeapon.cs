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

using System.Drawing;
using HSCardGenerator.framework.Types.Graphics;

namespace HSCardGenerator.framework.Constants.Graphics
{
    /// <summary>
    /// Class used to store static/constant values for weapon card elements, overriding when necessary
    /// </summary>
    public class CardWeapon : CardVisualProperties
    {
        // These ones define where to draw property texts and the text font to use
        public override CardGraphicComponent Cost        { get; } = new CardGraphicComponent { Top = 26,  Left = 56,  Width = 98,  Height = 98,  font = new Font(Fonts.BelweFamily,          80.0f, FontStyle.Regular), Outline = 6.0f };
        public override CardGraphicComponent Attack      { get; } = new CardGraphicComponent { Top = 448, Left = 64,  Width = 90,  Height = 90,  font = new Font(Fonts.BelweFamily,          74.0f, FontStyle.Regular), Outline = 6.0f };
        public override CardGraphicComponent Health      { get; } = new CardGraphicComponent { Top = 448, Left = 340, Width = 90,  Height = 90,  font = new Font(Fonts.BelweFamily,          74.0f, FontStyle.Regular), Outline = 6.0f };
        public override CardGraphicComponent Description { get; } = new CardGraphicComponent { Top = 372, Left = 74,  Width = 240, Height = 110, font = new Font(Fonts.FranklinGothicFamily, 20.0f, FontStyle.Bold) };
        public override CardGraphicComponent Background  { get; } = new CardGraphicComponent { Top = 70,  Left = 82,  Width = 230, Height = 230 };
        public override CardGraphicComponent Watermark   { get; } = new CardGraphicComponent { Top = 366, Left = 118, Width = 154, Height = 118 };
        // To be used to get card art from resources
        public override string typePath     { get; } = "Weapon";
        // Points for the curve (2, so it's a linear curve)
        public override PointF[] namePoints { get; } = new[] { new PointF(58, 282), new PointF(328, 282) };
    }
}