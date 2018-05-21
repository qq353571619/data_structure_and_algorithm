using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 线性表
{
    class StringLink2
    {
        class Node {
            public char[] data;
            public Node next;
            public int length = 0;
        }

        private Node _headNode = null;
        private Node _lastNode = null;
        private int _nodeLength = 10;
        private int _length = 0;

        public StringLink2():this(null,0) {
        }

        public StringLink2(char[] chars):this(chars,0) {
        }

        public StringLink2(char[] chars, int nodeLenth) {
            if (nodeLenth > 0) {
                this._nodeLength = nodeLenth;
            }

            this._headNode = new Node();
            this._headNode.next = null;
            this._headNode.data = null;
            this.ValueTo(chars);
        }

        //赋值为字符数组
        public void ValueTo(char[] chars) {
            this.Clear();
            if (chars == null) {
                return;
            }
            
            for (int i = 0; i < chars.Length; i++) {
                if (i % this._nodeLength == 0) {
                    Node addNode = new Node();
                    addNode.data = new char[this._nodeLength];
                    addNode.next = null;
                    if (this._lastNode == null)
                    {
                        this._headNode.next = addNode;
                        this._lastNode = addNode;
                    }
                    else {
                        this._lastNode.next = addNode;
                        this._lastNode = addNode;
                    }
                    
                }

                this._lastNode.data[this._lastNode.length] = chars[i];
                this._lastNode.length++;
            }

            //空的地方填充为\0
            for (int i = this._lastNode.length; i < this._nodeLength; i++) {
                this._lastNode.data[this._lastNode.length] = '\0';
            }

            this._length = chars.Length;
        }

        //赋值为串
        public void ValueTo(StringLink2 s) {
            this.ValueTo(s.ToChars());
        }

        //链接字符数组在串后面
        public bool Concat(char[] chars) {
            if (chars == null || this.IsEmpty()) {
                Console.WriteLine("字符数组或串不能为空！");
                return false;
            }

            for (int i = 0; i < chars.Length; i++) {
                if (this._lastNode.length >= this._nodeLength) {
                    Node addNode = new Node();
                    addNode.data = new char[this._nodeLength];
                    addNode.next = null;
                    this._lastNode.next = addNode;
                    this._lastNode = addNode;
                }

                this._lastNode.data[this._lastNode.length++] = chars[i];
            }

            for (int i = this._lastNode.length; i < this._nodeLength; i++) {
                this._lastNode.data[i] = '\0';
            }

            this._length += chars.Length;
            return true;
        }

        public bool Concat(StringLink2 s) {
            char[] chars = s.ToChars();
            return this.Concat(chars);
        }

        //在指定位置插入字符数组
        public bool Insert(int index, char[] chars) {

            if (this.IsEmpty() || chars == null) {
                Console.WriteLine("被插入的串或插入的字符数组不能为空！");
                return false;
            }

            if (index < 1 || index > this.GetLength()) {
                Console.WriteLine("索引错误！");
                return false;
            }

            int i = 0;
            Node preNode = this._headNode;
            Node targetNode = this._headNode;
            while (i < index) {
                preNode = targetNode;
                targetNode = targetNode.next;
                i += targetNode.length;
            }

            //获取目标位置中目标结点中数据的下标
            int sub = targetNode.length - (i - index) - 1;

            Node addNode = null;
            if (sub == 0)
            {
                //填充前一个结点的空闲位置
                addNode = preNode;
            }
            else {
                addNode = new Node();
                addNode.data = new char[this._nodeLength];
                addNode.next = null;
            }
            
            //填充目标结点中目标下标前的数据
            for (int j = 0; j < sub; j++) {
                addNode.data[addNode.length++] = targetNode.data[j];
            }
            
            for (int j = 0; j < chars.Length; j++) {
                if (addNode.length >= this._nodeLength)
                {
                    preNode.next = addNode;
                    preNode = addNode;
                    addNode = new Node();
                    addNode.data = new char[this._nodeLength];
                    addNode.next = null;
                }
                addNode.data[addNode.length++] = chars[j];
            }

            //链接最后新加的结点
            preNode.next = addNode;
            preNode = addNode;
            

            if(sub != 0){
                for (int j = sub; j < targetNode.length; j++) {
                    if (addNode.length >= this._nodeLength) {
                        addNode = new Node();
                        addNode.data = new char[this._nodeLength];
                        addNode.next = null;
                    }

                    addNode.data[addNode.length++] = targetNode.data[j];
                }

                //链接
                preNode.next = addNode;
                preNode = addNode;

                //抛弃targetNode结点
                targetNode = targetNode.next;
            }

            preNode.next = targetNode;
            if (targetNode == null) {
                this._lastNode = preNode;
            }

            this._length += chars.Length;
            return true;
        }

        public bool Insert(int index,StringLink2 s) {
            char[] chars = s.ToChars();
            return this.Insert(index,chars);
        }

        //删除指定长度的子串
        public bool Delete(int index, int length) {
            if (this.IsEmpty()) {
                Console.WriteLine("删除的串不能为空！");
                return false;
            }

            if (index < 1 || index > this._length) {
                Console.WriteLine("索引错误！");
                return false;
            }


            Node preNode = this._headNode;
            Node targetNode = this._headNode;
            int i = 0;
            while (i < index) {
                preNode = targetNode;
                targetNode = targetNode.next;
                i += targetNode.length;
            }
            //获取位置下标
            int sub = targetNode.length - (i - index) - 1;


            //找出被删除后分开出来的第二部分子串
            int j = i - targetNode.length;
            int saveIndex = index + length;

            if (saveIndex > this.GetLength()) {
                //删除后面全部
                if (index == 1)
                {
                    this.Clear();
                }
                else {
                    if (sub == 0)
                    {
                        this._lastNode = preNode;
                        preNode.next = null;
                    }
                    else {
                        targetNode.length = sub;
                        for (int k = sub; k < targetNode.length; k++) {
                            targetNode.data[k] = '\0';
                        }
                        this._lastNode = targetNode;
                        targetNode.next = null;
                    }
                }

                this._length -= length;
                return true;
            }

            Node saveNode = preNode;
            while(j < saveIndex)
            {
                saveNode = saveNode.next;
                j += saveNode.length;
            }
            int saveSub = saveNode.length - (j - saveIndex) - 1;
            

            if (saveSub >= sub)
            {
                for (int k = saveSub; k < saveNode.length; k++)
                {
                    targetNode.data[sub++] = saveNode.data[k];
                }
                targetNode.length = sub;

                if (targetNode != saveNode) {
                    targetNode.next = saveNode.next;
                }
            }
            else {
                targetNode.length = sub;
                for (int k = sub; k < this._nodeLength; k++) {
                    targetNode.data[k] = '\0';
                }

                if (saveSub != 0) {
                    for (int k = 0; k < saveNode.length - saveSub; k++)
                    {
                        saveNode.data[k] = saveNode.data[saveSub++];
                    }

                    saveNode.length = saveSub;
                }
                
                targetNode.next = saveNode;
            }

            this._length -= length;
            return true;
        }

        //替换
        public bool Replace(StringLink2 match, StringLink2 replace, int index = 1) {
            if (match.IsEmpty() || replace.IsEmpty() || this.IsEmpty()) {
                Console.WriteLine("串不能为空！");
                return false;
            }

            if (index < 1 || index > this.GetLength()) {
                Console.WriteLine("索引错误！");
                return false;
            }

            int i = this.Index(match, index);
            if (i != 0) {
                this.Delete(i, match.GetLength());
                if (i > this.GetLength()) {
                    return this.Concat(replace);
                } else {
                    return this.Insert(i, replace);
                }
            }

            return false;
        }

        //KMP匹配
        public int Index(StringLink2 match,int index = 1)
        {
            if (match.IsEmpty() || this.IsEmpty()) {
                Console.WriteLine("串不能为空！");
                return 0;
            }

            if (index < 1 || index > this.GetLength()) {
                Console.WriteLine("索引错误！");
                return 0;
            }

            int[] nextval = this._getNextVal(match);
            int i = index;
            int j = 1;
            while(i<=this.GetLength()) {
                if (j == 0 || this[i] == match[j])
                {
                    i++;
                    j++;
                }
                else {
                    j = nextval[j];
                }

                if (j > match.GetLength())
                {
                    return i - match.GetLength();
                }
            }

            

            return 0;
        }

        private int[] _getNextVal(StringLink2 s) {
            int[] nextVal = new int[s.GetLength()+1];
            nextVal[1] = 0;
            int i = 1;
            int j = 0;
            for (; i < s.GetLength();) {
                if (j == 0 || s[i] == s[j])
                {
                    j++;
                    i++;
                    if (s[i] == s[j])
                    {
                        nextVal[i] = nextVal[j];
                    }
                    else
                    {
                        nextVal[i] = j;
                    }
                }
                else
                {
                    j = nextVal[j];
                }
            }

            return nextVal;
        }

        //转换为字符数组
        public char[] ToChars() {
            if (this.IsEmpty()) {
                return null;
            }

            Node goNode = this._headNode;
            char[] chars = new char[this._length];
            int i = 0;
            while (goNode.next != null) {
                goNode = goNode.next;
                for (int j = 0; j < goNode.length; j++) {
                    chars[i] = goNode.data[j];
                    i++;
                }
            }

            return chars;
        }

        //获取指定位置的字符
        public bool GetElementBuIndex(int index, ref char c) {
            if (index < 1 || index > this.GetLength()) {
                Console.WriteLine("索引错误！");
                return false;
            }

            int i = 0;
            Node goNode = this._headNode;
            while (i < index) {
                goNode = goNode.next;
                i += goNode.length;
            }

            c = goNode.data[goNode.length - i + index - 1];
            return true;
        }

        //清空串
        public void Clear() {
            this._headNode.next = null;
            this._lastNode = null;
            this._length = 0;
        }

        //判断是否为空
        public bool IsEmpty() {
            if (this._headNode.next == null) {
                return true;
            }

            return false;
        }

        //获取串的长度
        public int GetLength() {
            return this._length;
        }

        public char this[int index] {
            get {
                char c = '\0';
                this.GetElementBuIndex(index, ref c);
                return c;
            }
        }

    }
}
