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
    /// Vard rarity as enum value.
    /// Each card has a rarity based on its probability of appearance in packs.
    /// Rarity is identified in card picture by using a colored gem under card's name.
    /// </summary>
    public enum CardQuality : int
    {
        Unknown   = -1, // Wrong value?
        Free      = 0,  // No rarity. Souldbound cards, collective ones or from promotions
        Common    = 1,  // Common. White gem.
        Rare      = 2,  // Rare. Blue gem.
        Epic      = 3,  // Epic. Magenta gem.
        Legendary = 4   // Legendary. Golden gem and minions have a bracket with the shape of a dragon around portrait.
    }
}
