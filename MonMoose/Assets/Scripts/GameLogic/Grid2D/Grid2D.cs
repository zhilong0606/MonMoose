using System;

namespace MonMoose.Logic
{
    [Serializable]
    public struct Grid2D
    {
        public int x;
        public int y;

        public Grid2D(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}