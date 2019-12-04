using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;
using System.Xml;
using System.Net;
using System.Threading;
using Newtonsoft.Json;
using System.Diagnostics;
using MaterialSkin.Controls;
using MaterialSkin;
using System.Collections;
using Steamworks;
using static ALF.Main;

namespace Arma_Life_France_Launcher
{
    public partial class Form1 : MaterialForm
    {
        
        private string APIurl = "https://arma3lifefrance.fr/HKu7W22yf6oAcU4HL4eHez35MEu7/";
        private string BGurl = "https://i.imgur.com/xfopdlF.png";
        private string fileAPI = "ALF.xml";
        private string armaFolder = "G:\\Steam\\steamapps\\common\\Arma 3\\";
        private string modName = "@ALF";
        private string modeCompression = "lzma";
        private string remoteVersion = "";
        private string serverIP = "151.80.109.47";
        private int serverPort = 2302;
        private List<FileALF> remoteFiles;
        private List<FileALF> localFiles;
        private ConfigALF config = new ConfigALF();

        //ColorScheme
        ColorScheme redColor = new ColorScheme(Primary.Red600, Primary.Red800, Primary.Red800, Accent.Red200, TextShade.WHITE);
        ColorScheme blueColor = new ColorScheme(Primary.Blue600, Primary.Blue800, Primary.Blue800, Accent.Blue200, TextShade.WHITE);
        ColorScheme greenColor = new ColorScheme(Primary.Green600, Primary.Green800, Primary.Green800, Accent.Green200, TextShade.WHITE);
        ColorScheme pinkColor = new ColorScheme(Primary.Pink600, Primary.Pink800, Primary.Pink800, Accent.Pink200, TextShade.WHITE);
        ColorScheme lightblueColor = new ColorScheme(Primary.LightBlue600, Primary.LightBlue800, Primary.LightBlue800, Accent.LightBlue200, TextShade.WHITE);
        ColorScheme lightgreenColor = new ColorScheme(Primary.LightGreen600, Primary.LightGreen800, Primary.LightGreen800, Accent.LightGreen200, TextShade.WHITE);
        ColorScheme yellowColor = new ColorScheme(Primary.Yellow600, Primary.Yellow800, Primary.Yellow800, Accent.Yellow200, TextShade.WHITE);


        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            progressBar2.Value = 0;
            progressBar1.Maximum = 100;
            progressBar2.Maximum = 100;
            button1.Enabled = false;
            playBtn.Enabled = false;
            lookupForLocal.RunWorkerAsync("forced");
        }

        private List<string> getAllFile(string folder, string callbackFolder = "")
        {
            List<string> result = new List<string>();

            string[] directories = Directory.GetDirectories(folder);
            foreach(string dir in directories)
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(dir);
                List<string> tmp = getAllFile(dir, callbackFolder);
                foreach(string t in tmp)
                {
                    result.Add(callbackFolder + directoryInfo.Name + "/"+t);
                }
            }

            string[] files = Directory.GetFiles(folder);
            foreach (string file in files)
            {
                FileInfo info = new FileInfo(file);
                result.Add(info.Name);
            }

