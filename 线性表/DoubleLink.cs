using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 线性表
{
    class DoubleLink<T>
    {
        class Node {
            public T data;
            public Node pre;
            public Node next;
        }

        private Node _head;
        private Node _last;
        private int _currentLength = 0;

        //初始化为只有头结点的空链表
        public DoubleLink() {
            _head = new Node();
            _head.pre = null;
            _head.next = null;

            _last = _head;
        }

        //添加元素在尾部
        public bool Add(T element) {
            Node addNode = new Node();
            addNode.data = element;
            addNode.pre = this._last;
            addNode.next = null;
            this._last.next = addNode;
            this._last = addNode;

            this._currentLength++;
            return true;
        }

        //在头部添加元素
        public bool AddAtFirst(T element) {
            Node addNode = new Node();
            addNode.data = element;
            addNode.pre = this._head;
            addNode.next = this._head.next;

            if (this._head.next != null)
            {
                this._head.next.pre = addNode;
                this._head.next = addNode;
            }
            else {
                //为空链表
                this._last = addNode;
            }

            this._currentLength++;
            return true;
        }

        //获取某个位置的值
        public bool GetElement(int index, ref T element) {
            //排除头结点
            if (index == 0) {
                Console.WriteLine("头结点没有数据!");
                return false;
            }

            Node node = this.GetNodeByIndex(index);

            if (node == null) {
                return false;
            }

            element = node.data;
            return true;
        }

        //插入结点
        public bool Inset(int index, T element) {
            Node preNode = this.GetNodeByIndex(index - 1);
            if (preNode == null || preNode.next == null) {
                return false;
            }

            Node addNode = new Node();
            addNode.data = element;
            addNode.pre = preNode;
            addNode.next = preNode.next;

            preNode.next.pre = addNode;
            preNode.next = addNode;

            this._currentLength++;
            return true;
        }

        //删除某个位置的结点
        public bool Delete(int index, ref T element) {
            Node preNode = this.GetNodeByIndex(index - 1);

            if (preNode == null || preNode.next == null) {
                Console.WriteLine("删除的元素不存在！");
                return false;
            }

            Node deleteNode = preNode.next;
            element = deleteNode.data;

            if (index == this._currentLength)
            {
                this._last = preNode;
            }
            else {
                deleteNode.next.pre = preNode;
            }
            
            preNode.next = deleteNode.next;
            deleteNode = null;

            

            this._currentLength--;
            return true;
        }


        //获取某个位置的结点
        private Node GetNodeByIndex(int index) {
            if (index < 0 || index > this._currentLength) {
                Console.WriteLine("索引错误！");
                return null;
            }

            int i;
            Node goNode;

            if (index < this._currentLength / 2 + 1)
            {
                //想获得的结点靠前，通过前序向后遍历
                i = 0;
                goNode = this._head;
                while (i != index && goNode.next != null)
                {
                    goNode = goNode.next;
                    i++;
                }
                
            }
            else
            {
                //后序向前遍历寻找结点
                i = this._currentLength;
                goNode = this._last;

                while (i != index && goNode.pre != null) {
                    goNode = goNode.pre;
                    i--;
                }
            }

            return goNode;
        }

        //获取长度
        public int GetLength() {
            return this._currentLength;
        }


    }
}
