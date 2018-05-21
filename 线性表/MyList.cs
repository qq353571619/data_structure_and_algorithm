using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 线性表
{
    //线性表的顺序存储结构
    public class MyList<T>
    {
        private T[] _data;
        private int _maxLength = 24;
        private int _currentLength = 0;

        //默认构造函数
        public MyList() {
            this._data = new T[this._maxLength];
        }

        //自定义线性表的长度
        public MyList(int length) {
            if (length <= 0) {
                this._data = new T[this._maxLength];
            }
            else
            {
                this._data = new T[length];
                this._maxLength = length;
            }
        }

        //添加元素
        public bool Add(T element) {
            if (this._currentLength < this._maxLength) {
                this._data[this._currentLength] = element;
                this._currentLength++;
                return true;
            }

            Console.WriteLine("元素已满！！");
            return false;
        }

        //插入元素
        public bool Insert(int index, T element) {
            if (index < 1 || index > this._currentLength) {
                Console.WriteLine("索引非法！");
                return false;
            }

            if (this._currentLength < this._maxLength) {
                for (int i = this._currentLength-1; i >= index-1; i--) {
                    this._data[i + 1] = this._data[i];
                }

                this._data[index - 1] = element;
                this._currentLength++;
                return true;
            }

            Console.WriteLine("不能向已满的表插入元素！");
            return false;
        }

        //通过索引删除元素
        public T DeleteByIndex(int index) {
            if (index < 1 || index > this._currentLength) {
                Console.WriteLine("索引非法！");
                return default(T);
            }

            T deleteData = this._data[index - 1];

            for (int i = index - 1; i < this._currentLength - 1; i++) {
                this._data[i] = this._data[i + 1];
            }
            this._currentLength--;
            return deleteData;
        }

        //删除元素
        public bool DeleteElement(T element) {
            int index = this.GetIndexByElement(element);
            if (index != -1) {
                this.DeleteByIndex(index);
                return true;
            }

            return false;
        }


        //通过索引获取元素
        public bool GetElement(int index, ref T element) {
            if (index < 1 || index > this._maxLength) {
                Console.WriteLine("索引非法！");
                return false;
            }
            element = this._data[index - 1];
            return true;
        }

        //获取元素的索引，从后先前的第一个
        public int GetIndexByElement(T element) {
            for (int i = this._currentLength - 1; i >= 0; i--) {
                if (element.Equals(this._data[i])) {
                    return i + 1;
                }
            }

            return -1;
        }

        //请空表
        public void Clear() {

            for (int i = 0; i < this._currentLength; i++) {
                this._data[i] = default(T);
            }

            this._currentLength = 0;
        }

        //获取线性表元素的个数
        public int GetListLength() {
            return this._currentLength;
        }

        //获取线性表元素的允许的最大个数
        public int GeiListMaxLength() {
            return this._maxLength;
        }
    }
}
