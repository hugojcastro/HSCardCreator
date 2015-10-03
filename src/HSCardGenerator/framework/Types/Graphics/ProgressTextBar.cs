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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Windows.Forms;

namespace HSCardGenerator.framework.Types.Graphics
{
    /// <summary>
    /// Custom progress bar to show text in the middle with selected font.
    /// Show a progress bar but with text in the middle. Text will be drawn using inverted fore/background colors.
    /// If there is a text value, progress bar will show: _Text_ (_X_%); Otherwise, only _X_%
    /// </summary>
    class ProgressTextBar : ProgressBar
    {
        //Property to hold the custom text and its font
        public string text { get; set; }
        public Font font   { get; set; }

        /// <summary>
        /// Constructor for custom progress bar.
        /// It puts default values as we need, so I avoid use of code in main window :D
        /// </summary>
        public ProgressTextBar()
        {
            // Default custom values: no text and simple font.
            text  = "";
            font  = new Font("Tahoma", 8);
            // Default general values for bar
            Maximum   = 100;
            Minimum   = 0;
            Step      = 10;
            // Default values for style and aesthetics
            Style     = ProgressBarStyle.Blocks;
            ForeColor = Color.SaddleBrown;
            BackColor = Color.AntiqueWhite;
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        }

        /// <summary>
        /// Custom OnPaint event.
        /// Just override the default method to draw text and do our stuff.
        /// </summary>
        /// <param name="e">Arguments from Paint Event</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            // Take area of progressbar
            var rect = ClientRectangle;
            // Create a bitmap to draw all elements of progress bar and, finally, paint in in main control
            using (var bmp = new Bitmap(rect.Width, rect.Height, PixelFormat.Format32bppPArgb))
            {
                // Take bitmap graphics to paint stuff
                using (var bg = System.Drawing.Graphics.FromImage(bmp))
                {
                    // set atialiasing
                    bg.SmoothingMode     = SmoothingMode.HighQuality;            // AntiAlias
                    bg.InterpolationMode = InterpolationMode.HighQualityBicubic; // High
                    bg.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                    bg.PixelOffsetMode   = PixelOffsetMode.HighQuality;
                    // Fill it with Background Color
                    bg.FillRectangle(new SolidBrush(BackColor), rect);
                    // If we have a value to show...
                    if (Value > 0)
                    {
                        // Decrease rectangle to paint progress bar
                        rect.Inflate(-2, -2);
                        // Get progress bar area
                        var clip = new Rectangle(rect.X, rect.Y, (int)Math.Round(((float)Value / Maximum) * rect.Width), rect.Height);
                        // And paint it
                        bg.FillRectangle(new SolidBrush(ForeColor), clip);
                    }
                    // Get text: given text + percent or simply % by default
                    var txt  = (text.Length == 0) ? string.Format("{0}%", Value) : string.Format("{0} ({1}%)", text, Value);
                    var size = bg.MeasureString(txt, font);
                    var pos  = new PointF((Width - size.Width) / 2, (Height - size.Height) / 2);
                    // Paint it inverted over the bar: Get the progress bar
                    rect       = ClientRectangle;
                    rect.Inflate(-2, -2);
                    // Progress bar width as percent of progress
                    rect.Width = (int)Math.Round(((float)Value / Maximum) * rect.Width);
                    // Create regions: region on the left is the progress bar region
                    using (var leftRegion = new Region(rect))
                    {
                        // And region in the right is the main bar, as exclusion of whole bar minus progress bar
                        using (var rightRegion = new Region(ClientRectangle))
                        {
                            rightRegion.Exclude(leftRegion);
                            // Use progress bar (region on the left) as clip
                            bg.Clip = leftRegion;
                            // And draw text over it
                            bg.DrawString(txt, font, new SolidBrush(BackColor), pos);
                            // Now, the main bar part (region on the right): use it as clip
                            bg.Clip = rightRegion;
                            // And draw text in same way but inversed color
                            bg.DrawString(txt, font, new SolidBrush(ForeColor), pos);
                            // Finally, paint bar in main control :D
                            e.Graphics.DrawImage(bmp, new Point(0,0));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Override Dispose method to free font resource
        /// </summary>
        /// <param name="disposing">Parameter for base Dispose</param>
        protected override void Dispose(bool disposing)
        {
            font.Dispose();
            base.Dispose(disposing);
        }
    }
}
