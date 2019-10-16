using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading;

namespace Loader {
    public partial class Form2 : MetroFramework.Forms.MetroForm {
        static bool Stable = true;
        static bool Beta = false;

        public Form2() {
            InitializeComponent();

            if (Settings.Betastatus == 1) {
                metroRadioButton1.Visible = metroRadioButton1.Visible = true;
            }
            else {
                metroRadioButton1.Visible = metroRadioButton1.Visible = false;
            }

            timer1.Interval = 50;
            timer1.Start();
        }

        void Form2_Load(object sender, EventArgs e) { }

        void metroButton1_Click(object sender, EventArgs e) {
            this.metroButton1.Enabled = false;

            using (var WebClient = new WebClient()) {
                WebClient.Proxy = null;

                WebClient.Headers.Add("user-agent", Settings.User_Agent);

                Settings.HWID = Hwid.UserID();

                var temp = Authentication.Login(
                    Settings.Username,
                    Settings.Password,
                    Settings.HWID);

                dynamic Results = JsonConvert.DeserializeObject(temp);

                WebClient.DownloadProgressChanged += DownloadChanged;
                WebClient.DownloadDataCompleted += DownloadCompleted;

                if (Stable)
                    WebClient.DownloadDataAsync(new Uri(Results.stableurl.ToString()));

                if (Beta)
                    WebClient.DownloadDataAsync(new Uri(Results.betaurl.ToString()));
            }
        }

        void DownloadChanged(object sender, DownloadProgressChangedEventArgs e) {
            double bytesIn = double.Parse(e.BytesReceived.ToString());
            double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
            double percentage = bytesIn / totalBytes * 100;
            metroProgressBar1.Value = int.Parse(Math.Truncate(percentage).ToString());
        }

        void DownloadCompleted(object sender, DownloadDataCompletedEventArgs e) {
            var temp = Authentication.Login(
                Settings.Username,
                Settings.Password,
                Settings.HWID);

            dynamic Results = JsonConvert.DeserializeObject(temp);

            temp = Results.DecryptionKey.ToString();

            Thread InjectionThread = new Thread(() => InjectionHelper.Inject(e.Result, temp));
            InjectionThread.Start();

            this.Hide();
        }

        void metroRadioButton2_Click(object sender, EventArgs e) {
            Stable = true;
            Beta = false;
        }

        void metroRadioButton1_Click(object sender, EventArgs e) {
            Stable = false;
            Beta = true;
        }

        void timer1_Tick(object sender, EventArgs e) {
            timer1.Stop();
            metroLabel3.Text = Settings.Username;
            metroLabel4.Text = Settings.Expires;
        }
    }
}

/* Made by Roshly */
