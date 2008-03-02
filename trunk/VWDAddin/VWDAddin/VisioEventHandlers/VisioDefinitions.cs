using System;
using System.Collections.Generic;
using System.Text;

namespace VWDAddin
{
    public class VisioDefinitions
    {
        public enum VISIO_EVENT_TYPES
        {
            CLASS_ADDED,
            CLASS_DELETED,
            CLASS_NAME_CHANGED,
            CLASS_ATTR_CHANGED,

            ASSOCIATION_ADDED,
            ASSOCIATION_CONNECTED,
            ASSOCIATION_DISCONNECTED,
            ASSOCIATION_DELETED,
            ASSOCIATION_NAME_CHANGED,
            ASSOCIATION_END_NAME_CHANGED,
            ASSOCIATION_MULTIPLICITY_CHANGED,

            COMPOSITION_ADDED,
            COMPOSITION_DELETED,
            COMPOSITION_NAME_CHANGED,
            COMPOSITION_END_NAME_CHANGED,
            COMPOSITION_MULTIPLICITY_CHANGED,
        }
    }
}
