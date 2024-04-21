using System;
using System.Windows.Forms;

namespace CodzeiLauncher
{
    public partial class ModsJEI : Form
    {

        public ModsJEI()
        {
            InitializeComponent();
        }

        private void Mods_Load(object sender, EventArgs e)
        {

        }

        private void modsPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            MainPage mainForm = new MainPage();
            mainForm.Show();

            // MainPage formundaki modsPanel'i göster
            Guna.UI2.WinForms.Guna2Panel modsPanel = mainForm.GetModsPanel();
            if (modsPanel != null)
            {
                modsPanel.Visible = true;
            }

            // Mods formunu kapat
            this.Close();

        }

        private void guna2CircleButton3_Click(object sender, EventArgs e)
        {
            MainPage mainForm = new MainPage();
            mainForm.ShowDialog();
            this.Hide();
        }

        private void label20_Click(object sender, EventArgs e)
        {
            string url = "https://www.curseforge.com/minecraft/mc-mods/jei";
            System.Diagnostics.Process.Start(url);
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string url = "https://www.curseforge.com/api/v1/mods/238222/files/5106177/download";
            System.Diagnostics.Process.Start(url);
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            string url = "https://www.curseforge.com/api/v1/mods/238222/files/3040523/download";
            System.Diagnostics.Process.Start(url);
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            string url = "https://www.curseforge.com/api/v1/mods/238222/files/3681294/download";
            System.Diagnostics.Process.Start(url);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
