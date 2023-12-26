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
    public partial class Form2 : Form
    {
        public int invitenum = 3;
        public List<DataUser> ListDataUserRank = new List<DataUser>();
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            if (invitenum == 2)
            {
                gp3.Visible = false;
                gp4.Visible = false;
                this.Size = new Size(529, 180);
            }
            else if (invitenum == 3)
            {
                gp4.Visible = false;
                this.Size = new Size(529, 300);

            }
            else if (invitenum == 4)
            {
                this.Size = new Size(529, 300);
            }
        }

        private void gp1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(this.BackColor);
        }
    }
}
