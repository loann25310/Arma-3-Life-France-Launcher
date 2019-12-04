using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace ALF
{
    public class Main
    {
        public class FileALF
        {
            public string path;
            public string hash;
            public string url;
        }

        public class ConfigALF
        {
            private string AppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            private string armaPath = "";
            private string customColor = "";
            private bool darkMode = false;

            public ConfigALF()
            {
                if (!Directory.Exists(AppData + "\\ALF_Launcher"))
                {
                    Directory.CreateDirectory(AppData + "\\ALF_Launcher");
                }
                AppData += "\\ALF_Launcher\\";

                if (!File.Exists(AppData + "options.json"))
                {
                    using (OpenFileDialog openFileDialog = new OpenFileDialog())
                    {
                        openFileDialog.Filter = "Arma 3 x64|arma3_x64.exe|Arma 3|arma3.exe";
                        openFileDialog.InitialDirectory = @"C:\Program Files (x86)\Steam\steamapps\common\Arma 3\";
                        while (openFileDialog.ShowDialog() != DialogResult.OK) { }
                        FileInfo fileInfo = new FileInfo(openFileDialog.FileName);
                        armaPath = fileInfo.Directory.FullName + @"\";
                    }
                    SaveOptionFile();
                }
                else
                {
                    LoadOptionFile();
                }
            }

            public void SaveFileList(List<FileALF> localFiles)
            {
                string result = JsonConvert.SerializeObject(localFiles);
                File.WriteAllText(AppData + "files_list.json", result);
            }
            public List<FileALF> GetFileList()
            {
                try
                {
                    string result = File.ReadAllText(AppData + "files_list.json");
                    return JsonConvert.DeserializeObject<List<FileALF>>(result);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }

            public void SaveOptionFile()
            {
                ConfigFileALF fileALF = new ConfigFileALF();
                fileALF.path = armaPath;
                fileALF.customColor = customColor;
                if (darkMode)
                {
                    fileALF.themeMode = "dark";
                }
                else
                {
                    fileALF.themeMode = "light";
                }
                File.WriteAllText(AppData + "options.json", JsonConvert.SerializeObject(fileALF));
            }

            private void LoadOptionFile()
            {
                ConfigFileALF fileALF = JsonConvert.DeserializeObject<ConfigFileALF>(File.ReadAllText(AppData + "options.json"));
                armaPath = fileALF.path;
                customColor = fileALF.customColor;
                darkMode = (fileALF.themeMode == "dark");
            }

            public string GetArmaPath()
            {
                return armaPath;
            }

            public void SetArmaPath(string newPath)
            {
                armaPath = newPath;
            }

            public void SetCustomColor(string value)
            {
                customColor = value;
            }

            public string GetCustomColor()
            {
                return customColor;
            }

            public void SetTheme(bool IsDark)
            {
                darkMode = IsDark;
            }

            public bool GetIsDarkTheme()
            {
                return darkMode;
            }
        }

        public class ConfigFileALF
        {
            public string path;
            public string customColor;
            public string themeMode;
        }
    }
}
