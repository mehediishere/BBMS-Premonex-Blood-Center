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
    public partial class adminLogin : Form
    {
        public adminLogin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) // home btn
        {
            this.Hide();
            Form1 f1 = new Form1();
            f1.Show();
        }

        private void button4_Click(object sender, EventArgs e) //donor login btn
        {
            this.Hide();
            donorLogin f1 = new donorLogin();
            f1.Show();
        }

       

        private void button2_Click(object sender, EventArgs e) //login btn
        {
            

            MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;database=premonexbc");

            MySqlDataAdapter sda = new MySqlDataAdapter("select count(*) from admin_pass where username = '" + txt1.Text + "' and password='" + txt2.Text + "' and id='"+txt3.Text+"' " , conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows[0][0].ToString() == "1")
            {
                //MessageBox.Show("Username and Password matched", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();
                afterAdmin afd = new afterAdmin();
                afd.Show();
            }
            else
            {
                MessageBox.Show("Incorrect Username and Password", "info", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
