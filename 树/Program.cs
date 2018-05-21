using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 树
{
    class Program
    {
        static void Main(string[] args)
        {
            TestMyTree();
            Console.ReadLine();
        }

        static public void TestMyTree() {
            MyTree<int> t = new MyTree<int>(1);
            PrintfMyTree(t, "添加多个结点前：");
            t.AddNodeTo(1, 2);
            t.AddNodeTo(1, 3);
            t.AddNodeTo(2, 4);
            t.AddNodeTo(2, 5);
            t.AddNodeTo(3, 6);
            PrintfMyTree(t,"添加多个结点后：");
            Console.WriteLine("树结点的个数为：" + t.GetLength());
            Console.WriteLine();

            t.DeleteByIndex(4);
            PrintfMyTree(t,"删除第四个结点后：");
            Console.WriteLine("树结点的个数为：" + t.GetLength());
            Console.WriteLine();


            MyTree<int>.Node node1 = t.GetNodeByIndex(3);
            Console.WriteLine("第三个结点的数据为：" + node1.data);
            MyTree<int>.Node node2 = t.GetNodeByIndex(4);
            Console.WriteLine("第四个结点的数据为："+node2.data);
            Console.WriteLine();

            int local = 0;
            MyTree<int>.Node node3 = t.GetNodeParentAndLocalByIndex(5, ref local);
            Console.WriteLine("第5个结点的双亲数据为：" + node3.data + ",位置为：" + local);
            MyTree<int>.Node node4 = t.GetNodeParentAndLocalByIndex(3, ref local);
            Console.WriteLine("第3个结点的双亲数据为：" + node4.data + ",位置为：" + local);
            Console.WriteLine("树的高度为！" + t.GetHeight());
            Console.WriteLine();

            Console.WriteLine("第1个结点的数据为：" + t.GetElementByIndex(1));
            Console.WriteLine("第2个结点的数据为：" + t.GetElementByIndex(2));
            Console.WriteLine();

            t.Clear();
            PrintfMyTree(t, "清空树后：");
            t.CreatRoot(100);
            PrintfMyTree(t, "创建根节点后：");
            Console.WriteLine("树的高度为！" + t.GetHeight());

        }

        static public void PrintfMyTree(MyTree<int> t,string text = "输出为：") {
            Console.Write(text);
            int[] datas = t.GetDatasByLevels();
            if (datas != null) {
                for (int i = 0; i < datas.Length; i++)
                {
                    Console.Write(datas[i] + " ");
                }
            }
            
            Console.WriteLine();
        }
    }
}
