using Newtonsoft.Json;
using System;
using System.Windows.Forms;

namespace Loader {
    public partial class Form1 : MetroFramework.Forms.MetroForm {
        public Form1() {
            InitializeComponent();
        }

        void Form1_Load(object sender, EventArgs e) {
            try {
                if (!System.IO.Directory.Exists(System.IO.Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "HyperHook"))) {
                    System.IO.Directory.CreateDirectory(System.IO.Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "HyperHook"));

                    return;
                }

                var streamReader = new System.IO.StreamReader(System.IO.Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "HyperHook\\Credentials")
                );

                metroTextBox4.Text = Extensions.Base64Decode(streamReader.ReadLine());
                metroTextBox3.Text = Extensions.Base64Decode(streamReader.ReadLine());

                streamReader.Close();
            }
            catch { }
        }

        void metroButton2_Click(object sender, EventArgs e) {
            /* Login Button */

            Settings.Username = metroTextBox4.Text;
            Settings.Password = metroTextBox3.Text;

            if (string.IsNullOrEmpty(Settings.Username)) return;
            if (string.IsNullOrEmpty(Settings.Password)) return;

            Settings.HWID = Hwid.UserID();

            var temp = Authentication.Login(
                Settings.Username,
                Settings.Password,
                Settings.HWID);

            dynamic Results = JsonConvert.DeserializeObject(temp);

            Settings.Username = Results.Username.ToString();
            Settings.Expires = Results.Expires.ToString();

            switch (bool.Parse(Results.Authenticated.ToString())) {
                case true:

                    var StreamWriter = new System.IO.StreamWriter(System.IO.Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "HyperHook\\Credentials")
                    );

                    StreamWriter.WriteLine(Extensions.Base64Encode(Settings.Username));
                    StreamWriter.WriteLine(Extensions.Base64Encode(Settings.Password));

                    StreamWriter.Close();

                    Settings.Betastatus = int.Parse(Results.Betastatus.ToString());

                    MessageBox.Show(Results.Description.ToString());

                    var Form2 = new Form2();
                    Hide();
                    Form2.Closed += (s, args) => Close();
                    Form2.Show();

                    break;

                case false:
                    MessageBox.Show(Results.Description.ToString());

                    break;

                default:
                    MessageBox.Show("connection error");

                    break;
            }
        }

        void metroButton3_Click(object sender, EventArgs e) {
            /* Register Button */

            Settings.Username = metroTextBox5.Text;
            Settings.Password = metroTextBox6.Text;

            if (string.IsNullOrEmpty(Settings.Username)) return;
            if (string.IsNullOrEmpty(Settings.Password)) return;

            Settings.HWID = Hwid.UserID();

            var temp = Authentication.Register(
                Settings.Username,
                Settings.Password,
                Settings.HWID);

            dynamic Results = JsonConvert.DeserializeObject(temp);

            switch (bool.Parse(Results.Authenticated.ToString())) {
                case true:
                    MessageBox.Show(Results.Description.ToString());

                    break;

                case false:
                    MessageBox.Show(Results.Description.ToString());

                    break;

                default:
                    MessageBox.Show("connection error");

                    break;
            }
        }

        void metroButton1_Click(object sender, EventArgs e) {
            /* Activation Button */

            Settings.Username = metroTextBox1.Text;
            Settings.License = metroTextBox2.Text;

            if (string.IsNullOrEmpty(Settings.Username)) return;
            if (string.IsNullOrEmpty(Settings.License)) return;

            Settings.HWID = Hwid.UserID();

            string temp = Authentication.Activate(
                Settings.Username,
                Settings.License,
                Settings.HWID);

            dynamic Results = JsonConvert.DeserializeObject(temp);

            switch (bool.Parse(Results.Authenticated.ToString())) {
                case true:
                    MessageBox.Show(Results.Description.ToString());

                    break;

                case false:
                    MessageBox.Show(Results.Description.ToString());

                    break;

                default:
                    MessageBox.Show("connection error");

                    break;
            }
        }

        void Form1_FormClosed(object sender, FormClosedEventArgs e) {
            Environment.Exit(0);
        }
    }
}

/* Made by Roshly */
