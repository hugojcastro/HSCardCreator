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
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;

using HSCardGenerator.framework;
using HSCardGenerator.framework.Constants;
using HSCardGenerator.framework.Constants.Common;
using HSCardGenerator.framework.Methods;
using HSCardGenerator.framework.Types.Graphics;
using HSCardGenerator.framework.Types.JSON;

using Newtonsoft.Json;

namespace HSCardGenerator
{
    /// <summary>
    /// Form for batch process window
    /// It reads card portrait files from folder, card data from JSON files and creates all the cards in the locales included in JSON files.
    /// User can preview loaded cards in their locales and choose size for output pictures.
    /// If no portrait is found, user can choose what to do: do not generate card, use a default picture or leave empty the portrait hole.
    /// </summary>
    public partial class BatchForm : Form
    {
        /// <summary>
        /// Custom cursors to enhace UI by moving window by dragging or changing cursor when clicking
        /// </summary>
        private Cursor cursorPointer     = new Cursor(Properties.Resources.cursorPointer.GetHicon());
        private Cursor cursorPointerDown = new Cursor(Properties.Resources.cursorPointerDown.GetHicon());
        private Cursor cursorGrabOn      = new Cursor(Properties.Resources.cursorGrab.GetHicon());
        private Cursor cursorGrabOff     = new Cursor(Properties.Resources.cursorHand.GetHicon());
        /// <summary>
        /// Data used in the batch process
        /// </summary>
        // Collection of cards and locales
        private CardItemCollection loadedCollection = new CardItemCollection();
        // Source folder with portrait pictures
        private string sourcePath      = "";
        // Target folder to store generated card pictures
        private string destinationPath = "";
        // Source file of JSON card data
        private string jsonFilename    = "";
        // Internal var to avoid weird behaviours when working with comboboxes
        private bool feedingComboBoxes = false; // Used to avoid card previews when feeding comboboxes
        /// <summary>
        /// Vars to manage localized resource strings
        /// </summary>
        private ResourceManager res;
        private CultureInfo ci;
        /// <summary>
        /// Custom controls: A combobox for flags and a progressbar with text
        /// </summary>
        private FlagComboBox cbLocale       = new FlagComboBox();
        private ProgressTextBar progressBar = new ProgressTextBar();

        /// <summary>
        /// We use this to avoid flickering and other UI glitches
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
        /// Show a picture in the given PictureBox acording to given status.
        /// Pictures are used to show the status of some process/action:
        /// An alert icon to show something is still pending
        /// A check mark to show all is ok
        /// A cross mark to show something gone wrong
        /// </summary>
        /// <param name="_destination">PictureBox to assign status picture</param>
        /// <param name="_status">Status to reflect in PictureBox</param>
        private void showStatusPicture(PictureBox _destination, byte _status)
        {
            // Choose resource path according to status var.
            var image = ((_status == 0) ? "imgAlert" : ((_status == 1) ? "StatusOk" : "StatusNot"));
            // Get image from resources
            var picture = string.Format("HSCardGenerator.Resources.Images.Checks.{0}.png", image);
            // Assign it to picture
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(picture))
            {
                _destination.Image = Image.FromStream(stream);
            }
            // Finally, show the picture
            _destination.Visible = true;
        }

