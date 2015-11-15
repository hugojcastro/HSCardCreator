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
using System.Globalization;
using System.Resources;
using System.Windows.Forms;

using HSCardGenerator.framework;
using HSCardGenerator.framework.Constants.Common;
using HSCardGenerator.framework.Methods;
using HSCardGenerator.framework.Types.Graphics;

using TheArtOfDev.HtmlRenderer.WinForms;
using System;

namespace HSCardGenerator
{
    /// <summary>
    /// The main form for principal window
    /// It also loads fonts to be used in buttons and creates a flag combobox to select locale to use.
    /// Locale used for UI is store in Application Settings.
    /// </summary>
    public partial class mainForm : Form
    {
        /// <summary>
        /// Use custom cursors to drag/move the window and enhace clicking experience :D
        /// </summary>
        private Cursor cursorPointer     = new Cursor(Properties.Resources.cursorPointer.GetHicon());
        private Cursor cursorPointerDown = new Cursor(Properties.Resources.cursorPointerDown.GetHicon());
        private Cursor cursorGrabOn      = new Cursor(Properties.Resources.cursorGrab.GetHicon());
        private Cursor cursorGrabOff     = new Cursor(Properties.Resources.cursorHand.GetHicon());
        /// <summary>
        /// Vars for UI language
        /// </summary>
        private Locales language      = Locales.enUS; // Use this as default, but we take it from Settings var.
        private FlagComboBox cbLocale = new FlagComboBox(true, true, false);
        /// <summary>
        /// To work with localized resource strings
        /// </summary>
        private ResourceManager res;
        private CultureInfo ci;

        /// <summary>
        /// Do this trick to reduce flickering and graphic glitches
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }

        /// <summary>
        /// Main form: Do all magic here :P
        /// </summary>
        public mainForm()
        {
            // Initialize Windows Forms components
            InitializeComponent();
            // Stop painting
            SuspendLayout();
            // Get language from resources
            ci  = CultureInfo.CreateSpecificCulture(General.getCultureLocale(language));
            res = new ResourceManager("HSCardGenerator.Resources.Texts.Strings", typeof(CustomForm).Assembly);
            // Assign strings to cursors
            btCreateCollection.Text = res.GetString("strCreateCollection", ci);
            btCreateCustom.Text     = res.GetString("strCreateCustom", ci);
            // Load fonts from resource into static class
            Fonts.loadFonts();
            // Add them to HTML Renderer (to render texts for card descriptions)
            HtmlRender.AddFontFamily(Fonts.BelweFamily);
            HtmlRender.AddFontFamily(Fonts.FranklinGothicFamily);
            HtmlRender.AddFontFamily(Fonts.EasternCharsFamily);
            // Assign pointer cursor to buttons and manage events to do their animations :3
            Font font    = new Font(Fonts.BelweFamily, 9f, FontStyle.Regular);
            Button[] btn = new[] { btExit, btCreateCustom, btCreateCollection };
            foreach (var button in btn)
            {
                button.UseCompatibleTextRendering = true;
                button.Font   = font;
                button.Cursor = cursorPointer;

                button.MouseDown += (s, e) => { if (e.Button == MouseButtons.Left) { button.Cursor = cursorPointerDown; } };
                button.MouseUp   += (s, e) => { if (e.Button == MouseButtons.Left) { button.Cursor = cursorPointer; } };
            }
            // Assign hand cursor to main panel and add event handlers to make it drag main form for moving ^^
            mainPanel.Cursor = cursorGrabOff;
            mainPanel.MouseDown += (s, e) => 
            {
                if (e.Button == MouseButtons.Left)
                {
                    Cursor = cursorGrabOn;
                    SafeNativeMethods.ReleaseCapture();
                    SafeNativeMethods.SendMessage(Handle, SafeNativeMethods.WM_NCLBUTTONDOWN, (IntPtr)SafeNativeMethods.HT_CAPTION, IntPtr.Zero);
                }
            };
            // Assign event handler to exit button (yep, I know it's easier using IDE, but code won't be reused. So... :)
            btExit.Click += (s, e) => 
            {
                Close();
            };
            // Assign event handler to "create custom card" button (same as above)
            btCreateCustom.Click += (s, e) => 
            {
                // Show Modal CustomForm Window
                using (var custom = new CustomForm(language))
                {
                    custom.ShowDialog();
                }
            };
            // Finally, assign event to "batch create collection" button
            btCreateCollection.Click += (s, e) =>
            {
                // Show Modal BatchForm Window
                using (var batch = new BatchForm(language))
                {
                    batch.ShowDialog();
                }
            };
            // Create custom combobox with flags for locales
            cbLocale.Top    = 4;
            cbLocale.Left   = 520;
            cbLocale.Size   = new Size(50, 30);
            cbLocale.Parent = mainPanel;
            cbLocale.Cursor = cursorPointer;
            // Populate combobox
            foreach (var item in Config.locales)
            {
                cbLocale.Items.Add(item);
            }
            // Assign event handlers: To enhace click experience xD
            cbLocale.MouseDown += (s, e) => { if (e.Button == MouseButtons.Left) { cbLocale.Cursor = cursorPointerDown; } };
            cbLocale.MouseUp   += (s, e) => { if (e.Button == MouseButtons.Left) { cbLocale.Cursor = cursorPointer; } };
            // When selecting a new locale...
            cbLocale.SelectedIndexChanged += (s, e) =>
            {
                // Get it from resources
                language = General.getLocaleFromString((string) cbLocale.Items[cbLocale.SelectedIndex]);
                ci       = CultureInfo.CreateSpecificCulture(General.getCultureLocale(language));
                res      = new ResourceManager("HSCardGenerator.Resources.Texts.Strings", typeof(CustomForm).Assembly);
                // Assign new texts to buttons
                btCreateCollection.Text = res.GetString("strCreateCollection", ci);
                btCreateCustom.Text     = res.GetString("strCreateCustom", ci);
                // Store locale into Settings
                Properties.Settings.Default.Locale = cbLocale.SelectedIndex;
                Properties.Settings.Default.Save();
            };
            // And assign previously stored locale to combobox, to refresh UI
            cbLocale.SelectedIndex = (int)Properties.Settings.Default.Locale;
            // Continue painting
            ResumeLayout();
        }
    }
}
