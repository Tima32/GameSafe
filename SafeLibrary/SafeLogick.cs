using System;
using System.Collections.Generic;

namespace SafeLibrary
{
    public class SafeLogick : List<List<bool>>
    {
        public SafeLogick() { }
        public void SetSize(int size)
        {
            this.size = size;
            base.Clear();
            Random rand = new Random();
            bool active = Convert.ToBoolean(rand.Next() % 2);
            for (int x = 0; x < size; ++x)
            {
                List<bool> colum = new List<bool>();
                for (int y = 0; y < size; ++y)
                {
                    colum.Add(active);
                }
                base.Add(colum);
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
            if (xp >= size || xp < 0 || yp >= size || yp < 0)
                throw new IndexOutOfRangeException();
            for (int x = 0; x < size; ++x)
                base[x][yp] = !base[x][yp];

            for (int y = 0; y < size; ++y)
            {
                if (yp != y)
                    base[xp][y] = !base[xp][y];
            }
        }

        public bool IsOpen()
        {
            for (int x = 0; x < size; ++x)
            {
                for (int y = 0; y < size; ++y)
                {
                    if (base[0][0] != base[x][y])
                        return false;
                }
            }

            return true;
        }
        public int size = 0;
    }
}
