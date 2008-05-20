using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace VWDAddin
{
    public partial class ChangeValue : Form
    {
        public ChangeValue()
        {
            InitializeComponent();
        }

        public ChangeValue(ListBox listBox, bool clickedOnListBox)
        {
            InitializeComponent();
            m_listBox = listBox;
            m_clickedOnListBox = clickedOnListBox;
            ValueTextBox.Text = listBox.Text;
            ValueTextBox.SelectAll();
        }

        private void OKBtn_Click(object sender, EventArgs e)
        {
            if (m_listBox.Items.IndexOf(ValueTextBox.Text) >= 0)
            {
                MessageBox.Show("Атрибут с таким именем уже существует");
                return;
            }

            if (m_clickedOnListBox)
            {
                m_listBox.Items.Insert(m_listBox.SelectedIndex, ValueTextBox.Text);
                m_listBox.Items.RemoveAt(m_listBox.SelectedIndex);
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                m_listBox.Items.Add(ValueTextBox.Text);
                this.DialogResult = DialogResult.OK;
            }
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private ListBox m_listBox;
        private bool m_clickedOnListBox;
    }
}