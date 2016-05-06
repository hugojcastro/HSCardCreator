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

using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

using HSCardGenerator.framework.Constants.Common;

namespace HSCardGenerator.framework.Types.Graphics
{
    /// <summary>
    /// Custom combobox to draw flags as items. It can show flag names too, if necessary.
    /// How does it work? We have several flag pictures as resurces, stored with the right path and using a common pattern: locale in the format xxYY (i.e. esES, enUS).
    /// Each item of the combobox will have as value/text the locale of the flag. The control will get the right picture from resource, paint it, and show the locale name if desired.
    /// </summary>
    public class FlagComboBox : ComboBox
    {
        /// <summary>
        /// Definitions for properties
        /// Only one: show or do not show labels for flags.
        /// </summary>
        private bool showLabels;

        /// <summary>
        /// Constructor for custom control.
        /// Creates the combobox with default parameters, based in the ones pased as arguments.
        /// </summary>
        /// <param name="visible">Is it visible after creation? Default: false</param>
        /// <param name="enabled">Is it enabled after creation? Default: false</param>
        /// <param name="_labels">Does it shows labels for flags? Default: true</param>
        public FlagComboBox(bool visible = false, bool enabled = false, bool _labels = true)
        {
            // Assign parameters as corresponding values
            showLabels     = _labels;
            Enabled        = enabled;
            Visible        = visible;
            // Set default values for some style properties
            DrawMode       = DrawMode.OwnerDrawVariable;
            FlatStyle      = FlatStyle.Flat;
            ItemHeight     = 24;
            DoubleBuffered = true;
            DropDownStyle  = ComboBoxStyle.DropDownList;
        }

        /// <summary>
        /// Custom Paint Event Handler.
        /// We override the default handler to paint flags from resources and hide (or not) text labels.
        /// This event fires when painting the "text" area, not the dropdown list.
        /// </summary>
        /// <param name="e">Arguments from Paint Event</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            // Do the base painting (mainly for selected item frame, etc.)
            base.OnPaint(e);

            // Get text value and resource path from it
            var text     = Text;
            var resource = string.Format("HSCardGenerator.Resources.Images.Flags.{0}.png", text.ToLower());
            // Read resource as stream
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource))
            {
                // To use the stream as picture to paint it in the control
                e.Graphics.DrawImage(Image.FromStream(stream), new PointF(Bounds.X, Bounds.Y));
            }
            // Draw text if necessary
            if (showLabels)
            {
                // Define vars to show it in the control
                var txt    = Config.localesFull[(int)Methods.General.getLocaleFromString(text)];
                var limits = e.Graphics.MeasureString(txt, Font);
                var top    = (24 - limits.Height) / 2;
                // And do it! (Shia will be proud)
                e.Graphics.DrawString(txt, Font, new SolidBrush(ForeColor), Bounds.Left + 24, Bounds.Top + top);
            }
        }

        /// <summary>
        /// Custom Draw Item Handler.
        /// It fires when painting each item in dropdown list. We use item value (text) to create the path to picture in resource and paint it in the list.
        /// </summary>
        /// <param name="e">Arguments from DrawItem Event</param>
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            // Draw background and focus rectangle, if necessary.
            e.DrawBackground();
            e.DrawFocusRectangle();
            // If something is selected, paint it
            if (e.Index > -1)
            {
                // Get text from current item
                var text     = (string)Items[e.Index];
                // Get resource path to picture
                var resource = string.Format("HSCardGenerator.Resources.Images.Flags.{0}.png", text.ToLower());
                // Read it as stream
                using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource))
                {
                    // And paint it
                    e.Graphics.DrawImage(Image.FromStream(stream), new PointF(e.Bounds.X, e.Bounds.Y));
                }
                // Draw text if necessary
                if (showLabels)
                {
                    // Same as for OnPaint event
                    var txt    = Config.localesFull[(int)Methods.General.getLocaleFromString(text)];
                    var limits = e.Graphics.MeasureString(txt, e.Font);
                    var top    = (24 - limits.Height) / 2;
                    e.Graphics.DrawString(txt, e.Font, new SolidBrush(e.ForeColor), e.Bounds.Left + 24, e.Bounds.Top + top);
                }
                // We need to do this, no wonder why :/
                if ((e.Index == SelectedIndex) && (DroppedDown == false))
                {
                    e.DrawFocusRectangle();
                }
            }
        }
    }
}
