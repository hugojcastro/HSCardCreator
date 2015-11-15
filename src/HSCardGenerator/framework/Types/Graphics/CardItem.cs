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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Reflection;

using HSCardGenerator.framework.Constants;
using HSCardGenerator.framework.Constants.Common;
using HSCardGenerator.framework.Constants.Graphics;

using TheArtOfDev.HtmlRenderer.WinForms;

namespace HSCardGenerator.framework.Types.Graphics
{
    /// <summary>
    /// Class representing a card and its elements.
    /// Class is used to create card item object that stored card data and paint them as bitmaps.
    /// The base picture is made and stored upon creation of the object, and consumer can get a custom copy in any locale the card has.
    /// Locales are used to store the diferent card texts with their own representation. Consumer can acces to any localized text properties by setting a locale and reading the corresponding properties for each text.
    /// </summary>
    public class CardItem : IDisposable
    {
        /// <summary>
        /// Properties for the card item
        /// </summary>
        #region Properties
        #region Id and Int properties
        public string Id      { get; set; } = "";
        public int Cost       { get; set; } = 0;
        public int Attack     { get; set; } = 0;
        public int Health     { get; set; } = 0;
        public int Durability { get; set; } = 0;
        #endregion

        #region Enum properties
        public CardClass Class     { get; set; } = CardClass.Neutral;
        public CardRace Race       { get; set; } = CardRace.None;
        public CardType Type       { get; set; } = CardType.Hero;
        public CardSet Set         { get; set; } = CardSet.Basic;
        public CardQuality Quality { get; set; } = CardQuality.Free;
        #endregion

        #region Localized text properties
        private Dictionary<Locales, string> name     { get; set; }
        // Name is a public read only string value. It returns the name of card in selected locale
        public string Name
        {
            get
            {
                string result;
                if (!name.TryGetValue(defaultLocale, out result))
                    result = "";
                return result;
            }
        }
        private Dictionary<Locales, string> text     { get; set; }
        // Text is a public read only string value. It returns the text of the card in the selected locale
        public string Text
        {
            get
            {
                string result;
                if (!text.TryGetValue(defaultLocale, out result))
                    result = "";
                return result;
            }
        }
        private Dictionary<Locales, string> raceName { get; set; }
        // RaceName is a public read only string value. It returns the race name of the minion in the selected locale
        public string RaceName
        {
            get
            {
                string result;
                if ((raceName == null) || (!raceName.TryGetValue(defaultLocale, out result)))
                    result = "";
                return result;
            }
        }
        #endregion

        #region Visual properties
        // Items to draw the card
        private CardVisualProperties Canvas { get; set; } = null;
        // Base template: card with all elements except name, text and race
        private Bitmap Template             { get; set; } = null;
        // Locale to use when getting name, text or race
        private Locales defaultLocale       { get; set; } = Locales.enUS;
        #endregion
        #endregion

