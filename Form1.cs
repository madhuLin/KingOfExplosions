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
using System.Drawing.Printing;
namespace KingOfExplosions
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.KeyPreview = true; // 啟用按鍵事件的 Form 預覽
        }

        const int baseL = 50, N = 10;
        public int UserNumber;  //P?
        private string path = System.Environment.CurrentDirectory, terrain;
        double runningSpeedBase = 5, runningSpeedRatio = 1;  //基礎跑速
        int[,] arr = new int[N, N];
        Box[,] arrBox = new Box[N, N];  //紀錄箱子
        Prop[,] arrProp = new Prop[N, N];  //紀錄道具
        bool walking = true;
        Socket T;                                    //通訊物件
        Thread Th;                                   //網路監聽執行緒
        String User;
        DataUser dataUser;
        PictureBox pictureBoxSelf;  //本機玩家
        Dictionary<int, PictureBox> userPictureName = new Dictionary<int, PictureBox>();  //P? 對應角色
        Dictionary<int, Bomb> mapBomb = new Dictionary<int, Bomb>();  //紀錄炸彈編號 物件
        int power = 1; //炸彈威力

        public List<DataUser> ListDataUser = new List<DataUser>();  //套用class的list

        public List<DataUser> ListDataUserRank = new List<DataUser>();  //套用class的list
        private void InitTmp(int num, string user, string picname) 
        {
            GroupBox groupBox = this.Controls.Find("groupBox" + num.ToString(), false).FirstOrDefault() as GroupBox;
            string headPic = $"pictureBoxP{num.ToString()}Head";
            PictureBox Pc = groupBox.Controls.Find(headPic, false).FirstOrDefault() as PictureBox;
            Pc.SizeMode = PictureBoxSizeMode.StretchImage;
            Pc.Image = Image.FromFile(path + picname + ".jpg");
            PictureBox PcPlay = null;
            if (num == 1) PcPlay = pictureBoxP1;
            else if (num == 2) PcPlay = pictureBoxP2;
            else if (num == 3) PcPlay = pictureBoxP3;
            else if (num == 4) PcPlay = pictureBoxP4;
            PcPlay.Image = Image.FromFile(path + picname + ".png");
            PcPlay.SizeMode = PictureBoxSizeMode.StretchImage;
            panel1.Controls.Add(PcPlay);
            userPictureName.Add(num, PcPlay);
        }

        private void InitSelf(int num, string user, string picname)
        {
            //dataUser = new DataUser();
            //dataUser.UserNumber = num;
            //dataUser.User = user;
            //dataUser.PicName = picname + ".png";
            GroupBox groupBox = this.Controls.Find("groupBox" + num.ToString(), false).FirstOrDefault() as GroupBox;
            string headPic = $"pictureBoxP{num.ToString()}Head";
            PictureBox Pc = groupBox.Controls.Find(headPic, false).FirstOrDefault() as PictureBox;
            Pc.SizeMode = PictureBoxSizeMode.StretchImage;
            Pc.Image = Image.FromFile(path + picname + ".jpg");

            if (num == 1) pictureBoxSelf = pictureBoxP1;
            else if (num == 2) pictureBoxSelf = pictureBoxP2;
            else if (num == 3) pictureBoxSelf = pictureBoxP3;
            else if (num == 4) pictureBoxSelf = pictureBoxP4;
            pictureBoxSelf.Image = Image.FromFile(path + dataUser.PicName + ".png");
            pictureBoxSelf.SizeMode = PictureBoxSizeMode.StretchImage;
            panel1.Controls.Add(pictureBoxSelf);
            userPictureName.Add(num, pictureBoxSelf);
        }

        //初始化物件
        private void Init()
        {
            path = path.Substring(0, path.IndexOf("bin")) + "Img\\";

        }

        //建構地圖
        private void buildTerrain(string terrain)
        {
            using (StringReader stringReader = new StringReader(terrain))
            {
                // 逐行读取文本
                string line;
                int i = 0;
                while ((line = stringReader.ReadLine()) != null)
                {
                    for (int j = 0; j < line.Length; j++) arr[i, j] = line[j] - '0';
                    i++;
                }
            }

            for (int i = 0; i < N; i++)
            {

                for (int j = 0; j < N; j++)
                {
                    if (arr[i, j] == 1 || arr[i, j] == 2)  //增加障礙物
                    {
                        Obstacle obstacle = new Obstacle(j * baseL, i * baseL, arr[i, j]);
                        if (panel1.InvokeRequired)
                        {
                            panel1.Invoke((MethodInvoker)delegate
                            {
                                panel1.Controls.Add(obstacle.Pc);
                            });
                        }
                        else
                        {
                            panel1.Controls.Add(obstacle.Pc);
                        }
                    }
                    else if (arr[i, j] == 3)  //箱子
                    {
                        Box box = new Box(j * baseL, i * baseL, panel1);
                        if (panel1.InvokeRequired)
                        {
                            panel1.Invoke((MethodInvoker)delegate
                            {
                                panel1.Controls.Add(box.Pc);
                            });
                        }
                        else
                        {
                            panel1.Controls.Add(box.Pc);
                        }

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
            listBox1.Items.Add("UserNumber" + UserNumber);
            foreach (var data in ListDataUser)
            {
                if (data.UserNumber == this.UserNumber)
                {
                    dataUser = data;
                    InitSelf(data.UserNumber, data.User, data.PicName);
                    ConnectServer(data.UserNumber, data.User, data.Ip, data.Port, data.PicName);
                }
                else
                {
                    InitTmp(data.UserNumber, data.User, data.PicName);
                }
            }


        }


        //滑鼠事件監聽
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (!walking) return;
            int distance = (int)(runningSpeedBase * runningSpeedRatio);
            //if (play == false) return;
            DataGame data = null;
            bool keyT = false;
            switch (e.KeyCode)
            {
                case Keys.A:
                    if (pictureBoxSelf.Left >= 0)
                    {
                        data = new DataGame(dataUser.UserNumber, 1, pictureBoxSelf.Left - distance, pictureBoxSelf.Top);
                        //if (await canWalk(data)) pictureBoxSelf.Left -= distance; //左移
                        keyT = true;
                    }

                    break;
                case Keys.D:
                    if (pictureBoxSelf.Right < panel1.Width)
                    {
                        data = new DataGame(dataUser.UserNumber, 1, pictureBoxSelf.Left + distance, pictureBoxSelf.Top);
                        //if (await canWalk(data)) pictureBoxSelf.Left += distance; //右移
                        keyT = true;

                    }
                    break;
                case Keys.W:
                    if (pictureBoxSelf.Top >= 0)
                    {
                        data = new DataGame(dataUser.UserNumber, 1, pictureBoxSelf.Left, pictureBoxSelf.Top - distance);
                        //if (await canWalk(data)) pictureBoxSelf.Top -= distance; //上移
                        keyT = true;
                    }

                    break;
                case Keys.S:
                    if (pictureBoxSelf.Bottom < panel1.Height)
                    {
                        data = new DataGame(dataUser.UserNumber, 1, pictureBoxSelf.Left, pictureBoxSelf.Top + distance);
                        //if (await canWalk(data)) pictureBoxSelf.Top += distance; //下移
                        keyT = true;
                    }
                    break;
                case Keys.Space:
                    data = new DataGame(dataUser.UserNumber, 0, pictureBoxSelf.Left, pictureBoxSelf.Top);
                    keyT = true;
                    break;
                default:
                    keyT = false;
                    break;
            }
            string json = JsonConvert.SerializeObject(data);
            if (keyT) Send("J" + json);
            buttonFocus.Select();
        }

        //新增圖片 
        private void AddPictureBox(List<DataProp> list)
        {
            foreach (DataProp data in list)
            {
                int r = data.Position.Item1, c = data.Position.Item2;
                panel1.Controls.Remove(arrBox[r, c].Pc);
                //arrBox[r, c].Dispose();  //釋放資源
                if (data.type == 0) continue;
                Prop prop = new Prop(c * baseL, r * baseL, data.type);
                arrProp[r, c] = prop;
                //檢查線程
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

        }

        int V;
        private System.Threading.Timer timer;
        private object lockObject = new object();

        private string MyIP()
        {
            string hn = Dns.GetHostName();
            IPAddress[] ip = Dns.GetHostEntry(hn).AddressList;
            foreach (IPAddress ipAddr in ip)
            {
                if (ipAddr.AddressFamily == AddressFamily.InterNetwork)
                { return ipAddr.ToString(); }
            }
            return "";
        }

        private void ConnectServer(int userNumber, string User, string IP, int Port, string picName)
        {
            listBox1.Items.Add("ConnectServer");
            Control.CheckForIllegalCrossThreadCalls = false; //忽略跨執行緒操作的錯誤
            this.UserNumber = userNumber;
            this.User = User;                            //使用者名稱
            Random random = new Random();
            Thread.Sleep(random.Next(100));
            try
            {
                IPEndPoint EP = new IPEndPoint(IPAddress.Parse(IP), Port);          //建立伺服器端點資訊
                //建立TCP通訊物件
                T = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                T.Connect(EP);           //連上Server的EP端點(類似撥號連線)
                Th = new Thread(Listen); //建立監聽執行緒
                Th.IsBackground = true;  //設定為背景執行緒
                Th.Start();              //開始監聽
                //textBox1.Text = "A已連線伺服器！" + "\r\n";
                Send("0" + UserNumber);        //隨即傳送自己的 UserName 給 Server
                listBox1.Items.Add(userNumber + "已連線伺服器！");
            }
            catch
            {
                //textBox1.Text = "無法連上伺服器！" + "\r\n";  //連線失敗時顯示訊息
                listBox1.Items.Add(userNumber + "無法連上伺服器！！");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false; //忽略跨執行緒操作的錯誤
            User = "A";                            //使用者名稱
            UserNumber = 1;
            string IP = MyIP();                   //伺服器IP
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
                Send("0" + UserNumber);        //隨即傳送自己的 UserName 給 Server
            }
            catch
            {
                textBox1.Text = "無法連上伺服器！" + "\r\n";  //連線失敗時顯示訊息
            }

            InitTmp(1, "A", "crayon");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false; //忽略跨執行緒操作的錯誤
            String User;
            User = "B";                            //使用者名稱
            UserNumber = 2;
            string IP = MyIP();                   //伺服器IP
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
                Send("0" + UserNumber);        //隨即傳送自己的 UserName 給 Server
            }
            catch
            {
                textBox1.Text = "無法連上伺服器！" + "\r\n";  //連線失敗時顯示訊息
            }
            InitTmp(2, "2", "nini");
        }

        //DataGame json 行檔案行動
        private void GameAction(DataGame data)
        {
            GroupBox groupBox = this.Controls.Find("groupBox" + UserNumber.ToString(), false).FirstOrDefault() as GroupBox;
            PictureBox Pc;
            switch (data.Action)
            {
                case "MOVE": //移動
                    //listBox1.Items.Add("MOVE:" + data);
                    PictureBox picturebox = userPictureName[data.UserNumber];
                    picturebox.Left = data.Position.Item1;
                    picturebox.Top = data.Position.Item2;
                    break;
                case "DROP":  //放炸蛋
                    listBox1.Items.Add("data");
                    Bomb bomb = new Bomb(data.Position.Item1, data.Position.Item2, power);
                    if (panel1.InvokeRequired)
                    {
                        // 如果不在 UI 线程上，使用 Invoke 来在 UI 线程上执行增加操作
                        panel1.Invoke((MethodInvoker)delegate { panel1.Controls.Add(bomb.Pc); });
                        if (!mapBomb.ContainsKey(data.numberBomb)) mapBomb.Add(data.numberBomb, bomb);
                        else mapBomb[data.numberBomb] = bomb;

                    }
                    else
                    {
                        // 在 UI 线程上执行增加操作
                        panel1.Controls.Add(bomb.Pc);
                        if (!mapBomb.ContainsKey(data.numberBomb)) mapBomb.Add(data.numberBomb, bomb);
                        else mapBomb[data.numberBomb] = bomb;
                    }
                    panel1.Controls.Add(bomb.Pc);
                    break;
                case "PROP":  //撿到道具
                    panel1.Controls.Remove(arrProp[data.Position.Item1, data.Position.Item2].Pc);
                    if (data.UserNumber != UserNumber) return;
                    int type = data.TypeProp;
                    switch (type)
                    {
                        case 5:  //加速
                            runningSpeedRatio = 1.7;

                            string heart = $"pictureBoxP{UserNumber.ToString()}Shoe";
                            listBox1.Items.Add(heart);
                            Pc = groupBox.Controls.Find(heart, false).FirstOrDefault() as PictureBox;
                            if (panel1.InvokeRequired) panel1.Invoke((MethodInvoker)delegate { Pc.Visible = true; });
                            else Pc.Visible = true;

                            break;
                        case 6: //保護
                            string Protect = $"pictureBoxP{UserNumber.ToString()}Protect";
                            Pc = groupBox.Controls.Find(Protect, false).FirstOrDefault() as PictureBox;
                            if (panel1.InvokeRequired) panel1.Invoke((MethodInvoker)delegate { Pc.Visible = true; });
                            else Pc.Visible = true;
                            break;
                        case 7:  //不能動
                            walking = false;
                            break;
                        case 8: //加功
                            power = 2;
                            string attack = $"pictureBoxP{UserNumber.ToString()}Attack";
                            Pc = groupBox.Controls.Find(attack, false).FirstOrDefault() as PictureBox;
                            if (panel1.InvokeRequired) panel1.Invoke((MethodInvoker)delegate { Pc.Visible = true; });
                            else Pc.Visible = true;
                            break;
                        case 9:  //補血
                            string heartAdd = $"pictureBoxP{UserNumber.ToString()}Heart{data.Carry}";
                            Pc = groupBox.Controls.Find(heartAdd, false).FirstOrDefault() as PictureBox;
                            if (panel1.InvokeRequired) panel1.Invoke((MethodInvoker)delegate { Pc.Image = Image.FromFile(path + "love.png"); });
                            else Pc.Image = Image.FromFile(path + "love.png");

                            break;
                    }
                    break;
                case "PORPSOVER":  //道具結束
                    DataGame dataGame = data;
                    switch (dataGame.TypeProp)
                    {
                        case 5:  //停加速
                            runningSpeedRatio = 1;
                            groupBox = this.Controls.Find("groupBox" + UserNumber.ToString(), false).FirstOrDefault() as GroupBox;
                            string heart = $"pictureBoxP{UserNumber}Shoe";
                            Pc = FindControl(groupBox, heart) as PictureBox;
                            if (panel1.InvokeRequired) panel1.Invoke((MethodInvoker)delegate { Pc.Visible = false; });
                            else Pc.Visible = false;
                            break;
                        case 6:  //保護不扣血
                            string Protect = $"pictureBoxP{UserNumber.ToString()}Protect";
                            Pc = groupBox.Controls.Find(Protect, false).FirstOrDefault() as PictureBox;
                            if (panel1.InvokeRequired) panel1.Invoke((MethodInvoker)delegate { Pc.Visible = false; });
                            else Pc.Visible = false;
                            break;
                        case 7:  //能動
                            walking = true;
                            break;
                        case 8: //
                            power = 1;
                            string attack = $"pictureBoxP{UserNumber.ToString()}Attack";
                            Pc = groupBox.Controls.Find(attack, false).FirstOrDefault() as PictureBox;
                            if (panel1.InvokeRequired) panel1.Invoke((MethodInvoker)delegate { Pc.Visible = false; });
                            else Pc.Visible = false;
                            break;
                    }
                    break;

            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Send("9" + UserNumber.ToString());//傳送自己的離線訊息給伺服器
            //T.Close();       //關閉網路通訊器
            Application.DoEvents();
            //Application.Exit();
        }

        private void RmIamge(object sender, EventArgs e)
        {
            Tool tool = (Tool)sender;
            panel1.Controls.Remove(mapBomb[int.Parse(tool.str)].Pc);
            //mapBomb[int.Parse(tool.str)].Dispose();  //釋放資源
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
                string[] datas = Msg.Split('|');
                foreach (string tmpstr in datas)
                {
                    if (tmpstr == "") continue;
                    Msg = tmpstr;
                    //listBox1.Items.Add(Msg);
                    St = Msg.Substring(0, 1);                      //取出命令碼 (第一個字)
                    Str = Msg.Substring(1);                        //取出命令碼之後的訊息
                                                                   //listBox1.Items.Add("Msg:" + Msg);
                    switch (St)                                    //依命令碼執行功能
                    {
                        case "T": //初始化地形
                            listBox1.Items.Add(Str);
                            buildTerrain(Str);
                            break;
                        case "J":  //json檔案
                            try
                            {
                                DataGame data = JsonConvert.DeserializeObject<DataGame>(Str);
                                GameAction(data);
                            }
                            catch (Exception)
                            {

                            }
                            break;
                        case "D":  //炸彈爆炸
                            string[] tmp = Str.Split(' ');
                            //mapBomb[int.Parse(tmp[0])].Pc;
                            listBox1.Items.Add(Str);
                            List<DataProp> list = JsonConvert.DeserializeObject<List<DataProp>>(tmp[1]);
                            Bomb bomb = mapBomb[int.Parse(tmp[0])];
                            Tool tool = new Tool(tmp[0]);
                            tool.Exploded += RmIamge;
                            if (panel1.InvokeRequired)
                            {
                                //panel1.Controls.Remove(mapBomb[int.Parse(tmp[0])].Pc);
                                bomb.setIamge();
                                tool.reciprocal(5);

                                // 如果不在 UI 线程上，使用 Invoke 来在 UI 线程上执行移除操作
                                panel1.Invoke(new Action(() => AddPictureBox(list)));
                            }
                            else
                            {
                                //panel1.Controls.Remove(mapBomb[int.Parse(tmp[0])].Pc);
                                // 在 UI 线程上执行移除操作
                                AddPictureBox(list);
                            }
                            break;
                        case "H":  //扣血
                            Attack attack = JsonConvert.DeserializeObject<Attack>(Str);

                            GroupBox groupBox = this.Controls.Find("groupBox" + attack.UserNumber.ToString(), false).FirstOrDefault() as GroupBox;
                            string heart = $"pictureBoxP{attack.UserNumber}Heart{attack.Heart}";
                            PictureBox Pc = FindControl(groupBox, heart) as PictureBox;
                            Pc.Image = Image.FromFile(path + "lovenull.png");
                            break;
                        case "O":  //死亡
                            walking = false;
                            pictureBoxSelf.Visible = false;
                            if (panel1.InvokeRequired)
                            {
                                panel1.Invoke((MethodInvoker)delegate
                                {
                                    PictureBox pictureBox = new PictureBox();
                                    pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                                    pictureBox.SetBounds(0, 0, 500, 500);
                                    pictureBox.Image = Image.FromFile(path + "gameover.png");
                                    panel1.Controls.Add(pictureBox);
                                    // 將 PictureBox 移到 Z 軸的最上層
                                    pictureBox.BringToFront();
                                });
                            }
                            else
                            {

                            }
                            break;
                        case "K":  //結束遊戲
                            listBox1.Items.Add("K" + Str);
                            string[] dies = Str.Split(' ');
                            dies.Reverse();
                            foreach (var die in dies)
                            {
                                foreach (var data in ListDataUser)
                                {
                                    if (data.UserNumber == int.Parse(die))
                                    {
                                        ListDataUserRank.Add(data);
                                    }
                                }
                            }

                            if (this.InvokeRequired)
                            {
                                this.Invoke((MethodInvoker)delegate
                                {
                                    Send("9" + UserNumber.ToString());//傳送自己的離線訊息給伺服器
                                    T.Close();
                                    this.Visible = false;
                                    this.Close();
                                });
                            }
                            else
                            {
                                Send("9" + UserNumber.ToString());//傳送自己的離線訊息給伺服器
                                T.Close();
                                this.Visible = false;
                                this.Close();
                            }
                            Form2 f2 = new Form2();
                            if (f2.InvokeRequired)
                            {
                                f2.Invoke((MethodInvoker)delegate
                                {
                                    f2.ListDataUserRank = this.ListDataUserRank;
                                    f2.ShowDialog();
                                });
                            }
                            else
                            {
                                f2.ListDataUserRank = this.ListDataUserRank;
                                f2.ShowDialog();
                            }

                            break;

                    }
                }
            }
        }
        private void HelpServer()
        {
            try
            {
                IPEndPoint EP = new IPEndPoint(IPAddress.Parse(dataUser.Ip), dataUser.Port);          //建立伺服器端點資訊
                //建立TCP通訊物件
                T = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                T.Connect(EP);           //連上Server的EP端點(類似撥號連線)
                Th = new Thread(Listen); //建立監聽執行緒
                Th.IsBackground = true;  //設定為背景執行緒
                Th.Start();              //開始監聽
                //textBox1.Text = "A已連線伺服器！" + "\r\n";
                Send("0" + UserNumber);        //隨即傳送自己的 UserName 給 Server
                listBox1.Items.Add(UserNumber+ "已連線伺服器！");
            }
            catch
            {
                //textBox1.Text = "無法連上伺服器！" + "\r\n";  //連線失敗時顯示訊息
                listBox1.Items.Add(UserNumber + "無法連上伺服器！！");
            }
        }

        private void buttonHelp_Click(object sender, EventArgs e)
        {
            HelpServer();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false; //忽略跨執行緒操作的錯誤
            String User;
            User = "C";                            //使用者名稱
            UserNumber = 3;
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
                textBox1.Text = "C已連線伺服器！" + "\r\n";
                Send("0" + UserNumber);        //隨即傳送自己的 UserName 給 Server
            }
            catch
            {
                textBox1.Text = "無法連上伺服器！" + "\r\n";  //連線失敗時顯示訊息
            }
            InitTmp(UserNumber, "3", "p2.png");
        }

        private void Send(string Str)
        {
            byte[] B = Encoding.Default.GetBytes(Str + "|"); //翻譯文字成Byte陣列
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
        static Control FindControl(Control container, string name)
        {
            // 使用 LINQ 查询在 Controls 集合中查找匹配的控件
            return container.Controls.Find(name, true).FirstOrDefault();
        }
    }
}
