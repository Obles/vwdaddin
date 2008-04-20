using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace VWDAddin
{
    public partial class DslTemplate
    {
        private const String DefaultCompany = "Company";
        private const String DefaultProduct = "Language1";

        public String Company = DefaultCompany;
        public String Product = DefaultProduct;
        public String RootPath;

        public DslTemplate(String RootPath)
        {
            this.RootPath = RootPath;
        }

        #region Paths...
        public String BasePath
        {
            get { return RootPath + @"\" + Product; }
        }

        public String DslPath
        {
            get { return BasePath + @"\Dsl\DslDefinition.dsl"; }
        }

        public String TemplatePath
        {
            get { return @"C:\Andrey\Work\vwdaddin\Template\DslTemplate"; }
        }
        #endregion

        public void Create()
        {
            Trace.WriteLine("Creating Dsl Template");
            Trace.Indent();
            if (Directory.Exists(BasePath))
            {
                throw new IOException("Папка '" + BasePath + "' уже существует!");
            }
            else Directory.CreateDirectory(BasePath);

            CreateFileSystem(TemplatePath);

            Trace.Unindent();
        }

        private void CreateFileSystem(String root)
        {
            foreach (String dir in Directory.GetDirectories(root))
            {
                CreateDirectory(dir.Substring(TemplatePath.Length));
                CreateFileSystem(dir);
            }
            foreach (String file in Directory.GetFiles(root))
            {
                CreateFile(file.Substring(TemplatePath.Length));
            }
        }

        private void CreateDirectory(String dir)
        {
            Trace.WriteLine("Creating " + dir);
            Directory.CreateDirectory(BasePath + dir);
        }

        private void CreateFile(String file)
        {
            String ext = file.Substring(file.LastIndexOf('.'));
            CreateBinaryFile(file);
        }

        private void CreateTextFile(String file)
        {
            Trace.WriteLine("Creating " + file);
            String f = File.ReadAllText(TemplatePath + file);

            //TODO обработка файла

            File.WriteAllText(BasePath + file.Replace("%Product%", Product).Replace("%Company%", Company), f);
        }

        private void CreateBinaryFile(String file)
        {
            Trace.WriteLine("Creating " + file);
            File.Copy(TemplatePath + file, BasePath + file.Replace("%Product%", Product).Replace("%Company%", Company));
        }
    }
}