        /// <summary>
        /// Constructor using independent var values as input parameters
        /// </summary>
        /// <param name="_id"></param>
        /// <param name="_cost"></param>
        /// <param name="_attack"></param>
        /// <param name="_health"></param>
        /// <param name="_durability"></param>
        /// <param name="_type"></param>
        /// <param name="_class"></param>
        /// <param name="_race"></param>
        /// <param name="_set"></param>
        /// <param name="_quality"></param>
        /// <param name="_filename"></param>
        public CardItem(string _id, int _cost, int _attack, int _health, int _durability, CardType _type, CardClass _class, CardRace _race, CardSet _set, CardQuality _quality, string _filename = "", bool useDefault = true)
        {
            // Id and Int types
            Id         = _id;
            Cost       = _cost;
            Attack     = _attack;
            Health     = _health;
            Durability = _durability;
            // Enum types
            Type    = _type;
            Class   = _class;
            Race    = _race;
            // these sets doesn't have a background, same as basic set
            Set     = ((_set == CardSet.Promotion) || (_set == CardSet.Reward)) ? CardSet.Basic : _set;
            Quality = _quality;
            // Localized strings
            name = new Dictionary<Locales, string>();
            text = new Dictionary<Locales, string>();
            raceName = ((Type == CardType.Minion) && (Race != CardRace.None)) ? new Dictionary<Locales, string>() : null;
            // Create template picture
            switch (Type)
            {
                case CardType.Minion:
                    Canvas = new CardMinion();
                    break;
                case CardType.Spell:
                    Canvas = new CardSpell();
                    break;
                case CardType.Weapon:
                    Canvas = new CardWeapon();
                    break;
                default:
                    throw new System.ArgumentException("Unknown card type!", "Type");
            }
            // Create base bitmap
            Template = new Bitmap(Config.cardWidth, Config.cardHeight);
            // Draw components if all is fine
            if (Canvas != null)
            {
                // And take graphics to render on it
                var g = System.Drawing.Graphics.FromImage(Template);

                // set atialiasing
                g.SmoothingMode     = SmoothingMode.HighQuality; // AntiAlias
                g.InterpolationMode = InterpolationMode.HighQualityBicubic; // High
                g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                g.PixelOffsetMode   = PixelOffsetMode.HighQuality;
                g.PageUnit          = GraphicsUnit.Pixel;
                // Get proper mask from resources according to card type
                var maskPath = string.Format("HSCardGenerator.Resources.Images.Mask.mask_{0}.png", Canvas.typePath.ToLower());
                using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(maskPath))
                {
                    // Apply mask
                    using (var mask = new Bitmap(Image.FromStream(stream)))
                    {
                        var maskColor = mask.GetPixel(0, 0);
                        var w         = mask.Width;
                        var h         = mask.Height;
                        // Get background if necessary
                        Bitmap background = null;
                        // If there's a file as background, use it:
                        if (_filename.Length != 0)
                        {
                            background = Graphic.resizeImage(Image.FromFile(_filename), new Rectangle(0, 0, w, h));
                        }
                        // No background; should we use the default one?
                        else if (useDefault)
                        {
                            using (Stream stream2 = Assembly.GetExecutingAssembly().GetManifestResourceStream("HSCardGenerator.Resources.Images.Cards.noimageavailable.png"))
                            {
                                // Apply mask
                                background = new Bitmap(Image.FromStream(stream2));
                            }
                        }
                        // If there's a background, paint it masked
                        if (background != null)
                        {
                            // Just check pixels for unmasked values
                            // (I know, it's not the best method but it's the only one I found that worked for me at the first run :D)
                            using (var destination = new Bitmap(w, h))
                            {
                                for (var lin = 0; lin < h; lin++)
                                {
                                    for (var col = 0; col < w; col++)
                                    {
                                        if (mask.GetPixel(col, lin) != maskColor)
                                            destination.SetPixel(col, lin, background.GetPixel(col, lin));
                                    }
                                }
                                // Draw resulting image in the right place
                                g.DrawImage(destination,
                                    Canvas.Background.Left + Canvas.Face.Left,
                                    Canvas.Background.Top + Canvas.Face.Top,
                                    Canvas.Background.Width,
                                    Canvas.Background.Height
                                );
                            }
                            // Free resources
                            background.Dispose();
                        }
                    }
                }
                // Get right image and draw it at right position
                var resource = string.Format("HSCardGenerator.Resources.Images.Cards.{0}.{1}.{2}_{3}_{4}.png",
                    Canvas.typePath,
                    Config.qualityPath[(byte)Quality],
                    Canvas.typePath.ToLower(),
                    Config.qualityPath[(byte)Quality],
                    Config.cardClass[(byte)Class]
                );
                // This is the "card" picture
                using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource))
                {
                    g.DrawImage(Image.FromStream(stream), Canvas.Face.Left, Canvas.Face.Top);
                }
                // Render CardGraphic Cost
                Graphic.drawOutlineText(g, Cost.ToString(), Canvas.Cost, Canvas.Face, true);
                // If not a Spell, render CardGraphic Attack/Damage and Health/Durability
                if (Type != CardType.Spell)
                {
                    // Render Attack/Damage
                    Graphic.drawOutlineText(g, Attack.ToString(), Canvas.Attack, Canvas.Face, true);
                    // Render Health/Durability
                    if (Type == CardType.Minion)
                    {
                        Graphic.drawOutlineText(g, Health.ToString(), Canvas.Health, Canvas.Face, true);
                    }
                    else
                    {
                        Graphic.drawOutlineText(g, Durability.ToString(), Canvas.Health, Canvas.Face, true);
                    }
                }
                // Render watermark for card set if necessary
                if (Set > CardSet.Basic)
                {
                    var mark = string.Format("HSCardGenerator.Resources.Images.Watermark.watermark_{0}_{1}.png",
                        Canvas.typePath.ToLower(),
                        Config.watermarkPath[(byte)Set]
                        );
                    using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(mark))
                    {
                        g.DrawImage(Image.FromStream(stream),
                            Canvas.Watermark.Left + Canvas.Face.Left,
                            Canvas.Watermark.Top + Canvas.Face.Top,
                            Canvas.Watermark.Width,
                            Canvas.Watermark.Height
                        );
                    }
                }
                // Render banner race if necessary
                if ((Type == CardType.Minion) && (Race != CardRace.None))
                {
                    // Render banner first
                    var bracket = "HSCardGenerator.Resources.Images.Brackets.race.png";
                    using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(bracket))
                    {
                        g.DrawImage(Image.FromStream(stream), Canvas.RaceBracket.Left + Canvas.Face.Left, Canvas.RaceBracket.Top + Canvas.Face.Top);
                    }
                }
            }
        }
                
        /// <summary>
        /// Add localized texts for a card item
        /// </summary>
        /// <param name="locale">Locales var type with locales to store strings for</param>
        /// <param name="_name">Name of card using given locale</param>
        /// <param name="_text">Text of card using given locale</param>
        /// <param name="_race">Race of card, if necessary, using given locale</param>
        public void addLocale(Locales locale, string _name, string _text="", string _race="")
        {
            // Check if locale exists
            if (name.ContainsKey(locale)) return;

            // Always add a name (must be given!)
            name.Add(locale, _name);
            // Add text if given (not all cards have one, right?)
            if (_text != "")
            {
                // Be careful: '$' and '#' are used to manage values affected by spell power, in example.
                text.Add(locale, string.Join("", _text.Split('$', '#')));
            }
            // Add race if given and it applies (not all minions have one, right?)
            if ((Type == CardType.Minion) && (Race != CardRace.None) && (_race != ""))
            {
                raceName.Add(locale, _race);
            }
            // Set this locale as default
            defaultLocale = locale;
        }
        
        /// <summary>
        /// Set locale to return name and other text values, mainly to use it with comboboxes, etc.
        /// </summary>
        /// <param name="locale">Locale to use as default</param>
        public void setLocale(Locales locale)
        {
            defaultLocale = locale;
        }
        
        /// <summary>
        /// Get all locales used in the card.
        /// It helps to know how many localized texts has the card
        /// </summary>
        /// <returns></returns>
        public List<Locales> getLocales()
        {
            var locales = new List<Locales>();
            // Names *must* be in all locales for this card, so we use it as reference
            foreach (var kvp in name)
            {
                // For each name, add its locale to list
                locales.Add(kvp.Key);
            }
            // return list
            return locales;
        }

        /// <summary>
        /// Check if card has texts for a locale.
        /// As name is the only text that never can be empty, we use it as reference.
        /// </summary>
        /// <param name="locale">Locale to check if card contains it</param>
        /// <returns></returns>
        public bool hasLocale(Locales locale)
        {
            // Return value as check for dictionary key
            return name.ContainsKey(locale);
        }

        /// <summary>
        /// Method to get card picture with texts in default locale and specified size
        /// </summary>
        /// <param name="width">Width of the picture (default: 380px)</param>
        /// <param name="height">Height of the picture (default: 550px)</param>
        /// <returns></returns>
        public Bitmap Picture(int width = 380, int height = 550)
        {
            // The best choice is to get a card in any locale, so we just do this method as a wrapper for the "main" one, using selected/default locale
            return Picture(defaultLocale, width, height);
        }
        
        /// <summary>
        /// Method to get card picture with texts in given locale and specified size
        /// </summary>
        /// <param name="locale">Locales var type with localization to take texts for</param>
        /// <param name="width">Width of the picture (default: 380px)</param>
        /// <param name="height">Height of the picture (default: 550px)</param>
        /// <returns></returns>
        public Bitmap Picture(Locales locale, int width = 380, int height = 550)
        {
            // Check conditions to proper render picture
            string value;
            // If no right canvas or card name in locale, return null
            if ((Canvas == null) || (!name.TryGetValue(locale, out value)))
            {
                return null;
            }
            // Create a bitmap
            var bmp = (Bitmap)Template.Clone();
            var g   = System.Drawing.Graphics.FromImage(bmp);
            // Assign some graphic properties for drawing
            g.PageUnit          = GraphicsUnit.Pixel;
            g.SmoothingMode     = SmoothingMode.HighQuality; // better than AntiAlias...?
            g.InterpolationMode = InterpolationMode.HighQualityBicubic; // better than High...?
            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            g.PixelOffsetMode   = PixelOffsetMode.HighQuality;
            // Render Name
            PathCurve textCurve = new PathCurve(Canvas.namePoints);
            textCurve.renderText(g, value, Canvas.Name, true, Config.debug);
            // Render Description text (from html) if necessary
            if (text.TryGetValue(locale, out value))
            {
                var border = (Config.debug) ? "1px solid red" : "0px";
                var html = string.Format(
                    "<table style=\"border:{0}; padding:0px; margin:0px;\"><tr><td style=\"width:{1}px; height:{2}px; vertical-align:middle; text-align:center; font-family:'Franklin Gothic', '{3}'; font-size:{4}px; color:#{5}; line-height:25px; }}\">{6}</td></tr></table>",
                    border,
                    Canvas.Description.Width - 4,
                    Canvas.Description.Height - 4,
                    Canvas.Description.font.FontFamily.Name,
                    Canvas.Description.font.Size,
                    Config.cardDescColors[(byte)Type],
                    value
                    );
                using (Image image = HtmlRender.RenderToImageGdiPlus(html, Canvas.Description.Width, Canvas.Description.Height, TextRenderingHint.AntiAlias))
                {
                    g.DrawImage(image, Canvas.Description.Left + Canvas.Face.Left, Canvas.Description.Top + Canvas.Face.Top);
                }
            }
            // Render Race name if necessary
            if ((Type == CardType.Minion) && (Race != CardRace.None) && (raceName.TryGetValue(locale, out value)))
            {
                Graphic.drawOutlineText(g, value, Canvas.RaceBracketText, Canvas.Face, true);
            }
            // Return the resulting image
            if ((bmp.Height != height) || (bmp.Width != width))
            {
                using (var fixedImage = new Bitmap(width, height))
                {
                    using (var graphics = System.Drawing.Graphics.FromImage(fixedImage))
                    {
                        var source = new Rectangle(0, 0, bmp.Width, bmp.Height);
                        var target = new Rectangle(0, 0, width, height);

                        graphics.PageUnit = GraphicsUnit.Pixel;
                        graphics.DrawImage(bmp, target, source, GraphicsUnit.Pixel);
                    }
                    // Dispose previous image
                    bmp.Dispose();
                    // Create new one
                    bmp = (Bitmap)fixedImage.Clone();
                }
            }
            // all fine
            return bmp;
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
                if (Canvas != null) Canvas.Dispose();
                if (Template != null) Template.Dispose();
                name.Clear();
                text.Clear();
                raceName.Clear();
            }
        }

        /// <summary>
        /// Disposable types usually implement a finalizer.
        /// </summary>
        ~CardItem()
        {
            Dispose(false);
        }
    }
}
