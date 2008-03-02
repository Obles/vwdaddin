using System;
using System.Collections.Generic;
using System.Text;
using EventTypes = VWDAddin.VisioDefinitions.VISIO_EVENT_TYPES;

namespace VWDAddin
{
    public abstract class BaseEvent
    {
        protected EventTypes m_type;

        public BaseEvent(EventTypes type)
        {
            m_type = type;
        }
    }
}
