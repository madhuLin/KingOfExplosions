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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox2P = new System.Windows.Forms.PictureBox();
            this.pictureBox1P = new System.Windows.Forms.PictureBox();
            this.buttonFocus = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2P)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1P)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pictureBox2P);
            this.panel1.Controls.Add(this.pictureBox1P);
            this.panel1.Location = new System.Drawing.Point(69, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(500, 500);
            this.panel1.TabIndex = 0;
            // 
            // pictureBox2P
            // 
            this.pictureBox2P.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2P.Image")));
            this.pictureBox2P.Location = new System.Drawing.Point(451, 449);
            this.pictureBox2P.Name = "pictureBox2P";
            this.pictureBox2P.Size = new System.Drawing.Size(46, 48);
            this.pictureBox2P.TabIndex = 1;
            this.pictureBox2P.TabStop = false;
            // 
            // pictureBox1P
            // 
            this.pictureBox1P.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1P.Name = "pictureBox1P";
            this.pictureBox1P.Size = new System.Drawing.Size(45, 54);
            this.pictureBox1P.TabIndex = 0;
            this.pictureBox1P.TabStop = false;
            // 
            // buttonFocus
            // 
            this.buttonFocus.Location = new System.Drawing.Point(594, 499);
            this.buttonFocus.Name = "buttonFocus";
            this.buttonFocus.Size = new System.Drawing.Size(23, 23);
            this.buttonFocus.TabIndex = 2;
            this.buttonFocus.UseVisualStyleBackColor = true;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(608, 28);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(120, 220);
            this.listBox1.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(917, 566);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.buttonFocus);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2P)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1P)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox2P;
        private System.Windows.Forms.PictureBox pictureBox1P;
        private System.Windows.Forms.Button buttonFocus;
        private System.Windows.Forms.ListBox listBox1;
    }
}

