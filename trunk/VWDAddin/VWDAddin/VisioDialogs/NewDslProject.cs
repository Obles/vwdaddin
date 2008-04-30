using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace VWDAddin
{
    public partial class NewDslProject : Form
    {
        public NewDslProject()
        {
            InitializeComponent();
        }

        private void Product_TextChanged(object sender, EventArgs e)
        {
            Namespace.Text = CompanyName + "." + ProductName;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                Path.Text = folderBrowserDialog.SelectedPath + @"\" + ProductName;
            }
        }

        public String BasePath
        {
            get { return Path.Text; }
        }

        public new String CompanyName
        {
            get { return Translit.Encode(Company.Text); }
        }

        public new String ProductName
        {
            get { return Translit.Encode(Product.Text); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Regex regex = new Regex(@"^[a-zA-Z]\w*$");
            if (!regex.IsMatch(ProductName))
            {
                MessageBox.Show("Неверно указано имя проекта!");
            }
            else if (!regex.IsMatch(CompanyName))
            {
                MessageBox.Show("Неверно указано имя компании!");
            }
            else if (BasePath == String.Empty)
            {
                MessageBox.Show("Не указан путь к проекту!");
            }
            else if (System.IO.Directory.Exists(BasePath))
            {
                MessageBox.Show("Папка '" + BasePath + "' уже существует!");
            }
            else this.DialogResult = DialogResult.OK;
        }
    }
}