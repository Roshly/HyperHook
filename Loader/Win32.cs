using System.Runtime.InteropServices;

namespace Loader {
    internal class Win32 {
        [DllImport("kernel32.dll")]
        public static extern unsafe bool VirtualProtect(byte* lpAddress, int dwSize, uint flNewProtect, out uint lpflOldProtect);
    }
}
