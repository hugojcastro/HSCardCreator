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
    partial class mainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainForm));
            this.btExit = new System.Windows.Forms.Button();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.btCreateCollection = new System.Windows.Forms.Button();
            this.btCreateCustom = new System.Windows.Forms.Button();
            this.mainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // btExit
            // 
            this.btExit.BackColor = System.Drawing.Color.Transparent;
            this.btExit.BackgroundImage = global::HSCardGenerator.Properties.Resources.ButtonClose24x24;
            this.btExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btExit.FlatAppearance.BorderSize = 0;
            this.btExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btExit.Location = new System.Drawing.Point(572, 4);
            this.btExit.Margin = new System.Windows.Forms.Padding(0);
            this.btExit.Name = "btExit";
            this.btExit.Size = new System.Drawing.Size(24, 24);
            this.btExit.TabIndex = 6;
            this.btExit.UseVisualStyleBackColor = false;
            // 
            // mainPanel
            // 
            this.mainPanel.BackColor = System.Drawing.Color.Transparent;
            this.mainPanel.BackgroundImage = global::HSCardGenerator.Properties.Resources.Background600x210;
            this.mainPanel.Controls.Add(this.btCreateCollection);
            this.mainPanel.Controls.Add(this.btCreateCustom);
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Margin = new System.Windows.Forms.Padding(0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(600, 210);
            this.mainPanel.TabIndex = 5;
            // 
            // btCreateCollection
            // 
            this.btCreateCollection.BackColor = System.Drawing.Color.Transparent;
            this.btCreateCollection.BackgroundImage = global::HSCardGenerator.Properties.Resources.ButtonCreateCollection200x120;
            this.btCreateCollection.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btCreateCollection.FlatAppearance.BorderSize = 0;
            this.btCreateCollection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btCreateCollection.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btCreateCollection.ForeColor = System.Drawing.Color.White;
            this.btCreateCollection.Location = new System.Drawing.Point(323, 46);
            this.btCreateCollection.Name = "btCreateCollection";
            this.btCreateCollection.Padding = new System.Windows.Forms.Padding(0, 52, 0, 0);
            this.btCreateCollection.Size = new System.Drawing.Size(200, 120);
            this.btCreateCollection.TabIndex = 1;
            this.btCreateCollection.Text = "strCreateCollection";
            this.btCreateCollection.UseVisualStyleBackColor = false;
            // 
            // btCreateCustom
            // 
            this.btCreateCustom.BackColor = System.Drawing.Color.Transparent;
            this.btCreateCustom.BackgroundImage = global::HSCardGenerator.Properties.Resources.ButtonCreateCustom150x150;
            this.btCreateCustom.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btCreateCustom.FlatAppearance.BorderSize = 0;
            this.btCreateCustom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btCreateCustom.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btCreateCustom.ForeColor = System.Drawing.Color.White;
            this.btCreateCustom.Location = new System.Drawing.Point(62, 31);
            this.btCreateCustom.Name = "btCreateCustom";
            this.btCreateCustom.Padding = new System.Windows.Forms.Padding(0, 26, 0, 0);
            this.btCreateCustom.Size = new System.Drawing.Size(150, 150);
            this.btCreateCustom.TabIndex = 0;
            this.btCreateCustom.Text = "strCreateCustom";
            this.btCreateCustom.UseVisualStyleBackColor = false;
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(600, 210);
            this.Controls.Add(this.btExit);
            this.Controls.Add(this.mainPanel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(200, 200);
            this.Name = "mainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hearthstone CardGraphic Generator v1.3";
            this.TopMost = true;
            this.mainPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Button btCreateCollection;
        private System.Windows.Forms.Button btCreateCustom;
        private System.Windows.Forms.Button btExit;
    }
}

