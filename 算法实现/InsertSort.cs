using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 算法实现
{
    class InsertSort
    {
        public bool Sort(int[] datas)
        {
            if (datas.Length < 1) return false;

            for (int i = 1; i < datas.Length; i++)
            {
                int temp = datas[i];
                for (int j = 0; j < i; j++)
                {
                    if (temp < datas[j])
                    {
                        for (int k = i; k > j; k--)
                        {
                            datas[k] = datas[k - 1];
                        }
                        datas[j] = temp;
                        break;
                    }
                }
            }

            return true;
        }
    }
}
