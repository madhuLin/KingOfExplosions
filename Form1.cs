using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        string appPath = System.IO.Path.GetFullPath(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\"));
        bool play = false;

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1P.Image = Image.FromFile(appPath + @"img\p1.jpg");
            pictureBox2P.Image = Image.FromFile(appPath + @"img\p2.png");
            PictureBox[][] pictureBoxes = new PictureBox[10][];

            // 初始化 PictureBox 並添加到控制項
            for (int i = 0; i < pictureBoxes.Length; i++)
            {
                pictureBoxes[i] = new PictureBox[10]; // 創建每一行的 PictureBox 陣列

                for (int j = 0; j < pictureBoxes[i].Length; j++)
                {
                    PictureBox pictureBox = new PictureBox();
                    pictureBoxes[i][j] = pictureBox;

                    // 設定 PictureBox 的位置和大小
                    int pictureBoxWidth = 50; // 設定寬度
                    int pictureBoxHeight = 50; // 設定高度
                    int pictureBoxSpacing = 0; // 設定 PictureBox 之間的間距
                    //pictureBox.Image = KingOfExplosions.Properties.Resources.floor;
                    pictureBox.Image = Image.FromFile(appPath + @"img\floor.png");

                    pictureBox.SetBounds(j * (pictureBoxWidth + pictureBoxSpacing), i * (pictureBoxHeight + pictureBoxSpacing), pictureBoxWidth, pictureBoxHeight);

                    // 將 PictureBox 添加到 Panel 的控制項集合中
                    panel1.Controls.Add(pictureBox);
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

            //if (play == false) return;
            switch (e.KeyCode)
            {
                case Keys.A:
                    if(pictureBox1P.Left >= 0) pictureBox1P.Left -= 5; //左移                    
                    break;
                case Keys.D:
                    if (pictureBox1P.Right < panel1.Width) pictureBox1P.Left += 5; //右移
                    break;
                case Keys.W:
                    if (pictureBox1P.Top >= 0) pictureBox1P.Top -= 5; //上移
                    break;
                case Keys.S:
                    if(pictureBox1P.Bottom < panel1.Height) pictureBox1P.Top += 5; //下移
                    break;
                case Keys.Space:
                    PictureBox pictureBox = new PictureBox();
                    
                    int pictureBoxWidth = 45; // 設定寬度
                    int pictureBoxHeight = 45; // 設定高度
                    listBox1.Items.Add(pictureBox1P.Left +"   "+pictureBox1P.Top);
                    pictureBox.Image = Image.FromFile(appPath + @"\img\bom.png");
                    pictureBox.SetBounds(pictureBox1P.Left, pictureBox1P.Top, pictureBoxWidth, pictureBoxHeight);
                    panel1.Controls.Add(pictureBox);
                    break;
            }
            buttonFocus.Select();

        }
        
    }
}
