using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin.Controls;
using MaterialSkin;
using System.Net;
using System.IO;
using static ALF.Main;
using System.Reflection;

namespace Arma_Life_France_Launcher
{
    public partial class ParamForm : MaterialForm
    {
        public string AppVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        private string currentColor = "";
        private ConfigALF config = new ConfigALF();
        //ColorScheme
        MaterialSkinManager materialSkin = MaterialSkinManager.Instance;
        ColorScheme redColor = new ColorScheme(Primary.Red600, Primary.Red800, Primary.Red800, Accent.Red200, TextShade.WHITE);
        ColorScheme blueColor = new ColorScheme(Primary.Blue600, Primary.Blue800, Primary.Blue800, Accent.Blue200, TextShade.WHITE);
        ColorScheme greenColor = new ColorScheme(Primary.Green600, Primary.Green800, Primary.Green800, Accent.Green200, TextShade.WHITE);
        ColorScheme pinkColor = new ColorScheme(Primary.Pink600, Primary.Pink800, Primary.Pink800, Accent.Pink200, TextShade.WHITE);
        ColorScheme lightblueColor = new ColorScheme(Primary.LightBlue600, Primary.LightBlue800, Primary.LightBlue800, Accent.LightBlue200, TextShade.WHITE);
        ColorScheme lightgreenColor = new ColorScheme(Primary.LightGreen600, Primary.LightGreen800, Primary.LightGreen800, Accent.LightGreen200, TextShade.WHITE);
        ColorScheme yellowColor = new ColorScheme(Primary.Yellow600, Primary.Yellow800, Primary.Yellow800, Accent.Yellow200, TextShade.WHITE);

        public ParamForm()
        {
            InitializeComponent();
        }

        private void ParamForm_Load(object sender, EventArgs e)
        {
            versionLabel.Text = "V." + AppVersion;
            config = new ConfigALF();
            Form form = new Form1();
            materialSkin.AddFormToManage(this);
            if (config.GetIsDarkTheme())
            {
                materialSkin.Theme = MaterialSkinManager.Themes.DARK;
                materialRadioButton8.Checked = true;
            }
            else
            {
                materialSkin.Theme = MaterialSkinManager.Themes.LIGHT;
                materialRadioButton7.Checked = true;
            }
            materialSingleLineTextField1.Text = config.GetArmaPath();
            switch (Get("http://127.0.0.1/alf/View.php?api&getTheme"))
            {
                case "red":
                    materialRadioButton1.Checked = true;
                    materialSkin.ColorScheme = redColor;
                    break;
                case "green":
                    materialRadioButton2.Checked = true;
                    materialSkin.ColorScheme = greenColor;
                    break;
                case "blue":
                    materialRadioButton3.Checked = true;
                    materialSkin.ColorScheme = blueColor;
                    break;
                case "pinkColor":
                    materialRadioButton4.Checked = true;
                    materialSkin.ColorScheme = pinkColor;
                    break;
                case "lightblueColor":
                    materialRadioButton5.Checked = true;
                    materialSkin.ColorScheme = lightblueColor;
                    break;
                case "lightgreenColor":
                    materialRadioButton6.Checked = true;
                    materialSkin.ColorScheme = lightgreenColor;
                    break;
                case "maintenance":
                    materialSkin.ColorScheme = yellowColor;
                    break;
                case "ignore":
                    materialRadioButton1.Enabled = true;
                    materialRadioButton2.Enabled = true;
                    materialRadioButton3.Enabled = true;
                    materialRadioButton4.Enabled = true;
                    materialRadioButton5.Enabled = true;
                    materialRadioButton6.Enabled = true;
                    switch (config.GetCustomColor())
                    {
                        case "red":
                            materialRadioButton1.Checked = true;
                            break;
                        case "green":
                            materialRadioButton3.Checked = true;
                            break;
                        case "blue":
                            materialRadioButton2.Checked = true;
                            break;
                        case "pinkColor":
                            materialRadioButton4.Checked = true;
                            break;
                        case "lightblueColor":
                            materialRadioButton5.Checked = true;
                            break;
                        case "lightgreenColor":
                            materialRadioButton6.Checked = true;
                            break;
                    }
                    break;
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

        private void materialRadioButton1_EnabledChanged(object sender, EventArgs e)
        {
            MaterialRadioButton radioButton = (MaterialRadioButton) sender;
            if (!radioButton.Checked)
                return;
            switch (radioButton.Text)
            {
                case "Rouge":
                    currentColor = "red";
                    materialSkin.ColorScheme = redColor;
                    break;
                case "Bleu":
                    currentColor = "blue";
                    materialSkin.ColorScheme = blueColor;
                    break;
                case "Vert":
                    currentColor = "green";
                    materialSkin.ColorScheme = greenColor;
                    break;
                case "Rose":
                    currentColor = "pinkColor";
                    materialSkin.ColorScheme = pinkColor;
                    break;
                case "Bleu Clair":
                    currentColor = "lightblueColor";
                    materialSkin.ColorScheme = lightblueColor;
                    break;
                case "Vert Clair":
                    currentColor = "lightgreenColor";
                    materialSkin.ColorScheme = lightgreenColor;
                    break;
            }
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            ConfigALF newConfig = config;
            newConfig.SetCustomColor(currentColor);
            newConfig.SetTheme(materialRadioButton8.Checked);
            if (Directory.Exists(materialSingleLineTextField1.Text))
                newConfig.SetArmaPath(materialSingleLineTextField1.Text);
            newConfig.SaveOptionFile();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void ThemeModeChanged(object sender, EventArgs e)
        {
            if (materialRadioButton8.Checked)
                materialSkin.Theme = MaterialSkinManager.Themes.DARK;
            else
                materialSkin.Theme = MaterialSkinManager.Themes.LIGHT;
        }

        private void materialSingleLineTextField1_DoubleClick(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Arma 3|arma3battleye.exe";
                openFileDialog.FileName = "arma3battleye.exe";
                openFileDialog.InitialDirectory = @"C:\Program Files (x86)\Steam\steamapps\common\Arma 3\";
                while (openFileDialog.ShowDialog() != DialogResult.OK) { }
                FileInfo fileInfo = new FileInfo(openFileDialog.FileName);
                materialSingleLineTextField1.Text = fileInfo.Directory.FullName + @"\";
            }
        }
    }
}
