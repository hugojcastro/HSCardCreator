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
using System.Drawing.Imaging;
using System.Globalization;
using System.Resources;
using System.Windows.Forms;

using HSCardGenerator.framework;
using HSCardGenerator.framework.Constants;
using HSCardGenerator.framework.Constants.Common;
using HSCardGenerator.framework.Methods;
using HSCardGenerator.framework.Types.Graphics;

namespace HSCardGenerator
{
    /// <summary>
    /// Form for Window used to create custom cards
    /// It takes as parameter a Locale for the texts of controls and comboboxes.
    /// User can choose diferent values for card (type, rarity, set...) and give his/her own values (name, description, cost...)
    /// For card description, user can use HTML tags. Hearthstone (tm) uses only <b></b> and <i></i> for bold and italic effects.
    /// User can give a picture to use as card portrait, use a default one (it's fancy!) or leave the portrait empty (so sad :_O )
    /// </summary>
    public partial class CustomForm : Form
    {
        /// <summary>
        /// Custom cursors to enhace window movement and clicking experience ;)
        /// </summary>
        private Cursor cursorPointer     = new Cursor(Properties.Resources.cursorPointer.GetHicon());
        private Cursor cursorPointerDown = new Cursor(Properties.Resources.cursorPointerDown.GetHicon());
        private Cursor cursorGrabOn      = new Cursor(Properties.Resources.cursorGrab.GetHicon());
        private Cursor cursorGrabOff     = new Cursor(Properties.Resources.cursorHand.GetHicon());
        /// <summary>
        /// Locale used for UI and card texts (i.e. minion race)
        /// </summary>
        private Locales language         = Locales.enUS;
        private string filename          = "";
        /// <summary>
        /// Used to reference localized text resources
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
        /// Custom handler to move windows by dragging with cursor from anywhere on it :P
        /// It uses native methods (Windows), so to port tool to other OS it is necessary to use another trick. In example, the offset x-y method.
        /// </summary>
        /// <param name="sender">Object that was clicked</param>
        /// <param name="e">Argument from Mouse Event</param>
        private void moveFormOnMouseDown(object sender, MouseEventArgs e)
        {
            // If button clicked is the left one, do our magic
            if (e.Button == MouseButtons.Left)
            {
                // Change cursor for a hand grabbing
                Cursor = cursorGrabOn;
                // Call native methods to release mouse and do a 'natural' drag effect  (that's all! simple ^^)
                SafeNativeMethods.ReleaseCapture();
                SafeNativeMethods.SendMessage(Handle, SafeNativeMethods.WM_NCLBUTTONDOWN, (IntPtr)SafeNativeMethods.HT_CAPTION, IntPtr.Zero);
            }
        }

