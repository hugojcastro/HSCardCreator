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
    /// Class to store default settings / strings for the tool
    /// </summary>
    public class Config
    {
        // Real full card dimensions
        public static int cardWidth  = 380;
        public static int cardHeight = 550;
        // To squish characters on string
        public static float align = 3.25f;
        // filters to open files with same component
        public static string filterImages    = "Image Files|*.png;*.jpg;*.tga;*.bmp";
        public static string filterJSON      = "JSON Files|*.json";
        public static string extensionImages = "*.png";
        public static string extensionJSON   = "*.json";
        // These string arrays are used to access localized resource strings easily
        public static string[] cardClasses   = { "strNeutral", "strWarrior", "strPaladin", "strHunter", "strRogue", "strPriest", "strShaman", "strMage", "strWarlock", "strDruid" };
        public static string[] cardRaces     = { "strNone", "strTotem", "strDemon", "strMech", "strDragon", "strBeast", "strMurloc", "strPirate" };
        public static string[] cardTypes     = { "strMinion", "strSpell", "strWeapon" };
        public static string[] cardQualities = { "strFree", "strCommon", "strRare", "strEpic", "strLegendary" };
        public static string[] cardSets      = { "strBasic", "strClassic", "strNax", "strGvG", "strBRM", "strTGT" };
        // Language strings (short and long form, and cultureinfo format)
        public static string[] locales        = { "enUS", "enGB", "esES", "esMX", "deDE", "frFR", "itIT", "koKR", "plPL", "ptBR", "ruRU", "zhCN", "zhTW" };
        public static string[] localesFull    = { "English (US)", "English (GB)", "Español (ES)", "Español (LA)", "Deutsch", "Français", "Italiano", "한국의", "Polski", "Português", "русский", "普通話", "国语" };
        public static string[] cultureLocales = { "en-US", "en-GB", "es-ES", "es-MX", "de-DE", "fr-FR", "it-IT", "ko-KR", "pl-PL", "pt-BR", "ru-RU", "zh-CN", "zh-TW" };
        // String used to access resource data
        public static string[] watermarkPath  = { "", "cla", "nax", "gvg", "brm", "tgt" };
        public static string[] qualityPath    = { "free", "common", "rare", "epic", "legendary" };
        public static string[] cardDescColors = { "000000", "000000", "ffffff" };
        public static string[] cardClass      = { "neutral", "warrior", "paladin", "hunter", "rogue", "priest", "shaman", "mage", "warlock", "druid" };
        // Will we show aditional stuff?
        public static bool debug = false;
    }
}
