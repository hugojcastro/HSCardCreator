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

namespace HSCardGenerator.framework.Constants.Graphics
{
    // Enum used to define a curve type to draw names in cards
    public enum CurveType : byte
    {
        None      = 0, // No curve (weird)
        Linear    = 1, // Linear curve (a line defined by 2 points)
        Quadratic = 2, // Quadratic curve (a curve defined by 3 points, like an inverted 'U')
        Cubic     = 3  // Cubic curve (a curve defined by 4 points, like a laying 'S')
    }
}
