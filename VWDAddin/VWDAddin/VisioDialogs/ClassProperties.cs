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
    public partial class ClassProperties : Form
    {
        public const String MarkerName = "ShowClassProperties";

        public ClassProperties()
        {
            InitializeComponent();
        }

        public ClassProperties(Shape shape)
        {
            InitializeComponent();
            //try
            {
                m_shape = new VisioClass(shape);
                colorBox.BackColor = m_shape.Color;
                ClassNameTextBox.Text = m_shape["class_name"].Text;
                ClassDSLNameTextBox.Text = m_shape.Name;
                m_attributes = m_shape["attr_section"].Text.Split(new Char[] { '\n' });
                AttrListBox.Items.Clear();
                foreach (string attribute in m_attributes)
                {
                    if (!attribute.Equals(string.Empty))
                        AttrListBox.Items.Add(attribute);
                }
                DSLRootClass.Checked = m_shape.IsRootClass;
            }
            //catch (Exception e)
            //{
            //    Debug.WriteLine(e.Message + "Possible cause: Shape or child shape doesn't have user.type cell");
            //}
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            //try
            {
                m_shape.Color = colorBox.BackColor;
                m_shape["class_name"].Text = ClassNameTextBox.Text;
                m_shape.Name = ClassDSLNameTextBox.Text;
                string attr_sect = string.Empty;
                foreach (object attr in AttrListBox.Items)
                {
                    if (!attr.Equals(string.Empty))
                        attr_sect += attr.ToString() + '\n';
                }
                if (!attr_sect.Equals(string.Empty))
                    m_shape["attr_section"].Text = attr_sect.Substring(0, attr_sect.Length - 1);
                else
                    m_shape["attr_section"].Text = string.Empty;
                this.DialogResult = DialogResult.OK;
            }
            //catch (Exception exception)
            //{
            //    Debug.WriteLine(exception.Message + "Possible cause: Shape or child shape doesn't have user.type cell");
            //}
        }

        private void Fill()
        {
            AttrListBox.Items.Clear();
            foreach (string attribute in m_attributes)
            {
                AttrListBox.Items.Add(attribute);
            }
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void AtrrListBox_DoubleClick(object sender, System.EventArgs e)
        {
            if (AttrListBox.SelectedIndex >= 0)
                new ChangeValue(AttrListBox, true).ShowDialog();
        }

        string[] m_attributes;

        private void AddAttrBtn_Click(object sender, EventArgs e)
        {
            new ChangeValue(AttrListBox, false).ShowDialog();
        }

        private void RemoveAttrBtn_Click(object sender, EventArgs e)
        {
            if (AttrListBox.SelectedIndex >= 0)
            {
                AttrListBox.Items.RemoveAt(AttrListBox.SelectedIndex);
            }
        }

        private void colorBox_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                colorBox.BackColor = colorDialog.Color;
            }
        }

        private void DSLRootClass_Click(object sender, EventArgs e)
        {
            VisioPage page = m_shape.Page;
            if (DSLRootClass.Checked == true)
            {
                if (page.RootClass == null ||
                    MessageBox.Show(
                        "�� ������ �������� �������� ����� �� �������?",
                        "�������� ����� ��� �����",
                        MessageBoxButtons.YesNo
                    ) == DialogResult.Yes)
                {
                    page.RootClass = m_shape;
                }
                else DSLRootClass.Checked = false;
            }
            else page.RootClass = null;
        }
    }
}