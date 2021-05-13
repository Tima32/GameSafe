using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;



namespace Game_safe
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

    static class Program
    {
        [STAThread]
        static void Main()
        {
            // Запускаем консоль.
            if (AllocConsole())
            {
                Console.WriteLine("Привет мир");
                //Console.ReadLine();
            }

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool AllocConsole();

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool FreeConsole();
    }
}
