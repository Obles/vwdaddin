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

        public const string CLASS_NAME_PREFIX = "�����: ";
        public const string CLASS_NAME_DESCR_PREFIX = "��������: ";
        public const string CLASS_ATTR_NAME_PREFIX = "�������: ";
        public const string CLASS_ATTR_NAME_DESCR_PREFIX = "��������: ";
        public const string CLASS_ASSOC_NAME_PREFIX = "����������: ";
        public const string CLASS_ASSOC_DESCR_PREFIX = "��������: ";
        public const string CLASS_ASSOC_NAME_END_PREFIX = "��� �����: ";
        public const string CLASS_ASSOC_MULT_PREFIX = "���������������: ";
        public const string CLASS_ASSOC_TYPE_PREFIX = "���: ";


        #region SHAPE_NAMES
        public const string CLASS_NAME = "class_name";
        #endregion
    }
}
