using System.Collections.Generic;
using System.Drawing;

namespace Game_2
{
    public class QuadTree
    {
        private const int Capacity = 1;
        private bool _divided;

        private Rectangle _boundary;

        private QuadTree _northWest;
        private QuadTree _northEast;
        private QuadTree _southWest;
        private QuadTree _southEast;

        public List<Cell> Cells = new List<Cell>();

        public QuadTree(Rectangle boundary)
        {
            _boundary = boundary;
        }

        private void SubDivide()
        {
            var nw = new Rectangle
                (_boundary.Location, new Size(_boundary.Width / 2, _boundary.Height / 2));
            _northWest = new QuadTree(nw);
            var ne = new Rectangle
                (_boundary.X + _boundary.Width / 2, _boundary.Y, _boundary.Width / 2, _boundary.Height / 2);
            _northEast = new QuadTree(ne);
            var sw = new Rectangle
                (_boundary.X, _boundary.Y + _boundary.Height / 2, _boundary.Width / 2, _boundary.Height / 2);
            _southWest = new QuadTree(sw);
            var se = new Rectangle
            (_boundary.X + _boundary.Width / 2, _boundary.Y + _boundary.Height / 2, _boundary.Width / 2,
                _boundary.Height / 2);
            _southEast = new QuadTree(se);
        }

        public void Insert(Cell cell)
        {
            if (!_boundary.Contains(cell.Rect)) return;

            if (Cells.Count < Capacity)
            {
                Cells.Add(cell);
            }
            else
            {
                if (!_divided)
                {
                    SubDivide();
                    _divided = true;
                }

                _northWest.Insert(cell);
                _northEast.Insert(cell);
                _southWest.Insert(cell);
                _southEast.Insert(cell);
            }
        }
    }
}