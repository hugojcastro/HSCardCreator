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

namespace HSCardGenerator.framework.Constants
{
    /// <summary>
    /// Card class as enum.
    /// There are cards only allowed for specific heroes or neutral card, allowed for all.
    /// </summary>
    public enum CardClass : int
    {
        Unknown = -1, // Wrong value?
        Neutral = 0,  // Neutral card: use allowed for all classes
        Warrior = 1,  // Card for Warrior only
        Paladin = 2,  // Card for Paladin only
        Hunter  = 3,  // Card for Hunter only
        Rogue   = 4,  // Card for Rogue only
        Priest  = 5,  // Card for Priest only
        Shaman  = 6,  // Card for Shaman only
        Mage    = 7,  // Card for Mage only
        Warlock = 8,  // Card for Warlock only
        Druid   = 9   // Card for Druid only
    }
}