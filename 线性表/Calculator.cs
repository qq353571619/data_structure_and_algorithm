using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 线性表
{
    public class Calculator
    {
        private string _expression;
        public MyList<string> _elements;
        private StackList<string> _midStack;
        public MyList<string> _laterString;

        public Calculator() {
            _elements = new MyList<string>(100);
            _midStack = new StackList<string>(100);
            _laterString = new MyList<string>(100);
        }

        //计算表达式
        public string Cal(string expression) {

            this.Clear();
            _expression = expression;
            if (!this.Match()) {
                Console.WriteLine("表达式非法！");
                return "";
            }

            this.TranformMidToLater();
            return this.GoResult();
        }

        private void Clear() {
            this._elements.Clear();
            this._midStack.ClearStack();
            this._laterString.Clear();
        }
        

        private bool Match() {
            int index = 0;
            int exprLength = this._expression.Length;
            int lKuo = 0;
            int rKuo = 0;

            while (index != exprLength) {
                char who = this._expression[index];
                char next = '\0';
                if (index != exprLength-1)
                {
                    next = this._expression[index + 1];
                }
                
                
                switch (who)
                {
                    case '(':
                        if (index == exprLength - 1 || isRightKuo(next) || isSymbol(next)) {
                            return false;
                        }
                        lKuo++;

                        //保证前面的左括号多于右括号
                        if (rKuo >= lKuo) {
                            return false;
                        }

                        this._elements.Add("(");
                        index++;
                        break;

                    case ')':
                        rKuo++;
                        if (index == exprLength - 1) {
                            this._elements.Add(")");
                            index++;
                            break;
                        }

                        if (index == 0 || isLeftKuo(next) || isNumber(next)) {
                            return false;
                        }
                        
                        if (rKuo > lKuo) {
                            return false;
                        }

                        this._elements.Add(")");
                        index++;
                        break;

                    default:
                        //如果是符号类型
                        if (isSymbol(who)){
                            if (index == 0 || index == exprLength - 1) {
                                return false;
                            }

                            if (isRightKuo(next) || isSymbol(next)) {
                                return false;
                            }

                            this._elements.Add(who.ToString());
                            index++;
                            break;
                        }

                        
                    if (!isNumber(who)) {
                        return false;
                    }

                    //剩下为数字，匹配数字的范围
                    int numberLenth = 0;
                    bool isXiaoShu = false;

                    while (isNumber(this._expression[index + numberLenth]) || this._expression[index + numberLenth] == '.') {
                        if (this._expression[index + numberLenth] == '.') {
                            if (isXiaoShu)
                            {
                                //不能有两个小数点以上
                                return false;
                            }
                            else
                            {
                                isXiaoShu = true;
                            }
                        }

                        int myIndex = index + numberLenth;
                        numberLenth++;
                        if (myIndex == exprLength - 1) {
                            //最后一个元素
                            break;
                        }
                            
                    }
                    string num = this._expression.Substring(index, numberLenth);
                    this._elements.Add(num);
                    index += numberLenth;

                    if (isLeftKuo(this._expression[index]))
                    {
                        return false;
                    }

                    break;
                }
            }

            if (lKuo != rKuo) {
                return false;
            }

            return true;
            
        }

        //计算出后缀表达式
        private bool TranformMidToLater() {

            int eleLength = this._elements.GetListLength();

            if (eleLength == 0) {
                return false;
            }

            for (int i = 1; i <= eleLength; i++)
            {
                string element = "";
                this._elements.GetElement(i, ref element);

                if (element == "(")
                {
                    this._midStack.Push(element);
                    continue;
                }

                if (element == ")")
                {
                    //匹配前面的左括号
                    string e = "";
                    this._midStack.Pop(ref e);
                    while (e != "(")
                    {
                        this._laterString.Add(e);
                        this._midStack.Pop(ref e);
                    }
                    continue;
                }

                if (element == "+" || element == "-")
                {

                    if (this._midStack.GetLength() == 0)
                    {
                        //栈中没有元素
                        this._midStack.Push(element);
                        continue;
                    }

                    //获取栈顶元素
                    string e = "";
                    this._midStack.GetTop(ref e);
                    if (e == "*" || e == "/")
                    {
                        //栈顶符号优先级较高，输出
                        while (e != "(" && this._midStack.GetLength() != 0)
                        {
                            this._midStack.Pop(ref e);
                            this._laterString.Add(e);
                            this._midStack.GetTop(ref e);
                        }
                    }

                    this._midStack.Push(element);
                    continue;
                }

                if (element == "*" || element == "/")
                {
                    this._midStack.Push(element);
                    continue;
                }

                //剩下就是数字了
                //直接输出
                this._laterString.Add(element);

                //判断是否为最后一个数字
                if (i == eleLength) {
                    //将中专栈中的符号全部输出
                    while (this._midStack.GetLength() > 0) {
                        string e = "";
                        this._midStack.Pop(ref e);
                        this._laterString.Add(e);
                    }
                }

            }

            return true;
        }

        public string GoResult() {
            int Length = this._laterString.GetListLength();
            if (Length <= 0) {
                Console.WriteLine("后缀表达式为空！");
                return "";
            }
            
            string e = "";
            //遍历整个后缀表达式
            for (int i = 1; i <= Length; i++) {
                this._laterString.GetElement(i, ref e);

                //如果为符号，出栈两个进行计算
                if (e == "+") {
                    string termElement = "";
                    this._midStack.Pop(ref termElement);
                    double e1 = Double.Parse(termElement);

                    this._midStack.Pop(ref termElement);
                    double e2 = Double.Parse(termElement);

                    double result = e2 + e1;
                    this._midStack.Push(result.ToString());
                    continue;
                }

                if (e == "-") {
                    string termElement = "";
                    this._midStack.Pop(ref termElement);
                    double e1 = Double.Parse(termElement);

                    this._midStack.Pop(ref termElement);
                    double e2 = Double.Parse(termElement);

                    double result = e2 - e1;
                    this._midStack.Push(result.ToString());
                    continue;
                }

                if (e == "*")
                {
                    string termElement = "";
                    this._midStack.Pop(ref termElement);
                    double e1 = Double.Parse(termElement);

                    this._midStack.Pop(ref termElement);
                    double e2 = Double.Parse(termElement);

                    double result = e2 * e1;
                    this._midStack.Push(result.ToString());
                    continue;
                }

                if (e == "/")
                {
                    string termElement = "";
                    this._midStack.Pop(ref termElement);
                    double e1 = Double.Parse(termElement);

                    this._midStack.Pop(ref termElement);
                    double e2 = Double.Parse(termElement);

                    if (e1 == 0) {
                        Console.WriteLine("除数不能为0！");
                        return "";
                    }
                    double result = e2 / e1;
                    this._midStack.Push(result.ToString());
                    continue;
                }

                //剩下为数字，入栈
                this._midStack.Push(e);
            }

            //栈中会留有一个元素，那就是结果了
            string r = "";
            this._midStack.Pop(ref r);
            return r;
        }


        //判断是否为数字
        private bool isNumber(char c) {
            if (c >= '0' && c <= '9') {
                return true;
            }

            return false;
        }

        //判断是否为符号
        private bool isSymbol(char c) {
            if (c == '+' || c == '-' || c == '*' || c == '/')
            {
                return true;
            }

            return false;
        }


        //判断是否为左括号
        private bool isLeftKuo(char c) {
            if (c == '(') {
                return true;
            }

            return false;
        }

        //判断是否为右括号
        private bool isRightKuo(char c) {
            if (c == ')') {
                return true;
            }
            
            return false;
        }
    }
}
