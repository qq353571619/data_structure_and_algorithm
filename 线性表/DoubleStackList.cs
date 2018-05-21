using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 线性表
{
    //两栈共享内存
    class DoubleStackList<T>
    {
        //_topA为-1时，栈A我空
        //_topB为数组最大长度时，栈B为空
        private int _topA;
        private int _topB;

        private int _currentLengthA = 0;
        private int _currentLengthB = 0;
        private int _maxLength = 24;

        private T[] _data;

        //初始化为两个空栈
        public DoubleStackList() {
            this._data = new T[this._maxLength];
            this._topA = -1;
            this._topB = this._maxLength;
        }

        public DoubleStackList(int length) {
            if (length > 0) {
                this._maxLength = length;
            }

            this._data = new T[this._maxLength];
            this._topA = -1;
            this._topB = this._maxLength;
        }

        //入栈操作A
        public bool PushA(T element) {
            if (this._topB == this._topA + 1) {
                Console.WriteLine("共享的空间已满！");
                return false;
            }

            this._topA++;
            this._data[this._topA] = element;
            this._currentLengthA++;

            return true;
        }

        //入栈操作B
        public bool PushB(T element) {
            if (this._topB == this._topA + 1) {
                Console.WriteLine("共享的空间已满！");
                return false;
            }

            this._topB--;
            this._data[this._topB] = element;
            this._currentLengthB++;

            return true;
        }

        //出栈操作A
        public bool PopA(ref T element) {
            if (this._topA == -1) {
                Console.WriteLine("栈A为空！！");
                return false;
            }

            element = this._data[this._topA];
            this._data[this._topA] = default(T);
            this._topA--;
            this._currentLengthA--;

            return true;
        }

        //出栈操作B
        public bool PopB(ref T element)
        {
            if (this._topB == this._maxLength) {
                Console.WriteLine("栈B为空！！");
                return false;
            }

            element = this._data[this._topB];
            this._data[this._topB] = default(T);
            this._topB++;
            this._currentLengthB--;

            return true;
        }

        //获取栈A长度
        public int GetLengthA() {
            return this._currentLengthA;
        }

        //获取栈B长度
        public int GetLengthB() {
            return this._currentLengthB;
        }
    }
}
