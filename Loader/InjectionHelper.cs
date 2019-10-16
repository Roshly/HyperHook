using Injection;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Loader {
    internal class InjectionHelper {
        public static void Inject(byte[] File, string Password) {
            var num = 0;

            while (Process.GetProcessesByName(Settings.ProcessName).Length == 0) {
                Thread.Sleep(500);

                num += 500;

                if (num >= 300000) {
                    MessageBox.Show("Injection timed out.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }

            var injection =
                new ManualMapInjector(Process.GetProcessesByName(Settings.ProcessName).First()) {
                    AsyncInjection = true
                };

            injection.Inject(Extensions.DecryptModule(File, Password)).ToInt64();
            Environment.Exit(0);
        }
    }
}