        /// <summary>
        /// Set visibility and access for UI controls, based on actual step on batch conversion
        /// Each step in the batch conversion process has specific controls enabled or disabled, visible or hidden.
        /// According to step, just prepare UI.
        /// </summary>
        /// <param name="status">Step in the process where we are</param>
        private void setBatchUIStatus(BatchUIStatus status)
        {
            // Update UI controls according to given status of the creation process
            switch (status)
            {
                // First step: just ask for picture folder (folder where are all portrait pictures)
                // Hide all controls except button to select image folder
                case BatchUIStatus.AskForPictureFolder:
                    // Status for buttons
                    Button[] bt = new[] { btBatchProcess, btBatchCancel, btOpenJSON, btDestinationFolder };
                    foreach (var button in bt)
                    {
                        button.Enabled = true;  // Enable all previously disabled buttons
                        button.Visible = false; // Hide all buttons
                    }
                    btImageFolder.Enabled = true; // Enable Image Folder Selection button
                    btImageFolder.Visible = true; // Show Image Folder Selection button
                    // Status for combobox
                    cbCollection.Visible = false; // Hide combobox just in case
                    cbLocale.Visible     = false; // Same with this one
                    loadedCollection.Clear();     // Clear collection content
                    // Picture status
                    PictureBox[] pb = new[] { pbJSON, pbDestination };
                    foreach (var picturebox in pb)
                    {
                        picturebox.Visible = false; // Hide all Picture Status
                    }
                    pbPreview.Image = null;
                    // Hide previous labels
                    Label[] lbl = new[] { lblImageFolder, lblJSONFile, lblDestinationFolder };
                    foreach (var label in lbl)
                    {
                        label.Visible = false;
                        label.Text    = "";
                    }
                    // Show alert image
                    showStatusPicture(pbImage, 0);
                    // Text console
                    txtConsole.Text = "";
                    // Restore size panel status
                    sizePanel.Visible = false;
                    rbZoom100.Checked = true;
                    txtWidth.Text     = "150";
                    txtHeight.Text    = "215";
                    // No animation (just in case)
                    pbWaitingAnim.Visible = false;
                    progressBar.Visible   = false;
                    // No image combobox: default option
                    cbNoImage.SelectedIndex = 0;
                    // Hide no image combobox and label
                    cbNoImage.Visible       = false;
                    lblNoImage.Visible      = false;
                    // Hide console
                    txtConsole.Visible = false;
                    // Reset text var
                    sourcePath = "";
                    break;
                case BatchUIStatus.AskForJSONFile:
                    // Process buttons
                    btImageFolder.Enabled    = false; // Disable Image Folder Selection button
                    btOpenJSON.Visible       = true;  // Show Open JSON button
                    btOpenJSON.Enabled       = true;  // And enable it, in case of cancel a previous read
                    btBatchCancel.Visible    = true;  // Show Cancel process button
                    btBackCollection.Visible = true;  // in case of cancel previous read
                    // Reset text var
                    jsonFilename = "";
                    // Clear all combobox data
                    feedingComboBoxes    = true;
                    cbCollection.Visible = false;
                    cbLocale.Visible     = false;
                    cbCollection.Items.Clear();
                    cbLocale.Items.Clear();
                    feedingComboBoxes = false;
                    // Show no image combobox and label
                    cbNoImage.Visible  = true;
                    cbNoImage.Enabled  = true;
                    lblNoImage.Visible = true;
                    // Picture status
                    showStatusPicture(pbJSON, 0);
                    break;
                case BatchUIStatus.LoadingJSONFile:
                    // Status for buttons
                    btOpenJSON.Enabled       = false; // Disable Open JSON button
                    btBackCollection.Visible = false; // Can't go back
                    cbNoImage.Enabled        = false; // Can't change actions for not found pics
                    // Show console
                    txtConsole.Visible = true;
                    // Clear actual collection
                    loadedCollection.Clear();
                    // Show animations
                    pbWaitingAnim.Visible = true;
                    progressBar.Visible   = true;
                    break;
                case BatchUIStatus.AskForDestinationFolder:
                    // Show buttons
                    btBackCollection.Visible    = true; // Show button after reading
                    btDestinationFolder.Visible = true; // Show Destination Folder Selection button
                    // Reset text var
                    destinationPath = "";
                    // Picture status
                    showStatusPicture(pbDestination, 0);
                    break;
                case BatchUIStatus.WaitingForAction:
                    // Status for buttons
                    btDestinationFolder.Enabled = false; // Disable Destination Folder Selection button
                    btBatchProcess.Enabled      = true;  // Enable Batch Process button
                    btBatchProcess.Visible      = true;  // Show Batch Process button
                    // Show panel to choose output size
                    sizePanel.Visible = true;
                    break;
            }
        }

        /// <summary>
        /// Method to move form around by dragging
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void moveFormOnMouseDown(object sender, MouseEventArgs e)
        {
            // If left button pressed
            if (e.Button == MouseButtons.Left)
            {
                // Change cursor
                Cursor = cursorGrabOn;
                // And send system message to move window :D
                SafeNativeMethods.ReleaseCapture();
                SafeNativeMethods.SendMessage(Handle, SafeNativeMethods.WM_NCLBUTTONDOWN, (IntPtr)SafeNativeMethods.HT_CAPTION, IntPtr.Zero);
            }
        }

