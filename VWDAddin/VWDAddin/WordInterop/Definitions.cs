using System;
using System.Collections.Generic;
using System.Text;

namespace VWDAddin
{
    public class Definitions
    {
        public enum ASSOCIATION_TYPES
        {
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
        public const string CLASS_ASSOC_NAME_PREFIX =       "Ассоциация: "; // WARNING! Don't change this line! These two lines must have same length. Current length is 12
        public const string CLASS_COMPOSITION_NAME_PREFIX = "Композиция: "; // WARNING! Don't change this line! These two lines must have same length. Current length is 12
        public const string CLASS_ASSOC_DESCR_PREFIX = "Описание: ";
        public const string CLASS_ASSOC_NAME_END_PREFIX = "Имя конца: ";
        public const string CLASS_ASSOC_MULT_PREFIX = "Множественность: ";
        public const string CLASS_ASSOC_TYPE_PREFIX = "Тип: ";
        public const string CLASS_ATTR_PART_PREFIX = "Атрибуты";
        public const string CLASS_ASSOC_PART_PREFIX = "Ассоциации";

        public const string CLASS_NAME_STYLE = "Heading1";
        public const string CLASS_PARENT_STYLE = "Heading3";
        public const string CLASS_NAME_DESCR_STYLE = "Normal";
        public const string CLASS_ATTR_NAME_STYLE = "Heading4";
        public const string CLASS_ATTR_NAME_DESCR_STYLE = "Normal";
        public const string CLASS_ASSOC_NAME_STYLE = "Heading4";
        public const string CLASS_ASSOC_DESCR_STYLE = "Normal";
        public const string CLASS_ASSOC_NAME_END_STYLE = "Normal";
        public const string CLASS_ASSOC_MULT_STYLE = "Normal";
        public const string CLASS_ASSOC_TYPE_STYLE = "Normal";
        public const string CLASS_DEFAULT_STYLE = "Normal";
        public const string CLASS_ATTR_PART_STYLE = "Heading2";
        public const string CLASS_ASSOC_PART_STYLE = "Heading2";

        public const string CLASS = "class";
        public const string CLASS_NAME = "class_name";
        public const string CLASS_PARENT = "class_parent";
        public const string CLASS_DESCR = "class_descr";
        public const string CLASS_ATTR_PART = "attr_part";
        public const string CLASS_ATTR_SECTION = "attr_section";
        public const string CLASS_ATTR_NAME = "attr_name";
        public const string CLASS_ATTR_DESCR = "attr_descr";
        public const string CLASS_ASSOC_PART = "assoc_part";
        public const string CLASS_ASSOC_SECTION = "assoc_section";
        public const string CLASS_ASSOC_NAME = "assoc_name";
        public const string CLASS_ASSOC_DESCR = "assoc_descr";
        public const string CLASS_ASSOC_NAME_END = "assoc_name_end";
        public const string CLASS_ASSOC_MULT = "assoc_mult";
        public const string CLASS_ASSOC_TYPE = "assoc_type";

        public const string ATTR_GUID = "GUID";
        public const string ATTR_CONNECTION_TYPE = "CONNECTION_TYPE";
    }
}
