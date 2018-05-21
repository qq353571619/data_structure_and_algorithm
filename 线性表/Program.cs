using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace 线性表
{
    class Program
    {

        static int num = 0;

        static void Main(string[] args)
        {
            //TestMyList();
            //TestLink();
            //TestStaticLink();
            //TestCycleLink();
            //TestDoubleLink();
            //TestStackList();
            //TestDoubleStackList();
            //TestStackLink();
            //Fbi(4);
            //TestCalculator();
            //TestQueueCycleList();
            //TestQueueLink();
            TestStringList();
            //TestStringLink();
            //TestStringLink2();

            Console.ReadLine();
        }

        static void TestStringLink2() {
            StringLink2 s = new StringLink2(new char[] { 'h', 'e', 'l', 'g', 'o' },3);
            PrintStringLink2(s,"串s为：");
            StringLink2 s2 = new StringLink2(new char[] { 'g', 'o' });
            s.Insert(2, new char[] { 'w', 'o' });
            PrintStringLink2(s,"串s在2位置插入wo后：");
            PrintStringLink2(s2, "串s2为：");
            Console.WriteLine("在串s中查找s2的位置为："+s.Index(s2));
            s.Insert(3, new char[] { 'f', 't' });
            PrintStringLink2(s,"串s在3号位插入ft后：");
            s.Delete(3, 3);
            PrintStringLink2(s,"串s删除3号位后3个字符后：");
            Console.WriteLine();

            s.Replace(s2, new StringLink2(new char[] { 'c', 'h', 'i' }));
            PrintStringLink2(s,"替换go为chi后：");
            s.Concat(new char[] { 'l', 'o', 'v', 'e' });
            PrintStringLink2(s,"链接love后：");
            s.Replace(new StringLink2(new char[] { 'v' }), s2);
            PrintStringLink2(s,"替换v为go后：");
            s.Insert(3, s2);
            PrintStringLink2(s,"3号位插入go后：");
            Console.WriteLine();

            Console.WriteLine("go的位置是："+ s.Index(s2));
            s.Delete(5, 1);
            PrintStringLink2(s,"删除5号位字符后：");
            s.Delete(1, s.GetLength());
            PrintStringLink2(s,"删除全部后：");
            s.ValueTo(new char[] { 'a', 'b', 'c' });
            PrintStringLink2(s,"赋值为abc后：");
            s.Concat(s2);
            PrintStringLink2(s,"链接go后：");
            Console.WriteLine("go的位置为："+s.Index(s2));
        }

        static void PrintStringLink2(StringLink2 s, string text = "输出：") {
            Console.Write(text);
            for (int i = 1; i <= s.GetLength(); i++) {
                char c = '\0';
                s.GetElementBuIndex(i, ref c);
                Console.Write(c);
            }
            Console.WriteLine();
        }

        static void TestStringLink() {
            int nodeLength = 3;
            StringLink s = new StringLink(new char[] { 'a', 'f', 'g', 'g', 's', 'k' }, nodeLength);
            PrintStringLink(s,"串s为：");
            StringLink s2 = new StringLink(new char[] { '5', '2', '0','a' }, nodeLength);
            PrintStringLink(s2, "串s2为：");
            s.Concat(s2);
            StringLink s3 = new StringLink(new char[] { 'l', 'o', 'v', 'e' }, 20);
            PrintStringLink(s,"串链接s2后：");
            Console.WriteLine();

            Console.WriteLine("在串s中查找串s2：的位置为："+s.Index(s2));
            s.Insert(5, new char[] { '1', '2', '8' });
            PrintStringLink(s,"在串s位置5插入128后：");
            s.Replace(s2, new StringLink(new char[] { 'l', 'u', 'o','9' }, nodeLength));
            PrintStringLink(s,"替换串s中的s2为luo9后：");
            Console.WriteLine();
            
            Console.WriteLine("在算中查找gg的位置为："+s.Index(new StringLink(new char[] { 'g', 'g' }, nodeLength)));
            s.Insert(9, s2);
            PrintStringLink(s,"在串s位置9前插入s2(520a)后：");
            s.Replace(new StringLink(new char[] { 'g', 'g' }, nodeLength), s2);
            PrintStringLink(s,"在串s中替换gg为s2(520a)后：");
            Console.WriteLine();

            PrintStringLink(s3, "串s3为：");
            s.Concat(s3);
            PrintStringLink(s,"串s链接串s3后：");
            s.Delete(2, 3);
            PrintStringLink(s,"删除串s位置为2长度为3的子串后：");
            s.Concat(s3);
            PrintStringLink(s,"串s链接串s3后：");
            s.Delete(1, 3);
            PrintStringLink(s, "删除串s位置为1长度为3的子串后：");
            s.Delete(6, 1);
            PrintStringLink(s, "删除串s位置为6长度为1的子串后：");
        }

        static void PrintStringLink(StringLink s,string text = "输出:") {

            Console.Write(text);
            for (int i = 0; i < s.GetLength(); i++) {
                Console.Write(s[i + 1]);
            }
            Console.WriteLine();
        }

        static void TestStringList() {
            //初始化和赋值
            StringList s1 = new StringList(new char[] { 'a', 'b', 'c', 'g', 'h' });
            StringList s2 = new StringList();
            s2.ValueTo(s1);
            PrintStringList(s1,"字符串s1:");
            PrintStringList(s2, "字符串s2:");
            s2.ValueTo(new char[] { 'h', 'l', 's' });
            PrintStringList(s2, "s2重新赋值后：");
            Console.WriteLine();

            //插入和删除操作
            PrintStringList(s1, "原s1串：");
            s1.Delete(2, 2);
            PrintStringList(s1, "删除bc后：");
            PrintStringList(s2, "原s2串：");
            s2.Insert(3, new char[] { '5', '2', '0' });
            PrintStringList(s2, "在3号位插入520后：");
            Console.WriteLine();

            //复制操作
            PrintStringList(s1, "原s1串：");
            s1.CopyTo(2, s2);
            PrintStringList(s1, "复制s2整个字符串在2号位后：");
            PrintStringList(s2, "原s2串：");
            s2.CopyTo(1, new char[] { 'a', 'b', 'c', 'g', 'h' }, 3, 3);
            PrintStringList(s2, "复制cgh到1号位后：");
            Console.WriteLine();

            //链接串操作
            PrintStringList(s1, "串1：");
            PrintStringList(s2, "串2：");
            StringList s3 = s1.ConcatToNew(s2);
            PrintStringList(s3, "链接s1和s2为s3后：");
            Console.WriteLine();

            //截取串操作
            PrintStringList(s3, "s3串为：");
            StringList s4 = s3.SubString(5, 8);
            PrintStringList(s4, "5到13的串为：");
            Console.WriteLine();
            
            //匹配字符串
            PrintStringList(s3, "字符串s3为：");
            Console.WriteLine("在s3中匹配s2的位置：");
            Console.WriteLine("从1开始："+s3.SimpleIndex(s2));
            Console.WriteLine("从1开始："+s3.Index(s2));
            StringList match = new StringList(new char[] { '5', '2', '0' });
            Console.WriteLine("在s3中匹配520的位置：");
            Console.WriteLine("从8开始："+s3.Index(match,8));
            Console.WriteLine("从1开始："+s3.SimpleIndex(match));
            Console.WriteLine();

            //索引错误测试
            char c = s3[200];
            s1.Insert(0, null);
            s3.CopyTo(-1, s4);

            char[] dataas = new char[1000000];
            for (int t = 0; t < 1000000; t++)
            {
                if (t == 999999)
                {
                    dataas[t] = '1';
                }
                else {
                    dataas[t] = '0';
                }
            }
            char[] searchhs = new char[100];
            for (int t = 0; t < 100; t++)
            {
                if (t == 99)
                {
                    searchhs[t] = '1';
                }
                else
                {
                    searchhs[t] = '0';
                }
            }
            StringList datas = new StringList(dataas);
            StringList search = new StringList(searchhs);
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            int res = datas.Index(search);
            stopwatch.Stop();
            Console.WriteLine("结果：" + res + " KMP:" + stopwatch.ElapsedMilliseconds);

            stopwatch.Reset();
            stopwatch.Start();
            res = datas.SimpleIndex(search);
            stopwatch.Stop();
            Console.WriteLine("结果：" + res + " Normal:" + stopwatch.ElapsedMilliseconds);
        }

        static void PrintStringList(StringList s, string text = "输出：") {
            Console.Write(text);
            for (int i = 1; i <= s.Length(); i++) {
                Console.Write(s[i]);
            }
            Console.WriteLine();
        }

        static void TestQueueLink() {
            QueueLink<int> q = new QueueLink<int>();
            q.EnQueue(6);
            q.EnQueue(5);
            q.EnQueue(80);
            Console.WriteLine("队列是否为空：" + q.isEmpty());
            Console.WriteLine("队列长度：" + q.GetLength());
            int e = 0;
            Console.Write("依次出队为：");
            while (!q.isEmpty())
            {

                q.DeQueue(ref e);
                Console.Write(e + " ");
            }
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("队列是否为空：" + q.isEmpty());
            Console.WriteLine("队列长度：" + q.GetLength());
            Console.WriteLine();

            q.EnQueue(22);
            q.EnQueue(33);
            Console.Write("依次出队为：");
            while (!q.isEmpty())
            {

                q.DeQueue(ref e);
                Console.Write(e + " ");
            }
        }

        static void TestQueueCycleList() {
            QueueCycleList<int> q = new QueueCycleList<int>(4);
            q.EnQueue(2);
            q.EnQueue(22);
            q.EnQueue(30);
            q.EnQueue(55);
            q.EnQueue(66);
            Console.WriteLine("队列是否为空："+q.isEmpty());
            Console.WriteLine("队列是否已满：" + q.isFull());
            Console.WriteLine("队列长度：" + q.GetLength());
            int e = 0;
            Console.Write("依次出队为：");
            while (!q.isEmpty()) {
                
                q.DeQueue(ref e);
                Console.Write(e+ " ");
            }
            Console.WriteLine();
            Console.WriteLine();


            q.EnQueue(30);
            q.DeQueue(ref e);
            q.DeQueue(ref e);
            Console.WriteLine("队列是否为空：" + q.isEmpty());
            Console.WriteLine("队列是否已满：" + q.isFull());
            Console.WriteLine("队列长度：" + q.GetLength());
            Console.WriteLine();


            q.EnQueue(66);
            q.EnQueue(22);
            Console.WriteLine("队列是否为空：" + q.isEmpty());
            Console.WriteLine("队列是否已满：" + q.isFull());
            Console.WriteLine("队列长度：" + q.GetLength());
            Console.Write("依次出队为：");
            while (!q.isEmpty())
            {

                q.DeQueue(ref e);
                Console.Write(e + " ");
            }
            Console.WriteLine();


            q.EnQueue(20);
            Console.WriteLine("队列长度：" + q.GetLength());
            q.Clear();
            Console.WriteLine("队列长度：" + q.GetLength());

        }

        static void TestCalculator() {
            while (true)
            {
                string exp = Console.ReadLine();
                Calculator c = new Calculator();
                string result = c.Cal(exp);

                Console.Write("匹配元素：");
                string s = "";
                for (int i = 1; i <= c._elements.GetListLength(); i++)
                {
                    c._elements.GetElement(i, ref s);
                    Console.Write(s + " ");
                }
                Console.WriteLine();

                Console.Write("后缀表达式：");
                for (int i = 1; i <= c._laterString.GetListLength(); i++)
                {
                    c._laterString.GetElement(i, ref s);
                    Console.Write(s + " ");
                }
                Console.WriteLine();

                Console.WriteLine("结果：" + result);
                Console.WriteLine();
            }
        }

        //斐波那契数列 递归实现  栈的应用
        static int Fbi(int i) {

            Console.WriteLine("Fbi(" + i + ") 被调用(入栈)");
            if (i < 2)
            {
                num++;
                Console.WriteLine("Fbi(" + i + ") 调用完毕（出栈）");
                return i == 0 ? 0 : 1;
            }

            int a = Fbi(i - 1);
            int b = Fbi(i - 2);

            Console.WriteLine("Fbi(" + i + ") 调用完毕（出栈）");
            return a + b;
        }

        static void TestStackLink() {
            //入栈
            StackLink<int> s = new StackLink<int>();
            PrintStackLink(s, "入栈前：");
            s.Push(2);
            s.Push(9);
            s.Push(10);
            PrintStackLink(s, "依次出栈输出：");
            Console.WriteLine();

            //清空栈测试
            s.Push(100);
            s.Push(200);
            s.ClearStack();
            PrintStackLink(s, "清空栈后：");
        }

        static void PrintStackLink(StackLink<int> s, string text = "输出：") {
            int element = 0;
            int length = s.GetLength();
            Console.Write(text);
            for (int i = 0; i < length; i++) {
                s.Pop(ref element);
                Console.Write(element.ToString() + " ");
            }
            Console.WriteLine();
        }

        static void TestDoubleStackList() {
            //入栈
            DoubleStackList<int> d = new DoubleStackList<int>();
            PrintDoubleStackList(d, "入栈前：");
            d.PushA(0);
            d.PushA(2);
            d.PushA(5);
            d.PushB(4);
            d.PushB(1);
            d.PushB(3);
            d.PushB(1);
            PrintDoubleStackList(d, "入栈后出栈输出：");
            d.PushA(66);
            d.PushB(33);
            PrintDoubleStackList(d, "再次入栈测试出栈：");
            Console.WriteLine();

            //测试栈满
            DoubleStackList<int> dd = new DoubleStackList<int>(3);
            dd.PushA(1);
            dd.PushB(2);
            dd.PushA(3);
            dd.PushA(66);
            PrintDoubleStackList(dd);
        }

        static void PrintDoubleStackList(DoubleStackList<int> d, string text = "输出：") {
            //输出A栈
            int element = 0;
            int length = d.GetLengthA();
            Console.Write("栈A ");
            for (int i = 0; i < length; i++) {
                d.PopA(ref element);
                Console.Write(element.ToString() + " ");
            }

            //栈B出栈并输出
            length = d.GetLengthB();
            Console.Write("栈B ");
            for (int i = 0; i < length; i++) {
                d.PopB(ref element);
                Console.Write(element.ToString() + " ");
            }

            Console.WriteLine();
        }

        static void TestStackList() {
            //进栈测试
            StackList<int> s = new StackList<int>();
            PrintStackList(s, "进栈前：");
            s.Push(5);
            s.Push(20);
            s.Push(33);
            PrintStackList(s, "依次进栈5，20，33后依次出栈：");
            Console.WriteLine(s.isEmpty());
            Console.WriteLine();

            //清空栈
            s.Push(99);
            s.Push(88);
            s.Push(22);
            s.ClearStack();
            PrintStackList(s, "清空栈后：");

            
        }

        static void PrintStackList(StackList<int> s, string text = "输出：") {
            int data = 0;
            int length = s.GetLength();

            Console.Write(text);
            for (int i = 0; i < length; i++) {
                s.Pop(ref data);
                Console.Write(data.ToString() + " ");
            }
            Console.WriteLine();
        }

        static void TestMyList() {

            //增加元素到最大值
            MyList<int> maxList = new MyList<int>(3);
            maxList.Add(2);
            maxList.Add(3);
            maxList.Add(6);
            PrintList(maxList, "添加过量元素前：");
            maxList.Add(8);
            PrintList(maxList, "添加过量的元素后：");
            Console.WriteLine();

            //插入元素
            MyList<int> myList = new MyList<int>();
            myList.Add(5);
            myList.Add(10);
            myList.Add(22);
            PrintList(myList, "插入元素前：");
            myList.Insert(2, 30);
            PrintList(myList, "在索引2插入元素30 ：");
            Console.WriteLine();

            //向已满线性表中插入元素
            PrintList(maxList, "已满线性表插入之前：");
            maxList.Insert(2, 30);
            PrintList(maxList, "向已满线性表索引2插入30：");
            Console.WriteLine();

            //通过索引删除元素
            PrintList(myList, "删除元素之前：");
            myList.DeleteByIndex(2);
            PrintList(myList, "删除索引2的元素后：");
            Console.WriteLine();

            //通过元素删除
            myList.Add(10);
            myList.Add(3);
            PrintList(myList, "通过元素删除之前：");
            myList.DeleteElement(10);
            PrintList(myList, "通过元素删除之后：");
            Console.WriteLine();

            //错误索引测试
            int someElement = 0;
            myList.GetElement(200, ref someElement);
            myList.GetElement(-20, ref someElement);
            myList.DeleteByIndex(100);
            myList.Insert(-2, 10);
        }

        static void PrintList(MyList<int> l, string text = "输出：") {
            int data = 0;

            Console.Write(text);
            for (int i = 0; i < l.GetListLength(); i++) {
                l.GetElement(i + 1, ref data);
                Console.Write(data.ToString() + " ");
            }
            Console.WriteLine();
        }

        static void TestLink() {

            //增加一个元素，删除两个元素
            MyLink<int> myLink = new MyLink<int>();
            myLink.Add(2);
            PrintLink(myLink, "增加一个元素后：");
            myLink.DeleteByIndex(1);
            PrintLink(myLink, "删除一个元素后：");
            myLink.DeleteByIndex(1);
            PrintLink(myLink, "再删除空链表的一个元素后：");
            Console.WriteLine();

            //快速增加元素与插入元素
            myLink.AddQuick(5);
            PrintLink(myLink, "快速添加3个元素后：");
            myLink.Insert(1, 80);
            PrintLink(myLink, "向索引1前插入80后：");
            myLink.DeleteByIndex(1);
            myLink.DeleteByIndex(1);
            PrintLink(myLink, "连续删除第一个元素后：");
            Console.WriteLine();

            //在头部添加两个元素，在快速插入一个
            myLink.AddToHead(20);
            PrintLink(myLink, "第一次在头部添加元素后：");
            myLink.AddToHead(10);
            PrintLink(myLink, "第二次在头部添加元素后：");
            myLink.AddQuick(30);
            PrintLink(myLink, "快速添加元素后：");
            myLink.DeleteByIndex(3);
            PrintLink(myLink, "删除第三个元素后：");
            myLink.AddQuick(5);
            PrintLink(myLink, "在快速添加元素5:");
            Console.WriteLine();

            //批量添加和插入元素
            int[] array = { 1, 3, 6 };
            myLink.AddVolume(array);
            PrintLink(myLink, "批量插入1,3,6后：");
            myLink.InsertVolume(2, array);
            PrintLink(myLink, "在索引2前批量插入1，3，6后：");
            myLink.Add(55);
            myLink.AddQuick(66);
            PrintLink(myLink, "添加55，和快速添加66后：");
            Console.WriteLine();

            //索引非法检测
            int data = 0;
            myLink.GetElement(0, ref data);
            myLink.GetElement(100, ref data);
            myLink.Insert(30, 20);
            myLink.DeleteByIndex(-1);
            myLink.DeleteByIndex(200);
        }

        static void PrintLink(MyLink<int> l, string text = "输出：") {
            int data = 0;

            Console.Write(text);
            for (int i = 0; i < l.GetLinkLength(); i++) {
                l.GetElement(i + 1, ref data);
                Console.Write(data.ToString() + " ");
            }
            Console.WriteLine();
        }

        static void TestStaticLink() {
            int element = 0;

            //添加与加满测试
            StaticLink<int> s = new StaticLink<int>(3);
            s.Add(2);
            s.AddQuick(3);
            s.Add(5);
            PrintStaticLink(s, "加满三个元素后：");
            s.AddQuick(6);
            PrintStaticLink(s, "在往满的链表中添加元素：");
            Console.WriteLine();

            //删除元素测试
            PrintStaticLink(s, "删除元素前：");
            s.Delete(2, ref element);
            PrintStaticLink(s, "删除第二个元素后：");
            s.Delete(1, ref element);
            PrintStaticLink(s, "再删除第一个元素后：");
            Console.WriteLine();

            //插入元素测试
            PrintStaticLink(s, "插入元素之前：");
            s.Insert(1, 100);
            PrintStaticLink(s, "在一号前插入100后：");
            s.Insert(2, 200);
            PrintStaticLink(s, "在二号位插入200后：");
            s.Insert(3, 20);
            PrintStaticLink(s, "往已满链表插入元素：");
            Console.WriteLine();

            //索引测试
            s.Insert(5, 2);
            s.Delete(-1, ref element);
            s.Delete(100, ref element);
            s.Insert(-2, 3);
            s.GetElement(100, ref element);

        }

        static void PrintStaticLink(StaticLink<int> s, string text = "输出：") {
            int data = 0;

            Console.Write(text);
            for (int i = 0; i < s.GetStaticLinkLength(); i++)
            {
                s.GetElement(i + 1, ref data);
                Console.Write(data.ToString() + " ");
            }
            Console.WriteLine();
        }

        static void TestCycleLink() {

            //添加测试
            int element = 0;
            CycleLink<int> c = new CycleLink<int>();
            c.Add(3);
            PrintCycleLink(c, "添加一个元素后：");
            c.Delete(1, ref element);
            c.AddAtFrist(5);
            c.Add(8);
            c.AddAtFrist(10);
            PrintCycleLink(c, "删除一个元素后再加三个元素：");
            Console.WriteLine();

            //插入和删除
            c.Insert(2, 100);
            PrintCycleLink(c, "在2号位插入100后：");
            c.Delete(3, ref element);
            PrintCycleLink(c, "删除第三个元素后：");
            c.Delete(1, ref element);
            PrintCycleLink(c, "再删除第一个元素后：");
            Console.WriteLine();

            //通过一个节点遍历链表
            c.Add(1001);
            c.AddAtFrist(22);
            c.Add(88);
            Console.Write("通过第二个节点遍历整个链表：");
            c.CycleLinkByNode(c.GetNodeByIndex(2));
            Console.WriteLine(); Console.WriteLine();


            //索引测试
            c.GetElement(-1, ref element);
            c.Delete(200, ref element);
            c.GetNodeByIndex(-2);
            c.Insert(20, 30);
            Console.WriteLine();

            //测试链接两链表
            CycleLink<int> c2 = new CycleLink<int>();
            c2.Add(99);
            c2.Add(33);
            c2.Add(22);
            PrintCycleLink(c, "链接链表前：");
            c.Connect(c2);
            PrintCycleLink(c, "链接链表后：");
            c.Add(0);
            PrintCycleLink(c, "链接链表后添加元素：");
        }

        static void PrintCycleLink(CycleLink<int> c, string text = "输出：") {
            Console.Write(text);
            c.CycleLinkByNode(c.GetNodeByIndex(0));
            Console.WriteLine();
        }

        static void TestDoubleLink() {

            //添加测试
            DoubleLink<int> d = new DoubleLink<int>();
            d.Add(2);
            d.Add(90);
            d.AddAtFirst(32);
            d.Add(8);
            PrintDoubleLink(d,"添加四个元素后");
            Console.WriteLine();

            //插入测试
            PrintDoubleLink(d, "插入前：");
            d.Inset(1, 88);
            PrintDoubleLink(d, "一号位插入88后：");
            d.Inset(5, 60);
            PrintDoubleLink(d, "五号位插入60后：");
            d.AddAtFirst(99);
            PrintDoubleLink(d, "在头部添加99后：");
            Console.WriteLine();

            //删除测试
            int data = 0;
            PrintDoubleLink(d, "删除元素前：");
            d.Delete(4, ref data);
            PrintDoubleLink(d, "删除第四个元素后：");
            d.Delete(1, ref data);
            d.Delete(1, ref data);
            d.Delete(1, ref data);
            d.Delete(1, ref data);
            d.Delete(1, ref data);
            d.Delete(1, ref data);
            d.Delete(1, ref data);
            PrintDoubleLink(d, "删除完元素后：");
            d.Add(30);
            d.AddAtFirst(20);
            PrintDoubleLink(d, "在添加两个元素后：");
            Console.WriteLine();

            //索引测试
            d.Delete(30, ref data);
            d.Inset(0, 1);
            d.GetElement(0 ,ref data);
            d.GetElement(-1, ref data);
        }


        static void PrintDoubleLink(DoubleLink<int> d, string text = "输出：")
        {
            int data = 0;

            Console.Write(text);
            for (int i = 0; i < d.GetLength(); i++)
            {
                d.GetElement(i + 1, ref data);
                Console.Write(data.ToString() + " ");
            }
            Console.WriteLine();
        }
    }
}
