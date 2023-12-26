namespace KingOfExplosions
{
    partial class Form7
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbl_countdown = new System.Windows.Forms.Label();
            this.btn_ready = new System.Windows.Forms.Button();
            this.lbl_wait = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.pc4 = new System.Windows.Forms.PictureBox();
            this.pc3 = new System.Windows.Forms.PictureBox();
            this.pc2 = new System.Windows.Forms.PictureBox();
            this.pc1 = new System.Windows.Forms.PictureBox();
            this.label8 = new System.Windows.Forms.Label();
            this.lB_gamer = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cb_invitenum = new System.Windows.Forms.ComboBox();
            this.lbl_port = new System.Windows.Forms.Label();
            this.lbl_user = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lbl_ip = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_invite = new System.Windows.Forms.Button();
            this.lB_onname = new System.Windows.Forms.ListBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btn_logout = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pc4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pc3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pc2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pc1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.BackColor = System.Drawing.Color.OldLace;
            this.listBox1.Font = new System.Drawing.Font("Microsoft JhengHei", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 38;
            this.listBox1.Location = new System.Drawing.Point(492, 615);
            this.listBox1.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            this.listBox1.Name = "listBox1";
            this.listBox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listBox1.Size = new System.Drawing.Size(764, 270);
            this.listBox1.TabIndex = 42;
            this.listBox1.Tag = "f2";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(239)))), ((int)(((byte)(219)))));
            this.label10.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label10.Location = new System.Drawing.Point(274, 653);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(145, 40);
            this.label10.TabIndex = 46;
            this.label10.Text = "等待其他";
            this.label10.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(239)))), ((int)(((byte)(219)))));
            this.label9.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label9.Location = new System.Drawing.Point(274, 723);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(209, 40);
            this.label9.TabIndex = 45;
            this.label9.Text = "等待其他玩家";
            this.label9.Visible = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lbl_countdown);
            this.groupBox2.Controls.Add(this.btn_ready);
            this.groupBox2.Controls.Add(this.lbl_wait);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.pc4);
            this.groupBox2.Controls.Add(this.pc3);
            this.groupBox2.Controls.Add(this.pc2);
            this.groupBox2.Controls.Add(this.pc1);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.lB_gamer);
            this.groupBox2.Location = new System.Drawing.Point(746, 35);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox2.Size = new System.Drawing.Size(636, 540);
            this.groupBox2.TabIndex = 44;
            this.groupBox2.TabStop = false;
            this.groupBox2.Paint += new System.Windows.Forms.PaintEventHandler(this.gp_Paint);
            // 
            // lbl_countdown
            // 
            this.lbl_countdown.AutoSize = true;
            this.lbl_countdown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(239)))), ((int)(((byte)(219)))));
            this.lbl_countdown.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbl_countdown.Location = new System.Drawing.Point(36, 425);
            this.lbl_countdown.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_countdown.Name = "lbl_countdown";
            this.lbl_countdown.Size = new System.Drawing.Size(145, 40);
            this.lbl_countdown.TabIndex = 38;
            this.lbl_countdown.Text = "遊戲倒數";
            this.lbl_countdown.Visible = false;
            // 
            // btn_ready
            // 
            this.btn_ready.BackColor = System.Drawing.Color.OldLace;
            this.btn_ready.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btn_ready.ForeColor = System.Drawing.SystemColors.Desktop;
            this.btn_ready.Location = new System.Drawing.Point(244, 475);
            this.btn_ready.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            this.btn_ready.Name = "btn_ready";
            this.btn_ready.Size = new System.Drawing.Size(152, 53);
            this.btn_ready.TabIndex = 31;
            this.btn_ready.Text = "準備";
            this.btn_ready.UseVisualStyleBackColor = false;
            this.btn_ready.Click += new System.EventHandler(this.btn_ready_Click);
            // 
            // lbl_wait
            // 
            this.lbl_wait.AutoSize = true;
            this.lbl_wait.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(239)))), ((int)(((byte)(219)))));
            this.lbl_wait.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbl_wait.Location = new System.Drawing.Point(24, 492);
            this.lbl_wait.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_wait.Name = "lbl_wait";
            this.lbl_wait.Size = new System.Drawing.Size(209, 40);
            this.lbl_wait.TabIndex = 37;
            this.lbl_wait.Text = "等待其他玩家";
            this.lbl_wait.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(239)))), ((int)(((byte)(219)))));
            this.label1.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(512, 393);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 33);
            this.label1.TabIndex = 36;
            this.label1.Text = "角色4";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(239)))), ((int)(((byte)(219)))));
            this.label6.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label6.Location = new System.Drawing.Point(332, 393);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 33);
            this.label6.TabIndex = 35;
            this.label6.Text = "角色3";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(239)))), ((int)(((byte)(219)))));
            this.label3.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(512, 200);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 33);
            this.label3.TabIndex = 34;
            this.label3.Text = "角色2";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(239)))), ((int)(((byte)(219)))));
            this.label2.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(332, 200);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 33);
            this.label2.TabIndex = 33;
            this.label2.Text = "角色1";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(239)))), ((int)(((byte)(219)))));
            this.label7.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label7.Location = new System.Drawing.Point(308, 10);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(241, 40);
            this.label7.TabIndex = 32;
            this.label7.Text = "請選擇一種角色";
            // 
            // pc4
            // 
            this.pc4.Image = global::KingOfExplosions.Properties.Resources.Masao;
            this.pc4.Location = new System.Drawing.Point(496, 263);
            this.pc4.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            this.pc4.Name = "pc4";
            this.pc4.Size = new System.Drawing.Size(120, 125);
            this.pc4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pc4.TabIndex = 30;
            this.pc4.TabStop = false;
            this.pc4.Tag = "Masao";
            this.pc4.Click += new System.EventHandler(this.Pc_Click);
            // 
            // pc3
            // 
            this.pc3.Image = global::KingOfExplosions.Properties.Resources.nini;
            this.pc3.Location = new System.Drawing.Point(316, 263);
            this.pc3.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            this.pc3.Name = "pc3";
            this.pc3.Size = new System.Drawing.Size(120, 125);
            this.pc3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pc3.TabIndex = 29;
            this.pc3.TabStop = false;
            this.pc3.Tag = "nini";
            this.pc3.Click += new System.EventHandler(this.Pc_Click);
            // 
            // pc2
            // 
            this.pc2.Image = global::KingOfExplosions.Properties.Resources.wind;
            this.pc2.Location = new System.Drawing.Point(496, 68);
            this.pc2.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            this.pc2.Name = "pc2";
            this.pc2.Size = new System.Drawing.Size(120, 125);
            this.pc2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pc2.TabIndex = 28;
            this.pc2.TabStop = false;
            this.pc2.Tag = "wind";
            this.pc2.Click += new System.EventHandler(this.Pc_Click);
            // 
            // pc1
            // 
            this.pc1.Image = global::KingOfExplosions.Properties.Resources.crayon;
            this.pc1.Location = new System.Drawing.Point(316, 68);
            this.pc1.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            this.pc1.Name = "pc1";
            this.pc1.Size = new System.Drawing.Size(120, 125);
            this.pc1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pc1.TabIndex = 27;
            this.pc1.TabStop = false;
            this.pc1.Tag = "crayon";
            this.pc1.Click += new System.EventHandler(this.Pc_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(239)))), ((int)(((byte)(219)))));
            this.label8.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label8.Location = new System.Drawing.Point(4, 0);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(172, 48);
            this.label8.TabIndex = 26;
            this.label8.Text = "參與玩家";
            // 
            // lB_gamer
            // 
            this.lB_gamer.BackColor = System.Drawing.Color.OldLace;
            this.lB_gamer.Font = new System.Drawing.Font("Microsoft JhengHei", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lB_gamer.FormattingEnabled = true;
            this.lB_gamer.ItemHeight = 38;
            this.lB_gamer.Location = new System.Drawing.Point(12, 68);
            this.lB_gamer.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            this.lB_gamer.Name = "lB_gamer";
            this.lB_gamer.Size = new System.Drawing.Size(208, 270);
            this.lB_gamer.TabIndex = 25;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_logout);
            this.groupBox1.Controls.Add(this.cb_invitenum);
            this.groupBox1.Controls.Add(this.lbl_port);
            this.groupBox1.Controls.Add(this.lbl_user);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.lbl_ip);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.btn_invite);
            this.groupBox1.Controls.Add(this.lB_onname);
            this.groupBox1.Location = new System.Drawing.Point(32, 17);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Size = new System.Drawing.Size(706, 548);
            this.groupBox1.TabIndex = 43;
            this.groupBox1.TabStop = false;
            this.groupBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.gp_Paint);
            // 
            // cb_invitenum
            // 
            this.cb_invitenum.BackColor = System.Drawing.Color.OldLace;
            this.cb_invitenum.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cb_invitenum.FormattingEnabled = true;
            this.cb_invitenum.Items.AddRange(new object[] {
            "2",
            "3",
            "4"});
            this.cb_invitenum.Location = new System.Drawing.Point(508, 265);
            this.cb_invitenum.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            this.cb_invitenum.Name = "cb_invitenum";
            this.cb_invitenum.Size = new System.Drawing.Size(72, 41);
            this.cb_invitenum.TabIndex = 22;
            this.cb_invitenum.Tag = "f2";
            this.cb_invitenum.Text = "2";
            // 
            // lbl_port
            // 
            this.lbl_port.BackColor = System.Drawing.Color.OldLace;
            this.lbl_port.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbl_port.Location = new System.Drawing.Point(276, 85);
            this.lbl_port.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_port.Name = "lbl_port";
            this.lbl_port.Size = new System.Drawing.Size(422, 50);
            this.lbl_port.TabIndex = 18;
            this.lbl_port.Tag = "f2";
            this.lbl_port.Text = "你的Port : ";
            // 
            // lbl_user
            // 
            this.lbl_user.BackColor = System.Drawing.Color.OldLace;
            this.lbl_user.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbl_user.Location = new System.Drawing.Point(276, 153);
            this.lbl_user.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_user.Name = "lbl_user";
            this.lbl_user.Size = new System.Drawing.Size(422, 50);
            this.lbl_user.TabIndex = 19;
            this.lbl_user.Tag = "f2";
            this.lbl_user.Text = "使用者 : ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(239)))), ((int)(((byte)(219)))));
            this.label5.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label5.Location = new System.Drawing.Point(324, 265);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(172, 48);
            this.label5.TabIndex = 21;
            this.label5.Tag = "f2";
            this.label5.Text = "遊玩人數";
            // 
            // lbl_ip
            // 
            this.lbl_ip.BackColor = System.Drawing.Color.OldLace;
            this.lbl_ip.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbl_ip.Location = new System.Drawing.Point(268, 18);
            this.lbl_ip.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_ip.Name = "lbl_ip";
            this.lbl_ip.Size = new System.Drawing.Size(430, 50);
            this.lbl_ip.TabIndex = 17;
            this.lbl_ip.Tag = "f2";
            this.lbl_ip.Text = "你的IP :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(239)))), ((int)(((byte)(219)))));
            this.label4.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label4.Location = new System.Drawing.Point(8, 18);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(172, 48);
            this.label4.TabIndex = 20;
            this.label4.Tag = "f2";
            this.label4.Text = "上線玩家";
            // 
            // btn_invite
            // 
            this.btn_invite.BackColor = System.Drawing.Color.OldLace;
            this.btn_invite.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btn_invite.ForeColor = System.Drawing.SystemColors.Desktop;
            this.btn_invite.Location = new System.Drawing.Point(284, 353);
            this.btn_invite.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            this.btn_invite.Name = "btn_invite";
            this.btn_invite.Size = new System.Drawing.Size(124, 53);
            this.btn_invite.TabIndex = 16;
            this.btn_invite.Tag = "f2";
            this.btn_invite.Text = "邀請";
            this.btn_invite.UseVisualStyleBackColor = false;
            this.btn_invite.Click += new System.EventHandler(this.btn_invite_Click);
            // 
            // lB_onname
            // 
            this.lB_onname.BackColor = System.Drawing.Color.OldLace;
            this.lB_onname.Font = new System.Drawing.Font("Microsoft JhengHei", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lB_onname.FormattingEnabled = true;
            this.lB_onname.ItemHeight = 38;
            this.lB_onname.Location = new System.Drawing.Point(16, 85);
            this.lB_onname.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            this.lB_onname.Name = "lB_onname";
            this.lB_onname.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lB_onname.Size = new System.Drawing.Size(216, 270);
            this.lB_onname.TabIndex = 15;
            this.lB_onname.Tag = "f2";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btn_logout
            // 
            this.btn_logout.BackColor = System.Drawing.Color.OldLace;
            this.btn_logout.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btn_logout.ForeColor = System.Drawing.SystemColors.Desktop;
            this.btn_logout.Location = new System.Drawing.Point(475, 353);
            this.btn_logout.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            this.btn_logout.Name = "btn_logout";
            this.btn_logout.Size = new System.Drawing.Size(124, 53);
            this.btn_logout.TabIndex = 23;
            this.btn_logout.Tag = "f2";
            this.btn_logout.Text = "登出";
            this.btn_logout.UseVisualStyleBackColor = false;
            this.btn_logout.Click += new System.EventHandler(this.btn_logout_Click);
            // 
            // Form7
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(220)))));
            this.ClientSize = new System.Drawing.Size(1600, 968);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.Name = "Form7";
            this.Text = "Form7";
            this.Load += new System.EventHandler(this.Form7_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pc4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pc3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pc2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pc1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lbl_countdown;
        private System.Windows.Forms.Button btn_ready;
        private System.Windows.Forms.Label lbl_wait;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.PictureBox pc4;
        private System.Windows.Forms.PictureBox pc3;
        private System.Windows.Forms.PictureBox pc2;
        private System.Windows.Forms.PictureBox pc1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ListBox lB_gamer;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cb_invitenum;
        private System.Windows.Forms.Label lbl_port;
        private System.Windows.Forms.Label lbl_user;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbl_ip;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_invite;
        private System.Windows.Forms.ListBox lB_onname;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btn_logout;
    }
}