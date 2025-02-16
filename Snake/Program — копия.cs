using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    internal class Program
    {
        static string[,] field = new string[20, 30];
        static string head = "☺";
        static int x = 10;
        static int y = 15;
        static void Main(string[] args)
        {
            CreateField();
            PrintField();
            Movement();
        }
        static void Movement()
        {
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.W: x++;  break;
                case ConsoleKey.S: x--;  break;
                case ConsoleKey.A: y++;  break;
                case ConsoleKey.D: y--;  break;
                   
            }

            PrintField();
            Movement();
        }
        static void CreateField()
        {
            for (int i = 0; i < field.GetLength(0); i++)
            {
                for(int j = 0; j < field.GetLength(1); j++)
                {
                    field[i, j] = ".";
                }
            }
        }

        static void PrintField()
        {
            Console.Clear();
            for (int i = 0; i < field.GetLength(0); i++)
            {
                for(int j = 0; j < field.GetLength(1); j++)
                {
                    if (i == x && j == y)
                    {
                        field[i,j]=head;
                    }
                    else
                    {
                        field[i, j] = ".";
                    }
                    Console.Write(" "+field[i,j]+" ");
                }
                Console.WriteLine();
            }

        }


    }
}
