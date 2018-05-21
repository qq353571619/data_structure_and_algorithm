using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 线性表
{
    //静态链表
    class StaticLink<T>
    {
        struct Node {
            public T data;
            public int cur;
        }

        private Node[] _node;
        private int _currentLength = 0;
        private int _maxLength = 24;
        private int _lastSubscript;

        //初始化静态链表大小等
        public StaticLink() {
            this._node = new Node[this._maxLength];
            this._node[0].cur = 1;//备用链表的头一个
            this._node[this._maxLength - 1].cur = 0;//数据链表的第一个，0表示链表为空
            for (int i = 1; i < this._maxLength - 2; i++) {
                this._node[i].cur = i + 1;
            }
            this._node[this._maxLength - 2].cur = 0;//备用链表结束
            this._lastSubscript = this._maxLength - 1;
        }

        //自定义静态链表的大小
        public StaticLink(int length) {
            if (length > 1) {
                this._maxLength = length+2;  //数组第一个和最后一个为特殊元素
            }
            this._node = new Node[this._maxLength];
            this._node[0].cur = 1;//备用链表的头一个
            this._node[this._maxLength - 1].cur = 0;//数据链表的第一个，0表示链表为空
            for (int i = 1; i < this._maxLength - 2; i++)
            {
                this._node[i].cur = i + 1;
            }
            this._node[this._maxLength - 2].cur = 0;//备用链表结束
            this._lastSubscript = this._maxLength - 1;
        }

        //获取到链表的第几个元素
        public bool GetElement(int index, ref T element) {

            int subscript = this.GetNodeByIndex(index);

            //排除两种特殊情况
            if (subscript == 0) {
                return false;
            }
            if (subscript == this._maxLength - 1) {
                Console.WriteLine("头结点没有数据可以获取！");
                return false;
            }

            element = this._node[subscript].data;
            return true;
        }

        //增加元素，在链表后面
        public bool Add(T element) {

            int addSubscript = this.GetLeisure();
            if (addSubscript == 0) {
                Console.WriteLine("链表已满！");
                return false;
            }

            //找到最后一个元素
            int lastSubscript = this._maxLength - 1;

            while (this._node[lastSubscript].cur != 0) {
                lastSubscript = this._node[lastSubscript].cur;
            }
            

            //添加元素
            this._node[addSubscript].data = element;
            this._node[addSubscript].cur = 0;
            this._node[lastSubscript].cur = addSubscript;
            this._lastSubscript = addSubscript;

            this._currentLength++;

            return true;
        }

        //快速添加元素，在链表后面
        public bool AddQuick(T element) {

            int addSubscript = this.GetLeisure();
            if (addSubscript == 0)
            {
                Console.WriteLine("链表已满！");
                return false;
            }

            //添加元素
            this._node[addSubscript].data = element;
            this._node[addSubscript].cur = 0;
            this._node[this._lastSubscript].cur = addSubscript;
            this._lastSubscript = addSubscript;

            this._currentLength++;

            return true;
        }

        //插入结点
        public bool Insert(int index, T element) {
            int preSubscript = this.GetNodeByIndex(index - 1);

            //判断一下index位置的结点是否存在
            if (preSubscript == 0 || this._node[preSubscript].cur == 0) {
                return false;
            }

            int addSubscript = GetLeisure();
            if (addSubscript == 0) {
                Console.WriteLine("链表已满！");
                return false;
            }

            this._node[addSubscript].data = element;
            this._node[addSubscript].cur = this._node[preSubscript].cur;
            this._node[preSubscript].cur = addSubscript;
            this._currentLength++;
            return true;
        }

        //删除指定位置的结点
        public bool Delete(int index,ref T element) {
            int preSubscript = this.GetNodeByIndex(index - 1);

            //判断一下index位置的结点是否存在
            if (preSubscript == 0 || this._node[preSubscript].cur == 0) {
                return false;
            }

            int deleteSubscript = this._node[preSubscript].cur;
            element = this._node[deleteSubscript].data;
            this._node[preSubscript].cur = this._node[deleteSubscript].cur;
            this.BackLeisure(deleteSubscript);
            this._currentLength--;
            return true;
        }

        //取出备用链表的空闲空间
        private int GetLeisure() {
            //得到备用链表的第一个结点
            int subscript = this._node[0].cur;
            
            //将备用链表第二个结点变第一个
            this._node[0].cur = this._node[subscript].cur;

            //返回取出的空闲节点
            return subscript;
        }

        //回收空闲的结点
        private void BackLeisure(int subScript) {
            this._node[subScript].cur = this._node[0].cur;
            this._node[0].cur = subScript;
        }

        //获取静态链表长度
        public int GetStaticLinkLength() {
            return this._currentLength;
        }

        //获取静态链表的最大长度
        public int GetStaticLinkMaxlength() {
            return this._maxLength;
        }

        //获取指定位置的结点
        private int GetNodeByIndex(int index) {
            if (index == 0)
            {
                return this._maxLength - 1;
            }

            if (index < 0 || index > this._currentLength) {
                Console.WriteLine("索引错误！");
                return 0;
            }
            

            int i = 1;
            int subscript = this._node[this._maxLength - 1].cur;
            if (subscript == 0) {
                Console.WriteLine("链表为空！");
                return 0;
            }
            while (i != index && this._node[subscript].cur != 0) {
                subscript = this._node[subscript].cur;
                i++;
            }

            return subscript;
        }
    }
}