        /// <summary>
        /// Constructor for batch process window.
        /// </summary>
        /// <param name="_lang">Which locale will be used in control texts</param>
        public BatchForm(Locales _lang = Locales.enUS)
        {
            // Do WinForms magic
            InitializeComponent();
            // Initialize languages
            ci  = CultureInfo.CreateSpecificCulture(General.getCultureLocale(_lang));
            res = new ResourceManager("HSCardGenerator.Resources.Texts.Strings", typeof(CustomForm).Assembly);
            // Stop painting
            SuspendLayout();
            // Get main font
            var font = new Font(Fonts.BelweFamily, 9f, FontStyle.Regular);
            // Assign text/button labels
            btImageFolder.Text       = res.GetString("strImageFolder", ci);
            btOpenJSON.Text          = res.GetString("strOpenJSONFile", ci);
            btDestinationFolder.Text = res.GetString("strChooseDestination", ci);
            btBatchProcess.Text      = res.GetString("strCreateCards", ci);
            btBatchCancel.Text       = res.GetString("strCancelProcess", ci);
            lblOutputSize.Text       = res.GetString("strOutputSize", ci);
            lblNoImage.Text          = res.GetString("strNoImage", ci);
            rbCustom.Text            = res.GetString("strCustom", ci);
            // Populate options for no image combobox
            var txt = res.GetString("strNoImageOptions", ci).Split('-');
            cbNoImage.Items.Clear();
            foreach (var str in txt)
            {
                cbNoImage.Items.Add(str);
            }
            // Create custom combobox for locales with flags
            cbLocale.Size     = new Size(126, 30);
            cbLocale.Location = new Point(262, 226);
            cbLocale.Parent   = batchPanel;
            // Create custom progress bar to show text properly
            progressBar.Size     = new Size(220, 24);
            progressBar.Location = new Point(212, 196);
            progressBar.Parent   = batchPanel;
            progressBar.font     = font;
            // Assign font and pointer cursor to buttons
            Button[] btn = new[] { btOpenJSON, btImageFolder, btDestinationFolder, btBatchProcess, btBatchCancel, btBackCollection };
            foreach (var button in btn)
            {
                button.Font   = font;
                button.Cursor = cursorPointer;

                button.MouseDown += (s, e) => { if (e.Button == MouseButtons.Left) { button.Cursor = cursorPointerDown; } };
                button.MouseUp   += (s, e) => { if (e.Button == MouseButtons.Left) { button.Cursor = cursorPointer; } };
            }
            // Assign pointer cursor to comboboxes
            ComboBox[] cbx = new[] { cbCollection, cbLocale, cbNoImage };
            foreach (var combobox in cbx)
            {
                combobox.Cursor = cursorPointer;

                combobox.MouseDown += (s, e) => { if (e.Button == MouseButtons.Left) { combobox.Cursor = cursorPointerDown; } };
                combobox.MouseUp   += (s, e) => { if (e.Button == MouseButtons.Left) { combobox.Cursor = cursorPointer; } };
            }
            // Assign hand cursor to panels to move form
            Panel[] pnl = new[] { batchPanel, sizePanel };
            foreach (var panel in pnl)
            {
                panel.Cursor     = cursorGrabOff;
                panel.MouseDown += moveFormOnMouseDown;
            }
            // Assign cursor and movement to progressbar
            progressBar.Cursor = cursorGrabOff;
            progressBar.MouseDown += moveFormOnMouseDown;
            // Assign font and cursor to labels
            Label[] lbl = new[] { lblOutputSize, lblX, lblImageFolder, lblJSONFile, lblDestinationFolder, lblNoImage };
            foreach (var label in lbl)
            {
                label.Font       = font;
                label.TextAlign  = ContentAlignment.MiddleLeft;
                label.MouseDown += moveFormOnMouseDown;
            }
            // Textboxes will only accept numbers
            TextBox[] txtbx = new[] { txtHeight, txtWidth };
            foreach (var textbox in txtbx)
            {
                textbox.KeyPress += (s, e) =>
                {
                    e.Handled = !(char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar) || char.IsPunctuation(e.KeyChar));
                };
            }
            // Assign pointers for radiobuttons
            RadioButton[] rbt = new[] { rbZoom025, rbZoom050, rbZoom075, rbZoom100, rbZoom150, rbZoom200, rbCustom };
            foreach (var radiobutton in rbt)
            {
                // Set cursor and fonts
                radiobutton.Cursor = cursorPointer;
                radiobutton.Font   = font;
                // Set events to animate button on click
                radiobutton.MouseDown += (s, e) => { if (e.Button == MouseButtons.Left) { radiobutton.Cursor = cursorPointerDown; } };
                radiobutton.MouseUp   += (s, e) => { if (e.Button == MouseButtons.Left) { radiobutton.Cursor = cursorPointer; } };
                // Make them a radiogroup and do stuff when clicking
                radiobutton.CheckedChanged += (s, e) =>
                {
                    // Check if custom controls must be shown
                    var customVisible = (rbCustom.Checked);
                    // Assign visibility
                    txtWidth.Visible  = customVisible;
                    lblX.Visible      = customVisible;
                    txtHeight.Visible = customVisible;
                };
            }
            // Assign pointers for paintboxes to move form
            PictureBox[] pbx = new[] { pbImage, pbDestination, pbJSON, pbPreview, pbWaitingAnim };
            foreach (var picturebox in pbx)
            {
                picturebox.Cursor     = cursorGrabOff;
                picturebox.MouseDown += moveFormOnMouseDown;
            }
            // Set UI status: first step of process -> ask for a picture folder to take card images from.
            setBatchUIStatus(BatchUIStatus.AskForPictureFolder);
            // Assign events for comboboxes: first, collection
            cbCollection.SelectedIndexChanged += (s, e) =>
            {
                if ((cbCollection.SelectedItem != null) && !feedingComboBoxes)
                {
                    // Take selected card
                    var cardId = ((KeyValuePair<string, string>)cbCollection.SelectedItem).Key;
                    CardItem card;
                    // Show card thumbnail
                    if (loadedCollection.Cards.TryGetValue(cardId, out card))
                    {
                        pbPreview.Image = card.Picture(General.getLocaleFromString((string)cbLocale.SelectedItem), pbPreview.Width, pbPreview.Height);
                    }
                }
            };
            // Then, locales
            cbLocale.SelectedIndexChanged += (s, e) =>
            {
                if ((cbCollection.SelectedItem != null) && !feedingComboBoxes)
                {
                    // Take selected card
                    var cardId = ((KeyValuePair<string, string>)cbCollection.SelectedItem).Key;
                    CardItem card;
                    // Show card thumbnail
                    if (loadedCollection.Cards.TryGetValue(cardId, out card))
                    {
                        pbPreview.Image = card.Picture(General.getLocaleFromString((string)cbLocale.SelectedItem), pbPreview.Width, pbPreview.Height);
                    }
                }
            };
            // Handler to manage event for Image Folder Selection button
            btImageFolder.Click += (s, e) =>
            {
                // User can't create new folders (only open existing ones)
                folderBrowserDialog.ShowNewFolderButton = false;
                // Give user a dialog to choose a folder
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    // Store selected path
                    sourcePath = folderBrowserDialog.SelectedPath;
                    // Status for picture 
                    showStatusPicture(pbImage, 1);
                    // Show image folder info
                    lblImageFolder.Text    = sourcePath;
                    lblImageFolder.Visible = true;
                    // Go to next step
                    setBatchUIStatus(BatchUIStatus.AskForJSONFile);
                }
            };
            // Handler to manage event for Open JSON File button (output to "console")
            btOpenJSON.Click += (s, e) =>
            {
                // Check if worker working :D
                if (!bgJSONWorker.IsBusy)
                {
                    // Ask for JSON file
                    openFileDialog.DefaultExt = Config.extensionJSON;
                    openFileDialog.Filter     = Config.filterJSON;
                    // Open it and do magic if all is fine
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Store value
                        jsonFilename = openFileDialog.FileName;
                        // Hide UI
                        setBatchUIStatus(BatchUIStatus.LoadingJSONFile);
                        // Show status
                        txtConsole.Text += string.Format("> {0}\n", res.GetString("strJSONProcess", ci));
                        var useDefault = (cbNoImage.SelectedIndex == 1);
                        var createCard = (cbNoImage.SelectedIndex != 0);
                        // Pass parameters as objects to bg task
                        object[] bgParameters = new object[] { loadedCollection, useDefault, createCard };
                        bgJSONWorker.RunWorkerAsync(bgParameters);
                    }
                }
            };
            // Handler to manage event for Destination Folder Selection button
            btDestinationFolder.Click += (s, e) =>
            {
                // User can create new folders
                folderBrowserDialog.ShowNewFolderButton = true;
                // Show dialog and check response
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    // Store path
                    destinationPath = folderBrowserDialog.SelectedPath;
                    // Show OK status
                    showStatusPicture(pbDestination, 1);
                    // Go to next stage
                    setBatchUIStatus(BatchUIStatus.WaitingForAction);
                    // Show destination folder
                    lblDestinationFolder.Text = destinationPath;
                    lblDestinationFolder.Visible = true;
                }
            };
            // Handler to manage event for Batch Process button
            btBatchProcess.Click += (s, e) =>
            {
                // Check if worker working :D
                if (!bgBatchWorker.IsBusy)
                {
                    // Hide UI
                    btBatchProcess.Enabled   = false;
                    sizePanel.Visible        = false;
                    btBackCollection.Visible = false;
                    // Get zoom value
                    var zoom      = 0.0f;
                    var outWidth  = Config.cardWidth;
                    var outHeight = Config.cardHeight;
                    // based on which radiobutton is checked
                    if (rbZoom025.Checked) zoom = 0.25f;
                    else if (rbZoom050.Checked) zoom = 0.50f;
                    else if (rbZoom075.Checked) zoom = 0.75f;
                    else if (rbZoom100.Checked) zoom = 1.0f;
                    else if (rbZoom150.Checked) zoom = 1.5f;
                    else if (rbZoom200.Checked) zoom = 2.0f;
                    // If custom is checked, take dimensions only if all are fine
                    if (zoom == 0.0f)
                    {
                        if ((txtWidth.Text.Length > 0) && (txtHeight.Text.Length > 0))
                        {
                            outWidth = Convert.ToInt32(txtWidth.Text);
                            outHeight = Convert.ToInt32(txtHeight.Text);
                        }
                    }
                    // Zoom selected? apply zoom scale for main card size
                    else
                    {
                        outWidth = (int)(Config.cardWidth * zoom);
                        outHeight = (int)(Config.cardHeight * zoom);
                    }
                    // Show animations
                    pbWaitingAnim.Visible = true;
                    progressBar.Visible   = true;
                    // Pass parameters as objects to bg task
                    object[] bgParameters = new object[] { loadedCollection, outWidth, outHeight };
                    bgBatchWorker.RunWorkerAsync(bgParameters);
                }
            };
            // Handler to manage event for Cancel Batch Process button
            btBatchCancel.Click += (s, e) =>
            {
                // Doing batch process? cancel
                if (bgBatchWorker.IsBusy)
                {
                    bgBatchWorker.CancelAsync();
                }
                // Reading JSON file? cance
                else if (bgJSONWorker.IsBusy)
                {
                    bgJSONWorker.CancelAsync();
                }
                // Otherwise, go to first step of batch process
                else
                {
                    setBatchUIStatus(BatchUIStatus.AskForPictureFolder);
                }
            };
            // Handler to read JSON data from file in other thread
            bgJSONWorker.DoWork += (s, e) =>
            {
                // Get vars from method parameters
                var sendingWorker = (BackgroundWorker)s;
                var arrObjects    = (object[])e.Argument;
                var aCollection   = (CardItemCollection)arrObjects[0];
                var useDefault    = (bool)arrObjects[1];
                var createCard    = (bool)arrObjects[2];
                // Init internal vars
                int step;     // Cards processed so far
                int percent;  // Percent done
                int socent;   // aux var
                int ncards;   // Cards to process
                // To do stuff according to portrait existence or not
                string portrait;
                bool fileExists;
                // Read all JSON text from file
                var json = File.ReadAllText(jsonFilename);
                // Is a "AllSets.xxXX.json" file? (xxXX-Locale)
                var locale = General.getLocaleFromFilename(jsonFilename);
                if (locale != Locales.unkn)
                {
                    // Get cultureinfo for file locale
                    var cinfo = CultureInfo.CreateSpecificCulture(General.getCultureLocale(locale));
                    // Parse cards file
                    var dict  = JsonConvert.DeserializeObject<Dictionary<string, CardJSON[]>>(json);
                    // Get num. cards to process
                    ncards   = 0;
                    step     = 0;
                    percent  = 0;
                    foreach (KeyValuePair<string, CardJSON[]> kvp in dict)
                    {
                        // Only take into account valid sets
                        if (General.isValidCardSet(General.getCardSet(kvp.Key)))
                        {
                            ncards += kvp.Value.Length;
                        }
                    }
                    bgJSONWorker.ReportProgress(0, string.Format("{0}/{1}", step, ncards));
                    // File as array of sets with cards
                    foreach (KeyValuePair<string, CardJSON[]> kvp in dict)
                    {
                        // If user didn't cancel process
                        if (!sendingWorker.CancellationPending)
                        {
                            // store actual set to speed up things
                            var set = General.getCardSet(kvp.Key);
                            // Got a valid set?
                            if (General.isValidCardSet(set))
                            {
                                // Process all its cards
                                foreach (CardJSON card in kvp.Value)
                                {
                                    // If user didn't cancel process
                                    if (!sendingWorker.CancellationPending)
                                    {
                                        // Card is valid (minion, spell, weapon) and collectible?
                                        if ((General.isValidCardType(General.getMainCardType(card.Type))) && (card.Collectible))
                                        {
                                            // Get card portrait path
                                            portrait   = Path.Combine(sourcePath, card.Id + ".png");
                                            fileExists = File.Exists(portrait);
                                            // If it does not exist, empty filename
                                            if (!fileExists)
                                            {
                                                portrait = "";
                                            }
                                            if (fileExists || createCard)
                                            {
                                                // Take localized race value from JSON
                                                var race = (General.getCardRace(card.Race) != CardRace.None) ? res.GetString("str" + card.Race, cinfo) : "";
                                                // Create card from JSON data
                                                aCollection.Add(card, set, locale, portrait, race, useDefault);
                                            }
                                        }
                                        // Update status
                                        step++;
                                        socent = (int)Math.Floor((double)(step * 100 / ncards));
                                        if (socent > percent)
                                        {
                                            percent = socent;
                                            bgJSONWorker.ReportProgress(percent, string.Format("{0}/{1}", step, ncards));
                                        }
                                    }
                                    else
                                    {
                                        // cancel!
                                        e.Cancel = true;
                                        break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            // cancel!
                            e.Cancel = true;
                            break;
                        }
                    }
                }
                // it must be a "AllSetsAllLanguages.json"
                else
                {
                    // Process all locales: parse full JSON file
                    var dict = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, CardJSON[]>>>(json);
                    // This file will have an array of locales and, for each one, the same as the previous case
                    foreach (KeyValuePair<string, Dictionary<string, CardJSON[]>> local in dict)
                    {
                        // Did user cancel process?
                        if (!sendingWorker.CancellationPending)
                        {
                            // Take locale value now to speed up things
                            locale    = General.getLocaleFromString(local.Key);
                            var cinfo = CultureInfo.CreateSpecificCulture(General.getCultureLocale(locale));
                            // Info
                            txtConsole.Invoke(new MethodInvoker(() => txtConsole.Text += string.Format("> {0} '{1}'...\n", res.GetString("strProcessingLocale", ci), local.Key)));
                            // Get num. cards to process
                            ncards  = 0;
                            step    = 0;
                            percent = 0;
                            bgJSONWorker.ReportProgress(0, string.Format("{0}/{1}", step, ncards));
                            foreach (KeyValuePair<string, CardJSON[]> cards in local.Value)
                            {
                                if (General.isValidCardSet(General.getCardSet(cards.Key)))
                                    ncards += cards.Value.Length;
                            }
                            // For each set of cards...
                            foreach (KeyValuePair<string, CardJSON[]> cards in local.Value)
                            {
                                // store actual set to speed up things
                                var set = General.getCardSet(cards.Key);
                                // Valid set found? process its cards
                                if (General.isValidCardSet(set))
                                {
                                    // For each card...
                                    foreach (CardJSON card in cards.Value)
                                    {
                                        // Did user cancel process?
                                        if (!sendingWorker.CancellationPending)
                                        {
                                            if (General.isValidCardType(General.getMainCardType(card.Type)) && (card.Collectible))
                                            {
                                                // Get card portrait path
                                                portrait = Path.Combine(sourcePath, card.Id + ".png");
                                                fileExists = File.Exists(portrait);
                                                // If it does not exist, empty filename
                                                if (!fileExists)
                                                {
                                                    portrait = "";
                                                }
                                                if (fileExists || createCard)
                                                {
                                                    // Take localized race value from JSON
                                                    var race = (General.getCardRace(card.Race) != CardRace.None) ? res.GetString("str" + card.Race, cinfo) : "";
                                                    // Create card from JSON data
                                                    aCollection.Add(card, set, locale, portrait, race, useDefault);
                                                }
                                            }
                                            // Update status
                                            step++;
                                            socent = (int)Math.Floor((double)(step * 100 / ncards));
                                            if (socent > percent)
                                            {
                                                percent = socent;
                                                bgJSONWorker.ReportProgress(percent, string.Format("{0}/{1}", step, ncards));
                                            }
                                        }
                                        else
                                        {
                                            // cancel!
                                            e.Cancel = true;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            // cancel!
                            e.Cancel = true;
                            break;
                        }
                    }
                }
                // Process finished
                e.Result = string.Format("> {0}\n", res.GetString("strJSONParseSuccess", ci));
            };
            // Handler when finished reading data
            bgJSONWorker.RunWorkerCompleted += (s, e) =>
            {
                // Check if the worker has been canceled or if an error occurred
                if ((!e.Cancelled) && (e.Error == null))
                {
                    // Show return status in console
                    txtConsole.Text += (string)e.Result;
                    // Check if data was readed
                    var cardExists = (loadedCollection.Cards.Count > 0);
                    // Manage collections
                    if (cardExists)
                    {
                        // Populate locales combobox
                        feedingComboBoxes = true;
                        cbLocale.Items.Clear();
                        foreach (var locale in loadedCollection.Languages)
                        {
                            cbLocale.Items.Add((string)locale.Value);
                        }
                        cbLocale.SelectedIndex = 0;
                        // Populate collection combobox with card references, showing name
                        foreach (var kvp in loadedCollection.Cards)
                        {
                            cbCollection.Items.Add(new KeyValuePair<string, string>(kvp.Key, kvp.Value.Name));
                        }
                        cbCollection.ValueMember   = "Key";
                        cbCollection.DisplayMember = "Value";
                        feedingComboBoxes = false;
                        cbCollection.SelectedIndex = 0;
                        // Show JSON file
                        lblJSONFile.Text    = jsonFilename;
                        lblJSONFile.Visible = true;
                        // Set icon to "ok"
                        showStatusPicture(pbJSON, 1);
                        // Enable next step
                        setBatchUIStatus(BatchUIStatus.AskForDestinationFolder);
                    }
                    // Show or hide comboboxes based on result
                    ComboBox[] cbxs = new[] { cbCollection, cbLocale };
                    foreach (var combobox in cbxs)
                    {
                        combobox.Visible = cardExists;
                        combobox.Enabled = cardExists;
                    }
                    // Show info about loaded data
                    txtConsole.Text += string.Format(">" + res.GetString("strStatus", ci) + "\n", loadedCollection.Cards.Count, loadedCollection.Languages.Count); // "> Loaded {0} cards with {1} locales\n"
                }
                else if (e.Cancelled)
                {
                    // User cancelled process? warn
                    txtConsole.Text += string.Format("! {0}\n", res.GetString("strUserCancel", ci));
                    // Clear all
                    loadedCollection.Clear();
                    setBatchUIStatus(BatchUIStatus.AskForJSONFile);
                }
                else
                {
                    // Unknown error? warn
                    txtConsole.Text += string.Format("! {0}\n", res.GetString("strUnknownError", ci));
                    loadedCollection.Clear();
                    setBatchUIStatus(BatchUIStatus.AskForJSONFile);
                }
                // Restore UI status after completion
                pbWaitingAnim.Visible    = false;
                progressBar.Visible      = false;
                btBackCollection.Visible = true;
            };
            // Handler to update progress bar
            bgJSONWorker.ProgressChanged += (s, e) =>
            {
                // Use text from handler (card progress)
                progressBar.text = (string)e.UserState;
                // Store progress (percent value)
                progressBar.Value = e.ProgressPercentage;
            };
            // Handler to write card pictures to disk
            bgBatchWorker.DoWork += (s, e) =>
            {
                var sendingWorker = (BackgroundWorker)s;
                var arrObjects    = (object[])e.Argument;

                var aCollection = (CardItemCollection)arrObjects[0];
                var width       = (int)arrObjects[1];
                var height      = (int)arrObjects[2];

                // Do main stuff from here
                var directoryInfo     = new DirectoryInfo(sourcePath);
                var targetPath        = "";
                var destinationFile   = "";
                int ncards            = aCollection.Cards.Count;
                int percent;
                int socent;
                int step;
                // First of all, we'll create all directories for locales. They will be needed later, so... ^^
                foreach (var loc in aCollection.Languages)
                {
                    // Get directory path
                    targetPath = Path.Combine(destinationPath, loc.Value);
                    // If doesn't exist, create it
                    if (!Directory.Exists(targetPath))
                    {
                        Directory.CreateDirectory(targetPath);
                    }
                }
                // Reset progress counter
                step    = 0;
                percent = 0;
                // Report progress
                txtConsole.Invoke(new MethodInvoker(() => txtConsole.Text += string.Format("> {0}\n", res.GetString("strJSONProcess", ci))));
                bgBatchWorker.ReportProgress(0, string.Format("{0}/{1}", step, ncards));
                // Process all cards
                foreach (var card in aCollection.Cards)
                {
                    // Check if not cancelled
                    if (!sendingWorker.CancellationPending)
                    {
                        // Process all locales
                        foreach (var loc in aCollection.Languages)
                        {
                            // If not cancelled, process locale
                            if (!sendingWorker.CancellationPending)
                            {
                                // Get file path
                                destinationFile = Path.Combine(destinationPath, loc.Value, card.Key + ".png");
                                // Get localized card
                                var cardBitmap = card.Value.Picture(loc.Key, width, height);
                                // Save to file if exists
                                if (cardBitmap != null)
                                {
                                    cardBitmap.Save(destinationFile);
                                    // Free resources
                                    cardBitmap.Dispose();
                                }
                            }
                        }
                        // Update status
                        step++;
                        socent = (int)Math.Floor((double)(step * 100 / ncards));
                        if (socent > percent)
                        {
                            percent = socent;
                            bgBatchWorker.ReportProgress(percent, string.Format("{0}/{1}", step, ncards));
                        }
                    }
                    else
                    {
                        // User cancelled
                        e.Cancel = true;
                        break;
                    }
                }
                // Process finished
                e.Result = string.Format("> {0}\n", res.GetString("strCollectionSuccess", ci));
            };
            // Handler when finished writing card pictures
            bgBatchWorker.RunWorkerCompleted += (s, e) =>
            {
                // Check if the worker has been canceled or if an error occurred
                if ((!e.Cancelled) && (e.Error == null))
                {
                    // Show return status
                    txtConsole.Text += (string)e.Result;
                }
                else if (e.Cancelled)
                {
                    // Process cancelled by user? warn
                    txtConsole.Text += string.Format("! {0}\n", res.GetString("strUserCancel", ci));
                }
                else
                {
                    // Some other stuff? warn
                    txtConsole.Text += string.Format("! {0}\n", res.GetString("strUnknownError", ci));
                }
                // Restore UI status after completion
                pbWaitingAnim.Visible    = false;
                progressBar.Visible      = false;
                btBatchProcess.Enabled   = true;
                sizePanel.Visible        = true;
                btBackCollection.Visible = true;
            };
            // Handler to update progress bar
            bgBatchWorker.ProgressChanged += (s, e) =>
            {
                // Use text from handler (card progress)
                progressBar.text  = (string)e.UserState;
                // Store progress (percent value)
                progressBar.Value = e.ProgressPercentage;
            };
            // Resume painting
            ResumeLayout();
        }
    }
}
