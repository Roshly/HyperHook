using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Loader {
    internal class Hwid {
        static string hardware_ID = string.Empty;

        public static string UserID() {
            if (string.IsNullOrEmpty(hardware_ID)) {
                hardware_ID = GetHash("CPU >> " + CpuID() + "\nBIOS >> " + BiosID() + "\nBASE >> " + BaseID());
            }

            return hardware_ID;
        }

        static string GetHash(string s) {
            return GetHexString(new MD5CryptoServiceProvider().ComputeHash(Encoding.ASCII.GetBytes(s)));
        }

        private static string GetHexString(IList<byte> bt) {
            var s = string.Empty;

            for (int i = 0; i < bt.Count; i++) {
                var b = bt[i];
                int n = b;
                var n1 = n & 15;
                var n2 = (n >> 4) & 15;

                if (n2 > 9)
                    s += ((char)(n2 - 10 + 'A')).ToString(System.Globalization.CultureInfo.InvariantCulture);
                else
                    s += n2.ToString(System.Globalization.CultureInfo.InvariantCulture);

                if (n1 > 9)
                    s += ((char)(n1 - 10 + 'A')).ToString(System.Globalization.CultureInfo.InvariantCulture);
                else
                    s += n1.ToString(System.Globalization.CultureInfo.InvariantCulture);

                if ((i + 1) != bt.Count && (i + 1) % 2 == 0) s += "-";
            }

            return s;
        }

        private static string Identifier(string wmiClass, string wmiProperty) {
            var result = string.Empty;
            var mc = new System.Management.ManagementClass(wmiClass);
            var moc = mc.GetInstances();

            foreach (System.Management.ManagementBaseObject mo in moc) {
                if (result != string.Empty) continue;

                try {
                    result = mo[wmiProperty].ToString();

                    break;
                }
                catch { }
            }

            return result;
        }

        private static string CpuID() {
            var retVal = Identifier("Win32_Processor", "UniqueId");

            if (retVal != string.Empty) return retVal;
            retVal = Identifier("Win32_Processor", "ProcessorId");

            if (retVal != string.Empty) return retVal;
            retVal = Identifier("Win32_Processor", "Name");

            if (retVal == string.Empty) {
                retVal = Identifier("Win32_Processor", "Manufacturer");
            }

            retVal += Identifier("Win32_Processor", "MaxClockSpeed");

            return retVal;
        }

        // BIOS Identifier
        private static string BiosID() {
            return Identifier("Win32_BIOS", "Manufacturer") + Identifier("Win32_BIOS", "SMBIOSBIOSVersion") +
                   Identifier("Win32_BIOS", "IdentificationCode") + Identifier("Win32_BIOS", "SerialNumber") +
                   Identifier("Win32_BIOS", "ReleaseDate") + Identifier("Win32_BIOS", "Version");
        }

        // Motherboard ID
        private static string BaseID() {
            return Identifier("Win32_BaseBoard", "Model") + Identifier("Win32_BaseBoard", "Manufacturer") +
                   Identifier("Win32_BaseBoard", "Name") + Identifier("Win32_BaseBoard", "SerialNumber");
        }
    }
}
