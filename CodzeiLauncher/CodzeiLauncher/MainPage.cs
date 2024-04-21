using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CmlLib.Core;
using CmlLib.Core.Auth;
using System.Threading;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Drawing.Drawing2D;
using CmlLib.Core.Version;
using System.Net;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Diagnostics;
using System.Web.UI.WebControls;
using System.IO;
using System.Security.Cryptography;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;


namespace CodzeiLauncher
{
    public partial class MainPage : Form
    {

        CMLauncher launcher;
        string kullaniciAdi = Environment.UserName;

        public static string version;
        
        private MSession newsession;

        public MainPage()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            // var session = MSession.GetOfflineSession(LoginScreen.user);
        }

        public void UpdateSession(MSession session)
        {
            newsession = session;
        }

        private async void Main_Shown(object sender, EventArgs e)
        {
            var defaultPath = new MinecraftPath(MinecraftPath.GetOSDefaultPath());
            await initializeLauncher(defaultPath);

            showBeta.CheckedChanged += Versions;
            showAlpha.CheckedChanged += Versions;
            showSnapshot.CheckedChanged += Versions;
        }

        private async Task initializeLauncher(MinecraftPath path)
        {
            lbUsername.Text = newsession.Username;
            launcher = new CMLauncher(path);
        }

        private void MainPage_Load(object sender, EventArgs e)
        {
            lbUsername.Text = LoginScreen.user;

            gameLoadingBar.Hide();
            SettingsPanel.Hide();
            modsPanel.Hide();
            infoPanel.Hide();
            path();
            selectedVersion.Text = Properties.Settings.Default.VersionBox;
            var request = WebRequest.Create("https://minotar.net/helm/" + lbUsername.Text + "/100.png");
            using (var response = request.GetResponse())
            using (var stream = response.GetResponseStream())
            {
                var bmp = new Bitmap(stream);
                userskinpicture.Image = bmp;
            }
            Init_Data();

        }

        private void path()
        {
            MinecraftPath path = new MinecraftPath();
            var launcher = new CMLauncher(path);

            launcher.FileChanged += (e) =>
            {
                //listBox1.Items.Add(string.Format("[{0}] {1} - {2}/{3}", e.FileKind.ToString(), e.FileName, e.ProgressedFileCount, e.TotalFileCount));
            };
            launcher.ProgressChanged += (s, e) =>
            {
                //bar2.Value = e.ProgressPercentage;
            };

            string[] allowedVersions = new string[]
            {
                "1.20.4",
                "1.20.1",
                "1.19.4",
                "1.19.1",
                "1.19",
                "1.18.2",
                "1.17.1",
                "1.16.5",
                "1.16.2",
                "1.15.2",
                "1.14.4",
                "1.14.1",
                "1.13.2",
                "1.12.2",
                "1.12",
                "1.11.2",
                "1.11",
                "1.10.2",
                "1.9.4",
                "1.8.9",
                "1.8.7",
                "1.8.2",
                "1.7.10",
                "1.7.2",
                "1.6.4",
                "1.5.2",
                "1.4.4",
                "1.3.2",
                "1.2.5",
                "1.1",
                "1.0",
            };

            versionBox.Items.AddRange(allowedVersions);

        }

        private void launch()
        {
            MainPage mp = new MainPage();

            gameLoadingBar.Show();

            MinecraftPath path = new MinecraftPath();
            var launcher = new CMLauncher(path);
            launcher.FileChanged += (e) =>
            {
                double progressPercentage = (double)e.ProgressedFileCount / e.TotalFileCount * 100;
                gameLoadingBar.Value = (int)progressPercentage;

                //listBox1.Items.Add(string.Format("[{0}] {1} - {2}/{3}", e.FileKind.ToString(), e.FileName, e.ProgressedFileCount, e.TotalFileCount));
            };
            var launchOption = new MLaunchOption
            {
                Session = MSession.GetOfflineSession(LoginScreen.user),
            };
            version = versionBox.SelectedItem.ToString();
            var process = launcher.CreateProcess(version, launchOption);

            process.Start();
            {
                Hide();
            }
            
        }

        private void play_btn_Click(object sender, EventArgs e)
        {
            play_btn.Enabled = false;
            Thread thread = new Thread(() => launch());
            thread.IsBackground = true;
            thread.Start();
        }

