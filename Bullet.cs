using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Game_2
{
    public class Bullet
    {
        public bool IsFlying = true;
        
        private Point _position;
        private readonly Rotation _rotation;

        private Rectangle _body;

        private readonly Image _sheet = new Bitmap(Path
            .Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent?.FullName ?? string.Empty,
                "Sprites\\Bullet.png"));

        private const int Speed = 10;

        public Bullet(Point position, Rotation rotation)
        {
            _position = position;
            _rotation = rotation;
            _body = new Rectangle(position, new Size(10, 10));
        }

        #region Movement

        public void Move()
        {
            switch (_rotation)
            {
                case Rotation.Right:
                    MoveRight();
                    break;
                case Rotation.Left:
                    MoveLeft();
                    break;
                case Rotation.Up:
                    MoveUp();
                    break;
                case Rotation.Down:
                    MoveDown();
                    break;
            }
        }

        private bool CanMoveRight()
        {
            var walls = FindWalls();
            return walls.All(cell => cell.X <= _body.X);
        }

        private void MoveRight()
        {
            if (!CanMoveRight())
            {
                IsFlying = false;
                return;
            }
            _body.X += Speed;
            _position.X += Speed;
        }

        private bool CanMoveLeft()
        {
            var walls = FindWalls();
            return walls.All(cell => cell.X >= _body.X);
        }

        private void MoveLeft()
        {
            if (!CanMoveLeft())
            {
                IsFlying = false;
                return;
            }
            _body.X -= Speed;
            _position.X -= Speed;
        }

        private bool CanMoveUp()
        {
            var walls = FindWalls();
            return walls.All(cell => cell.Y >= _body.Y);
        }

        private void MoveUp()
        {
            if (!CanMoveUp())
            {
                IsFlying = false;
                return;
            }
            _body.Y -= Speed;
            _position.Y -= Speed;
        }

        private bool CanMoveDown()
        {
            var walls = FindWalls();
            return walls.All(cell => cell.Y <= _body.Y);
        }

        private void MoveDown()
        {
            if (!CanMoveDown())
            {
                IsFlying = false;
                return;
            }
            _body.Y += Speed;
            _position.Y += Speed;
        }

        private IEnumerable<Cell> FindWalls()
        {
            return GameController.Map.Cast<Cell>().ToList()
                .Where(x => x.CellType == CellType.Wall && x.Rect.IntersectsWith(_body)).ToList();
        }

        #endregion

        public void PlayAnimation(Graphics g)
        {
            //g.FillRectangle(Brushes.Indigo, body);
            g.DrawImage(_sheet, _position);
        }
    }
}