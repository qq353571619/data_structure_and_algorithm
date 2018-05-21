using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 算法实现
{
    /// <summary>
    /// 不用加号，计算两个数的和
    /// </summary>
    class AandB
    {
        public static int Add(int a, int b)
        {
            int c = 0;
            int d = 0;

            while (b != 0)
            {
                c = a ^ b;
                d = (a & b) << 1;

                a = c;
                b = d;
            }
            return a;
        }
    }
}
