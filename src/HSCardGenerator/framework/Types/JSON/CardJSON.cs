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

using System.Runtime.Serialization;

namespace HSCardGenerator.framework.Types.JSON
{
    /// <summary>
    /// Class to parse JSON files with card definitions.
    /// Cards are composed of string fields with diferent properties. Not all are needed.
    /// We will specify a default value to speed up any future parse process.
    /// </summary>
    [DataContract]
    public class CardJSON
    {
        // Use default values, so in case property not present in json data, you won't need to do weird checks to avoid exceptions
        // Short name, image filename; all cards
        [DataMember(Name = "id", IsRequired = true)]
        public string Id { get; set; } = "";
        // name; all cards
        [DataMember(Name = "name", IsRequired = true)]
        public string Name { get; set; } = "";
        // type (Minion, Spell, Weapon...); I guess not all
        [DataMember(Name = "type", IsRequired = true)]
        public string Type { get; set; } = "Hero";
        // Text in card; description. Change '$' and '#' to ''
        [DataMember(Name = "text", IsRequired = false)]
        public string Text { get; set; } = "";
        // Cost; I guess all
        [DataMember(Name = "cost", IsRequired = false)]
        public byte Cost { get; set; } = 0;
        // Attack; only minions and weapons
        [DataMember(Name = "attack", IsRequired = false)]
        public byte Attack { get; set; } = 0;
        // Health; only minions
        [DataMember(Name = "Health", IsRequired = false)]
        public byte Health { get; set; } = 0;
        // Durability; only weapons
        [DataMember(Name = "durability", IsRequired = false)]
        public byte Durability { get; set; } = 0;
        // Only important collectible ones (so value=true)
        [DataMember(Name = "collectible", IsRequired = false)]
        public bool Collectible { get; set; } = false;
        // Rarity of the card; allowed values: 'Free', 'Common', 'Rare', 'Epic', 'Legendary'
        [DataMember(Name = "rarity", IsRequired = false)]
        public string Rarity { get; set; } = "Free";
        // Hero class (if present; otherwise -> Neutral); 'Hunter', 'Druid', 'Shaman', 'Priest', 'Warlock', 'Mage', 'Rogue', 'Warrior', 'Paladin'
        [DataMember(Name = "playerClass", IsRequired = false)]
        public string PlayerClass { get; set; } = "Neutral";
        // Race to show in card banner; 'Mech', 'Dragon', 'Totem', 'Murloc', 'Beast', 'Demon'...
        [DataMember(Name = "race", IsRequired = false)]
        public string Race { get; set; } = "None";
        // alternative text description
        [DataMember(Name = "flavor", IsRequired = false)]
        public string Flavor { get; set; } = "";
        // Who painted background art?
        [DataMember(Name = "artist", IsRequired = false)]
        public string Artist { get; set; } = "";
        // Array of strings with mechanics on this card: 'battlecry', 'deathrattle', etc.
        [DataMember(Name = "mechanics", IsRequired = false)]
        public string[] Mechanics { get; set; }
        // Description about how to get card
        [DataMember(Name = "howToGet", IsRequired = false)]
        public string HowToGet { get; set; } = "";
        // Description about how to get gold version
        [DataMember(Name = "howToGetGold", IsRequired = false)]
        public string HowToGetGold { get; set; } = "";
        // If it's elite card (idk)
        [DataMember(Name = "elite", IsRequired = false)]
        public string Elite { get; set; } = "";
        // not sure; 'Alliance', 'Neutral'...
        [DataMember(Name = "faction", IsRequired = false)]
        public string Faction { get; set; } = "";
        // not sure; 'Alliance', 'Neutral'...
        [DataMember(Name = "set", IsRequired = false)]
        public string Set { get; set; } = "";
    }
}
