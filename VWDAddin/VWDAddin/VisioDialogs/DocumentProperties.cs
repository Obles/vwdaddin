using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Office.Interop.Visio;
using System.Diagnostics;

namespace VWDAddin
{
    public partial class DocumentProperties : Form
    {
        public const String MarkerName = "ShowDocumentProperties";

        private const String DSLFilter = "DSL Tools Project (*.dsl)|*.dsl";
        private const String WordFilter = "Word Document (*.docx)|*.docx";
        private const String AllFilter = "All Files (*.*)|*.*";

        public DocumentProperties(Document Document)
        {
            InitializeComponent();
            this.Document = Document;

            DSLPath.Text = VisioHelpers.GetDSLPath(Document);
            WordPath.Text = VisioHelpers.GetWordPath(Document);
        }

        private Document Document;

        private void btnSelectDSL_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = DSLFilter + "|" + AllFilter;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                DSLPath.Text = openFileDialog.FileName;
            }
        }

        private void btnSelectWord_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = WordFilter + "|" + AllFilter;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                WordPath.Text = openFileDialog.FileName;
            }
        }

        private void btnCreateDSL_Click(object sender, EventArgs e)
        {
            saveFileDialog.Filter = DSLFilter;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                DSLPath.Text = saveFileDialog.FileName;
                //TODO создание dsl-проекта
            }
        }

        private void btnCreateWord_Click(object sender, EventArgs e)
        {
            saveFileDialog.Filter = WordFilter;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                WordPath.Text = saveFileDialog.FileName;
                //TODO создание документа
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            VisioHelpers.SetDSLPath(Document, DSLPath.Text);
            VisioHelpers.SetWordPath(Document, WordPath.Text);
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}