using System.Drawing;
using System.Windows.Forms;

namespace Game_2
{
    public sealed partial class Form1 : Form
    {
        private readonly PLayer _player = new PLayer(256, 128);

        public Form1()
        {
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            ClientSize = new Size(
                GameController.MapWidth * 32,
                GameController.MapHeight * 32);


            var timer = new Timer {Interval = 100};
            timer.Tick += (sender, args) => { Invalidate(); };
            timer.Start();

            Paint += (sender, args) =>
            {
                var g = args.Graphics;
                GameController.DrawMap(g);
                _player.PlayAnimation(g);
            };
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.A:
                case Keys.Left:
                    _player.MoveLeft();
                    break;
                case Keys.D:
                case Keys.Right:
                    _player.MoveRight();
                    break;
                case Keys.W:
                case Keys.Up:
                    _player.MoveUp();
                    break;
                case Keys.S:
                case Keys.Down:
                    _player.MoveDown();
                    break;
            }
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            _player.SetAnimationConfiguration(0);
        }
    }
}