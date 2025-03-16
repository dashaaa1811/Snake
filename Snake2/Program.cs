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
        //объявление глобальных переменных для создания и управления игрой
        static int width = 40;//ширина поля
        static int height = 20;//высота поля
        static int snakeLength = 3;//первоначальная длина змейки
        static int foodX;//координаты еды по оси x
        static int foodY;//координаты еды по оси y
        static int[]snakeX= new int[100];//координаты змейки по оси x   (массив из 100 элементов)
        static int[]snakeY= new int[100]; //координаты змейки по оси y   (массив из 100 элементов)
        static Random r = new Random();//рандомайзер
        static bool isGameOver = false;//флаг окончания игры
        static int moveX = 1;//определяет направление движения змейки по оси x (1 - движение вправо, -1 - влево, 0 - не меняется)
        static int moveY = 0;//определяет направление движения змейки по оси y (1 - движение вправо, -1 - влево, 0 - не меняется)
        static int speed = 500;//скорость игры в миллисекундах
        static int collectedFood = 0;//счетчик съеденной еды
        static void Main(string[] args)
        {
            Console.CursorVisible = false;//скрывает курсор
            Console.SetWindowSize(width+1, height+1);//устанавливает размер окна консоли
            CreatGame();//инициализируем игру (устанавывливаем положения змейки и еды)

            while (!isGameOver)//цикл продолжается пока игра не закончена
            {
                if (Console.KeyAvailable)//если была нажата клавиша
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);//сохранение нажатой клавиши

                    switch (key.Key)//выбор конкретно нажатой клавиши (определяем какую клавишу нажал игрок)
                    {
                        case ConsoleKey.W: moveY = -1; moveX = 0; break;//установка направления движения вверх
                        case ConsoleKey.S: moveY = 1; moveX = 0; break;//установка направления движения вниз
                        case ConsoleKey.A: moveX = -1; moveY = 0; break;//установка направления движения влево
                        case ConsoleKey.D: moveX = 1; moveY = 0; break;//установка направления движения вправо

                    }

                }
                Movement(moveY,moveX);//вызываем функцию движения змейки
                PrintGame();//обновляем вывод на экране
                Thread.Sleep(speed);//задержка выполнения команд, миллисекунды. скорость игры (задержка для регулирования)
            }
                Console.SetCursorPosition((width/2)-5, height/2);//устанавлевает курсор в указанную позицию (в центр экрана)
            Console.WriteLine("Game Over");//выводим сообщение о конце игры
            Console.ReadKey();//задержка выполнения команды до нажатия любой клавиши (ждём нажатия клавиши для завершения игры - закрытие окна с игрой)
        }

        /// <summary>
        /// создаёт игровое поле, устанавливает змейку в указанную (начальную) позицию.
        /// вызывает метод для создания еды
        /// </summary>
        static void CreatGame()
        {
            snakeX[0] = width / 2;//устанавливаем позицию головы змейки по центру по оси x
            snakeY[0] = height / 2;//устанавливаем позицию головы змейки по центру по оси y
            for (int i = 1; i < snakeLength; i++)//создаём тело змейки
            {
                snakeX[i]=snakeX[i-1]-1;//каждую следующую часть тела змейки смещаем влево
                snakeY[i]=snakeY[i-1];//по оси y змейка не изменится
            }
            CreatFood();//вызываем метод для создания еды
        }

        /// <summary>
        /// метод генирирует случайное положение для еды
        /// </summary>
        static void CreatFood()
        {
            foodX = r.Next(0,width);//случайным образом генирируем координату x для еды
            foodY = r.Next(1,height);//случайным образом генирируем координату y для еды
        }

        /// <summary>
        /// метод управляет движением змейки.
        /// проверяет столкновение и при необходимости увеличивает длину змейки
        /// или изменяет скорость игры
        /// </summary>
        /// <param name="y">направление движения по горизонтали</param>
        /// <param name="x">направление движения по вертикали</param>
        static void Movement(int y, int x)
        {
            int newHeadX = snakeX[0]+x;//вычисляем новые координаты головы змейки по x
            int newHeadY = snakeY[0]+y;//вычисляем новые координаты головы змейки по y

            //подбор еды
            if (newHeadX == foodX && newHeadY == foodY)
            {
                snakeLength++;//увеличивает длинну змейки
                collectedFood++;//увеличиваем счётчик еды
                speed -= 5;//уменьшаем скорость игры (игра становится быстрее)
                CreatFood();//генирируем новую еду
            }

            //проверка столкновений змейки с краем экрана или с самой собой
            if (newHeadX < 0 || newHeadX >= width || 
                newHeadY<1 || newHeadY >= height ||
                (snakeX.Take(snakeLength).Contains(newHeadX) && 
                snakeY.Take(snakeLength).Contains(newHeadY)))
            {
                isGameOver = true;//если произошло столкновение, заканчиваем игру
                return;
            }

            //непосредственное движение змейки
            for (int i = snakeLength-1; i > 0; i--)
            {
                snakeX[i]= snakeX[i-1];//копируем координаты предыдущей части тела змейки по оси x
                snakeY[i]= snakeY[i-1];//копируем координаты предыдущей части тела змейки по оси y

            }
            snakeX[0] = newHeadX;//обновляем координаты головы змейки по оси x
            snakeY[0] = newHeadY; //обновляем координаты головы змейки по оси y
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
