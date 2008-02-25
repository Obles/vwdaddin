using System;
using System.Collections.Generic;
using System.Text;

namespace VWDAddin {
  public class Definitions {
    public enum ACTION_TYPES {
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

    public enum ASSOCIATION_TYPES {
      ASSOCIATION,
      COMPOSOTION,
      NULL,
    }

    public const string WORD_PROCESSING_ML = "http://schemas.openxmlformats.org/wordprocessingml/2006/main";
    public const string WORD_XML_PREFIX = "w";
    public const string WORD_CONTENT_TYPE = "application/vnd.openxmlformats-officedocument.wordprocessingml.document.main+xml";

    public const string CLASS_NAME_PREFIX = "Класс: ";
    public const string CLASS_NAME_DESCR_PREFIX = "Описание: ";
    public const string CLASS_ATTR_NAME_PREFIX = "Атрибут: ";
    public const string CLASS_ATTR_NAME_DESCR_PREFIX = "Описание: ";
    public const string CLASS_ASSOC_NAME_PREFIX = "Ассоциация: ";
    public const string CLASS_ASSOC_DESCR_PREFIX = "Описание: ";
    public const string CLASS_ASSOC_NAME_END_PREFIX = "Имя конца: ";
    public const string CLASS_ASSOC_MULT_PREFIX = "Множественность: ";
    public const string CLASS_ASSOC_TYPE_PREFIX = "Тип: ";

    public const int MAX_ATTRIBUTES = 11;
                                             
    #region SHAPE_NAMES
    public const string CLASS_NAME = "class_name";
    #endregion
  }
}
