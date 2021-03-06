﻿using System;
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
using Octokit;
using System.Diagnostics;
using MaterialSkin.Controls;
using MaterialSkin;
using System.Collections;
using System.Reflection;
using Steamworks;
using static ArzoraLife.Main;
using Newtonsoft.Json.Linq;

namespace Arzora_Life_Launcher
{
    public partial class Form1 : MaterialForm
    {
        
        private string APIurl = "http://193.70.6.201/";
        private string BGurl = "https://media.discordapp.net/attachments/747076192921976882/747831890261835846/Banniere_luncher.png?width=1440&height=226";
        private string fileAPI = "main.xml";
        private string armaFolder = "G:\\Steam\\steamapps\\common\\Arma 3\\";
        private string modName = "@ArzoraLife";
        private string modeCompression = "lzma";
        private string remoteVersion = "";
        private string serverIP = "193.70.6.201";
        private int serverPort = 2302;
        private List<FileArzoraLife> remoteFiles;
        private List<FileArzoraLife> localFiles;
        private ConfigArzoraLife config;
        public string AppVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        private bool blockLaunch = false;
        private bool Maintenance = false;
        private bool showNews = true;
#if DEBUG
        private bool debug = true;
#else
        private bool debug = false;
#endif


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
            blockLaunch = true;
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
                this.Invoke(new Action(() =>
                {
                    pictureBox2.Visible = false;
                }));
                blockLaunch = true;
                XmlDocument doc = new XmlDocument();
                string xmlData = Get(APIurl + fileAPI);

                doc.Load(new StringReader(xmlData));

                XmlNode patch = doc.ChildNodes[0].ChildNodes[0];

                // modName = patch.Attributes.GetNamedItem("data").Value;
                modeCompression = patch.Attributes.GetNamedItem("mode").Value;
                remoteVersion = patch.Attributes.GetNamedItem("launcherVersion").Value;
                remoteFiles = new List<FileArzoraLife>();
                List<string> FileNameRemote = new List<string>();
                List<string> FileHashRemote = new List<string>();

                foreach (XmlNode file in patch.ChildNodes)
                {
                    FileArzoraLife tmp = new FileArzoraLife();
                    tmp.path = file.Attributes.GetNamedItem("path").Value;
                    tmp.hash = file.Attributes.GetNamedItem("hash").Value;
                    tmp.url = file.Attributes.GetNamedItem("url").Value;
                    tmp.size = int.Parse(file.Attributes.GetNamedItem("size").Value);
                    remoteFiles.Add(tmp);
                    FileNameRemote.Add(tmp.path);
                    FileHashRemote.Add(tmp.hash);
                }


