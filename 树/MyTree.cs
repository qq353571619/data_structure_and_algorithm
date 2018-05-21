using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 树
{
    class MyTree<T>
    {
        public class Node {
            public MyTree<T> tree = null;
            public T data = default(T);
            public Node[] childs = null;

            public Node(MyTree<T> tree) {
                this.tree = tree;
            }

            public void ValueTo(T data) {
                this.data = data;
            }
        }

        //头结点，头结点的child[0]就表示根节点
        private Node _headNode = null;
        private int _length = 0;
        
        public MyTree() {
            this._headNode = new Node(this);
        }

        //指定根的数据
        public MyTree(T data) {
            this._headNode = new Node(this);

            Node newNode = new Node(this);
            newNode.data = data;
            this._headNode.childs = new Node[1];
            this._headNode.childs[0] = newNode;
            
            this._length = 1;
        }

        //创建树的根
        public bool CreatRoot(T data) {
            if (!this.isEmpty()) {
                Console.WriteLine("树的根已存在，不需要重新创建！");
                return false;
            }

            Node root = new Node(this);
            root.data = data;
            this._headNode.childs = new Node[1];
            this._headNode.childs[0] = root;
            this._length = 1;

            return true;
        }

        //为指定结点添加子节点
        public void AddNodeTo(Node toNode, T data) {

            if (!this.isMyNode(toNode)){
                Console.WriteLine("你所操作的结点不是本树的结点！");
                return;
            }

            Node addNode = new Node(this);
            addNode.data = data;

            Node[] temp = toNode.childs;
            int childLength = (temp == null ? 1 : temp.Length + 1);
            toNode.childs = new Node[childLength];
            int i = 0;
            for (; i < childLength-1; i++) {
                toNode.childs[i] = temp[i];
            }

            toNode.childs[i] = addNode;
            this._length++;
        }

        //为指定位置的结点添加子节点
        public void AddNodeTo(int index, T data) {
            if (!this._checkIndex(index)){
                Console.WriteLine("目标位置的结点不存在！");
                return;
            }

            Node toNode = this.GetNodeByIndex(index);
            this.AddNodeTo(toNode, data);
        }

        //删除目标位置的结点及其子节点
        public void DeleteByIndex(int index) {
            if (!this._checkIndex(index)){
                Console.WriteLine("索引错误！");
                return;
            }
            
            if (index == 1) {
                //删除树的根，就是删除整棵树
                this._headNode.childs[0] = null;
                this._length = 0;
                return;
            }

            int local = 0;
            Node goNode = this.GetNodeParentAndLocalByIndex(index, ref local);

            if (goNode == null) {
                Console.WriteLine("删除失败，请检查索引是否正确！");
                return;

            }

            //删除双亲结点对目标子节点的引用，表示删除
            Node[] temp = goNode.childs;
            goNode.childs = new Node[temp.Length - 1];
            for (int i = 0; i < temp.Length; i++) {
                if (i < local - 1)
                {
                    goNode.childs[i] = temp[i];
                }
                else if (i > local - 1) {
                    goNode.childs[i - 1] = temp[i];
                }
            }

            this._length--;
        }

        //层次遍历返回数组
        public T[] GetDatasByLevels() {
            if (this.isEmpty()) {
                Console.WriteLine("树为空树！");
                return null;
            }

            Queue<Node> q = new Queue<Node>();
            q.Enqueue(this._headNode.childs[0]);

            Node goNode = null;
            T[] datas = new T[this._length];
            int goIndex = 0;
            while (q.Count != 0) {
                goNode = q.Dequeue();
                datas[goIndex] = goNode.data;
                int num = goNode.childs == null ? 0 : goNode.childs.Length;
                for (int i = 0; i < num; i++) {
                    q.Enqueue(goNode.childs[i]);
                }
                goIndex++;
            }

            return datas;
        }

        //返回指定位置的结点
        public Node GetNodeByIndex(int index) {
            if (!this._checkIndex(index)) {
                Console.WriteLine("索引错误!");
                return null;
            }

            if (this.isEmpty()) {
                Console.WriteLine("此树为空树！");
                return null;
            }

            //将头结点加入到队列中
            Queue<Node> q = new Queue<Node>();
            q.Enqueue(this._headNode.childs[0]);

            int goindex = 1;
            Node goNode = this._headNode;
            while (goindex < index) {
                goNode = q.Dequeue();
                int nodeLength = goNode.childs.Length;
                goindex += nodeLength;

                for (int i = 0; i < nodeLength; i++) {
                    q.Enqueue(goNode.childs[i]);
                }
            }

            int sub = goNode.childs.Length - goindex + index - 1;
            return goNode.childs[sub];
        }

        //返回指定位置结点的父节点及其它为第几位
        public Node GetNodeParentAndLocalByIndex(int index,ref int local)
        {
            if (!this._checkIndex(index))
            {
                Console.WriteLine("索引错误！");
                return null;
            }

            if (index == 1) {
                //根节点没有双亲
                Console.WriteLine("根节点没有双亲！");
                return null;
            }

            if (this.isEmpty())
            {
                Console.WriteLine("此树为空树！");
                return null;
            }

            //将头结点加入到队列中
            Queue<Node> q = new Queue<Node>();
            q.Enqueue(this._headNode.childs[0]);

            int goindex = 1;
            Node goNode = this._headNode;
            while (goindex < index)
            {
                goNode = q.Dequeue();
                int nodeLength = goNode.childs.Length;
                goindex += nodeLength;

                for (int i = 0; i < nodeLength; i++)
                {
                    q.Enqueue(goNode.childs[i]);
                }
            }

            local = goNode.childs.Length - goindex + index ;
            return goNode;
        }

        //清空树
        public void Clear() {
            this._headNode.childs = null;
            this._length = 0;
        }

        //获取指定位置的数据
        public T GetElementByIndex(int index) {
            Node target = this.GetNodeByIndex(index);
            if (target != null) {
                return target.data;
            }

            Console.WriteLine("获取结点数据出错！");
            return default(T);
        }

        //返回结点的个数
        public int GetLength() {
            return this._length;
        }

        //返回树的高度
        public int GetHeight() {

            if (this.isEmpty()) {
                Console.WriteLine("此树为空！");
                return 0;
            }

            int height = 0;
            Queue<Node> q = new Queue<Node>();
            q.Enqueue(this._headNode.childs[0]);//根结点入队列
            int count1 = 1;
            int count2 = 1;
            Node temp;

            while (count2 != 0) {
                height++;
                count2 = 0;
                for (int i = 0; i < count1; i++) {
                    temp = q.Dequeue();
                    if (temp.childs != null) {
                        count2 += temp.childs.Length;
                        for (int j = 0; j < temp.childs.Length; j++)
                        {
                            q.Enqueue(temp.childs[j]);
                        }
                    }
                }

                count1 = count2;
            }

            return height;
        }

        //判断是否为空树
        public bool isEmpty() {
            if (this._headNode.childs == null) {
                return true;
            }

            return false;
        }

        //检查是否为该树的结点
        public bool isMyNode(Node target) {
            if (target.tree == this) {
                return true;
            }

            return false;
        }

        //检查索引是否合法
        private bool _checkIndex(int index) {
            if (index < 1 || index > this.GetLength()) {
                return false;
            }

            return true;
        }
    }
}
