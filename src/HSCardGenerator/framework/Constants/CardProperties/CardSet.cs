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
    /// Enum with know card sets.
    /// Not all sets contain collectible cards, and not all cards for each set are collectibles.
    /// </summary>
    public enum CardSet : int
    {
        Unknown            = -1, // wrong set?
        Basic              = 0,  // "Basic"
        Classic            = 1,  // "Classic"
        Naxxramas          = 2,  // "Curse of Naxxramas"
        GoblinsVsGnomes    = 3,  // "Goblins vs Gnomes"
        BlackRockMountain  = 4,  // "Blackrock Mountain"
        TheGrandTournament = 5,  // "The Grand Tournament"
        Promotion          = 6,  // "Promotion"
        Reward             = 7,  // "Reward"
        Credits            = 8,  // "Credits"
        Debug              = 9,  // "Debug"
        HeroSkins          = 10, // "Hero Skins"
        Missions           = 11, // "Missions"
        System             = 12, // "System"
        TavernBrawl        = 13  // "Tavern Brawl"
    }
}
