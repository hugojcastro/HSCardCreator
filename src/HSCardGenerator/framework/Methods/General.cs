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

using System;

using HSCardGenerator.framework.Constants;
using HSCardGenerator.framework.Constants.Common;

namespace HSCardGenerator.framework.Methods
{
    /// <summary>
    /// Functions used in several classes and contexts
    /// </summary>
    public class General
    {
        /// <summary>
        /// Arrays used in functions, mainly from the JSON values.
        /// </summary>
        #region String constant arrays
        private static string[] mainCardTypes   = { "Minion", "Spell", "Weapon", "Hero", "Hero Power", "Enchantment" };
        private static string[] customCardTypes = { "Minion", "Spell", "Weapon" };
        private static string[] cardQualities   = { "Free", "Common", "Rare", "Epic", "Legendary" };
        private static string[] cardClasses     = { "Neutral", "Warrior", "Paladin", "Hunter", "Rogue", "Priest", "Shaman", "Mage", "Warlock", "Druid" };
        public static string[] cardRaces        = { "None", "Totem", "Demon", "Mech", "Dragon", "Beast", "Murloc", "Pirate" };
        private static string[] cardSets        = { "Basic", "Classic", "Curse of Naxxramas", "Goblins vs Gnomes", "Blackrock Mountain", "The Grand Tournament", "League Of Explorers", "Whispers Of The Old Gods",
                                                    "Promotion", "Reward", "Credits", "Debug", "Hero Skins", "Missions", "System", "Tavern Brawl"
        };
        #endregion

        /// <summary>
        /// Check card values to determine if they are right (i.e. are into allowed range for each property)
        /// </summary>
        /// <param name="_type">Card type as enum</param>
        /// <param name="_class">Card class as enum</param>
        /// <param name="_set">Card set as enum</param>
        /// <param name="_race">Card race as enum</param>
        /// <param name="_quality">Card quality as enum</param>
        /// <returns></returns>
        public static CardError checkCard(CardType _type, CardClass _class, CardSet _set, CardRace _race, CardQuality _quality, string _name)
        {
            if ((_type < CardType.Minion) || (_type > CardType.Weapon))
            {
                return CardError.BadType;
            }
            // Wrong card class?
            if ((_class < CardClass.Neutral) || (_class > CardClass.Druid))
            {
                return CardError.BadClass;
            }
            // Wrong card set?
            if ((_set < CardSet.Basic) || (_set > CardSet.WhispersOfTheOldGods))
            {
                return CardError.BadSet;
            }
            // Wrong minion race?
            if ((_type == CardType.Minion) && ((_race < CardRace.None) || (_race > CardRace.Pirate)))
            {
                return CardError.BadRace;
            }
            // Wrong card quality?
            if ((_quality < CardQuality.Free) || (_quality > CardQuality.Legendary))
            {
                return CardError.BadQuality;
            }
            // Wrong (empty) name?
            if (_name.Length == 0)
            {
                return CardError.BadName;
            }
            return CardError.None;
        }

        /// <summary>
        /// Returns a valid string to use with CultureInfo
        /// </summary>
        /// <param name="lang">Language as string</param>
        /// <returns></returns>
        public static string getCultureLocale(string lang)
        {
            var lower = lang.ToLower();
            for (var i = 0; i < Config.locales.Length; i++)
            {
                if (Config.locales[i].ToLower() == lower)
                {
                    return Config.cultureLocales[i];
                }
            }
            return "es-ES"; // my default value :D
        }

        /// <summary>
        /// Returns a valid string to use with CultureInfo
        /// </summary>
        /// <param name="lang">language as enum value</param>
        /// <returns></returns>
        public static string getCultureLocale(Locales lang)
        {
            return Config.cultureLocales[(int)lang];
        }

        /// <summary>
        /// Get card type from string for JSON imports
        /// </summary>
        /// <param name="_type">string with card type</param>
        public static CardType getMainCardType(string _type)
        {
            return ((CardType)Array.IndexOf(mainCardTypes, _type));
        }

        /// <summary>
        /// Get card type from string for internal imports
        /// </summary>
        /// <param name="_type">string with card type</param>
        public static CardType getCustomCardType(string _type)
        {
            return ((CardType)Array.IndexOf(customCardTypes, _type));
        }

        /// <summary>
        /// Check if card type is valid (only minion, weapon and spell are used
        /// </summary>
        /// <param name="_type">CardType var with type</param>
        /// <returns></returns>
        public static bool isValidCardType(CardType _type)
        {
            return ((_type != CardType.Unknown) && (_type < CardType.Hero));
        }

