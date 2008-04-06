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
using VWDAddin.VisioWrapper;
using VWDAddin.VisioLogger;
using VWDAddin.Synchronize;

namespace VWDAddin
{
    public partial class DocumentProperties : Form
    {
        public const String MarkerName = "ShowDocumentProperties";

        private const String DSLFilter = "DSL Tools Project (*.dsl)|*.dsl";
        private const String WordFilter = "Word Document (*.docx)|*.docx";
        private const String AllFilter = "All Files (*.*)|*.*";

        public DocumentProperties(Logger Logger)
        {
            InitializeComponent();
            this.Logger = Logger;

            DSLPath.Text = VisioHelpers.GetDSLPath(Logger.Document);
            WordPath.Text = VisioHelpers.GetWordPath(Logger.Document);
        }

        private Logger Logger;

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
                string path = Environment.GetFolderPath(Environment.SpecialFolder.Templates) + "\\EmptyDoc.docx";
                if (File.Exists(path))
                {
                    File.Copy(path, saveFileDialog.FileName, true);
                }
                else
                {   
                    MessageBox.Show("EmptyDoc not found");                    
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (VisioHelpers.GetDSLPath(Logger.Document) != DSLPath.Text)
            {
                VisioHelpers.SetDSLPath(Logger.Document, DSLPath.Text);
                new VisioSync(Logger).Synchronize();
            }
            VisioHelpers.SetWordPath(Logger.Document, WordPath.Text);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

    }
}