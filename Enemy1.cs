using System.Drawing;

namespace Game_2
{
    public class Enemy1
    {
        private int X { get; set; }
        private int Y { get; set; }

        private const int Width = 32;
        private const int Height = 32;

        private const int EnemySpeed = 10;
        private const int MaxHealth = 100;

        private int _healthPoint;
        private int _damage = 25;

        public Rectangle Body;
        
        public Enemy1(int x, int y)
        {
            X = x + 16;
            Y = y + 16;
            Body = new Rectangle(x, y, Width, Height);

            _healthPoint = MaxHealth;
        }
    }
}