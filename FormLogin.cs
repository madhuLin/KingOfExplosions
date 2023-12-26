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
using System.Reflection.Emit;

namespace KingOfExplosions
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        public int Port;
        public string IP;
        public string User;
        Socket T;
        Thread Th;
        IPEndPoint EP;
        private void btn_rules_Click(object sender, EventArgs e)
        {
            Form7 f2 = new Form7();  //邀請表單
            Control.CheckForIllegalCrossThreadCalls = false; //忽略跨執行緒操作的錯誤
            IP = tB_IP.Text;
            Port = int.Parse(tB_Port.Text);
            EP = new IPEndPoint(IPAddress.Parse(IP), Port); //伺服器的連線端點資訊
            T = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            User = tB_user.Text;
            if (User != "")            //如果沒有使用者名稱 
            {
                try
                {
                    T.Connect(EP);           //連上伺服器的端點EP(類似撥號給電話總機)
                    Th = new Thread(Listen); //建立監聽執行緒
                    Th.IsBackground = true;  //設定為背景執行緒
                    Th.Start();              //開始監聽
                    
                    f2.IP = this.IP;         //將資料傳去form2
                    f2.Port = this.Port;
                    f2.User = this.User;

                    if (this.InvokeRequired)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            this.Visible = false;
                            //this.Close();
                        });
                    }
                    else
                    {
                        this.Visible = false;
                        //this.Close();
                    }
                    if (f2.InvokeRequired)
                    {
                        f2.Invoke((MethodInvoker)delegate
                        {
                            f2.ShowDialog();
                        });
                    }
                    else
                    {
                        f2.ShowDialog();
                    }

                }
                catch (Exception)
                {
                    MessageBox.Show("無法連上伺服器！");
                    return;
                }


            }
            else
            {
                MessageBox.Show("沒有使用者！");
            }
        }

        private void Send(string Str)
        {
            byte[] B = Encoding.Default.GetBytes(Str); //翻譯文字成Byte陣列
            T.Send(B, 0, B.Length, SocketFlags.None);  //傳送訊息給伺服器
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
                switch (St)                                    //依命令碼執行功能
                {
                    case "3":  //t玩家名稱重複
                        tB_user.Text = "";
                        MessageBox.Show("玩家名稱重複！");
                        break;
                }
            }
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            if (label1.InvokeRequired)
            {
                label1.Invoke((MethodInvoker)delegate
                {
                    label1.Visible = true;
                });
            }
            else
            {
                label1.Visible = true;
            }
            if (label2.InvokeRequired)
            {
                label2.Invoke((MethodInvoker)delegate
                {
                    label2.Visible = true;
                });
            }
            else
            {
                label2.Visible = true;
            }
            if (label3.InvokeRequired)
            {
                label3.Invoke((MethodInvoker)delegate
                {
                    label3.Visible = true;
                });
            }
            else
            {
                label3.Visible = true;
            }

            if (tB_IP.InvokeRequired)
            {
                tB_IP.Invoke((MethodInvoker)delegate
                {
                    tB_IP.Visible = true;
                });
            }
            else
            {
                tB_IP.Visible = true;
            }
            if (tB_Port.InvokeRequired)
            {
                tB_Port.Invoke((MethodInvoker)delegate
                {
                    tB_Port.Visible = true;
                });
            }
            else
            {
                tB_Port.Visible = true;
            }
            if (tB_user.InvokeRequired)
            {
                tB_user.Invoke((MethodInvoker)delegate
                {
                    tB_user.Visible = true;
                });
            }
            else
            {
                tB_user.Visible = true;
            }
            if (pictureBox1.InvokeRequired)
            {
                pictureBox1.Invoke((MethodInvoker)delegate
                {
                    pictureBox1.Visible = true;
                });
            }
            else
            {
                pictureBox1.Visible = true;
            }
        }
    }
}
