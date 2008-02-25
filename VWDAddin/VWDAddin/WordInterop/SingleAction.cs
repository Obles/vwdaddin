using System;
using System.Collections.Generic;
using System.Text;

namespace VWDAddin {
  public class SingleAction {
    public SingleAction(Definitions.ACTION_TYPES actionType, int id, string mainName, string attributes,
                        Definitions.ASSOCIATION_TYPES associationType, string endName, string multiplicity,
                        int assocID, double toEnd) {
      try {
        m_actionType = actionType;
        m_attributes = attributes;
        m_mainName = mainName;
        m_associationType = associationType;
        m_endName = endName;
        m_multiplicity = multiplicity;
        m_objectID = id;
        m_assocEndID = assocID;
        m_toEnd = toEnd;        
      }
      catch (Exception err) {
        int abc;
      }
    }

    public Definitions.ACTION_TYPES m_actionType;
    public Definitions.ASSOCIATION_TYPES m_associationType;
    public string m_attributes;       //for attributes
    public string m_mainName;         //for all
    public string m_endName;          //for associations   
    public string m_multiplicity;     //for associations   
    public int m_objectID;            //for all
    public int m_assocEndID;             //for associations  
    public double m_toEnd;    
  }
}
