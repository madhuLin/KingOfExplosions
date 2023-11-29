namespace KingOfExplosions
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.buttonFocus = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.pictureBox2P = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1P = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2P)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1P)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonFocus
            // 
            this.buttonFocus.Location = new System.Drawing.Point(792, 624);
            this.buttonFocus.Margin = new System.Windows.Forms.Padding(4);
            this.buttonFocus.Name = "buttonFocus";
            this.buttonFocus.Size = new System.Drawing.Size(31, 29);
            this.buttonFocus.TabIndex = 2;
            this.buttonFocus.UseVisualStyleBackColor = true;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 15;
            this.listBox1.Location = new System.Drawing.Point(752, 35);
            this.listBox1.Margin = new System.Windows.Forms.Padding(4);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(159, 274);
            this.listBox1.TabIndex = 3;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "floor.png");
            this.imageList1.Images.SetKeyName(1, "bom.png");
            this.imageList1.Images.SetKeyName(2, "p1.jpg");
            this.imageList1.Images.SetKeyName(3, "p2.png");
            this.imageList1.Images.SetKeyName(4, "floor.png");
            // 
            // pictureBox2P
            // 
            this.pictureBox2P.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2P.Location = new System.Drawing.Point(551, 551);
            this.pictureBox2P.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox2P.Name = "pictureBox2P";
            this.pictureBox2P.Size = new System.Drawing.Size(45, 45);
            this.pictureBox2P.TabIndex = 1;
            this.pictureBox2P.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImage = global::KingOfExplosions.Properties.Resources.background;
            this.panel1.Controls.Add(this.pictureBox2P);
            this.panel1.Controls.Add(this.pictureBox1P);
            this.panel1.Location = new System.Drawing.Point(92, 31);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(600, 600);
            this.panel1.TabIndex = 0;
            // 
            // pictureBox1P
            // 
            this.pictureBox1P.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1P.Location = new System.Drawing.Point(4, 4);
            this.pictureBox1P.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1P.Name = "pictureBox1P";
            this.pictureBox1P.Size = new System.Drawing.Size(45, 45);
            this.pictureBox1P.TabIndex = 0;
            this.pictureBox1P.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(976, 708);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.buttonFocus);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2P)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1P)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox2P;
        private System.Windows.Forms.PictureBox pictureBox1P;
        private System.Windows.Forms.Button buttonFocus;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ImageList imageList1;
    }
}

