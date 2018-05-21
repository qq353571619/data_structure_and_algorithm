using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 线性表
{
    //队列的顺序存储结构-循环队列
    class QueueCycleList<T>
    {
        private T[] _data;
        private int _front;
        private int _rear;
        private int _maxLength = 24;

        public QueueCycleList() {
            //留一个空位，用于判断队满的情况
            this._data = new T[this._maxLength+1];
            _front = _rear = 0;
        }

        public QueueCycleList(int length) {
            if (length > 0) {
                this._maxLength = length;
            }

            this._data = new T[this._maxLength+1];
            this._front = this._rear = 0;
        }

        //入队
        public bool EnQueue(T element) {
            if (this.isFull()) {
                Console.WriteLine("队列已满！");
                return false;
            }

            this._data[this._rear] = element;
            //指标向下移动
            this._rear = (this._rear + 1) % (this._maxLength + 1);
            return true;
        }

        //出队
        public bool DeQueue(ref T element) {
            if (this.isEmpty()) {
                Console.WriteLine("队列为空！");
                return false;
            }

            element = this._data[this._front];
            //指标向前移动
            this._front = (this._front + 1) % (this._maxLength + 1);
            return true;
        }

        //获得对头元素，不移除
        public bool GetHead(ref T element) {
            if (this.isEmpty())
            {
                Console.WriteLine("队列为空！");
                return false;
            }

            element = this._data[this._front];
            return true;
        }

        //清空
        public void Clear() {
            while (this._front != this._rear) {
                this._data[this._front] = default(T);
                this._front = (this._front + 1) % (this._maxLength + 1);
            }

            this._rear = this._front = 0;
        }

        public bool isEmpty() {
            if (this._rear == this._front) {
                return true;
            }

            return false;
        }

        public bool isFull() {
            int next = (this._rear + 1) % (this._maxLength + 1);
            if (next == this._front) {
                return true;
            }

            return false;
        }

        //获取循环队列长度
        public int GetLength() {
            int maxLength = this._maxLength + 1;
            return (this._rear - this._front + maxLength) % maxLength;
        }
    }
}
