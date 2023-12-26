using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace KingOfExplosions
{
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
            

        }

        //public int Port;
        //public string IP;
        public string User;
        //public DataUser dataUser = new DataUser();
        Socket T;
        Thread Th;
        IPEndPoint EP;
        public string player = "";    //儲存參與玩家名，並以"_"區隔
        public string[] players; //儲存玩家名
        public List<string> listpy = new List<string>();
        bool noinvite = true;    //確認是否邀請過
        bool namesingle = true;  //玩家名是否重複
        int invitenum;    //邀請人數
        List<string> pcchoose = new List<string>();        //已被選擇的圖片
        PictureBox Pc = new PictureBox();                //玩家選擇的圖片
        public DataUser dataUser = new DataUser();  //套用class
        bool character = false;       //沒有重複腳色
        bool characterc = false;       //沒有重複腳色
        List<DataUser> datauserlist = new List<DataUser>();  ////套用class的list
        int UserNumber;

        List<string> chready = new List<string>();        //
        private void Form7_Load(object sender, EventArgs e)
        {
            groupBox2.Visible = false;
            Control.CheckForIllegalCrossThreadCalls = false; //忽略跨執行緒操作的錯誤
            EP = new IPEndPoint(IPAddress.Parse(dataUser.Ip), dataUser.Port); //伺服器的連線端點資訊
            T = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            T.Connect(EP);           //連上伺服器的端點EP(類似撥號給電話總機)
            Th = new Thread(Listen); //建立監聽執行緒
            Th.IsBackground = true;  //設定為背景執行緒
            Th.Start();              //開始監聽

            //Thread myThread = new Thread(myRun);
            //myThread.Start();
            User = dataUser.User;
            Send("0" + dataUser.User);        //連線後隨即傳送自己的名稱給伺服器   

            lbl_port.Text = "你的Port : " + dataUser.Port;   //顯示玩家資料
            lbl_ip.Text = "你的IP : " + dataUser.Ip;
            lbl_user.Text = "使用者 :      " + User;
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            //e.Cancel = true;
        }
        private void btn_invite_Click(object sender, EventArgs e)
        {
            invitenum = int.Parse(cb_invitenum.Text);
            if (lB_onname.SelectedItems.Count < invitenum)//
            {
                foreach (var selectedItem in lB_onname.SelectedItems)
                {
                    noinvite = true;
                    foreach (string item in listpy)
                    {
                        if (item == User)
                        {

                        }
                        else if (selectedItem.ToString() == item)
                        {
                            MessageBox.Show(item + "已邀請過");
                            noinvite = false;
                        }
                    }
                    if (selectedItem.ToString() == User)
                    {
                        MessageBox.Show("不可邀請自己");
                        noinvite = false;

                    }

                    while (noinvite)
                    {
                        MessageBox.Show("已向" + selectedItem + "送出邀請訊息");
                        Send("I" + User + "|" + selectedItem);
                        break;
                    }
                }

            }
            else if (lB_onname.SelectedItems.Count == 0)
            {
                MessageBox.Show("你沒有選取任何玩家");

            }
            else
            {
                MessageBox.Show("最多選取" + (invitenum - 1).ToString() + "名玩家");
            }
        }

        private void Pc_Click(object sender, EventArgs e)
        {
            Pc = (PictureBox)sender;              //被選到的牌
            foreach (string a in pcchoose)
            {
                if (Pc.Tag.ToString() == a)
                {
                    character = true;
                }
            }

            if (characterc)
            {
                MessageBox.Show("你的腳色已選擇", "角色");
            }
            else if (character)
            {
                MessageBox.Show("你選擇的腳色已被其他人選擇", "角色重複");
                character = false;
            }
            else
            {
                if (MessageBox.Show("你選擇的腳色是" + Pc.Tag.ToString(), "角色確認", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    characterc = true;
                    for (int i = 0; i < listpy.Count; i++)
                    {
                        Thread.Sleep(200);
                        Send("B" + User + "," + Pc.Tag.ToString() + "|" + listpy[i]);
                        //if (listpy[i] == User)
                        //{
                        //    //DataUser dataUser = new DataUser() { UserNumber = i, User = User, PicName = Pc.Tag.ToString(), Ip = IP, Port = (Port - 1).ToString() };
                        //    //datauserlist.Add(dataUser);
                        //    Send("X" + User + "," + Pc.Tag.ToString() + "," + (i + 1).ToString() + "|" + listpy[i]);
                        //    //Send("X" + JsonConvert.SerializeObject(dataUser));
                        //}
                        //else
                        //{
                        //    Send("X" + User + "," + Pc.Tag.ToString() + "|" + listpy[i]);
                        //}

                    }
                }
                else
                {
                    MessageBox.Show("請重選角色");
                }
            }
        }

        private void Send(string Str)
        {
            byte[] B = Encoding.Default.GetBytes(Str);//翻譯字串Str為Byte陣列B
            T.Send(B, 0, B.Length, SocketFlags.None); //使用連線物件傳送資料
            listBox1.Items.Add(Str);
        }

        private void Listen()
        {
            EndPoint ServerEP = (EndPoint)T.RemoteEndPoint;       //Server 的 EndPoint
            byte[] B = new byte[1023];                            //接收用的 Byte 陣列
            int inLen = 0;                                        //接收的位元組數目
            string Msg;                                           //接收到的完整訊息
            string St;                                            //命令碼
            string Str;                                           //訊息內容(不含命令碼)
            while (true)                                          //無限次監聽迴圈
            {
                try
                {
                    inLen = T.ReceiveFrom(B, ref ServerEP);       //收聽資訊並取得位元組數

                }
                catch (Exception)                                 //產生錯誤時
                {
                    T.Close();                                    //關閉通訊器
                    MessageBox.Show("伺服器斷線了！");            //顯示斷線
                    Th.Abort();                                   //刪除執行緒
                }
                Msg = Encoding.Default.GetString(B, 0, inLen);    //解讀完整訊息
                St = Msg.Substring(0, 1);                         //取出命令碼 (第一個字)
                Str = Msg.Substring(1);                           //取出命令碼之後的訊息
                switch (St)                                       //依命令碼執行功能
                {
                    case "L":   //接收線上名單
                        lB_onname.Items.Clear();                   //清除名單
                        string[] M = Str.Split(',');              //拆解名單成陣列
                        for (int i = 0; i < M.Length; i++)
                        {
                            lB_onname.Items.Add(M[i]);             //逐一加入名單
                        }
                        break;
                    case "I":  //送出邀請
                        string[] C = Str.Split('|');
                        if (MessageBox.Show(C[0] + "邀請您玩爆爆王，是否接受?", "邀請訊息", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            Send("A" + User + "|" + C[0]);  //對手同意邀請
                            charactershowwait();
                        }
                        else
                        {
                            Send("U" + User + "|" + C[0]);  //對手不同意邀請
                        }
                        break;
                    case "A":   //對手同意邀請
                        string[] ainvite = Str.Split('|');
                        MessageBox.Show(ainvite[0] + "同意您的邀請");
                        listpy.Add(ainvite[0]);

                        if (listpy.Count == invitenum - 1)  //如果邀請玩家同意數等於邀請數，跳至Form4
                        {
                            listpy.Add(User);
                            for (int i = 0; i < listpy.Count - 1; i++)
                            {
                                player += listpy[i];
                                player += "_";
                            }
                            player += User;
                            for (int i = 0; i < listpy.Count; i++)
                            {
                                Thread.Sleep(100);
                                Send("F" + User + "," + player + "," + invitenum + "|" + listpy[i]);
                            }
                        }
                        break;
                    case "F":  //換成選角畫面
                        string[] p = Str.Split('|');
                        string[] pp = p[0].Split(',');
                        listpy = pp[1].Split('_').ToList();
                        invitenum = int.Parse(pp[2]);
                        charactershow(listpy);
                        break;
                    case "U":   //對手不同意邀請
                        string[] binvite = Str.Split('|');
                        MessageBox.Show(binvite[0] + "不同意您的邀請");
                        break;
                    case "T":
                        countdown();
                        break;
                    case "B":   //更新選角狀況
                        string[] A = Str.Split('|');
                        string[] CC = A[0].Split(',');
                        //DataUser dataUser = JsonConvert.DeserializeObject<DataUser>(Str);
                        pcchoose.Add(CC[1]);
                        int playerID = 1;
                        foreach (string s in listpy)
                        {
                            if (s == CC[0])
                            {
                                break;
                            }
                            else
                            {
                                playerID++;
                            }
                        }
                        DataUser dataUserTmp = new DataUser() { UserNumber = playerID, User = CC[0], PicName = CC[1], Ip = dataUser.Ip, Port = (dataUser.Port - 1) };
                        datauserlist.Add(dataUserTmp);
                        break;
                    case "3":  //t玩家名稱重複
                        MessageBox.Show("玩家名稱重複！");
                        doublename(); 
                        break;
                    case "Y":
                        string[] y = Str.Split('|');
                        string[] yy = y[0].Split(',');
                        chready.Add(yy[0]);
                        break;

                }
            }
        }

        private void doublename()
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    this.Visible = false;
                });
            }
            else
            {
                this.Visible = false;
            }
            FormLogin fg = new FormLogin();
            if (fg.InvokeRequired)
            {
                fg.Invoke((MethodInvoker)delegate
                {
                    fg.ShowDialog();
                });
            }
            else
            {
                fg.ShowDialog();
            }

        }

        private void charactershowwait()
        {
            if (groupBox1.InvokeRequired)
            {
                groupBox1.Invoke((MethodInvoker)delegate
                {
                    groupBox1.Visible = false;
                });
            }
            else
            {
                groupBox1.Visible = false;
            }


            if (label9.InvokeRequired)
            {
                label9.Invoke((MethodInvoker)delegate
                {
                    label9.Visible = true;
                });
            }
            else
            {
                label9.Visible = true;
            }

            if (label10.InvokeRequired)
            {
                label10.Invoke((MethodInvoker)delegate
                {
                    label9.Visible = true;
                });
            }
            else
            {
                label10.Visible = true;
            }
        }
        private void charactershow(List<string> n)
        {
            if (groupBox1.InvokeRequired)
            {
                groupBox1.Invoke((MethodInvoker)delegate
                {
                    groupBox1.Visible = false;
                    groupBox1.Location = new Point(777, 27);
                });
            }
            else
            {
                groupBox1.Visible = false;
                groupBox1.Location = new Point(777, 27);
            }

            if (groupBox2.InvokeRequired)
            {
                groupBox2.Invoke((MethodInvoker)delegate
                {
                    groupBox2.Visible = true;
                    groupBox2.Location = new Point(13,9);
                });
            }
            else
            {
                groupBox2.Visible = true;
                groupBox2.Location = new Point(13, 9);
            }

            if (label9.InvokeRequired)
            {
                label9.Invoke((MethodInvoker)delegate
                {
                    label9.Visible = false;
                });
            }
            else
            {
                label9.Visible = false;
            }

            if (label10.InvokeRequired)
            {
                label10.Invoke((MethodInvoker)delegate
                {
                    label9.Visible = false;
                });
            }
            else
            {
                label10.Visible = false;
            }
            if (lB_gamer.InvokeRequired)
            {
                lB_gamer.Invoke((MethodInvoker)delegate
                {
                    for (int i = 0; i < n.Count; i++)
                    {
                        lB_gamer.Items.Add("P" + (i + 1).ToString());
                        lB_gamer.Items.Add(n[i]);
                    }
                });
            }
            else
            {
                for (int i = 0; i < n.Count; i++)
                {
                    lB_gamer.Items.Add("P" + (i + 1).ToString());
                    lB_gamer.Items.Add(n[i]);
                }
            }
        }

        private void countdown()
        {
            if (lbl_countdown.InvokeRequired)
            {
                lbl_countdown.Invoke((MethodInvoker)delegate
                {
                    lbl_countdown.Visible = true;
                });
            }
            else
            {
                lbl_countdown.Visible = true;

            }

            if (lbl_wait.InvokeRequired)
            {
                lbl_wait.Invoke((MethodInvoker)delegate
                {
                    lbl_wait.Visible = false;
                });
            }
            else
            {
                lbl_wait.Visible = false;
            }
            Thread.Sleep(3000);
            gamestart();
            //timer1.Start();
        }

        private void gp_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(this.BackColor);
        }

        private void btn_ready_Click(object sender, EventArgs e)
        {
            if (characterc == false)
            {
                MessageBox.Show("你尚未選擇角色", "角色");
            }
            else
            {
                int playerID = 1;
                foreach (string s in listpy)
                {
                    if (s == User)
                    {
                        break;
                    }
                    else
                    {
                        playerID++;
                    }
                }
                UserNumber = playerID;
                btn_ready.Visible = false;
                lbl_wait.Visible = true;
                //for (int i = 0; i < listpy.Count; i++)
                //{
                //    Thread.Sleep(100);
                //    Send("Y" + User + "," + "1" + "|" + listpy[i]);
                //}
                //if (chready.Count == invitenum)
                //{
                //    if (pcchoose.Count == invitenum)
                //    {
                //        for (int i = 0; i < listpy.Count; i++)
                //        {
                //            Thread.Sleep(100);
                //            Send("T" + User + "|" + listpy[i]);
                //        }
                //    }

                //}
                if (datauserlist.Count == invitenum)
                {
                    for (int i = 0; i < listpy.Count; i++)
                    {
                        Thread.Sleep(100);
                        Send("T" + User + "|" + listpy[i]);
                    }
                }
                

            }

        }

        int gamedown = 5;
        private void timer1_Tick(object sender, EventArgs e)
        {
            lbl_countdown.Text = "遊戲倒數" + gamedown;
            gamedown--;
            if (gamedown == 0)
            {
                timer1.Stop();
                gamestart();
            }
        }

        private void gamestart()
        {
            Form1 f1 = new Form1();
            //string jsonData = JsonConvert.SerializeObject(datauserlist);
            f1.UserNumber = this.UserNumber;
            f1.ListDataUser = datauserlist;
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    this.Visible = false;
                    this.Close();
                });
            }
            else
            {
                this.Visible = false;
                this.Close();
            }

            if (f1.InvokeRequired)
            {
                f1.Invoke((MethodInvoker)delegate
                {
                    f1.ShowDialog();
                });
            }
            else
            {
                f1.ShowDialog();
            }
        }

        private void btn_logout_Click(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    this.Visible = false;
                });
            }
            else
            {
                this.Visible = false;
            }
            FormLogin fg = new FormLogin();
            if (fg.InvokeRequired)
            {
                fg.Invoke((MethodInvoker)delegate
                {
                    fg.ShowDialog();
                });
            }
            else
            {
                fg.ShowDialog();
            }
            Send("9" + User);
        }
    }
}