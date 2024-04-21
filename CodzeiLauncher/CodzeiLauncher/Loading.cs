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

namespace CodzeiLauncher
{
    public partial class Loading : Form
    {
        public Loading()
        {
            InitializeComponent();
        }

        private DiscordRPC.EventHandlers handlers;
        private DiscordRPC.RichPresence presence;
        void RPC()
        {
            this.handlers = default(DiscordRPC.EventHandlers);
            DiscordRPC.Initialize("1228441469954560222", ref this.handlers, true, null);
            this.presence.details = "The best Minecraft Launcher";
            this.presence.state = "discord.gg/5rzkVaBK9G";
            this.presence.largeImageKey = "logo-bg";
            this.presence.largeImageText = "Codzei Launcher";
            this.presence.startTimestamp = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;
            DiscordRPC.UpdatePresence(ref this.presence);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            panel2.Width += 3;
            if (panel2.Width >= 500)
            {
                timer1.Stop();
                LoginScreen m = new LoginScreen();
                m.Show();
                Hide();
            }
        }

        private void Giris_Load(object sender, EventArgs e)
        {
            RPC();
        }
    }
}
