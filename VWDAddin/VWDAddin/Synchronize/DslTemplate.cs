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
        private const String DefaultPathToSDK = @"$(ProgramFiles)\Visual Studio 2005 SDK\2006.09";
        
        private String[] TextFilesExtensions = {
            ".cs", ".tt", ".csproj", "mydsl3", ".vstemplate", ".resx", ".dsl", ".dsl.diagram"
        };

        public String Company = DefaultCompany;
        public String Product = DefaultProduct;
        public String PathToSDK = DefaultPathToSDK;
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

            TestPathToSDK();

            if (Directory.Exists(BasePath))
            {
                throw new IOException("Папка '" + BasePath + "' уже существует!");
            }
            else Directory.CreateDirectory(BasePath);

            CreateFileSystem(TemplatePath);

            Trace.Unindent();
        }

        private void TestPathToSDK()
        {
            Trace.WriteLine("Testing Path To SDK");

            String dir = PathToSDK.Replace("$(ProgramFiles)", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
            if (Directory.Exists(dir)) 
            {
                Trace.WriteLine("OK: " + dir);
                return;
            }
            dir = Directory.GetParent(dir).FullName + @"\2007.02";
            if (Directory.Exists(dir))
            {
                PathToSDK = dir.Replace(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "$(ProgramFiles)");
                Trace.WriteLine("OK: " + dir);
                return;
            }
            String[] dirs = Directory.GetDirectories(Directory.GetParent(dir).FullName);
            if (dirs.Length > 0)
            {
                dir = dirs[0];
                PathToSDK = dir.Replace(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "$(ProgramFiles)");
                Trace.WriteLine("OK: " + dir);
                return;
            }
            throw new NotImplementedException("Не найден Microsoft Visual Studio SDK");
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

            f = f.Replace("<?Company?>", Company).Replace("<?Product?>", Product).Replace("<?PathToSDK?>", PathToSDK);

            File.WriteAllText(BasePath + file.Replace("%Product%", Product).Replace("%Company%", Company), f);
        }

        private void CreateBinaryFile(String file)
        {
            Trace.WriteLine("Creating Bin " + file);
            File.Copy(TemplatePath + file, BasePath + file.Replace("%Product%", Product).Replace("%Company%", Company));
        }
    }
}
