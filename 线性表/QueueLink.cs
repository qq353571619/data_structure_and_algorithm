using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 线性表
{
        class QueueLink<T>
        {
            class Node {
                public T data;
                public Node next;
            }

            private Node _front;
            private Node _rear;
            private int _currentLength = 0;

            public QueueLink() {
                //新建一个头结点
                Node head = new Node();
                head.next = null;
                this._front = this._rear = head;
            }

            //入队
            public bool EnQueue(T element) {
                Node addNode = new Node();
                addNode.data = element;
                addNode.next = this._rear.next;

                this._rear.next = addNode;
                this._rear = addNode;
                this._currentLength++;
                return true;
            }

            //出队
            public bool DeQueue(ref T element) {
                if (this.isEmpty()) {
                    Console.WriteLine("队列为空！");
                    return false;
                }

                Node getNode = this._front.next;
                element = getNode.data;

                if (this._rear == getNode) {
                    //队列中只有一个元素
                    this._rear = this._front;
                }
                this._front.next = getNode.next;
                getNode = null;
                this._currentLength--;
                return true;
            }

            //清空
            public void Clear() {
                Node deleteNode = this._front.next;
                while (deleteNode != null) {
                    Node go = deleteNode.next;
                    deleteNode = null;
                    deleteNode = go;
                }

                this._rear = this._front;
                this._currentLength = 0;
            }

            public bool isEmpty() {
                if (this._front == this._rear) {
                    return true;
                }

                return false;
            }

            public int GetLength() {
                return this._currentLength;
            }
        }
}
