using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace ArzoraLife
{
    public class Main
    {
        public class FileArzoraLife
        {
            public string path;
            public string hash;
            public string url;
            public long size;
        }

        public class ConfigArzoraLife
        {
            private string AppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            private string armaPath = "";
            private string customColor = "";
            private bool darkMode = false;

            public ConfigArzoraLife()
            {
                if (!Directory.Exists(AppData + "\\ArzoraLife_Launcher"))
                {
                    Directory.CreateDirectory(AppData + "\\ArzoraLife_Launcher");
                }
                AppData += "\\ArzoraLife_Launcher\\";

                if (!File.Exists(AppData + "options.json"))
                {
                    MessageBox.Show("Une fenetre va s'ouvrir pour verifier l'emplacement de votre jeu.", "Verification d'emplacement", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    using (OpenFileDialog openFileDialog = new OpenFileDialog())
                    {
                        openFileDialog.Filter = "Arma 3|arma3battleye.exe";
                        openFileDialog.FileName = "arma3battleye.exe";
                        openFileDialog.InitialDirectory = @"C:\Program Files (x86)\Steam\steamapps\common\Arma 3\";
                        bool lockVar = true;
                        bool ignore = false;
                        while (lockVar)
                        {
                            DialogResult result = openFileDialog.ShowDialog();

                            if(result == DialogResult.OK)
                            {
                                lockVar = false;
                            }
                            else if(result == DialogResult.Cancel)
                            {
                                DialogResult resusult1 = MessageBox.Show("Voulez vous annuler l'installation de votre launcher ?", "Annuler", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                                if(resusult1 == DialogResult.Yes)
                                {
                                    lockVar = false;
                                    ignore = true;
                                }
                            }
                        }
                        if (ignore)
                        {
                            System.Windows.Forms.Application.Exit();
                            return;
                        }
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

            public void SaveFileList(List<FileArzoraLife> localFiles)
            {
                string result = JsonConvert.SerializeObject(localFiles);
                File.WriteAllText(AppData + "files_list.json", result);
            }
            public List<FileArzoraLife> GetFileList()
            {
                try
                {
                    string result = File.ReadAllText(AppData + "files_list.json");
                    return JsonConvert.DeserializeObject<List<FileArzoraLife>>(result);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }

            public void SaveOptionFile()
            {
                ConfigFileArzoraLife fileArzoraLife = new ConfigFileArzoraLife();
                fileArzoraLife.path = armaPath;
                fileArzoraLife.customColor = customColor;
                if (darkMode)
                {
                    fileArzoraLife.themeMode = "dark";
                }
                else
                {
                    fileArzoraLife.themeMode = "light";
                }
                File.WriteAllText(AppData + "options.json", JsonConvert.SerializeObject(fileArzoraLife));
            }

            private void LoadOptionFile()
            {
                ConfigFileArzoraLife fileArzoraLife = JsonConvert.DeserializeObject<ConfigFileArzoraLife>(File.ReadAllText(AppData + "options.json"));
                armaPath = fileArzoraLife.path;
                customColor = fileArzoraLife.customColor;
                darkMode = (fileArzoraLife.themeMode == "dark");
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

            public string GetAppData()
            {
                return AppData;
            }
        }

        public class ConfigFileArzoraLife
        {
            public string path;
            public string customColor;
            public string themeMode;
        }

        public class NewsArzoraLife
        {
            public string name;
            public string text;
            public string image;
        }
    }
}
