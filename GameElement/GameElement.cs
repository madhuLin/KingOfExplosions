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
    // 遊戲元素基類
    public class GameElement
    {
        public PictureBox Pc { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public int W { get; set; }
        public int H { get; set; }

        protected string path = System.Environment.CurrentDirectory + "\\Img\\";
    }

    // 炸彈類
    public class Bomb : GameElement
    {
        private int V;
        private bool type = true;
        private System.Threading.Timer timer;
        private object lockObject = new object();
        // 定义倒计时结束时触发的事件
        public event EventHandler Exploded;
        Panel panel;

        public Bomb(int x, int y, Panel panel)
        {
            X = x;
            Y = y;
            W = 40;
            H = 40;
            this.panel = panel;
            InitPic();
            reciprocal(25);
        }

        private void InitPic()
        {
            Pc = new PictureBox();
            Pc.SizeMode = PictureBoxSizeMode.StretchImage;
            Pc.BackColor = Color.Transparent;
            Pc.Image = Image.FromFile(path + "bom.png");
            Pc.SetBounds(X, Y, W, H);
            Pc.BringToFront(); //移到上一層
        }

        private void reciprocal(int t)
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
                    Console.WriteLine("V" + V.ToString());
                    timer.Change(Timeout.Infinite, Timeout.Infinite);
                    Explode();
                }
                else if (V > 0)
                {
                    V -= 1;
                }
            }
        }

        public void Explode()
        {
            lock (lockObject)
            {
                if (type)
                {
                    Pc.Image = Image.FromFile(path + "explosion.png");
                    Pc.Invoke((MethodInvoker)delegate
                    {
                        Pc.SetBounds(X-40, Y-40, 120, 120);
                    });
                    OnExploded();
                    type = false;
                    reciprocal(5);
                }
                else
                {
                    Pc.Invoke((MethodInvoker)delegate
                    {
                        panel.Controls.Remove(Pc);
                    });
                    //OnExploded();
                    Pc.Image = null;
                    type = true;
                    return;
                }
                // 爆炸的邏輯
                Console.WriteLine("Boom! Exploded!");
            }
            // 触发 Exploded 事件的方法
        }
        protected virtual void OnExploded()
        {
            Exploded?.Invoke(this, EventArgs.Empty);
        }

        private bool disposed = false;

        // 实现 IDisposable 接口的 Dispose 方法
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // 实际的资源释放逻辑
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // 释放托管资源
                    if (timer != null)
                    {
                        timer.Dispose();
                        timer = null;
                    }
                }

                // 释放非托管资源

                disposed = true;
            }
        }

    }

    // 箱子類
    public class Box : GameElement
    {
        int[] Probability = {-1,3,6, 6,4,5};

        public Box(int x, int y, Panel panel)
        {
            X = x;
            Y = y;
            W = 50;
            H = 50;
            InitPic();
        }

        public int getProp()
        {
            Random random = new Random();
            int num = random.Next(Probability.Length);
            Console.WriteLine(num);
            return Probability[num];
        }
        private void InitPic()
        {
            Pc = new PictureBox();
            Pc.SizeMode = PictureBoxSizeMode.StretchImage;
            Pc.BackColor = Color.Transparent;
            Pc.Image = Image.FromFile(path + "box.png");
            Pc.SetBounds(X, Y, W, H);
        }
        public void Open()
        {
            // 開啟箱子的邏輯
            Console.WriteLine("Box opened!");
        }
    }

    // 障礙物類
    public class Obstacle : GameElement
    {
        public Obstacle(int x, int y)
        {
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
            Pc.Image = Image.FromFile(path + "wall.png");
            Pc.SetBounds(X, Y, W, H);
        }

        public void Block()
        {
            // 障礙物的邏輯
            Console.WriteLine("Obstacle blocking the way!");
        }
    }

    public class Prop : GameElement
    {
        public int type { get; set; }
        public double ratio = 1.6;
        public Prop(int x, int y, int type)
        {
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
            switch(type)
            {
                case 3:
                    Pc.Image = Image.FromFile(path + "run.png");
                    break;
                case 4:
                    Pc.Image = Image.FromFile(path + "Protect.png");
                    break;
                case 5:
                    Pc.Image = Image.FromFile(path + "banana.png");
                    break;
                case 6:
                    Pc.Image = Image.FromFile(path + "power.png");
                    break;
            }
        }
    }
}
