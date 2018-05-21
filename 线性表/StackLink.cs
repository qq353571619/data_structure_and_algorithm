using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 线性表
{
    //栈的链式存储结构
    class StackLink<T>
    {
        class Node {
            public T data;
            public Node next;
        }

        //栈顶指向链表的第一个结点
        private Node _top;
        private int _currentLenth = 0;

        //构造空链表
        public StackLink() {
            this._top = null;
        }

        //进栈操作
        public bool Push(T element) {

            Node addNode = new Node();
            addNode.data = element;
            addNode.next = this._top;
            this._top = addNode;

            this._currentLenth++;
            return true;
        }

        //出栈操作
        public bool Pop(ref T element) {

            if (this._top == null) {
                Console.WriteLine("该栈为空！");
                return false;
            }

            Node deleteNode = this._top;
            element = deleteNode.data;
            this._top = deleteNode.next;
            deleteNode = null;
            this._currentLenth--;
            return true;
        }

        //获取栈顶元素不删除
        public bool GetTop(ref T element) {

            if (this._top == null)
            {
                Console.WriteLine("该栈为空！");
                return false;
            }

            element = this._top.data;
            return true;
        }

        //清空栈
        public bool ClearStack() {

            Node deleteNode = this._top;

            while (deleteNode != null) {
                //这样写其实没有必要，这里只是为了突出删除
                //c#的堆内存如果没有变量引用的话，它是会被GC回收的
                Node goNode = deleteNode.next;
                deleteNode = null;
                deleteNode = goNode;
            }

            this._top = null;
            this._currentLenth = 0;
            return true;

        }

        //是否为空
        public bool isEmpty() {
            if (this._top == null) {
                return true;
            }

            return false;
        }

        //获取当前长度
        public int GetLength() {
            return this._currentLenth;
        }

    }
}
