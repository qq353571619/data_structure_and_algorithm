using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 线性表
{


    //串的顺序存储结构
    class StringList
    {
        private char[] _data;

        public StringList(char[] chars) {
            this.ValueTo(chars);
        }

        //创建空串
        public StringList() {
            this._data = null;
        }

        //串赋值为字符数组
        public void ValueTo(char[] chars) {
            if (chars == null) {
                this._data = null;
                return;
            }

            this._data = new char[chars.Length];
            for (int i = 0; i < chars.Length; i++)
            {
                _data[i] = chars[i];
            }
        }

        //串赋值为串
        public void ValueTo(StringList s) {
            if (s.isEmpty()) {
                Console.WriteLine("赋值的字符串不能为空！");
                return;
            }
           
            int length = s.Length();
            this._data = new char[length];
            char c = default(char);

            for (int i = 0; i < length; i++) {
                s.GetCharByIndex(i + 1,ref c);
                this._data[i] = c;
            }
            
        }

        //复制指定字符数组的指定位置长度的字符
        public bool CopyTo(int sounceIndex, char[] copy, int copyIndex = 1, int length = 0) {
            
          
            if (length == 0 && copyIndex > 0)
            {
                //复制copy数组copyIndex后面的所有字符
                length = copy.Length - copyIndex + 1;
            }

            char[] copyChars = this.subChar(copy, copyIndex, length);

            if (copyChars == null) {
                return false;
            }

            
            return this.Insert(sounceIndex, copyChars);
        }

        //复制串到插入到指定位置
        public bool CopyTo(int sounceIndex, StringList s, int copyIndex = 1, int length = 0) {
            return this.CopyTo(sounceIndex, s.ToChars(), copyIndex, length);
        }

        //比较两个串 1本串大  -1 s串大  0相等
        public int Compare(StringList s) {
            int sounceLength = this.Length();
            int length = s.Length();
            int i = 0;
            int j = 0;
            char c = '\0';

            while (i < sounceLength && j < length) {
                s.GetCharByIndex(j + 1, ref c);
                if (this._data[i] > c) {
                    return 1;
                }

                if (this._data[i] < c) {
                    return -1;
                }

                i++;
                j++;
            }

            if (sounceLength == length)
            {
                return 0;
            }

            if (sounceLength > length) {
                return 1;
            }

            return -1;
        }

        //获取到指定位置的字符
        public bool GetCharByIndex(int index, ref char c) {
            if (index < 1 || index > this.Length()) {
                Console.WriteLine("索引错误！");
                return false;
            }

            c = this._data[index - 1];
            return true;
        }

        //插入字符数组
        public bool Insert(int index , char[] chars)
        {
            if (this.isEmpty()||chars == null) {
                Console.WriteLine("插入的字符不能为空！");
                return false;
            }

            int length = _data.Length;
            if (index > length || index < 1) {
                Console.WriteLine("索引位置错误！");
                return false;
            }

            int cLength = chars.Length;

            char[] term = _data;
            _data = new char[length + cLength];
            int i = 0;
            for (; i < index - 1; i++) {
                _data[i] = term[i];
            }

            for (int j = 0; j < cLength; j++) {
                _data[i] = chars[j];
                i++;
            }

            for (int k = index - 1; k < length; k++) {
                _data[i] = term[k];
                i++;
            }

            return true;
        }
        

        //删除指定位置长度的字符串
        public bool Delete(int index, int length) {
            if (this.isEmpty()) {
                Console.WriteLine("被删除的字符串为空！");
                return false;
            }

            int sounceLength = this._data.Length;
            if (index < 1 || index > sounceLength || length < 1 || index + length - 1 > sounceLength) {
                Console.WriteLine("索引位置和长度错误！");
                return false;
            }

            char[] term = this._data;
            int newLength = sounceLength - length;
            this._data = new char[newLength];
            int go = 0;

            for (int i = 0; i < newLength; i++) {
                if (i == index - 1) {
                    go = length;
                }

                this._data[i] = term[i + go];
            }

            return true;
        }

        //截取指定位置长度的串，成为一个新的串
        public StringList SubString(int index, int length) {
            char[] sub = this.subChar(this._data, index, length);
            if (sub == null) {
                return null;
            }

            StringList newString = new StringList(sub);
            return newString;
        }

        //链接本串和s串为新串返回
        public StringList ConcatToNew(StringList s) {
            if (this.isEmpty() || s.isEmpty()) {
                return null;
            }

            StringList newString = new StringList();
            newString.ValueTo(this);
            newString.Concat(s);
            return newString;
            
        }

        //链接串s在尾部
        public bool Concat(StringList s) {
            if (this.isEmpty() || s.isEmpty())
            {
                return false;
            }

            int length1 = this.Length();
            int length2 = s.Length();

            char[] term = this._data;
            this._data = new char[length1 + length2];
            int i = 0;

            for (; i < length1; i++) {
                this._data[i] = term[i];
            }

            term = s.ToChars();
            for (; i < length1 + length2; i++) {
                this._data[i] = term[i-length1];
            }

            return true;
        }

        //普通匹配查找串的位置
        public int SimpleIndex(StringList T, int pos = 1) {
            if (T.isEmpty() || this.isEmpty()) {
                return 0;
            }

            int mainLength = this.Length();
            int matchLength = T.Length();
            int i;
            if (pos > 0 && pos <= mainLength) {
                i = pos;

                while (i <= mainLength - matchLength + 1) {
                    StringList sub = this.SubString(i, matchLength);
                    if (sub.Compare(T) != 0)
                    {
                        i++;
                    }
                    else {
                        return i;
                    }
                }
            }
            return 0;
        }

        //KMP匹配方法
        public int Index(StringList T, int pos = 1) {
            if (this.isEmpty() || T.isEmpty()) {
                return 0;
            }

            int mainLength = this.Length();
            int matchLength = T.Length();
            int i = 1;
            int j = 0;
            if (pos > 0 && pos <= mainLength) {
                i = pos;
                int[] nextval = new int[matchLength + 1];
                this.getNextval(T, nextval);
                for (; i <= mainLength; i++)
                {
                    while (j > 0 && this[i] != T[j + 1])
                    {
                        j = nextval[j];
                    }

                    if (this[i] == T[j + 1])
                    {
                        j++;
                    }

                    if (j == matchLength)
                    {
                        return i - matchLength + 1;
                    }
                }
            }
            return 0;
        }

        private void getNextval(StringList T, int[] nextval) {

            int i, j;   //代表第几个字符
            i = 1;
            j = 0;
            nextval[1] = 0;
            for (i=2; i < nextval.Length; i++)
            {
                while (j > 0 && T[i] != T[j + 1])
                {
                    j = nextval[j];
                }
                if (T[i] == T[j + 1])
                {
                    j++;
                }
                nextval[i] = j;
            }

        }

        //转换为字符数组并返回
        public char[] ToChars() {
            if (this.isEmpty()) {
                return null;
            }

            int sounceLength = this._data.Length;
            char[] newChar = this.subChar(this._data, 1, sounceLength);

            return newChar;
        }

        //是否为空
        public bool isEmpty() {
            if (this._data == null) {
                return true;
            }

            return false;
        }

        //获取长度
        public int Length() {
            if (this.isEmpty())
            {
                return 0;
            }
            return this._data.Length;
        }

        //获取子字符数组
        private char[] subChar(char[] chars, int index, int length) {

            if (chars == null) {
                Console.WriteLine("截取的字符数组为空！");
                return null;
            }

            int cLength = chars.Length;
            if (length <1 || index < 1||index > cLength || index + length - 1 > cLength) {
                Console.WriteLine("索引和长度错误！");
                return null;
            }

            char[] newChar = new char[length];
            for (int i = 0; i < length; i++) {
                newChar[i] = chars[index - 1 + i];
            }

            return newChar;
        }

        public char this[int index] {
            get {
                int length = this.Length();
                if (index < 1 || index > length) {
                    Console.WriteLine("索引错误！");
                    return default(char);
                }
                return this._data[index - 1];
            }
        }

    }
}
