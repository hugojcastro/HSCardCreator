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
    /// Enum for card race values.
    /// Only apply to minion type cards. Races are used to get sinergy with other cards while playing.
    /// Race card is shown as a banner with race (localized value) in the lower part of the card, between health and attack values.
    /// </summary>
    public enum CardRace : int
    {
        Unknown = -1, // Wrong value?
        None    = 0,  // No race or not minion card
        Totem   = 1,  // Totem
        Demon   = 2,  // Demon (mainly warlock oriented)
        Mech    = 3,  // Mech (got bursted after GvG expansion)
        Dragon  = 4,  // Dragon (got bursted after TGT expansion)
        Beast   = 5,  // Beast (mainly hunter oriented)
        Murloc  = 6,  // Murloc (mrglmgrlmgrlglglglglrrrbahbah)
        Pirate  = 7   // Pirate (Rogues approve this race!)
    }
}
