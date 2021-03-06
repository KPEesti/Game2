using System.Drawing;
using System.Windows.Forms;

namespace Game_2
{
    public sealed partial class Form1 : Form
    {
        private readonly PLayer _player = new PLayer(256, 128);
        private readonly Enemy1 _enemy1 = new Enemy1(512, 128);

        public Form1()
        {
            DoubleBuffered = true;
            ClientSize = new Size(
                GameController.MapWidth * 32,
                GameController.MapHeight * 32);


            var timer = new Timer {Interval = 100};
            timer.Tick += (sender, args) =>
            {
                foreach (var bullet in _player.Bullets)
                {
                    bullet.Move();
                    if (!bullet.IsFlying) _player.Bullets.Remove(bullet);
                }

                var playerPos = new Point(_player.X, _player.Y);
                _enemy1.PlayerPos = playerPos;

                _enemy1.MoveToPlayer();
                Invalidate();
            };
            timer.Start();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            GameController.DrawMap(g);
            _player.PlayAnimation(g);
            g.FillRectangle(Brushes.Indigo, _enemy1.Body);
            foreach (var bullet in _player.Bullets) bullet.PlayAnimation(g);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.A:
                    _player.MoveLeft();
                    break;
                case Keys.D:
                    _player.MoveRight();
                    break;
                case Keys.W:
                    _player.MoveUp();
                    break;
                case Keys.S:
                    _player.MoveDown();
                    break;
                case Keys.Right:
                    _player.SpawnBullet(Rotation.Right);
                    break;
                case Keys.Left:
                    _player.SpawnBullet(Rotation.Left);
                    break;
                case Keys.Up:
                    _player.SpawnBullet(Rotation.Up);
                    break;
                case Keys.Down:
                    _player.SpawnBullet(Rotation.Down);
                    break;
            }
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            _player.SetAnimationConfiguration(0);
        }
    }
}