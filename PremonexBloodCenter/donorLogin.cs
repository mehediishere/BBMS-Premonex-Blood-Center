using MySql.Data.MySqlClient;
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
    public partial class donorLogin : Form
    {
        public donorLogin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) //home btn
        {
            this.Hide();
            Form1 f1 = new Form1();
            f1.Show();
        }

        private void button5_Click(object sender, EventArgs e) //admin btn
        {
            this.Hide();
            adminLogin f1 = new adminLogin();
            f1.Show();
        }

        private void button3_Click(object sender, EventArgs e) //signup btn
        {
            this.Hide();
            signup f1 = new signup();
            f1.Show();
        }

        private void button2_Click(object sender, EventArgs e) //login btn
        {
            string user = txt1.Text;
            string pass = txt2.Text;

            MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;database=premonexbc");

            MySqlDataAdapter sda = new MySqlDataAdapter("select count(*) from pass_user where Email = '" + txt1.Text + "' and Password='" + txt2.Text + "'", conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows[0][0].ToString() == "1")
            {
                //MessageBox.Show("Username and Password matched", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();
                afterDonor afd = new afterDonor(txt1.Text);
                afd.Show();
            }
            else
            {
                MessageBox.Show("Incorrect Username and Password", "info", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
