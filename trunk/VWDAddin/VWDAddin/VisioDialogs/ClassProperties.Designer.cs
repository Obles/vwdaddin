using Microsoft.Office.Interop.Visio;
using VWDAddin.VisioWrapper;
namespace VWDAddin
{
    partial class ClassProperties
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
            this.ClassNameTextBox = new System.Windows.Forms.TextBox();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.OKButton = new System.Windows.Forms.Button();
            this.AttrListBox = new System.Windows.Forms.ListBox();
            this.AddAttrBtn = new System.Windows.Forms.Button();
            this.RemoveAttrBtn = new System.Windows.Forms.Button();
            this.AttributesGroupBox = new System.Windows.Forms.GroupBox();
            this.ClassNameGroupBox = new System.Windows.Forms.GroupBox();
            this.colorBox = new System.Windows.Forms.PictureBox();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.ClassDSLName = new System.Windows.Forms.GroupBox();
            this.ClassDSLNameTextBox = new System.Windows.Forms.TextBox();
            this.AttributesGroupBox.SuspendLayout();
            this.ClassNameGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.colorBox)).BeginInit();
            this.ClassDSLName.SuspendLayout();
            this.SuspendLayout();
            // 
            // ClassNameTextBox
            // 
            this.ClassNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ClassNameTextBox.Location = new System.Drawing.Point(6, 19);
            this.ClassNameTextBox.Name = "ClassNameTextBox";
            this.ClassNameTextBox.Size = new System.Drawing.Size(168, 20);
            this.ClassNameTextBox.TabIndex = 0;
            // 
            // CancelBtn
            // 
            this.CancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelBtn.Location = new System.Drawing.Point(146, 256);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(75, 23);
            this.CancelBtn.TabIndex = 6;
            this.CancelBtn.Text = "Cancel";
            this.CancelBtn.UseVisualStyleBackColor = true;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // OKButton
            // 
            this.OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.OKButton.Location = new System.Drawing.Point(12, 255);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 5;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // AttrListBox
            // 
            this.AttrListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.AttrListBox.FormattingEnabled = true;
            this.AttrListBox.HorizontalScrollbar = true;
            this.AttrListBox.Location = new System.Drawing.Point(6, 19);
            this.AttrListBox.Name = "AttrListBox";
            this.AttrListBox.Size = new System.Drawing.Size(117, 82);
            this.AttrListBox.TabIndex = 2;
            this.AttrListBox.DoubleClick += new System.EventHandler(this.AtrrListBox_DoubleClick);
            // 
            // AddAttrBtn
            // 
            this.AddAttrBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AddAttrBtn.Location = new System.Drawing.Point(128, 19);
            this.AddAttrBtn.Name = "AddAttrBtn";
            this.AddAttrBtn.Size = new System.Drawing.Size(75, 23);
            this.AddAttrBtn.TabIndex = 3;
            this.AddAttrBtn.Text = "Add";
            this.AddAttrBtn.UseVisualStyleBackColor = true;
            this.AddAttrBtn.Click += new System.EventHandler(this.AddAttrBtn_Click);
            // 
            // RemoveAttrBtn
            // 
            this.RemoveAttrBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RemoveAttrBtn.Location = new System.Drawing.Point(128, 48);
            this.RemoveAttrBtn.Name = "RemoveAttrBtn";
            this.RemoveAttrBtn.Size = new System.Drawing.Size(75, 23);
            this.RemoveAttrBtn.TabIndex = 4;
            this.RemoveAttrBtn.Text = "Remove";
            this.RemoveAttrBtn.UseVisualStyleBackColor = true;
            this.RemoveAttrBtn.Click += new System.EventHandler(this.RemoveAttrBtn_Click);
            // 
            // AttributesGroupBox
            // 
            this.AttributesGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.AttributesGroupBox.Controls.Add(this.AttrListBox);
            this.AttributesGroupBox.Controls.Add(this.RemoveAttrBtn);
            this.AttributesGroupBox.Controls.Add(this.AddAttrBtn);
            this.AttributesGroupBox.Location = new System.Drawing.Point(12, 134);
            this.AttributesGroupBox.Name = "AttributesGroupBox";
            this.AttributesGroupBox.Size = new System.Drawing.Size(209, 115);
            this.AttributesGroupBox.TabIndex = 2;
            this.AttributesGroupBox.TabStop = false;
            this.AttributesGroupBox.Text = "Attributes";
            // 
            // ClassNameGroupBox
            // 
            this.ClassNameGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ClassNameGroupBox.Controls.Add(this.colorBox);
            this.ClassNameGroupBox.Controls.Add(this.ClassNameTextBox);
            this.ClassNameGroupBox.Location = new System.Drawing.Point(12, 12);
            this.ClassNameGroupBox.Name = "ClassNameGroupBox";
            this.ClassNameGroupBox.Size = new System.Drawing.Size(209, 55);
            this.ClassNameGroupBox.TabIndex = 0;
            this.ClassNameGroupBox.TabStop = false;
            this.ClassNameGroupBox.Text = "Class name";
            // 
            // colorBox
            // 
            this.colorBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.colorBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.colorBox.Location = new System.Drawing.Point(180, 19);
            this.colorBox.Name = "colorBox";
            this.colorBox.Size = new System.Drawing.Size(25, 20);
            this.colorBox.TabIndex = 2;
            this.colorBox.TabStop = false;
            this.colorBox.Click += new System.EventHandler(this.colorBox_Click);
            // 
            // ClassDSLName
            // 
            this.ClassDSLName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ClassDSLName.Controls.Add(this.ClassDSLNameTextBox);
            this.ClassDSLName.Location = new System.Drawing.Point(12, 73);
            this.ClassDSLName.Name = "ClassDSLName";
            this.ClassDSLName.Size = new System.Drawing.Size(209, 55);
            this.ClassDSLName.TabIndex = 1;
            this.ClassDSLName.TabStop = false;
            this.ClassDSLName.Text = "Class DSL name";
            // 
            // ClassDSLNameTextBox
            // 
            this.ClassDSLNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ClassDSLNameTextBox.Location = new System.Drawing.Point(6, 19);
            this.ClassDSLNameTextBox.Name = "ClassDSLNameTextBox";
            this.ClassDSLNameTextBox.Size = new System.Drawing.Size(197, 20);
            this.ClassDSLNameTextBox.TabIndex = 1;
            // 
            // ClassProperties
            // 
            this.AcceptButton = this.OKButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelBtn;
            this.ClientSize = new System.Drawing.Size(233, 291);
            this.Controls.Add(this.ClassDSLName);
            this.Controls.Add(this.ClassNameGroupBox);
            this.Controls.Add(this.AttributesGroupBox);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.CancelBtn);
            this.Name = "ClassProperties";
            this.Text = "Class Properties";
            this.AttributesGroupBox.ResumeLayout(false);
            this.ClassNameGroupBox.ResumeLayout(false);
            this.ClassNameGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.colorBox)).EndInit();
            this.ClassDSLName.ResumeLayout(false);
            this.ClassDSLName.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox ClassNameTextBox;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.Button OKButton;
        private VisioClass m_shape;
        private System.Windows.Forms.ListBox AttrListBox;
        private System.Windows.Forms.Button AddAttrBtn;
        private System.Windows.Forms.Button RemoveAttrBtn;
        private System.Windows.Forms.GroupBox AttributesGroupBox;
        private System.Windows.Forms.GroupBox ClassNameGroupBox;
        private System.Windows.Forms.PictureBox colorBox;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.GroupBox ClassDSLName;
        private System.Windows.Forms.TextBox ClassDSLNameTextBox;
    }
}