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
    public partial class ModsCOLLECTIVE : Form
    {
        public ModsCOLLECTIVE()
        {
            InitializeComponent();
        }

        private void collectivePanel_Paint(object sender, PaintEventArgs e)
        {

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

        private void guna2CircleButton4_Click(object sender, EventArgs e)
        {
            //Go to MainPage
            MainPage mainForm = new MainPage();
            mainForm.ShowDialog();
            this.Hide();
        }

        private void guna2Button6_Click_1(object sender, EventArgs e)
        {
            string url = "https://www.curseforge.com/api/v1/mods/342584/files/4312565/download";
            System.Diagnostics.Process.Start(url);
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            string url = "https://www.curseforge.com/api/v1/mods/342584/files/3533131/download";
            System.Diagnostics.Process.Start(url);
        }

        private void label1_Click_1(object sender, EventArgs e)
        {
            string url = "https://www.curseforge.com/minecraft/mc-mods/collective";
            System.Diagnostics.Process.Start(url);
        }
    }
}
