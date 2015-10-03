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
    /// Class used to store static/constant values for minion card elements, overriding when necessary
    /// </summary>
    public class CardMinion : CardVisualProperties
    {
        // These ones define where to draw property texts and the text font to use
        public override CardGraphicComponent Cost        { get; } = new CardGraphicComponent { Top = 26,  Left = 56,  Width = 98,  Height = 98,  font = new Font(Fonts.BelweFamily,          80.0f, FontStyle.Regular), Outline = 6.0f };
        public override CardGraphicComponent Attack      { get; } = new CardGraphicComponent { Top = 448, Left = 66,  Width = 90,  Height = 90,  font = new Font(Fonts.BelweFamily,          74.0f, FontStyle.Regular), Outline = 6.0f };
        public override CardGraphicComponent Health      { get; } = new CardGraphicComponent { Top = 448, Left = 338, Width = 90,  Height = 90,  font = new Font(Fonts.BelweFamily,          74.0f, FontStyle.Regular), Outline = 6.0f };
        public override CardGraphicComponent Description { get; } = new CardGraphicComponent { Top = 362, Left = 66,  Width = 248, Height = 110, font = new Font(Fonts.FranklinGothicFamily, 20.0f, FontStyle.Bold) };
        public override CardGraphicComponent Background  { get; } = new CardGraphicComponent { Top = 38,  Left = 50,  Width = 292, Height = 292 };
        public override CardGraphicComponent Watermark   { get; } = new CardGraphicComponent { Top = 360, Left = 124, Width = 148, Height = 128 };
        // To be used to get card art from resources
        public override string typePath     { get; } = "Minion";
        // Points for the curve (4, so it's a cubic curve)
        public override PointF[] namePoints { get; } = new[] { new PointF(54, 298), new PointF(146, 286), new PointF(266, 274), new PointF(342, 290) };
    }
}
