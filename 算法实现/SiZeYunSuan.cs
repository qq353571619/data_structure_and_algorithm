using System;
using System.Data;

namespace 算法实现
{
    /// <summary>
    /// 计算1000-9999中，插入最少一个运算符后使得结果为原来序列的反序。
    /// 例如：886-> 8 * 86 = 688
    /// </summary>
    class SiZeYunSuan
    {
        DataTable table = new DataTable();
        //private Calculator calculator = new Calculator();我的计算机类已经不知道跑去哪里了
        string[] sign = { "+", "-", "*", "/", "" };

        public void Cal()
        {
            string exp = "";
            for (int i = 1000; i < 10000; i++)
            {
                string num = i.ToString();
                for (int j = 0; j < sign.Length; j++)
                {
                    for (int k = 0; k < sign.Length; k++)
                    {
                        for (int l = 0; l < sign.Length; l++)
                        {
                            exp = num[3] + sign[j] + num[2] + sign[k] + num[1] + sign[l] + num[0];
                            if (exp.Length > 4) {
                                if (table.Compute(exp,"").ToString() == num) {
                                    
                                    Console.WriteLine(" "+num[3]+num[2]+num[1]+num[0]);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