        private void Versions(object sender, EventArgs e)
        {
            versionBox.Items.Clear();

            string[] allowedVersions = new string[]
            {
                "1.20.4",
                "1.20.1",
                "1.19.4",
                "1.19.1",
                "1.19",
                "1.18.2",
                "1.17.1",
                "1.16.5",
                "1.16.2",
                "1.15.2",
                "1.14.4",
                "1.14.1",
                "1.13.2",
                "1.12.2",
                "1.12",
                "1.11.2",
                "1.11",
                "1.10.2",
                "1.9.4",
                "1.8.9",
                "1.8.7",
                "1.8.2",
                "1.7.10",
                "1.7.2",
                "1.6.4",
                "1.5.2",
                "1.4.4",
                "1.3.2",
                "1.2.5",
                "1.1",
                "1.0",
            };

            versionBox.Items.AddRange(allowedVersions);

            path();

        }

        private void Init_Data()
        {
            if (string.IsNullOrWhiteSpace(Properties.Settings.Default.Username)) return;

            RamComboBox.Text = Properties.Settings.Default.MaxRam.ToString();
            versionBox.Text = Properties.Settings.Default.VersionBox;
            comboBoxLanguages.Text = Properties.Settings.Default.Localization;
            showSnapshot.Checked = Properties.Settings.Default.Snapshot;
            showBeta.Checked = Properties.Settings.Default.Beta;
            showAlpha.Checked = Properties.Settings.Default.Alpha;
            FullScreen.Checked = Properties.Settings.Default.FullScreenBool;
            showSnapshot.Checked = Properties.Settings.Default.Snapshot;
            showBeta.Checked = Properties.Settings.Default.Beta;
            showAlpha.Checked = Properties.Settings.Default.Alpha;
            selectedVersion.Text = Properties.Settings.Default.VersionBox;
        }

