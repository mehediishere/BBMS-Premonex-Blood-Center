using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PremonexBloodCenter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)  //login panel make big or small
        {
            if (panel1.Height == 44)
            {
                panel1.Height = 129;
            }
            else
            {
                panel1.Height = 44;
            }
        }

        private void button4_Click(object sender, EventArgs e)  //Become a donor button
        {
            this.Hide();
            signup sp = new signup();
            sp.Show();
        }

        private void button2_Click(object sender, EventArgs e) // admin login btn
        {
            this.Hide();
            adminLogin al = new adminLogin();
            al.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            donorLogin dl = new donorLogin();
            dl.Show();
        }
    }
}
