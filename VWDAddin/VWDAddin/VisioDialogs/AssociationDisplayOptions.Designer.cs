using Microsoft.Office.Interop.Visio;
using VWDAddin.VisioWrapper;
namespace VWDAddin
{
    partial class AssociationDisplayOptions
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
            this.DisplayName = new System.Windows.Forms.CheckBox();
            this.OKButton = new System.Windows.Forms.Button();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.DisplayEnd2Name = new System.Windows.Forms.CheckBox();
            this.DisplayEnd1Name = new System.Windows.Forms.CheckBox();
            this.DisplayEnd1MP = new System.Windows.Forms.CheckBox();
            this.DisplayEnd2MP = new System.Windows.Forms.CheckBox();
            this.DisplayArrows = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // DisplayName
            // 
            this.DisplayName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.DisplayName.AutoSize = true;
            this.DisplayName.Location = new System.Drawing.Point(12, 47);
            this.DisplayName.Name = "DisplayName";
            this.DisplayName.Size = new System.Drawing.Size(145, 17);
            this.DisplayName.TabIndex = 1;
            this.DisplayName.Text = "Display association name";
            this.DisplayName.UseVisualStyleBackColor = true;
            this.DisplayName.CheckedChanged += new System.EventHandler(this.DisplayName_CheckedChanged);
            // 
            // OKButton
            // 
            this.OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.OKButton.Location = new System.Drawing.Point(12, 151);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 6;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // CancelBtn
            // 
            this.CancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelBtn.Location = new System.Drawing.Point(217, 151);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(75, 23);
            this.CancelBtn.TabIndex = 7;
            this.CancelBtn.Text = "Cancel";
            this.CancelBtn.UseVisualStyleBackColor = true;
            this.CancelBtn.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // DisplayEnd2Name
            // 
            this.DisplayEnd2Name.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.DisplayEnd2Name.AutoSize = true;
            this.DisplayEnd2Name.Location = new System.Drawing.Point(159, 82);
            this.DisplayEnd2Name.Name = "DisplayEnd2Name";
            this.DisplayEnd2Name.Size = new System.Drawing.Size(116, 17);
            this.DisplayEnd2Name.TabIndex = 3;
            this.DisplayEnd2Name.Text = "Display end2 name";
            this.DisplayEnd2Name.UseVisualStyleBackColor = true;
            this.DisplayEnd2Name.CheckedChanged += new System.EventHandler(this.DisplayEnd2Name_CheckedChanged);
            // 
            // DisplayEnd1Name
            // 
            this.DisplayEnd1Name.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.DisplayEnd1Name.AutoSize = true;
            this.DisplayEnd1Name.Location = new System.Drawing.Point(12, 82);
            this.DisplayEnd1Name.Name = "DisplayEnd1Name";
            this.DisplayEnd1Name.Size = new System.Drawing.Size(116, 17);
            this.DisplayEnd1Name.TabIndex = 2;
            this.DisplayEnd1Name.Text = "Display end1 name";
            this.DisplayEnd1Name.UseVisualStyleBackColor = true;
            this.DisplayEnd1Name.CheckedChanged += new System.EventHandler(this.DisplayEnd1Name_CheckedChanged);
            // 
            // DisplayEnd1MP
            // 
            this.DisplayEnd1MP.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.DisplayEnd1MP.AutoSize = true;
            this.DisplayEnd1MP.Location = new System.Drawing.Point(12, 117);
            this.DisplayEnd1MP.Name = "DisplayEnd1MP";
            this.DisplayEnd1MP.Size = new System.Drawing.Size(137, 17);
            this.DisplayEnd1MP.TabIndex = 4;
            this.DisplayEnd1MP.Text = "Display end1 multiplicity";
            this.DisplayEnd1MP.UseVisualStyleBackColor = true;
            this.DisplayEnd1MP.CheckedChanged += new System.EventHandler(this.DisplayEnd1MP_CheckedChanged);
            // 
            // DisplayEnd2MP
            // 
            this.DisplayEnd2MP.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.DisplayEnd2MP.AutoSize = true;
            this.DisplayEnd2MP.Location = new System.Drawing.Point(159, 117);
            this.DisplayEnd2MP.Name = "DisplayEnd2MP";
            this.DisplayEnd2MP.Size = new System.Drawing.Size(137, 17);
            this.DisplayEnd2MP.TabIndex = 5;
            this.DisplayEnd2MP.Text = "Display end2 multiplicity";
            this.DisplayEnd2MP.UseVisualStyleBackColor = true;
            this.DisplayEnd2MP.CheckedChanged += new System.EventHandler(this.DisplayEnd2MP_CheckedChanged);
            // 
            // DisplayArrows
            // 
            this.DisplayArrows.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.DisplayArrows.AutoSize = true;
            this.DisplayArrows.Location = new System.Drawing.Point(12, 12);
            this.DisplayArrows.Name = "DisplayArrows";
            this.DisplayArrows.Size = new System.Drawing.Size(94, 17);
            this.DisplayArrows.TabIndex = 0;
            this.DisplayArrows.Text = "Display arrows";
            this.DisplayArrows.UseVisualStyleBackColor = true;
            this.DisplayArrows.CheckedChanged += new System.EventHandler(this.DisplayArrows_CheckedChanged);
            // 
            // AssociationDisplayOptions
            // 
            this.AcceptButton = this.OKButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelBtn;
            this.ClientSize = new System.Drawing.Size(304, 186);
            this.Controls.Add(this.DisplayArrows);
            this.Controls.Add(this.DisplayEnd2MP);
            this.Controls.Add(this.DisplayEnd1MP);
            this.Controls.Add(this.DisplayEnd1Name);
            this.Controls.Add(this.DisplayEnd2Name);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.DisplayName);
            this.Name = "AssociationDisplayOptions";
            this.Text = "Association Display Options";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox DisplayName;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.CheckBox DisplayEnd2Name;
        private System.Windows.Forms.CheckBox DisplayEnd1Name;
        private System.Windows.Forms.CheckBox DisplayEnd1MP;
        private System.Windows.Forms.CheckBox DisplayEnd2MP;
        private VisioShape m_shape;
        private System.Windows.Forms.CheckBox DisplayArrows;
    }
}