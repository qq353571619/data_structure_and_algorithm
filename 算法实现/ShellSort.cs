using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 算法实现
{
    class ShellSort
    {
        public bool Sort(int[] datas)
        {
            if (datas.Length < 1) return false;

            for (int gap = datas.Length / 2; gap > 0; gap /= 2)
            {
                for (int i = gap; i < datas.Length; i++)
                {
                    int j = i;
                    while (j - gap >= 0 && datas[j] < datas[j - gap])
                    {
                        swap(datas, j, j - gap);
                        j -= gap;
                    }
                }
            }


            return true;
        }

        private void swap(int[] data, int a, int b)
        {
            data[a] = data[a] + data[b];
            data[b] = data[a] - data[b];
            data[a] = data[a] - data[b];
        }
    }
}
