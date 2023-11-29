using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KingOfExplosions.GameElement;

namespace KingOfExplosions
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.KeyPreview = true; // 啟用按鍵事件的 Form 預覽
        //buttonFocus.Select();
        }
        const int baseL = 50, N = 10;
        private string path = System.Environment.CurrentDirectory + "\\";
        bool play = false;
        int[][] arr = new int[N][];
        PictureBox[, ] arrPictureBox = new PictureBox[N, N];

        private void Init()
        {
            pictureBox1P.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2P.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1P.Image = imageList1.Images[2];
            pictureBox2P.Image = imageList1.Images[3];
            panel1.Controls.Add(pictureBox1P);
            panel1.Controls.Add(pictureBox2P);
            panel1.SetBounds(20, 20, 500, 500);

            if (File.Exists(path + "Terrain.txt"))
            {
                using (StreamReader sr = new StreamReader(path + "Terrain.txt"))
                {
                    int i = 0;
                    string line = "";
                    while ((line = sr.ReadLine()) != null)
                    {
                        arr[i] = new int[10];
                        for (int j = 0; j < line.Length; j++) arr[i][j] = line[j]-'0';
                        i++;
                        //listBox1.Items.Add(line);
                    }
                    
                }
            }
            
            for(int i = 0; i < arr.Length; i++)
            {

                for(int j = 0; j < arr[i].Length; j++)
                {
                    if (arr[i][j] == 1)
                    {
                        Obstacle obstacle = new Obstacle(j * baseL, i * baseL);
                        panel1.Controls.Add(obstacle.Pc);
                    }
                    else if(arr[i][j] == 2)
                    {
                        Box box = new Box(j * baseL, i * baseL, panel1);
                        panel1.Controls.Add(box.Pc);
                        arrPictureBox[i, j] = box.Pc;
                    }
                }
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Init();
            
            PictureBox[][] pictureBoxes = new PictureBox[10][];
            
            // 設定背景圖像的顯示模式
            panel1.BackgroundImageLayout = ImageLayout.Stretch;
            // 初始化 PictureBox 並添加到控制項
            //for (int i = 0; i < pictureBoxes.Length; i++)
            //{
            //    pictureBoxes[i] = new PictureBox[10]; // 創建每一行的 PictureBox 陣列

            //    for (int j = 0; j < pictureBoxes[i].Length; j++)
            //    {
            //        PictureBox pictureBox = new PictureBox();
            //        pictureBoxes[i][j] = pictureBox;

            //        // 設定 PictureBox 的位置和大小
            //        int pictureBoxWidth = 50; // 設定寬度
            //        int pictureBoxHeight = 50; // 設定高度
            //        int pictureBoxSpacing = 0; // 設定 PictureBox 之間的間距
            //        pictureBox.Image = imageList1.Images[0];
            //        pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            //        pictureBox.SendToBack();
            //        pictureBox.SetBounds(j * (pictureBoxWidth + pictureBoxSpacing), i * (pictureBoxHeight + pictureBoxSpacing), pictureBoxWidth, pictureBoxHeight);

            //        // 將 PictureBox 添加到 Panel 的控制項集合中
            //        panel1.Controls.Add(pictureBox);
            //    }
            //}
            listBox1.Items.Add(panel1.Width + " "+panel1.Height);
            
        }

        private void BombExploded(object sender, EventArgs e)
        {
            Bomb bomb = (Bomb)sender;

            //listBox1.Items.Add("BombExploded");
            CheckBom(bomb.X, bomb.Y);
        }
        private bool cnaWalk(int x, int y)
        {
            for (int i = y/50; i < Math.Min(y / 50 + 2, N); i++)
            {
                for (int j = x / 50; j < Math.Min(x/50+2,N); j++)
                {
                    if (arr[i][j] == 1 || arr[i][j] == 2)
                    {
                        int xW = j * baseL, yW = i * baseL;
                        if (x > xW && x < xW + 50 && y > yW && y < yW + 50) return false;
                        if (x > xW && x  < xW + 50 && y + 40 > yW && y + 40 < yW + 50) return false;
                        if (x+40 > xW && x +40< xW + 50 && y > yW && y < yW + 50) return false;
                        if (x+40 > xW && x +40< xW + 50 && y+40 > yW && y+40 < yW + 50) return false;
                    }
                }
            }
            return true;
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

            //if (play == false) return;
            switch (e.KeyCode)
            {
                case Keys.A:
                    if (pictureBox1P.Left >= 0 && cnaWalk(pictureBox1P.Left-5, pictureBox1P.Top)) pictureBox1P.Left -= 5; //左移
                    
                    break;
                case Keys.D:
                    if (pictureBox1P.Right < panel1.Width && cnaWalk(pictureBox1P.Left+ 5, pictureBox1P.Top)) pictureBox1P.Left += 5; //右移
                    break;
                case Keys.W:
                    if (pictureBox1P.Top >= 0 && cnaWalk(pictureBox1P.Left, pictureBox1P.Top-5)) pictureBox1P.Top -= 5; //上移
                    break;
                case Keys.S:
                    if(pictureBox1P.Bottom < panel1.Height && cnaWalk(pictureBox1P.Left, pictureBox1P.Top + 5)) pictureBox1P.Top += 5; //下移
                    break;
                case Keys.Space:

                    listBox1.Items.Add(pictureBox1P.Left + "   " + pictureBox1P.Top);
                    Bomb bomb = new Bomb(pictureBox1P.Left, pictureBox1P.Top, panel1);
                    bomb.Exploded += BombExploded;
                    listBox1.Items.Add(bomb.Pc.Location);
                    panel1.Controls.Add(bomb.Pc);
                    
                    break;
            }
            buttonFocus.Select();
        }

        private void RemovePictureBox(int r, int c)
        {
            panel1.Controls.Remove(arrPictureBox[r, c]);
        }

        private void CheckBom(int x, int y)
        {
            x /= 50;
            y /= 50;
            int[] dir = new int[] { 0,1,0,-1,0};
            for(int d = 0; d < dir.Length-1; d++)
            {
                int r = y + dir[d], c = x + dir[d + 1];
                if (r < 0 || c < 0 || r >= N || c >= N) continue;
                if(arr[y+dir[d]][x+dir[d+1]] == 2)
                {
                    //listBox1.Items.Add(y.ToString() + x.ToString());
                    arr[r][c] = 0;
                    if (panel1.InvokeRequired)
                    {
                        // 如果不在 UI 线程上，使用 Invoke 来在 UI 线程上执行移除操作
                        panel1.Invoke(new Action(() => RemovePictureBox(r, c)));
                    }
                    else
                    {
                        // 在 UI 线程上执行移除操作
                        panel1.Controls.Remove(arrPictureBox[r, c]);

                    }
                }
            }
            

        }
    }
}
