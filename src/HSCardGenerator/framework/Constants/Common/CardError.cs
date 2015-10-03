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

namespace HSCardGenerator.framework.Constants.Common
{
    /// <summary>
    /// Enum values to identify errors when parsing cards
    /// </summary>
    public enum CardError : byte
    {
        None       = 0, // All fine (yay!)
        BadType    = 1, // Wrong / Unknown card type
        BadClass   = 2, // Wrong / Unknown card class/hero
        BadSet     = 3, // Wrong / Unknown card set
        BadQuality = 4, // Wrong / Unknown card rarity
        BadRace    = 5, // Wrong / Unknown card race
        BadName    = 6  // Empty card name
    }
}
