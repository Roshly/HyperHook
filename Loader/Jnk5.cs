using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loader {
    internal class Jnk5 {
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
            dynamic str1 = Extensions.Other.RandomString(15);
            dynamic str2 = str1 + Extensions.Other.RandomString(15);
            dynamic str3 = str2 + Extensions.Other.RandomString(15);
            dynamic str4 = str3 + Extensions.Other.RandomString(15);
            dynamic str5 = str4 + Extensions.Other.RandomString(15);

            return Extensions.Other.RandomString(15);
        }

        static dynamic RandProfiler_2() {
            dynamic str1 = Extensions.Other.RandomString(15);
            dynamic str2 = str1 + Extensions.Other.RandomString(15);
            dynamic str3 = str2 + Extensions.Other.RandomString(15);
            dynamic str4 = str3 + Extensions.Other.RandomString(15);
            dynamic str5 = str4 + Extensions.Other.RandomString(15);

            return Extensions.Other.RandomString(15);
        }

        static dynamic RandProfiler_3() {
            dynamic str1 = Extensions.Other.RandomString(15);
            dynamic str2 = str1 + Extensions.Other.RandomString(15);
            dynamic str3 = str2 + Extensions.Other.RandomString(15);
            dynamic str4 = str3 + Extensions.Other.RandomString(15);
            dynamic str5 = str4 + Extensions.Other.RandomString(15);

            return Extensions.Other.RandomString(15);
        }

        static dynamic RandProfiler_4() {
            dynamic str1 = Extensions.Other.RandomString(15);
            dynamic str2 = str1 + Extensions.Other.RandomString(15);
            dynamic str3 = str2 + Extensions.Other.RandomString(15);
            dynamic str4 = str3 + Extensions.Other.RandomString(15);
            dynamic str5 = str4 + Extensions.Other.RandomString(15);

            return Extensions.Other.RandomString(15);
        }

        static dynamic RandProfiler_5() {
            dynamic str1 = Extensions.Other.RandomString(15);
            dynamic str2 = str1 + Extensions.Other.RandomString(15);
            dynamic str3 = str2 + Extensions.Other.RandomString(15);
            dynamic str4 = str3 + Extensions.Other.RandomString(15);
            dynamic str5 = str4 + Extensions.Other.RandomString(15);

            return Extensions.Other.RandomString(15);
        }
    }
}