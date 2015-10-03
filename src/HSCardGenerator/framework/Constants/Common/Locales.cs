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
    /// Enum values to identify locales
    /// </summary>
    public enum Locales : int
    {
        unkn = -1, // Wrong value?
        enUS = 0, // English (USA)
        enGB = 1, // English (Britain)
        esES = 2, // Spanish (Spain)
        esMX = 3, // Spanish (Latin America) - My apologies for using only 'Mexican' as reference. It's the way the game does it :( 
        deDE = 4, // Deutsch (German)
        frFR = 5, // French (France)
        itIT = 6, // Italian (Italy)
        koKR = 7, // Korean (Korea)
        plPL = 8, // Polish (Poland)
        ptBR = 9, // Portuguese (Brazil)
        ruRU = 10, // Russian
        zhCN = 11, // Chinese (Simplified, China)
        zhTW = 12  // Chinese (Traditional, Taiwan)
    }
}
