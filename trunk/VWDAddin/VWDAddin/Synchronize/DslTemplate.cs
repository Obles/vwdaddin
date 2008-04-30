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
        private const String DefaultProduct = "Language";
        private String[] TextFilesExtensions = {
            ".cs", ".tt", ".csproj", "mydsl3", ".vstemplate", ".resx", ".dsl", ".dsl.diagram"
        };

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
            get { return RootPath; }
        }

        public String DslPath
        {
            get { return BasePath + @"\Dsl\DslDefinition.dsl"; }
        }

        public String TemplatePath
        {
            get { return Environment.GetFolderPath(Environment.SpecialFolder.Templates) + @"\DslTemplate"; }
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
                if (!dir.EndsWith(@"\.svn"))
                {
                    CreateDirectory(dir.Substring(TemplatePath.Length));
                    CreateFileSystem(dir);
                }
            }
            foreach (String file in Directory.GetFiles(root))
            {
                CreateFile(file.Substring(TemplatePath.Length));
            }
        }

        private void CreateDirectory(String dir)
        {
            Trace.WriteLine("Creating Dir " + dir);
            Directory.CreateDirectory(BasePath + dir);
        }

        private void CreateFile(String file)
        {
            String filelower = file.ToLower();
            foreach (String ext in TextFilesExtensions)
            {
                if (filelower.EndsWith(ext))
                {
                    CreateTextFile(file);
                    return;
                }
            }                
            CreateBinaryFile(file);
        }

        private void CreateTextFile(String file)
        {
            Trace.WriteLine("Creating Txt " + file);
            String f = File.ReadAllText(TemplatePath + file);

            f = f.Replace("<?Company?>", Company).Replace("<?Product?>", Product);

            File.WriteAllText(BasePath + file.Replace("%Product%", Product).Replace("%Company%", Company), f);
        }

        private void CreateBinaryFile(String file)
        {
            Trace.WriteLine("Creating Bin " + file);
            File.Copy(TemplatePath + file, BasePath + file.Replace("%Product%", Product).Replace("%Company%", Company));
        }
    }
}
