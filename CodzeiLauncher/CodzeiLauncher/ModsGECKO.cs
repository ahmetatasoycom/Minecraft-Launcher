using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodzeiLauncher
{
    public partial class ModsGECKO : Form
    {

        public ModsGECKO()
        {
            InitializeComponent();
        }

        private void modsPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label24_Click(object sender, EventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void geckoPanel_Paint(object sender, PaintEventArgs e)
        {

        }


        private void guna2CircleButton4_Click(object sender, EventArgs e)
        {
            MainPage mainForm = new MainPage();
            mainForm.ShowDialog();
            this.Hide();
        }

        private void guna2Button6_Click_1(object sender, EventArgs e)
        {
            string url = "https://www.curseforge.com/api/v1/mods/388172/files/4407241/download";
            System.Diagnostics.Process.Start(url);
        }

        private void guna2Button4_Click_1(object sender, EventArgs e)
        {
            string url = "https://www.curseforge.com/api/v1/mods/388172/files/4181370/download";
            System.Diagnostics.Process.Start(url);
        }

        private void guna2Button5_Click_1(object sender, EventArgs e)
        {
            string url = "https://www.curseforge.com/api/v1/mods/388172/files/4182600/download";
            System.Diagnostics.Process.Start(url);
        }

        private void guna2CircleButton2_Click(object sender, EventArgs e)
        {
            //Go to ModsPanel
            MainPage mainForm = new MainPage();
            mainForm.Show();
            Guna.UI2.WinForms.Guna2Panel modsPanel = mainForm.GetModsPanel();
            if (modsPanel != null)
            {
                modsPanel.Visible = true;
            }
            this.Close();
        }

        private void guna2CircleButton4_Click_1(object sender, EventArgs e)
        {
            //Go to MainPage
            MainPage mainForm = new MainPage();
            mainForm.ShowDialog();
            this.Hide();
        }

        private void label24_Click_1(object sender, EventArgs e)
        {
            string url = "https://www.curseforge.com/minecraft/mc-mods/geckolib";
            System.Diagnostics.Process.Start(url);
        }
    }
}

