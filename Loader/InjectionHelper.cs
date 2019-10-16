using System;
using Injection;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Loader {
    internal class InjectionHelper {
        static byte[] ToInject;

        static Process Prepare() {
            Process[] proc = Process.GetProcessesByName("csgo");

            if (proc.Length == 0) return null;

            var proc2 = proc[0];

            var clientBase = 0;
            var engineBase = 0;

            foreach (object obj in proc2.Modules) {
                var processModule = (ProcessModule)obj;

                if (processModule.ModuleName == "client.dll") {
                    clientBase = (int)processModule.BaseAddress;
                }

                if (processModule.ModuleName == "engine.dll") {
                    engineBase = (int)processModule.BaseAddress;
                }

                if (clientBase > 0 && engineBase > 0) {
                    break;
                }
            }

            if (clientBase != 0 && engineBase != 0) {
                return proc2;
            }

            return null;
        }

        public static void Inject(byte[] File, string Password) {
            var num = 0;

            MessageBox.Show("Remote injection thread started.");

            for (; ; ) {
                Process Ready = null;

                while (Ready == null) {
                    Ready = Prepare();
                    Thread.Sleep(500);
                    num += 500;

                    if (num >= 300000) {
                        MessageBox.Show("Injection timed out.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }

                Thread.Sleep(500);

                ManualMapInjector Injection =
                    new ManualMapInjector(Process.GetProcessesByName(Settings.ProcessName).First()) {
                        AsyncInjection = true
                    };

                Injection.Inject(Extensions.Other.DecryptModule(File, Password)).ToInt64();
                Environment.Exit(0);
            }
        }
    }
}