            return result;
        }

        private string getFileMD5(string filename)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filename))
                {
                    var hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
        }

        public string Get(string uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        private void lookupForLocal_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                string xmlData = Get(APIurl + fileAPI);

                doc.Load(new StringReader(xmlData));

                XmlNode patch = doc.ChildNodes[0].ChildNodes[1];

                modName = patch.Attributes.GetNamedItem("data").Value;
                modeCompression = patch.Attributes.GetNamedItem("mode").Value;
                remoteVersion = patch.Attributes.GetNamedItem("launcherVersion").Value;
                remoteFiles = new List<FileALF>();
                List<string> FileNameRemote = new List<string>();
                List<string> FileHashRemote = new List<string>();

                foreach (XmlNode file in patch.ChildNodes)
                {
                    FileALF tmp = new FileALF();
                    tmp.path = file.Attributes.GetNamedItem("path").Value;
                    tmp.hash = file.Attributes.GetNamedItem("hash").Value;
                    tmp.url = file.Attributes.GetNamedItem("url").Value;
                    tmp.url = null;
                    remoteFiles.Add(tmp);
                    FileNameRemote.Add(tmp.path);
                    FileHashRemote.Add(tmp.hash);
                }


                localFiles = new List<FileALF>();
                List<string> FileNameLocal = new List<string>();
                List<string> FileHashLocal = new List<string>();

                bool internalForce = false;
                if (e.Argument == null)
                {
                    if (config.GetFileList() == null)
                    {
                        internalForce = true;
                    }
                }

                if (e.Argument != null && e.Argument.ToString() == "forced" || internalForce)
                {
                    this.Invoke(new Action(() =>
                    {
                        stateLabel.Text = "Recherche des mods ...";
                    }));
                    if (!Directory.Exists(armaFolder + modName))
                    {
                        Directory.CreateDirectory(armaFolder + modName);
                    }
                    List<string> files = getAllFile(armaFolder + modName);
                    this.Invoke(new Action(() =>
                    {
                        progressBar1.Maximum = files.Count;
                        progressBar1.Value = 0;
                    }));
                    foreach (string file in files)
                    {
                        FileALF tmp = new FileALF();
                        tmp.path = file;
                        tmp.hash = getFileMD5(armaFolder + modName + "\\" + file);
                        tmp.url = null;
                        localFiles.Add(tmp);
                        FileNameLocal.Add(tmp.path);
                        FileHashLocal.Add(tmp.hash);
                        this.Invoke(new Action(() =>
                        {
                            progressBar1.Value++;
                        }));
                    }
                }
                else
                {
                    localFiles = config.GetFileList();
                    this.Invoke(new Action(() =>
                    {
                        progressBar1.Maximum = localFiles.Count;
                        progressBar1.Value = 0;
                    }));
                    foreach (FileALF file in localFiles)
                    {
                        FileNameLocal.Add(file.path);
                        FileHashLocal.Add(file.hash);
                        this.Invoke(new Action(() =>
                        {
                            progressBar1.Value++;
                        }));
                    }
                }

                List<string> HaveToDelete = FileNameLocal.Except(FileNameRemote).ToList();
                foreach (string file in HaveToDelete)
                {
                    File.Delete(armaFolder + modName + "\\" + file);
                    localFiles.Remove(localFiles.FirstOrDefault(o => o.path == file));
                }


                remoteFiles = new List<FileALF>();
                foreach (XmlNode file in patch.ChildNodes)
                {
                    FileALF tmp = new FileALF();
                    tmp.path = file.Attributes.GetNamedItem("path").Value;
                    tmp.hash = file.Attributes.GetNamedItem("hash").Value;
                    tmp.url = file.Attributes.GetNamedItem("url").Value;
                    remoteFiles.Add(tmp);
                    FileNameRemote.Add(tmp.path);
                    FileHashRemote.Add(tmp.hash);
                }

                List<FileALF> HaveToDownload = new List<FileALF>();
                foreach (FileALF remoteFile in remoteFiles)
                {
                    if (localFiles.FirstOrDefault(o => o.path == remoteFile.path) == null)
                    {
                        HaveToDownload.Add(remoteFile);
                    }
                    else if (localFiles.FirstOrDefault(o => o.hash == remoteFile.hash) == null)
                    {
                        HaveToDownload.Add(remoteFile);
                    }
                }


                if (HaveToDownload.Count == 0)
                {
                    localFiles = remoteFiles;
                    config.SaveFileList(localFiles);
                    this.Invoke(new Action(() =>
                    {
                        button1.Enabled = true;
                        playBtn.Enabled = true;
                        stateLabel.Text = "ModPack à jour";
                    }));
                }
                else
                {
                    int c = 0;
                    int total = HaveToDownload.Count;
                    foreach (FileALF file in HaveToDownload)
                    {
                        c++;
                        this.Invoke(new Action(() =>
                        {
                            stateLabel.Text = "Téléchargement de " + c + "/" + total + " mods";
                            progressBar1.Maximum = total;
                            progressBar1.Value = c;
                        }));
                        DownloadGamefile DGF = new DownloadGamefile();
                        FileInfo fileInfo = new FileInfo(armaFolder + modName + "\\" + file.path);
                        if (!Directory.Exists(fileInfo.Directory.FullName))
                        {
                            Directory.CreateDirectory(fileInfo.Directory.FullName);
                        }
                        if (!File.Exists(armaFolder + modName + "\\" + file.path))
                        {
                            File.Delete(armaFolder + modName + "\\" + file.path);
                        }
                        string dlurl = APIurl + file.url;
                        DGF.DownloadFile(dlurl, armaFolder + modName + "\\" + file.path + "." + modeCompression);
                        while (!DGF.DownloadCompleted.finish)
                        {
                            this.Invoke(new Action(() =>
                            {
                                FileCallBackInfoDownload tmp = DGF.DownloadCompleted;
                                if (tmp.TotalBytesToReceive != 0)
                                {
                                    progressBar2.Value = (int)(tmp.BytesReceived * 100 / tmp.TotalBytesToReceive);
                                    label1.Text = FormatBytes(tmp.BytesReceived) + "/" + FormatBytes(tmp.TotalBytesToReceive);
                                }
                            }));
                        }
                        this.Invoke(new Action(() =>
                        {
                            progressBar2.Value = 100;
                            label1.Text = "Décompression en cours ...";
                        }));
                        DecompressFileLZMA(armaFolder + modName + "\\" + file.path + "." + modeCompression, armaFolder + modName + "\\" + file.path);
                        this.Invoke(new Action(() =>
                        {
                            label1.Text = "";
                        }));

                        while (File.Exists(armaFolder + modName + "\\" + file.path + "." + modeCompression))
                        {
                            try
                            {
                                File.Delete(armaFolder + modName + "\\" + file.path + "." + modeCompression);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }
                    }

                    localFiles = remoteFiles;
                    config.SaveFileList(localFiles);

                    this.Invoke(new Action(() =>
                    {
                        button1.Enabled = true;
                        playBtn.Enabled = true;
                        stateLabel.Text = "ModPack à jour";
                    }));
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Erreur Fatale !\r\n"+ex.Message);
            }
        }

        private static string FormatBytes(long bytes)
        {
            string[] Suffix = { "B", "KB", "MB", "GB", "TB" };
            int i;
            double dblSByte = bytes;
            for (i = 0; i < Suffix.Length && bytes >= 1024; i++, bytes /= 1024)
            {
                dblSByte = bytes / 1024.0;
            }

            return String.Format("{0:0.##} {1}", dblSByte, Suffix[i]);
        }

        private static void DecompressFileLZMA(string inFile, string outFile)
        {
            SevenZip.Compression.LZMA.Decoder coder = new SevenZip.Compression.LZMA.Decoder();
            FileStream input = new FileStream(inFile, FileMode.Open);
            FileStream output = new FileStream(outFile, FileMode.Create);

            // Read the decoder properties
            byte[] properties = new byte[5];
            input.Read(properties, 0, 5);

            // Read in the decompress file size.
            byte[] fileLengthBytes = new byte[8];
            input.Read(fileLengthBytes, 0, 8);
            long fileLength = BitConverter.ToInt64(fileLengthBytes, 0);

            coder.SetDecoderProperties(properties);
            coder.Code(input, output, input.Length, fileLength, null);
            output.Flush();
            output.Close();
            input.Flush();
            input.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Init();
        }

        private void playBtn_Click(object sender, EventArgs e)
        {
            Process arma3 = new Process();
            arma3.StartInfo.FileName = armaFolder + "arma3battleye.exe";
            arma3.StartInfo.Arguments = "-mod="+modName+" -skipIntro -noSplash -noPause -world=empty -noFilePatching -nologs -connect="+ serverIP + " -port="+serverPort;
            arma3.Start();
            this.Visible = false;
            Thread.Sleep(1000);
            notifyIcon1.Visible = true;
            notifyIcon1.ShowBalloonTip(10000, "Launcher Réduit", "Votre launcher ALF à été réduit le temps que vous êtes sur le servuer.", ToolTipIcon.Info);
        }

        private void materialFlatButton1_Click(object sender, EventArgs e)
        {
            Process.Start("ts3server://151.80.109.47");
        }

        private void materialFlatButton3_Click(object sender, EventArgs e)
        {
            Process.Start("https://arma3lifefrance.fr/forum/");
        }

        private void materialFlatButton2_Click(object sender, EventArgs e)
        {
            Process.Start("https://intranet.arma3lifefrance.fr/");
        }

        private void materialFlatButton4_Click(object sender, EventArgs e)
        {
            Process.Start("https://lifeshare.fr/");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            materialLabel3.Text = "Joueurs en ligne : " + Get("https://home.serveur-lagarde.fr/alf/View.php?api&getplayerscounter");
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Form parmaForm = new ParamForm();
            this.Visible = false;
            parmaForm.ShowDialog();
            this.Visible = true;
            Init();
        }

        private void Init()
        {
            config = new ConfigALF();
            armaFolder = config.GetArmaPath();
            button1.Enabled = false;
            playBtn.Enabled = false;
            lookupForLocal.RunWorkerAsync();
            pictureBox1.Load(BGurl);

            MaterialSkinManager materialSkin = MaterialSkinManager.Instance;
            materialSkin.AddFormToManage(this);
            if(config.GetIsDarkTheme())
                materialSkin.Theme = MaterialSkinManager.Themes.DARK;
            else
                materialSkin.Theme = MaterialSkinManager.Themes.LIGHT;
            switch (Get("https://home.serveur-lagarde.fr/alf/View.php?api&getTheme"))
            {
                case "red":
                    materialSkin.ColorScheme = redColor;
                    break;
                case "green":
                    materialSkin.ColorScheme = greenColor;
                    break;
                case "blue":
                    materialSkin.ColorScheme = blueColor;
                    break;
                case "pinkColor":
                    materialSkin.ColorScheme = pinkColor;
                    break;
                case "lightblueColor":
                    materialSkin.ColorScheme = lightblueColor;
                    break;
                case "lightgreenColor":
                    materialSkin.ColorScheme = lightgreenColor;
                    break;
                case "yellowColor":
                    materialSkin.ColorScheme = yellowColor;
                    break;
                case "ignore":
                    switch (config.GetCustomColor())
                    {
                        case "red":
                            materialSkin.ColorScheme = redColor;
                            break;
                        case "green":
                            materialSkin.ColorScheme = greenColor;
                            break;
                        case "blue":
                            materialSkin.ColorScheme = blueColor;
                            break;
                        case "pinkColor":
                            materialSkin.ColorScheme = pinkColor;
                            break;
                        case "lightblueColor":
                            materialSkin.ColorScheme = lightblueColor;
                            break;
                        case "lightgreenColor":
                            materialSkin.ColorScheme = lightgreenColor;
                            break;
                    }
                    break;
            }

            materialLabel3.Text = "Joueurs en ligne : " + Get("https://home.serveur-lagarde.fr/alf/View.php?api&getplayerscounter");
            SteamAPI.Init();
            if (SteamAPI.IsSteamRunning())
            {
                materialLabel4.Text = "SteamID : " + SteamUser.GetSteamID().ToString();
            }
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            this.Visible = true;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            Process[] pname = Process.GetProcessesByName("arma3battleye");
            if (pname.Length == 0)
            {
                if (notifyIcon1.Visible)
                {
                    notifyIcon1.Visible = false;
                    this.Visible = true;
                }
            }
        }
    }

    class FileCallBackInfoDownload
    {
        public long BytesReceived = 0;
        public long TotalBytesToReceive = 0;
        public bool finish = false;
    }

    class DownloadGamefile
    {
        private volatile FileCallBackInfoDownload _completed;

        public void DownloadFile(string address, string location)
        {
            WebClient client = new WebClient();
            Uri Uri = new Uri(address);
            _completed = new FileCallBackInfoDownload();

            client.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);

            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgress);
            client.DownloadFileAsync(Uri, location);

        }

        public FileCallBackInfoDownload DownloadCompleted { get { return _completed; } }

        private void DownloadProgress(object sender, DownloadProgressChangedEventArgs e)
        {
            // Displays the operation identifier, and the transfer progress.
            Console.WriteLine("{0}    downloaded {1} of {2} bytes. {3} % complete...",
                (string)e.UserState,
                e.BytesReceived,
                e.TotalBytesToReceive,
                e.ProgressPercentage);
            _completed.BytesReceived = e.BytesReceived;
            _completed.TotalBytesToReceive = e.TotalBytesToReceive;
        }

        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                Console.WriteLine("Download has been canceled.");
            }
            else
            {
                Console.WriteLine("Download completed!");
            }

            _completed.finish = true;
        }
    }
}
