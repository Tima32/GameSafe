using System;
using System.Collections.Generic;

namespace Game_safe_console
{
    class Program
    {
        class SafeLogick
        {
            public SafeLogick() { handles = new List<List<bool>>(); }
            public void SetSize(int size)
            {
                this.size = size;
                handles.Clear();
                Random rand = new Random();
                bool active = Convert.ToBoolean(rand.Next() % 2);
                for (int x = 0; x < size; ++x)
                {
                    List<bool> colum = new List<bool>();
                    for (int y = 0; y < size; ++y)
                    {
                        colum.Add(active);
                    }
                    handles.Add(colum);
                }
            }

            public void Randomization()
            {
                Random rand = new Random();
                for (int i = 0; i < size * size; ++i)
                {
                    int x = rand.Next() % size;
                    int y = rand.Next() % size;

                    Turn(x, y);
                }
                if (IsOpen())
                    Randomization();
            }

            public void Turn(int xp, int yp)
            {
                for (int x = 0; x < size; ++x)
                    handles[x][yp] = !handles[x][yp];

                for (int y = 0; y < size; ++y)
                {
                    if (yp != y)
                        handles[xp][y] = !handles[xp][y];
                }
            }

            public bool IsOpen()
            {
                for (int x = 0; x < size; ++x)
                {
                    for (int y = 0; y < size; ++y)
                    {
                        if (handles[0][0] != handles[x][y])
                            return false;
                    }
                }

                return true;
            }

            public List<List<bool>> handles; //true - горизонтально
            public int size = 0;
        }
        static void Main(string[] args)
        {
            origRow = Console.CursorTop;
            origCol = Console.CursorLeft;
            Console.Write("Размер поля: ");
            size = Convert.ToInt32(Console.ReadLine());
            Console.Write(size);
            sl.SetSize(size);
            sl.Randomization();

            drawMap();
            while (true)
            {
                if (cursor_mode)
                {
                    
                }
                else
                {
                    Console.Write("x ручки: ");
                    int x = Convert.ToInt32(Console.ReadLine());
                    Console.Write("y ручки: ");
                    int y = Convert.ToInt32(Console.ReadLine());
                    sl.Turn(x, y);
                }
                reDrawMap();
                if (sl.IsOpen())
                {
                    Console.WriteLine("Сейф открыт");
                    return;
                }
            }
        }
        static void drawMap()
        {
            Console.Clear();
            for(int x = 0; x < size; ++x)
            {
                for (int y = 0; y < size; ++y)
                {
                    WriteAt(sl.handles[x][y] ? "+" : "-", x, y);
                }
            }
            Console.WriteLine();
            CopyMap(sl.handles);
        }
        static void reDrawMap()
        {
            if (!cursor_mode)
            {
                int c_top = Console.CursorTop;
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.Write(new string(' ', Console.WindowWidth));
            }
            for (int x = 0; x < size; ++x)
            {
                for (int y = 0; y < size; ++y)
                {
                    if (drawn_map[x][y] != sl.handles[x][y])
                        WriteAt(sl.handles[x][y] ? "+" : "-", x, y);
                }
            }
            Console.SetCursorPosition(0, size);
            CopyMap(sl.handles);
        }
        static void WriteAt(string s, int x, int y)
        {
            try
            {
                Console.SetCursorPosition(origCol + x, origRow + y);
                Console.Write(s);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }
        static void CopyMap(List<List<bool>> map)
        {
            drawn_map.Clear();

            for (int x = 0; x < size; ++x)
            {
                drawn_map.Add(new List<bool>(map[x]));
            }
        }

        static int size = 0;
        static SafeLogick sl = new SafeLogick();

        static bool cursor_mode = false;
        static int origRow;
        static int origCol;
        static List<List<bool>> drawn_map = new List<List<bool>>();
    }
}
