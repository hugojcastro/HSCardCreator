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
    partial class CustomForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomForm));
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.customPanel = new System.Windows.Forms.Panel();
            this.customSubPanel = new System.Windows.Forms.Panel();
            this.btClearFilename = new System.Windows.Forms.Button();
            this.lblFilename = new System.Windows.Forms.Label();
            this.btBackCustom = new System.Windows.Forms.Button();
            this.btImage = new System.Windows.Forms.Button();
            this.btSave = new System.Windows.Forms.Button();
            this.cbRace = new System.Windows.Forms.ComboBox();
            this.lblRace = new System.Windows.Forms.Label();
            this.cbQuality = new System.Windows.Forms.ComboBox();
            this.lblQuality = new System.Windows.Forms.Label();
            this.btUpdate = new System.Windows.Forms.Button();
            this.cbSet = new System.Windows.Forms.ComboBox();
            this.lblSet = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.txtHealth = new System.Windows.Forms.TextBox();
            this.lblHealth = new System.Windows.Forms.Label();
            this.txtAttack = new System.Windows.Forms.TextBox();
            this.lblAttack = new System.Windows.Forms.Label();
            this.txtCost = new System.Windows.Forms.TextBox();
            this.lblCost = new System.Windows.Forms.Label();
            this.cbClass = new System.Windows.Forms.ComboBox();
            this.lblClass = new System.Windows.Forms.Label();
            this.cbType = new System.Windows.Forms.ComboBox();
            this.lblType = new System.Windows.Forms.Label();
            this.canvas = new System.Windows.Forms.PictureBox();
            this.lblDefaultPicture = new System.Windows.Forms.Label();
            this.cbDefaultPicture = new System.Windows.Forms.CheckBox();
            this.customPanel.SuspendLayout();
            this.customSubPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog
            // 
            this.openFileDialog.Title = "Select a file to open...";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "*.png";
            this.saveFileDialog.Filter = "All PNG files|*.png";
            this.saveFileDialog.Title = "Save card to...";
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.ShowNewFolderButton = false;
            // 
            // customPanel
            // 
            this.customPanel.BackgroundImage = global::HSCardGenerator.Properties.Resources.Background650x632;
            this.customPanel.Controls.Add(this.customSubPanel);
            this.customPanel.Controls.Add(this.canvas);
            this.customPanel.Location = new System.Drawing.Point(0, 0);
            this.customPanel.Margin = new System.Windows.Forms.Padding(0);
            this.customPanel.Name = "customPanel";
            this.customPanel.Size = new System.Drawing.Size(650, 577);
            this.customPanel.TabIndex = 5;
            // 
            // customSubPanel
            // 
            this.customSubPanel.BackColor = System.Drawing.Color.Transparent;
            this.customSubPanel.Controls.Add(this.cbDefaultPicture);
            this.customSubPanel.Controls.Add(this.lblDefaultPicture);
            this.customSubPanel.Controls.Add(this.btClearFilename);
            this.customSubPanel.Controls.Add(this.lblFilename);
            this.customSubPanel.Controls.Add(this.btBackCustom);
            this.customSubPanel.Controls.Add(this.btImage);
            this.customSubPanel.Controls.Add(this.btSave);
            this.customSubPanel.Controls.Add(this.cbRace);
            this.customSubPanel.Controls.Add(this.lblRace);
            this.customSubPanel.Controls.Add(this.cbQuality);
            this.customSubPanel.Controls.Add(this.lblQuality);
            this.customSubPanel.Controls.Add(this.btUpdate);
            this.customSubPanel.Controls.Add(this.cbSet);
            this.customSubPanel.Controls.Add(this.lblSet);
            this.customSubPanel.Controls.Add(this.txtDescription);
            this.customSubPanel.Controls.Add(this.lblDescription);
            this.customSubPanel.Controls.Add(this.txtName);
            this.customSubPanel.Controls.Add(this.lblName);
            this.customSubPanel.Controls.Add(this.txtHealth);
            this.customSubPanel.Controls.Add(this.lblHealth);
            this.customSubPanel.Controls.Add(this.txtAttack);
            this.customSubPanel.Controls.Add(this.lblAttack);
            this.customSubPanel.Controls.Add(this.txtCost);
            this.customSubPanel.Controls.Add(this.lblCost);
            this.customSubPanel.Controls.Add(this.cbClass);
            this.customSubPanel.Controls.Add(this.lblClass);
            this.customSubPanel.Controls.Add(this.cbType);
            this.customSubPanel.Controls.Add(this.lblType);
            this.customSubPanel.Location = new System.Drawing.Point(410, 12);
            this.customSubPanel.Margin = new System.Windows.Forms.Padding(0);
            this.customSubPanel.Name = "customSubPanel";
            this.customSubPanel.Size = new System.Drawing.Size(228, 550);
            this.customSubPanel.TabIndex = 2;
            // 
            // btClearFilename
            // 
            this.btClearFilename.BackgroundImage = global::HSCardGenerator.Properties.Resources.ButtonClear24x24;
            this.btClearFilename.FlatAppearance.BorderSize = 0;
            this.btClearFilename.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btClearFilename.Location = new System.Drawing.Point(190, 421);
            this.btClearFilename.Margin = new System.Windows.Forms.Padding(0);
            this.btClearFilename.Name = "btClearFilename";
            this.btClearFilename.Size = new System.Drawing.Size(24, 24);
            this.btClearFilename.TabIndex = 26;
            this.btClearFilename.UseVisualStyleBackColor = true;
            this.btClearFilename.Visible = false;
            // 
            // lblFilename
            // 
            this.lblFilename.AutoEllipsis = true;
            this.lblFilename.Location = new System.Drawing.Point(14, 425);
            this.lblFilename.Name = "lblFilename";
            this.lblFilename.Size = new System.Drawing.Size(170, 16);
            this.lblFilename.TabIndex = 25;
            this.lblFilename.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblFilename.Visible = false;
            // 
            // btBackCustom
            // 
            this.btBackCustom.BackColor = System.Drawing.Color.Transparent;
            this.btBackCustom.BackgroundImage = global::HSCardGenerator.Properties.Resources.ButtonBack40x24;
            this.btBackCustom.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btBackCustom.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btBackCustom.FlatAppearance.BorderSize = 0;
            this.btBackCustom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btBackCustom.Location = new System.Drawing.Point(183, 521);
            this.btBackCustom.Margin = new System.Windows.Forms.Padding(0);
            this.btBackCustom.Name = "btBackCustom";
            this.btBackCustom.Size = new System.Drawing.Size(40, 24);
            this.btBackCustom.TabIndex = 24;
            this.btBackCustom.UseVisualStyleBackColor = false;
            // 
            // btImage
            // 
            this.btImage.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btImage.BackgroundImage")));
            this.btImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btImage.FlatAppearance.BorderSize = 0;
            this.btImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btImage.Location = new System.Drawing.Point(14, 395);
            this.btImage.Name = "btImage";
            this.btImage.Size = new System.Drawing.Size(200, 24);
            this.btImage.TabIndex = 23;
            this.btImage.Text = "strSelectImage";
            this.btImage.UseVisualStyleBackColor = true;
            // 
            // btSave
            // 
            this.btSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btSave.BackgroundImage")));
            this.btSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btSave.Enabled = false;
            this.btSave.FlatAppearance.BorderSize = 0;
            this.btSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btSave.Location = new System.Drawing.Point(14, 479);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(200, 24);
            this.btSave.TabIndex = 22;
            this.btSave.Text = "strSaveCard";
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Visible = false;
            // 
            // cbRace
            // 
            this.cbRace.BackColor = System.Drawing.Color.Red;
            this.cbRace.ForeColor = System.Drawing.Color.White;
            this.cbRace.FormattingEnabled = true;
            this.cbRace.Location = new System.Drawing.Point(92, 136);
            this.cbRace.MaxDropDownItems = 4;
            this.cbRace.Name = "cbRace";
            this.cbRace.Size = new System.Drawing.Size(122, 21);
            this.cbRace.TabIndex = 21;
            this.cbRace.Text = "[Select One]";
            this.cbRace.Visible = false;
            // 
            // lblRace
            // 
            this.lblRace.AutoSize = true;
            this.lblRace.Location = new System.Drawing.Point(10, 142);
            this.lblRace.Name = "lblRace";
            this.lblRace.Size = new System.Drawing.Size(44, 13);
            this.lblRace.TabIndex = 20;
            this.lblRace.Text = "strRace";
            this.lblRace.Visible = false;
            // 
            // cbQuality
            // 
            this.cbQuality.BackColor = System.Drawing.Color.Red;
            this.cbQuality.ForeColor = System.Drawing.Color.White;
            this.cbQuality.FormattingEnabled = true;
            this.cbQuality.Location = new System.Drawing.Point(92, 110);
            this.cbQuality.MaxDropDownItems = 4;
            this.cbQuality.Name = "cbQuality";
            this.cbQuality.Size = new System.Drawing.Size(122, 21);
            this.cbQuality.TabIndex = 19;
            this.cbQuality.Text = "[Select One]";
            // 
            // lblQuality
            // 
            this.lblQuality.AutoSize = true;
            this.lblQuality.Location = new System.Drawing.Point(10, 115);
            this.lblQuality.Name = "lblQuality";
            this.lblQuality.Size = new System.Drawing.Size(50, 13);
            this.lblQuality.TabIndex = 18;
            this.lblQuality.Text = "strQuality";
            // 
            // btUpdate
            // 
            this.btUpdate.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btUpdate.BackgroundImage")));
            this.btUpdate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btUpdate.FlatAppearance.BorderSize = 0;
            this.btUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btUpdate.Location = new System.Drawing.Point(14, 451);
            this.btUpdate.Name = "btUpdate";
            this.btUpdate.Size = new System.Drawing.Size(200, 24);
            this.btUpdate.TabIndex = 17;
            this.btUpdate.Text = "strUpdateCard";
            this.btUpdate.UseVisualStyleBackColor = true;
            // 
            // cbSet
            // 
            this.cbSet.BackColor = System.Drawing.Color.Red;
            this.cbSet.ForeColor = System.Drawing.Color.White;
            this.cbSet.FormattingEnabled = true;
            this.cbSet.Location = new System.Drawing.Point(92, 84);
            this.cbSet.MaxDropDownItems = 4;
            this.cbSet.Name = "cbSet";
            this.cbSet.Size = new System.Drawing.Size(122, 21);
            this.cbSet.TabIndex = 16;
            this.cbSet.Text = "[Select One]";
            // 
            // lblSet
            // 
            this.lblSet.AutoSize = true;
            this.lblSet.Location = new System.Drawing.Point(10, 88);
            this.lblSet.Name = "lblSet";
            this.lblSet.Size = new System.Drawing.Size(34, 13);
            this.lblSet.TabIndex = 15;
            this.lblSet.Text = "strSet";
            // 
            // txtDescription
            // 
            this.txtDescription.AcceptsReturn = true;
            this.txtDescription.BackColor = System.Drawing.Color.Green;
            this.txtDescription.ForeColor = System.Drawing.Color.White;
            this.txtDescription.Location = new System.Drawing.Point(13, 225);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(200, 64);
            this.txtDescription.TabIndex = 14;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblDescription.Location = new System.Drawing.Point(10, 209);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(39, 13);
            this.lblDescription.TabIndex = 13;
            this.lblDescription.Text = "strText";
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.Color.Red;
            this.txtName.ForeColor = System.Drawing.Color.White;
            this.txtName.Location = new System.Drawing.Point(13, 185);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(200, 20);
            this.txtName.TabIndex = 12;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblName.Location = new System.Drawing.Point(10, 169);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(46, 13);
            this.lblName.TabIndex = 11;
            this.lblName.Text = "strName";
            // 
            // txtHealth
            // 
            this.txtHealth.BackColor = System.Drawing.Color.Red;
            this.txtHealth.ForeColor = System.Drawing.Color.White;
            this.txtHealth.Location = new System.Drawing.Point(191, 346);
            this.txtHealth.Name = "txtHealth";
            this.txtHealth.Size = new System.Drawing.Size(22, 20);
            this.txtHealth.TabIndex = 9;
            this.txtHealth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtHealth.Visible = false;
            // 
            // lblHealth
            // 
            this.lblHealth.AutoSize = true;
            this.lblHealth.Location = new System.Drawing.Point(10, 349);
            this.lblHealth.Name = "lblHealth";
            this.lblHealth.Size = new System.Drawing.Size(49, 13);
            this.lblHealth.TabIndex = 8;
            this.lblHealth.Text = "strHealth";
            this.lblHealth.Visible = false;
            // 
            // txtAttack
            // 
            this.txtAttack.BackColor = System.Drawing.Color.Red;
            this.txtAttack.ForeColor = System.Drawing.Color.White;
            this.txtAttack.Location = new System.Drawing.Point(191, 320);
            this.txtAttack.Name = "txtAttack";
            this.txtAttack.Size = new System.Drawing.Size(22, 20);
            this.txtAttack.TabIndex = 7;
            this.txtAttack.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAttack.Visible = false;
            // 
            // lblAttack
            // 
            this.lblAttack.AutoSize = true;
            this.lblAttack.Location = new System.Drawing.Point(10, 325);
            this.lblAttack.Name = "lblAttack";
            this.lblAttack.Size = new System.Drawing.Size(49, 13);
            this.lblAttack.TabIndex = 6;
            this.lblAttack.Text = "strAttack";
            this.lblAttack.Visible = false;
            // 
            // txtCost
            // 
            this.txtCost.BackColor = System.Drawing.Color.Red;
            this.txtCost.ForeColor = System.Drawing.Color.White;
            this.txtCost.Location = new System.Drawing.Point(191, 294);
            this.txtCost.Name = "txtCost";
            this.txtCost.Size = new System.Drawing.Size(22, 20);
            this.txtCost.TabIndex = 5;
            this.txtCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblCost
            // 
            this.lblCost.AutoSize = true;
            this.lblCost.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblCost.Location = new System.Drawing.Point(10, 301);
            this.lblCost.Name = "lblCost";
            this.lblCost.Size = new System.Drawing.Size(39, 13);
            this.lblCost.TabIndex = 4;
            this.lblCost.Text = "strCost";
            // 
            // cbClass
            // 
            this.cbClass.BackColor = System.Drawing.Color.Red;
            this.cbClass.ForeColor = System.Drawing.Color.White;
            this.cbClass.FormattingEnabled = true;
            this.cbClass.Location = new System.Drawing.Point(92, 32);
            this.cbClass.Name = "cbClass";
            this.cbClass.Size = new System.Drawing.Size(122, 21);
            this.cbClass.TabIndex = 3;
            this.cbClass.Text = "[Select One]";
            // 
            // lblClass
            // 
            this.lblClass.AutoSize = true;
            this.lblClass.Location = new System.Drawing.Point(10, 34);
            this.lblClass.Name = "lblClass";
            this.lblClass.Size = new System.Drawing.Size(43, 13);
            this.lblClass.TabIndex = 2;
            this.lblClass.Text = "strClass";
            // 
            // cbType
            // 
            this.cbType.BackColor = System.Drawing.Color.Red;
            this.cbType.ForeColor = System.Drawing.Color.White;
            this.cbType.FormattingEnabled = true;
            this.cbType.Location = new System.Drawing.Point(92, 58);
            this.cbType.MaxDropDownItems = 4;
            this.cbType.Name = "cbType";
            this.cbType.Size = new System.Drawing.Size(122, 21);
            this.cbType.TabIndex = 1;
            this.cbType.Text = "[Select One]";
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(10, 61);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(42, 13);
            this.lblType.TabIndex = 0;
            this.lblType.Text = "strType";
            // 
            // canvas
            // 
            this.canvas.BackColor = System.Drawing.Color.Transparent;
            this.canvas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.canvas.ErrorImage = null;
            this.canvas.InitialImage = null;
            this.canvas.Location = new System.Drawing.Point(12, 12);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(380, 550);
            this.canvas.TabIndex = 1;
            this.canvas.TabStop = false;
            // 
            // lblDefaultPicture
            // 
            this.lblDefaultPicture.AutoSize = true;
            this.lblDefaultPicture.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblDefaultPicture.Location = new System.Drawing.Point(10, 373);
            this.lblDefaultPicture.Name = "lblDefaultPicture";
            this.lblDefaultPicture.Size = new System.Drawing.Size(85, 13);
            this.lblDefaultPicture.TabIndex = 27;
            this.lblDefaultPicture.Text = "strDefaultPicture";
            // 
            // cbDefaultPicture
            // 
            this.cbDefaultPicture.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbDefaultPicture.Location = new System.Drawing.Point(198, 373);
            this.cbDefaultPicture.Margin = new System.Windows.Forms.Padding(0);
            this.cbDefaultPicture.Name = "cbDefaultPicture";
            this.cbDefaultPicture.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cbDefaultPicture.Size = new System.Drawing.Size(14, 14);
            this.cbDefaultPicture.TabIndex = 28;
            this.cbDefaultPicture.UseVisualStyleBackColor = false;
            // 
            // CustomForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(650, 579);
            this.Controls.Add(this.customPanel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(200, 200);
            this.Name = "CustomForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Create Custom CardGraphic";
            this.TopMost = true;
            this.customPanel.ResumeLayout(false);
            this.customSubPanel.ResumeLayout(false);
            this.customSubPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel customPanel;
        private System.Windows.Forms.Panel customSubPanel;
        private System.Windows.Forms.Button btBackCustom;
        private System.Windows.Forms.Button btImage;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.ComboBox cbRace;
        private System.Windows.Forms.Label lblRace;
        private System.Windows.Forms.ComboBox cbQuality;
        private System.Windows.Forms.Label lblQuality;
        private System.Windows.Forms.Button btUpdate;
        private System.Windows.Forms.ComboBox cbSet;
        private System.Windows.Forms.Label lblSet;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtHealth;
        private System.Windows.Forms.Label lblHealth;
        private System.Windows.Forms.TextBox txtAttack;
        private System.Windows.Forms.Label lblAttack;
        private System.Windows.Forms.TextBox txtCost;
        private System.Windows.Forms.Label lblCost;
        private System.Windows.Forms.ComboBox cbClass;
        private System.Windows.Forms.Label lblClass;
        private System.Windows.Forms.ComboBox cbType;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.PictureBox canvas;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Label lblFilename;
        private System.Windows.Forms.Button btClearFilename;
        private System.Windows.Forms.Label lblDefaultPicture;
        private System.Windows.Forms.CheckBox cbDefaultPicture;
    }
}