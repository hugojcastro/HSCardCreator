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

using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;

using HSCardGenerator.framework.Constants.Common;
using HSCardGenerator.framework.Types.Graphics;

namespace HSCardGenerator.framework
{
    /// <summary>
    /// Class with custom graphic methods and constants
    /// </summary>
    public class Graphic
    {
        /// <summary>
        /// When looking for the best font size, it represents the minimum value it will have.
        /// </summary>
        public static float minFontSize = 4.0f;

        /// <summary>
        /// Draw an outlined text on a graphic.
        /// Draw a text using an outline in it, from custom CardGraphicComponent values. These are used to get position of text and destination picture/graphic.
        /// </summary>
        /// <param name="g">Graphic to draw the text</param>
        /// <param name="text">Text to draw</param>
        /// <param name="canvas">Values for position, font, etc. of the text</param>
        /// <param name="parent">If canvas has a parent, use it for relative position of the text</param>
        /// <param name="centered">If text must be drawn centered or not</param>
        public static void drawOutlineText(Graphics g, string text, CardGraphicComponent canvas, CardGraphicComponent parent, bool centered=false)
        {
            // Check parent to get proper offsets
            var offsetTop  = 0;
            var offsetLeft = 0;
            if (parent != null)
            {
                offsetLeft = parent.Left;
                offsetTop  = parent.Top;
            }
            // set atialiasing for drawing
            g.SmoothingMode     = SmoothingMode.HighQuality; // AntiAlias
            g.InterpolationMode = InterpolationMode.HighQualityBicubic; // High
            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            g.PixelOffsetMode   = PixelOffsetMode.HighQuality;
            // Create GraphicsPath to draw text
            using (GraphicsPath path = new GraphicsPath())
            {
                // Calculate position in case text must be draw centered or not
                float txtWidth = 0;
                if (centered)
                    txtWidth = getStringWidth(g, text, new Font(canvas.font.FontFamily, canvas.font.Size, canvas.font.Style, GraphicsUnit.Point, ((byte)(0)))) / 2;
                // Add string to path
                path.AddString(text, canvas.font.FontFamily, (int)canvas.font.Style, canvas.font.Size, new Point(canvas.Left + offsetLeft - (int)txtWidth, canvas.Top + offsetTop), StringFormat.GenericTypographic);
                // Draw text using this pen. This does the trick (with the path) to draw the outline
                using (Pen pen = new Pen(canvas.borderColor, canvas.Outline))
                {
                    pen.LineJoin = LineJoin.Round;

                    g.DrawPath(pen, path);
                    g.FillPath(canvas.textColor, path);
                }
            }
        }

        /// <summary>
        /// Get best font size if text too big for the place where it will be drawn
        /// </summary>
        /// <param name="g">Graphic where to draw text</param>
        /// <param name="text">Text to be drawn</param>
        /// <param name="font">Which font to use to draw text</param>
        /// <param name="expectedWidth">Width of the path/place where text will be drawn</param>
        /// <param name="squish">I do this to avoid the irregular spaces when using MeasureString</param>
        /// <returns></returns>
        public static Font findBestFitFont(Graphics g, string text, Font font, float expectedWidth, bool squish = false)
        {
            // We will shrink font unless we reach limit size or text fits in expected area/path
            var size    = (float)font.Size;
            // Create auxiliary font
            var tmpFont = new Font(font.FontFamily, size, font.Style);
            // Get potential text width
            var width   = getStringWidth(g, text, tmpFont, squish);
            // Compute actual size and shrink if needed (but limited to min. size)
            while ((width > expectedWidth) && (width > minFontSize))
            {
                // Force-free resources
                tmpFont.Dispose();
                // Try new size (0.5% smaller)
                size    = size * 0.95f;
                // Create new font
                tmpFont = new Font(font.FontFamily, size, font.Style);
                // get text width and loop
                width   = getStringWidth(g, text, tmpFont, squish);
            }
            // return found font
            return tmpFont;
        }

