using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Office.Interop.Visio;
using VWDAddin.VisioWrapper;
using VWDAddin.DslWrapper;

namespace VWDAddin.VisioLogger.Actions
{
    public class AssociationAction : BaseAction
    {
        public AssociationAction(VisioConnector targetShape)           
        {
            Connector = targetShape.ToStaticConnector();
        }

        #region Members
        private StaticConnector _connector;
        public StaticConnector Connector
        {
            get { return _connector; }
            set { _connector = value; }
        }
        #endregion

        protected void FixRolePropertyNames(DomainRelationship Relationship)
        {
            String SourceName = Connector.Source == null ? "" : Connector.Source.Name;
            String TargetName = Connector.Target == null ? "" : Connector.Target.Name;

            String SourceText = Connector.SourceText == String.Empty ? SourceName : Connector.SourceText;
            String TargetText = Connector.TargetText == String.Empty ? TargetName : Connector.TargetText;

            DomainRole source = Relationship.Source;
            source.Xml.SetAttribute("Name", SourceName);
            source.Xml.SetAttribute("DisplayName", SourceText);
            source.Xml.SetAttribute("PropertyName", TargetName);
            source.Xml.SetAttribute("PropertyDisplayName", TargetText);

            DomainRole target = Relationship.Target;
            target.Xml.SetAttribute("Name", TargetName);
            target.Xml.SetAttribute("DisplayName", TargetText);
            target.Xml.SetAttribute("PropertyName", SourceName);
            target.Xml.SetAttribute("PropertyDisplayName", SourceText);
        }
    }
}
