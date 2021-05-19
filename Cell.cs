using System.Drawing;

namespace Game_2
{
    public class Cell
    {
        public int X; 
        public int Y; // X и Y соответствуют координатам левого верхнего угла клетки
        private const int Width = 32;
        private const int Height = 32;
        public readonly bool HaveCollision;
        public CellType CellType;
        public Rectangle Rect;

        public Cell(int x, int y,bool collision)
        {
            X = x;
            Y = y;
            HaveCollision = collision;
            CellType = collision ? CellType.Wall : CellType.Field;
            Rect = new Rectangle(X, Y, Width, Height);
        }
    }
}