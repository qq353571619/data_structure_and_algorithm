using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 算法实现
{
    class Node//结点
    {
        public bool isOb = false;
        public Node Parent;
        public int F;
        public int G;
        public int H;

        public int X;
        public int Y;

        public bool isLine = false;
    }

    class Map//地图
    {
        public Node[][] nodes;
        public int XLength;
        public int YLength;

        public void Clear()
        {
            for (int i = 0; i < XLength; i++)
            {
                for (int j = 0; j < YLength; j++)
                {
                    Node node = nodes[i][j];
                    node.F = 0;
                    node.G = 0;
                    node.H = 0;
                    node.isLine = false;
                    node.Parent = null;
                }
            }
        }

        //输出地图和寻路结果信息
        public void PrintMap()
        {
            for (int i = 0; i < YLength; i++)
            {
                if (i == 0)
                {
                    Console.Write(" " + " ");
                    for (int k = 0; k < XLength; k++)
                    {
                        Console.Write(k + " ");
                    }
                    Console.WriteLine();
                }
                for (int j = 0; j < XLength; j++)
                {
                    if (j == 0)
                    {
                        Console.Write(i + " ");
                    }

                    Node node = nodes[j][i];

                    if (node.isLine)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }

                    if (node.isOb)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("#" + " ");
                    }
                    else
                    {   
                        Console.Write("*" + " ");
                    }
                    Console.ForegroundColor = ConsoleColor.White;
                }

                Console.WriteLine();
            }
        }
    }

    class AXin//寻找
    {
        private List<Node> openList;
        private List<Node> closeList;
        private Map map;

        private Node startNode;
        private Node endNode;
        private Node preNode;

        public AXin()
        {
            openList = new List<Node>();
            closeList = new List<Node>();
        }

        public void Search(int startX, int startY,int endX,int endY)
        {
            if (map == null)
            {
                Console.WriteLine("请设置地图");
                return;
            }

            openList.Clear();
            closeList.Clear();
            map.Clear();
            startNode = map.nodes[startX][startY];
            if (startNode.isOb)
            {
                Console.WriteLine("开始位置为障碍物！");
                return;
            }
            endNode = map.nodes[endX][endY];
            openList.Add(startNode);
            Search();
        }

        private void Search()
        {
            bool isSearch = false;

            Node tempNode = null;
            while (openList.Count > 0 && !isSearch)
            {
                tempNode = GetLowerFNode();
                openList.Remove(tempNode);
                closeList.Add(tempNode);
                preNode = tempNode; //用来计算其他的结点的F

                int x = tempNode.X, y = tempNode.Y;
                //遍历周边结点8个  障碍和在close的 忽略
                for (int i = -1; i < 2; i++)//x
                {
                    for (int j = -1; j < 2; j++)//y
                    {
                        x = tempNode.X + i;
                        y = tempNode.Y + j;
                        if (x < 0 || x >= map.XLength || y < 0 || y >= map.YLength)
                        {
                            //越界了
                            continue;
                        }
                        if (i == 0 && j == 0) continue;//中心点

                        Node node = map.nodes[x][y];
                        //障碍跳过
                        if (node.isOb) continue;
                        //在close里面的跳过
                        if (isInClose(node)) continue;
                        //检查对角线 两边有障碍物的不能过
                        if (i != 0 && j != 0)
                        {
                            Node oneNode = map.nodes[tempNode.X][y];
                            if (oneNode.isOb) continue;
                            oneNode = map.nodes[x][tempNode.Y];
                            if (oneNode.isOb) continue;
                        }
                        //已经在openList
                        if (isInOpen(node))
                        {
                            //判断是否需要重新算 F
                            int offsetX = Math.Abs(node.X - preNode.X);
                            int offsetY = Math.Abs(node.Y - preNode.Y);
                            int len = 10;
                            if (offsetX + offsetY == 2)
                            {
                                len = 14;
                            }

                            if (preNode.G + len < node.G)
                            {
                                //重新算
                                node.G = preNode.G + len;
                                node.F = node.G + node.H;
                                node.Parent = preNode;
                            }
                        }
                        else {
                            //计算F 加进去 设置父物体
                            CalF(node);
                            openList.Add(node);
                            node.Parent = preNode;

                            if (node == endNode)
                            {
                                isSearch = true;
                            }
                        }
                    }
                }
            }

            if (isSearch)
            {
                Node go = endNode;
                //输出结果
                while (go != startNode)
                {
                    //Console.WriteLine("X：" + go.X +"||" + "Y：" + go.Y);
                    //Console.WriteLine("权重F="+go.F +" G="+go.G + " H="+go.H);
                    go.isLine = true;//标志路径
                    go = go.Parent;
                }
                go.isLine = true;
                PrintMap();
            }
            else {
                Console.WriteLine("没有找到！");
            }
        }

        public void SetMap(Map m)
        {
            map = m;
        }

        //计算F的值
        private void CalF(Node node)
        {
            int offsetX = Math.Abs(node.X - preNode.X);
            int offsetY = Math.Abs(node.Y - preNode.Y);
            if (offsetX + offsetY == 2)
            {
                node.G += 14;
            }
            else {
                node.G += 10;
            }
            
            //计算方向
            int DirectX = Math.Sign(endNode.X - node.X);
            int DirectY = Math.Sign(endNode.Y - node.Y);

            //计算H的X
            int indexX = node.X;
            while (indexX != endNode.X)
            {
                if (map.nodes[indexX][node.Y].isOb == false)
                {
                    node.H += 10;
                }
                indexX += DirectX;
            }

            //计算H的Y
            int indexY = node.Y;
            while (indexY != endNode.Y)
            {
                if (map.nodes[node.X][indexY].isOb == false)
                {
                    node.H += 10;
                }

                indexY += DirectY;
            }

            node.F = node.G + node.H;

        }

        private bool isInClose(Node node)
        {
            //可优化 在节点中保存信息
            bool isIn = false;
            foreach (var i in closeList)
            {
                if (node == i)
                    isIn = true;
            }

            return isIn;
        }

        private bool isInOpen(Node node)
        {
            //可优化 在节点中保存信息
            bool isIn = false;
            foreach (var i in openList)
            {
                if (node == i)
                    isIn = true;
            }

            return isIn;
        }

        private Node GetLowerFNode()
        {
            //可优化，用最小堆
            if (openList.Count < 1) {
                Console.WriteLine("怎么会没有了呢!");
                return null;
            }

            Node node = openList[0];

            for (int i = 1; i < openList.Count; i++)
            {
                if (openList[i].F < node.F)
                {
                    node = openList[i];
                }
            }

            return node;
        }

        public void PrintMap()
        {
            map.PrintMap();
        }
    }
}
