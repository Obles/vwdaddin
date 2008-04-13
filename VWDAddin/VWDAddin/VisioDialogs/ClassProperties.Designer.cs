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
            this.label1 = new System.Windows.Forms.Label();
            this.AttrListBox = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.AddAttrBtn = new System.Windows.Forms.Button();
            this.RemoveAttrBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ClassNameTextBox
            // 
            this.ClassNameTextBox.Location = new System.Drawing.Point(12, 12);
            this.ClassNameTextBox.Name = "ClassNameTextBox";
            this.ClassNameTextBox.Size = new System.Drawing.Size(120, 20);
            this.ClassNameTextBox.TabIndex = 0;
            // 
            // CancelBtn
            // 
            this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelBtn.Location = new System.Drawing.Point(149, 166);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(75, 23);
            this.CancelBtn.TabIndex = 3;
            this.CancelBtn.Text = "Cancel";
            this.CancelBtn.UseVisualStyleBackColor = true;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(12, 166);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 2;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(153, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Class name";
            // 
            // AttrListBox
            // 
            this.AttrListBox.FormattingEnabled = true;
            this.AttrListBox.HorizontalScrollbar = true;
            this.AttrListBox.Location = new System.Drawing.Point(12, 60);
            this.AttrListBox.Name = "AttrListBox";
            this.AttrListBox.Size = new System.Drawing.Size(120, 82);
            this.AttrListBox.TabIndex = 1;
            this.AttrListBox.DoubleClick += new System.EventHandler(this.AtrrListBox_DoubleClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(153, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Attributes";
            // 
            // AddAttrBtn
            // 
            this.AddAttrBtn.Location = new System.Drawing.Point(149, 90);
            this.AddAttrBtn.Name = "AddAttrBtn";
            this.AddAttrBtn.Size = new System.Drawing.Size(75, 23);
            this.AddAttrBtn.TabIndex = 6;
            this.AddAttrBtn.Text = "Add";
            this.AddAttrBtn.UseVisualStyleBackColor = true;
            this.AddAttrBtn.Click += new System.EventHandler(this.AddAttrBtn_Click);
            // 
            // RemoveAttrBtn
            // 
            this.RemoveAttrBtn.Location = new System.Drawing.Point(149, 119);
            this.RemoveAttrBtn.Name = "RemoveAttrBtn";
            this.RemoveAttrBtn.Size = new System.Drawing.Size(75, 23);
            this.RemoveAttrBtn.TabIndex = 7;
            this.RemoveAttrBtn.Text = "Remove";
            this.RemoveAttrBtn.UseVisualStyleBackColor = true;
            this.RemoveAttrBtn.Click += new System.EventHandler(this.RemoveAttrBtn_Click);
            // 
            // ClassProperties
            // 
            this.AcceptButton = this.OKButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelBtn;
            this.ClientSize = new System.Drawing.Size(236, 195);
            this.Controls.Add(this.RemoveAttrBtn);
            this.Controls.Add(this.AddAttrBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.AttrListBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.ClassNameTextBox);
            this.Name = "ClassProperties";
            this.Text = "Class Properties";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ClassNameTextBox;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Label label1;
        private VisioShape m_shape;
        private System.Windows.Forms.ListBox AttrListBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button AddAttrBtn;
        private System.Windows.Forms.Button RemoveAttrBtn;
    }
}