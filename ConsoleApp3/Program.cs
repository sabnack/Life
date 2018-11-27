using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ConsoleApp3
{

    public class Cell
    {
        public int X;
        public int Y;

        public Cell(int x, int y)
        {
            X = x;
            Y = y;
        }


    }
    class Program
    {
        const int FieldX = 40;//60; //60; 35
        const int FieldY = 40;//230;//180; 80

        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            int[,] Field = new int[FieldX, FieldY];
            int[,] FieldNxtGen = new int[FieldX, FieldY];
            //Random rand = new Random();
            //for (var i = 0; i < 300; i++)
            //{
            //    Field[rand.Next(1, Field.GetLength(0) - 1), rand.Next(1, Field.GetLength(1) - 1)] = 1;
            //}

            //Field[0, 0] = 1;
            //Field[0, 1] = 1;
            //Field[0, FieldY - 1] = 1;

            //Field[8, 16] = 1;
            //Field[8, 17] = 1;
            //Field[8, 18] = 1;

            ////Field[6, 7] = 1;
            ////Field[7, 7] = 1;
            ////Field[7, 8] = 1;


            //// Glider
            Field[1, 2] = 1;
            Field[2, 3] = 1;
            Field[3, 1] = 1;
            Field[3, 2] = 1;
            Field[3, 3] = 1;
            //Glider 2
            Field[1, FieldY - 3] = 1;
            Field[2, FieldY - 4] = 1;
            Field[3, FieldY - 4] = 1;
            Field[3, FieldY - 3] = 1;
            Field[3, FieldY - 2] = 1;
            //Glider 3
            Field[FieldX - 2, 2] = 1;
            Field[FieldX - 3, 3] = 1;
            Field[FieldX - 4, 1] = 1;
            Field[FieldX - 4, 2] = 1;
            Field[FieldX - 4, 3] = 1;
            //Glider 4
            //Field[FieldX - 2, FieldY - 3] = 1;
            //Field[FieldX - 3, FieldY - 4] = 1;
            //Field[FieldX - 4, FieldY - 4] = 1;
            //Field[FieldX - 4, FieldY - 3] = 1;
            //Field[FieldX - 4, FieldY - 2] = 1;
            ////Toad
            //Field[4, 50] = 1;
            //Field[4, 51] = 1;
            //Field[4, 52] = 1;
            //Field[5, 51] = 1;
            //Field[5, 52] = 1;
            //Field[5, 53] = 1;
            ////stable form
            //Field[3, 25] = 1;
            //Field[4, 25] = 1;
            //Field[4, 26] = 1;
            //Field[2, 26] = 1;
            //Field[2, 27] = 1;
            //Field[3, 27] = 1;

            ////Toad
            //Field[50, 50] = 1;
            //Field[50, 51] = 1;
            //Field[50, 52] = 1;
            //Field[51, 51] = 1;
            //Field[51, 52] = 1;
            //Field[51, 53] = 1;
            while (true)
            {
                PrintField(Field);
                Thread.Sleep(100);
                //Console.ReadKey();
                NextGeneration(ref Field, ref FieldNxtGen);
                //Console.SetCursorPosition(0, 0);
                //PrintField(FieldNxtGen);
                //Thread.Sleep(500);

                Console.SetCursorPosition(0, 0);
            }
        }

        static void NextGeneration(ref int[,] Field, ref int[,] FieldNxtGen)
        {
            int LifeCount = 0;
            for (var x = 0; x < FieldX; x++)
            {
                for (var y = 0; y < FieldY; y++)
                {

                    for (var x1 = x - 1; x1 < x + 2; x1++)
                    {
                        for (var y1 = y - 1; y1 < y + 2; y1++)
                        {
                            if (x1 == x && y1 == y)
                                continue;

                            int tmpX, tmpY;
                            LoopBack(out tmpX, out tmpY, x1, y1);

                            if (Field[tmpX, tmpY] == 1)
                                LifeCount++;
                        }
                    }

                    switch (Field[x, y])
                    {
                        case 0:
                            {
                                if (LifeCount == 3)
                                    FieldNxtGen[x, y] = 1;
                                break;
                            }

                        case 1:
                            {
                                if (LifeCount == 2 || LifeCount == 3)
                                    FieldNxtGen[x, y] = 1;
                                break;
                            }
                    }

                    LifeCount = 0;
                }
            }

            Field = FieldNxtGen;
            int[,] tmpField = new int[FieldX, FieldY];
            FieldNxtGen = tmpField;
        }

        static void LoopBack(out int tmpX, out int tmpY, int x1, int y1)
        {
            switch (x1)
            {
                case -1:
                    {
                        tmpX = FieldX - 1;
                        break;
                    }

                case FieldX:
                    {
                        tmpX = 0;
                        break;
                    }

                default:
                    {
                        tmpX = x1;
                        break;
                    }
            }

            switch (y1)
            {
                case -1:
                    {
                        tmpY = FieldY - 1;
                        break;
                    }

                case FieldY:
                    {
                        tmpY = 0;
                        break;
                    }

                default:
                    {
                        tmpY = y1;
                        break;
                    }
            }

        }

        static void PrintField(int[,] Field)
        {
            Console.Write("┌");
            for (var i = 0; i < FieldY*2; i++)
            {
                Console.Write("─");
            }
            Console.WriteLine("┐");
            for (var x = 0; x < FieldX; x++)
            {
                Console.Write("│");
                for (var y = 0; y < FieldY; y++)
                {
                    if (Field[x, y] == 1)
                        Console.Write("■ ");
                    else
                        Console.Write("  ");
                }
                Console.Write("│");
                Console.WriteLine();
            }
            Console.Write("└");
            for (var i = 0; i < FieldY*2; i++)
            {
                Console.Write("─");
            }
            Console.Write("┘");
        }
    }
}
