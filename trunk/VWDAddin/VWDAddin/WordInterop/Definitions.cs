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

        public const string CLASS_NAME_PREFIX = "Class :";
        public const string CLASS_PARENT_PREFIX = "Base class :";
        public const string CLASS_NAME_DESCR_PREFIX = "Description :";
        public const string CLASS_ATTR_NAME_PREFIX = "Attribute :";
        public const string CLASS_ATTR_NAME_DESCR_PREFIX = "Description :";
        public const string CLASS_ASSOC_NAME_PREFIX = "Association :"; // WARNING! Don't change this line! These two lines must have same length. Current length is 13
        public const string CLASS_COMPOSITION_NAME_PREFIX = "Composition :"; // WARNING! Don't change this line! These two lines must have same length. Current length is 13
        public const string CLASS_ASSOC_DESCR_PREFIX = "Description :";
        public const string CLASS_ASSOC_NAME_END_PREFIX = "Target name :";
        public const string CLASS_ASSOC_MULT_PREFIX = "Multiplicity :";
        public const string CLASS_ASSOC_TYPE_PREFIX = "Type :";
        public const string CLASS_ATTR_PART_PREFIX = "Attributes";
        public const string CLASS_ASSOC_PART_PREFIX = "Associations";

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
        public const string CONTENT_TYPE_NODE = "content_type";
        public const string CONTENT_NODE = "content";
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

        public const string VALIDATION_FAILED = "Прикрепленный документ не соответствует схеме. ";
    }
}
