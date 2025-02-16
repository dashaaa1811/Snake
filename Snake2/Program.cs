using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Snake2
{
    internal class Program
    {
        static int width = 40;//ширина поля
        static int height = 20;//высота поля
        static int snakeLength = 3;//первоначальная длина змейки
        static int foodX;//координаты еды
        static int foodY;
        static int[]snakeX= new int[100];//координаты змейки   
        static int[]snakeY= new int[100];   
        static Random r = new Random();//рандомайзер
        static bool isGameOver = false;
        static int moveX = 1;//определяет направление движения змейки
        static int moveY = 0;
        static int speed = 500;
        static int collectedFood = 0;
        static void Main(string[] args)
        {
            Console.CursorVisible = false;//скрывает курсор
            Console.SetWindowSize(width+1, height+1);//устанавливает размер окна консоли
            CreatGame();

            while (!isGameOver)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);//сохранение нажатой клавиши

                    switch (key.Key)//выбор конкретно нажатой клавиши
                    {
                        case ConsoleKey.W: moveY = -1; moveX = 0; break;//установка направления движения
                        case ConsoleKey.S: moveY = 1; moveX = 0; break;
                        case ConsoleKey.A: moveX = -1; moveY = 0; break;
                        case ConsoleKey.D: moveX = 1; moveY = 0; break;

                    }

                }
                Movement(moveY,moveX);
                PrintGame();
                Thread.Sleep(speed);//задержка выполнения команд, миллисекунды. скорость игры
            }
                Console.SetCursorPosition((width/2)-5, height/2);//устанавлевает курсор в указанную позицию
            Console.WriteLine("Game Over");
            Console.ReadKey();//задержка выполнения команды до нажатия любой клавиши
        }

        /// <summary>
        /// создаёт игровое поле, устанавливает змейку в указанную позицию
        /// </summary>
        static void CreatGame()
        {
            snakeX[0] = width / 2;//устанавливаем позицию змейки по центру
            snakeY[0] = height / 2;
            for (int i = 1; i < snakeLength; i++)
            {
                snakeX[i]=snakeX[i-1]-1;
                snakeY[i]=snakeY[i-1];
            }
            CreatFood();
        }

        /// <summary>
        /// установка позиции еды
        /// </summary>
        static void CreatFood()
        {
            foodX = r.Next(0,width);
            foodY = r.Next(1,height);
        }

        /// <summary>
        /// движение змейки
        /// </summary>
        /// <param name="y">направление движения по горизонтали</param>
        /// <param name="x">направление движения по вертикали</param>
        static void Movement(int y, int x)
        {
            int newHeadX = snakeX[0]+x;//изменение позиций
            int newHeadY = snakeY[0]+y;

            //подбор еды
            if (newHeadX == foodX && newHeadY == foodY)
            {
                snakeLength++;//увеличивает длинну змейки
                collectedFood++;
                speed -= 5;//изменение скорости змейки
                CreatFood();
            }

            //проверка столкновений с краями экрана и с самой собой
            if (newHeadX < 0 || newHeadX >= width || 
                newHeadY<1 || newHeadY >= height ||
                (snakeX.Take(snakeLength).Contains(newHeadX) && 
                snakeY.Take(snakeLength).Contains(newHeadY)))
            {
                isGameOver = true;
                return;
            }

            //непосредственное движение
            for (int i = snakeLength-1; i > 0; i--)
            {
                snakeX[i]= snakeX[i-1];
                snakeY[i]= snakeY[i-1];

            }
            snakeX[0] = newHeadX;
            snakeY[0] = newHeadY;   
        }

        /// <summary>
        /// вывод игры в консоль
        /// </summary>
        static void PrintGame()
        {
            Console.Clear();//очистка консоли

            Console.WriteLine(new string('_', width));
            Console.WriteLine($"очки: {collectedFood}");
            for (int i = 0; i < snakeLength; i++)
            {
                Console.SetCursorPosition(snakeX[i], snakeY[i]);
                Console.Write(i==0?"@": "о");//распечатка либо головы, либо хвоста
            }
            Console.SetCursorPosition(foodX, foodY);
            Console.Write("*");
        }
    }
}