        /// <summary>
        /// Constructor for window, based on locale from main window.
        /// It takes the language given as argument to retrieve and assing localized texts from resources.
        /// Assign cursors to UI controls and diferent handlers for events on controls, mainly to move window and do a nice 'click' mouse effect.
        /// </summary>
        /// <param name="_lang">Locale var. with language to be used in UI</param>
        public CustomForm(Locales _lang = Locales.enUS)
        {
            // As any winforms form... ^^
            InitializeComponent();
            // Initialize languages
            language = _lang;
            ci  = CultureInfo.CreateSpecificCulture(General.getCultureLocale(_lang));
            res = new ResourceManager("HSCardGenerator.Resources.Texts.Strings", typeof(CustomForm).Assembly);
            // Stop painting
            SuspendLayout();
            // Assign localized texts to labels and buttons
            lblClass.Text          = res.GetString("strClass", ci);
            lblType.Text           = res.GetString("strType", ci);
            lblSet.Text            = res.GetString("strSet", ci);
            lblQuality.Text        = res.GetString("strQuality", ci);
            lblRace.Text           = res.GetString("strRace", ci);
            lblName.Text           = res.GetString("strName", ci);
            lblDescription.Text    = res.GetString("strText", ci);
            lblCost.Text           = res.GetString("strCost", ci);
            lblAttack.Text         = res.GetString("strAttack", ci);
            lblHealth.Text         = res.GetString("strHealth", ci);
            lblDefaultPicture.Text = res.GetString("strDefaultPicture", ci);
            btImage.Text           = res.GetString("strSelectImage", ci);
            btUpdate.Text          = res.GetString("strUpdateCard", ci);
            btSave.Text            = res.GetString("strSaveCard", ci);
            // Populate card type combobox with localized values
            foreach (var item in Config.cardTypes)
            {
                cbType.Items.Add(res.GetString(item, ci));
            }
            // Populate card quality combobox with localized values
            foreach (var item in Config.cardQualities)
            {
                cbQuality.Items.Add(res.GetString(item, ci));
            }
            // Populate card set combobox with localized values  d
            foreach (var item in Config.cardSets)
            {
                cbSet.Items.Add(res.GetString(item, ci));
            }
            // Populate minion race combobox with localized values
            foreach (var item in Config.cardRaces)
            {
                cbRace.Items.Add(res.GetString(item, ci));
            }
            // No race by default
            cbRace.SelectedIndex = -1;
            // Populate hero classes combobox with localized values
            foreach (var item in Config.cardClasses)
            {
                cbClass.Items.Add(res.GetString(item, ci));
            }
            // No class by default
            cbClass.SelectedIndex = -1;
            // Adjust canvas size (just in case)
            canvas.Width  = Config.cardWidth;
            canvas.Height = Config.cardHeight;
            // Assign fonts to controls:
            // Create font
            Font font = new Font(Fonts.BelweFamily, 9f, FontStyle.Regular);
            // Assign font to text labels and create handler to move window by dragging
            Label[] lbl = new[] { lblCost, lblAttack, lblHealth, lblClass, lblType, lblQuality, lblRace, lblName, lblSet, lblDescription, lblFilename, lblDefaultPicture };
            foreach (var label in lbl)
            {
                label.UseCompatibleTextRendering = true;
                label.Font = font;
                label.MouseDown += moveFormOnMouseDown;
            }
            // Assign font to Button labels and pointer cursor to buttons
            Button[] btn = new[] { btUpdate, btSave, btImage, btBackCustom, btClearFilename };
            foreach (var button in btn)
            {
                button.UseCompatibleTextRendering = true;
                button.Font   = font;
                button.Cursor = cursorPointer;

                button.MouseDown += (s, e) => { if (e.Button == MouseButtons.Left) { button.Cursor = cursorPointerDown; } };
                button.MouseUp   += (s, e) => { if (e.Button == MouseButtons.Left) { button.Cursor = cursorPointer; } };
            }
            // Assign cursor to checkbox and handlers to do a fancy effect
            cbDefaultPicture.Cursor = cursorPointer;
            cbDefaultPicture.MouseDown += (s, e) => { if (e.Button == MouseButtons.Left) { cbDefaultPicture.Cursor = cursorPointerDown; } };
            cbDefaultPicture.MouseUp   += (s, e) => { if (e.Button == MouseButtons.Left) { cbDefaultPicture.Cursor = cursorPointer; } };
            // Assign pointer cursor to comboboxes and event handlers to animate cursor, check their value and show according color
            ComboBox[] cbx = new[] { cbClass, cbQuality, cbRace, cbSet, cbType };
            foreach (var combobox in cbx)
            {
                combobox.Cursor = cursorPointer;

                combobox.MouseDown += (s, e) => { if (e.Button == MouseButtons.Left) { combobox.Cursor = cursorPointerDown; } };
                combobox.MouseUp   += (s, e) => { if (e.Button == MouseButtons.Left) { combobox.Cursor = cursorPointer; } };

                combobox.SelectedValueChanged += (s, e) =>
                {
                    // Take changed combo
                    var cb = s as ComboBox;
                    // Assign background color based on selected index
                    cb.BackColor = (cb.SelectedIndex < 0) ? Color.Red : Color.Green;
                    // Check if card type changed
                    if (cb == cbType)
                    {
                        var showRace  = false;
                        var showStats = true;
                        // Based on new index...
                        switch ((CardType)cb.SelectedIndex)
                        {
                            case CardType.Minion:
                                lblHealth.Text = string.Format("{0}:", res.GetString("strHealth", ci));
                                lblAttack.Text = string.Format("{0}:", res.GetString("strAttack", ci));
                                showRace = true;
                                break;
                            case CardType.Spell:
                                showStats = false;
                                break;
                            case CardType.Weapon:
                                lblHealth.Text = string.Format("{0}:", res.GetString("strDurability", ci));
                                lblAttack.Text = string.Format("{0}:", res.GetString("strDamage", ci));
                                break;
                        }
                        // Show/hide health/durability and attack/damage
                        lblHealth.Visible = showStats;
                        txtHealth.Visible = showStats;
                        lblAttack.Visible = showStats;
                        txtAttack.Visible = showStats;
                        // Show/hide race combo
                        lblRace.Visible = showRace;
                        cbRace.Visible = showRace;
                    }
                };
            }
            // Assign hand cursor to panels
            Panel[] pnl = new[] { customPanel, customSubPanel };
            foreach (var panel in pnl)
            {
                panel.Cursor = cursorGrabOff;
                panel.MouseDown += moveFormOnMouseDown;
            }
            // Textboxes will only accept numbers
            TextBox[] txtbx = new[] { txtCost, txtAttack, txtHealth };
            foreach (var textbox in txtbx)
            {
                // After a keypress: if invalid char, don't handle it
                textbox.KeyPress += (s, e) => 
                {
                    e.Handled = !(char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar) || char.IsPunctuation(e.KeyChar));
                };
            }
            // Check if values are fine on each textbox
            TextBox[] txtbxs = new[] { txtCost, txtAttack, txtHealth, txtName };
            foreach (var textbox in txtbxs)
            {
                // Text boxes can't have empty texts. Show color according to status
                textbox.TextChanged += (s, e) =>
                {
                    var txt = (TextBox)s;
                    var clr = (txt.Text.Length == 0) ? Color.Red : Color.Green;

                    txt.BackColor = clr;
                };
            }
            // Add handler to manage event to move window on dragging for canvas control
            canvas.MouseDown += moveFormOnMouseDown;
            // Add handler to manage event for save card button
            btSave.Click += (s, e) => 
            {
                // Ask for destination image file. If all fine, save it
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    canvas.Image.Save(saveFileDialog.FileName, ImageFormat.Png);
                }
            };
            // Add handler to manage event for update card button: Create and show card with given values from window controls
            btUpdate.Click += (s, e) =>
            {
                // Get combobox values as right types
                var _type    = (CardType)cbType.SelectedIndex;
                var _class   = (CardClass)cbClass.SelectedIndex;
                var _set     = (CardSet)cbSet.SelectedIndex;
                var _race    = (CardRace)cbRace.SelectedIndex;
                var _quality = (CardQuality)cbQuality.SelectedIndex;
                var _name    = txtName.Text;
                var _cost    = Convert.ToInt32(txtCost.Text);
                var _attack  = Convert.ToInt32(txtAttack.Text);
                var _health  = Convert.ToInt32(txtHealth.Text);

                // Do checks on ComboBoxes values
                var comboCheck = General.checkCard(_type, _class, _set, _race, _quality, _name);
                switch (comboCheck)
                {
                    case CardError.BadType: // Wrong card type?
                        MessageBox.Show(res.GetString("errorWrongCardType", ci));
                        cbType.BackColor = Color.Red;
                        cbType.Focus();
                        break;
                    case CardError.BadClass: // Wrong card class?
                        MessageBox.Show(res.GetString("errorWrongHeroClass", ci));
                        cbClass.BackColor = Color.Red;
                        cbClass.Focus();
                        break;
                    case CardError.BadSet: // Wrong card set?
                        MessageBox.Show(res.GetString("errorWrongCardSet", ci));
                        cbSet.BackColor = Color.Red;
                        cbSet.Focus();
                        break;
                    case CardError.BadQuality: // Wrong card quality?
                        MessageBox.Show(res.GetString("errorWrongCardQuality", ci));
                        cbQuality.BackColor = Color.Red;
                        cbQuality.Focus();
                        break;
                    case CardError.BadRace: // Wrong minion race?
                        MessageBox.Show(res.GetString("errorWrongMinionRace", ci));
                        cbRace.BackColor = Color.Red;
                        cbRace.Focus();
                        break;
                    case CardError.BadName: // Bad (empty) name?
                        MessageBox.Show(res.GetString("errorWrongCardName", ci));
                        txtName.BackColor = Color.Red;
                        txtName.Focus();
                        break;
                    case CardError.None: // All fine ^^ Create card based on specified data
                        // Get race name as localized string
                        var race = (_race == CardRace.None) ? "" : res.GetString(Config.cardRaces[(byte)_race], ci);
                        // Create card from data
                        var card = new CardItem("TEST",           // Id for card (useless here)
                            _cost, _attack, _health, _health,     // numeric values (durability is health here)
                            _type, _class, _race, _set, _quality, // enum values from comboboxes
                            filename, cbDefaultPicture.Checked    // portrait or behaviour if not given
                        );
                        // Assign text to card
                        card.addLocale(language, _name, txtDescription.Text, race);
                        // Show card :)
                        canvas.Image = card.Picture();
                        // Show button to save card
                        btSave.Enabled = true;
                        btSave.Visible = true;
                        break;
                }
            };
            // Add handler to manage event to select a portrait for card
            btImage.Click += (s, e) =>
            {
                // Ask for image file
                openFileDialog.DefaultExt = Config.extensionImages;
                openFileDialog.Filter     = Config.filterImages;
                // If all fine, open it
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Get name
                    filename         = openFileDialog.FileName;
                    lblFilename.Text = filename;
                    // Show it in label and show button to erase portrait
                    lblFilename.Visible       = true;
                    btClearFilename.Visible   = true;
                    // Hide option to choose a default image or not, because we have one now :)
                    lblDefaultPicture.Visible = false;
                    cbDefaultPicture.Visible  = false;
                }
            };
            // Add handler to manage event to clear portrait filename and hide controls button
            btClearFilename.Click += (s, e) =>
            {
                // Delete all strings
                filename         = "";
                lblFilename.Text = "";
                // Hide controls and give option to choose default image or not
                lblFilename.Visible       = false;
                btClearFilename.Visible   = false;
                lblDefaultPicture.Visible = true;
                cbDefaultPicture.Visible  = true;
            };
            #region Temp: default values to check stuff
            // For comboboxes
            cbClass.SelectedIndex   = 5; // Neutral
            cbType.SelectedIndex    = 0; // Minion
            cbQuality.SelectedIndex = 1; // Common
            cbRace.SelectedIndex    = 4; // Dragon
            cbSet.SelectedIndex     = 0; // None
            // For textboxes
            txtAttack.Text      = "4";
            txtCost.Text        = "4";
            txtHealth.Text      = "4";
            txtName.Text        = "Hechicero dragonante";
            txtDescription.Text = "Obtiene +1/+1 cada vez que este esbirro es el objetivo de alguno de tus hechizos.";
            #endregion
            // Continue painting
            ResumeLayout();
        }
    }
}
