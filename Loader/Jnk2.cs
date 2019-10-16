using System;

namespace Loader {
    internal class Jnk2 {
        public static void Execute() {
            Random Rand = new Random();

            dynamic Num = Rand.Next(0, 6);

            switch (Num) {
                case 1:
                    RandProfiler_1();

                    break;

                case 2:
                    RandProfiler_2();

                    break;

                case 3:
                    RandProfiler_3();

                    break;

                case 4:
                    RandProfiler_4();

                    break;

                case 5:
                    RandProfiler_5();

                    break;
            }
        }

        static dynamic RandProfiler_1() {
            dynamic str1 = Extensions.RandomString(15);
            dynamic str2 = str1 + Extensions.RandomString(15);
            dynamic str3 = str2 + Extensions.RandomString(15);
            dynamic str4 = str3 + Extensions.RandomString(15);
            dynamic str5 = str4 + Extensions.RandomString(15);

            return Extensions.RandomString(15);
        }

        static dynamic RandProfiler_2() {
            dynamic str1 = Extensions.RandomString(15);
            dynamic str2 = str1 + Extensions.RandomString(15);
            dynamic str3 = str2 + Extensions.RandomString(15);
            dynamic str4 = str3 + Extensions.RandomString(15);
            dynamic str5 = str4 + Extensions.RandomString(15);

            return Extensions.RandomString(15);
        }

        static dynamic RandProfiler_3() {
            dynamic str1 = Extensions.RandomString(15);
            dynamic str2 = str1 + Extensions.RandomString(15);
            dynamic str3 = str2 + Extensions.RandomString(15);
            dynamic str4 = str3 + Extensions.RandomString(15);
            dynamic str5 = str4 + Extensions.RandomString(15);

            return Extensions.RandomString(15);
        }

        static dynamic RandProfiler_4() {
            dynamic str1 = Extensions.RandomString(15);
            dynamic str2 = str1 + Extensions.RandomString(15);
            dynamic str3 = str2 + Extensions.RandomString(15);
            dynamic str4 = str3 + Extensions.RandomString(15);
            dynamic str5 = str4 + Extensions.RandomString(15);

            return Extensions.RandomString(15);
        }

        static dynamic RandProfiler_5() {
            dynamic str1 = Extensions.RandomString(15);
            dynamic str2 = str1 + Extensions.RandomString(15);
            dynamic str3 = str2 + Extensions.RandomString(15);
            dynamic str4 = str3 + Extensions.RandomString(15);
            dynamic str5 = str4 + Extensions.RandomString(15);

            return Extensions.RandomString(15);
        }
    }
}