        /// <summary>
        /// Return card class from string
        /// </summary>
        /// <param name="_class">String with card class</param>
        /// <returns></returns>
        public static CardClass getCardClass(string _class)
        {
            return ((CardClass)Array.IndexOf(cardClasses, _class));
        }

        /// <summary>
        /// Get cardset from string
        /// </summary>
        /// <param name="_cardset">String with cardset name</param>
        /// <returns></returns>
        public static Constants.CardSet getCardSet(string _cardset)
        {
            return ((CardSet)Array.IndexOf(cardSets, _cardset));
        }

        /// <summary>
        /// Check if cardset is valid or not (only released, not internal, are allowed)
        /// </summary>
        /// <param name="_cardset">CardSet type var to check</param>
        /// <returns></returns>
        public static bool isValidCardSet(CardSet _cardset)
        {
            return ((_cardset != CardSet.Unknown) && (_cardset < CardSet.Credits));
        }

        /// <summary>
        /// Get card rarity as enum value from string given as parameter
        /// </summary>
        /// <param name="_rarity">String with card rarity (Free, Common, Rare, Epic, Legendary). Empty string means Free card rarity.</param>
        /// <returns></returns>
        public static CardQuality getCardRarity(string _rarity)
        {
            // No string? Rarity is Free
            if (_rarity.Length == 0) return CardQuality.Free;
            // Take the right rarity value as enum
            return ((CardQuality)Array.IndexOf(cardQualities, _rarity));
        }

        /// <summary>
        /// Get card race as enum value from string given as parameter
        /// </summary>
        /// <param name="_race">String with race for minion card. If no string, no race.</param>
        /// <returns></returns>
        public static CardRace getCardRace(string _race)
        {
            // No string? (i.e. weapons, spells or minion without race) Race is "none".
            if (_race.Length == 0) return CardRace.None;
            // Get 
            return ((CardRace)Array.IndexOf(cardRaces, _race));
        }

        /// <summary>
        /// Get Locale from filename. It looks for locale string in filename.
        /// in example: AllSets.enUS.json will return a Locales.enUS value.
        /// </summary>
        /// <param name="_filename">File name to check locale</param>
        /// <returns></returns>
        public static Locales getLocaleFromFilename(string _filename)
        {
            // Default value: unknown locale
            var idx = -1;
            // Check filename to contain any of all locale strings (CASE SENSITIVE)
            for (var i = 0; i < Config.locales.Length; i++)
            {
                // If filename contains this one
                if (_filename.IndexOf(Config.locales[i]) != -1)
                {
                    // return it
                    idx = i;
                    break;
                }
            }
            // not found? unknown locale
            return ((Locales)idx);
        }

        /// <summary>
        /// Get locale enum value from string value.
        /// Converts string values with valid locales to enum values (case insensitive).
        /// </summary>
        /// <param name="_locale">String with locale to convert (i.e. enUS, eses, deDE, koKR, zhCN...).
        /// Note: strings are compared lowercase, so all forms are allowed)</param>
        /// <returns></returns>
        public static Locales getLocaleFromString(string _locale)
        {
            // Default: unknown
            var idx = -1;
            // Take parameter as lowercase
            var lower = _locale.ToLower();
            // Check each known locale
            for (var i = 0; i < Config.locales.Length; i++)
            {
                // Found?
                if (Config.locales[i].ToLower() == lower)
                {
                    // Take index and return
                    idx = i;
                    break;
                }
            }
            // Return index as enum
            return ((Locales)idx);
        }

        /// <summary>
        /// Get locale string from enum value (short form).
        /// Given a *known* locale value, returns a string corresponding to that locale in xxYY format (in example, esES, enGB...).
        /// </summary>
        /// <param name="_locale">Enum with locale</param>
        /// <returns></returns>
        public static string getShortLocale(Locales _locale)
        {
            // Get locale string from array based on its enum as position
            return Config.locales[(int)_locale];
        }

        /// <summary>
        /// Get locale string from enum value (long form).
        /// Given a *known* locale value, returns a string corresponding to that locale in long form.
        /// </summary>
        /// <param name="_locale">Enum with locale</param>
        /// <returns></returns>
        public static string getLongLocale(Locales _locale)
        {
            // Get locale string from array based on its enum as position
            return Config.localesFull[(int)_locale];
        }
    }
}