        private void Save_Data()
        {
            Properties.Settings.Default.Resolution = FullScreen.Text;
            Properties.Settings.Default.VersionBox = versionBox.Text;
            Properties.Settings.Default.Localization = comboBoxLanguages.Text;
            Properties.Settings.Default.FullScreenBool = FullScreen.Checked;
            Properties.Settings.Default.Snapshot = showSnapshot.Checked;
            Properties.Settings.Default.Beta = showBeta.Checked;
            Properties.Settings.Default.Alpha = showAlpha.Checked;

            if (int.TryParse(RamComboBox.Text, out int value))
                Properties.Settings.Default.MaxRam = value;
            else
                Properties.Settings.Default.MaxRam = 1024;

            Properties.Settings.Default.Save();
        }


        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }


        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void kullanici_adi_Click(object sender, EventArgs e)
        {

        }

        private void ram_scroll_bar_Scroll(object sender, ScrollEventArgs e)
        {
        }

        private void RamScrollBar_ValueChanged(object sender, ScrollEventArgs e)
        {
            
        }

        public class MyUserSettings
        {
            public int MaximumRamMb { get; set; }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void version_combo_box_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void version_combo_box_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index >= 0)
            {
                var itemFont = new Font("Tahoma", 12, FontStyle.Regular); // Eleman fontunu belirleyin
                e.DrawBackground();
                e.Graphics.DrawString(versionBox.Items[e.Index].ToString(), itemFont, Brushes.Black, e.Bounds);
                e.DrawFocusRectangle();
            }
        }


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void ram_text_Click(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox3_Click(object sender, EventArgs e)
        {
            string url = "https://discord.gg/5rzkVaBK9G";
            System.Diagnostics.Process.Start(url);
        }

        private void label5_Click(object sender, EventArgs e)
        {
            string url = "https://discord.gg/5rzkVaBK9G";
            System.Diagnostics.Process.Start(url);
        }

        private void dc_panel_Paint(object sender, PaintEventArgs e)
        {;
        }

        private void guna2ShadowPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2PictureBox4_Click(object sender, EventArgs e)
        {
            string url = "https://www.minecraft.net/en-us/article/the-trails---tales-update";
            System.Diagnostics.Process.Start(url);
        }

        private void version_combo_box_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void guna2Panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            Save_Data();
            SettingsPanel.Show();
            SettingsPanel.Visible = false;
            settingsBtn.Enabled = true;
            selectedVersion.Text = Properties.Settings.Default.VersionBox;
        }

        private void settingsBtn_Click(object sender, EventArgs e)
        {
            if (!SettingsPanel.Visible)
            {
                Init_Data();
                SettingsPanel.Visible = true;
            }
            else
            {
                DialogResult result = settingsWarning.Show();
                if (result == DialogResult.OK)
                {
                    SettingsPanel.Hide();
                    SettingsPanel.Visible = false;
                    settingsBtn.Enabled = true;
                    Init_Data();
                }
                else
                {
                    SettingsPanel.Hide();

                }
                if (result == DialogResult.Cancel)
                {
                    SettingsPanel.Show();
                }

            }
        }

        private void guna2CheckBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (FullScreen.Checked)
            {
                cmbDisplayMode.Enabled = false;
                ForeColor = label8.ForeColor;
            }
            else
            {
                cmbDisplayMode.Enabled = true;
                ForeColor = label8.ForeColor;
            }
        }

        private void guna2ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void SettingsPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void logoutBtn_Click(object sender, EventArgs e)
        {
            LoginScreen ls = new LoginScreen();
            ls.Show();
            Hide();
        }

        private void userskinpicture_Click(object sender, EventArgs e)
        {

        }

        private void cmbDisplayMode_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                // Kullanıcı adını al
                string kullaniciAdi = Environment.UserName;

                // Varsayılan olarak görünecek klasörün dosya yolunu belirle
                string varsayilanKlasor = Path.Combine(@"C:\Users\" + kullaniciAdi + @"\AppData\Roaming\.minecraft\mods");

                // Ayarla
                folderDialog.SelectedPath = varsayilanKlasor;

                DialogResult result = folderDialog.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderDialog.SelectedPath))
                {
                    // Seçilen klasörün dosya yolunu al ve Guna2Button'un textine yaz
                    guna2Button3.Text = folderDialog.SelectedPath;

                    guna2Button3.AutoSize = true;
                    foreach (Control control in guna2Button3.Controls)
                    {
                        if (control.GetType() == typeof(System.Windows.Forms.Label))
                        {
                            // Label'ın TextAlign özelliğini ayarla
                            ((System.Windows.Forms.Label)control).TextAlign = ContentAlignment.MiddleCenter;
                            break;
                        }
                    }
                }
            }
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            infoPanel.Show();
        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void guna2CircleButton2_Click(object sender, EventArgs e)
        {
            infoPanel.Hide();
        }

        private void openFolder_btn_Click_1(object sender, EventArgs e)
        {
            string gamePath = @"C:\Users\" + kullaniciAdi + @"\AppData\Roaming\.minecraft";


            Process process = new Process();
            process.StartInfo.FileName = gamePath;
            process.Start();
        }

        private void guna2PictureBox11_Click(object sender, EventArgs e)
        {
            modsPanel.Show();
        }

        public Guna.UI2.WinForms.Guna2Panel GetModsPanel()
        {
            return modsPanel; //Come back panel name
        }

        private void curseForgeMods_Click(object sender, EventArgs e)
        {
            //JEI
            ModsJEI ModsJei = new ModsJEI();
            ModsJei.Show();
            Hide();
        }

        private void label16_Click(object sender, EventArgs e)
        {
            //JEI
            ModsJEI ModsJei = new ModsJEI();
            ModsJei.Show();
            Hide();
        }

        private void curseForgeModGECKO_Click(object sender, EventArgs e)
        {
            //GECKO
            ModsGECKO ModsGecko = new ModsGECKO();
            ModsGecko.Show();
            Hide();
        }

        private void label17_Click(object sender, EventArgs e)
        {
            //GECKO
            ModsGECKO ModsGecko = new ModsGECKO();
            ModsGecko.Show();
            Hide();
        }

        private void curseForgeModJM_Click(object sender, EventArgs e)
        {
            ModsJOURNEY ModsJourney = new ModsJOURNEY();
            ModsJourney.Show();
            Hide();
        }

        private void label18_Click(object sender, EventArgs e)
        {
            ModsJOURNEY ModsJourney = new ModsJOURNEY();
            ModsJourney.Show();
            Hide();
        }

        private void guna2PictureBox13_Click(object sender, EventArgs e)
        {
            ModsCOLLECTIVE ModsCollective = new ModsCOLLECTIVE();
            ModsCollective.Show();
            Hide();

        }

        private void label19_Click(object sender, EventArgs e)
        {
            ModsCOLLECTIVE ModsCollective = new ModsCOLLECTIVE();
            ModsCollective.Show();
            Hide();
        }

        private void modsPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2CircleButton3_Click(object sender, EventArgs e)
        {
            modsPanel.Hide();
        }

        private void curseForgeModWaystones_Click(object sender, EventArgs e)
        {
            string url = "https://www.curseforge.com/minecraft/mc-mods/waystones";
            System.Diagnostics.Process.Start(url);
        }

        private void label23_Click(object sender, EventArgs e)
        {
            string url = "https://www.curseforge.com/minecraft/mc-mods/waystones";
            System.Diagnostics.Process.Start(url);
        }

        private void curseForgeModYAPIF_Click(object sender, EventArgs e)
        {
            string url = "https://www.curseforge.com/minecraft/mc-mods/yungs-api";
            System.Diagnostics.Process.Start(url);
        }

        private void label24_Click(object sender, EventArgs e)
        {
            string url = "https://www.curseforge.com/minecraft/mc-mods/yungs-api";
            System.Diagnostics.Process.Start(url);
        }

        private void guna2PictureBox14_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void selectedVersion_Click(object sender, EventArgs e)
        {

        }

        private void guna2Separator3_Click(object sender, EventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void showSnapshot_CheckedChanged(object sender, EventArgs e)
        {
            if (showSnapshot.Checked)
            {
                string[] snapshotVersions = new string[]
            {
                "1.20.4",
                "1.20.1",
                "1.19.4",
                "1.19.1",
                "1.19",
                "1.18.2",
                "1.17.1",
                "1.16.5",
                "1.16.2",
                "1.15.2",
                "1.14.4",
                "1.14.1",
                "1.13.2",
                "1.12.2",
                "1.12.2-pre1",
                "1.11.2",
                "1.11",
                "1.10.2",
                "1.9.4",
                "1.8.9",
                "1.8.7",
                "1.8.2",
                "1.7.10",
                "1.7.2",
                "1.6.4",
                "1.5.2",
                "1.4.4",
                "1.3.2",
                "1.2.5",
                "1.1",
                "1.0",
            };

                versionBox.Items.Clear();
                versionBox.Items.AddRange(snapshotVersions);
            }
            else
            {
                path();
            }

            
        }

        private void showAlpha_CheckedChanged(object sender, EventArgs e)
        {
            if (showSnapshot.Checked)
            {
                string[] alphaVersions = new string[]
            {
                "1.20.4",
                "1.20.1",
                "1.19.4",
                "1.19.1",
                "1.19",
                "1.18.2",
                "1.17.1",
                "1.16.5",
                "1.16.2",
                "1.15.2",
                "1.14.4",
                "1.14.1",
                "1.13.2",
                "1.12.2",
                "1.12",
                "1.11.2",
                "1.11",
                "1.10.2",
                "1.9.4",
                "1.8.9",
                "1.8.7",
                "1.8.2",
                "1.7.10",
                "1.7.2",
                "1.6.4",
                "1.5.2",
                "1.4.4",
                "1.3.2",
                "1.2.5",
                "1.1",
                "1.0",
                "a1.2.8",
                "a1.2.7",
                "a1.2.5",
                "a1.2.1",
                "a1.1.2",
                "a1.1.0",
                "a1.0.4",

            };

                versionBox.Items.Clear();
                versionBox.Items.AddRange(alphaVersions);
            }
            else {
                path(); 
            }
        }

        private void showBeta_CheckedChanged(object sender, EventArgs e)
        {
            if (showBeta.Checked)
            {
                string[] betaVersions = new string[]
                {
                "1.20.4",
                "1.20.1",
                "1.19.4",
                "1.19.1",
                "1.19",
                "1.18.2",
                "1.17.1",
                "1.16.5",
                "1.16.2",
                "1.15.2",
                "1.14.4",
                "1.14.1",
                "1.13.2",
                "1.12.2",
                "1.12",
                "1.11.2",
                "1.11",
                "1.10.2",
                "1.9.4",
                "1.8.9",
                "1.8.7",
                "1.8.2",
                "1.7.10",
                "1.7.2",
                "1.6.4",
                "1.5.2",
                "1.4.4",
                "1.3.2",
                "1.2.5",
                "1.1",
                "1.0",
                "b1.6.3",
                "b1.6.2",
                "b1.6",
                "b1.5",
                "b1.4",
                "b1.2",
                "b1.0.2",
                "b1.0",
                };

                versionBox.Items.Clear();
                versionBox.Items.AddRange(betaVersions);
            }
            else
            {
                path();
            }

        }
    }
}
