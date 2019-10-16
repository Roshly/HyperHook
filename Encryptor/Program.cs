using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Encryptor {
    class Settings {
        public string OutputFolder = "_Encrypted";
        public string EncryptionKey = "Password";
        public string StableUrl = "http://localhost/Files/inj/Stable.dll";
        public string BetaUrl = "http://localhost/Files/inj/Beta.dll";
        public bool NoFilesWarning = true;
    }

    class JsonConfig {
        public string OutputFolder { get; set; }
        public string EncryptionKey { get; set; }
        public string StableUrl { get; set; }
        public string BetaUrl { get; set; }
        public bool NoFilesWarning { get; set; }
    }

    class Program {
        private static string Password = "Password";

        private static List<string> FileList = new List<string>();

        static void Main(string[] args) {
            Settings Settings = new Settings();

            if (File.Exists("Encryptor.json")) {
                JsonConfig JsonConfig = JsonConvert.DeserializeObject<JsonConfig>(File.ReadAllText("Encryptor.json"));

                Settings.OutputFolder = JsonConfig.OutputFolder;
                Settings.EncryptionKey = JsonConfig.EncryptionKey;
                Settings.StableUrl = JsonConfig.StableUrl;
                Settings.BetaUrl = JsonConfig.BetaUrl;
                Settings.NoFilesWarning = JsonConfig.NoFilesWarning;
            }
            else {
                JsonConfig JsonConfig = new JsonConfig {
                    OutputFolder = "_Encrypted",
                    EncryptionKey = "Password",
                    StableUrl = "http://localhost/Files/inj/Stable.dll",
                    BetaUrl = "http://localhost/Files/inj/Beta.dll",
                    NoFilesWarning = true
                };

                using (StreamWriter FilePath = File.CreateText("Encryptor.json")) {
                    JsonSerializer Serializer = new JsonSerializer();
                    Serializer.Serialize(FilePath, JsonConfig);
                }
            }

            if (args.Length > 0) {
                if (!Directory.Exists(Settings.OutputFolder)) {
                    Directory.CreateDirectory(Settings.OutputFolder);
                }

                for (var i = 0; i < args.Length; i++) {
                    string FilePath = args[i];
                    string FileName = Path.GetFileName(FilePath);
                    string Output = Settings.OutputFolder + "\\" + FileName;

                    FileList.Add(FileName);

                    File.WriteAllBytes(Output, EncryptModule(File.ReadAllBytes(FilePath), Settings.EncryptionKey));
                }

                using (StreamWriter Line = new StreamWriter(Settings.OutputFolder + "\\" + "Results.md")) {
                    Line.WriteLine("Date&Time - " + DateTime.Now);
                    Line.WriteLine(string.Empty);

                    foreach (string Item in FileList) {
                        Line.WriteLine(Item + " | Sha256 " + GetHash(Settings.OutputFolder + "\\" + Item));
                    }

                    Line.WriteLine(string.Empty);

                    Line.WriteLine(Settings.StableUrl + " | Base64 " + Base64Encode(Settings.StableUrl));
                    Line.WriteLine(Settings.BetaUrl + " | Base64 " + Base64Encode(Settings.BetaUrl));

                    Line.Close();
                }

                return;
            }

            if (Settings.NoFilesWarning) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR - No file(s) selected!");
                Console.ReadKey();
            }
        }

        static string GetHash(string FilePath) {
            Settings Settings = new Settings();

            using (FileStream stream = File.OpenRead(FilePath)) {
                SHA256Managed sha = new SHA256Managed();
                byte[] hash = sha.ComputeHash(stream);

                return BitConverter.ToString(hash).Replace("-", String.Empty);
            }
        }

        static string Base64Encode(string plainText) {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            return Convert.ToBase64String(plainTextBytes);
        }

        static byte[] EncryptModule(byte[] File, string Password) {
            byte[] PasswordBytes = Encoding.UTF8.GetBytes(Password);
            PasswordBytes = SHA256.Create().ComputeHash(PasswordBytes);
            byte[] EncryptedBytes = null;
            byte[] SaltBytes = PasswordBytes;

            using (MemoryStream ms = new MemoryStream()) {
                using (RijndaelManaged AES = new RijndaelManaged()) {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(PasswordBytes, SaltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (CryptoStream cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write)) {
                        cs.Write(File, 0, File.Length);
                        cs.Close();
                    }

                    EncryptedBytes = ms.ToArray();
                }
            }

            return EncryptedBytes;
        }
    }
}

/* Made by Roshly */