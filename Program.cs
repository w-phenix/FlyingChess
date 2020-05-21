using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyingChess
{
    class Program
    {
        public static int[] map = new int[100];
        public static int[] playerPos = new int[2];
        public static string[] playerNames = new string[2];
        public static bool[] pauseFlags = new bool[2];
        static void Main(string[] args)
        {
            ShowHead();
            InitMap();
            Console.Write("请输入 玩家1 的名字：");
            playerNames[0] = Console.ReadLine();
            while(playerNames[0] == "")
            {
                Console.Write("玩家1 名字不能为空，请重新输入：");
                playerNames[0] = Console.ReadLine();
            }
            Console.Write("请输入 玩家2 的名字：");
            playerNames[1] = Console.ReadLine();
            while (playerNames[1] == "" || playerNames[1] == playerNames[0])
            {
                if (playerNames[1] == "")
                {
                    Console.Write("玩家2 名字不能为空，请重新输入：");
                    playerNames[1] = Console.ReadLine();
                }
                if(playerNames[1] == playerNames[0])
                {
                    Console.Write("玩家2 名字不能和玩家1 名字一样，请重新输入：");
                    playerNames[1] = Console.ReadLine();
                }
            }
            Console.Clear();
            ShowHead();
            Console.WriteLine("玩家 {0} 用A表示", playerNames[0]);
            Console.WriteLine("玩家 {0} 用B表示", playerNames[1]);
            ShowMap();
            while (playerPos[0] < 99 && playerPos[1] < 99)
            {
                if (pauseFlags[0] == false)
                {
                    PlayGame(0);
                }
                else
                {
                    pauseFlags[0] = false;
                }
                if(playerPos[0] >= 99)
                {
                    Console.WriteLine("玩家{0}胜利！！！", playerNames[0]);
                    break;
                }
                if (pauseFlags[1] == false)
                {
                    PlayGame(1);
                }
                else
                {
                    pauseFlags[1] = false;
                }
                if (playerPos[1] >= 99)
                {
                    Console.WriteLine("玩家{0}胜利！！！", playerNames[1]);
                    break;
                }
            }
            GameWin();
            Console.ReadKey();
        }

        public static void ShowHead()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("**********************************");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("         0521飞行棋 v1.0");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("**********************************");
        }

        public static void InitMap()
        {
            int i;
            int[] luckyTurn = { 6, 23, 40, 55, 69, 83 };
            int[] landMine = { 5, 13, 17, 33, 38, 50, 64, 80, 94 };
            int[] pause = { 9, 27, 60, 93 };
            int[] timeTunnel = { 20, 25, 45, 63, 72, 88, 90 };
            // 幸运轮盘----1
            for (i = 0; i < luckyTurn.Length; ++i)
            {
                map[luckyTurn[i]] = 1;
            }
            // 地雷----2
            for (i = 0; i < landMine.Length; ++i)
            {
                map[landMine[i]] = 2;
            }
            // 暂停----3
            for (i = 0; i < pause.Length; ++i)
            {
                map[pause[i]] = 3;
            }
            // 时空隧道----4
            for (i = 0; i < timeTunnel.Length; ++i)
            {
                map[timeTunnel[i]] = 4;
            }
        }

        public static void ShowMap()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("图例：幸运轮盘：◎  地雷：★  暂停：▲  时空隧道：卍  ");
            int i;
            for (i = 0; i < 30; ++i)
            {
                DrawString(i);
            }
            Console.WriteLine();

            for (i = 30; i < 35; ++i)
            {
                for (int j = 0; j < 29; ++j)
                {
                    Console.Write("  ");
                }
                DrawString(i);
                Console.WriteLine();
            }

            for(i = 64;i >= 35;--i)
            {
                DrawString(i);
            }
            Console.WriteLine();

            for(i = 65;i < 70;++i)
            {
                DrawString(i);
                Console.WriteLine();
            }

            for(i = 70;i < 100;++i)
            {
                DrawString(i);
            }
            Console.WriteLine();
        }

        public static void DrawString(int i)
        {
            if (playerPos[0] == playerPos[1] && playerPos[0] == i)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("<>");
            }
            else if (playerPos[0] == i)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("A");
            }
            else if (playerPos[1] == i)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("B");
            }
            else
            {
                switch (map[i])
                {
                    case 0:
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("□");
                        break;
                    case 1:
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write("◎");
                        break;
                    case 2:
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        Console.Write("★");
                        break;
                    case 3:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("▲");
                        break;
                    case 4:
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.Write("卍");
                        break;
                }
            }
        }

        public static void PlayGame(int i)
        {
            Random r = new Random();
            int rNumber = r.Next(1, 7);
            Console.WriteLine("玩家 {0} 按任意键开始掷骰子", playerNames[i]);
            Console.ReadKey(true);
            Console.WriteLine("玩家 {0} 掷出了 {1} ", playerNames[i], rNumber);
            Console.ReadKey(true);
            playerPos[i] += rNumber;
            ChangePos();
            Console.WriteLine("玩家 {0} 按任意键开始行动", playerNames[i]);
            Console.ReadKey(true);
            Console.WriteLine("玩家 {0} 行动完了", playerNames[i]);
            Console.ReadKey(true);
            if(playerPos[i] == playerPos[1 - i])
            {
                Console.WriteLine("玩家 {0} 踩到了 玩家{1}， 玩家{1} 退6格", playerNames[i], playerNames[1 - i]);
                playerPos[1 - i] -= 6;
                Console.ReadKey(true);
            }
            else
            {
                switch(map[playerPos[i]])
                {
                    case 0:
                        Console.WriteLine("玩家{0} 踩到了方块，安全", playerNames[i]);
                        Console.ReadKey(true);
                        break;
                    case 1:
                        Console.Write("玩家{0} 踩到了幸运转盘，请选择 1--交换位置 2--轰炸对方：", playerNames[i]);
                        string input = Console.ReadLine();
                        while(true)
                        {
                            if(input == "1")
                            {
                                Console.WriteLine("玩家{0} 选择跟 玩家{1} 交换位置", playerNames[i], playerNames[1-i]);
                                Console.ReadKey(true);
                                int tmp = playerPos[i];
                                playerPos[i] = playerPos[1 - i];
                                playerPos[1 - i] = tmp;
                                Console.WriteLine("交换完成！请按任意键继续游戏！");
                                Console.ReadKey(true);
                                break;
                            }
                            else if(input == "2")
                            {
                                Console.WriteLine("玩家{0} 选择轰炸 玩家{1}， 玩家{2} 退6格", playerNames[i], playerNames[1 - i], playerNames[1 - i]);
                                Console.ReadKey(true);
                                playerPos[1 - i] -= 6;                               
                                Console.WriteLine("玩家{0} 退了6格", playerNames[1 - i]);
                                Console.ReadKey(true);
                                break;
                            }
                            else
                            {
                                Console.Write("输入错误，请重新输入 1--交换位置 2--轰炸对方：");
                                input = Console.ReadLine();
                            }
                        }
                        break;
                    case 2:
                        Console.WriteLine("玩家{0} 踩到了地雷，退6格", playerNames[i]);
                        Console.ReadKey(true);
                        playerPos[i] -= 6;                       
                        break;
                    case 3:
                        Console.WriteLine("玩家{0} 踩到了暂停，暂停一回合", playerNames[i]);
                        Console.ReadKey(true);
                        pauseFlags[i] = true;
                        break;
                    case 4:
                        Console.WriteLine("玩家{0} 踩到了时空隧道，前进10格", playerNames[i]);
                        Console.ReadKey(true);
                        playerPos[i] += 10;                        
                        break;
                }
            }
            ChangePos();
            Console.Clear();
            ShowMap();
        }

        public static void ChangePos()
        {
            if(playerPos[0] < 0)
            {
                playerPos[0] = 0;
            }
            if(playerPos[0] > 99)
            {
                playerPos[0] = 99;
            }
            if (playerPos[1] < 0)
            {
                playerPos[1] = 0;
            }
            if (playerPos[1] > 99)
            {
                playerPos[1] = 99;
            }
        }

        public static void GameWin()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("■                 ■                  ■     ■■■■■     ■        ■");
            Console.WriteLine(" ■               ■ ■               ■          ■        ■■       ■");
            Console.WriteLine("  ■             ■   ■             ■           ■        ■ ■      ■");
            Console.WriteLine("   ■           ■     ■           ■            ■        ■  ■     ■");
            Console.WriteLine("    ■         ■       ■         ■             ■        ■   ■    ■");
            Console.WriteLine("     ■       ■         ■       ■              ■        ■    ■   ■");
            Console.WriteLine("      ■     ■           ■     ■               ■        ■     ■  ■");
            Console.WriteLine("       ■   ■             ■   ■                ■        ■      ■ ■");
            Console.WriteLine("        ■ ■               ■ ■                 ■        ■       ■■");
            Console.WriteLine("         ■                  ■               ■■■■■    ■        ■");
            Console.ResetColor();
        }
    }
}
