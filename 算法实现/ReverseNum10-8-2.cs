using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 算法实现
{
    /// <summary>
    /// 求用十进制、二进制、八进制表示都是回文数的所有数字中，大于十进制数10的最小值
    /// 二进制数为回文数，所以不会以0开头结尾，所以必为奇数
    /// </summary>
    class ReverseNum10_8_2
    {
        public void Cal()
        {
            bool s_out = false;
            int i = 11;
            while (!s_out)
            {
                string str2 =  Convert.ToString(i, 2);
                string str8 = Convert.ToString(i, 8);
                string str10 = Convert.ToString(i, 10);

                /*char[] c2 = str2.ToCharArray();
                Array.Reverse(c2);
                char[] c8 = str8.ToCharArray();
                Array.Reverse(c8);
                char[] c10 = str10.ToCharArray();
                Array.Reverse(c10);*/

                if (str2 == reverse(str2) && str8 == reverse(str8) && str10 == reverse(str10))
                {
                    Console.WriteLine("该数字为：" + str10);
                    Console.WriteLine("二进制为：" + str2);
                    Console.WriteLine("八进制为：" + str8);
                    s_out = true;
                }

                i += 2;
            }
        }

        private string reverse(string str)
        {
            int Length = str.Length;
            char[] rChar = new char[Length];
            for (int i = 0; i < Length; i++)
            {
                rChar[i] = str[Length - i - 1];
            }

            return new string(rChar);

        }
    }
}
