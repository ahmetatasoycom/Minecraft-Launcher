using Guna.UI2.WinForms;
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

    public partial class LoginScreen : Form
    {
        public LoginScreen()
        {
            InitializeComponent();
            Init_Data();
        }

        public static string user;

        private void LoginScreen_Load(object sender, EventArgs e)
        {
            username_error.Hide();
            
        }

        private void btn_kullanici_giris_Click(object sender, EventArgs e)
        {
            string lbUsername = txtUsername.Text;
            if (string.IsNullOrWhiteSpace(lbUsername))
            {
                username_error.Show();
                return;
            }
            user = txtUsername.Text;
            MainPage mp = new MainPage();
            mp.Show();
            Hide();
            Save_Data();
        }

        private void Init_Data()
        {
            if (Properties.Settings.Default.Username != string.Empty)
            {
                if (Properties.Settings.Default.Remember == true)
                {
                    txtUsername.Text = Properties.Settings.Default.Username;
                    rememberme.Checked = true;
                }
                else
                {
                    txtUsername.Text = Properties.Settings.Default.Username;
                }
            }
        }

        private void Save_Data()
        {
            if (rememberme.Checked)
            {
                Properties.Settings.Default.Username = txtUsername.Text.Trim();
                Properties.Settings.Default.Remember = true;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.Username = "";
                Properties.Settings.Default.Remember = false;
                Properties.Settings.Default.Save();
            }

            
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            
        }

        private void btn_kullanici_ad_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
    }
}
