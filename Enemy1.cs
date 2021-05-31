using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Game_2
{
    public class Enemy1
    {
        private int X { get; set; }
        private int Y { get; set; }

        private const int Width = 32;
        private const int Height = 32;

        private const int Speed = 10;
        private const int MaxHealth = 100;

        private int _healthPoint;
        private int _damage = 25;

        public Point PlayerPos { get; set; }

        public Rectangle Body;
        
        public Enemy1(int x, int y)
        {
            X = x + 16;
            Y = y + 16;
            Body = new Rectangle(x, y, Width, Height);

            _healthPoint = MaxHealth;
        }

        public void MoveToPlayer()
        {
            if (PlayerPos.X <= X && Math.Abs(PlayerPos.X - X) > 32)
            {
                MoveLeft();
            }
            else if (PlayerPos.X > X && Math.Abs(PlayerPos.X - X) > 32)
            {
                MoveRight();
            }
            else if (PlayerPos.Y <= Y && Math.Abs(PlayerPos.X - X) <= 32)
            {
                MoveUp();
            }
            else if (PlayerPos.Y > Y && Math.Abs(PlayerPos.X - X) <= 32)
            {
                MoveDown();
            }
        }
        
        #region Movement

        private bool CanMoveRight()
        {
            var walls = FindWalls();
            return walls.All(cell => cell.X <= Body.X);
        }

        public void MoveRight()
        {
            if (!CanMoveRight()) return;
            Body.X += Speed;
            X += Speed;
        }

        private bool CanMoveLeft()
        {
            var walls = FindWalls();
            return walls.All(cell => cell.X >= Body.X);
        }

        public void MoveLeft()
        {
            if (!CanMoveLeft()) return;
            Body.X -= Speed;
            X -= Speed;
        }

        private bool CanMoveUp()
        {
            var walls = FindWalls();
            return walls.All(cell => cell.Y >= Body.Y);
        }

        public void MoveUp()
        {
            if (!CanMoveUp()) return;
            Body.Y -= Speed;
            Y -= Speed;
        }

        private bool CanMoveDown()
        {
            var walls = FindWalls();
            return walls.All(cell => cell.Y <= Body.Y);
        }

        public void MoveDown()
        {
            if (!CanMoveDown()) return;
            Body.Y += Speed;
            Y += Speed;
        }

        private IEnumerable<Cell> FindWalls()
        {
            return GameController.Map.Cast<Cell>().ToList()
                .Where(x => x.CellType == CellType.Wall && x.Rect.IntersectsWith(Body)).ToList();
        }

        #endregion
    }
}