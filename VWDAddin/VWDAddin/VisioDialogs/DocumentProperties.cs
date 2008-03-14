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
                if (File.Exists("EmptyDoc.docx"))
                {
                    File.Copy("EmptyDoc.docx", WordPath.Text, true);
                }
                else
                {
                    // MAYBE - Manually generate empty file
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (VisioHelpers.GetDSLPath(Logger.Document) != DSLPath.Text)
            {
                VisioHelpers.SetDSLPath(Logger.Document, DSLPath.Text);
                //TODO сделать нормальную синхронихацию
                // сейчас это частичная генерация из Dsl-проекта
                Logger.Active = false;

                while (Logger.Document.Pages[1].Shapes.Count > 0)
                {
                    Logger.Document.Pages[1].Shapes[1].Delete();
                }

                DslDocument dslDocument = new DslDocument();
                dslDocument.Load(DSLPath.Text);
                foreach (DomainClass dc in dslDocument.Dsl.Classes)
                {
                    VisioClass vc = new VisioClass(VisioMaster.Drop(Logger.Document, "Class"));
                    vc.GUID = dc.GUID;
                    vc.Name = dc.Xml.GetAttribute("Name");
                    String attrs = "";
                    foreach (DomainProperty prop in dc.Properties)
                    {
                        attrs += prop.Xml.GetAttribute("Name") + "\n";
                    }
                    vc.Attributes = attrs.Trim();
                    // ... ... ...
                }
                foreach (DomainClass dc in dslDocument.Dsl.Classes)
                {
                    if (dc.BaseClass != null)
                    {
                        Shape vc = VisioHelpers.GetShapeByGUID(dc.GUID, Logger.Document);
                        Shape bc = VisioHelpers.GetShapeByGUID(
                            dslDocument.Dsl.Classes[dc.BaseClass].GUID,
                            Logger.Document
                        );
                        VisioMaster.DropConnection(vc, bc, Constants.Generalization);
                    }
                }
                foreach (DomainRelationship dr in dslDocument.Dsl.Relationships)
                {
                    VisioConnector vc = new VisioConnector(VisioMaster.DropConnection(
                        VisioHelpers.GetShapeByGUID(dslDocument.Dsl.Classes[dr.Source.RolePlayer].GUID, Logger.Document),
                        VisioHelpers.GetShapeByGUID(dslDocument.Dsl.Classes[dr.Target.RolePlayer].GUID, Logger.Document),
                        (dr.IsEmbedding ? Constants.Composition : Constants.Association)
                    ));
                    vc.GUID = dr.GUID;
                    vc.Name = dr.Xml.GetAttribute("Name");
                    // ... ... ...
                }
                Logger.Active = true;
                Logger = Logger.LoggerManager.ResetLogger(Logger.Document);
            }
            VisioHelpers.SetWordPath(Logger.Document, WordPath.Text);
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

    }
}