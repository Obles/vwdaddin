using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Office.Interop.Visio;
using System.Diagnostics;
using System.IO;

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
                //TODO синхронизация документов, если это возможно
            }
        }

        private void btnSelectWord_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = WordFilter + "|" + AllFilter;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                WordPath.Text = openFileDialog.FileName;
                //TODO синхронизация документов, если это возможно
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
                // создание документа
                // Переделать, так как если пользователь нажмет отмену, то файл все равно создается
                if (File.Exists("EmptyDoc.docx"))
                {
                    File.Copy("EmptyDoc.docx", WordPath.Text);
                }
                else
                {
                    // MAYBE - Manually generate empty file
                }
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