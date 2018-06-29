using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace 算法实现
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestYueSeFu();
            //TestMoShuShi();
            //TestReverse();
            //TestSiZe();
            //TestAXin();
            //TestAandB();
            TestInsertSort();
            Console.ReadKey();
        }

        static void TestInsertSort()
        {
            int[] datas = { 5, 6, 9, 1, 100,3, 7, 0, 2, 4 };
            InsertSort insertSort = new InsertSort();
            insertSort.Sort(datas);
            foreach (var data in datas)
            {
                Console.Write(data + " ");
            }
        }

        //A和B加法测试
        static void TestAandB()
        {
            while (true)
            {
                int a = Int32.Parse(Console.ReadLine());
                int b = Int32.Parse(Console.ReadLine());
                Console.WriteLine(a + "+" + b + "=" + AandB.Add(a, b));
            }
            
        }

        //A*算法测试
        static void TestAXin()
        {
            //构造地图
            int width = 10;
            int height = 10;
            Map map = new Map();
            map.nodes = new Node[width][];
            for (int i = 0; i < width; i++)
            {
                map.nodes[i] = new Node[height];
            }
            map.XLength = width;
            map.YLength = height;

            Random r = new Random();
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {

                    Node node = new Node();
                    node.X = i;node.Y = j;
                    if (r.Next(0, 100) > 80)
                    {
                        node.isOb = true;
                    }
                    map.nodes[i][j] = node;
                }
            }

            //寻路
            AXin aXin = new AXin();
            aXin.SetMap(map);
            Console.WriteLine("搜索22到98");
            aXin.Search(2, 2, 9, 8);
            Console.WriteLine();
            Console.WriteLine("搜索85到13");
            aXin.Search(8, 5, 1, 3);
        }

        //四则运算测试
        static void TestSiZe()
        {
            SiZeYunSuan siZeYun = new SiZeYunSuan();
            siZeYun.Cal();
        }

        //回文测试
        static void TestReverse()
        {
            ReverseNum10_8_2 r = new ReverseNum10_8_2();
            r.Cal();
        }

        //约瑟夫问题测试
        static void TestYueSeFu()
        {
            YueSeFuProbrom yueSeFu = new YueSeFuProbrom();
            yueSeFu.Cal(5, 2);
            yueSeFu.Cal(100, 3);
            yueSeFu.Cal(6, 3);
            yueSeFu.Cal(41, 3);
        }

        //魔术师问题测试
        static void TestMoShuShi()
        {
            MoShuShiFaPai moShuShiFaPai = new MoShuShiFaPai();
            moShuShiFaPai.Cal();
            moShuShiFaPai.Print();
        }
    }
}
