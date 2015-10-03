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
using System.Collections.Generic;

using HSCardGenerator.framework.Constants;
using HSCardGenerator.framework.Constants.Common;
using HSCardGenerator.framework.Methods;
using HSCardGenerator.framework.Types.JSON;

namespace HSCardGenerator.framework.Types.Graphics
{
    /// <summary>
    /// Class to store card collection data in memory.
    /// Cards are stored in a dictionary, as well as the locales used in the collection
    /// </summary>
    public class CardItemCollection : IDisposable
    {
        #region Definition of properties
        /// <summary>
        /// Dictionary with all card data.
        /// We use card ID as key (string) and card data as Value (CardItem)
        /// </summary>
        public Dictionary<string, CardItem> Cards { get; set; }
        /// <summary>
        /// Dictionary with all locales used for card collection.
        /// We use enum value as ID and string as Value
        /// </summary>
        public Dictionary<Locales, string> Languages { get; set; }
        #endregion

        /// <summary>
        /// Constructor. Creates the dictionary for pairs Id|card and enum|string
        /// </summary>
        public CardItemCollection()
        {
            // Dictionary for cards
            Cards     = new Dictionary<string, CardItem>();
            // Dictionary for locales
            Languages = new Dictionary<Locales, string>();
        }

        /// <summary>
        /// Add a card to collection from card item.
        /// Add a card to collection. If card already exists, check if it has all localized texts stored in the new one, adding the new ones.
        /// Update locales dictionary with new locales from card, if necessary
        /// </summary>
        /// <param name="card"></param>
        public void Add(CardItem card)
        {
            // If card already stored, add new texts only
            CardItem item;
            if (Cards.TryGetValue(card.Id, out item))
            {
                // Add all locales in card to collection
                var locales = card.getLocales();
                // So for each one, check if already stored
                foreach (var local in locales)
                {
                    // Not stored?
                    if (!Languages.ContainsKey(local))
                    {
                        // Do it (just do it, Shia!)
                        Languages.Add(local, General.getShortLocale(local));
                    }
                    // Not locale in stored card? store it
                    if (!item.hasLocale(local))
                    {
                        card.setLocale(local);
                        item.addLocale(local, card.Name, card.Text, card.RaceName);
                    }
                }
            }
            // Otherwise, add new card
            else
            {
                // Add card to collection
                Cards.Add(card.Id, card);
                // Add all locales in card to collection
                var locales = card.getLocales();
                // So for each one, check if already stored
                foreach (var local in locales)
                {
                    // Not stored?
                    if (!Languages.ContainsKey(local))
                    {
                        // Do it (just do it, Shia!)
                        Languages.Add(local, General.getShortLocale(local));
                    }
                }
            }
        }

        /// <summary>
        /// Add card to collection from JSON (string) parameters.
        /// Adds or updates a card in your collection, based on data passed as parameters.
        /// It is intended to be used to load data from JSON files and store it as cards/locales in your collection.
        /// </summary>
        /// <param name="card">JSON card data for card (mainly strings)</param>
        /// <param name="set">Set which card is related to</param>
        /// <param name="locale">Locale of the texts from the card</param>
        /// <param name="filename">File name of the picture used as background for card</param>
        /// <param name="race">Localized text for card race, if necessary</param>
        /// <param name="useDefault">Use default image if no filename is given</param>
        public void Add(CardJSON card, CardSet set, Locales locale, string filename, string race = "", bool useDefault = true)
        {
            // If card already stored, add new texts only if necessary
            CardItem item;
            if (Cards.TryGetValue(card.Id, out item))
            {
                // If locale is not included, add it
                if (!item.hasLocale(locale))
                {
                    item.addLocale(locale, card.Name, card.Text, race);
                }
            }
            else
            // Add new card
            {
                // Take JSON values
                var _type   = General.getCustomCardType(card.Type);
                var _class  = General.getCardClass(card.PlayerClass);
                var _race   = General.getCardRace(card.Race);
                var _rarity = General.getCardRarity(card.Rarity);
                // Create card from them
                item = new CardItem(card.Id, card.Cost, card.Attack, card.Health, card.Durability, _type, _class, _race, set, _rarity, filename, useDefault);
                // Add localized texts
                item.addLocale(locale, card.Name, card.Text, race);
                // Add it to collection
                Cards.Add(card.Id, item);
            }
            // If locale not included in locales, include it
            if (!Languages.ContainsKey(locale))
            {
                Languages.Add(locale, General.getShortLocale(locale));
            }
        }

        /// <summary>
        /// Assign a default locale for all card.
        /// This can be used to populate data with all text fields from cards in the same locale, in a fast and simple way
        /// </summary>
        /// <param name="locale">new Locale to be used</param>
        public void SetLocale(Locales locale)
        {
            // go for each card in collection and sets the default locale.
            foreach (KeyValuePair<string, CardItem> card in Cards)
            {
                // With this, when you take Name, Text or Race, they will be returned in this locale.
                card.Value.setLocale(locale);
            }
        }

        /// <summary>
        /// Quick method to clear all dictionaries of the collection in a fast way
        /// </summary>
        public void Clear()
        {
            // Clear card dictionary
            Cards.Clear();
            // Clear locales dictionary
            Languages.Clear();
        }

        /// <summary>
        /// Public implementation of Dispose pattern callable by consumers.
        /// </summary>
        public void Dispose()
        {
            // Do the job
            Dispose(true);
            // And interact with Garbage Collector
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Protected implementation of Dispose pattern.
        /// </summary>
        /// <param name="disposing">To grant only one use</param>
        protected virtual void Dispose(bool disposing)
        {
            // Free stuff
            if (disposing)
            {
                // Free managed objects
                Cards.Clear();
                Languages.Clear();
            }
        }

        /// <summary>
        /// Disposable types usually implement a finalizer.
        /// </summary>
        ~CardItemCollection()
        {
            Dispose(false);
        }
    }
}

