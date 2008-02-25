using Shape = Microsoft.Office.Interop.Visio.Shape;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace VWDAddin
{
    public partial class AssociationDisplayOptions : Form
    {
        public AssociationDisplayOptions()
        {
            InitializeComponent();
        }

        private void SetCheckBoxFromShape(CheckBox checkBox, Shape shape, string cellName)
        {
            if (shape.get_Cells(cellName).Formula.Equals("TRUE"))
                checkBox.Checked = false;
            else
                checkBox.Checked = true;
        }

        private void SetShapeFromCheckBox(CheckBox checkBox, Shape shape, string cellName)
        {
            if (checkBox.Checked == true)
                shape.get_Cells("HideText").Formula = "FALSE";
            else
                shape.get_Cells("HideText").Formula = "TRUE";
        }

        public AssociationDisplayOptions(Shape shape)
        {
            try
            {

                InitializeComponent();
                if (shape != null)
                {
                    m_shape = shape;
                    SetCheckBoxFromShape(DisplayName, shape, "HideText");
                    foreach (Shape childShape in shape.Shapes)
                    {
                        string type = VisioHelpers.GetShapeType(childShape);
                        switch (type)
                        {
                            case "end1_name":
                                SetCheckBoxFromShape(DisplayEnd1Name, childShape, "HideText");
                                break;
                            case "end2_name":
                                SetCheckBoxFromShape(DisplayEnd2Name, childShape, "HideText");
                                break;
                            case "end1_mp":
                                SetCheckBoxFromShape(DisplayEnd1MP, childShape, "HideText");
                                break;
                            case "end2_mp":
                                SetCheckBoxFromShape(DisplayEnd2MP, childShape, "HideText");
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                int abc = 0;
            }
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            SetShapeFromCheckBox(DisplayName, m_shape, "HideText");
            foreach (Shape childShape in m_shape.Shapes)
            {
                string type = VisioHelpers.GetShapeType(childShape);
                switch (type)
                {
                    case "end1_name":
                        SetShapeFromCheckBox(DisplayEnd1Name, childShape, "HideText");
                        break;
                    case "end2_name":
                        SetShapeFromCheckBox(DisplayEnd2Name, childShape, "HideText");
                        break;
                    case "end1_mp":
                        SetShapeFromCheckBox(DisplayEnd1MP, childShape, "HideText");
                        break;
                    case "end2_mp":
                        SetShapeFromCheckBox(DisplayEnd2MP, childShape, "HideText");
                        break;
                    default:
                        break;
                }
            }
            this.Close();
        }

        private Shape m_shape;

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}