using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Game_2
{
    public class PLayer
    {
        private int X { get; set; }
        private int Y { get; set; }

        private const int Width = 32;
        private const int Height = 32;

        private const int PlayerSpeed = 2;
        private const int MaxHealth = 100;

        public int HealthPoint { get; private set; }

        public Rectangle Body;

        private Image Sheet = new Bitmap(Path
            .Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName,
                "Sprites\\Hero\\Soldier-Sheet.png"));

        private int _currentLimit;
        private int _currentFrame;
        private int _currentAnimation;
        private const int IdleFrames = 6;
        private const int RunFrames = 6;

        public PLayer(int x, int y)
        {
            X = x;
            Y = y;
            Body = new Rectangle(x, y, Width, Height);

            HealthPoint = MaxHealth;

            _currentLimit = IdleFrames;
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
            Body.X += PlayerSpeed;
            X += PlayerSpeed;
            SetAnimationConfiguration(1);
        }

        private bool CanMoveLeft()
        {
            var walls = FindWalls();
            return walls.All(cell => cell.X >= Body.X);
        }

        public void MoveLeft()
        {
            if (!CanMoveLeft()) return;
            Body.X -= PlayerSpeed;
            X -= PlayerSpeed;
            SetAnimationConfiguration(2);
        }

        private bool CanMoveUp()
        {
            var walls = FindWalls();
            return walls.All(cell => cell.Y >= Body.Y);
        }

        public void MoveUp()
        {
            if (!CanMoveUp()) return;
            Body.Y -= PlayerSpeed;
            Y -= PlayerSpeed;
            SetAnimationConfiguration(1);
        }

        private bool CanMoveDown()
        {
            var walls = FindWalls();
            return walls.All(cell => cell.Y <= Body.Y);
        }

        public void MoveDown()
        {
            if (!CanMoveDown()) return;
            Body.Y += PlayerSpeed;
            Y += PlayerSpeed;
            SetAnimationConfiguration(1);
        }

        private IEnumerable<Cell> FindWalls()
        {
            return GameController.Map.Cast<Cell>().ToList()
                .Where(x => x.CellType == CellType.Wall && x.Rect.IntersectsWith(Body)).ToList();
        }
        #endregion

        #region Health

        

        #endregion

        #region Animation

        public void PlayAnimation(Graphics g)
        {
            //g.FillRectangle(Brushes.Indigo, Body);
            g.DrawImage(Sheet,new Rectangle(new Point(X,Y), new Size(32, 32)),
                32 * _currentFrame, 32 * _currentAnimation, Width, Height, GraphicsUnit.Pixel);

            if (_currentFrame < _currentLimit - 1)
                _currentFrame++;
            else _currentFrame = 0;
        }

        public void SetAnimationConfiguration(int currentAnimation)
        {
            _currentAnimation = currentAnimation;

            switch (currentAnimation)
            {
                case 0:
                    _currentLimit = IdleFrames;
                    break;
                case 1:
                case 2:
                    _currentLimit = RunFrames;
                    break;
            }
        }

        #endregion
        
    }

    public class Bullet
    {
        private Point _position;
        private double _rotation;
        private int _speed;

        public Bullet(Point position)
        {
            _position = position;
        }
    }
}