namespace VWDAddin
{
    partial class DocumentProperties
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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSelectDSL = new System.Windows.Forms.Button();
            this.DSLPath = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnCreateDSL = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.WordPath = new System.Windows.Forms.TextBox();
            this.btnSelectWord = new System.Windows.Forms.Button();
            this.btnCreateWord = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(324, 131);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(405, 131);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "������";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnCreateDSL);
            this.groupBox1.Controls.Add(this.DSLPath);
            this.groupBox1.Controls.Add(this.btnSelectDSL);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(468, 50);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "DSL Tools";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "���� � dsl-�������:";
            // 
            // btnSelectDSL
            // 
            this.btnSelectDSL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectDSL.Location = new System.Drawing.Point(355, 19);
            this.btnSelectDSL.Name = "btnSelectDSL";
            this.btnSelectDSL.Size = new System.Drawing.Size(26, 20);
            this.btnSelectDSL.TabIndex = 1;
            this.btnSelectDSL.Text = "...";
            this.btnSelectDSL.UseVisualStyleBackColor = true;
            this.btnSelectDSL.Click += new System.EventHandler(this.btnSelectDSL_Click);
            // 
            // DSLPath
            // 
            this.DSLPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.DSLPath.Location = new System.Drawing.Point(114, 19);
            this.DSLPath.Name = "DSLPath";
            this.DSLPath.Size = new System.Drawing.Size(235, 20);
            this.DSLPath.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btnCreateWord);
            this.groupBox2.Controls.Add(this.btnSelectWord);
            this.groupBox2.Controls.Add(this.WordPath);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(12, 68);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(468, 50);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Word";
            // 
            // btnCreateDSL
            // 
            this.btnCreateDSL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreateDSL.Location = new System.Drawing.Point(387, 19);
            this.btnCreateDSL.Name = "btnCreateDSL";
            this.btnCreateDSL.Size = new System.Drawing.Size(75, 20);
            this.btnCreateDSL.TabIndex = 2;
            this.btnCreateDSL.Text = "�������";
            this.btnCreateDSL.UseVisualStyleBackColor = true;
            this.btnCreateDSL.Click += new System.EventHandler(this.btnCreateDSL_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "���� � ���������:";
            // 
            // WordPath
            // 
            this.WordPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.WordPath.Location = new System.Drawing.Point(114, 19);
            this.WordPath.Name = "WordPath";
            this.WordPath.Size = new System.Drawing.Size(235, 20);
            this.WordPath.TabIndex = 3;
            // 
            // btnSelectWord
            // 
            this.btnSelectWord.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectWord.Location = new System.Drawing.Point(355, 18);
            this.btnSelectWord.Name = "btnSelectWord";
            this.btnSelectWord.Size = new System.Drawing.Size(26, 20);
            this.btnSelectWord.TabIndex = 4;
            this.btnSelectWord.Text = "...";
            this.btnSelectWord.UseVisualStyleBackColor = true;
            this.btnSelectWord.Click += new System.EventHandler(this.btnSelectWord_Click);
            // 
            // btnCreateWord
            // 
            this.btnCreateWord.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreateWord.Location = new System.Drawing.Point(387, 18);
            this.btnCreateWord.Name = "btnCreateWord";
            this.btnCreateWord.Size = new System.Drawing.Size(75, 20);
            this.btnCreateWord.TabIndex = 5;
            this.btnCreateWord.Text = "�������";
            this.btnCreateWord.UseVisualStyleBackColor = true;
            this.btnCreateWord.Click += new System.EventHandler(this.btnCreateWord_Click);
            // 
            // DocumentProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 166);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.MinimumSize = new System.Drawing.Size(500, 200);
            this.Name = "DocumentProperties";
            this.Text = "�������� ���������";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox DSLPath;
        private System.Windows.Forms.Button btnSelectDSL;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnCreateDSL;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCreateWord;
        private System.Windows.Forms.Button btnSelectWord;
        private System.Windows.Forms.TextBox WordPath;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;

    }
}