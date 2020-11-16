using System;

namespace Reversi
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board();
            Game g = new Game();
        }

        class Game : Board
        {
            string xy;
            public Game()
            {
                Start(); //Place start pieces on the board
                Print(); //Print starting board
                while (End())
                {
                    Turn("Red"); //Red turn
                    Turn("Blue"); //Blue turn
                }
            }

            public void Turn(string Player)
            {
                Print();
                do
                {
                    Console.WriteLine("\n" + Player + ": Please choose where you want to place piece XY");
                }
                while (Input() && !PlacedPiece(xy[1] - '0', xy[0] - '0', Player));
            }

            public bool Input()
            {
                int Check;
                xy = Console.ReadLine();

                if (xy.Length > 1 && xy.Length < 3 && int.TryParse(xy, out Check))
                    return true;
                else
                    return false;
            }

            public bool PlacedPiece(int xx, int yy, string Player) //Player must place piece to end his turn
            {
                bool OpponentPiecesFliped = false; //Was opponent pieces turned over

                int PlayerNum = 1; //First player default
                int Opponent = 2; //Opponent default

                if (Player == "Blue")
                {
                    PlayerNum = 2;
                    Opponent--;
                }

                if (!Check(xx, yy, PlayerNum)) //Check if pick is in board range and not the player itself
                    return false;

                int xdir = 0;
                int ydir = 0;

                for (int i = 0; i <= 2; i++)
                    for (int j = 0; j <= 2; j++)
                        for (int n = 1; n <= x; n++)
                        {
                            xdir = xx + n * (i - 1);
                            ydir = yy + n * (j - 1);

                            if (xdir < 0 || ydir < 0 || xdir >= x || ydir >= y || b[xdir, ydir] == 0)
                                break;
                            else if (b[xdir, ydir] == Opponent)
                                continue;
                            else if (n > 1)
                            {
                                for (int z = 1; z < n; z++)
                                    PlacePiece(xx + z * (i - 1), yy + z * (j - 1), PlayerNum);
                                OpponentPiecesFliped = true;
                            }
                            else
                                break;
                        }

                if (OpponentPiecesFliped)
                {
                    PlacePiece(xx, yy, PlayerNum);
                    return true;
                }
                else
                {
                    Console.WriteLine("Invalid pick");
                    return false;
                }
            }

            public bool Check(int xx, int yy, int player)
            {

                if (b[xx, yy] == player)
                {
                    Console.WriteLine("You can't pick yourself");
                    return false;
                }
                else if (xx < 0 || xx >= x || yy >= y || yy < 0)
                {
                    Console.WriteLine("Try picking position in the board");
                    return false;
                }
                else
                    return true;
            }

            public void Start()
            {
                b[x / 2 - 1, y / 2 - 1] = 1;
                b[x / 2, y / 2] = 1;
                b[x / 2, y / 2 - 1] = 2;
                b[x / 2 - 1, y / 2] = 2;
            }

            bool End()
            {
                for (int i = 0; i < x; i++)
                    for (int j = 0; j < y; j++)
                        if (b[i, j] == 0)
                            return true; //Game havent ended

                return false; //Game have ended
            }
        }

        class Board
        {
            //Board size, must be square!
            public const int x = 8; //Board size
            public const int y = 8; //Must be square

            public int[,] b = new int[x, y];


            public Board() //Check if board dimmensions are correct
            {
                if (x != y)
                {
                    Console.WriteLine("Invalid board size, must be square.");
                    System.Environment.Exit(0);
                }

            }
            public void Print()
            {
                Console.Clear();

                for (int i = 0; i < x; i++)
                {
                    Console.Write(i + "  ");

                    for (int j = 0; j < y; j++)
                    {
                        Console.Write(b[i, j] + " ");
                    }
                    Console.WriteLine();
                }

                Console.Write("\n" + "   ");
                for (int i = 0; i < x; i++)
                    Console.Write(i + " ");

                Console.WriteLine();
            }

            public void PlacePiece(int x, int y, int Register)
            {
                b[x, y] = Register;
            }
        }
    }
}
