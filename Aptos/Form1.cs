using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aptos
{
    public partial class Form1 : Form
    {

        public Bitmap HandlerTexture=Resource1.Handler,
            TargetTexture = Resource1.Target;

        private Point _TargetPosition = new  Point(200,200);

        private Point _direction = Point.Empty;
        int _score = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void timer_Tick1(object sender, EventArgs e)
        {
            Refresh();
            SetStyle(ControlStyles.OptimizedDoubleBuffer | 
                ControlStyles.AllPaintingInWmPaint | 
                ControlStyles.UserPaint, true);
            UpdateStyles();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            Random r = new Random();
            timer2.Interval = r.Next(10, 10000);
            _direction.X = r.Next(-1, 2);
            _direction.Y = r.Next(-1, 2);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            var localPosition = this.PointToClient(Cursor.Position);

            _TargetPosition.X += _direction.X*10;
            _TargetPosition.Y += _direction.Y*10;

            if(_TargetPosition.X < 0 ||_TargetPosition.X > 800 )
            {
                _direction.X *= -1;
                
            }
            if(_TargetPosition.Y <0 ||  _TargetPosition.Y > 390)
            {
                _direction.Y *= -1;
;
            }

            Point between = new Point(localPosition.X - _TargetPosition.X, localPosition.Y - _TargetPosition.Y);
            float distance = (float)Math.Sqrt((between.X * between.X) + (between.Y * between.Y));
            if (distance < 20)
            {
                AddScore(1);
            }


            var handlerRect = new Rectangle(localPosition.X - 50, localPosition.Y - 50, 100, 100);
            var targetRect = new Rectangle(_TargetPosition.X- 50, _TargetPosition.Y - 50, 100, 100);
            g.DrawImage(HandlerTexture, handlerRect);
            g.DrawImage(TargetTexture, targetRect);
        }

        //private void scoreLabel_Click(object sender, EventArgs e)
        //{
        //}

        private void AddScore (int score)
        {
            _score += score;
            scoreLabel.Text = _score.ToString();
        }
    }
}
