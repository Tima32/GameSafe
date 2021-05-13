using System;
using System.Collections.Generic;
using SafeLibrary;
using System.Runtime.InteropServices;
using System.Threading;

namespace Game_safe_console
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleToolInit();
            origRow = Console.CursorTop;
            origCol = Console.CursorLeft;
            Console.Write("Режим с курсором - true | без - false: ");
            while (true)
            {
                try
                {
                    cursor_mode = Convert.ToBoolean(Console.ReadLine());
                    if (cursor_mode == true)
                    {
                        Console.WriteLine("Переключать ручки можно левой или правой кнопками мыши.\nЧтобы, работал левый клик необходимо отключить выделение в настройках консоли.");
                    }
                    break;
                }
                catch
                {
                    Console.WriteLine("Неверный формат. Попробуйте сново.");
                }
            }
            Console.Write("Размер поля: ");
            while (true)
            {
                try
                {
                    size = Convert.ToInt32(Console.ReadLine());
                    break;
                }
                catch
                {
                    Console.WriteLine("Неверный формат. Попробуйте сново.");
                }
            }
            //Console.Write(size);
            sl.SetSize(size);
            sl.Randomization();
            Console.Clear();

            drawMap();
            while (true)
            {
                if (cursor_mode)
                {
                    try
                    {
                        int x, y;
                        if(GetMousePos(out x, out y))
                            sl.Turn(x, y);
                    }
                    catch
                    {

                    }
                }
                else
                {
                    try
                    {
                        Console.Write("x ручки: ");
                        int x = Convert.ToInt32(Console.ReadLine()) - 1;
                        Console.Write("y ручки: ");
                        int y = Convert.ToInt32(Console.ReadLine()) - 1;
                        sl.Turn(x, y);
                    }
                    catch
                    {
                        Console.WriteLine("Вы ввели не правельно координаты ручки.\nНажмите Enter.");
                        Console.ReadLine();
                    }
                }
                reDrawMap();
                if (sl.IsOpen())
                {
                    Console.WriteLine("Сейф открыт");
                    Console.Read();
                    return;
                }
            }
        }
        static void drawMap()
        {
            Console.Clear();
            for (int x = 0; x < size; ++x)
            {
                for (int y = 0; y < size; ++y)
                {
                    WriteAt(sl[x][y] ? "+" : "-", x, y);
                }
            }
            Console.WriteLine();
            CopyMap(sl);
        }
        static void reDrawMap()
        {
            if (!cursor_mode)
            {
                for (int c_top = Console.CursorTop; size != Console.CursorTop; --c_top)
                {
                    Console.SetCursorPosition(0, c_top);
                    Console.Write(new string(' ', Console.WindowWidth));
                }
            }
            for (int x = 0; x < size; ++x)
            {
                for (int y = 0; y < size; ++y)
                {
                    if (drawn_map[x][y] != sl[x][y])
                        WriteAt(sl[x][y] ? "+" : "-", x, y);
                }
            }
            Console.SetCursorPosition(0, size);
            CopyMap(sl);
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

        static bool cursor_mode = true;
        static int origRow;
        static int origCol;
        static List<List<bool>> drawn_map = new List<List<bool>>();

        [DllImport("..\\ConsoleTool\\ConsoleTool.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern void ConsoleToolInit();
        [DllImport("..\\ConsoleTool\\ConsoleTool.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern bool GetMousePos(out Int32 x, out Int32 y);
    }
}
