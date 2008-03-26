using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Office.Interop.Visio;
using System.Diagnostics;
using VWDAddin.VisioLogger;
using VWDAddin.DslWrapper;
using VWDAddin.VisioWrapper;

namespace VWDAddin.Synchronize
{
    public class VisioSync
    {
        private Logger Logger;

        public VisioSync(Logger Logger)
        {
            this.Logger = Logger;
        }

        public void Synchronize()
        {
            //TODO сделать нормальную синхронихацию
            // сейчас это частичная генерация из Dsl-проекта
            Logger.Active = false;

            while (Logger.Document.Pages[1].Shapes.Count > 0)
            {
                Logger.Document.Pages[1].Shapes[1].Delete();
            }

            DslDocument dslDocument = new DslDocument();
            dslDocument.Load(VisioHelpers.GetDSLPath(Logger.Document));

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
    }
}
