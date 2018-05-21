using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 线性表
{
    class StringLink
    {
        class Node {
            public char[] data;
            public Node next; 
        }

        private int _nodeLength = 10;
        private int _length = 0;
        private Node _headNode = null;
        private Node _lastNode = null;

        //初始化为空串
        public StringLink() : this(null, 0) { }

        //初始化串
        public StringLink(char[] chars):this(chars,0) {
        }

        //自定义单节点长度串
        public StringLink(char[] chars,int nodeLength) {

            if (nodeLength > 1) {
                this._nodeLength = nodeLength;
            }

            //新建头结点
            this._headNode = new Node();
            this._headNode.data = null;
            this._headNode.next = null;

            this.ValueTo(chars);
            
        }

        //赋值为链串
        public void ValueTo(StringLink s) {
            char[] chars = s.ToChars();
            this.ValueTo(chars);
        }

        //赋值为字符数组
        public void ValueTo(char[] chars) {
            this.Clear();

            if (chars == null)
            {
                return;
            }

            int length = chars.Length;
            int j = 0;
            for (int i = 0; i < length; i++)
            {
                if (i % this._nodeLength == 0)
                {

                    //新建一个结点继续保存数据
                    Node addNode = new Node();
                    addNode.data = new char[this._nodeLength];
                    addNode.next = null;
                    if (this._lastNode == null)
                    {
                        this._lastNode = addNode;
                        this._headNode.next = addNode;
                    }
                    else {
                        this._lastNode.next = addNode;
                        this._lastNode = addNode;
                    }
                    j = 0;
                }

                this._lastNode.data[j] = chars[i];
                j++;
            }

            this._length = length;
        }

        //在尾部链接串
        public bool Concat(StringLink s) {
            if (s.IsEmpty()) {
                Console.WriteLine("链接的串不能为空！");
                return false;
            }

            char[] chars = s.ToChars();
            return this.Concat(chars);
        }

        //在尾部链接字符数组
        public bool Concat(char[] chars) {
            if (this._headNode.next == null || chars == null) {
                Console.WriteLine("链接的串或字符数组不能为空！");
                return false;
            }
            

            //计算最后结点的偏移
            int offset = this._length % this._nodeLength;
            int j = offset;
            for (int i = 0; i < chars.Length; i++) {
                if ((i + offset) % this._nodeLength == 0) {
                    //新建结点
                    Node addNode = new Node();
                    addNode.data = new char[this._nodeLength];
                    addNode.next = null;
                    this._lastNode.next = addNode;
                    this._lastNode = addNode;
                    j = 0;
                }

                this._lastNode.data[j] = chars[i];
                j++;
                
            }

            this._length += chars.Length;

            return true;
        }

        //转换为字符数组
        public char[] ToChars() {
            if (this._length == 0) {
                return null;
            }

            char[] chars = new char[this._length];
            Node valueNode = this._headNode.next;
            int j = 0;
            for (int i = 0; i < this._length; i++) {
                chars[i] = valueNode.data[j];
                j++;
                if (j == this._nodeLength) {
                    //当前结点赋值完毕，到下一个结点
                    valueNode = valueNode.next;
                    j = 0;
                }
            }

            return chars;
        }

        //插入字符数组
        public bool Insert(int index, char[] chars) {
            if (chars == null || this.IsEmpty()) {
                Console.WriteLine("插入的字符数组或自身为空！");
                return false;
            }

            if (index < 1 || index > this.GetLength()) {
                Console.WriteLine("索引错误！");
                return false;
            }

            //1.找出index的位置，是哪一个结点
            int nodeIndex = index / this._nodeLength;
            //2.找出是那个节点的第几位
            int dataIndex = (index-1) % this._nodeLength;
            if (dataIndex != this._nodeLength - 1) {
                nodeIndex++;
            }
            
            //遍历找到第nodeIndex-1结点
            Node preNode = this._headNode;
            for (int i = 1; i < nodeIndex; i++) {
                preNode = preNode.next;
            }

            //保存第nodeIndex个结点
            Node targetNode = preNode.next;

            //新建结点
            Node addNode = new Node();
            addNode.data = new char[this._nodeLength];
            //填充目标结点targetIndex目标位置dataIndex前的数据
            for (int i = 0; i < dataIndex; i++) {
                addNode.data[i] = targetNode.data[i];
            }

            //初始化插入指标为dataIndex
            int cur = dataIndex;
            //插入chars数组的数据
            for (int i = 0; i < chars.Length; i++) {
                addNode.data[cur] = chars[i];
                cur++;
                if (cur >= this._nodeLength) {
                    preNode.next = addNode;
                    preNode = addNode;
                    cur = 0;
                    if (i != chars.Length - 1) {
                        addNode = new Node();
                        addNode.data = new char[this._nodeLength];
                    }
                }
            }

            

            //将前面新加的结点和被插入部分的结点链接起来
            if (cur != 0) {
                //表示addNode没填充满，也没有进行链接
                preNode.next = addNode;
                preNode = addNode;
            }
            preNode.next = targetNode;

            //不需要向前移动的情况
            if (cur == 0 && dataIndex == 0)
            {
                return true;
            }

            //只需要移动少数的情况
            if (this._nodeLength - cur == dataIndex) {
                while (cur < this._nodeLength) {
                    preNode.data[cur] = targetNode.data[dataIndex];
                    cur++;
                    dataIndex++;
                }

                //删除target结点
                preNode.next = targetNode.next;
                if (targetNode.next == null) {
                    this._lastNode = preNode;
                }
                return true;
            }

            //向前移动数据
            int goIndex = index;
            int iIndex = cur;
            int jIndex = dataIndex;
            Node iNode = addNode;
            Node jNode = targetNode;
            if (iIndex == 0) {
                iNode = addNode.next;
            }
            
            while (goIndex <= this.GetLength()) {//表示要向前移动的数据
                iNode.data[iIndex] = jNode.data[jIndex];

                iIndex++;
                jIndex++;

                if (iIndex >= this._nodeLength) {
                    iIndex = 0;
                    iNode = iNode.next;
                }
                if (jIndex >= this._nodeLength) {
                    jIndex = 0;
                    jNode = jNode.next;
                }

                goIndex++;
            }

            if (this._nodeLength - cur + dataIndex > this._nodeLength) {
                //表示最后一个结点为空了,抛弃最后那个结点
                this._lastNode = iNode;
                iNode.next = null;
            }
            this._lastNode = iNode;
            iNode.next = null;

            this._length += chars.Length;

            return true;

        }

        //插入字符串到指定位置
        public bool Insert(int index, StringLink s) {
            if (s.IsEmpty())
            {
                Console.WriteLine("插入的字符串不能为空！");
                return false;
            }

            char[] chars = s.ToChars();
            return this.Insert(index, chars);
        }



        //替换字符串
        public int Replace(StringLink match, StringLink replace, int index = 1) {
            if (match.IsEmpty() || this.IsEmpty()) {
                Console.WriteLine("当前串或匹配串不能为空！");
                return 0;
            }

            if (index < 1 || index > this.GetLength()) {
                Console.WriteLine("索引错误！");
                return 0;
            }

            int matchIndex = this.Index(match, index);
            if (matchIndex != 0) {
                this.Delete(matchIndex, match.GetLength());
                if (matchIndex - 1 == this.GetLength())
                {
                    this.Concat(replace);
                }
                else {
                    this.Insert(matchIndex, replace);
                }
                

                return 1;
            }

            return 0;
        }

        //删除指定位置长度的字符
        public bool Delete(int index, int length)
        {
            if (this.IsEmpty())
            {
                Console.WriteLine("字符串不能为空！");
                return false;
            }

            if (index < 1 || index > this.GetLength())
            {
                Console.WriteLine("索引错误！");
                return false;
            }

            if (length + index - 1 > this.GetLength())
            {
                Console.WriteLine("删除的长度有误！");
                return false;
            }

            int iNodeIndex = index / this._nodeLength;
            int iDataIndex = (index - 1) % this._nodeLength;
            if (iDataIndex != this._nodeLength - 1)
            {
                iNodeIndex++;
            }


            Node preNode = this._headNode;
            for (int i = 1; i < iNodeIndex; i++)
            {
                preNode = preNode.next;
            }
            Node itargetNode = preNode.next;

            if (index + length - 1 == this.GetLength())
            {
                if (index == 1)
                {
                    //表示删除所有元素
                    this._lastNode = null;
                    this._headNode = null;
                }
                else
                {
                    //删除index后面全部
                    if (iDataIndex == 0)
                    {
                        //要删除itargetNode及后面的全部
                        preNode.next = null;
                        this._lastNode = preNode;
                    }
                    else
                    {
                        //只删除itargetNode的部分及后面全部
                        itargetNode.next = null;
                        this._lastNode = itargetNode;
                    }

                }

                this._length -= length;
                return true;
            }

            int jIndex = index + length;
            int jNodeIndex = jIndex / this._nodeLength;
            int jDataIndex = (jIndex - 1) % this._nodeLength;
            if (jDataIndex != this._nodeLength - 1)
            {
                jNodeIndex++;
            }

            Node jtargetNode = itargetNode;
            for (int i = iNodeIndex; i < jNodeIndex; i++)
            {
                jtargetNode = jtargetNode.next;
            }

            int goIndex = index + length;
            while (goIndex <= this.GetLength())
            {
                itargetNode.data[iDataIndex] = jtargetNode.data[jDataIndex];
                iDataIndex++;
                jDataIndex++;
                goIndex++;
                if (iDataIndex >= this._nodeLength)
                {
                    itargetNode = itargetNode.next;
                    iDataIndex = 0;
                }

                if (jDataIndex >= this._nodeLength)
                {
                    jtargetNode = jtargetNode.next;
                    jDataIndex = 0;
                }

            }

            this._lastNode = itargetNode;
            this._lastNode.next = null;
            this._length -= length;

            return true;
        }

        //KMP匹配串的位置
        public int Index(StringLink s,int where = 1) {
            if (s.IsEmpty() || this.IsEmpty() || where < 1 || where > this.GetLength()) {
                Console.WriteLine("串不能为空或索引非法！");
                return 0;
            }

            int[] nextval = this._GetNextVal(s);
            int matchLength = s.GetLength();
            int length = this.GetLength();
            int i = where;
            int j = 1; 
            while (i <= length && j<=matchLength) {
                if (j == 0 || this[i] == s[j])
                {
                    j++;
                    i++;
                }
                else {
                    j = nextval[j];
                }

            }

            if (j > matchLength) {
                return i - matchLength;
            }

            return 0;
        }

        private int[] _GetNextVal(StringLink s) {
            int length = s.GetLength();
            int[] nextval = new int[length + 1];
            int i = 1;
            int j = 0;
            nextval[1] = 0;

            for (; i < length;) {

                if (j == 0 || s[i] == s[j])
                {
                    i++;
                    j++;
                    if (s[i] == s[j])
                    {
                        nextval[i] = nextval[j];
                    }
                    else
                    {
                        nextval[i] = j;
                    }
                }
                else {
                    j = nextval[j];
                }
            }

            return nextval;
        }

        //获取指定位置的字符
        public bool GetElementByIndex(int index, ref char c) {
            if (index < 1 || index > this._length) {
                Console.WriteLine("索引非法！");
                return false;
            }

            int nodeIndex = 0;
            int dataIndex = (index - 1) % this._nodeLength;
            if (dataIndex + 1 == this._nodeLength)
            {
                //刚刚结点尾数据
                nodeIndex = index / this._nodeLength;
            }
            else {
                nodeIndex = (int)(index / this._nodeLength) + 1;
            }

            Node whichNode = this._headNode;
            for (int i = 0; i < nodeIndex; i++) {
                whichNode = whichNode.next;
            }

            c = whichNode.data[dataIndex];
            return true;
        }

        //是否为空串
        public bool IsEmpty() {
            if (this._lastNode == null) {
                return true;
            }

            return false;
        }

        //清空串
        public void Clear() {
            this._headNode.next = null;
            //第一个结点地址没有被引用，被回收
            //它所指向的下一结点也失去引用，也被回收......
            this._lastNode = null;
        }

        //获取串的长度
        public int GetLength() {
            return this._length;
        }

        public char this[int index] {
            get {
                char c = '\0';
                this.GetElementByIndex(index, ref c);
                return c;
            }
        }

    }
}
