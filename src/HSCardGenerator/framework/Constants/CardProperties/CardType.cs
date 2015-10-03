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
    /// Enum with card type values.
    /// Based on type, each card has a picture for its face. Also, each card has custom properties based on its type.
    /// </summary>
    public enum CardType : int
    {
        Unknown     = -1, // Wrong type value
        Minion      = 0, // Minion card
        Spell       = 1, // Spell card
        Weapon      = 2, // Weapon card
        Hero        = 3, // Hero card (not collectible)
        HeroPower   = 4, // Hero Power card (not collectible)
        Enchantment = 5  // Enchantment card (not collectible and mainly for internal ingame stuff)
    }
}
