using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 线性表
{
    //单链表实现
    class MyLink<T>
    {
        class Node{
            public T data;
            public Node next;
        }


        private Node _head;
        private Node _last;
        private int _curentLinkLength;

        //构造函数，创建头结点，空链表
        public MyLink() {
            _head = new Node();
            _head.next = null;
            _last = _head;
            this._curentLinkLength = 0;
        }

        //获取链表中的第i位元素
        public bool GetElement(int index, ref T element) {
            Node go = this.GetNodeByIndex(index);

            if (go == null||go == this._head)
            {
                Console.WriteLine("索引非法！");
                return false;
            }

            element = go.data;
            return true;
        }

        //向链表后面添加元素
        public bool Add(T element) {
            Node last = _head;
            while (last.next != null) {
                last = last.next;
            }


            //此时last为链表的最后元素
            Node add = new Node();
            add.data = element;
            add.next = null;
            last.next = add;
            this._last = add;
            this._curentLinkLength++;

            return true;
        }

        //向链表后面快速添加元素
        public bool AddQuick(T element) {

            Node add = new Node();
            add.data = element;
            add.next = null;
            
            this._last.next = add;
            this._last = add;
            this._curentLinkLength++;
            return true;

        }

        //向链表头部增加元素
        public bool AddToHead(T element) {
            Node add = new Node();
            add.data = element;
            add.next = _head.next;
            if (_head.next == null) {
                //判断是否是空链表，如果是，尾结点就是插入结点
                this._last = add;
            }

            _head.next = add;
            this._curentLinkLength++;
            return true;
        }

        //向后批量增加元素
        public bool AddVolume(T[] elements) {
            for (int i = 0; i < elements.Length; i++) {
                this.AddQuick(elements[i]);
            }

            return true;
        }

        //向链表索引i前插入元素
        public bool Insert(int index, T element) {
            Node go = this.GetNodeByIndex(index - 1);

            if (go == null || go.next == null)
            {
                Console.WriteLine("索引非法！");
                return false;
            }

            Node add = new Node();
            add.data = element;
            add.next = go.next;
            go.next = add;
            this._curentLinkLength++;

            return true;
        }

        //批量插入元素
        public bool InsertVolume(int index, T[] elements) {
            Node go = this.GetNodeByIndex(index - 1);
            if (go == null || go.next == null) {
                Console.WriteLine("索引非法!");
                return false;
            }
            for (int i = 0; i < elements.Length; i++) {
                Node add = new Node();
                add.data = elements[i];
                add.next = go.next;
                go.next = add;
                go = add;
                this._curentLinkLength++;
            }

            return true;
        }

        //通过索引删除元素
        public T DeleteByIndex(int index) {
            

            Node go = this.GetNodeByIndex(index - 1);

            if (go == null || go.next == null) {
                Console.WriteLine("索引非法！");
                return default(T);
            }
            

            Node p = go.next;
            T old = p.data;
            go.next = p.next;
            p = null;

            if (index == this._curentLinkLength)
            {
                //说明删除的是最后一个元素
                this._last = go;
            }
            this._curentLinkLength--;
            return old;
        }

        //获取链表的长度
        public int GetLinkLength() {
            return this._curentLinkLength;
        }

        //获取指定位置的节点
        private Node GetNodeByIndex(int index) {
            Node go = this._head;
            int i = 0;
            while (go != null && i < index) {
                go = go.next;
                i++;
            }

            return go;
        }

    }
}
