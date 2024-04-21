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
    public partial class ModsJOURNEY : Form
    {

        public ModsJOURNEY()
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


        private void guna2Button6_Click(object sender, EventArgs e)
        {
            string url = "https://www.curseforge.com/api/v1/mods/32274/files/2916002/download";
            System.Diagnostics.Process.Start(url);
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            string url = "https://www.curseforge.com/api/v1/mods/32274/files/2367915/download";
            System.Diagnostics.Process.Start(url);
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            string url = "https://www.curseforge.com/api/v1/mods/32274/files/3397059/download";
            System.Diagnostics.Process.Start(url);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            string url = "https://www.curseforge.com/minecraft/mc-mods/journeymaps";
            System.Diagnostics.Process.Start(url);
        }

        private void guna2CircleButton2_Click_1(object sender, EventArgs e)
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

        private void guna2CircleButton4_Click_2(object sender, EventArgs e)
        {
            //Go to MainPage
            MainPage mainForm = new MainPage();
            mainForm.ShowDialog();
            this.Hide();
        }
    }
}


