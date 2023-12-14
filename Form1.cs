using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using KingOfExplosions.GameElement;
using Newtonsoft.Json;
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
        private string path = System.Environment.CurrentDirectory;
        double runningSpeedBase = 5, runningSpeedRatio = 1;
        int[][] arr = new int[N][];
        Box[, ] arrBox = new Box[N, N];
        Prop[,] arrProp = new Prop[N, N];
        bool walking = true;
        Socket T;                                    //通訊物件
        Thread Th;                                   //網路監聽執行緒
        String User;
        DataUser dataUser;
        PictureBox pictureBoxSelf;
        Dictionary<int, PictureBox> userPictureName = new Dictionary<int, PictureBox>();
        private void InitTmp(int num, string user, string picname)
        {
            dataUser = new DataUser();
            dataUser.UserNumber = num;
            dataUser.User = user;
            dataUser.PicName = picname;
            if (num == 1) pictureBoxSelf = pictureBox1P;
            else if(num == 2) pictureBoxSelf = pictureBox2P;
        }
        private void Init()
        {
            path = path.Substring(0, path.IndexOf("bin"));
            pictureBox1P.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2P.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1P.Image = imageList1.Images[2];
            pictureBox2P.Image = imageList1.Images[3];
            panel1.Controls.Add(pictureBox1P);
            panel1.Controls.Add(pictureBox2P);

            userPictureName.Add(1, pictureBox1P);
            userPictureName.Add(2, pictureBox2P);

            //panel1.SetBounds(20, 20, 500, 500);

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
                        arrBox[i, j] = box;
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
            //listBox1.Items.Add(panel1.Width + " "+panel1.Height);
            
        }

        private void BombExploded(object sender, EventArgs e)
        {
            Bomb bomb = (Bomb)sender;

            //listBox1.Items.Add("BombExploded");
            CheckBom(bomb.X, bomb.Y);
        }
        //private bool cnaWalk(int x, int y)
        //{
        //    for (int i = y/50; i < Math.Min(y / 50 + 2, N); i++)
        //    {
        //        for (int j = x / 50; j < Math.Min(x/50+2,N); j++)
        //        {
        //            if (arr[i][j] == 1 || arr[i][j] == 2)
        //            {
        //                int xW = j * baseL, yW = i * baseL;
        //                if (x > xW && x < xW + 50 && y > yW && y < yW + 50) return false;
        //                if (x > xW && x  < xW + 50 && y + 40 > yW && y + 40 < yW + 50) return false;
        //                if (x+40 > xW && x +40< xW + 50 && y > yW && y < yW + 50) return false;
        //                if (x+40 > xW && x +40< xW + 50 && y+40 > yW && y+40 < yW + 50) return false;
        //            }
        //        }
        //    }
        //    int r = (y + 20) / 50, c = (x+20) / 50;
        //    if (arr[r][c] >= 3)
        //    {
        //        Prop prop = arrProp[r,c];
        //        panel1.Controls.Remove(prop.Pc);
        //        int type = prop.type;
        //        switch (prop.type)
        //        {
        //            case 3:
        //                runningSpeedRatio = prop.ratio;
        //                reciprocal(70, type);
        //                break;
        //            case 4:

        //                break;
        //            case 5:
        //                walking = false;
        //                reciprocal(15, type);
        //                break;
        //            case 6:

        //                break;
        //        }
        //        arr[r][c] = 0;
        //    }
        //        return true;
        //}

        bool canWalkFlag = false, canWalkServer = false;
        private async Task CheckCanWalkResult()
        {
            // 等待canWalkFlag為true，表示已經收到伺服器端的結果
            while (!canWalkFlag)
            {
                await Task.Delay(10); // 避免無窮迴圈造成CPU高佔用，可以根據實際情況調整等待時間
            }
        }

        private async Task<bool> canWalk(DataGame data)
        {
            Send("J" + data);
            await CheckCanWalkResult(); // 等待伺服器端結果
            return canWalkServer;
        }
        void buildMoveData(DataGame data, int type, int x, int y)
        {
            data.UserNumber = dataUser.UserNumber;
            if (type == 0) data.Action = "WALK";
            else if (type == 1) data.Action = "MOVE";
            else data.Action = "BOMB";
            data.Position = new Tuple<int,int> (x, y);
        }
        private async void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (!walking) return;
            int distance = (int)(runningSpeedBase * runningSpeedRatio);
            //if (play == false) return;
            DataGame data = new DataGame();
            bool keyT = true;
            switch (e.KeyCode)
            {
                case Keys.A:
                    if (pictureBoxSelf.Left >= 0) {
                        buildMoveData(data, 0, pictureBoxSelf.Left - distance, pictureBoxSelf.Top);
                        if (await canWalk(data)) pictureBoxSelf.Left -= distance; //左移
                    }
                    
                    break;
                case Keys.D:
                    if (pictureBoxSelf.Right < panel1.Width) {
                        buildMoveData(data, 0, pictureBoxSelf.Left+ distance, pictureBoxSelf.Top);
                        if (await canWalk(data)) pictureBoxSelf.Left += distance; //右移
                        
                    } 
                    break;
                case Keys.W:
                    if (pictureBoxSelf.Top >= 0) {
                        buildMoveData(data, 1, pictureBoxSelf.Left, pictureBoxSelf.Top- distance);
                        if (await canWalk(data)) pictureBoxSelf.Top -= distance; //上移
                    }
                    
                    break;
                case Keys.S:
                    if (pictureBoxSelf.Bottom < panel1.Height)
                    {
                        buildMoveData(data, 1, pictureBoxSelf.Left, pictureBoxSelf.Top+ distance);
                        if (await canWalk(data)) pictureBoxSelf.Top += distance; //下移
                    } 
                    break;
                case Keys.Space:

                    listBox1.Items.Add(pictureBoxSelf.Left + "   " + pictureBoxSelf.Top);
                    Bomb bomb = new Bomb(pictureBoxSelf.Left, pictureBoxSelf.Top, panel1);
                    bomb.Exploded += BombExploded;
                    listBox1.Items.Add(bomb.Pc.Location);
                    panel1.Controls.Add(bomb.Pc);
                    buildMoveData(data, 0, pictureBoxSelf.Left, pictureBoxSelf.Top);
                    break;
                default:
                    keyT = false;
                    break;
            }
            string json = JsonConvert.SerializeObject(data);
            //if (keyT) Send("J"+ json);
            buttonFocus.Select();
        }

        private void RemovePictureBox(int r, int c, Prop prop)
        {
            panel1.Controls.Remove(arrBox[r, c].Pc);
            if (prop == null) return;
            if (panel1.InvokeRequired)
            {
                panel1.Invoke((MethodInvoker)delegate
                {
                    panel1.Controls.Add(prop.Pc);
                });
            }
            else
            {
                panel1.Controls.Add(prop.Pc);
            }
        }

        private void CheckBom(int x, int y)
        {
            x = (x + 20) / 50;
            y = (y+20) / 50;
            int[] dir = new int[] { 0,1,0,-1,0};
            for(int d = 0; d < dir.Length-1; d++)
            {
                int r = y + dir[d], c = x + dir[d + 1];
                if (r < 0 || c < 0 || r >= N || c >= N) continue;
                if(arr[y+dir[d]][x+dir[d+1]] == 2)
                {
                    //listBox1.Items.Add(y.ToString() + x.ToString());
                    int type = arrBox[r,c].getProp();
                    arr[r][c] = 0;
                    Console.WriteLine("type" + type.ToString());
                    Prop prop = null;
                    if (type != -1) 
                    {
                        arr[r][c] = type;
                        prop = new Prop(c * baseL, r * baseL, type);
                        arrProp[r, c] = prop;
                    } 
                    
                    if (panel1.InvokeRequired)
                    {
                        // 如果不在 UI 线程上，使用 Invoke 来在 UI 线程上执行移除操作
                        panel1.Invoke(new Action(() => RemovePictureBox(r, c, prop)));
                    }
                    else
                    {
                        // 在 UI 线程上执行移除操作
                        panel1.Controls.Remove(arrBox[r, c].Pc);
                        if(prop != null) panel1.Controls.Add(prop.Pc);
                    }
                    
                }
            }
        }

        int V;
        private System.Threading.Timer timer;
        private object lockObject = new object();

        private void button1_Click(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false; //忽略跨執行緒操作的錯誤
            User = "A";                            //使用者名稱
            string IP = "127.0.0.1";                   //伺服器IP
            int Port = int.Parse("2019");             //伺服器Port
            try
            {
                IPEndPoint EP = new IPEndPoint(IPAddress.Parse(IP), Port);          //建立伺服器端點資訊
                //建立TCP通訊物件
                T = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                T.Connect(EP);           //連上Server的EP端點(類似撥號連線)
                Th = new Thread(Listen); //建立監聽執行緒
                Th.IsBackground = true;  //設定為背景執行緒
                Th.Start();              //開始監聽
                textBox1.Text = "A已連線伺服器！" + "\r\n";
                Send("0" + User);        //隨即傳送自己的 UserName 給 Server
            }
            catch
            {
                textBox1.Text = "無法連上伺服器！" + "\r\n";  //連線失敗時顯示訊息
            }

            InitTmp(1, "A", "p1.jpg");
        }

        private void reciprocal(int t, int type)
        {
            V = t;
            timer = new System.Threading.Timer(CountDown, type, 0, 100);
        }

        private void CountDown(object state)
        {
            lock (lockObject)
            {
                int type = (int)state;
                if (V == 0)
                {
                    timer.Change(Timeout.Infinite, Timeout.Infinite);
                    switch (type)
                    {
                        case 3:
                            runningSpeedRatio = 1;
                            break;
                        case 4:

                            break;
                        case 5:
                            walking = true;
                            break;
                        case 6:

                            break;
                    }
                }
                else if (V > 0)
                {
                    V -= 1;
                }
                
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false; //忽略跨執行緒操作的錯誤
            String User;
            User = "B";                            //使用者名稱
            string IP = "127.0.0.1";                   //伺服器IP
            int Port = int.Parse("2019");             //伺服器Port
            try
            {
                IPEndPoint EP = new IPEndPoint(IPAddress.Parse(IP), Port);          //建立伺服器端點資訊
                //建立TCP通訊物件
                T = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                T.Connect(EP);           //連上Server的EP端點(類似撥號連線)
                Th = new Thread(Listen); //建立監聽執行緒
                Th.IsBackground = true;  //設定為背景執行緒
                Th.Start();              //開始監聽
                textBox1.Text = "B已連線伺服器！" + "\r\n";
                Send("0" + User);        //隨即傳送自己的 UserName 給 Server
            }
            catch
            {
                textBox1.Text = "無法連上伺服器！" + "\r\n";  //連線失敗時顯示訊息
            }
            InitTmp(2, "B", "p2.png");
        }

        private void GameAction(DataGame data)
        {
            switch (data.Action)
            {
                case "MOVE":
                    //listBox1.Items.Add("MOVE:" + data);
                    PictureBox picturebox = userPictureName[data.UserNumber];
                    picturebox.Left = data.Position.Item1;
                    picturebox.Top = data.Position.Item2;
                    break;

            }
        }



        //監聽 Server 訊息 (Listening to the Server)
        private void Listen()
        {
            EndPoint ServerEP = (EndPoint)T.RemoteEndPoint; //Server 的 EndPoint
            byte[] B = new byte[1023];                      //接收用的 Byte 陣列
            int inLen = 0;                                  //接收的位元組數目
            string Msg;                                     //接收到的完整訊息
            string St;                                      //命令碼
            string Str;                                     //訊息內容(不含命令碼)
            while (true)                                    //無限次監聽迴圈
            {
                try
                {
                    inLen = T.ReceiveFrom(B, ref ServerEP); //收聽資訊並取得位元組數
                }
                catch (Exception)
                {
                    T.Close();                                 //關閉通訊器
                    MessageBox.Show("伺服器斷線了！");         //顯示斷線
                    button1.Enabled = true;                    //連線按鍵恢復可用
                    Th.Abort();                                //刪除執行緒
                }
                Msg = Encoding.Default.GetString(B, 0, inLen); //解讀完整訊息
                St = Msg.Substring(0, 1);                      //取出命令碼 (第一個字)
                Str = Msg.Substring(1);                        //取出命令碼之後的訊息
                //listBox1.Items.Add("Msg:" + Msg);
                switch (St)                                    //依命令碼執行功能
                {
                    case "J":
                        DataGame data = JsonConvert.DeserializeObject<DataGame>(Str);
                        //listBox1.Items.Add("MOVE:" + data);
                        GameAction(data);
                        break;
                    case "W":  //if canWalk
                        if (Str == "WALK") canWalkServer = true;
                        else canWalkServer = false;
                        canWalkFlag = true;
                        break;
                }
            }
        }


        private void Send(string Str)
        {
            byte[] B = Encoding.Default.GetBytes(Str); //翻譯文字成Byte陣列
            try
            {
                T.Send(B, 0, B.Length, SocketFlags.None);  //傳送訊息給伺服器
            }
            catch (Exception ex)
            {
                // Handle exception (log, display, etc.)
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
