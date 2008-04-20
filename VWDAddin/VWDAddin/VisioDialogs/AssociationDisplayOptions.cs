using Shape = Microsoft.Office.Interop.Visio.Shape;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using VWDAddin.VisioWrapper;

namespace VWDAddin
{
    public partial class AssociationDisplayOptions : Form
    {
        public const String MarkerName = "ShowAssocDisplayOptions";

        public AssociationDisplayOptions()
        {
            InitializeComponent();
        }

        private static void SetCheckBoxFromShape(CheckBox checkBox, Shape shape, string cellName)
        {
            if (shape.get_Cells(cellName).FormulaU.Equals("TRUE"))
                checkBox.Checked = false;
            else
                checkBox.Checked = true;
        }

        private static void SetShapeFromCheckBox(CheckBox checkBox, Shape shape, string cellName)
        {
            if (checkBox.Checked == true)
                shape.get_Cells("HideText").FormulaU = "FALSE";
            else
                shape.get_Cells("HideText").FormulaU = "TRUE";
        }

        private static void SetArrowCheckBoxFromShape(CheckBox checkBox, Shape shape, string cellName)
        {
            if (shape.get_Cells(cellName).FormulaU.Equals("0"))
                checkBox.Checked = false;
            else
                checkBox.Checked = true;
        }

        private static void SetShapeFromArrowCheckBox(CheckBox checkBox, Shape shape)
        {
            if (checkBox.Checked == true)
            {
                shape.get_Cells("BeginArrow").FormulaU = "1";
                shape.get_Cells("EndArrow").FormulaU = "1";
            }
            else
            {
                shape.get_Cells("BeginArrow").FormulaU = "0";
                shape.get_Cells("EndArrow").FormulaU = "0";
            }
        }

        public AssociationDisplayOptions(Shape shape)
        {
            try
            {
                InitializeComponent();
                if (shape != null)
                {
                    m_shape = new VisioClass(shape);
                    SetCheckBoxFromShape(DisplayName, shape, "HideText");
                    SetCheckBoxFromShape(DisplayEnd1Name, m_shape["end1_name"], "HideText");
                    SetCheckBoxFromShape(DisplayEnd2Name, m_shape["end2_name"], "HideText");
                    SetCheckBoxFromShape(DisplayEnd1MP, m_shape["end1_mp"], "HideText");
                    SetCheckBoxFromShape(DisplayEnd2MP, m_shape["end2_mp"], "HideText");
                    if (VisioHelpers.GetShapeType(shape) == Constants.Composition)
                    {
                        DisplayArrows.Visible = false;
                    }
                    else
                    {
                        DisplayArrows.Visible = true;
                        SetArrowCheckBoxFromShape(DisplayArrows, m_shape.Shape, "BeginArrow");
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message + "Possible cause: Shape or child shape doesn't have user.type cell");
            }
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }        

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void DisplayName_CheckedChanged(object sender, EventArgs e)
        {
            SetShapeFromCheckBox(DisplayName, m_shape.Shape, "HideText");
        }

        private void DisplayEnd1Name_CheckedChanged(object sender, EventArgs e)
        {
            SetShapeFromCheckBox(DisplayEnd1Name, m_shape["end1_name"], "HideText");
        }

        private void DisplayEnd2Name_CheckedChanged(object sender, EventArgs e)
        {
            SetShapeFromCheckBox(DisplayEnd2Name, m_shape["end2_name"], "HideText");
        }

        private void DisplayEnd1MP_CheckedChanged(object sender, EventArgs e)
        {
            SetShapeFromCheckBox(DisplayEnd1MP, m_shape["end1_mp"], "HideText");
        }

        private void DisplayEnd2MP_CheckedChanged(object sender, EventArgs e)
        {
            SetShapeFromCheckBox(DisplayEnd2MP, m_shape["end2_mp"], "HideText");
        }

        private void DisplayArrows_CheckedChanged(object sender, EventArgs e)
        {
            SetShapeFromArrowCheckBox(DisplayArrows, m_shape.Shape);
        }
    }
}