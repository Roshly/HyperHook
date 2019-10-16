using System.Net;
using System.Text;

namespace Loader {
    internal class Authentication {
        public static string Login(string Username, string Password, string HWID) {
            using (var WebClient = new WebClient()) {
                WebClient.Proxy = null;

                WebClient.Headers.Add("user-agent", Settings.User_Agent);

                var data = new System.Collections.Specialized.NameValueCollection {
                    ["Username"] = Username,
                    ["Password"] = Password,
                    ["HWID"] = HWID
                };

                var response = WebClient.UploadValues(Settings.Website + "Loader/php/Login.php", data);

                return Encoding.UTF8.GetString(response);
            }
        }

        public static string Register(string Username, string Password, string HWID) {
            using (var WebClient = new WebClient()) {
                WebClient.Proxy = null;

                WebClient.Headers.Add("user-agent", Settings.User_Agent);

                var data = new System.Collections.Specialized.NameValueCollection {
                    ["Username"] = Username,
                    ["Password"] = Password,
                    ["HWID"] = HWID
                };

                byte[] Response = WebClient.UploadValues(Settings.Website + "Loader/php/Register.php", data);

                return Encoding.UTF8.GetString(Response);
            }
        }

        public static string Activate(string Username, string License, string HWID) {
            using (var WebClient = new WebClient()) {
                WebClient.Proxy = null;

                WebClient.Headers.Add("user-agent", Settings.User_Agent);

                var data = new System.Collections.Specialized.NameValueCollection {
                    ["Username"] = Username,
                    ["License"] = License,
                    ["HWID"] = HWID
                };

                byte[] Response = WebClient.UploadValues(Settings.Website + "Loader/php/Activate.php", data);

                return Encoding.UTF8.GetString(Response);
            }
        }

        public static string Alert(string Message) {
            using (var WebClient = new WebClient()) {
                WebClient.Proxy = null;

                WebClient.Headers.Add("user-agent", Settings.User_Agent);

                var data = new System.Collections.Specialized.NameValueCollection {
                    ["Message"] = Message
                };

                byte[] Response = WebClient.UploadValues(Settings.Website + "Loader/php/Alert.php", data);

                return Encoding.UTF8.GetString(Response);
            }
        }
    }
}
