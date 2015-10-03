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

namespace HSCardGenerator
{
    partial class BatchForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                cursorGrabOff.Dispose();
                cursorGrabOn.Dispose();
                cursorPointer.Dispose();
                cursorPointerDown.Dispose();

                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BatchForm));
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.bgBatchWorker = new System.ComponentModel.BackgroundWorker();
            this.batchPanel = new System.Windows.Forms.Panel();
            this.lblNoImage = new System.Windows.Forms.Label();
            this.cbNoImage = new System.Windows.Forms.ComboBox();
            this.lblDestinationFolder = new System.Windows.Forms.Label();
            this.lblJSONFile = new System.Windows.Forms.Label();
            this.lblImageFolder = new System.Windows.Forms.Label();
            this.sizePanel = new System.Windows.Forms.Panel();
            this.lblOutputSize = new System.Windows.Forms.Label();
            this.lblX = new System.Windows.Forms.Label();
            this.txtHeight = new System.Windows.Forms.TextBox();
            this.txtWidth = new System.Windows.Forms.TextBox();
            this.rbCustom = new System.Windows.Forms.RadioButton();
            this.rbZoom200 = new System.Windows.Forms.RadioButton();
            this.rbZoom150 = new System.Windows.Forms.RadioButton();
            this.rbZoom100 = new System.Windows.Forms.RadioButton();
            this.rbZoom075 = new System.Windows.Forms.RadioButton();
            this.rbZoom050 = new System.Windows.Forms.RadioButton();
            this.rbZoom025 = new System.Windows.Forms.RadioButton();
            this.pbPreview = new System.Windows.Forms.PictureBox();
            this.btBackCollection = new System.Windows.Forms.Button();
            this.btBatchCancel = new System.Windows.Forms.Button();
            this.btBatchProcess = new System.Windows.Forms.Button();
            this.cbCollection = new System.Windows.Forms.ComboBox();
            this.txtConsole = new System.Windows.Forms.RichTextBox();
            this.pbImage = new System.Windows.Forms.PictureBox();
            this.pbDestination = new System.Windows.Forms.PictureBox();
            this.pbJSON = new System.Windows.Forms.PictureBox();
            this.btDestinationFolder = new System.Windows.Forms.Button();
            this.btImageFolder = new System.Windows.Forms.Button();
            this.btOpenJSON = new System.Windows.Forms.Button();
            this.pbWaitingAnim = new System.Windows.Forms.PictureBox();
            this.bgJSONWorker = new System.ComponentModel.BackgroundWorker();
            this.batchPanel.SuspendLayout();
            this.sizePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDestination)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbJSON)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbWaitingAnim)).BeginInit();
            this.SuspendLayout();
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.ShowNewFolderButton = false;
            // 
            // openFileDialog
            // 
            this.openFileDialog.Title = "Select a file to open...";
            // 
            // bgBatchWorker
            // 
            this.bgBatchWorker.WorkerReportsProgress = true;
            this.bgBatchWorker.WorkerSupportsCancellation = true;
            // 
            // batchPanel
            // 
            this.batchPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.batchPanel.BackgroundImage = global::HSCardGenerator.Properties.Resources.Background600x240;
            this.batchPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.batchPanel.Controls.Add(this.lblNoImage);
            this.batchPanel.Controls.Add(this.cbNoImage);
            this.batchPanel.Controls.Add(this.lblDestinationFolder);
            this.batchPanel.Controls.Add(this.lblJSONFile);
            this.batchPanel.Controls.Add(this.lblImageFolder);
            this.batchPanel.Controls.Add(this.sizePanel);
            this.batchPanel.Controls.Add(this.pbPreview);
            this.batchPanel.Controls.Add(this.btBackCollection);
            this.batchPanel.Controls.Add(this.btBatchCancel);
            this.batchPanel.Controls.Add(this.btBatchProcess);
            this.batchPanel.Controls.Add(this.cbCollection);
            this.batchPanel.Controls.Add(this.txtConsole);
            this.batchPanel.Controls.Add(this.pbImage);
            this.batchPanel.Controls.Add(this.pbDestination);
            this.batchPanel.Controls.Add(this.pbJSON);
            this.batchPanel.Controls.Add(this.btDestinationFolder);
            this.batchPanel.Controls.Add(this.btImageFolder);
            this.batchPanel.Controls.Add(this.btOpenJSON);
            this.batchPanel.Controls.Add(this.pbWaitingAnim);
            this.batchPanel.Location = new System.Drawing.Point(0, 0);
            this.batchPanel.Margin = new System.Windows.Forms.Padding(0);
            this.batchPanel.Name = "batchPanel";
            this.batchPanel.Size = new System.Drawing.Size(600, 260);
            this.batchPanel.TabIndex = 4;
            // 
            // lblNoImage
            // 
            this.lblNoImage.BackColor = System.Drawing.Color.Transparent;
            this.lblNoImage.Location = new System.Drawing.Point(12, 59);
            this.lblNoImage.Name = "lblNoImage";
            this.lblNoImage.Size = new System.Drawing.Size(186, 23);
            this.lblNoImage.TabIndex = 20;
            this.lblNoImage.Text = "strNoImage";
            this.lblNoImage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblNoImage.Visible = false;
            // 
            // cbNoImage
            // 
            this.cbNoImage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbNoImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbNoImage.FormattingEnabled = true;
            this.cbNoImage.ItemHeight = 13;
            this.cbNoImage.Items.AddRange(new object[] {
            "Do not generate card",
            "Use default image",
            "Leave an empty space"});
            this.cbNoImage.Location = new System.Drawing.Point(213, 60);
            this.cbNoImage.Name = "cbNoImage";
            this.cbNoImage.Size = new System.Drawing.Size(220, 21);
            this.cbNoImage.TabIndex = 19;
            this.cbNoImage.Visible = false;
            // 
            // lblDestinationFolder
            // 
            this.lblDestinationFolder.AutoEllipsis = true;
            this.lblDestinationFolder.BackColor = System.Drawing.Color.Transparent;
            this.lblDestinationFolder.Location = new System.Drawing.Point(244, 84);
            this.lblDestinationFolder.Name = "lblDestinationFolder";
            this.lblDestinationFolder.Size = new System.Drawing.Size(190, 24);
            this.lblDestinationFolder.TabIndex = 18;
            this.lblDestinationFolder.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblDestinationFolder.Visible = false;
            // 
            // lblJSONFile
            // 
            this.lblJSONFile.AutoEllipsis = true;
            this.lblJSONFile.BackColor = System.Drawing.Color.Transparent;
            this.lblJSONFile.Location = new System.Drawing.Point(243, 34);
            this.lblJSONFile.Name = "lblJSONFile";
            this.lblJSONFile.Size = new System.Drawing.Size(190, 24);
            this.lblJSONFile.TabIndex = 17;
            this.lblJSONFile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblJSONFile.Visible = false;
            // 
            // lblImageFolder
            // 
            this.lblImageFolder.AutoEllipsis = true;
            this.lblImageFolder.BackColor = System.Drawing.Color.Transparent;
            this.lblImageFolder.Location = new System.Drawing.Point(243, 9);
            this.lblImageFolder.Name = "lblImageFolder";
            this.lblImageFolder.Size = new System.Drawing.Size(190, 24);
            this.lblImageFolder.TabIndex = 16;
            this.lblImageFolder.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblImageFolder.Visible = false;
            // 
            // sizePanel
            // 
            this.sizePanel.BackColor = System.Drawing.Color.Transparent;
            this.sizePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sizePanel.Controls.Add(this.lblOutputSize);
            this.sizePanel.Controls.Add(this.lblX);
            this.sizePanel.Controls.Add(this.txtHeight);
            this.sizePanel.Controls.Add(this.txtWidth);
            this.sizePanel.Controls.Add(this.rbCustom);
            this.sizePanel.Controls.Add(this.rbZoom200);
            this.sizePanel.Controls.Add(this.rbZoom150);
            this.sizePanel.Controls.Add(this.rbZoom100);
            this.sizePanel.Controls.Add(this.rbZoom075);
            this.sizePanel.Controls.Add(this.rbZoom050);
            this.sizePanel.Controls.Add(this.rbZoom025);
            this.sizePanel.Location = new System.Drawing.Point(7, 112);
            this.sizePanel.Margin = new System.Windows.Forms.Padding(0);
            this.sizePanel.Name = "sizePanel";
            this.sizePanel.Size = new System.Drawing.Size(198, 90);
            this.sizePanel.TabIndex = 15;
            // 
            // lblOutputSize
            // 
            this.lblOutputSize.AutoSize = true;
            this.lblOutputSize.Location = new System.Drawing.Point(3, 2);
            this.lblOutputSize.Name = "lblOutputSize";
            this.lblOutputSize.Size = new System.Drawing.Size(70, 13);
            this.lblOutputSize.TabIndex = 10;
            this.lblOutputSize.Text = "strOutputSize";
            // 
            // lblX
            // 
            this.lblX.AutoSize = true;
            this.lblX.Location = new System.Drawing.Point(145, 66);
            this.lblX.Name = "lblX";
            this.lblX.Size = new System.Drawing.Size(14, 13);
            this.lblX.TabIndex = 9;
            this.lblX.Text = "X";
            this.lblX.Visible = false;
            // 
            // txtHeight
            // 
            this.txtHeight.Location = new System.Drawing.Point(160, 62);
            this.txtHeight.MaxLength = 4;
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new System.Drawing.Size(30, 20);
            this.txtHeight.TabIndex = 8;
            this.txtHeight.Visible = false;
            // 
            // txtWidth
            // 
            this.txtWidth.Location = new System.Drawing.Point(115, 62);
            this.txtWidth.MaxLength = 4;
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Size = new System.Drawing.Size(30, 20);
            this.txtWidth.TabIndex = 7;
            this.txtWidth.Visible = false;
            // 
            // rbCustom
            // 
            this.rbCustom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbCustom.ForeColor = System.Drawing.Color.Black;
            this.rbCustom.Location = new System.Drawing.Point(4, 63);
            this.rbCustom.Margin = new System.Windows.Forms.Padding(0);
            this.rbCustom.Name = "rbCustom";
            this.rbCustom.Size = new System.Drawing.Size(74, 17);
            this.rbCustom.TabIndex = 6;
            this.rbCustom.Text = "strCustom";
            this.rbCustom.UseVisualStyleBackColor = true;
            // 
            // rbZoom200
            // 
            this.rbZoom200.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbZoom200.ForeColor = System.Drawing.Color.DodgerBlue;
            this.rbZoom200.Location = new System.Drawing.Point(127, 42);
            this.rbZoom200.Margin = new System.Windows.Forms.Padding(0);
            this.rbZoom200.Name = "rbZoom200";
            this.rbZoom200.Size = new System.Drawing.Size(62, 17);
            this.rbZoom200.TabIndex = 5;
            this.rbZoom200.Text = "200%";
            this.rbZoom200.UseVisualStyleBackColor = true;
            // 
            // rbZoom150
            // 
            this.rbZoom150.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbZoom150.ForeColor = System.Drawing.Color.DodgerBlue;
            this.rbZoom150.Location = new System.Drawing.Point(62, 42);
            this.rbZoom150.Margin = new System.Windows.Forms.Padding(0);
            this.rbZoom150.Name = "rbZoom150";
            this.rbZoom150.Size = new System.Drawing.Size(62, 17);
            this.rbZoom150.TabIndex = 4;
            this.rbZoom150.Text = "150%";
            this.rbZoom150.UseVisualStyleBackColor = true;
            // 
            // rbZoom100
            // 
            this.rbZoom100.Checked = true;
            this.rbZoom100.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbZoom100.ForeColor = System.Drawing.Color.ForestGreen;
            this.rbZoom100.Location = new System.Drawing.Point(4, 42);
            this.rbZoom100.Margin = new System.Windows.Forms.Padding(0);
            this.rbZoom100.Name = "rbZoom100";
            this.rbZoom100.Size = new System.Drawing.Size(62, 17);
            this.rbZoom100.TabIndex = 3;
            this.rbZoom100.TabStop = true;
            this.rbZoom100.Text = "100%";
            this.rbZoom100.UseVisualStyleBackColor = true;
            // 
            // rbZoom075
            // 
            this.rbZoom075.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbZoom075.ForeColor = System.Drawing.Color.SaddleBrown;
            this.rbZoom075.Location = new System.Drawing.Point(127, 22);
            this.rbZoom075.Margin = new System.Windows.Forms.Padding(0);
            this.rbZoom075.Name = "rbZoom075";
            this.rbZoom075.Size = new System.Drawing.Size(62, 17);
            this.rbZoom075.TabIndex = 2;
            this.rbZoom075.Text = "75%";
            this.rbZoom075.UseVisualStyleBackColor = true;
            // 
            // rbZoom050
            // 
            this.rbZoom050.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbZoom050.ForeColor = System.Drawing.Color.SaddleBrown;
            this.rbZoom050.Location = new System.Drawing.Point(62, 22);
            this.rbZoom050.Margin = new System.Windows.Forms.Padding(0);
            this.rbZoom050.Name = "rbZoom050";
            this.rbZoom050.Size = new System.Drawing.Size(62, 17);
            this.rbZoom050.TabIndex = 1;
            this.rbZoom050.Text = "50%";
            this.rbZoom050.UseVisualStyleBackColor = true;
            // 
            // rbZoom025
            // 
            this.rbZoom025.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbZoom025.ForeColor = System.Drawing.Color.SaddleBrown;
            this.rbZoom025.Location = new System.Drawing.Point(4, 22);
            this.rbZoom025.Margin = new System.Windows.Forms.Padding(0);
            this.rbZoom025.Name = "rbZoom025";
            this.rbZoom025.Size = new System.Drawing.Size(62, 17);
            this.rbZoom025.TabIndex = 0;
            this.rbZoom025.Text = "25%";
            this.rbZoom025.UseVisualStyleBackColor = true;
            // 
            // pbPreview
            // 
            this.pbPreview.BackColor = System.Drawing.Color.Transparent;
            this.pbPreview.ErrorImage = null;
            this.pbPreview.InitialImage = null;
            this.pbPreview.Location = new System.Drawing.Point(440, 8);
            this.pbPreview.Name = "pbPreview";
            this.pbPreview.Size = new System.Drawing.Size(150, 215);
            this.pbPreview.TabIndex = 14;
            this.pbPreview.TabStop = false;
            // 
            // btBackCollection
            // 
            this.btBackCollection.BackColor = System.Drawing.Color.Transparent;
            this.btBackCollection.BackgroundImage = global::HSCardGenerator.Properties.Resources.ButtonBack40x24;
            this.btBackCollection.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btBackCollection.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btBackCollection.FlatAppearance.BorderSize = 0;
            this.btBackCollection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btBackCollection.Location = new System.Drawing.Point(550, 227);
            this.btBackCollection.Margin = new System.Windows.Forms.Padding(0);
            this.btBackCollection.Name = "btBackCollection";
            this.btBackCollection.Size = new System.Drawing.Size(40, 24);
            this.btBackCollection.TabIndex = 13;
            this.btBackCollection.UseVisualStyleBackColor = false;
            // 
            // btBatchCancel
            // 
            this.btBatchCancel.BackColor = System.Drawing.Color.Transparent;
            this.btBatchCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btBatchCancel.BackgroundImage")));
            this.btBatchCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btBatchCancel.Enabled = false;
            this.btBatchCancel.FlatAppearance.BorderSize = 0;
            this.btBatchCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btBatchCancel.ForeColor = System.Drawing.Color.Black;
            this.btBatchCancel.Location = new System.Drawing.Point(7, 230);
            this.btBatchCancel.Margin = new System.Windows.Forms.Padding(0);
            this.btBatchCancel.Name = "btBatchCancel";
            this.btBatchCancel.Size = new System.Drawing.Size(200, 24);
            this.btBatchCancel.TabIndex = 12;
            this.btBatchCancel.Text = "strCancelProcess";
            this.btBatchCancel.UseVisualStyleBackColor = false;
            this.btBatchCancel.Visible = false;
            // 
            // btBatchProcess
            // 
            this.btBatchProcess.BackColor = System.Drawing.Color.Transparent;
            this.btBatchProcess.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btBatchProcess.BackgroundImage")));
            this.btBatchProcess.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btBatchProcess.Enabled = false;
            this.btBatchProcess.FlatAppearance.BorderSize = 0;
            this.btBatchProcess.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btBatchProcess.ForeColor = System.Drawing.Color.Black;
            this.btBatchProcess.Location = new System.Drawing.Point(7, 204);
            this.btBatchProcess.Margin = new System.Windows.Forms.Padding(0);
            this.btBatchProcess.Name = "btBatchProcess";
            this.btBatchProcess.Size = new System.Drawing.Size(200, 24);
            this.btBatchProcess.TabIndex = 11;
            this.btBatchProcess.Text = "strCreateCards";
            this.btBatchProcess.UseVisualStyleBackColor = false;
            this.btBatchProcess.Visible = false;
            // 
            // cbCollection
            // 
            this.cbCollection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCollection.Enabled = false;
            this.cbCollection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbCollection.FormattingEnabled = true;
            this.cbCollection.ItemHeight = 13;
            this.cbCollection.Location = new System.Drawing.Point(394, 229);
            this.cbCollection.Name = "cbCollection";
            this.cbCollection.Size = new System.Drawing.Size(147, 21);
            this.cbCollection.TabIndex = 10;
            this.cbCollection.Visible = false;
            // 
            // txtConsole
            // 
            this.txtConsole.BackColor = System.Drawing.Color.Black;
            this.txtConsole.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtConsole.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConsole.ForeColor = System.Drawing.Color.Lime;
            this.txtConsole.Location = new System.Drawing.Point(212, 112);
            this.txtConsole.Name = "txtConsole";
            this.txtConsole.ReadOnly = true;
            this.txtConsole.Size = new System.Drawing.Size(220, 79);
            this.txtConsole.TabIndex = 9;
            this.txtConsole.Text = "";
            // 
            // pbImage
            // 
            this.pbImage.BackColor = System.Drawing.Color.Transparent;
            this.pbImage.ErrorImage = null;
            this.pbImage.InitialImage = null;
            this.pbImage.Location = new System.Drawing.Point(213, 8);
            this.pbImage.Name = "pbImage";
            this.pbImage.Size = new System.Drawing.Size(24, 24);
            this.pbImage.TabIndex = 8;
            this.pbImage.TabStop = false;
            // 
            // pbDestination
            // 
            this.pbDestination.BackColor = System.Drawing.Color.Transparent;
            this.pbDestination.Enabled = false;
            this.pbDestination.ErrorImage = null;
            this.pbDestination.InitialImage = null;
            this.pbDestination.Location = new System.Drawing.Point(213, 84);
            this.pbDestination.Name = "pbDestination";
            this.pbDestination.Size = new System.Drawing.Size(24, 24);
            this.pbDestination.TabIndex = 7;
            this.pbDestination.TabStop = false;
            this.pbDestination.Visible = false;
            // 
            // pbJSON
            // 
            this.pbJSON.BackColor = System.Drawing.Color.Transparent;
            this.pbJSON.Enabled = false;
            this.pbJSON.ErrorImage = null;
            this.pbJSON.InitialImage = null;
            this.pbJSON.Location = new System.Drawing.Point(213, 34);
            this.pbJSON.Name = "pbJSON";
            this.pbJSON.Size = new System.Drawing.Size(24, 24);
            this.pbJSON.TabIndex = 6;
            this.pbJSON.TabStop = false;
            this.pbJSON.Visible = false;
            // 
            // btDestinationFolder
            // 
            this.btDestinationFolder.BackColor = System.Drawing.Color.Transparent;
            this.btDestinationFolder.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btDestinationFolder.BackgroundImage")));
            this.btDestinationFolder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btDestinationFolder.Enabled = false;
            this.btDestinationFolder.FlatAppearance.BorderSize = 0;
            this.btDestinationFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btDestinationFolder.ForeColor = System.Drawing.Color.Black;
            this.btDestinationFolder.Location = new System.Drawing.Point(7, 84);
            this.btDestinationFolder.Margin = new System.Windows.Forms.Padding(0);
            this.btDestinationFolder.Name = "btDestinationFolder";
            this.btDestinationFolder.Size = new System.Drawing.Size(200, 24);
            this.btDestinationFolder.TabIndex = 4;
            this.btDestinationFolder.Text = "strChooseDestination";
            this.btDestinationFolder.UseVisualStyleBackColor = false;
            this.btDestinationFolder.Visible = false;
            // 
            // btImageFolder
            // 
            this.btImageFolder.BackColor = System.Drawing.Color.Transparent;
            this.btImageFolder.BackgroundImage = global::HSCardGenerator.Properties.Resources.ButtonBackground200x24;
            this.btImageFolder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btImageFolder.FlatAppearance.BorderSize = 0;
            this.btImageFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btImageFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btImageFolder.ForeColor = System.Drawing.Color.Black;
            this.btImageFolder.Location = new System.Drawing.Point(7, 8);
            this.btImageFolder.Margin = new System.Windows.Forms.Padding(0);
            this.btImageFolder.Name = "btImageFolder";
            this.btImageFolder.Size = new System.Drawing.Size(200, 24);
            this.btImageFolder.TabIndex = 2;
            this.btImageFolder.Text = "strImageFolder";
            this.btImageFolder.UseVisualStyleBackColor = false;
            // 
            // btOpenJSON
            // 
            this.btOpenJSON.BackColor = System.Drawing.Color.Transparent;
            this.btOpenJSON.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btOpenJSON.BackgroundImage")));
            this.btOpenJSON.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btOpenJSON.Enabled = false;
            this.btOpenJSON.FlatAppearance.BorderSize = 0;
            this.btOpenJSON.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btOpenJSON.ForeColor = System.Drawing.Color.Black;
            this.btOpenJSON.Location = new System.Drawing.Point(7, 34);
            this.btOpenJSON.Margin = new System.Windows.Forms.Padding(0);
            this.btOpenJSON.Name = "btOpenJSON";
            this.btOpenJSON.Size = new System.Drawing.Size(200, 24);
            this.btOpenJSON.TabIndex = 0;
            this.btOpenJSON.Text = "strOpenJSONFile";
            this.btOpenJSON.UseVisualStyleBackColor = false;
            this.btOpenJSON.Visible = false;
            // 
            // pbWaitingAnim
            // 
            this.pbWaitingAnim.BackColor = System.Drawing.Color.Transparent;
            this.pbWaitingAnim.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pbWaitingAnim.ErrorImage = null;
            this.pbWaitingAnim.Image = global::HSCardGenerator.Properties.Resources.waitinganimation;
            this.pbWaitingAnim.InitialImage = null;
            this.pbWaitingAnim.Location = new System.Drawing.Point(60, 124);
            this.pbWaitingAnim.Margin = new System.Windows.Forms.Padding(0);
            this.pbWaitingAnim.Name = "pbWaitingAnim";
            this.pbWaitingAnim.Size = new System.Drawing.Size(100, 70);
            this.pbWaitingAnim.TabIndex = 5;
            this.pbWaitingAnim.TabStop = false;
            // 
            // bgJSONWorker
            // 
            this.bgJSONWorker.WorkerReportsProgress = true;
            this.bgJSONWorker.WorkerSupportsCancellation = true;
            // 
            // BatchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(600, 260);
            this.Controls.Add(this.batchPanel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(200, 200);
            this.Name = "BatchForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Create Collection";
            this.TopMost = true;
            this.batchPanel.ResumeLayout(false);
            this.sizePanel.ResumeLayout(false);
            this.sizePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDestination)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbJSON)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbWaitingAnim)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel batchPanel;
        private System.Windows.Forms.Button btBackCollection;
        private System.Windows.Forms.Button btBatchCancel;
        private System.Windows.Forms.Button btBatchProcess;
        private System.Windows.Forms.ComboBox cbCollection;
        private System.Windows.Forms.RichTextBox txtConsole;
        private System.Windows.Forms.PictureBox pbImage;
        private System.Windows.Forms.PictureBox pbDestination;
        private System.Windows.Forms.PictureBox pbJSON;
        private System.Windows.Forms.Button btDestinationFolder;
        private System.Windows.Forms.Button btImageFolder;
        private System.Windows.Forms.Button btOpenJSON;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.PictureBox pbPreview;
        private System.Windows.Forms.Panel sizePanel;
        private System.Windows.Forms.RadioButton rbCustom;
        private System.Windows.Forms.RadioButton rbZoom200;
        private System.Windows.Forms.RadioButton rbZoom150;
        private System.Windows.Forms.RadioButton rbZoom100;
        private System.Windows.Forms.RadioButton rbZoom075;
        private System.Windows.Forms.RadioButton rbZoom050;
        private System.Windows.Forms.RadioButton rbZoom025;
        private System.Windows.Forms.TextBox txtHeight;
        private System.Windows.Forms.TextBox txtWidth;
        private System.Windows.Forms.Label lblX;
        private System.Windows.Forms.Label lblOutputSize;
        private System.Windows.Forms.PictureBox pbWaitingAnim;
        private System.ComponentModel.BackgroundWorker bgBatchWorker;
        private System.Windows.Forms.Label lblImageFolder;
        private System.Windows.Forms.Label lblJSONFile;
        private System.Windows.Forms.Label lblDestinationFolder;
        private System.ComponentModel.BackgroundWorker bgJSONWorker;
        private System.Windows.Forms.Label lblNoImage;
        private System.Windows.Forms.ComboBox cbNoImage;
    }
}