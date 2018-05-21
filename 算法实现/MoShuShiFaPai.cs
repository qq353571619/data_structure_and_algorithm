using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 线性表;

namespace 算法实现
{
    /// <summary>
    /// 魔术师发牌问题的简介：一位魔术师掏出一叠扑克牌，魔术师取出其中13张黑桃，洗好后，把牌面朝下。
    /// 说：“我不看牌，只数一数就能知道每张牌是什么？”魔术师口中念一，将第一张牌翻过来看正好是A；
    /// 魔术师将黑桃A放到桌上，继续数手里的余牌，第二次数1，2，将第一张牌放到这叠牌的下面，将第二张牌翻开，
    /// 正好是黑桃2，也把它放在桌子上。第三次数1，2，3，前面二张牌放到这叠牌的下面，取出第三张牌，正好是黑桃3，
    /// 这样依次将13张牌翻出，全部都准确无误。求解：魔术师手中牌的原始顺序是什么样子的？
    /// </summary>
    class MoShuShiFaPai
    {
        private CycleLink<int> cycle = new CycleLink<int>();

        public void Cal()
        {
            cycle.Clear();

            for (int i = 0; i < 13; i++)
            {
                //初始化为-1  用来区分头结点
                cycle.Add(-1);
            }

            //第一张肯定是1
            CycleLink<int>.Node node = cycle.GetNodeByIndex(1);
            node.data = 1;
            int index = 0;

            for (int i = 2; i <= 13; i++)
            {
                //设置其他位置的牌  头结点也计算进去了
                while (index < i)
                {
                    node = node.next;
                    if (node.data > 0) continue;
                    
                    index++;
                    if (node.data == 0) index--;//跳过头结点不能算
                }

                index = 0;
                node.data = i;
            }
        }

        public void Print()
        {
            int data = 0;
            for (int i = 1; i <= 13; i++)
            {
                cycle.GetElement(i, ref data);
                Console.Write(data + " ");
            }
        }
    }
}
