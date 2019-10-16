using System;
using System.Threading;
using System.Windows.Forms;

namespace Loader {
    static class Program {
        [STAThread]
        static void Main() {
            var AntiDebugThread = new Thread(Extensions.Other.StartAntiDebug);
            var AntiDumpThread = new Thread(Extensions.Other.StartAntiDump);
            var ProfilerSpamThread = new Thread(Extensions.Other.StartProfilerSpam);
            AntiDebugThread.Start();
            AntiDumpThread.Start();
            ProfilerSpamThread.Start();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}

/* Made by Roshly */