        /// <summary>
        /// Return array with widths of chars for the strings, based on typography
        /// </summary>
        /// <param name="graphics">Graphics where text will be drawn</param>
        /// <param name="text">Text to be drawn</param>
        /// <param name="font">Font used to draw text</param>
        /// <param name="squish">Parameter to adjust weird spaces while using MeasureString</param>
        /// <returns></returns>
        public static IEnumerable<float> getCharacterWidths(Graphics graphics, string text, Font font, bool squish = false)
        {
            // The length of a space. Necessary because a space measured using StringFormat.GenericTypographic has no width.
            // We can't use StringFormat.GenericDefault for the characters themselves, as it adds unwanted spacing.
            var spaceLength = graphics.MeasureString(" ", font, Point.Empty, StringFormat.GenericDefault).Width;
            var factor      = (squish) ? Config.align : 0f;
            // Do a MeasureString for each char in text and add it to the array
            return text.Select(c => ((c == ' ') ? spaceLength : graphics.MeasureString(c.ToString(), font, Point.Empty, StringFormat.GenericTypographic).Width) - factor);
        }

        /// <summary>
        /// Get all width for a string using a font over a graphics object. Get all char widths and sum them
        /// </summary>
        /// <param name="graphics">Graphics where to draw text</param>
        /// <param name="text">Text to be drawn</param>
        /// <param name="font">Font used to draw text</param>
        /// <param name="squish">Parameter to adjust weird spaces when using MeasureString</param>
        /// <returns></returns>
        public static float getStringWidth(Graphics graphics, string text, Font font, bool squish = false)
        {
            // Just get all widths and sum them all ^^
            return (getCharacterWidths(graphics, text, font, squish).ToArray().Sum());
        }

        /// <summary>
        /// Get height for string draw on a Graphics object using a Font.
        /// </summary>
        /// <param name="graphics">Graphics where text will be drawn</param>
        /// <param name="text">Text to be drawn</param>
        /// <param name="font">Font used to draw text</param>
        /// <returns></returns>
        public static float getStringHeight(Graphics graphics, string text, Font font)
        {
            // Use MeasureString to get a Size object with info for that text/font, and return its height
            return graphics.MeasureString(text, font).Height;
        }

        /// <summary>
        /// Crop and resize a bitmap.
        /// Get an image and destination rect, and return a bitmap of that image resized to that rect.
        /// Important: I use square values because card portraits are originally 512x512 (square pictures).
        /// </summary>
        /// <param name="originalImage">Image to be resized</param>
        /// <param name="destinationRect">Where to put the image (size)</param>
        /// <returns></returns>
        public static Bitmap resizeImage(Image originalImage, Rectangle destinationRect)
        {
            // Init vars
            var x    = 0;
            var y    = 0;
            var wide = 0;
            // Check sizes. If destination dimensions and original dimensions are not equivalent, we must crop original picture.
            if (originalImage.Width > originalImage.Height)
            {
                // In case width > height, center image horizontally
                wide = originalImage.Height;
                x    = (wide - originalImage.Width) / 2;
                y    = 0;
            }
            else if (originalImage.Width < originalImage.Height)
            {
                // In case height > width, center the image vertically
                wide = originalImage.Width;
                x = 0;
                y = (wide - originalImage.Height) / 2;
            } else
            {
                // Square? do nothing
                wide = originalImage.Width;
            }
            // Crop picture over visible original image
            using (var fixedImage = new Bitmap(wide, wide))
            {
                // Crop original
                using (var graphics = Graphics.FromImage(fixedImage))
                {
                    graphics.PageUnit = GraphicsUnit.Pixel;
                    graphics.DrawImage(originalImage, x, y);
                }
                // Create result bitmap
                var result = new Bitmap(destinationRect.Width, destinationRect.Height);
                // And paint it where it belongs
                using (var gr = Graphics.FromImage(result))
                {
                    gr.PageUnit = GraphicsUnit.Pixel;
                    gr.DrawImage(fixedImage, destinationRect, new Rectangle(0, 0, wide, wide), GraphicsUnit.Pixel);
                }
                // Finally, return resulting bitmap
                return result;
            }
        }
    }
}
