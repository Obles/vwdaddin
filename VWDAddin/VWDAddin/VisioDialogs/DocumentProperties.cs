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
using VWDAddin.DslWrapper;
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
                VisioHelpers.SetDSLPath(Logger.Document, DSLPath.Text);
                new VisioSync(Logger).Synchronize();
            }
        }

        private void btnSelectWord_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = WordFilter + "|" + AllFilter;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                WordPath.Text = openFileDialog.FileName;
                VisioHelpers.SetWordPath(Logger.Document, WordPath.Text);
                //TODO ������������� ����������, ���� ��� ��������
            }
        }

        private void btnCreateDSL_Click(object sender, EventArgs e)
        {
            NewDslProject NewDslProject = new NewDslProject();
            if (NewDslProject.ShowDialog(this) == DialogResult.OK)
            {
                //try
                {
                    // ������� ������ �� �������
                    DslTemplate dsl = new DslTemplate(NewDslProject.BasePath);
                    dsl.Company = NewDslProject.CompanyName;
                    dsl.Product = NewDslProject.ProductName;
                    dsl.Create();

                    // ������������� ����
                    DSLPath.Text = dsl.DslPath;
                    VisioHelpers.SetDSLPath(Logger.Document, DSLPath.Text);
                    
                    // ��������� Visio-����� � Dsl
                    Logger = Logger.LoggerManager.ResetLogger(Logger.Document);

                    Logger.DslDocument = new DslDocument();
                    Logger.DslDocument.Load(dsl.DslPath);

                    new DslSync(Logger).Synchronize();

                    // ���������� dsl-���������
                    File.WriteAllText(dsl.DslPath + ".diagram", String.Empty);
                    Logger.DslDocument.Save(dsl.DslPath);
                    Logger.DslDocument = null;
                }
                //catch (Exception err)
                //{
                //    MessageBox.Show(err.Message);
                //}
            }
        }

        private void btnCreateWord_Click(object sender, EventArgs e)
        {
            saveFileDialog.Filter = WordFilter;
            if (WordPath.Text.Length > 0 || saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                WordPath.Text = saveFileDialog.FileName;
                VisioHelpers.SetWordPath(Logger.Document, WordPath.Text);
                // �������� ���������
                // ����������, ��� ��� ���� ������������ ������ ������, �� ���� ��� ����� ���������
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}