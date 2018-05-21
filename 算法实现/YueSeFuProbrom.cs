using 线性表;
using System;

namespace 算法实现
{
    /// <summary>
    /// 41个人围成一个圈，由第一个人开始报数，没数到第3个人就自杀，那么剩下的是在第几个。
    /// </summary>
    class YueSeFuProbrom
    {
        CycleLink<int> cycleLink = new CycleLink<int>();

        public void Cal(int pNum, int which)
        {
            cycleLink.Clear();
            //构建循环链表 表示圆圈
            for (int i = 1; i <= pNum; i++)
            {
                cycleLink.Add(i);
            }

            int index = 0;
            int counter = 0;
            int who = 0;
            while (cycleLink.GetCycleLinkLength() >= which)
            {
                index = index % cycleLink.GetCycleLinkLength();
                counter++;
                index++;
                if (counter % which == 0)
                {
                    cycleLink.Delete(index, ref who);
                    Console.Write(who + " ");
                    index--;
                }
            }
            Console.WriteLine();
            Console.Write("活着的有:");
            for (int i = 1; i < which; i++)
            {
                cycleLink.GetElement(i, ref who);
                Console.Write(who + " ");
            }
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
