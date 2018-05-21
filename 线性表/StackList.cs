using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 线性表
{
    //栈的顺序存储结构
    class StackList<T>
    {
        //用于记录栈顶的下标
        //-1表示空栈
        private int _top = -1;

        private int _currentLength = 0;
        private int _maxLength = 24;

        private T[] _data;

        public StackList() {
            _data = new T[this._maxLength];
        }

        public StackList(int length) {
            if (length > 0) {
                this._maxLength = length;
            }
            _data = new T[this._maxLength];
        }

        //入栈操作
        public bool Push(T element) {
            if (this._currentLength >= this._maxLength) {
                Console.WriteLine("栈的空间已满！");
                return false;
            }

            this._top++;
            this._data[this._top] = element;
            this._currentLength++;

            return true;
        }

        //出栈操作
        public bool Pop(ref T element) {
            if (_top == -1) {
                Console.WriteLine("栈顶元素为空！");
                return false;
            }

            element = this._data[this._top];
            this._data[this._top] = default(T);
            this._top--;
            this._currentLength--;

            return true;
        }

        //获取栈顶元素，不删除
        public bool GetTop(ref T element) {
            if (_top == -1) {
                Console.WriteLine("栈顶元素为空！");
                return false;
            }

            element = this._data[this._top];
            return true;
        }

        //是否为空
        public bool isEmpty() {
            if (this._top == -1) {
                return true;
            }

            return false;
        }

        //清空栈
        public bool ClearStack() {
            for (int i = this._currentLength-1; i >= 0; i--) {
                this._data[i] = default(T);
            }

            this._top = -1;
            this._currentLength = 0;

            return true;
        }

        //获取栈的长度
        public int GetLength() {
            return this._currentLength;
        }

        //获取栈的最大长度
        public int GetMaxLength() {
            return this._maxLength;
        }
    }
}
