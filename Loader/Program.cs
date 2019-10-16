using System;
using System.Threading;
using System.Windows.Forms;

namespace Loader {
    static class Program {
        static Program() {
            new Thread(Extensions.StartAntiDebug).Start();
            new Thread(Extensions.StartAntiDump).Start();
            new Thread(Extensions.StartProfilerSpam).Start();
        }

        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}

/* Made by Roshly */
