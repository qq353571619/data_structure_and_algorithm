using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 线性表
{
//循环链表的实现
public class CycleLink<T>
{
    public class Node {
        public T data;
        public Node next;
    }

    //定义一个指向尾结点的引用
    private Node _last;
    private int _currentLenth = 0;

        public CycleLink() {
            //新建头结点
            Node head = new Node();
            
            _last = head; 
            _last.next = head;

        }

        public void Clear()
        {
            Node head = new Node();

            _last = head;
            _last.next = head;
            _currentLenth = 0;
        }

        //在循环链表尾部添加结点
        public bool Add(T element) {
            Node addNode = new Node();
            addNode.data = element;
            addNode.next = this._last.next;
            this._last.next = addNode;
            this._last = addNode;
            this._currentLenth++;
            return true;
        }

        //在循环链表头部添加结点
        public bool AddAtFrist(T element) {
            //获取到头结点
            Node head = this._last.next;

            Node addNode = new Node();
            addNode.data = element;
            addNode.next = head.next;
            head.next = addNode;
            if (head == this._last) {
                //为空链表时
                this._last = addNode;
            }
            this._currentLenth++;
            return true;
        }

        //在循环链表指定位置插入结点
        public bool Insert(int index,T element) {
            Node preNode = this.GetNodeByIndex(index - 1);
            if (preNode == null || preNode.next == this._last.next) {
                Console.WriteLine("插入位置的结点不存在！");
                return false;
            }

            Node addNode = new Node();
            addNode.data = element;
            addNode.next = preNode.next;
            preNode.next = addNode;
            this._currentLenth++;
            return true;
        }

        //删除指定位置的结点
        public bool Delete(int index, ref T element) {
            Node preNode = this.GetNodeByIndex(index - 1);

            if (preNode == null || preNode.next == this._last.next) {
                Console.WriteLine("要删除的结点不存在！");
                return false;
            }

            Node deleteNode = preNode.next;
            if (deleteNode == this._last) {
                this._last = preNode;
            }
            element = deleteNode.data;
            preNode.next = deleteNode.next;
            deleteNode = null;
            this._currentLenth--;
            return true;
        }

        //获取指定位置的结点数据
        public bool GetElement(int index,ref T element){
            if (index == 0) {
                Console.WriteLine("头结点时没有数据的！");
                return false;
            }

            Node node = this.GetNodeByIndex(index);

            if (node == null) {
                return false;
            }

            element = node.data;
            return true;
        }

        //通过某个节点遍历整个链表
        public void CycleLinkByNode(Node node) {
            Node goNode = node.next;
            if(node != this._last.next){
                Console.Write(node.data.ToString() + " ");
            }
            

            while (goNode != node) {
                if(goNode != this._last.next)
                Console.Write(goNode.data.ToString()+ " ");

                goNode = goNode.next;
            }
        }

        //链接两条链表
        public bool Connect(CycleLink<T> C) {
            Node head1 = this._last.next;
            //取添加的链表的第一个结点
            Node first2 = C._last.next.next;
            this._last.next = first2;
            C._last.next = head1;
            this._currentLenth += C._currentLenth;
            this._last = C._last;

            return true;
        }

        //获取指定位置的结点
        public Node GetNodeByIndex(int index) {
            if (index < 0 || index > this._currentLenth) {
                Console.WriteLine("索引非法！");
                return null;
            }

            Node head = this._last.next;
            Node node = head;
            int i = 0;
            while (i != index && node.next != head) {
                i++;
                node = node.next;
            }

            //其实上面已经判断过索引的合法性了，这里是没必要的
            //但是写出来可以更好的理解上面的循环
            if (i < index) {
                Console.WriteLine("索引非法！");
                return null;
            }

            return node;
        }

        public int GetCycleLinkLength() {
            return this._currentLenth;
        }
    }
}
