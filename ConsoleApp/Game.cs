using System;
namespace ConsoleApp
{
    public class Game
    {
        protected static int origRow;
        protected static int origCol;
        protected static string map;

        // запуск игры
        public Game(string textFile)
        {
            map = textFile;
            Console.Clear();
            EventLoop eventLoop = new EventLoop();
            eventLoop.LeftHandler += OnLeft;
            eventLoop.RightHandler += OnRight;
            eventLoop.UpHandler += OnTop;
            eventLoop.DownHandler += OnBot;

            // инициализация карты
            using (StreamReader reader = new StreamReader(textFile))
            {
                string line = reader.ReadLine();
                //Continue to read until you reach end of file
                while (line != null)
                {
                    //write the line to console window
                    Console.WriteLine(line);
                    //Read the next line
                    line = reader.ReadLine();
                }
                //close the file
                reader.Close();
            }
            Console.SetCursorPosition(1, 1);
            origRow = Console.CursorTop;  // Пусть карты такие, что 1 1 всегда свободна
            origCol = Console.CursorLeft;
            Console.SetCursorPosition(origCol, origRow);
            Console.Write("@");
            eventLoop.Run();
        }

        // возвращает элемент карты на интересующей позиции
        protected static char FindElement(int x, int y)
        {
            using (StreamReader reader = new StreamReader(map))
            {
                string line = reader.ReadLine();

                for (int row = 0; row < y; row++)
                {
                    if (line == null)
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                    //Read the next line
                    line = reader.ReadLine();
                }

                if (x > line.Length)
                {
                    throw new ArgumentOutOfRangeException();
                }

                reader.Close();
                return line[x];
            }
        }

        //Move
        protected void MoveAt(int x, int y)
        {
            try
            {
                if (FindElement(origCol + x, origRow + y) != ' ')
                {                    
                    throw new ArgumentException();
                }
                Console.SetCursorPosition(origCol, origRow);
                Console.Write(" ");
                Console.SetCursorPosition(origCol + x, origRow + y);
                Console.Write("@");
                origCol += x;
                origRow += y;
            }   
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void OnLeft(object sender, EventArgs args)
        {
            MoveAt(-1, 0);
        }

        public void OnRight(object sender, EventArgs args)
        {
            MoveAt(1, 0);
        }

        public void OnTop(object sender, EventArgs args)
        {
            MoveAt(0, -1);
        }

        public void OnBot(object sender, EventArgs args)
        {
            MoveAt(0, 1);
        }
    }
}