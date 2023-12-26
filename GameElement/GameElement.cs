using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KingOfExplosions.GameElement
{

    //工具類
    public class Tool
    {
        private int V;
        private System.Threading.Timer timer;
        private object lockObject = new object();
        // 定义倒计时结束时触发的事件
        public event EventHandler Exploded;
        public string str { get; set; }
        public Tool(string str)
        {
            this.str = str;
        }

        //倒數計時工具
        public void reciprocal(int t)
        {
            V = t;
            timer = new System.Threading.Timer(CountDown, null, 0, 100);
        }

        private void CountDown(object state)
        {
            lock (lockObject)
            {
                if (V == 0)
                {
                    timer.Change(Timeout.Infinite, Timeout.Infinite);
                    OnExploded();

                }
                else if (V > 0)
                {
                    V -= 1;
                }

            }
        }

        //倒數結束事件
        protected virtual void OnExploded()
        {
            Exploded?.Invoke(this, EventArgs.Empty);
        }
    }

    // 遊戲元素基類
    public class GameElement
    {
        public PictureBox Pc { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public int W { get; set; }
        public int H { get; set; }

        protected string path = System.Environment.CurrentDirectory;
    }

    // 炸彈類
    public class Bomb : GameElement
    {
        public int pow;
        public Bomb(int x, int y, int power)
        {
            path = path.Substring(0, path.IndexOf("bin")) + "img\\";
            X = x;
            Y = y;
            W = 40;
            H = 40;
            pow = power;
            InitPic();
        }

        private void InitPic()  //初始圖片
        {
            Pc = new PictureBox();
            Pc.SizeMode = PictureBoxSizeMode.StretchImage;
            Pc.BackColor = Color.Transparent;
            Pc.Image = Image.FromFile(path + "bom.png");

            Pc.SetBounds(X - 3, Y - 3, W, H);
            Pc.BringToFront(); //移到上一層
        }

        //設置圖片
        public void setIamge()
        {
            if (pow == 1)
            {
                Pc.Image = Image.FromFile(path + "explosion.png");
                Pc.SetBounds(X - 50, Y - 50, 150, 150);
            }
            else // 3:5
            {
                Pc.Image = Image.FromFile(path + "explosion2.png");
                Pc.SetBounds(X - 100, Y - 100, 235, 235);
            }
        }
        // Implement IDisposable
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Dispose managed resources
                if (Pc != null)
                {
                    Pc.Dispose();
                    Pc = null;
                }
            }

            // Dispose unmanaged resources
            // (none in this example)

        }

        // Finalizer
        ~Bomb()
        {
            Dispose(false);
        }
    }



    // 箱子類
    public class Box : GameElement
    {

        public Box(int x, int y, Panel panel)
        {
            path = path.Substring(0, path.IndexOf("bin")) + "img\\";
            X = x;
            Y = y;
            W = 50;
            H = 50;
            InitPic();
        }

        private void InitPic()
        {
            Pc = new PictureBox();
            Pc.SizeMode = PictureBoxSizeMode.StretchImage;
            Pc.BackColor = Color.Transparent;
            Pc.Image = Image.FromFile(path + "box.png");
            Pc.SetBounds(X, Y, W, H);
        }

        // Implement IDisposable
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Dispose managed resources
                if (Pc != null)
                {
                    Pc.Dispose();
                    Pc = null;
                }
            }

            // Dispose unmanaged resources
            // (none in this example)

        }

        // Finalizer
        ~Box()
        {
            Dispose(false);
        }
    }

    // 障礙物類
    public class Obstacle : GameElement
    {
        public Obstacle(int x, int y, int type)
        {
            path = path.Substring(0, path.IndexOf("bin")) + "img\\";
            X = x;
            Y = y;
            W = 50;
            H = 50;
            InitPic(type);
        }

        private void InitPic(int type)
        {
            Pc = new PictureBox();
            Pc.SizeMode = PictureBoxSizeMode.StretchImage;
            Pc.BackColor = Color.Transparent;
            if (type == 1) Pc.Image = Image.FromFile(path + "wall.png");
            else if (type == 2) Pc.Image = Image.FromFile(path + "wall1.png");
            Pc.SetBounds(X, Y, W, H);
        }

        private void InitPic1(int type)
        {
            Pc = new PictureBox();
            Pc.SizeMode = PictureBoxSizeMode.StretchImage;
            Pc.BackColor = Color.Transparent;
            if (type == 1) Pc.Image = Image.FromFile(path + "wall2.png");
            else if (type == 2) Pc.Image = Image.FromFile(path + "wall1.png");
            Pc.SetBounds(X, Y, W, H);
        }

    }

    //道具
    public class Prop : GameElement
    {
        public int type { get; set; }
        public Prop(int x, int y, int type)
        {
            path = path.Substring(0, path.IndexOf("bin")) + "img\\";
            X = x;
            Y = y;
            W = 50;
            H = 50;
            this.type = type;
            InitPic();
        }

        private void InitPic()
        {
            Pc = new PictureBox();
            Pc.SizeMode = PictureBoxSizeMode.StretchImage;
            Pc.BackColor = Color.Transparent;
            Pc.SetBounds(X, Y, W, H);
            switch (type)
            {
                case 5:
                    Pc.Image = Image.FromFile(path + "run.png");
                    break;
                case 6:
                    Pc.Image = Image.FromFile(path + "Protect.png");
                    break;
                case 7:
                    Pc.Image = Image.FromFile(path + "banana.png");
                    break;
                case 8:
                    Pc.Image = Image.FromFile(path + "power.png");
                    break;
                case 9:
                    Pc.Image = Image.FromFile(path + "love.png");
                    break;
            }
        }
    }
}