                localFiles = new List<FileArzoraLife>();
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
                        FileArzoraLife tmp = new FileArzoraLife();
                        tmp.path = file;
                        tmp.hash = getFileMD5(armaFolder + modName + "\\" + file);
                        tmp.url = null;
                        tmp.size = (new FileInfo(armaFolder + modName + "\\" + file)).Length;
                        localFiles.Add(tmp);
                        FileNameLocal.Add(tmp.path);
                        FileHashLocal.Add(tmp.hash);
                        this.Invoke(new Action(() =>
                        {
                            progressBar1.Value++;
                        }));
                    }
                }
                else if(e.Argument != null && e.Argument.ToString() == "size")
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
                        FileArzoraLife tmp = new FileArzoraLife();
                        tmp.path = file;
                        tmp.hash = null;
                        tmp.url = null;
                        tmp.size = (new FileInfo(armaFolder + modName + "\\" + file)).Length;
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
                    foreach (FileArzoraLife file in localFiles)
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
                    if(File.Exists(armaFolder + modName + "\\" + file))
                    {
                        File.Delete(armaFolder + modName + "\\" + file);
                        localFiles.Remove(localFiles.FirstOrDefault(o => o.path == file));
                    }
                }


                remoteFiles = new List<FileArzoraLife>();
                foreach (XmlNode file in patch.ChildNodes)
                {
                    FileArzoraLife tmp = new FileArzoraLife();
                    tmp.path = file.Attributes.GetNamedItem("path").Value;
                    tmp.hash = file.Attributes.GetNamedItem("hash").Value;
                    tmp.url = file.Attributes.GetNamedItem("url").Value;
                    tmp.size = int.Parse(file.Attributes.GetNamedItem("size").Value);
                    remoteFiles.Add(tmp);
                    FileNameRemote.Add(tmp.path);
                    FileHashRemote.Add(tmp.hash);
                }

                List<FileArzoraLife> HaveToDownload = new List<FileArzoraLife>();
                foreach (FileArzoraLife remoteFile in remoteFiles)
                {
                    if(e.Argument != null && e.Argument.ToString() == "size")
                    {
                        if (localFiles.FirstOrDefault(o => o.path == remoteFile.path) != null)
                        {
                            FileArzoraLife tmp = localFiles.FirstOrDefault(o => o.path == remoteFile.path);

                            if(tmp.size != remoteFile.size)
                            {
                                HaveToDownload.Add(remoteFile);
                            }
                        }
                        else
                        {
                            HaveToDownload.Add(remoteFile);
                        }
                    }
                    else
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
                    blockLaunch = false;
                }
                else
                {
                    int c = 0;
                    int total = HaveToDownload.Count;
                    foreach (FileArzoraLife file in HaveToDownload)
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
                        string dlurl = APIurl + file.url + "." + modeCompression;
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
                    blockLaunch = false;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Erreur Fatale !\r\n"+ex.Message);
            }

            this.Invoke(new Action(() =>
            {
                pictureBox2.Visible = true;
            }));
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
            FileStream input = new FileStream(inFile, System.IO.FileMode.Open);
            FileStream output = new FileStream(outFile, System.IO.FileMode.Create);

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
            if (Maintenance)
                MessageBox.Show("Maintenance en cours ...");
            if (blockLaunch || Maintenance)
                return;
            Process arma3 = new Process();
            arma3.StartInfo.FileName = armaFolder + "arma3battleye.exe";
            arma3.StartInfo.Arguments = "-mod="+modName+" -skipIntro -noSplash -noPause -world=empty -noFilePatching -nologs -connect="+ serverIP + " -port="+serverPort;
            try
            {
                arma3.Start();
                this.Visible = false;
                Thread.Sleep(1000);
                notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(10000, "Launcher Réduit", "Votre launcher ArzoraLife à été réduit le temps que vous êtes sur le servuer.", ToolTipIcon.Info);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Le jeu n'a pas pu être lancé !", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            materialLabel3.Text = "Joueurs en ligne : " + Get("http://193.70.6.201/index.php?playersCount");
            if (Get("http://193.70.6.201/theme.php") == "maintenance")
            {
                Maintenance = true;
            }
            else
            {
                Maintenance = false;
            }
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
            config = new ConfigArzoraLife();
            armaFolder = config.GetArmaPath();
            button1.Enabled = false;
            playBtn.Enabled = false;
            lookupForLocal.RunWorkerAsync("size");
            pictureBox1.LoadAsync(BGurl);

            MaterialSkinManager materialSkin = MaterialSkinManager.Instance;
            materialSkin.AddFormToManage(this);
            if(config.GetIsDarkTheme())
                materialSkin.Theme = MaterialSkinManager.Themes.DARK;
            else
                materialSkin.Theme = MaterialSkinManager.Themes.LIGHT;
            Color progressColor = Color.DodgerBlue;

            news_image.Visible = showNews;
            news_name.Visible = showNews;
            news_text.Visible = showNews;
            if (showNews)
            {
                NewsArzoraLife news = JsonConvert.DeserializeObject<NewsArzoraLife>(Get("http://193.70.6.201/news.json"));
                news_image.LoadAsync(news.image);
                news_name.Text = news.name;
                news_text.Text = news.text;
            }

            Maintenance = false;
            switch (Get("http://193.70.6.201/theme.php"))
            {
                case "red":
                    progressColor = Color.FromArgb(229, 57, 53);
                    materialSkin.ColorScheme = redColor;
                    break;
                case "green":
                    progressColor = Color.FromArgb(67, 160, 71);
                    materialSkin.ColorScheme = greenColor;
                    break;
                case "blue":
                    progressColor = Color.FromArgb(30, 136, 229);
                    materialSkin.ColorScheme = blueColor;
                    break;
                case "pinkColor":
                    progressColor = Color.FromArgb(216, 27, 96);
                    materialSkin.ColorScheme = pinkColor;
                    break;
                case "lightblueColor":
                    progressColor = Color.FromArgb(3, 155, 229);
                    materialSkin.ColorScheme = lightblueColor;
                    break;
                case "lightgreenColor":
                    progressColor = Color.FromArgb(124, 179, 66);
                    materialSkin.ColorScheme = lightgreenColor;
                    break;
                case "maintenance":
                    progressColor = Color.FromArgb(253, 216, 53);
                    materialSkin.ColorScheme = yellowColor;
                    Maintenance = true;
                    break;
                case "ignore":
                    switch (config.GetCustomColor())
                    {
                        case "red":
                            progressColor = Color.FromArgb(229, 57, 53);
                            materialSkin.ColorScheme = redColor;
                            break;
                        case "green":
                            progressColor = Color.FromArgb(67, 160, 71);
                            materialSkin.ColorScheme = greenColor;
                            break;
                        case "blue":
                            progressColor = Color.FromArgb(30, 136, 229);
                            materialSkin.ColorScheme = blueColor;
                            break;
                        case "pinkColor":
                            progressColor = Color.FromArgb(216, 27, 96);
                            materialSkin.ColorScheme = pinkColor;
                            break;
                        case "lightblueColor":
                            progressColor = Color.FromArgb(3, 155, 229);
                            materialSkin.ColorScheme = lightblueColor;
                            break;
                        case "lightgreenColor":
                            progressColor = Color.FromArgb(124, 179, 66);
                            materialSkin.ColorScheme = lightgreenColor;
                            break;
                    }
                    break;
            }
            progressBar1.ForeColor = progressColor;

            //////////////////////////////////
            if(!debug)
                checkForUpdate();
            //////////////////////////////////

            materialLabel3.Text = "Joueurs en ligne : " + Get("http://193.70.6.201/index.php?playersCount");
            SteamAPI.Init();
            if (SteamAPI.IsSteamRunning())
            {
                materialLabel4.Text = "SteamID : " + SteamUser.GetSteamID().ToString();
                if(Get("http://193.70.6.201/index.php?whitelist="+ SteamUser.GetSteamID().ToString()) == "0")
                {
                    MessageBox.Show("Vous n'êtes pas whitelisté !", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Le client steam n'a pas été détecter !", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void checkForUpdate()
        {
            try
            {
                // string GitHubToken = "TOKENHERE";
                var client = new GitHubClient(new ProductHeaderValue("ArzoraLauncher"));
                // var tokenAuth = new Credentials(GitHubToken);
                // client.Credentials = tokenAuth;

                var LatestRelease = client.Repository.Release.GetLatest("loann25310", "ArzoraLauncher").Result;

                if (LatestRelease.TagName != AppVersion && !LatestRelease.Prerelease)
                {
                    MessageBox.Show(this, "Mise à jour disponible.\r\n\r\nNom :\r\n"+ LatestRelease.Name, "Mise à jour", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    WebClient WebClient = new WebClient();
                    Uri Uri = new Uri(LatestRelease.Assets[0].BrowserDownloadUrl);
                    // WebClient.Headers.Add("Authorization: token "+ GitHubToken);
                    // WebClient.Credentials = new NetworkCredential("loann25310", GitHubToken);
                    WebClient.DownloadFile(Uri, config.GetAppData() + "maj_alf_launcher.exe");
                    MessageBox.Show(this, "Téléchargement terminé.\r\nAprès la validation du message, l'installateur s'ouvrira. Réinstaller l'application normalement.", "Mise à jour", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Process.Start(config.GetAppData() + "maj_alf_launcher.exe");
                    System.Windows.Forms.Application.Exit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur Fatale lors de la verification/téléchargement de la mise à jour. Verifiez votre connection internet. Si le problème persite contactez un Administrateur.\r\n\r\nCode d'erreur : " + ex.Message, "Erreur Fatale", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Windows.Forms.Application.Exit();
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

        private void materialCheckBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Process.Start("https://intranet.arzora.fr/");
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Process.Start("https://forum.arzora.fr/");
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Process.Start("https://discord.gg/ursr2jT");
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Process.Start("ts3server://ts.arzora.fr");
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
